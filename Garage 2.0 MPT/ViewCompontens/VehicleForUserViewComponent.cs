using Garage_2._0_MPT.Models;
using Garage_2._0_MPT.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.ViewCompontens
{
    public class VehicleForUserViewComponent : ViewComponent
    {

        private Garage_2_0_MPTContext db;

        public VehicleForUserViewComponent(Garage_2_0_MPTContext context)
        {
            this.db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int MemberId)
        {
            var res = await db.Members
         .Include(v => v.Vehicles).ThenInclude(v => v.ParkedVehicles)
         .Include(v => v.Vehicles).ThenInclude(v => v.VehicleTyp)
         .Where(m => m.Id == MemberId).ToArrayAsync();

            var res2 = res.Select(x => new MemberViewModel()
            {
                Member = x,
                Vehicles = (x == null) ? null : x.Vehicles.Select(v => new SubVehicle
                {
                    Vehicle = v,
                    Vehicletype = v.VehicleTyp,
                    SubParkedViewModels = (v.ParkedVehicles == null || (!(v.ParkedVehicles.Any(pv => pv.ParkOutDate == null)))) ? null : v.ParkedVehicles.Select(pv =>
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
            }).FirstOrDefault();

            return View(res2);
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
    }
}
