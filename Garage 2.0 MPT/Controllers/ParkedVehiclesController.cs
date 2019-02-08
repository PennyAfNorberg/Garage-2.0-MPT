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
            InitPlots();
        }
        private ParkingsHouseStatusViewModel GetParkingsHouseStatus()
        {

            var work = parkhouse.GetNextFreeSpaces();
            ParkingsHouseStatusViewModel svar = new ParkingsHouseStatusViewModel();
            foreach (var item in work)
            {
                svar.NextFree[TranslateSize(item.Key)] = item.Value == null ? "Full" : item.Value.ToString();
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

                var res = (await AddTimeAndPrice());
                if (res != null)
                {
                    var res2 = res.Select(o => o.ParkedVehicles.Where(pw => pw.ParkedVehicle.Where == null))
                     .Select(p => p.Select(pw => pw.ParkedVehicle))
                     .ToList();

                    var needtosavetoo = new List<ParkedVehicle>();

                    foreach (var item in res2)
                    {
                        foreach (var item2 in item)
                        {
                            parkhouse.Park(item2);
                            needtosavetoo.Add(item2);
                        }

                    }
                    try
                    {
                        _context.ParkedVehicle.UpdateRange(needtosavetoo);
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;
                    }
                    loadedSeed = true;
                }
            }
        }
        // GET: ParkedVehicles

        public async Task<IActionResult> Index()
        {
            var res = await AddTimeAndPrice();


            var svar = new ListViewModel
            {
                ParkedViewModel = (IEnumerable<ParkedViewModel>)res,
                Message = "..."

            };

            return View(svar);
        }

        // GET: ParkedVehicles
        public async Task<IActionResult> Overview()
        {
            var freespaces = parkhouse.GetFreeSpaces();

            var res = await AddTimeAndPrice(true);

            var svar = new ListViewModel
            {
                ParkedViewModel = res
            };

            return View(svar);
        }



        private string PrettyPrintTime(TimeSpan? timespan)
        {
            if (timespan == null)
                throw new ArgumentNullException();
            if (timespan.Value.Days > 1)
            {

                return $"{timespan.Value.Days}Days " + PrettyPrintTime(timespan - timespan.Value.Days * new TimeSpan(1, 0, 0, 0));
            }
            else if (timespan.Value.Days > 0)
            {
                
                return $"{timespan.Value.Days}Day " + PrettyPrintTime(timespan - timespan.Value.Days * new TimeSpan(1, 0, 0, 0));
            }
            else
            {
                return $"{timespan.Value.Hours:D2}:{timespan.Value.Minutes:D2}:{timespan.Value.Seconds:D2}";
            }
        }
        public async Task<ActionResult> VehicleDetails(int? vehicleid)
        {

            if (vehicleid == null)
            {
                return NotFound();
            }

            var res = _context.Vehicles
                .Include(v => v.VehicleTyp)
                .Include(v => v.Member)
                .Where(v => v.Id == vehicleid);

            if (res.Count() == 0)
            {
                return NotFound();
            }

            var svar = new SingelViewModel
            {
                ParkedVehicle = new ParkedViewModel
                {
                    //        ParkedVehicles = res.
                    //   Select(o => o.ParkedVehicles).Select(pw => pw.Where(pwm => pwm.ParkedVehicle.Id == id))
                    //   .FirstOrDefault().ToList(),
                    Vehicle = await res.FirstOrDefaultAsync()
                ,
                    VehicleTyp = await res.Select(v => v.VehicleTyp).FirstOrDefaultAsync()
                ,
                    Member = await res.Select(v => v.Member).FirstOrDefaultAsync()
                }

            };
            svar.ParkedVehicle.ParkedVehicles = new List<SubParkedViewModel>
            {
             new SubParkedViewModel{
                 ParkedTime =null,
                 ParkedVehicle= new ParkedVehicle
                 {
                     ParkInDate=null,
                     Where=null

                 }
             }

            };



            return View("Details", svar);
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var res = (await AddTimeAndPrice(true));
            var svar = new SingelViewModel
            {
                ParkedVehicle = new ParkedViewModel
                {
                    Vehicle = res.Select(o => o.Vehicle).Where(v => v.ParkedVehicles.Any(pv => pv.Id == id)).FirstOrDefault()
                ,
                    VehicleTyp = res.Select(o => o.VehicleTyp).Where(vt => vt.Vehicles.Any(v => v.ParkedVehicles.Any(pv => pv.Id == id))).FirstOrDefault()
                ,
                    Member = res.Select(o => o.Member).Where(m => m.Vehicles.Any(v => v.ParkedVehicles.Any(pv => pv.Id == id))).FirstOrDefault()
                }
            };
            svar.ParkedVehicle.ParkedVehicles = svar.ParkedVehicle.Vehicle.ParkedVehicles.Where(pv => pv.Id == id).Select(x => new SubParkedViewModel
            {
                ParkedVehicle = x

            }).ToList();
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
            var res = await AddTimeAndPrice(true);
            var svar = new SingelViewModel
            {
                ParkedVehicle = new ParkedViewModel
                {
                    Vehicle = res.Select(o => o.Vehicle).Where(v => v.ParkedVehicles.Any(pv => pv.Id == id)).FirstOrDefault()
                ,
                    VehicleTyp = res.Select(o => o.VehicleTyp).Where(vt => vt.Vehicles.Any(v => v.ParkedVehicles.Any(pv => pv.Id == id))).FirstOrDefault()
                ,
                    Member = res.Select(o => o.Member).Where(m => m.Vehicles.Any(v => v.ParkedVehicles.Any(pv => pv.Id == id))).FirstOrDefault()
                }
            };
            var CostPerHour = res.Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.Id == id)).Select(o => o.ParkedVehicles).Select(spv => spv.Select(pv => pv.CostPerHour)).FirstOrDefault().FirstOrDefault();
            var Price = res.Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.Id == id)).Select(o => o.ParkedVehicles).Select(spv => spv.Select(pv => pv.Price)).FirstOrDefault().FirstOrDefault();
            var ParkedTime = res.Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.Id == id)).Select(o => o.ParkedVehicles).Select(spv => spv.Select(pv => pv.ParkedTime)).FirstOrDefault().FirstOrDefault();

            svar.ParkedVehicle.ParkedVehicles = svar.ParkedVehicle.Vehicle.ParkedVehicles.Where(pv => pv.Id == id).Select(x => new SubParkedViewModel
            {
                ParkedVehicle = x,
                CostPerHour = CostPerHour,
                Price = Price,
                ParkedTime = ParkedTime

            }).ToList();

            return View(svar);
        }

        // GET: ParkedVehicles/Create
        public async Task<IActionResult> Create()
        {
            var res = new CreateViewModel
            {
                ParkedVehicle = new ParkedVehicle(),
                vehicleTypes = await _context.VehicleTyp.OrderBy(vt => vt.Name).ToListAsync(),
                Members = await _context.Members.ToListAsync()
            };
            return View(res);
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleTypId,VehicleTyp,RegNr,VehicleColor,VehicleModel,VehicleBrand,NumberOfWheels,ParkInDate,ParkOutDate,MemberId")] CreateSetViewModel parkedVehicle)
        //   public async Task<IActionResult> Create(CreateSetViewModel parkedVehicle)
        {
            var reg_bussey = await _context.ParkedVehicle.Where(v => v.Vehicle.RegNr == parkedVehicle.RegNr && v.ParkOutDate == null).ToListAsync();

            var svar = new SingelViewModel
            {
                ParkedVehicle = new ParkedViewModel
                {
                    Vehicle = new Vehicle
                    {
                        RegNr = parkedVehicle.RegNr
                    }
                }
            };
            if (reg_bussey.Count > 0)
            {
                // return RedirectToAction(nameof(Create));

                return View("NotCreate", svar);
            }

            if (ModelState.IsValid)
            {
                await InitPlots();
                // if model does not exist.
                if (!(_context.Vehicles.Any(v => v.RegNr == parkedVehicle.RegNr)))
                {
                    var initVehicle = new Vehicle
                    {
                        VehicleTypId = parkedVehicle.VehicleTypId,
                        RegNr = parkedVehicle.RegNr,
                        VehicleColor = parkedVehicle.VehicleColor,
                        VehicleModel = parkedVehicle.VehicleModel,
                        VehicleBrand = parkedVehicle.VehicleBrand,
                        NumberOfWheels = parkedVehicle.NumberOfWheels,
                        MemberId = parkedVehicle.MemberId
                    };
                    _context.Vehicles.Add(initVehicle);
                    await _context.SaveChangesAsync();
                }

                parkedVehicle.VehicleTyp = await _context.VehicleTyp.Where(v => v.VehicleTypId == parkedVehicle.VehicleTypId).FirstOrDefaultAsync();

                ParkedVehicle InparkedVehicle = new ParkedVehicle
                {
                    MemberId = parkedVehicle.MemberId,
                    Member = await _context.Members.Where(m => m.Id == parkedVehicle.MemberId).FirstOrDefaultAsync(),
                    VehicleId = await _context.Vehicles.Where(m => m.RegNr == parkedVehicle.RegNr).Select(m => m.Id).FirstOrDefaultAsync(),
                    Vehicle = await _context.Vehicles.Where(m => m.RegNr == parkedVehicle.RegNr).FirstOrDefaultAsync()

                };

                if (parkhouse.Park(InparkedVehicle))
                {
                    _context.Add(InparkedVehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = InparkedVehicle.Id });
                }
                else
                {

                    return View("GarageFull", svar);
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
        public async Task<IActionResult> Parkthis(int vehicleid)
        {
            var thisvehicle = await _context.Vehicles.Where(v => v.Id == vehicleid).FirstOrDefaultAsync();
            var memberid = thisvehicle.MemberId;
            ParkedVehicle InparkedVehicle = new ParkedVehicle
            {
                MemberId = memberid,
                Member = await _context.Members.Where(m => m.Id == memberid).FirstOrDefaultAsync(),
                VehicleId = vehicleid,
                Vehicle = thisvehicle
            };
            if (parkhouse.Park(InparkedVehicle))
            {
                _context.Add(InparkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = InparkedVehicle.Id });
            }
            else
            {
                var svar = new SingelViewModel
                {
                    ParkedVehicle = new ParkedViewModel
                    {
                        Vehicle = new Vehicle
                        {
                            RegNr = thisvehicle.RegNr
                        }
                    }
                };
                return View("GarageFull", svar);
            }
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
            var res = await AddTimeAndPrice();

            var parkedVehicle = res.
                    Select(o => o.ParkedVehicles).Select(pw => pw.Where(pwm => pwm.ParkedVehicle.Id == id))
                    .FirstOrDefault(p => p.Any(pv => pv.ParkedVehicle.Id == id))
                    .FirstOrDefault(p => p != null)
                    .ParkedVehicle;

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
            var reta = await AddTimeAndPrice();
            return View("Index", reta);
        }
        private async Task<ParkedViewModel[]> AddTimeAndPrice(bool includeparkedout = false)
        {
            var res = _context.Vehicles
                .Include(v => v.ParkedVehicles)
                .Include(v => v.VehicleTyp)
                .Include(v => v.Member);

            var res2 = res
                .Where(v => (includeparkedout || (v.ParkedVehicles != null && v.ParkedVehicles.Any(pw => pw.ParkOutDate == null))));

            IQueryable<ParkedViewModel> res3;

            res3 = res2.Select(x => new ParkedViewModel()
            {
                ParkedVehicles = (x.ParkedVehicles == null) ? null : x.ParkedVehicles.Select(pv => new SubParkedViewModel
                {
                    ParkedVehicle = pv,
                    ParkedTime = (pv == null) ? null : PrettyPrintTime(((pv.ParkOutDate == null) ? DateTime.Now : pv.ParkOutDate) - pv.ParkInDate),
                    Price = pv.Vehicle.VehicleTyp.CostPerHour * (int)Math.Ceiling((((pv.ParkOutDate == null) ? DateTime.Now : pv.ParkOutDate) - pv.ParkInDate).Value.TotalHours),
                    CostPerHour = pv.Vehicle.VehicleTyp.CostPerHour
                }).ToList(),
                Vehicle = x,
                VehicleTyp = x.VehicleTyp,
                Member = x.Member
            });

            if (res3.Count() == 0)
                return null;
            else
                return res3.ToArray();
        }

        public async Task<IActionResult> SeekAndSort(string Message, string Sort = "Name", string SearchString = "")
        {
            var reta = await AddTimeAndPrice();
            string txt;
            if (SearchString != "") txt = $"Serch resultat of reg nr: {SearchString}";
            else txt = $"Sorted by {Message}";

            var svar = new ListViewModel
            {
                ParkedViewModel = reta.Where(o => o.Vehicle.VehicleTyp.Name.ToLower().Equals(SearchString.ToLower())
                || o.Vehicle.RegNr.ToLower().Contains(SearchString.ToLower())).OrderBy(s => Get_seek(s, Sort)),
                Message = txt
            };
            return View("Index", svar);
        }

        private static string Get_seek(ParkedViewModel s, string sort)
        {
            if (sort.Equals("Name")) return s.Vehicle.VehicleTyp.Name;
            else if (sort.Equals("FirstName")) return s.Member.LastName;
            else if (sort.Equals("RegNr")) return s.Vehicle.RegNr;
            else if (sort.Equals("VehicleColor")) return s.Vehicle.VehicleColor;
            else if (sort.Equals("VehicleModel")) return s.Vehicle.VehicleModel;
            else if (sort.Equals("VehicleBrand")) return s.Vehicle.VehicleBrand;
            else
                return s.Vehicle.VehicleTyp.Name;
        }

        public async Task<IActionResult> Statistik()
        {
            var reta = await AddTimeAndPrice(true);
            StatViewModel stat = new StatViewModel();
            stat.TotalWeels = reta
                .Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.ParkOutDate == null))
                .Select(o => o.Vehicle.NumberOfWheels)
                .Sum();

            stat.TotalIncome = reta
                .Select(o => o.ParkedVehicles)
                .Select(p => p.Select(pw => pw.Price).Sum())
                .Sum();
            stat.TodayTotalIncome = stat.TotalIncome -
                reta
                 .Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.ParkOutDate?.Date <= DateTime.Now.Date.AddDays(-1)))
                 .Select(p => p.ParkedVehicles.Select(pw => pw.Price).Sum())
                 .Sum();
            stat.myTypes = reta
                .Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.ParkOutDate == null))
                .GroupBy(v => v.VehicleTyp.Name)
                .Select(g => new MyTypes { Name = g.Key, Count = g.Count() });

            stat.members_count = _context.Members.Count();

            stat.ParkingSpaces = parkhouse.GetSpaces();
            stat.FreeParkingSpaces = parkhouse.GetFreeSpaces();

            return View(stat);
        }
    }

}
