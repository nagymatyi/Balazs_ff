using PKMXEN.Data;
using PKMXEN.Models;
using System.Linq;

namespace PKMXEN.Repository
{
    public class CarrierRepository : ICarrierRepository
    {
        ShippingDbContext shippingDB;
        public CarrierRepository(ShippingDbContext db)
        {
            shippingDB = db; ;
        }

        public void Create(Carrier carrier) // Adding a new order to the database
        {
            shippingDB.Carriers.Add(carrier);
            shippingDB.SaveChanges();
        }

        public Carrier Read(int id)   // Returns the correct Order object based on the id parameter
        {
            return shippingDB.Carriers.FirstOrDefault(x => x.CarrierID == id);
        }

        public IQueryable<Carrier> ReadAll()  // Returns the 'Orders' table
        {
            return shippingDB.Carriers;
        }

        public void Delete(int id)
        {
            shippingDB.Remove(Read(id));
            shippingDB.SaveChanges();
        }

        public void Update(Carrier carrier)
        {
            var oldCarrier = Read(carrier.CarrierID);
            oldCarrier.Name = carrier.Name;
            oldCarrier.Age = carrier.Age;
            oldCarrier.Salary = carrier.Salary;
            oldCarrier.TotalNumberOfParcels = carrier.TotalNumberOfParcels;
            shippingDB.SaveChanges();
        }
    }
}
