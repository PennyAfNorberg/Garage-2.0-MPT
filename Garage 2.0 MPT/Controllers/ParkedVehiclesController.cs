using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_2._0_MPT.Utils;

namespace Garage_2._0_MPT.Models
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage_2_0_MPTContext _context;
        private ParkHouse parkhouse;
        private bool loadedSeed = false;

        public ParkedVehiclesController(Garage_2_0_MPTContext context)
        {
            _context = context;
            int Floor = 3;
            int[] Twos = new int[3]
                { 2,3,2
                };
            int[] Threes = new int[3]
                    { 3,2,3
                    };

            parkhouse = new ParkHouse(Floor, Twos, Threes, _context);
   
        }
        private ParkingsHouseStatusViewModel GetParkingsHouseStatus()
        {

            var work = parkhouse.GetNextFreeSpaces();
            ParkingsHouseStatusViewModel svar = new ParkingsHouseStatusViewModel();
            foreach (var item in work)
            {
                svar.NextFree[TranslateSize(item.Key)] = item.Value==null?"Full":item.Value.ToString();
            }

            return svar;

        }

        private string TranslateSize(int Size)
        {
            switch (Size)
            {
                case -3:
                      return "MC";
                case 1:
                    return "Car";
                case 2:
                    return "Truck";
                case 3:
                    return "Bus";
                default:
                    return "";
            }
        }

        private async Task InitPlots()
        {
            if (!loadedSeed)
            {

                var res = (await AddTimeAndPrice()).Where(p => p.Where == null).ToList();
 

                foreach (var item in res)
                {
                    parkhouse.Park(item);
                }

    
                try
                {
                    _context.UpdateRange(res);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                loadedSeed = true;
            }
        }


        // GET: ParkedVehicles

        public async Task<IActionResult> Index()
        {
            await InitPlots();
            var res = await AddTimeAndPrice();
            ViewData["PHouseStatus"] = GetParkingsHouseStatus();

            var svar = new ListViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicles = res
            };

            return View(svar);
        }



        // GET: ParkedVehicles
        public async Task<IActionResult> Overview()
        {
            await InitPlots();
            var res = await AddTimeAndPrice();

            var svar = new ListViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicles = res
            };

            return View(svar);
        }       
        
        

        private string PrettyPrintTime(TimeSpan? timespan)
        {
            if (timespan == null)
                throw new ArgumentNullException();

            if (timespan.Value.Days > 0)
            {
                return $"{timespan.Value.Days} d " + PrettyPrintTime(timespan - timespan.Value.Days * new TimeSpan(1, 0, 0, 0));
            }
            else
            {

                return $"{timespan.Value.Hours:D2}:{timespan.Value.Minutes:D2}:{timespan.Value.Seconds:D2}";

            }


        }



        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await InitPlots();
            var res = await AddTimeAndPrice();




            var svar = new SingelViewModel
            {
                ParkedVehicle = res.FirstOrDefault(m => m.Id == id),
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus()
            };

            //await RePark();
            if (svar.ParkedVehicle == null)
            {
                return NotFound();
            }

            return View(svar);
        }

        // GET: ParkedVehicles/Receipt
        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await InitPlots();
            var svar = new SingelViewModel
            {
                ParkedVehicle = (await AddTimeAndPrice(true)).FirstOrDefault(m => m.Id == id),
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus()
            };


            return View(svar);
        }

        // GET: ParkedVehicles/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PHouseStatus"] = GetParkingsHouseStatus();
            var res = new CreateViewModel
            {
                ParkedVehicle = new ParkedVehicle(),
                vehicleTypes = await _context.VehicleTyp.OrderBy(vt => vt.Name).ToListAsync(),
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus()
            };

            return View(res);
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleTypId,VehicleTyp,RegNr,VehicleColor,VehicleModel,VehicleBrand,NumberOfWheels,ParkInDate,ParkOutDate")] ParkedVehicle parkedVehicle)
        {
            var reg_bussey = await _context.ParkedVehicle.Where(v=>v.RegNr == parkedVehicle.RegNr && v.ParkOutDate == null).ToListAsync();

            var svar = new SingelViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicle = new ParkedVehicle
                {
                    RegNr = parkedVehicle.RegNr
                }
               };


            if (reg_bussey.Count > 0)
            {
                // return RedirectToAction(nameof(Create));
                return View("NotCreate",svar);
            }
            if (ModelState.IsValid )
            {
                await InitPlots();
                parkedVehicle.VehicleTyp = await _context.VehicleTyp.Where(v => v.VehicleTypId == parkedVehicle.VehicleTypId).FirstOrDefaultAsync();

                if (parkhouse.Park(parkedVehicle))
                {
                    _context.Add(parkedVehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = parkedVehicle.Id });
                }
                else
                {

                    return View("GarageFull",svar);
                }
            }



            return View(svar);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleTypId,VehicleTyp,RegNr,VehicleColor,VehicleModel,VehicleBrand,NumberOfWheels,ParkInDate,ParkOutDate")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }


        public async Task<IActionResult> Check_Out(int? id)
        {
            await InitPlots();

            var parkedVehicle = (await AddTimeAndPrice()).FirstOrDefault(m => m.Id == id);
            parkedVehicle.ParkOutDate = DateTime.Now;
            try
            {

                parkhouse.Leave(parkedVehicle);
                _context.Update(parkedVehicle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkedVehicleExists(parkedVehicle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Receipt), new { id = id });
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Test(string SearchString)
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            return View("Index", reta);
        }

        private async Task<ParkedVehicle[]> AddTimeAndPrice( bool includeparkedout = false)
        {
            return await _context.ParkedVehicle.Where(v => (includeparkedout || v.ParkOutDate == null))
                            .Select(x => new ParkedVehicle()
                            {
                                Id = x.Id,
                                VehicleTyp = x.VehicleTyp,
                                RegNr = x.RegNr,
                                VehicleColor = x.VehicleColor,
                                VehicleModel = x.VehicleModel,
                                VehicleBrand = x.VehicleBrand,
                                NumberOfWheels = x.NumberOfWheels,
                                ParkInDate = x.ParkInDate,
                                ParkOutDate = x.ParkOutDate,
                                ParkedTime = PrettyPrintTime(((x.ParkOutDate == null) ? DateTime.Now : x.ParkOutDate) - x.ParkInDate),
                                Price = x.VehicleTyp.CostPerHour * (int)Math.Ceiling((((x.ParkOutDate == null) ? DateTime.Now : x.ParkOutDate) - x.ParkInDate).Value.TotalHours),
                                CostPerHour = x.VehicleTyp.CostPerHour,
                                Where = x.Where
                            })
                            .ToArrayAsync();
        }

        
        public async Task<IActionResult> ParkedCars(string SearchString)
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            var svar = new ListViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicles =reta.Where(o => o.RegNr.ToLower().Contains(SearchString.ToLower()))
            };

            return View("ParkedCars",svar);
            //return View("ParkedCars",reta.Where(o => o.RegNr.ToLower().Contains(SearchString.ToLower())));
        }
        public async Task<IActionResult> SortTyp()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            var svar = new ListViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicles = reta.OrderBy(o => o.VehicleTyp.Name)
            };
            return View("Index", svar);
         //   return View("Index", reta.OrderBy(o => o.VehicleTyp.Name));
        }
       
        public async Task<IActionResult> SortReg()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            var svar = new ListViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicles = reta.OrderBy(o => o.RegNr)
            };
            return View("Index", svar);
            //return View("Index", reta.OrderBy(o => o.RegNr));
        }
        
        public async Task<IActionResult> SortCol()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            var svar = new ListViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicles = reta.OrderBy(o => o.VehicleColor)
            };
            return View("Index", svar);
           // return View("Index", reta.OrderBy(o => o.VehicleColor));
        }
        
        public async Task<IActionResult> SortMod()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            var svar = new ListViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicles = reta.OrderBy(o => o.VehicleModel)
            };
            return View("Index", svar);
           // return View("Index", reta.OrderBy(o => o.VehicleModel));
        }
        
        public async Task<IActionResult> SortBrand()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            var svar = new ListViewModel
            {
                ParkingsHouseStatusViewModel = GetParkingsHouseStatus(),
                ParkedVehicles = reta.OrderBy(o => o.VehicleBrand)
            };
            return View("Index", svar);
           // return View("Index",reta.OrderBy(o => o.VehicleBrand));
        }

        public async Task<IActionResult> Statistik()
        {
            var reta = await AddTimeAndPrice(true);
           // var reta_no = await AddTimeAndPrice();
            //   reta.Select(o => o.NumberOfWheels).Sum();
            StatViewModel stat = new StatViewModel();
            stat.TotalWeels = reta.Where(o=>o.ParkOutDate==null).Select(o => o.NumberOfWheels).Sum();
            stat.TotalIncome = reta.Select(o => o.Price).Sum() ;
                      
            stat.TodayTotalIncome = stat.TotalIncome - reta.Where(o => o.ParkOutDate?.Date <= DateTime.Now.Date.AddDays(-1)).Select(o => o.Price).Sum();
     

            stat.ParkingsHouseStatusViewModel = GetParkingsHouseStatus();

            return View(stat);
        }
    }
}
