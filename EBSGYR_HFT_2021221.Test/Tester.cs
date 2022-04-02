using PKMXEN.Logic;
using PKMXEN.Models;
using PKMXEN.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PKMXEN.Test
{
    [TestFixture]
    public class Tester
    {
        ICarrierLogic carrierLogic;
        IParcelLogic parcelLogic;
        IOrderLogic orderLogic;

        [SetUp]
        public void Setup()
        {
            Mock<ICarrierRepository> mockCarrierRepository = new Mock<ICarrierRepository>();
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();

            // Carriers

            mockCarrierRepository.Setup(x => x.ReadAll()).Returns(new List<Carrier>
            {
                new Carrier()
                    {
                        CarrierID = 1,
                        Name = "Brendon",
                        Age = 27,
                        Salary = 2150,
                        TotalNumberOfParcels = 23
                    },
                new Carrier()
                    {
                        CarrierID = 2,
                        Name = "Johnson",
                        Age = 22,
                        Salary = 1490,
                        TotalNumberOfParcels = 32
                    },
                new Carrier
                    {
                        CarrierID = 3,
                        Name = "Adam",
                        Age = 30,
                        Salary = 4310,
                        TotalNumberOfParcels = 29
                    },
                new Carrier
                    {
                        CarrierID = 4,
                        Name = "Peter",
                        Age = 51,
                        Salary = 5000,
                        TotalNumberOfParcels = 178
                }
            }.AsQueryable());

            carrierLogic = new CarrierLogic(mockCarrierRepository.Object, mockOrderRepository.Object, mockParcelRepository.Object);

            // Parcels

            mockParcelRepository.Setup(y => y.ReadAll()).Returns(new List<Parcel>
            {
                new Parcel()
                {
                    TrackingID = 980254,
                    Weight = 4.2,
                    COD = false,
                    ShippingDate = new DateTime(2021,10,11),
                    Country = "DE",
                    Address = "Joachimstaler Str. 84",
                    OrderID = 1
                },
                new Parcel()
                {
                    TrackingID = 286299,
                    Weight = 11.7,
                    COD = true,
                    ShippingDate = new DateTime(2020,12,17),
                    Country = "USA",
                    Address = "127 Newmarket Road",
                    OrderID = 3
                },
                new Parcel()
                {
                    TrackingID = 915521,
                    Weight = 0.6,
                    COD = true,
                    ShippingDate = new DateTime(2021,11,25),
                    Country = "FR",
                    Address = "44 rue des lieutemants Thomazo",
                    OrderID = 2
                },
                new Parcel()
                {
                    TrackingID = 281411,
                    Weight = 2.4,
                    COD = false,
                    ShippingDate = new DateTime(2021,03,21),
                    Country = "USA",
                    Address = "49, Grand-Rue",
                    OrderID = 4
                },
                new Parcel()
                {
                    TrackingID = 755638,
                    Weight = 1.9,
                    COD = false,
                    ShippingDate = new DateTime(2021,02,05),
                    Country = "HR",
                    Address = "Obrov 8, 21000",
                    OrderID = 1
                },
            }.AsQueryable());

            parcelLogic = new ParcelLogic(mockParcelRepository.Object);

            // Orders
            mockOrderRepository.Setup(t => t.ReadAll()).Returns(new List<Order>
            {
                new Order()
                {
                    OrderID = 1,
                    OrderDescription = "Apple Iphone X",
                    OrderValue = 1099.99,
                    OrderDate = new DateTime(2021, 11, 25),
                    CarrierID = 2
                },
                new Order()
                {
                    OrderID = 2,
                    OrderDescription = "Xbox One X",
                    OrderValue = 799.99,
                    OrderDate = new DateTime(2020, 09, 07),
                    CarrierID = 3
                },
                new Order()
                {
                    OrderID = 3,
                    OrderDescription = "Razer Synapse",
                    OrderValue = 114.99,
                    OrderDate = new DateTime(2021, 01, 22),
                    CarrierID = 1
                },
                new Order()
                {
                    OrderID = 4,
                    OrderDescription = "Nike Fly Zoom %",
                    OrderValue = 247.99,
                    OrderDate = new DateTime(2020, 12, 22),
                    CarrierID = 4
                }
            }.AsQueryable());

            orderLogic = new OrderLogic(mockOrderRepository.Object);
        }

        [Test]
        public void AverageNumberOfParcels_Test()
        {
            //ACT
            var result = carrierLogic.AverageNumberOfParcels();

            //ASSERT
            Assert.That(result, Is.EqualTo(65.5));
        }

        [Test]
        public void MaxSalary_Test()
        {
            //ACT
            var result = carrierLogic.MaxSalary();

            //ASSERT
            Assert.That(result, Is.EqualTo(5000));
        }

        [Test]
        [TestCase(22)]
        public void NameOfCourierByAge_Test(int age)
        {
            //ACT
            var result = carrierLogic.NameOfCourierByAge(age).FirstOrDefault();

            //ASSERT
            Assert.That(result, Is.EqualTo(new KeyValuePair<string, int>("Johnson", 22)));
        }

        [Test]
        public void CreateCarrierTest()
        {
            //ACT + ASSERT
            Assert.That(() => carrierLogic.Create(new Carrier()
            {
                Name = "Balazs",
                Age = 101,  // It should not be over 99 => throws exception
                Salary = 250,
                TotalNumberOfParcels = 79
            }), Throws.Exception); ;
        }
        [Test]
        public void CreateParcelTest()
        {
            //ACT + ASSERT
            Assert.That(() => parcelLogic.Create(new Parcel()
            {
                Weight = 2.4,
                COD = false,
                ShippingDate = new DateTime(2021, 03, 21),
                Country = "LUX",
                Address = "",   // Can not be "" => throws exception
                OrderID = 4
            }), Throws.Exception); ;
        }
        [Test]
        public void CreateOrderTest()
        {
            //ACT + ASSERT
            Assert.That(() => orderLogic.Create(new Order()
            {
                OrderDescription = "Apple Iphone X",
                // OrderValue is Required => throws Exception
                OrderDate = new DateTime(2021, 11, 25),
                CarrierID = 2
            }), Throws.Exception); ;
        }
        [Test]
        [TestCase("Peter")]
        public void Test_OrderDetailsByCourierName(string name)
        {
            //ACT
            var result = carrierLogic.OrderDetailsByCourierName(name).FirstOrDefault();   // Csak egy elemet akarunk megkapni
            //ASSERT

            Assert.That(result, Is.EqualTo(new KeyValuePair<int, string>(4, "Nike Fly Zoom %")));
        }

        [Test]
        [TestCase(755638)]
        public void Test_CourierInfoByTrackingID(int trackingID)
        {
            //ACT
            var result = carrierLogic.CourierInfoByTrackingID(trackingID).Single();

            //ASSERT
            Assert.That(result, Is.EqualTo(new KeyValuePair<string, double>("Johnson", 22)));
        }

        [Test]
        public void Test_USAOrderValue()
        {
            //ACT
            var result = carrierLogic.USAOrderValue().ToArray();

            //ASSERT
            Assert.That(result[1], Is.EqualTo(new KeyValuePair<DateTime, string>(new DateTime(2021, 03, 21), "49, Grand-Rue")));    // Ez van "feljebb"
            Assert.That(result[0], Is.EqualTo(new KeyValuePair<DateTime, string>(new DateTime(2020, 12, 17), "127 Newmarket Road")));
        }

        [Test]
        [TestCase(3)]
        public void Test_OrderWeightByID(int orderID)
        {
            //ACT
            var result = carrierLogic.OrderWeightByID(orderID).SingleOrDefault();

            //ASSERT
            Assert.That(result, Is.EqualTo(new KeyValuePair<int, double>(286299, 11.7)));
            ;
        }
    }
}
