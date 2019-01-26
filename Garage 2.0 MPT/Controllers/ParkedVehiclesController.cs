using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var res = _context.ParkedVehicle.Where(v => v.ParkOutDate==null).Select(
                v => new IndexViewModel
                {
                    Id = v.Id,
                    VehicleTyp = v.VehicleTyp.Name,
                    RegNr = v.RegNr,
                    VehicleColor = v.VehicleColor,
                    VehicleModel = v.VehicleModel,
                    VehicleBrand = v.VehicleBrand,
                    NumberOfWheels = v.NumberOfWheels,
                    ParkedTime = PrettyPrintTime(((v.ParkOutDate == null) ? DateTime.Now : v.ParkOutDate) - v.ParkInDate),
                    ParkedHours = v.VehicleTyp.CostPerHour*(int)Math.Ceiling((((v.ParkOutDate == null) ? DateTime.Now : v.ParkOutDate) - v.ParkInDate).Value.TotalHours),
                    CostPerHour = v.VehicleTyp.CostPerHour
                    //(v.ParkOutDate?DateTime.Now-v.ParkInDate).toString()
                }
                );


            return View(await res.ToListAsync());
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

                   return $"{timespan.Value.Hours:D2}:{timespan.Value.Minutes:D2}:{timespan.Value.Seconds:D2}" ;

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

            var parkedVehicle = await _context.ParkedVehicle
                .Select(
                v => new IndexViewModel
                {
                    Id = v.Id,
                    VehicleTyp = v.VehicleTyp.Name,
                    RegNr = v.RegNr,
                    VehicleColor = v.VehicleColor,
                    VehicleModel = v.VehicleModel,
                    VehicleBrand = v.VehicleBrand,
                    NumberOfWheels = v.NumberOfWheels,
                    ParkedTime = PrettyPrintTime(((v.ParkOutDate == null) ? DateTime.Now : v.ParkOutDate) - v.ParkInDate),
                    ParkedHours = v.VehicleTyp.CostPerHour * (int)Math.Ceiling((((v.ParkOutDate == null) ? DateTime.Now : v.ParkOutDate) - v.ParkInDate).Value.TotalHours),
                    CostPerHour= v.VehicleTyp.CostPerHour
                    //(v.ParkOutDate?DateTime.Now-v.ParkInDate).toString()
                }
                ).FirstOrDefaultAsync(m => m.Id == id);
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
                vehicleTypes = await _context.VehicleTyp.ToListAsync()

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
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var parkedVehicle= await _context.ParkedVehicle
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
    }
}
