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
using Microsoft.AspNetCore.Http.Extensions;

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
            List<Customer> customers = await _db.Customers
                .Include((c) => c.MembershipType)
                .ToListAsync();
            List<CustomerDto> customersDto = _mapper.Map<List<Customer>, List<CustomerDto>>(customers);
            return Ok(customersDto);
        }

        //api/customers/id
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            Customer? customer = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);
             
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }


        //api/customers
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<CustomerDto>> AddCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer = _mapper.Map<Customer>(customerDto);
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.GetEncodedUrl() + "/" + customerDto.Id), customerDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);            
            if (customerInDb == null)
                return NotFound();

            _mapper.Map(customerDto, customerInDb);
            await _db.SaveChangesAsync();
            return Ok(customerDto);
        }

        //api/customers/id
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customerInDb = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _db.Customers.Remove(customerInDb);
            await _db.SaveChangesAsync();
            return Ok(customerInDb);
        }

    }
}
