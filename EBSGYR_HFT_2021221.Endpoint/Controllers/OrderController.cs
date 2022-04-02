using PKMXEN.Logic;
using PKMXEN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKMXEN.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderLogic ol;

        public OrderController(IOrderLogic ol)
        {
            this.ol = ol;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return ol.ReadAll();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return ol.Read(id);
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] Order value)
        {
            ol.Create(value);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Order value)
        {
            ol.Update(value);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ol.Delete(id);
        }
    }
}
