using PKMXEN.Models;
using System.Collections.Generic;

namespace PKMXEN.Logic
{
    public interface IParcelLogic
    {
        void Create(Parcel parcel);
        void Delete(int id);
        Parcel Read(int id);
        IEnumerable<Parcel> ReadAll();
        void Update(Parcel parcel);
    }
}