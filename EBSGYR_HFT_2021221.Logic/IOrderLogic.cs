using PKMXEN.Models;
using System.Collections.Generic;

namespace PKMXEN.Logic
{
    public interface IOrderLogic
    {
        double AVGPrice();
        IEnumerable<KeyValuePair<string, double>> AVGPriceByCarriers();
        void Create(Order order);
        void Delete(int id);
        Order Read(int id);
        IEnumerable<Order> ReadAll();
        void Update(Order order);
    }
}