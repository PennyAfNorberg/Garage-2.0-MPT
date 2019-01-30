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
                    new ParkedVehicle { Id = 1, VehicleTypId = 1, RegNr = "abc123", VehicleColor = "Green", VehicleModel="okänd", VehicleBrand= "Ferrari", NumberOfWheels=4, ParkInDate = DateTime.Now-new TimeSpan(1,2,3,4), ParkOutDate =null},
                    new ParkedVehicle { Id = 2, VehicleTypId = 2, RegNr = "abc234", VehicleColor = "Red", VehicleModel = "okänd", VehicleBrand = "Volvo", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(2, 2, 3, 4), ParkOutDate = null },
                    new ParkedVehicle { Id = 3, VehicleTypId = 5, RegNr = "abc345", VehicleColor = "Blue", VehicleModel = "okänd", VehicleBrand = "Saab", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(4, 2, 3, 4), ParkOutDate = DateTime.Now - new TimeSpan(2, 3, 0, 45) },
                    new ParkedVehicle { Id = 4, VehicleTypId = 1, RegNr = "abc456", VehicleColor = "Green", VehicleModel = "okänd", VehicleBrand = "Ferrari", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(1, 3, 3, 4), ParkOutDate = null },
                    new ParkedVehicle { Id = 5, VehicleTypId = 2, RegNr = "abc567", VehicleColor = "Red", VehicleModel = "okänd", VehicleBrand = "Volvo", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(2, 3, 3, 4), ParkOutDate = null },
                    new ParkedVehicle { Id = 6, VehicleTypId = 5, RegNr = "abc678", VehicleColor = "Black", VehicleModel = "okänd", VehicleBrand = "Saab", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(4, 2, 3, 4), ParkOutDate = null },
                    new ParkedVehicle { Id = 7, VehicleTypId = 1, RegNr = "Rymdopera", VehicleColor = "Green", VehicleModel = "okänd", VehicleBrand = "Ferrari", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(1, 2, 3, 5), ParkOutDate = null },
                    new ParkedVehicle { Id = 8, VehicleTypId = 2, RegNr = "abc987", VehicleColor = "Red", VehicleModel = "okänd", VehicleBrand = "Volvo", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(2, 2, 3, 6), ParkOutDate = null },
                    new ParkedVehicle { Id = 9, VehicleTypId = 5, RegNr = "Biffen", VehicleColor = "Black", VehicleModel = "okänd", VehicleBrand = "Bmv", NumberOfWheels = 4, ParkInDate = DateTime.Now - new TimeSpan(4, 2, 3, 7), ParkOutDate = null }
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
