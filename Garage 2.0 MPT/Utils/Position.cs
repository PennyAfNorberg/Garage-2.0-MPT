

using System;
using System.Collections.Generic;
using Garage_2._0_MPT.Models;

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


        public Position()
        {

        }
        //A (3,1)
        public Position(ParkedVehicle parkedVehicle)
        {
            var parts = parkedVehicle.Where.Split(" ");
             Z = (char)parts[0][0]-1+'A'-128;
            var parts2 = parts[2].Split(",");
             X = Int32.Parse(parts2[0].Substring(1));
             Y = Int32.Parse(parts2[1].Substring(0, parts2[1].Length - 1));

            if(parkedVehicle.VehicleTyp.SpacesNeeded<0)
            {
                SpaceLeftForFract = parkedVehicle.VehicleTyp.SpacesNeeded + 1;
            }
            else
            {
                SpaceOccupide = parkedVehicle.VehicleTyp.SpacesNeeded;
            }
        }

        public override string ToString()
        {
            return $"{((char)((char)Z - 1 + 'A')).ToString()}  ({X},{Y})";
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
