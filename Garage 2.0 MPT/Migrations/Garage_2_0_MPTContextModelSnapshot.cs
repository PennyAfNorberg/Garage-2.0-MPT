﻿// <auto-generated />
using System;
using Garage_2._0_MPT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Garage_2._0_MPT.Migrations
{
    [DbContext(typeof(Garage_2_0_MPTContext))]
    partial class Garage_2_0_MPTContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Garage_2._0_MPT.Models.ParkedVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfWheels");

                    b.Property<DateTime>("ParkInDate");

                    b.Property<DateTime?>("ParkOutDate");

                    b.Property<string>("RegNr");

                    b.Property<string>("VehicleBrand");

                    b.Property<string>("VehicleColor");

                    b.Property<string>("VehicleModel");

                    b.Property<int>("VehicleTyp");

                    b.HasKey("Id");

                    b.ToTable("ParkedVehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
