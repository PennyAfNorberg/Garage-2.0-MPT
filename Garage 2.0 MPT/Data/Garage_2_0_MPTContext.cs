using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Garage_2._0_MPT.Models
{
    public class Garage_2_0_MPTContext : DbContext
    {
        public Garage_2_0_MPTContext (DbContextOptions<Garage_2_0_MPTContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkedVehicle>()
                .HasData(
                    new ParkedVehicle { Id = 1, VehicleTyp = VehicleTyp.Car, RegNr = "Rymdopera", VehicleColor = "Green", VehicleModel="okänd", VehicleBrand= "Ferrari", NumberOfWheels=4, ParkInDate = DateTime.Now-new TimeSpan(1,2,3,4), ParkOutDate =null},
                    new ParkedVehicle { Id = 2, VehicleTyp = VehicleTyp.Bus, RegNr = "abc 123", VehicleColor = "Red", VehicleModel = "okänd", VehicleBrand = "Volvo", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(2, 2, 3, 4), ParkOutDate = null },
                    new ParkedVehicle { Id = 3, VehicleTyp = VehicleTyp.RV , RegNr = "acc 123", VehicleColor = "Blue", VehicleModel = "okänd", VehicleBrand = "Saab", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(4, 2, 3, 4), ParkOutDate = DateTime.Now - new TimeSpan(2, 3, 0, 45) }
                );

        }


        public DbSet<Garage_2._0_MPT.Models.ParkedVehicle> ParkedVehicle { get; set; }
    }
}
