using PKMXEN.Models;
using PKMXEN.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PKMXEN.Logic
{
    public class OrderLogic : IOrderLogic
    {
        IOrderRepository orderRepository;

        public OrderLogic(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public void Create(Order order) // Adding a new order to the database
        {
            if (order.OrderValue <= 0)
            {
                throw new ArgumentNullException("Order Value can not be less or equal to 0!");
            }
            orderRepository.Create(order);
        }

        public Order Read(int id)   // Returns the correct Order object based on the id parameter
        {
            return orderRepository.Read(id);
        }

        public IEnumerable<Order> ReadAll()  // Returns the 'Orders' table
        {
            return orderRepository.ReadAll();
        }

        public void Delete(int id)
        {
            orderRepository.Delete(id);
        }

        public void Update(Order order)
        {
            orderRepository.Update(order);
        }

        // Non-Crud methods
        // // In CarrierLogic.cs

        // Average Order Value 
        public double AVGPrice()
        {
            return orderRepository.ReadAll()
                .Average(t => t.OrderValue);

        }

        // Average Value by Carriers
        public IEnumerable<KeyValuePair<string, double>> AVGPriceByCarriers()
        {
            return from x in orderRepository.ReadAll()
                   group x by x.Carrier.Name into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(t => t.OrderValue));
        }
    }
}
