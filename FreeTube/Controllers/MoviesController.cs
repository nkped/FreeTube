using FreeTube.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
