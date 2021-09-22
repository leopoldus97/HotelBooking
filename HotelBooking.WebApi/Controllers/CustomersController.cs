using System.Collections.Generic;
using HotelBooking.Core;
using Microsoft.AspNetCore.Mvc;


namespace HotelBooking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> repository;

        public CustomersController(IRepository<Customer> repos)
        {
            repository = repos;
        }

        // GET: api/customers
        [HttpGet(Name = "GetCustomers")]
        public IEnumerable<Customer> Get()
        {
            return repository.GetAll();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer) {
            repository.Add(customer);
            return CreatedAtRoute("GetCustomers", null);
        }

    }
}
