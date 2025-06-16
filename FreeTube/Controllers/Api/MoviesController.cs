using FreeTube.Data;
using FreeTube.Dtos;
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
        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            List<Movie> moviesInDb = await _db.Movies
                .Include(m => m.Genre)
                .ToListAsync();

            return _mapper.Map<List<MovieDto>>(moviesInDb);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        {
            Movie? movieInDb = await _db.Movies.SingleOrDefaultAsync(m => m.Id == id);
            
            if (movieInDb == null)
                return NotFound();

            return _mapper.Map<MovieDto>(movieInDb);   
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<MovieDto>> AddMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Movie movie = _mapper.Map<Movie>(movieDto);

            await _db.Movies.AddAsync(movie);
            await _db.SaveChangesAsync();
            movieDto.Id = movie.Id;

            return Created(new Uri(Request.GetEncodedUrl() + "/" + movieDto.Id), movieDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Movie? movieInDb = await _db.Movies.SingleOrDefaultAsync(m => m.Id == id);
            
            if (movieInDb == null)
                return NotFound();

            _mapper.Map(movieDto, movieInDb);
            
            await _db.SaveChangesAsync();

            return Ok(movieDto);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            Movie? movieInDb = await _db.Movies.SingleOrDefaultAsync((m) => m.Id == id);
            if (movieInDb == null)
                return BadRequest();

            MovieDto movieDto = _mapper.Map<MovieDto>(movieInDb);
            _db.Movies.Remove(movieInDb);
            await _db.SaveChangesAsync();
            
            return Ok(movieDto);
        }


    }
}
