using PKMXEN.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKMXEN.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        ICarrierLogic cl;
        public StatController(ICarrierLogic cl)
        {
            this.cl = cl;
        }

        [HttpGet]   //GET:  /stat/AverageParcelNumber
        public double AverageParcelNumber()
        {
            return cl.AverageNumberOfParcels();
        }

        [HttpGet]   //GET:  /stat/MaxSalary
        public double MaxSalary()
        {
            return cl.MaxSalary();
        }

        [HttpGet]   //GET:  /stat/NameOfCourierByAge
        public IEnumerable<KeyValuePair<string, int>> NameOfCourierByAge()
        {
            return cl.NameOfCourierByAge(22);
        }

        [HttpGet]   //GET:  /stat/OrderDetailsByCourierName .
        public IEnumerable<KeyValuePair<int, string>> OrderDetailsByCourierName()
        {
            return cl.OrderDetailsByCourierName("Mack Brandy");
        }

        [HttpGet]   //GET:  /stat/CourierInfoByTrackingID
        public IEnumerable<KeyValuePair<string, double>> CourierInfoByTrackingID()
        {
            return cl.CourierInfoByTrackingID(2);
        }

        [HttpGet]   //GET:  /stat/OrderIDOfGivenAddress
        public IEnumerable<KeyValuePair<int, string>> OrderIDOfGivenAddress()
        {
            return cl.OrderIDOfGivenAddress();
        }

        [HttpGet]   //GET:  /stat/USAOrderValue
        public IEnumerable<KeyValuePair<DateTime, string>> USAOrderValue()
        {
            return cl.USAOrderValue();
        }

        [HttpGet]   //GET:  /stat/OrderWeightByID
        public IEnumerable<KeyValuePair<int, double>> OrderWeightByID()
        {
            return cl.OrderWeightByID(3);
        }
    }
}
