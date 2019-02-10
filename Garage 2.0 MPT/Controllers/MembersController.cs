using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Garage_2._0_MPT.Models;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Garage_2._0_MPT.Controllers
{
    public class MembersController : Controller
    {
        private readonly Garage_2_0_MPTContext _context;

        private readonly UserManager<GarageUser> userManager;
        private readonly IUserClaimsPrincipalFactory<GarageUser> claimsPrincipalFactory;
        private readonly IConfiguration configuration;

        public MembersController(Garage_2_0_MPTContext context,
             UserManager<GarageUser> userManager,
            IUserClaimsPrincipalFactory<GarageUser> claimsPrincipalFactory
             , IConfiguration Configuration)
        {
            _context = context;
            this.userManager = userManager;
            this.claimsPrincipalFactory = claimsPrincipalFactory;
            this.configuration = Configuration;
          
        }

        private async Task<string> GetRole(string UserId)
        {
            var res = await _context.GarageUser.FirstOrDefaultAsync(u => u.Id == UserId);

            return res?.Role;

        }

        private async Task<int?> GetMemberid(string UserId)
        {
            var res = await _context.GarageUser.FirstOrDefaultAsync(u => u.Id == UserId);

            return res?.MemberId;

        }

        // GET: Members
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var res = await _context.Members.ToListAsync();

            var id = userManager.GetUserId(User);
            var role = await GetRole(id);
            var memberid = await GetMemberid(id);
            memberid = memberid ?? -1;

            if (role != "Admin")
            {


                res = res.Where(v => v.Id == memberid.Value).ToList();

            }



            return View(res);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var res =   _context.Members
                .Include(v => v.Vehicles)
                 .Where(m => m.Id == id)
                 ;

            var members = await res.Select(x => new MemberViewModel()
            {
                Member = x,
                Vehicles = (x.Vehicles == null) ? null : x.Vehicles.Select(v => new SubVehicle
                {
                    Vehicle = v
                }
                ).ToList()
            }).FirstOrDefaultAsync();

            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }



        // GET: Members/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }




        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Street,City,ZipCode,PassWord")] Member members)
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
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members.FindAsync(id);
            var userid = userManager.GetUserId(User);
            var curruser = await userManager.FindByIdAsync(userid);
            var role = curruser.Role;

            if (role != "Admin")
            {
                if (members.Id != curruser.MemberId)
                    members = null;
            }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Street,City,ZipCode,PassWord")] Member members)
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
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);

            var userid = userManager.GetUserId(User);
            var curruser = await userManager.FindByIdAsync(userid);
            var role = curruser.Role;

            if (role != "Admin")
            {
                if (members.Id != curruser.MemberId)
                    members = null;
            }

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
