using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ApiContext _ctx;

        public ServerController(ApiContext ctx){
            _ctx = ctx;
        }
        // GET api/customer
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var data = _ctx.Customers.OrderBy(c => c.Id);
            return Ok(data);
        }

        // GET api/customer/5
        [HttpGet("{id}", Name="GetServer")]
        public ActionResult<string> Get(int id)
        {
            var customer = _ctx.Customers.Find(id);
            return Ok(customer);
        }

        // POST api/customer
        [HttpPost]
        public ActionResult Post([FromBody] Customer customer)
        {
            if(customer == null)
            {
                return BadRequest();
            }
            _ctx.Customers.Add(customer);
            _ctx.SaveChanges(); 

            return CreatedAtRoute("GetCustomer", new { id = customer.Id}, customer);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
