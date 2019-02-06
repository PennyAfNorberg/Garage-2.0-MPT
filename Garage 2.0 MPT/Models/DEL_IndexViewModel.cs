using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.Models
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Type")]
        public string VehicleTyp { get; set; }
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
        [Display(Name = "Parked Time")]
        public string ParkedTime { get; set; }
        [Display(Name = "Price")]
        public int ParkedHours { get; set; }
        [Display(Name = "CostPerHour")]
        public int? CostPerHour { get; set; }
    }
}
