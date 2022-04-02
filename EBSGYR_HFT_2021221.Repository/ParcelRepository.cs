using PKMXEN.Data;
using PKMXEN.Models;
using System.Linq;

namespace PKMXEN.Repository
{
    public class ParcelRepository : IParcelRepository
    {
        ShippingDbContext shippingDB;
        public ParcelRepository(ShippingDbContext db)
        {
            shippingDB = db; ;
        }

        public void Create(Parcel parcel) // Adding a new order to the database
        {
            shippingDB.Parcels.Add(parcel);
            shippingDB.SaveChanges();
        }

        public Parcel Read(int id)   // Returns the correct Order object based on the id parameter
        {
            return shippingDB.Parcels.FirstOrDefault(x => x.TrackingID == id);
        }

        public IQueryable<Parcel> ReadAll()  // Returns the 'Orders' table
        {
            return shippingDB.Parcels;
        }

        public void Delete(int id)
        {
            shippingDB.Remove(Read(id));
            shippingDB.SaveChanges();
        }

        public void Update(Parcel parcel)
        {
            var oldParcel = Read(parcel.TrackingID);
            oldParcel.Weight = parcel.Weight;
            oldParcel.COD = parcel.COD;
            oldParcel.ShippingDate = parcel.ShippingDate;
            oldParcel.CustomerName = parcel.CustomerName;
            oldParcel.Address = parcel.Address;
            oldParcel.Country = parcel.Country;
            oldParcel.OrderID = parcel.OrderID;
            shippingDB.SaveChanges();
        }
    }
}
