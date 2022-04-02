using PKMXEN.Models;
using System.Linq;

namespace PKMXEN.Repository
{
    public interface IOrderRepository
    {
        void Create(Order order);
        void Delete(int id);
        Order Read(int id);
        IQueryable<Order> ReadAll();
        void Update(Order order);
    }
}