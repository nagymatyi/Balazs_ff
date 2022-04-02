using PKMXEN.Models;
using PKMXEN.Repository;
using System;
using System.Collections.Generic;

namespace PKMXEN.Logic
{
    public class ParcelLogic : IParcelLogic
    {
        IParcelRepository parcelRepository;

        public ParcelLogic(IParcelRepository parcelRepository)
        {
            this.parcelRepository = parcelRepository;
        }

        public void Create(Parcel parcel) // Adding a new order to the database
        {
            if (parcel.Address == "")
            {
                throw new ArgumentException("Address can not be empty!");
            }
            parcelRepository.Create(parcel);
        }

        public Parcel Read(int id)   // Returns the correct Order object based on the id parameter
        {
            return parcelRepository.Read(id);
        }

        public IEnumerable<Parcel> ReadAll()  // Returns the 'Orders' table
        {
            return parcelRepository.ReadAll();
        }

        public void Delete(int id)
        {
            parcelRepository.Delete(id);
        }

        public void Update(Parcel parcel)
        {
            parcelRepository.Update(parcel);
        }

        // Non-Crud Methods
        // In CarrierLogic.cs
    }
}
