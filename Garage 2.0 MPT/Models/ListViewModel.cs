using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2._0_MPT.Models
{
    public class ListViewModel
    {
        public IEnumerable<ParkedViewModel> ParkedViewModel { get; set; }
        public ParkedViewModel Header { get; set; }
        public string Message { get; set; }
    }
}
