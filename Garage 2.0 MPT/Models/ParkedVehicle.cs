using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Garage_2._0_MPT.Utils;

namespace Garage_2._0_MPT.Models
{
    public class ParkedVehicle
    {
        public ParkedVehicle()
        {
            ParkInDate = DateTime.Now;
        }
        public int Id { get; set; } // Alla
        
        [Display(Name = "Parking Time")]
        public DateTime ParkInDate { get; set; } // Parked
        [Display(Name = "Leaving Time")]
        public DateTime? ParkOutDate { get; set; } // Parked


        [Display(Name = "Parking Lot")]  // Parked
        public string Where { get; set; }
        [NotMapped]
        public Position Position { get; set; } // parked


        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int VehicleId { get; set; } 
        public Vehicle Vehicle { get; set; }


    }

    public class Vehicle
    {
        public int Id { get; set; } // Alla
        [Display(Name = "Type")]
        public int VehicleTypId { get; set; } //Vech
        public VehicleTyp VehicleTyp { get; set; } //Vech
        [Display(Name = "Reg Nr")]
        [Required(ErrorMessage = "Reg nr is required")]
        public string RegNr { get; set; } //Vech
        [Display(Name = "Color")]
        public string VehicleColor { get; set; }  //Vech
        [Display(Name = "Model")]
        public string VehicleModel { get; set; }  //Vech
        [Display(Name = "Brand")]
        public string VehicleBrand { get; set; }  //Vech
        [Display(Name = "Wheels")]
        public int NumberOfWheels { get; set; }  //Vech

        public int MemberId { get; set; }
        public Member Member { get; set; }


        public ICollection<ParkedVehicle> ParkedVehicles { get; set; }


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

        public List<Vehicle> Vehicles { get; set; }
    }


    public class ParkedViewModel
    {

        public VehicleTyp VehicleTyp { get; set; } //Vech
        public Vehicle Vehicle { get; set; }
        public ParkedVehicle ParkedVehicle { get; set; }
        public Member Member { get; set; }
        [NotMapped]
        [Display(Name = "Parked Time")]
        public string ParkedTime { get; set; } // viewmodel
        [NotMapped]
        [Display(Name = "Time")]
        public int ParkedHours { get; set; } // viewmodel
        [NotMapped]
        [Display(Name = "Price")]
        public int Price { get; set; }  // viewmodel
        [NotMapped]
        [Display(Name = "CostPerHour")]
        public int CostPerHour { get; set; } // viewmodel

    }
}
