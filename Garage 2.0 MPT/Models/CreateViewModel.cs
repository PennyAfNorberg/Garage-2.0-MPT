using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.Models
{
    public class CreateViewModel
    {
        public ParkedVehicle ParkedVehicle { get; set; }
        public List<VehicleTyp>  vehicleTypes { get; set; }
        public List<Member> Members { get; set; }
        public Member Member { get; set; }
    }

    public class CreateSetViewModel
    {
        public int Id { get; set; }
        public int VehicleTypId { get; set; } //Vech
        public VehicleTyp VehicleTyp { get; set; } //Vech
        public string RegNr { get; set; } //Vech
        public string VehicleColor { get; set; }  //Vech
        public string VehicleModel { get; set; }  //Vech
        public string VehicleBrand { get; set; }  //Vech
        public int NumberOfWheels { get; set; }  //V
        public DateTime ParkInDate { get; set; } // Parked
        
        public DateTime? ParkOutDate { get; set; } // Parked
        public int MemberId { get; set; }

    }

}
