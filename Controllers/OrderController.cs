using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApiContext _ctx;

        public OrderController(ApiContext ctx){
            _ctx = ctx;
        }
        // GET api/order/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public ActionResult<IEnumerable<string>> Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Orders.Include(c => c.Customer)
                    .OrderByDescending(o => o.Placed);
            var page = new PaginatedResonse<Order>(data, pageIndex, pageSize);
            var totalCount = data.Count();
            var totalPage = Math.Ceiling((double)totalCount /pageSize );

            var response = new {
                Page =  page,
                TotalPages = totalPage,

            };
            return Ok(response);
        }

        // Get api/order/ByState
        [HttpGet("ByState")]
        public ActionResult<string> ByState()
        {
            var orders = _ctx.Orders.Include( c => c.Customer).ToList();
            var groupedResult = orders.GroupBy( o => o.Customer.State)
                .ToList()
                .Select(grp => new {
                    State = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(res => res.Total)
                .ToList();
            return Ok(groupedResult);
        }

        // Get api/order/ByCustomer
        [HttpGet("ByCustomer/{n}")]
        public ActionResult<string> ByCustomer(int n)
        {
            var orders = _ctx.Orders.Include( c => c.Customer).ToList();
            var groupedResult = orders.GroupBy( o => o.Customer.Id)
                .ToList()
                .Select(grp => new {
                    Customer = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(res => res.Total)
                .Take(n)
                .ToList();
            return Ok(groupedResult);
        }

        // GET api/order/5
        [HttpGet("GetOrder/{id}", Name="GetOrder")]
        public ActionResult<string> Get(int id)
        {
            var order = _ctx.Orders.Include(c => c.Customer).ToList().Find(o => o.Id == id);
            return Ok(order);
        }

        // POST api/order
        [HttpPost]
        public ActionResult Post([FromBody] Order order)
        {
            if(order == null)
            {
                return BadRequest();
            }
            _ctx.Orders.Add(order);
            _ctx.SaveChanges(); 

            return CreatedAtRoute("GetOrder", new { id = order.Id}, order);

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
