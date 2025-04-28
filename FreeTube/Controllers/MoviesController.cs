using FreeTube.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreeTube.Models;
using FreeTube.ViewModels;

namespace FreeTube.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FreeTubeContext _db;
        public MoviesController(FreeTubeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Movies.Include("Genre").ToList());
        }

        public IActionResult Details(int id)
        {
            var movie = _db.Movies.Include("Genre").SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }   

        [Route("movies/released/{year}/{month:regex(\\d{{2}}):range(1,12)}")]
        public IActionResult ByReleaseYear(int year, int month)
        {
            return Content($"Year: {year}, Month: {month}");
        }

        public IActionResult New()
        {
            var genres = _db.Genre.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }
    }
}
