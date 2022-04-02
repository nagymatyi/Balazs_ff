using PKMXEN.Models;
using System.Linq;

namespace PKMXEN.Repository
{
    public interface IParcelRepository
    {
        void Create(Parcel parcel);
        void Delete(int id);
        Parcel Read(int id);
        IQueryable<Parcel> ReadAll();
        void Update(Parcel parcel);
    }
}