using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage_2._0_MPT.Models;

namespace Garage_2._0_MPT.Utils
{

    public class ParkHouse
    {
        public int Floors { get;  }
        public List<int> Twos { get; set; }
        public List<int> Threes { get; set; }

        private readonly List<Position> OccupidePositions = new List<Position>();

        private Dictionary<int, Position> NextFreeSpaces = new Dictionary<int, Position>();

        private readonly Garage_2_0_MPTContext _context;

        public Dictionary<int, Position>  GetNextFreeSpaces()
        {
            return NextFreeSpaces.ToDictionary(k =>k.Key, k => k.Value  );
        }

        /// <summary>
        /// TEMP for Testing!!
        /// </summary>
        /// <returns></returns>
        public List<Position> GetOccupidePositions()
        {
            return OccupidePositions.ToList();
        }

        public ParkHouse(int Floors, int[] Twos, int[] Threes)
        {
            this.Floors = Floors;
            if (Twos.Length != Floors)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Floors">Numbers of floors</param>
        /// <param name="Twos">Number of double spots, needs Floors many</param>
        /// <param name="Threes">Number of tripple spots, needs Floors many</param>
        public ParkHouse( int Floors, int[] Twos, int[] Threes, Garage_2_0_MPTContext context)
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
            _context = context;
            var res = _context.ParkedVehicle.Where(p => p.Where != null).Select(x => new ParkedViewModel()
            {
                ParkedVehicle = x,
                VehicleTyp = x.Vehicle.VehicleTyp,

            });
            AddSavedVehicles(res);

            PopulateNextFreeSpaces();
        }

        protected void PopulateNextFreeSpaces()
        {

            var firstPosition = new Position()
            {
                Z = 1,
                X = 1,
                Y = 1 // 1,2,3 
            };
            foreach (var SpacesNeeded in _context.VehicleTyp.Select(vt => vt.SpacesNeeded).OrderBy(vt => vt).Distinct())
            {
                if (SpacesNeeded == 3)
                {
                    if (firstPosition.X <= Twos[firstPosition.Z - 1])
                        firstPosition = new Position()
                        {
                            Z = firstPosition.Z,
                            X = Twos[firstPosition.Z - 1] + 1,
                            Y = 1,
                            SpaceOccupide = 3
                        };
                }
                if (SpacesNeeded < 0 && firstPosition.SpaceOccupide == null)
                {
                    firstPosition.SpaceLeftForFract = SpacesNeeded + 1;
                }
                else if (SpacesNeeded > 0 && firstPosition.SpaceLeftForFract == null && firstPosition.SpaceOccupide == null)
                {
                    firstPosition.SpaceOccupide = SpacesNeeded;
                }
                while (NextFreeSpaces.ContainsValue(firstPosition) || OccupidePositions.Contains(firstPosition))
                    firstPosition = GetNextSpot(firstPosition, SpacesNeeded);

                NextFreeSpaces[SpacesNeeded] = firstPosition;

            }

        }
        /// <summary>
        ///  Gives parkedVehicle a park or returns false
        /// </summary>
        /// <param name="parkedVehicle"></param>
        /// <returns> ok/full</returns>

        public bool Park(ParkedVehicle parkedVehicle)
        {
            bool foundnextspot=GetNextSpot(parkedVehicle);
           
            return foundnextspot;
        }



        public void AddSavedVehicles(IEnumerable<ParkedViewModel> parkedVehicles)
        {
            foreach (var parkedVehicle in parkedVehicles)
            {
                if(parkedVehicle.ParkedVehicle.Where != null)
                  AddSavedVehicle(parkedVehicle.ParkedVehicle);
            }

            foreach(var item in NextFreeSpaces)
            {
                NextFreeSpaces[item.Key] = null;
            }

            var firstPosition = new Position()
            {
                Z = 1,
                X = 1,
                Y = 1 // 1,2,3 
            };

        }
        //A (3,1)
        protected void AddSavedVehicle(ParkedVehicle parkedVehicle)
        {
            var temppos = new Position(parkedVehicle);
            OccupidePositions.Add(temppos);
            parkedVehicle.Position = temppos;
            


        }

        public bool Leave(ParkedVehicle parkedVehicle)
        {
            OccupidePositions.Remove(parkedVehicle.Position);
            NextFreeSpaces[parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded] = null;
            parkedVehicle.Where = null;
            parkedVehicle.Position = null;
            var dickcount = NextFreeSpaces.Count;
            Dictionary<int, Position> TempNextFreeSpaces = NextFreeSpaces.ToDictionary(k => k.Key, k => k.Value);


            for (int i = 0; i < dickcount; i++)
            {
                var itemK = TempNextFreeSpaces.Keys.ElementAt(i);
                var itemv = TempNextFreeSpaces[itemK];
                if (itemv == null)
                    TempNextFreeSpaces[itemK] = GetNextSpot(new Position()
                    {
                        Z = 1,
                        X = 1,
                        Y = 1
                    }
                        , parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded);
            }
            NextFreeSpaces = TempNextFreeSpaces;
            return true;
        }




        /// <summary>
        ///  Actualldy do the parking
        /// </summary>
        /// <param name="parkedVehicle"></param>
        /// <returns>ok/full></returns>
        protected bool GetNextSpot(ParkedVehicle parkedVehicle)
        {
            Position nextOne = null;
            Position blaskn = null;
            Position blasko = null;
            if(NextFreeSpaces.TryGetValue(parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded, out nextOne))
            {
                if (nextOne != null)
                {
                    OccupidePositions.Add(nextOne);
                    parkedVehicle.Where = nextOne.ToString();
                    parkedVehicle.Position = nextOne;
                    NextFreeSpaces[parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded] = null; /// one less to check.
                    var nextOne2 = new Position()
                    {
                        Z = nextOne.Z,
                        X = nextOne.X,
                        Y = nextOne.Y
                    };
                    if (TestPos(nextOne2, parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded, out blaskn, out blasko))
                    {
                        if (parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded < 0)
                        {
                            nextOne2.SpaceLeftForFract = (blaskn != null) ? (blaskn.SpaceLeftForFract + 1) : ((blasko != null) ? (blasko.SpaceLeftForFract + 1) : (parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded + 1));
                        }
                        else
                        {
                            nextOne2.SpaceOccupide = parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded;
                        }
                        NextFreeSpaces[parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded] = nextOne2;
                    }
                    else
                    {
                        nextOne = GetNextSpotWrapper(nextOne, parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded, nextOne);
                        NextFreeSpaces[parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded] = nextOne;
                    }

                    return true;
                }
                else
                { // then we try to steal a prebooked slot.
                    if (NextFreeSpaces.Keys.Any(p => p > parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded))
                    { // something to steal
                        int stealfromhere = NextFreeSpaces.Keys.Where(p => p > parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded).OrderBy(p => p).FirstOrDefault();
                        nextOne = NextFreeSpaces[stealfromhere];
                        var saveoldSpaceneed = (nextOne.SpaceOccupide == null) ? nextOne.SpaceLeftForFract - 1 : nextOne.SpaceOccupide;
                        if (parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded < 0)
                        {
                            nextOne.SpaceLeftForFract = parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded + 1;
                        }
                        else
                        {
                            nextOne.SpaceOccupide = parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded;
                        }
                        OccupidePositions.Add(nextOne);
                        NextFreeSpaces[stealfromhere] = null;
                        var nextOne2 = new Position()
                        {
                            Z = nextOne.Z,
                            X = nextOne.X,
                            Y = nextOne.Y
                        };
                        NextFreeSpaces[stealfromhere] = GetNextSpotWrapper(nextOne, saveoldSpaceneed.Value, nextOne);
                    }
                }
            }
            else
            { // then we try to steal a prebooked slot.
                if (NextFreeSpaces.Keys.Any(p=>p> parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded))
                { // something to steal
                    int stealfromhere = NextFreeSpaces.Keys.Where(p => p > parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded).OrderBy(p => p).FirstOrDefault();
                    nextOne = NextFreeSpaces[stealfromhere];
                    var saveoldSpaceneed = (nextOne.SpaceOccupide == null) ? nextOne.SpaceLeftForFract - 1 : nextOne.SpaceOccupide;
                    if (parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded < 0)
                    {
                        nextOne.SpaceLeftForFract = parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded + 1;
                    }
                    else
                    {
                        nextOne.SpaceOccupide = parkedVehicle.Vehicle.VehicleTyp.SpacesNeeded;
                    }
                    OccupidePositions.Add(nextOne);
                    NextFreeSpaces[stealfromhere] = null;
                    var nextOne2 = new Position()
                    {
                        Z = nextOne.Z,
                        X = nextOne.X,
                        Y = nextOne.Y
                    };
                    NextFreeSpaces[stealfromhere] = GetNextSpotWrapper(nextOne, saveoldSpaceneed.Value, nextOne);

                }
            }
            return false;
        }
        /// <summary>
        ///  Tests if position is avalible, returns hits for detailstudy.
        /// </summary>
        /// <param name="position"> Check where</param>
        /// <param name="SpacesNeeded"> how big left when testing</param>
        /// <param name="checkthisN">Hit from nextpos if any</param>
        /// <param name="checkthisO">Hit from occupied if any</param>
        /// <param name="delta">used if slide check, default 0</param>
        /// <returns>true if no hit.</returns>
        protected bool TestPos(Position position, int SpacesNeeded, out Position checkthisN, out Position checkthisO ,int delta =0  )
        {
            checkthisN = null;
            checkthisO = null;
            if (NextFreeSpaces.ContainsValue(position))
            { // then if spaceNeeded < 0 whe need too check again
                checkthisN = GetPosFromNextFreeSpaces(position);
                if (SpacesNeeded < 0)
                {
                    
                   // SpaceLeftForFractN = checkthisN.SpaceLeftForFract;
                    if (checkthisN.SpaceOccupide == null && checkthisN.SpaceLeftForFract < 0)
                    {
                        checkthisN.SpaceLeftForFract++;

                    }
                    else
                        return false;
                }
                else
                { // heltäckande
                    if (checkthisN != null)
                    {
                        if (checkthisN.SpaceLeftForFract != null)
                            return false;
                        if(checkthisN.SpaceOccupide != null)
                        {
                            if (delta == 0)
                                return false;
                            if(delta<0)
                            {
                                if (checkthisN.SpaceOccupide + delta > 0)
                                    return false;
                                return true;
                            }
                            if(delta>0)
                            {
                                if (delta > SpacesNeeded)
                                    return false;
                                return true;
                            }
                        }

                        if (checkthisN.SpaceOccupide >= SpacesNeeded)
                            return false;
                    }
                }

            }
            if (OccupidePositions.Contains(position))
            {// then if spaceNeeded < 0 whe need too check again
                checkthisO = GetPosFromOccupidePositions(position);
                if (SpacesNeeded < 0)
                {
                    // we need to get all parked items for this space now

                    var checkAllThese= GetAllPosFromOccupidePositions(position);
                    if((-SpacesNeeded) <= checkAllThese.Where(vt => vt.SpaceOccupide == null).Count())
                        return false;
                    if (delta < 0)
                    {
                        if (checkAllThese.Any(vt => vt.SpaceOccupide != null && vt.SpaceOccupide>-delta))
                            return false;
                    }
                    else
                    {
                        if (checkAllThese.Any(vt => vt.SpaceOccupide != null ))
                            return false;
                    }
                    return true;

                 /*   //  SpaceLeftForFractO = checkthisO.SpaceLeftForFract;
                    if (checkthisO.SpaceOccupide == null && checkthisO.SpaceLeftForFract < 0)
                    {
                        checkthisO.SpaceLeftForFract++;
                        return true;
                    }
                    else
                        return false;
                        */
                }
                else
                {
                    if (checkthisO != null)
                    {
                        // heltäckande
                        if (checkthisO.SpaceLeftForFract != null)
                            return false;

                        if (checkthisO.SpaceOccupide != null)
                        {
                            if (delta == 0)
                                return false;
                            if (delta < 0)
                            {
                                if (checkthisO.SpaceOccupide + delta > 0)
                                    return false;
                                return true;
                            }
                            if (delta > 0)
                            {
                                if (delta > SpacesNeeded)
                                    return false;
                                return true;
                            }
                        }
                        if (checkthisO.SpaceOccupide >= SpacesNeeded)
                            return false;
                    }
                    return true;
                }
            }
            return true;

        }


        protected Position GetNextSpotWrapper(Position Position, int SpacesNeeded, Position StopPosition = null)
        {
            if (StopPosition == null)
            {
                return GetNextSpot(Position, SpacesNeeded, StopPosition);
            }
            else
            {

                Position SavePos = new Position
                {
                    Z = Position.Z,
                    X = Position.X,
                    Y = Position.Y,
                    SpaceLeftForFract = Position.SpaceLeftForFract,
                    SpaceOccupide = Position.SpaceOccupide

                };
                Position = GetNextSpot(Position, SpacesNeeded, null);
                if(Position==null)
                {
                    Position = GetNextSpot(SavePos, SpacesNeeded, StopPosition);
                }
                return Position;
            }

        }


        /// <summary>
        ///  Tries to find the next free position uses recursion 
        /// </summary>
        /// <param name="Position">to test</param>
        /// <param name="SpacesNeeded">Needed spaces</param>
        /// <param name="StopPosition">Stopps here or null</param>
        /// <returns>a free position or null if no avabile</returns>
        protected Position GetNextSpot(Position Position, int SpacesNeeded, Position StopPosition = null)
        {
             
            Position testPos = null;
            Position checkthisN = null;
            Position checkthisO = null;
            Position blaskN = null;
            Position blaskO = null;
            int nextfract;
            if (SpacesNeeded < 0)
            {
                Position = NextPostminus(Position, SpacesNeeded);
            }
            else if (SpacesNeeded == 1)
            {
                Position = NextPos1(Position);
            }
            else if (SpacesNeeded == 2)
            {
                Position = NextPos2(Position);
            }
            else if (SpacesNeeded == 3)
            {
                Position = NextPos3(Position);
            }
            else
                throw new NotImplementedException();
            if (StopPosition == null)
            {
                if (Position == null)
                    return null;
            }
            else
            {
                if (Position == null || Position.Equals(StopPosition))
                    return null;
            }
            if (TestPos(Position, SpacesNeeded, out checkthisN, out checkthisO))
            { // first ok needed allways
                if (SpacesNeeded < 0)
                { // mc now
                    nextfract = SpacesNeeded + 1;
                    if (checkthisN != null && checkthisN.SpaceLeftForFract != null)
                        nextfract = checkthisN.SpaceLeftForFract.Value + 1;
                    if (checkthisO != null && checkthisO.SpaceLeftForFract != null)
                        nextfract = checkthisO.SpaceLeftForFract.Value + 1;
                    if (Position.Y == 3)
                    {
                        testPos = new Position()
                        {
                            Z = Position.Z,
                            X = Position.X,
                            Y = Position.Y - 1,
                        };
                        if (TestPos(testPos, SpacesNeeded, out blaskN, out blaskO,-1))
                        {
                            testPos = new Position()
                            {
                                Z = Position.Z,
                                X = Position.X,
                                Y = Position.Y - 2,
                            };
                            if (TestPos(testPos, SpacesNeeded, out blaskN, out blaskO,-2))
                            {
                                return new Position()
                                {
                                    Z = Position.Z,
                                    X = Position.X,
                                    Y = Position.Y,
                                    SpaceLeftForFract = nextfract
                                };

                            }

                        }
                    }
                    if (Position.Y == 2)
                    {
                        testPos = new Position()
                        {
                            Z = Position.Z,
                            X = Position.X,
                            Y = Position.Y - 1,
                        };
                        if (TestPos(testPos, SpacesNeeded, out blaskN, out blaskO,-1))
                        {
                            return new Position()
                            {
                                Z = Position.Z,
                                X = Position.X,
                                Y = Position.Y,
                                SpaceLeftForFract = nextfract
                            };

                        }

                    }
                    // get next and return
                    return GetNextSpot(Position, SpacesNeeded, StopPosition);
                }
                else if (SpacesNeeded == 1)
                {
                    if (Position.Y == 3)
                    {
                        testPos = new Position()
                        {
                            Z = Position.Z,
                            X = Position.X,
                            Y = Position.Y - 1,
                        };
                        if (TestPos(testPos, SpacesNeeded+1, out blaskN, out blaskO, -1))
                        {
                            testPos = new Position()
                            {
                                Z = Position.Z,
                                X = Position.X,
                                Y = Position.Y - 2,
                            };
                            if (TestPos(testPos, SpacesNeeded+2, out blaskN, out blaskO,-2))
                            {
                                Position.SpaceOccupide = SpacesNeeded;
                                return Position;

                            }

                        }
                        else
                        {
                            if ((blaskN != null && blaskN.SpaceLeftForFract != null) || (blaskO != null && blaskO.SpaceLeftForFract != null))
                            {
                                testPos = new Position()
                                {
                                    Z = Position.Z,
                                    X = Position.X,
                                    Y = Position.Y - 2,
                                };
                                if (TestPos(testPos, SpacesNeeded + 2, out blaskN, out blaskO,-2))
                                {
                                    Position.SpaceOccupide = SpacesNeeded;
                                    return Position;

                                }
                                else
                                {
                                    if ((blaskN != null && blaskN.SpaceLeftForFract != null) || (blaskO != null && blaskO.SpaceLeftForFract != null))
                                    {
                                        Position.SpaceOccupide = SpacesNeeded;
                                        return Position;
                                    }
                                }

                            }
                         }
                    }
                    if (Position.Y == 2)
                    {
                        testPos = new Position()
                        {
                            Z = Position.Z,
                            X = Position.X,
                            Y = Position.Y - 1,
                        };
                        if (TestPos(testPos, SpacesNeeded+1, out blaskN, out blaskO,-1))
                        {
                            Position.SpaceOccupide = SpacesNeeded;
                            return Position;

                        }
                        else
                        {
                            if ((blaskN != null && blaskN.SpaceLeftForFract != null) || (blaskO != null && blaskO.SpaceLeftForFract != null))
                            {
                                Position.SpaceOccupide = SpacesNeeded;
                                return Position;
                            }
                        }
                    }
                    if (Position.Y == 1)
                    {
                        Position.SpaceOccupide = SpacesNeeded;
                        return Position;
                    }
                        // get next and return
                    return GetNextSpot(Position, SpacesNeeded, StopPosition);
                }
                else if (SpacesNeeded == 2)
                {
                    if (Position.Y == 1)
                    {
                        testPos = new Position()
                        {
                            Z = Position.Z,
                            X = Position.X,
                            Y = Position.Y + 1,
                        };
                        if (TestPos(testPos, SpacesNeeded-1, out blaskN, out blaskO,1))
                        {
                            Position.SpaceOccupide = SpacesNeeded;
                            return Position;
                        }

                    }
                    else
                    { // y=2
                        testPos = new Position()
                        {
                            Z = Position.Z,
                            X = Position.X,
                            Y = Position.Y - 1,
                        };
                        if (TestPos(testPos, SpacesNeeded+1, out blaskN, out blaskO,-1))
                        {

                            if (Position.X > Twos[Position.Z] - 1)
                            {
                                testPos = new Position()
                                {
                                    Z = Position.Z,
                                    X = Position.X,
                                    Y = Position.Y + 1,
                                };
                                if (TestPos(testPos, SpacesNeeded-1, out blaskN, out blaskO,1))
                                {
                                    Position.SpaceOccupide = SpacesNeeded;
                                    return Position;
                                }
                            }

                        }
                        if ((blaskN == null || (blaskN.SpaceOccupide == null || blaskN.SpaceOccupide == 0)) && (blaskO == null || (blaskO.SpaceOccupide == null || blaskO.SpaceOccupide == 0)))
                        { // är ledig ändå
                            if (Position.X > Twos[Position.Z])
                            {
                                testPos = new Position()
                                {
                                    Z = Position.Z,
                                    X = Position.X,
                                    Y = Position.Y + 1,
                                };
                                if (TestPos(testPos, SpacesNeeded-1, out blaskN, out blaskO,1))
                                {
                                    return Position;
                                }
                            }

                        }
                        // get next
                        return GetNextSpot(Position, SpacesNeeded, StopPosition);
                    }
                }
                else if (SpacesNeeded == 3)
                {
                    
                    testPos = new Position
                    {
                        Z = Position.Z,
                        X = Position.X,
                        Y = 2
                    };
                    if (TestPos(testPos, SpacesNeeded-2, out blaskN, out blaskO,2))
                    {
                        testPos = new Position
                        {
                            Z = Position.Z,
                            X = Position.X,
                            Y = 3
                        };
                        if (TestPos(testPos, SpacesNeeded-2, out blaskN, out blaskO,2))
                        {
                            return Position;
                        }

                    }
                    // get next
                    return GetNextSpot(Position, SpacesNeeded, StopPosition);

                }
                else
                    throw new NotImplementedException();
            }
            return GetNextSpot(Position, SpacesNeeded, StopPosition);
        }


        /// <summary>
        ///  traverses the z-layer strukture for the next position to test, when more than one vechile may park in a spot
        /// </summary>
        /// <param name="position">The last</param>
        /// <param name="SpacesNeeded">allways negative == fraktion</param>
        /// <returns>Next to test or null if no leave to test</returns>
        protected Position NextPostminus(Position position, int SpacesNeeded)
        {
            Position testPos = null;
            if ((position.Y < 2 && position.X <= Twos[position.Z - 1]) || (position.Y < 3 && position.X > Twos[position.Z - 1]))
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X,
                    Y = position.Y + 1,
                    SpaceLeftForFract = SpacesNeeded + 1
                };

            }
            else if (position.X < Twos[position.Z - 1] && position.Z <= Floors)
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X + 1,
                    Y = 1,
                    SpaceLeftForFract = SpacesNeeded + 1
                };
            }
            else if (position.X == Twos[position.Z - 1] && position.Z == Floors && position.X < Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = 1,
                    X = Twos[0] + 1,
                    Y = 1,
                    SpaceLeftForFract = SpacesNeeded + 1
                };
            }
            else if (position.X == Twos[position.Z-1] && position.Z <= Floors)
            {
                testPos = new Position()
                {
                    Z = position.Z + 1,
                    X = 1,
                    Y = 1,
                    SpaceLeftForFract = SpacesNeeded + 1
                };
            }

            else if (position.X > Twos[position.Z - 1] && position.Z <= Floors && position.X < Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X + 1,
                    Y = 1,
                    SpaceLeftForFract = SpacesNeeded + 1
                };
            }
            else if (position.X > Twos[position.Z - 1] && position.Z < Floors && position.X == Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = position.Z + 1,
                    X = Twos[position.Z]+1,
                    Y = 1,
                    SpaceLeftForFract = SpacesNeeded + 1
                };
            }
            return testPos;
        }

        /// <summary>
        /// traverses the z-layer strukture for the next position to test, when more than one vechile takes one spot, and just parks in egdes.
        /// </summary>
        /// <param name="position">Start position</param>
        /// <returns>Next to test or null if no leave to test</returns>
        protected Position NextPos1(Position position)
        {
            Position testPos = null;
            int SpacesNeeded = 1;
            if ((position.Y < 2 && position.X <= Twos[position.Z - 1]))
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X,
                    Y = position.Y + 1,
                    SpaceOccupide = SpacesNeeded
                };

            }
            else if (position.Y < 3 && position.X > Twos[position.Z - 1])
            {
                testPos = new Position()
                { // skip middle if one space vechile and 3 space parking lot.
                    Z = position.Z,
                    X = position.X,
                    Y = position.Y + (SpacesNeeded == 1 ? 2 : 1),
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.X < Twos[position.Z - 1] && position.Z <= Floors)
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.X == Twos[position.Z - 1] && position.Z == Floors && position.X < Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = 1,
                    X = Twos[0] + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.X == Twos[position.Z-1] && position.Z <= Floors)
            {
                testPos = new Position()
                {
                    Z = position.Z + 1,
                    X = 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }

            else if (position.X > Twos[position.Z - 1] && position.Z <= Floors && position.X < Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.X > Twos[position.Z - 1] && position.Z < Floors && position.X == Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = position.Z + 1,
                    X = Twos[position.Z] + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            return testPos;
        }


        /// <summary>
        /// traverses the z-layer strukture for the next position to test, when more than one vechile takes two spots
        /// </summary>
        /// <param name="position">start position</param>
        /// <returns>Next to test or null if no leave to test</returns>
        protected Position NextPos2(Position position)
        {
            Position testPos = null;
            int SpacesNeeded = 2;
            if ((position.Y == 1 && position.X < Twos[position.Z - 1]))
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X+1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };

            }
            else if (position.Y < 2 && position.X > Twos[position.Z - 1])
            {
                testPos = new Position()
                { // skip middle if one space vechile and 3 space parking lot.
                    Z = position.Z,
                    X = position.X,
                    Y = position.Y + (SpacesNeeded == 1 ? 2 : 1),
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.Y == 2 && position.X > Twos[position.Z - 1])
            {
                testPos = new Position()
                { // skip middle if one space vechile and 3 space parking lot.
                    Z = position.Z,
                    X = position.X +1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.X < Twos[position.Z - 1] && position.Z <= Floors)
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.X == Twos[position.Z - 1] && position.Z == Floors && position.X < Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = 1,
                    X = Twos[0] + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.X == Twos[position.Z-1] && position.Z <= Floors)
            {
                testPos = new Position()
                {
                    Z = position.Z + 1,
                    X = 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }

            else if (position.X > Twos[position.Z - 1] && position.Z <= Floors && position.X < Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.X > Twos[position.Z - 1] && position.Z < Floors && position.X == Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = position.Z + 1,
                    X = Twos[position.Z] + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            return testPos;
        }

        /// <summary>
        /// traverses the z-layer strukture for the next position to test, when more than one vechile takes 3 spots,  row needs too be a three
        /// </summary>
        /// <param name="position">start position</param>
        /// <returns>Next to test or null if no leave to test</returns>
        protected Position NextPos3(Position position)
        {
            Position testPos = null;
            int SpacesNeeded = 3;

            if (position.X < Twos[position.Z - 1] + Threes[position.Z - 1])
            {
                testPos = new Position()
                {
                    Z = position.Z,
                    X = position.X + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };
            }
            else if (position.Z < Floors)
            {
                testPos = new Position()
                {
                    Z = position.Z + 1,
                    X = Twos[position.Z] + 1,
                    Y = 1,
                    SpaceOccupide = SpacesNeeded
                };

            }


            return testPos;
        }


        protected List<Position> GetAllPosFromOccupidePositions(Position position)
        {
            return OccupidePositions.Where(p => p != null).Where(p => position.Equals(p)).ToList();
            /*
            List<Position> svar = new List<Position>();
            foreach (var items in OccupidePositions)
            {
                if (items != null)
                {
                    if (position.Equals(items))
                        svar.Add(items);
                }
            }
            return svar;
            */
        }

        /// <summary>
        /// Gets first hit from OccupidePositions that matches position
        /// </summary>
        /// <param name="position">To find</param>
        /// <returns>the hit or null if no hit</returns>
        protected Position GetPosFromOccupidePositions(Position position)
        {
            return OccupidePositions.Where(p => p != null).FirstOrDefault(p => position.Equals(p));
            /*
            foreach (var items in OccupidePositions)
            {
                if (items != null)
                {
                    if (position.Equals(items))
                    return items;
                }
            }
            return null;*/
        }

        /// <summary>
        /// Gets first hit from OccupidePositions that matches position
        /// </summary>
        /// <param name="position">To find</param>
        /// <returns>the hit or null if no hit</returns>
        protected Position GetPosFromNextFreeSpaces(Position position)
        {
            return NextFreeSpaces.Values.Where(p=>p!=null).FirstOrDefault(p => position.Equals(p));
            /*
            foreach (var items in NextFreeSpaces.Values)
            {
                if (items != null)
                {
                    if (position.Equals(items))
                        return items;
                }
            }
            return null;
            */
        }

    }
}
