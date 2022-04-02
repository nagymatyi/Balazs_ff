using PKMXEN.Models;
using System.Linq;

namespace PKMXEN.Repository
{
    public interface ICarrierRepository
    {
        void Create(Carrier carrier);
        void Delete(int id);
        Carrier Read(int id);
        IQueryable<Carrier> ReadAll();
        void Update(Carrier carrier);
    }
}