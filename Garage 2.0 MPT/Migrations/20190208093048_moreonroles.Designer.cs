﻿// <auto-generated />
using System;
using Garage_2._0_MPT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Garage_2._0_MPT.Migrations
{
    [DbContext(typeof(Garage_2_0_MPTContext))]
    [Migration("20190208093048_moreonroles")]
    partial class moreonroles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Garage_2._0_MPT.GarageUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Locale");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int>("MemberId");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Role");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("Locale");

                    b.HasIndex("MemberId");

                    b.ToTable("GarageUser");
                });

            modelBuilder.Entity("Garage_2._0_MPT.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .HasMaxLength(25);

                    b.Property<string>("LastName")
                        .HasMaxLength(25);

                    b.Property<byte[]>("PassWord");

                    b.Property<string>("Street")
                        .HasMaxLength(35);

                    b.Property<string>("ZipCode")
                        .HasMaxLength(8);

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Garage_2._0_MPT.Models.ParkedVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MemberId");

                    b.Property<DateTime?>("ParkInDate");

                    b.Property<DateTime?>("ParkOutDate");

                    b.Property<int>("VehicleId");

                    b.Property<string>("Where");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ParkedVehicle");
                });

            modelBuilder.Entity("Garage_2._0_MPT.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MemberId");

                    b.Property<int>("NumberOfWheels");

                    b.Property<string>("RegNr")
                        .IsRequired();

                    b.Property<string>("VehicleBrand");

                    b.Property<string>("VehicleColor");

                    b.Property<string>("VehicleModel");

                    b.Property<int>("VehicleTypId");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("VehicleTypId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Garage_2._0_MPT.Models.VehicleTyp", b =>
                {
                    b.Property<int>("VehicleTypId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CostPerHour");

                    b.Property<string>("Name");

                    b.Property<int>("SpacesNeeded");

                    b.HasKey("VehicleTypId");

                    b.ToTable("VehicleTyp");

                    b.HasData(
                        new
                        {
                            VehicleTypId = 1,
                            CostPerHour = 100,
                            Name = "Car",
                            SpacesNeeded = 1
                        },
                        new
                        {
                            VehicleTypId = 2,
                            CostPerHour = 300,
                            Name = "Bus",
                            SpacesNeeded = 3
                        },
                        new
                        {
                            VehicleTypId = 3,
                            CostPerHour = 50,
                            Name = "Motorbike",
                            SpacesNeeded = -3
                        },
                        new
                        {
                            VehicleTypId = 4,
                            CostPerHour = 150,
                            Name = "Caravan",
                            SpacesNeeded = 1
                        },
                        new
                        {
                            VehicleTypId = 5,
                            CostPerHour = 200,
                            Name = "RV",
                            SpacesNeeded = 2
                        },
                        new
                        {
                            VehicleTypId = 6,
                            CostPerHour = 200,
                            Name = "Truck",
                            SpacesNeeded = 2
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("IdentityUserClaim");
                });

            modelBuilder.Entity("Garage_2._0_MPT.GarageUser", b =>
                {
                    b.HasOne("Garage_2._0_MPT.Models.Member", "Member")
                        .WithMany("GarageUsers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Garage_2._0_MPT.Models.ParkedVehicle", b =>
                {
                    b.HasOne("Garage_2._0_MPT.Models.Member", "Member")
                        .WithMany("ParkedVehicles")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Garage_2._0_MPT.Models.Vehicle", "Vehicle")
                        .WithMany("ParkedVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Garage_2._0_MPT.Models.Vehicle", b =>
                {
                    b.HasOne("Garage_2._0_MPT.Models.Member", "Member")
                        .WithMany("Vehicles")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Garage_2._0_MPT.Models.VehicleTyp", "VehicleTyp")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleTypId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
