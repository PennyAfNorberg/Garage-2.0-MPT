using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Garage_2._0_MPT.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            return;
            var options = serviceProvider.GetRequiredService<DbContextOptions<Garage_2_0_MPTContext>>();
            using (var context = new Garage_2_0_MPTContext(options))
            {

                if (context.Vehicles.Any())
                { // ändra till return efter första som funkar
                    context.Members.RemoveRange(context.Members);
                    context.Vehicles.RemoveRange(context.Vehicles);
                    context.ParkedVehicle.RemoveRange(context.ParkedVehicle);

                }
                // Let's seed!
                var members = new List<Member>();

                for (int i = 0; i < 5; i++)
                {
                    string name = Faker.NameFaker.Name();

                    var member = new Member
                    {

                        FirstName = Faker.NameFaker.FirstName()
                        ,
                        LastName = Faker.NameFaker.LastName()
                        ,
                        Street = Faker.LocationFaker.Street()
                        ,
                        ZipCode = Faker.LocationFaker.PostCode()
                        ,
                        City = Faker.LocationFaker.City()
                        ,
                        Email = Faker.InternetFaker.Email(),
                        PassWord = new byte[] { 255, 0, 123 },
                    };
                    members.Add(member);
                }

                context.Members.AddRange(members);

                var Vehicles = new List<Vehicle>();
                foreach (var member in members)
                {

                    var Vehicle1 = new Vehicle
                    {
                        VehicleTypId = Faker.NumberFaker.Number(5) + 1
                        ,
                        RegNr = Faker.StringFaker.Randomize("ABC ") + Faker.NumberFaker.Number(999)
                        ,
                        VehicleColor = myColor()
                        ,
                        VehicleModel = "BM"
                        ,
                        VehicleBrand = myBrand()
                        ,
                        NumberOfWheels = (Faker.NumberFaker.Number(2) * 2) + 2
                        ,
                        MemberId = member.Id
                    };
                    Vehicles.Add(Vehicle1);
                    int i = 1;
                    while (i < Faker.NumberFaker.Number(5))
                    {
                        var Vehicle2 = new Vehicle
                        {
                            VehicleTypId = Faker.NumberFaker.Number(5) + 1
                           ,
                            RegNr = Faker.StringFaker.Randomize("CDE ") + Faker.NumberFaker.Number(999)
                           ,
                            VehicleColor = myColor()
                           ,
                            VehicleModel = "911"
                           ,
                            VehicleBrand = myBrand()
                           ,
                            NumberOfWheels = (Faker.NumberFaker.Number(2) * 2) + 2
                           ,
                            MemberId = member.Id
                        };
                        Vehicles.Add(Vehicle2);

                        i++;
                    }

                }
                context.Vehicles.AddRange(Vehicles);

                var ParkedVehicles = new List<ParkedVehicle>();
                foreach (var Vehicle in Vehicles)
                {
                    if (Faker.NumberFaker.Number(1) == 0)
                    {
                        var ParkedVehicle = new ParkedVehicle
                        {
                            ParkInDate = Faker.DateTimeFaker.DateTime(DateTime.Now.AddDays(-4), DateTime.Now)
                           ,
                            ParkOutDate = null
                           ,
                            Where = null
                           ,
                            Position = null
                            ,
                            MemberId = Vehicle.MemberId
                            ,
                            VehicleId = Vehicle.Id
                        };
                        if (Faker.NumberFaker.Number(1) == 0 && ParkedVehicle.ParkInDate < DateTime.Now.AddDays(-2))
                        {

                            ParkedVehicle.ParkOutDate = DateTime.Now.AddDays(-1);
                        }


                        ParkedVehicles.Add(ParkedVehicle);
                    }
                }
                context.ParkedVehicle.AddRange(ParkedVehicles);
                context.SaveChanges();
            }
        }

        private static string myBrand()
        {
            List<string> brand = new List<string>();
            brand.Add("Volvo");
            brand.Add("Saab");
            brand.Add("Bmv");
            brand.Add("Alfa Romeo");
            brand.Add("Apal");
            brand.Add("Audi");
            brand.Add("Bugatti");
            brand.Add("Chrysler");
            brand.Add("Koenigsegg");
            brand.Add("Lamborghini");

            return brand[Faker.NumberFaker.Number(9)];
        }
        private static string myColor()
        {
            List<string> brand = new List<string>();
            brand.Add("Red");
            brand.Add("Grean");
            brand.Add("Black");
            brand.Add("Pink");
            brand.Add("Blue");
            brand.Add("Gray");
            brand.Add("Darkgray");
            brand.Add("Gold");
            brand.Add("Silver");
            brand.Add("Brown");

            return brand[Faker.NumberFaker.Number(9)];
        }
    }
}