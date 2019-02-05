using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage_2._0_MPT.Models;

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

            modelBuilder.Entity<Vehicle>()
                .HasOne(Pv => Pv.VehicleTyp)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => new { v.VehicleTypId });


            modelBuilder.Entity<VehicleTyp>()
                .HasData(
                    new VehicleTyp { VehicleTypId = 1, Name = "Car", CostPerHour = 100, SpacesNeeded=1 },
                    new VehicleTyp { VehicleTypId = 2, Name = "Bus", CostPerHour = 300, SpacesNeeded = 3 },
                    new VehicleTyp { VehicleTypId = 3, Name = "Motorbike", CostPerHour = 50, SpacesNeeded = -3 },
                    new VehicleTyp { VehicleTypId = 4, Name = "Caravan", CostPerHour = 150, SpacesNeeded = 1 },
                    new VehicleTyp { VehicleTypId = 5, Name = "RV", CostPerHour = 200, SpacesNeeded = 2 },
                    new VehicleTyp { VehicleTypId = 6, Name = "Truck", CostPerHour = 200, SpacesNeeded = 2 }

                );


            modelBuilder.Entity<Vehicle>()
                .HasData(
                    new Vehicle { Id = 1, VehicleTypId = 1, RegNr = "abc123", VehicleColor = "Green", VehicleModel = "okänd", VehicleBrand = "Ferrari", NumberOfWheels = 4  },
                    new Vehicle { Id = 2, VehicleTypId = 2, RegNr = "abc234", VehicleColor = "Red", VehicleModel = "okänd", VehicleBrand = "Volvo", NumberOfWheels = 4  },
                    new Vehicle { Id = 3, VehicleTypId = 5, RegNr = "abc345", VehicleColor = "Blue", VehicleModel = "okänd", VehicleBrand = "Saab", NumberOfWheels = 4  },
                    new Vehicle { Id = 4, VehicleTypId = 1, RegNr = "abc456", VehicleColor = "Green", VehicleModel = "okänd", VehicleBrand = "Ferrari", NumberOfWheels = 4},
                    new Vehicle { Id = 5, VehicleTypId = 2, RegNr = "abc567", VehicleColor = "Red", VehicleModel = "okänd", VehicleBrand = "Volvo", NumberOfWheels = 4},
                    new Vehicle { Id = 6, VehicleTypId = 5, RegNr = "abc678", VehicleColor = "Black", VehicleModel = "okänd", VehicleBrand = "Saab", NumberOfWheels = 4 },
                    new Vehicle { Id = 7, VehicleTypId = 1, RegNr = "Rymdopera", VehicleColor = "Green", VehicleModel = "okänd", VehicleBrand = "Ferrari", NumberOfWheels = 4 },
                    new Vehicle { Id = 8, VehicleTypId = 2, RegNr = "abc987", VehicleColor = "Red", VehicleModel = "okänd", VehicleBrand = "Volvo", NumberOfWheels = 4 },
                    new Vehicle { Id = 9, VehicleTypId = 5, RegNr = "Biffen", VehicleColor = "Black", VehicleModel = "okänd", VehicleBrand = "Bmv", NumberOfWheels = 4 }
                );

        }
      

        public DbSet<ParkedVehicle> ParkedVehicle { get; set; }

        public DbSet<VehicleTyp> VehicleTyp { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
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
