using PKMXEN.Data;
using PKMXEN.Models;
using System.Linq;

namespace PKMXEN.Repository
{
    public class OrderRepository : IOrderRepository
    {
        ShippingDbContext shippingDB;
        public OrderRepository(ShippingDbContext db)
        {
            shippingDB = db; ;
        }

        public void Create(Order order) // Adding a new order to the database
        {
            shippingDB.Orders.Add(order);
            shippingDB.SaveChanges();
        }

        public Order Read(int id)   // Returns the correct Order object based on the id parameter
        {
            return shippingDB.Orders.FirstOrDefault(x => x.OrderID == id);
        }

        public IQueryable<Order> ReadAll()  // Returns the 'Orders' table
        {
            return shippingDB.Orders;
        }

        public void Delete(int id)
        {
            shippingDB.Remove(Read(id));
            shippingDB.SaveChanges();
        }

        public void Update(Order order)
        {
            var oldOrder = Read(order.OrderID);
            oldOrder.OrderValue = order.OrderValue;
            oldOrder.OrderDescription = order.OrderDescription;
            oldOrder.OrderDate = order.OrderDate;
            oldOrder.CarrierID = order.CarrierID;
            shippingDB.SaveChanges();
        }
    }
}
