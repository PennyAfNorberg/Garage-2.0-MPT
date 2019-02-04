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

                var res = (await AddTimeAndPrice()).Where(p => p.Where == null).ToList();


                foreach (var item in res)
                {
                    parkhouse.Park(item);
                }


                try
                {
                    db.UpdateRange(res);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                loadedSeed = true;
            }
        }

        private async Task<ParkedVehicle[]> AddTimeAndPrice(bool includeparkedout = false)
        {
            return await db.ParkedVehicle.Where(v => (includeparkedout || v.ParkOutDate == null))
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
                                Price = x.VehicleTyp.CostPerHour * (int)Math.Ceiling((((x.ParkOutDate == null) ? DateTime.Now : x.ParkOutDate) - x.ParkInDate).Value.TotalHours),
                                CostPerHour = x.VehicleTyp.CostPerHour,
                                Where = x.Where
                            })
                            .ToArrayAsync();
        }




    }
}
