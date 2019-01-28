

using System.Collections.Generic;

namespace Garage_2._0_MPT.Utils
{
    public class Position
    {
        
        public int X { get; set; }
        public int Y { get; set; }
        public string Z { get; set; }

        public List<int> ParkedVehicle { get; set; }

        public override string ToString()
        {
            return $"{Z}  ({X},{Y}) ";
        }
    }
}
