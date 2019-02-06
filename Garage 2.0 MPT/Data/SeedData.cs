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

                for (int i = 0; i < 2; i++)
                {
                    string name = Faker.NameFaker.Name();

                    var member = new Member
                    {

                        FirstName = Faker.NameFaker.FirstName()
                        ,
                        LastName = Faker.NameFaker.LastName()
                        ,
                        Address = new Address
                        {
                            Street = Faker.LocationFaker.Street(),
                            ZipCode = Faker.LocationFaker.PostCode(),
                            City = Faker.LocationFaker.City(),
                        },
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
                        RegNr = Faker.StringFaker.Randomize("AAA DDD")
                        ,
                        VehicleColor = "Green"
                        ,
                        VehicleModel = "BM"
                        ,
                        VehicleBrand = Faker.CompanyFaker.BS()
                        ,
                        NumberOfWheels = (Faker.NumberFaker.Number(2) * 2) + 2
                        ,
                        MemberId = member.Id
                    };
                    Vehicles.Add(Vehicle1);
                    int i = 1;
                    while (i < Faker.NumberFaker.Number(3))
                    {
                        var Vehicle2 = new Vehicle
                        {
                            VehicleTypId = Faker.NumberFaker.Number(5) + 1
                           ,
                            RegNr = Faker.StringFaker.Randomize("/(A-Z){3}\\s[0-9]{3}")
                           ,
                            VehicleColor = "Red"
                           ,
                            VehicleModel = "911"
                           ,
                            VehicleBrand = Faker.CompanyFaker.BS()
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
                    if (Faker.NumberFaker.Number(3) == 0)
                    {
                        var ParkedVehicle = new ParkedVehicle
                        {
                            ParkInDate = Faker.DateTimeFaker.DateTime(DateTime.Now.AddDays(-2), DateTime.Now)
                           ,
                            ParkOutDate = null
                           ,
                            Where = null
                           ,
                            Position = null
                            ,MemberId=Vehicle.MemberId
                            ,VehicleId=Vehicle.Id
                        };
                        ParkedVehicles.Add(ParkedVehicle);
                    }
                }
                context.ParkedVehicle.AddRange(ParkedVehicles);
                context.SaveChanges();
            }
        }
    }
}