using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.Models
{    
    public class StatViewModel
    {
        public int TotalWeels { get; set; }
        [DataType(dataType: DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public int TotalIncome { get; set; }
        [DataType(dataType: DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public int TodayTotalIncome { get; set; }
        public IEnumerable<MyTypes> myTypes { get; set; }
        public int members_count { get; set; }
        public int ParkingSpaces { get; set; }
        public int FreeParkingSpaces { get; set; }
    }
    public class MyTypes
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
