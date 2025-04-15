using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeTube.Data;
using FreeTube.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreeTube.Controllers
{
    public class CustomersController : Controller
    {
        private readonly FreeTubeContext _db;
        public CustomersController(FreeTubeContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View(_db.Customers.Include("MembershipType").ToList());
        }
    }
}
