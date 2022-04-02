using PKMXEN.Logic;
using PKMXEN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKMXEN.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        IParcelLogic pl;
        public ParcelController(IParcelLogic pl)
        {
            this.pl = pl;
        }

        // GET: api/<ParcelController>
        [HttpGet]
        public IEnumerable<Parcel> Get()
        {
            return pl.ReadAll();
        }

        // GET api/<ParcelController>/5
        [HttpGet("{id}")]
        public Parcel Get(int id)
        {
            return pl.Read(id);
        }

        // POST api/<ParcelController>
        [HttpPost]
        public void Post([FromBody] Parcel value)
        {
            pl.Create(value);
        }

        // PUT api/<ParcelController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Parcel value)
        {
            pl.Update(value);
        }

        // DELETE api/<ParcelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            pl.Delete(id);
        }
    }
}
