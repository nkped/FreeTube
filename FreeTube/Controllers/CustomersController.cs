using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeTube.Data;
using FreeTube.Models;
using Microsoft.AspNetCore.Mvc;

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
            List<Customer> customers = new List
            {
                new Customer {Id = 1, Name = "John Doe",
                IsSubscribedToNewsletter = true,
                Birthdate = new DateTime(1990, 1, 1),
                MembershipTypeId = 1 },
                new Customer {Id = 1, Name = "Ron Swanson",
                IsSubscribedToNewsletter = false,
                Birthdate = new DateTime(1977, 10, 10),
                MembershipTypeId = 2 }
            };
            return View(customers);
        }
    }
}
