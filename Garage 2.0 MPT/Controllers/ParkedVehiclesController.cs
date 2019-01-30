﻿using System;
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

        public ParkedVehiclesController(Garage_2_0_MPTContext context)
        {
            _context = context;
        }



        // GET: ParkedVehicles
        
        public async Task<IActionResult> Index()
        {          
            var res = await AddTimeAndPrice();
            return View(res);
        }
        // GET: ParkedVehicles
        public async Task<IActionResult> Overview()
        {
            var res = await AddTimeAndPrice();
            return View(res);
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

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Receipt
        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = (await AddTimeAndPrice(true)).FirstOrDefault(m => m.Id == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public async Task<IActionResult> Create()
        {

            var res = new CreateViewModel
            {
                ParkedVehicle = new ParkedVehicle(),
                vehicleTypes = await _context.VehicleTyp.OrderBy(vt => vt.Name).ToListAsync()

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
            if (reg_bussey.Count > 0)
            {
                // return RedirectToAction(nameof(Create));
                return View("NotCreate");
            }
            if (ModelState.IsValid )
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = parkedVehicle.Id });
            }
            return View(parkedVehicle);
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

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            parkedVehicle.ParkOutDate = DateTime.Now;
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
                                Id=x.Id,
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
                                CostPerHour = x.VehicleTyp.CostPerHour
                            })
                            .ToArrayAsync();
        }

        
        public async Task<IActionResult> ParkedCars(string SearchString)
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();           
            return View("ParkedCars",reta.Where(o => o.RegNr.ToLower().Contains(SearchString.ToLower())));
        }
        public async Task<IActionResult> SortTyp()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();          
            return View("Index", reta.OrderBy(o => o.VehicleTyp.Name));
        }
       
        public async Task<IActionResult> SortReg()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            return View("Index", reta.OrderBy(o => o.RegNr));
        }
        
        public async Task<IActionResult> SortCol()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            return View("Index", reta.OrderBy(o => o.VehicleColor));
        }
        
        public async Task<IActionResult> SortMod()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            return View("Index", reta.OrderBy(o => o.VehicleModel));
        }
        
        public async Task<IActionResult> SortBrand()
        {
            ParkedVehicle[] reta = await AddTimeAndPrice();
            return View("Index",reta.OrderBy(o => o.VehicleBrand));
        }

        public async Task<IActionResult> Labb()
        {
            int Floor = 2;
            int[] Twos = new int[2]
                { 2,3
                };
            int[] Threes = new int[2]
                    { 3,2
                    };

            Parkhouse parkhouse = new Parkhouse(Floor, Twos, Threes, _context);

            var Parkthese = await AddTimeAndPrice(true);
            foreach(var item  in Parkthese)
            {
                parkhouse.Park(item);
            }

           var res = parkhouse.getNextFreeSpaces();
            var res2 = parkhouse.GetOccupidePositions();
            return View("Labb", res);
        }
        public async Task<IActionResult> Statistik()
        {
            var reta = await AddTimeAndPrice(true);
            var reta_no = await AddTimeAndPrice();
            //   reta.Select(o => o.NumberOfWheels).Sum();
            StatViewModel stat = new StatViewModel();
            stat.TotalWeels = reta_no.Select(o => o.NumberOfWheels).Sum();
            stat.TotalIncome = reta.Select(o => o.Price).Sum() ;
           
            stat.TodayTotalIncome = reta.Where(o=>o.ParkInDate.Date == DateTime.Now.Date).Select(o => o.Price).Sum();
            
            

            return View(stat);
        }
    }
}
