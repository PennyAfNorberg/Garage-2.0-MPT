using System;
using System.Collections.Generic;
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
        public VehicleTyp VehicleTyp { get; set; }
        public string RegNr { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleBrand { get; set; }
        public int NumberOfWheels { get; set; }
        public DateTime ParkInDate { get; set; }
        public DateTime? ParkOutDate { get; set; }
    }

    public enum VehicleTyp
    {
        Car,
        Bus,
        Motorbike,
        Caravan,
        RV,
        Truck
    }
}
