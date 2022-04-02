using PKMXEN.Models;
using PKMXEN.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PKMXEN.Logic
{
    public class CarrierLogic : ICarrierLogic
    {
        // Repos
        ICarrierRepository carrierRepository;
        IOrderRepository orderRepository;
        IParcelRepository parcelRepository;

        public CarrierLogic(ICarrierRepository carrierRepository, IOrderRepository orderRepository, IParcelRepository parcelRepository)
        {
            this.carrierRepository = carrierRepository;
            this.orderRepository = orderRepository;
            this.parcelRepository = parcelRepository;
        }
        public void Create(Carrier carrier) // Adding a new order to the database
        {
            if (carrier.Name == "" || carrier.Age > 99)
            {
                throw new ArgumentException("Name can not be empty!");
            }
            carrierRepository.Create(carrier);
        }

        public Carrier Read(int id)   // Returns the correct Order object based on the id parameter
        {
            return carrierRepository.Read(id);
        }

        public IEnumerable<Carrier> ReadAll()  // Returns the 'Orders' table
        {
            return carrierRepository.ReadAll();
        }

        public void Delete(int id)
        {
            carrierRepository.Delete(id);
        }

        public void Update(Carrier carrier)
        {
            carrierRepository.Update(carrier);
        }

        // Non-Crud Methods

        /// <summary>
        /// Returns the average number of parcels per courier
        /// </summary>
        /// <returns></returns> 
        public double AverageNumberOfParcels()
        {
            return carrierRepository
                .ReadAll()
                .Average(c => c.TotalNumberOfParcels);
        }

        /// <summary>
        /// Return the highest salary
        /// </summary>
        /// <returns></returns>
        public double MaxSalary()
        {
            return carrierRepository
                .ReadAll()
                .Max(t => t.Salary);
        }
        /// <summary>
        /// Returns the Name & Age of given age parameter
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, int>> NameOfCourierByAge(int age)
        {
            var name = from carrier in carrierRepository.ReadAll()
                       where carrier.Age == age
                       select new KeyValuePair<string, int>(carrier.Name, carrier.Age);

            return name;
        }

        // Multiple Table Queries

        /// <summary>
        /// Returns the OrderID and OrderDescription of the order that has been shipped by given parameter string
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<int, string>> OrderDetailsByCourierName(string name)
        {
            var q1 = from carrier in carrierRepository.ReadAll()
                     join order in orderRepository.ReadAll()
                     on carrier.CarrierID equals order.CarrierID
                     where carrier.Name == name
                     select new KeyValuePair<int, string>
                     (
                         order.OrderID,
                         order.OrderDescription
                     );
            return q1;
        }
        /// <summary>
        /// Returns the courier name & age, of a given TrackingID
        /// </summary>
        public IEnumerable<KeyValuePair<string, double>> CourierInfoByTrackingID(int trackingID)
        {
            var q2 = from carrier in carrierRepository.ReadAll()
                     join order in orderRepository.ReadAll() on carrier.CarrierID equals order.CarrierID
                     join parcel in parcelRepository.ReadAll() on order.OrderID equals parcel.OrderID
                     where parcel.TrackingID == trackingID
                     select new KeyValuePair<string, double>
                     (
                         carrier.Name,
                         carrier.Age
                     );
            return q2;
        }
        /// <summary>
        /// Returns the order number associated with the predefined address
        /// </summary>
        public IEnumerable<KeyValuePair<int, string>> OrderIDOfGivenAddress()
        {
            var q3 = from carrier in carrierRepository.ReadAll()
                     join order in orderRepository.ReadAll() on carrier.CarrierID equals order.CarrierID
                     join parcel in parcelRepository.ReadAll() on order.OrderID equals parcel.OrderID
                     where parcel.Address == "70 Nith Street, GLENBRECK, ML12 5XN"
                     select new KeyValuePair<int, string>
                     (
                         order.OrderID,
                         order.OrderDescription
                     );
            return q3;
        }
        /// <summary>
        /// Returns the ShippingDate and ShippingAddress of packages ordered to the USA
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<DateTime, string>> USAOrderValue()
        {
            var q4 = from carrier in carrierRepository.ReadAll()
                     join order in orderRepository.ReadAll() on carrier.CarrierID equals order.CarrierID
                     join parcel in parcelRepository.ReadAll() on order.OrderID equals parcel.OrderID
                     where parcel.Country == "USA"
                     select new KeyValuePair<DateTime, string>
                     (
                         parcel.ShippingDate,
                         parcel.Address
                     );
            return q4;
        }
        /// <summary>
        /// Returns the CustomerName & Weight of the given OrderID
        /// </summary>
        public IEnumerable<KeyValuePair<int, double>> OrderWeightByID(int orderID)
        {
            var q5 = from carrier in carrierRepository.ReadAll()
                     join order in orderRepository.ReadAll() on carrier.CarrierID equals order.CarrierID
                     join parcel in parcelRepository.ReadAll() on order.OrderID equals parcel.OrderID
                     where order.OrderID == orderID
                     select new KeyValuePair<int, double>
                     (
                         parcel.TrackingID,
                         parcel.Weight
                     );
            return q5;
        }
    }
}
