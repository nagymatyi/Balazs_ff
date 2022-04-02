using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PKMXEN.Logic;
using PKMXEN.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKMXEN.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        ICarrierLogic cl;

        public CarrierController(ICarrierLogic cl)
        {
            this.cl = cl;
        }

        // GET: api/<CarrierController>
        [HttpGet]
        public IEnumerable<Carrier> Get()
        {
            return cl.ReadAll();
        }

        // GET api/<CarrierController>/5
        [HttpGet("{id}")]
        public Carrier Get(int id)
        {
            return cl.Read(id);
        }

        // POST api/<CarrierController>
        [HttpPost]
        public void Post([FromBody] Carrier value)
        {
            cl.Create(value);
        }

        // PUT api/<CarrierController>/5
        [HttpPut]
        public void Put([FromBody] Carrier value)
        {
            cl.Update(value);
        }

        // DELETE api/<CarrierController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cl.Delete(id);
        }
    }
}
