

using System;
using System.Collections.Generic;

namespace Garage_2._0_MPT.Utils
{
    public class Position : IEquatable<Position>
    {
        
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

      //  public List<int> ParkedVehicle { get; set; } = new List<int>();
        public int? SpaceLeftForFract { get; set; }

        public int? SpaceOccupide { get; set; }

        public override string ToString()
        {
            return $"{((char)Z - 1 - 'A').ToString()}  ({X},{Y}) ";
        }

        public bool Equals(Position x, Position y)
        {
            return ((x.X == y.X) && (x.Y == y.Y) && (x.Z == y.Z));
        }

        public bool Equals(Position other)
        {

            return ((X == other.X) && (Y == other.Y) && (Z == other.Z));
        }
    }
}
