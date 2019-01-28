using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage_2._0_MPT.Models;

namespace Garage_2._0_MPT.Utils
{
    public class Parkhouse
    {
        public int Floors { get;  }
        public List<int> Twos { get; set; }
        public List<int> Threes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Floors">Numbers of floors</param>
        /// <param name="Twos">Number of double spots, needs Floors many</param>
        /// <param name="Threes">Number of tripple spots, needs Floors many</param>
        public Parkhouse( int Floors, int[] Twos, int[] Threes)
        {
            this.Floors = Floors;
            if(Twos.Length != Floors)
            {
                throw new ArgumentOutOfRangeException($"Got {Twos.Length  } two arguments, needed {Floors} ");
            }
            if (Threes.Length != Floors)
            {
                throw new ArgumentOutOfRangeException($"Got {Threes.Length} three arguments, needed {Floors} ");
            }
            this.Twos = Twos.ToList();
            this.Threes = Threes.ToList();
        }

        public bool Park(ParkedVehicle parkedVehicle)
        {
            bool foundnextspot=getNextSpot(parkedVehicle);
           
            return foundnextspot;
        }

        private bool getNextSpot(ParkedVehicle parkedVehicle)
        {
            throw new NotImplementedException();
        }

        public bool Leave(ParkedVehicle parkedVehicle)
        {


            return true;
        }


    }
}
