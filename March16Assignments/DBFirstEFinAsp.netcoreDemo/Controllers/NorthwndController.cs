using DBFirstEFinAsp.netcoreDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBFirstEFinAsp.netcoreDemo.Controllers
{
    public class NorthwndController : Controller
    {
        public IActionResult SpainCustomers()
        {
            NORTHWNDContext obj = new NORTHWNDContext();
            var spainCust = obj.Customers.Where(x => x.Country == "Spain").
            Select(x => new SpainCustomersViewModel
            {
                Cid = x.CustomerId, Cname = x.ContactName, Comname = x.CompanyName
             }).ToList();
            return View(spainCust);
        }

        public IActionResult searchCustomer(string contactname)
        {
            NORTHWNDContext obj = new NORTHWNDContext();
            var searchCust = from customer in obj.Customers
                             where customer.ContactName == contactname
                             select new Customer
                             {
                                 ContactName = customer.ContactName,
                                 ContactTitle = customer.ContactTitle,
                                 CompanyName = customer.CompanyName
                             };
            var searchCust2 = obj.Customers.Where(x => x.ContactName == contactname).
                Select(x => new Customer
                {
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    CompanyName = x.CompanyName
                });
            var query1 = searchCust.Single();
            var query2 = searchCust2.Single();
            return View(query1);
        }
        public ActionResult ProductsInCategory(string categoryname)
        {
            NORTHWNDContext obj = new NORTHWNDContext();
            var productsInCategory = obj.Products
              .Where(x => x.Category.CategoryName == categoryname)
              .Select(x => new ProdCat
              {
                  prodname = x.ProductName,
                  catname = x.Category.CategoryName
              }).ToList();
            return View(productsInCategory);
        }

        // give all the customers whose order range is above some specific value
        public ActionResult OrderRange(string range)
        {
            NORTHWNDContext obj = new NORTHWNDContext();
            var range1 = Convert.ToInt16(range);
            var custOrderCount = obj.Customers
               .Where(x => x.Orders.Count > range1)
               .Select(x => new Customer
               {
                   CustomerId = x.CustomerId,
                   ContactName = x.ContactName,
               });
            return View(custOrderCount);
        }
    }
}
