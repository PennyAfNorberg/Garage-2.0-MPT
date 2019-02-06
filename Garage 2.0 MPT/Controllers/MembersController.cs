using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_2._0_MPT.Models;
using System.Linq.Expressions;

namespace Garage_2._0_MPT.Controllers
{
    public class MembersController : Controller
    {
        private readonly Garage_2_0_MPTContext _context;

        public MembersController(Garage_2_0_MPTContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = (await AddTimeAndPrice(id.Value)).FirstOrDefault();
            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }

        private async Task<MemberViewModel[]> AddTimeAndPrice(int id)
        {
            var res = _context.Members
                .Include(v => v.Vehicles).ThenInclude(v => v.ParkedVehicles)
                .Include(v => v.Vehicles).ThenInclude(v => v.VehicleTyp)
                .Where(m => m.Id == id);



            IQueryable<MemberViewModel> res3;

             res3 = res.Select(x => new MemberViewModel()
            {
                Member = x,
                Vehicles = (x.Vehicles == null) ? null : x.Vehicles.Select(v => new SubVehicle
                {
                    Vehicle=v,
                    Vehicletype=v.VehicleTyp,
                    SubParkedViewModels=(v.ParkedVehicles==null || (!(v.ParkedVehicles.Any(pv=>pv.ParkOutDate==null))))?null: v.ParkedVehicles.Select(pv =>
                    new SubParkedViewModel
                    {
                        ParkedVehicle = pv,
                        ParkedTime = (pv == null) ? null : PrettyPrintTime(((pv.ParkOutDate == null) ? DateTime.Now : pv.ParkOutDate) - pv.ParkInDate),
                        Price = pv.Vehicle.VehicleTyp.CostPerHour * (int)Math.Ceiling((((pv.ParkOutDate == null) ? DateTime.Now : pv.ParkOutDate) - pv.ParkInDate).Value.TotalHours),
                        CostPerHour = pv.Vehicle.VehicleTyp.CostPerHour
                    }
                    ).ToList()
                }
                
                ).ToList()
             });




            if (res3.Count() == 0)
                return null;
            else

                return res3.ToArray();

            //   .ToArrayAsync();
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

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PassWord")] Member members)
        {
            if (ModelState.IsValid)
            {
                _context.Add(members);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(members);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members.FindAsync(id);
            if (members == null)
            {
                return NotFound();
            }
            return View(members);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PassWord")] Member members)
        {
            if (id != members.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(members);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembersExists(members.Id))
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
            return View(members);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var members = await _context.Members.FindAsync(id);
            _context.Members.Remove(members);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembersExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
        public async Task<IActionResult> SeekAndSort(string Message, string Sort = "Name", string SearchString = "")
        {

            Member[] reta = await _context.Members.Where(o => o.FirstName.ToLower().Contains(SearchString.ToLower())
            || o.LastName.ToLower().Contains(SearchString.ToLower())
            || o.Email.ToLower().Contains(SearchString.ToLower()))
            .OrderBy(mySort(Sort)).ToArrayAsync();
            return View("Index", reta);
        }

        private static Expression<Func<Member, string>> mySort(string sort)
        {
            if (sort.Equals("FirstName")) return s => s.FirstName;
            else if (sort.Equals("LastName")) return s => s.LastName;
            else if (sort.Equals("Email")) return s => s.Email;

            else
                return s => s.LastName;
        }
    }
}
