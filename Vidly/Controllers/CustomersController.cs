using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Vidly.Models;
using System;
using Vidly.Data;
using Vidly.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Vidly.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Authorize(Policy = "OnlyAdminAccess")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);

            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ViewResult Index()
        {
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            //return View(customers);
            return View();
        }

        //public ActionResult Details(int id)
        //{
        //    var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

        //    if (customer == null)
        //        return NotFound();

        //    return View(customer);
        //}

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes
            };

            return View("CustomerForm", viewModel);

        }



    }
}