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
using Microsoft.AspNetCore.Http;



namespace FreeTube.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private readonly FreeTubeContext _db;

        public NewRentalsController(FreeTubeContext db)
        {
            _db = db;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult> CreateRental(NewRentalDto newRental)
        {
            
            Customer? customer = await _db.Customers.SingleAsync(c => c.Id == newRental.CustomerId);

            var movies = _db.Movies.Where(m => newRental.MovieIdes.Contains(m.Id));

            foreach (var movie in movies) {
                    var rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.UtcNow
                    };
                    await _db.Rental.AddAsync(rental);
                }
                    await _db.SaveChangesAsync();
            
            return Ok();
              
        }
    }
}
