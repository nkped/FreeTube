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
        //this action uses 'defensive' programming (vs 'optimistic' programming)
        //this approach is actually most suited for api's
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult CreateRental(NewRentalDto newRental)
        {
            Customer? customer = _db.Customers.Single(c => c.Id == newRental.CustomerId);

            var movies = _db.Movies.Where(m => newRental.MovieIdes.Contains(m.Id)).ToList();

            foreach (var movie in movies) {

                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable--;

                var rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.UtcNow
                    };
                _db.Rental.Add(rental);
                }
                
            _db.SaveChanges();
            
            return Ok();
              
        }
    }
}
