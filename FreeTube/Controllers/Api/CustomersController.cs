using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreeTube.Data;
using FreeTube.Models;
using System.Net;
using System.Web.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using FreeTube.Dtos;
using AutoMapper;

namespace FreeTube.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly FreeTubeContext _db;
        private readonly IMapper _mapper;
        public CustomersController(FreeTubeContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        //api/customers
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            List<Customer> customerList = await _db.Customers.ToListAsync();
            List<CustomerDto> customerDto = _mapper.Map<List<CustomerDto>>(customerList);
            return Ok(customerDto);
        }

        //api/customers/id
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            return customer;
        }


        //api/customers
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest); ;

            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();

            return customer;
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);            
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.Name = customer.Name;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipType = customer.MembershipType;

            await _db.SaveChangesAsync();
        }

        //api/customers/id
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async void DeleteCustomer(int id)
        {
            var customerInDb = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _db.Customers.Remove(customerInDb);
            await _db.SaveChangesAsync();
        }

    }
}
