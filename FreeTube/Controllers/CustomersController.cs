using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeTube.Data;
using FreeTube.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreeTube.ViewModels;

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
            var customers = _db.Customers.Include("MembershipType").ToList();
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            Customer? customer = _db.Customers.Include("MembershipType").SingleOrDefault(c => c.Id == id);
            
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        public IActionResult New()
        {
            var membershipTypes = _db.MembershipType.ToList();
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }

    }
}
