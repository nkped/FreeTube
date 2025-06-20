using FreeTube.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreeTube.Models;
using FreeTube.ViewModels;
using System.Web.Http;

namespace FreeTube.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly FreeTubeContext _db;
        private readonly ApplicationDbContext _identityContext;
        public MoviesController(FreeTubeContext db, ApplicationDbContext identityContext)
        {
            _db = db;
            _identityContext = identityContext;

        }
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View("List");
            return View("ReadOnlyList");
        }

        public IActionResult Details(int id)
        {
            var movie = _db.Movies.Include("Genre").SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }   

        [Microsoft.AspNetCore.Mvc.Route("movies/released/{year}/{month:regex(\\d{{2}}):range(1,12)}")]
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
        public ActionResult Edit(int id)
        {
            var movie = _db.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _db.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _db.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                _db.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _db.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Title = movie.Title;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.DateAdded = movie.DateAdded;
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }


}
