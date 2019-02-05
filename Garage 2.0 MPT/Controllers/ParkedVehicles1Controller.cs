using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_2._0_MPT.Models;

namespace Garage_2._0_MPT.Controllers
{
    public class ParkedVehicles1Controller : Controller
    {
        private readonly Garage_2_0_MPTContext _context;

        public ParkedVehicles1Controller(Garage_2_0_MPTContext context)
        {
            _context = context;
        }

        // GET: ParkedVehicles1
        public async Task<IActionResult> Index()
        {
            var garage_2_0_MPTContext = _context.ParkedVehicle.Include(p => p.Member).Include(p => p.Vehicle);
            return View(await garage_2_0_MPTContext.ToListAsync());
        }

        // GET: ParkedVehicles1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles1/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Email");
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "RegNr");
            return View();
        }

        // POST: ParkedVehicles1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParkInDate,ParkOutDate,Where,MemberId,VehicleId")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Email", parkedVehicle.MemberId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "RegNr", parkedVehicle.VehicleId);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles1/Edit/5
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Email", parkedVehicle.MemberId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "RegNr", parkedVehicle.VehicleId);
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParkInDate,ParkOutDate,Where,MemberId,VehicleId")] ParkedVehicle parkedVehicle)
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Email", parkedVehicle.MemberId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "RegNr", parkedVehicle.VehicleId);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles1/Delete/5
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
