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
    public class ParkingHouseStatusViewComponent : ViewComponent
    {

       private Garage_2_0_MPTContext db;
        private ParkHouse parkhouse;
        private bool loadedSeed = false;


        public ParkingHouseStatusViewComponent(Garage_2_0_MPTContext context)
        {
            this.db = context;
            int Floor = 3;
            int[] Twos = new int[3]
                { 2,3,2
                };
            int[] Threes = new int[3]
                    { 3,2,3
                    };

            parkhouse = new ParkHouse(Floor, Twos, Threes, db);
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            await InitPlots();
            var svar = GetParkingsHouseStatus();
            return View(svar);
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


                var res = await AddTimeAndPrice();
                var
                        ParkedVehicles = res.
                    Select(o => o.ParkedVehicles).Select(pw => pw.Where(pwm => pwm.ParkedVehicle.Where == null))
                    .FirstOrDefault();

                var needtosavetoo = new List<ParkedVehicle>();
        




                foreach (var item in res)
                {
                    foreach (var item2 in item.ParkedVehicles)
                    {
                        parkhouse.Park(item2.ParkedVehicle);
                        needtosavetoo.Add(item2.ParkedVehicle);
                    }
                    
                }


                try
                {
                    db.ParkedVehicle.UpdateRange(needtosavetoo);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                loadedSeed = true;
            }
        }

        private async Task<ParkedViewModel[]> AddTimeAndPrice(bool includeparkedout = false)
        {
            return await db.Vehicles
                .Include(v => v.ParkedVehicles)
                .Include(v => v.VehicleTyp)
                .Where(v => (includeparkedout || (v.ParkedVehicles != null && v.ParkedVehicles.Any(pw => pw.ParkOutDate == null))))
                            .Select(x => new ParkedViewModel()
                            {
                                ParkedVehicles = x.ParkedVehicles.Select(pv => new SubParkedViewModel
                                {
                                    ParkedVehicle = pv,

                                }).ToList(),
                                Vehicle=x,
                                VehicleTyp=x.VehicleTyp
                            })
                            .ToArrayAsync();
        }




    }
}
/*
 *         {
            var res = _context.Vehicles
                .Include(v => v.ParkedVehicles)
                .Include(v => v.VehicleTyp)
                .Include(v => v.Member);

            var res2 =  res
                .Where(v => (includeparkedout || (v.ParkedVehicles != null && v.ParkedVehicles.Any(pw => pw.ParkOutDate == null)))) ;



            var res3 = res2.Select(x => new ParkedViewModel()
            {
                ParkedVehicles =  x.ParkedVehicles.Select(pv => new SubParkedViewModel
                {
                    ParkedVehicle=pv,
                    ParkedTime = PrettyPrintTime(((pv.ParkOutDate == null) ? DateTime.Now : pv.ParkOutDate) - pv.ParkInDate),
                    Price= pv.Vehicle.VehicleTyp.CostPerHour * (int)Math.Ceiling((((pv.ParkOutDate == null) ? DateTime.Now : pv.ParkOutDate) - pv.ParkInDate).Value.TotalHours),
                    CostPerHour = pv.Vehicle.VehicleTyp.CostPerHour
                }).ToList() ,
                Vehicle = x,
                VehicleTyp = x.VehicleTyp,
                Member = x.Member  
            });
            return await  res3.ToArrayAsync();
*/