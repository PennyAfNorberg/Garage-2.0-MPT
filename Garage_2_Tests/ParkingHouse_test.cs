using Microsoft.VisualStudio.TestTools.UnitTesting;
using Garage_2._0_MPT.Models;
using Garage_2._0_MPT.Utils;

namespace Garage_2_Tests
{
    [TestClass()]
    public class ParkingHouse_test : ParkHouse
    {
        private readonly Vehicle Mc = new Vehicle
        {
            VehicleTypId = 3,
            RegNr = "AMC",
            VehicleColor = "Green",
            VehicleModel = "850gl",
            VehicleBrand = "hd",
            NumberOfWheels = 2,
          /*  ParkOutDate = null,
            Where = "A  (1,1)",
            Position = null,*/
            VehicleTyp= new VehicleTyp
            {
                VehicleTypId = 3,
                SpacesNeeded = -3
            }
        };

        private readonly Vehicle Car = new Vehicle
        {
            VehicleTypId = 1,
            RegNr = "ACar",
            VehicleColor = "Green",
            VehicleModel = "850gl",
            VehicleBrand = "hd",
            NumberOfWheels = 4,
           /* ParkOutDate = null,
            Where = "A  (1,2)",
            Position = null,*/
            VehicleTyp = new VehicleTyp
            {
                VehicleTypId = 1,
                SpacesNeeded = 1
            }
        };
        private readonly Vehicle Bus = new Vehicle
        {
            VehicleTypId = 2,
            RegNr = "ABus",
            VehicleColor = "Green",
            VehicleModel = "850gl",
            VehicleBrand = "hd",
            NumberOfWheels = 8,
            /*ParkOutDate = null,
            Where = "A  (3,1)",
            Position = null,*/
            VehicleTyp = new VehicleTyp
            {
                VehicleTypId = 2,
                SpacesNeeded = 3
            }


        };
        private readonly Vehicle Caravan = new Vehicle
        {
            VehicleTypId = 4,
            RegNr = "AHusvagn",
            VehicleColor = "Green",
            VehicleModel = "850gl",
            VehicleBrand = "hd",
            NumberOfWheels = 8,
          /*  ParkOutDate = null,
            Where = "A  (2,1)",
            Position = null,*/
            VehicleTyp = new VehicleTyp
            {
                VehicleTypId = 4,
                SpacesNeeded = 1
            }
        };
        private readonly Vehicle RV = new Vehicle
        {
            VehicleTypId = 5,
            RegNr = "AHusbil",
            VehicleColor = "Green",
            VehicleModel = "850gl",
            VehicleBrand = "hd",
            NumberOfWheels = 8,
          /*  ParkOutDate = null,
            Where = "B  (1,1)",
            Position = null,*/
            VehicleTyp = new VehicleTyp
            {
                VehicleTypId = 5,
                SpacesNeeded = 2
            }
        };

        private readonly Vehicle Truck = new Vehicle
        {
            VehicleTypId = 6,
            RegNr = "ATruck",
            VehicleColor = "Green",
            VehicleModel = "850gl",
            VehicleBrand = "hd",
            NumberOfWheels = 8,
           /* ParkOutDate = null,
            Where = "B  (2,1)",
            Position = null,*/
            VehicleTyp = new VehicleTyp
            {
                VehicleTypId = 6,
                SpacesNeeded = 2
            }
        };


        public ParkingHouse_test() : base(2, new int[2] { 2, 3 }, new int[2] { 3, 2 })
        {
                
        }


        public ParkingHouse_test(Garage_2_0_MPTContext context) : base(3, new int[3] { 2, 3, 2 }, new int[3] { 3, 2, 3 }, context)
        {

        }

        public ParkingHouse_test(int Floors, int[] Twos, int[] Threes) : base(Floors, Twos, Threes)
        {

        }
        public ParkingHouse_test(int Floors, int[] Twos, int[] Threes, Garage_2_0_MPTContext context) : base(Floors, Twos, Threes, context)
        {
        }

        [TestMethod()]
        public void AddSavedVehicle_Test()
        {
            //Arrange
            var preBooked = GetOccupidePositions();


            //Act
            /*
            AddSavedVehicle(Mc);
            AddSavedVehicle(Car);
            AddSavedVehicle(Bus);
            AddSavedVehicle(Caravan);
            AddSavedVehicle(RV);
            AddSavedVehicle(Truck);

            var postBooked = GetOccupidePositions();

            //Assert
            // the right number
            Assert.AreEqual(postBooked.Count - preBooked.Count, 6, "Wrong number added in OccupidePositions in AddSavedVehicle");
            Assert.IsTrue(postBooked.Contains(Mc.Position), "Mc's pos isn't in OccupidePositions");
            Assert.IsTrue(postBooked.Contains(Car.Position), "Car's pos isn't in OccupidePositions");
            Assert.IsTrue(postBooked.Contains(Bus.Position), "Bus' pos isn't in OccupidePositions");
            Assert.IsTrue(postBooked.Contains(Caravan.Position), "Caravan's pos isn't in OccupidePositions");
            Assert.IsTrue(postBooked.Contains(RV.Position), "RV's pos isn't in OccupidePositions");
            Assert.IsTrue(postBooked.Contains(Truck.Position), "Truck's pos isn't in OccupidePositions");
            Assert.IsTrue(Mc.Position.ToString()==Mc.Where, "Mc's position diffs between where and position");
            Assert.IsTrue(Car.Position.ToString()==Car.Where, "Car's position diffs between where and position");
            Assert.IsTrue(Bus.Position.ToString()==Bus.Where, "Bus' position diffs between where and position");
            Assert.IsTrue(Caravan.Position.ToString()==Caravan.Where, "Caravan's position diffs between where and position");
            Assert.IsTrue(RV.Position.ToString()==RV.Where, "RV's position diffs between where and position");
            Assert.IsTrue(Truck.Position.ToString()==Truck.Where, "Truck's position diffs between where and position");
            */
        }
 

        [TestMethod()]
        public void NextPostminus_test()
        {
            // +y, +x, +z, -> 3 , 3+y, 3+x, 3+z, full no skip.
            //Arrange
            Position test2y = new Position { Z = 1, X = 1, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test2x = new Position { Z = 1, X = 1, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test2z = new Position { Z = 1, X = 2, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3 = new Position { Z = 2, X = 3, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3y = new Position { Z = 1, X = 3, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3x = new Position { Z = 1, X = 3, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3z = new Position { Z = 1, X = 5, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null },
                     testfull = new Position { Z = 2, X = 5, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null };
            Position ok2y = new Position { Z = 1, X = 1, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok2x = new Position { Z = 1, X = 2, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok2z = new Position { Z = 2, X = 1, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3 = new Position { Z = 1, X = 3, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3y = new Position { Z = 1, X = 3, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3x = new Position { Z = 1, X = 4, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3z = new Position { Z = 2, X = 4, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null };
                    // okfull = null;

            //act
            test2y = NextPostminus(test2y, -3);
            test2x = NextPostminus(test2x, -3);
            test2z = NextPostminus(test2z, -3);
            test3  = NextPostminus(test3, -3);
            test3y = NextPostminus(test3y, -3);
            test3x = NextPostminus(test3x, -3);
            test3z = NextPostminus(test3z, -3);
            testfull = NextPostminus(testfull, -3);

            //Assert
            Assert.IsTrue(ok2y.Equals(test2y), "NextPostminus 2 +y diff");
            Assert.IsTrue(ok2x.Equals(test2x), "NextPostminus 2 +x diff");
            Assert.IsTrue(ok2z.Equals(test2z), "NextPostminus 2 +z diff");
            Assert.IsTrue(ok3.Equals(test3),   "NextPostminus to 3 diff");
            Assert.IsTrue(ok3y.Equals(test3y), "NextPostminus 3 +y diff");
            Assert.IsTrue(ok3x.Equals(test3x), "NextPostminus 3 +x diff");
            Assert.IsTrue(ok3z.Equals(test3z), "NextPostminus 3 +z diff");
            Assert.IsNull(testfull,     "NextPostminus full diff");
        }

      

        [TestMethod()]
        public void NextPos1_test()
        {
            // +y, +x, +z, -> 3 , 3+y, 3+x, 3+z, full no skip.
            //Arrange
            Position test2y = new Position { Z = 1, X = 1, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test2x = new Position { Z = 1, X = 1, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test2z = new Position { Z = 1, X = 2, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3 = new Position { Z = 2, X = 3, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3y = new Position { Z = 1, X = 3, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3x = new Position { Z = 1, X = 3, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3z = new Position { Z = 1, X = 5, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null },
                     testfull = new Position { Z = 2, X = 5, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null };
            Position ok2y = new Position { Z = 1, X = 1, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok2x = new Position { Z = 1, X = 2, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok2z = new Position { Z = 2, X = 1, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3 = new Position { Z = 1, X = 3, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3y = new Position { Z = 1, X = 3, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3x = new Position { Z = 1, X = 4, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3z = new Position { Z = 2, X = 4, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null };
            // okfull = null;

            //act
            test2y = NextPos1(test2y);
            test2x = NextPos1(test2x);
            test2z = NextPos1(test2z);
            test3 = NextPos1(test3);
            test3y = NextPos1(test3y);
            test3x = NextPos1(test3x);
            test3z = NextPos1(test3z);
            testfull = NextPos1(testfull);

            //Assert
            Assert.IsTrue(ok2y.Equals(test2y), "NextPos1 2 +y diff");
            Assert.IsTrue(ok2x.Equals(test2x), "NextPos1 2 +x diff");
            Assert.IsTrue(ok2z.Equals(test2z), "NextPos1 2 +z diff");
            Assert.IsTrue(ok3.Equals(test3), "NextPos1 to 3 diff");
            Assert.IsTrue(ok3y.Equals(test3y), "NextPos1 3 +y diff");
            Assert.IsTrue(ok3x.Equals(test3x), "NextPos1 3 +x diff");
            Assert.IsTrue(ok3z.Equals(test3z), "NextPos1 3 +z diff");
            Assert.IsNull(testfull, "NextPos1 full diff");
        }

     

        [TestMethod()]
        public void NextPos2_test()
        {
            // +y, +x, +z, -> 3 , 3+y, 3+x, 3+z, full no skip.
            //Arrange
            Position 
                     test2x = new Position { Z = 1, X = 1, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test2z = new Position { Z = 1, X = 2, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3 = new Position { Z = 2, X = 3, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3y = new Position { Z = 1, X = 3, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3x = new Position { Z = 1, X = 3, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null },
                     test3z = new Position { Z = 1, X = 5, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null },
                     testfull = new Position { Z = 2, X = 5, Y = 3, SpaceLeftForFract = -2, SpaceOccupide = null };
            Position ok2y = new Position { Z = 1, X = 1, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok2x = new Position { Z = 1, X = 2, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok2z = new Position { Z = 2, X = 1, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3 = new Position { Z = 1, X = 3, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3y = new Position { Z = 1, X = 3, Y = 2, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3x = new Position { Z = 1, X = 4, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
                     ok3z = new Position { Z = 2, X = 4, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null };
            // okfull = null;

            //act
 
            test2x = NextPos2(test2x);
            test2z = NextPos2(test2z);
            test3 =  NextPos2(test3);
            test3y = NextPos2(test3y);
            test3x = NextPos2(test3x);
            test3z = NextPos2(test3z);
            testfull = NextPos2(testfull);

            //Assert

            Assert.IsTrue(ok2x.Equals(test2x), "NextPos2 2 +x diff");
            Assert.IsTrue(ok2z.Equals(test2z), "NextPos2 2 +z diff");
            Assert.IsTrue(ok3.Equals(test3), "NextPos2 to 3 diff");
            Assert.IsTrue(ok3y.Equals(test3y), "NextPos2 3 +y diff");
            Assert.IsTrue(ok3x.Equals(test3x), "NextPos2 3 +x diff");
            Assert.IsTrue(ok3z.Equals(test3z), "NextPos2 3 +z diff");
            Assert.IsNull(testfull, "NextPos2 full diff");
        }

        //[TestMethod()]
        //public void NextPos3_test()
        //{
        //    // +y, +x, +z, -> 3 , 3+y, 3+x, 3+z, full no skip.
        //    //Arrange
        //    Position
                    
            
        //             test3x = new Position { Z = 1, X = 3, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
        //             test3z = new Position { Z = 1, X = 5, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
        //             testfull = new Position { Z = 2, X = 5, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null };
        //    Position 
         
        //             ok3x = new Position { Z = 1, X = 4, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null },
        //             ok3z = new Position { Z = 2, X = 4, Y = 1, SpaceLeftForFract = -2, SpaceOccupide = null };
        //    // okfull = null;

        //    //act



        //    test3x = NextPos2(test3x);
        //    test3z = NextPos2(test3z);
        //    testfull = NextPos2(testfull);

        //    //Assert


   
        //    Assert.IsTrue(ok3x.Equals(test3x), "NextPos3 3 +x diff");
        //    Assert.IsTrue(ok3z.Equals(test3z), "NextPos3 3 +z diff");
        //  //  Assert.IsNull(testfull, "NextPos3 full diff");
        //}


            //protected bool TestPos(Position position, int SpacesNeeded, out Position checkthisN, out Position checkthisO ,int delta =0  )

            //protected Position GetNextSpotWrapper(Position Position, int SpacesNeeded, Position StopPosition = null)

            //protected Position GetNextSpot(Position Position, int SpacesNeeded, Position StopPosition = null)

        }
}
 