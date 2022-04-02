using PKMXEN.Models;
using System;
using System.Collections.Generic;

namespace PKMXEN.Logic
{
    public interface ICarrierLogic
    {
        double AverageNumberOfParcels();

        double MaxSalary();
        IEnumerable<KeyValuePair<string, int>> NameOfCourierByAge(int age);
        IEnumerable<KeyValuePair<int, double>> OrderWeightByID(int orderID);
        void Create(Carrier carrier);
        void Delete(int id);
        IEnumerable<KeyValuePair<int, string>> OrderDetailsByCourierName(string name);
        IEnumerable<KeyValuePair<int, string>> OrderIDOfGivenAddress();
        IEnumerable<KeyValuePair<string, double>> CourierInfoByTrackingID(int trackingID);
        Carrier Read(int id);
        IEnumerable<Carrier> ReadAll();
        void Update(Carrier carrier);
        IEnumerable<KeyValuePair<DateTime, string>> USAOrderValue();
    }
}