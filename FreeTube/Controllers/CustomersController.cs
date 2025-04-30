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
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _db.MembershipType.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _db.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _db.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Customers"); 
        }

        
        public IActionResult Edit(int id)
        {
            Customer? customer = _db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            var membershipTypes = _db.MembershipType.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = customer
            };

            return View("CustomerForm", viewModel);
        }
    }
}
