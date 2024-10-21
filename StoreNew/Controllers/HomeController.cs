using KAMStoreNew.Identity;
using KAMStoreNew.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KAMStoreNew.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());
            CompanyDBContext db = new CompanyDBContext();
            if (appUser != null)
            {
                long Quantity = 0;
                List<Cart> carts = db.Carts.Where(row => row.OrderId == 0 && row.UserID == appUser.Id).ToList();
                foreach (var item in carts)
                {
                    Quantity += item.Quantity;
                }
                ViewData["Quantity"] = Quantity;
            }
            else
            {
                ViewData["Quantity"] = 0;
            }

            List<Product> products = db.Products.ToList();

            return View(products);
        }

        public JsonResult Search()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.ToList();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(products, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}