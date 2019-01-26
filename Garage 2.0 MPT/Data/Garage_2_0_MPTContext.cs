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
                .HasOne(Pv => Pv.VehicleTyp)
                .WithMany(c => c.ParkedVehicle)
                .HasForeignKey(v => new { v.VehicleTypId });
            modelBuilder.Entity<VehicleTyp>()
                .HasData(
                    new VehicleTyp { VehicleTypId = 1, Name = "Car", CostPerHour = 100 },
                    new VehicleTyp { VehicleTypId = 2, Name = "Bus", CostPerHour = 300 },
                    new VehicleTyp { VehicleTypId = 3, Name = "Motorbike", CostPerHour = 50 },
                    new VehicleTyp { VehicleTypId = 4, Name = "Caravan", CostPerHour = 150 },
                    new VehicleTyp { VehicleTypId = 5, Name = "RV", CostPerHour = 200 },
                    new VehicleTyp { VehicleTypId = 6, Name = "Truck", CostPerHour = 200 }

                );


            modelBuilder.Entity<ParkedVehicle>()
                .HasData(
                    new ParkedVehicle { Id = 1, VehicleTypId = 1, RegNr = "Rymdopera", VehicleColor = "Green", VehicleModel="okänd", VehicleBrand= "Ferrari", NumberOfWheels=4, ParkInDate = DateTime.Now-new TimeSpan(1,2,3,4), ParkOutDate =null},
                    new ParkedVehicle { Id = 2, VehicleTypId = 2, RegNr = "abc 123", VehicleColor = "Red", VehicleModel = "okänd", VehicleBrand = "Volvo", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(2, 2, 3, 4), ParkOutDate = null },
                    new ParkedVehicle { Id = 3, VehicleTypId = 5 , RegNr = "acc 123", VehicleColor = "Blue", VehicleModel = "okänd", VehicleBrand = "Saab", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(4, 2, 3, 4), ParkOutDate = DateTime.Now - new TimeSpan(2, 3, 0, 45) }
                );

        }


        public DbSet<Garage_2._0_MPT.Models.ParkedVehicle> ParkedVehicle { get; set; }

        public DbSet<Garage_2._0_MPT.Models.VehicleTyp> VehicleTyp { get; set; }
    }
}
/*
public enum VehicleTyp
{
    Car,
    Bus,
    Motorbike,
    Caravan,
    RV,
    Truck
}
*/
