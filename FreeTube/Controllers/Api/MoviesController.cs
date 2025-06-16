using FreeTube.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FreeTube.Models;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Http.Extensions;

//create MovieDto and add AutoMapper

namespace FreeTube.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly FreeTubeContext _db;
        private readonly IMapper _mapper;
        public MoviesController(FreeTubeContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;            
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _db.Movies.ToListAsync();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            Movie? movie = await _db.Movies.SingleOrDefaultAsync(m => m.Id == id);
            
            if (movie == null)
                return NotFound();
            
            return movie;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _db.Movies.AddAsync(movie);
            await _db.SaveChangesAsync();
            
            return Created(new Uri(Request.GetEncodedUrl() + "/" + movie.Id), movie);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            Movie? movieInDb = await _db.Movies.SingleOrDefaultAsync(m => m.Id == id);
            
            if (movieInDb == null)
                return NotFound();

            //mapper
            movieInDb.Title = movie.Title;
            
            await _db.SaveChangesAsync();

            return Ok(movie);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            Movie? movieInDb = await _db.Movies.SingleOrDefaultAsync((m) => m.Id == id);
            if (movieInDb == null)
                return BadRequest();
            
            _db.Movies.Remove(movieInDb);
            await _db.SaveChangesAsync();
            
            return Ok(movieInDb);
        }


    }
}
