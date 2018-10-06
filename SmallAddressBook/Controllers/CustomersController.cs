using SmallAddressBook.Models;
using SmallAddressBook.ViewModels;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SmallAddressBook.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult New()
        {
            var viewModel = new NewCustomerViewModel();
            return View("CustomerForm",viewModel);
        }
        // GET: Customers
        public ViewResult Index()
        {
            //Get the list of customers in the database
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer, HttpPostedFileBase ImageFile)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer
                };
                return View("CustomerForm", viewModel);
            }
     
               //Extract Image File Name.
                var fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                var extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //Set the Image File Path.
                customer.ImagePath = "~/Images/" + fileName;
                
                //Save the Image File in Folder.
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                   ImageFile.SaveAs(fileName);
                 


            //Add New Customer or user info into the database


            _context.Customers.Add(customer);

            _context.SaveChanges();

            //Redirect to Index Action.
            return RedirectToAction("Index", "Customers");

         
        }
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Single(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
     
         public ActionResult Edit(int id)
        {
           var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
              return HttpNotFound();
          var viewModel = new NewCustomerViewModel
           {
               Customer = customer
           };

           return View("CustomerForm", viewModel);
        }
        
    }
}