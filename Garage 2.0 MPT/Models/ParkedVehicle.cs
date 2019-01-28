﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.Models
{
    public class ParkedVehicle
    {
        public ParkedVehicle()
        {
            ParkInDate = DateTime.Now;
        }
        public int Id { get; set; }
        [Display(Name = "Type")]
        public int VehicleTypId { get; set; }
        public VehicleTyp VehicleTyp { get; set; }
        [Display(Name = "Reg Nr")]
        public string RegNr { get; set; }
        [Display(Name = "Color")]
        public string VehicleColor { get; set; }
        [Display(Name = "Model")]
        public string VehicleModel { get; set; }
        [Display(Name = "Brand")]
        public string VehicleBrand { get; set; }
        [Display(Name = "Wheels")]
        public int NumberOfWheels { get; set; }
        [Display(Name = "Parking Time")]
        public DateTime ParkInDate { get; set; }
        [Display(Name = "Leaving Time")]
        public DateTime? ParkOutDate { get; set; }
        [NotMapped]
        [Display(Name = "Parked Time")]
        public string ParkedTime { get; set; }
        [NotMapped]
        [Display(Name = "Time")]
        public int ParkedHours { get; set; }
        [NotMapped]
        [Display(Name = "Price")]
        public int Price { get; set; }  
        [NotMapped]
        [Display(Name = "CostPerHour")]
        public int CostPerHour { get; set; }

        [Display(Name = "Where")]
        public string Where { get; set; }


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
     public class VehicleTyp
    {
        public int VehicleTypId { get; set; }
        public string Name { get; set; }
        public int CostPerHour { get; set; }
        public int SpacesNeeded { get; set; } // - => 1/

        public List<ParkedVehicle> ParkedVehicle { get; set; }
    }
}
