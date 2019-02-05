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
            var res = await AddTimeAndPrice();
      

            var svar = new ListViewModel
            {
                ParkedVehicles = res,
                Message= "Parked Vehicles"

            };

            return View(svar);
        }



        // GET: ParkedVehicles
        public async Task<IActionResult> Overview()
        {
            var res = await AddTimeAndPrice(true);

            var svar = new ListViewModel
            {
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

            var res = await AddTimeAndPrice();




            var svar = new SingelViewModel
            {
                ParkedVehicle = res.FirstOrDefault(m => m.Id == id),
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

            };


            return View(svar);
        }

        // GET: ParkedVehicles/Create
        public async Task<IActionResult> Create()
        {
   
            var res = new CreateViewModel
            {
                ParkedVehicle = new ParkedVehicle(),
                vehicleTypes = await _context.VehicleTyp.OrderBy(vt => vt.Name).ToListAsync(),

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

            var parkedVehicle = (await AddTimeAndPrice()).FirstOrDefault(m => m.Id == id);
            await InitPlots();
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

        
        public async Task<IActionResult> ParkedCars( string Message, string Sort="Name", string SearchString = "")
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            string txt;
            if (SearchString != "") txt = $"Serch resultat of reg nr: {SearchString}";
            else txt = $"Parked Vehicles sorted by {Message}";

            var svar = new ListViewModel
            {
                ParkedVehicles = reta.Where(o => o.RegNr.ToLower().Contains(SearchString.ToLower())).OrderBy(s => Get_seek(s,Sort)),
                Message = txt
            
            };
            return View("Index", svar);           
        }

        private static string Get_seek(ParkedVehicle s, string sort)
        {
            if (sort.Equals("Name")) return s.VehicleTyp.Name;
            else if (sort.Equals("RegNr")) return s.RegNr;
            else if (sort.Equals("VehicleColor")) return s.VehicleColor;
            else if (sort.Equals("VehicleModel")) return s.VehicleModel;
            else if (sort.Equals("VehicleBrand")) return s.VehicleBrand;           
            else
            return s.VehicleTyp.Name;
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
            stat.myTypes = reta.Where(o => o.ParkOutDate == null).GroupBy(v => v.VehicleTyp.Name)
                    .Select(g => new MyTypes{Name = g.Key,Count = g.Count()});



            return View(stat);
        }
    }

}
