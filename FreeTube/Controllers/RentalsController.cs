using Microsoft.AspNetCore.Mvc;

namespace FreeTube.Controllers
{
    public class RentalsController : Controller
    {
        public IActionResult NewRental()
        {
            return View();
        }
    }
}
