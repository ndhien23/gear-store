using KAMStoreNew.Filters;
using KAMStoreNew.Identity;
using KAMStoreNew.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace KAMStoreNew.Controllers
{
    [MyAuthenFilter]
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());
            InformationUser informationUser = new InformationUser();
            informationUser.FullName = appUser.FullName;
            informationUser.PhoneNumber = appUser.PhoneNumber;
            informationUser.City = appUser.City;
            informationUser.District = appUser.District;
            informationUser.Ward = appUser.Ward;
            informationUser.Address = appUser.Address;
            ViewBag.AppUser = informationUser;
            decimal total = 0;
            long Quantity = 0;
            CompanyDBContext db = new CompanyDBContext();
            List<Cart> carts = db.Carts.Where(row => row.OrderId == 0 && row.UserID == appUser.Id).ToList();
            foreach (var item in carts)
            {
                if (item.Quantity == 0)
                {
                    return RedirectToAction("CartDelete", new { id = item.Cart_ID });
                }
                Quantity += item.Quantity;
                total += item.Quantity * (decimal)item.Cart_Price;
            }
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            string formattedMoney = total.ToString("C", cultureInfo);
            ViewBag.Total = formattedMoney;
            ViewData["Quantity"] = Quantity;
            ViewBag.S = TempData["ErrorCheckInfor"] as string;
            return View(carts);
        }

        [HttpPost]
        public ActionResult CartAdd(Product p)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());

            int count = 1;
            CompanyDBContext db = new CompanyDBContext();
            Cart cart = new Cart();
            List<Cart> carts = db.Carts.ToList();
            if (carts.Count != 0)
            {
                foreach (var item in carts)
                {
                    if (item.CartGroup_ID == p.ProductID && item.OrderId == 0 && appUser.Id == item.UserID)
                    {
                        count++;
                        item.Quantity = item.Quantity + 1;
                        db.SaveChanges();
                    }
                }
                if (count == 1)
                {
                    cart.Cart_Name = p.ProductName;
                    cart.Cart_Price = p.Price;
                    cart.Cart_Img = p.Img;
                    cart.CartGroup_ID = p.ProductID;
                    cart.Quantity = count;
                    cart.OrderId = 0;
                    cart.UserID = appUser.Id;
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }
            }
            else
            {
                cart.Cart_Name = p.ProductName;
                cart.Cart_Price = p.Price;
                cart.Cart_Img = p.Img;
                cart.CartGroup_ID = p.ProductID;
                cart.Quantity = count;
                cart.OrderId = 0;
                cart.UserID = appUser.Id;
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            return RedirectToAction("Detail", "Collection", new { id = p.ProductID , data = "cartadd"});
        }

        public ActionResult CartDelete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Cart cart = db.Carts.Where(row => row.Cart_ID == id).FirstOrDefault();
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddQuantity(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Cart cart = db.Carts.Where(row => row.Cart_ID == id).FirstOrDefault();
            cart.Quantity++;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveQuantity(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Cart cart = db.Carts.Where(row => row.Cart_ID == id).FirstOrDefault();
            cart.Quantity--;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ConfilmInfomation(InformationUser user)
        {
            if(ModelState.IsValid)
            {
                var appDBContext = new AppDBContext();
                var userStore = new AppUserStore(appDBContext);
                var userManager = new AppUserManager(userStore);
                AppUser appUser = userManager.FindById(User.Identity.GetUserId());

                ViewBag.AppUser = user;
                CompanyDBContext db = new CompanyDBContext();
                List<Cart> carts = db.Carts.Where(row => row.OrderId == 0 && row.UserID == appUser.Id).ToList();
                decimal total = 0;
                foreach (var item in carts)
                {
                    total += item.Quantity * (decimal)item.Cart_Price;
                }
                CultureInfo cultureInfo = new CultureInfo("vi-VN");
                string formattedMoney = total.ToString("C", cultureInfo);
                ViewBag.Total = formattedMoney;
                return View(carts);
            }
            else
            {
                TempData["ErrorCheckInfor"] = "InvalidInfor";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult OrderSuccess(InformationUser user)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());

            CompanyDBContext db = new CompanyDBContext();
            db.InformationUsers.Add(user);
            db.SaveChanges();
            List<Cart> carts = db.Carts.Where(row => row.OrderId == 0 && row.UserID == appUser.Id).ToList();
            decimal total = 0;
            long OrderId;
            Order order1 = db.Orders.OrderByDescending(row => row.OrderId).FirstOrDefault();
            if (order1 != null)
            {
                OrderId = order1.OrderId + 1;
            }
            else
            {
                OrderId = 1;
            }
            foreach (var item in carts)
            {
                item.OrderId = OrderId;
                total += item.Quantity * (decimal)item.Cart_Price;
            }
            db.SaveChanges();
            Order order = new Order();
            order.OrderId = OrderId;
            order.Total = total;
            order.OrderDate = DateTime.Now;
            InformationUser lastUser = db.InformationUsers.OrderByDescending(row => row.UserId).FirstOrDefault();
            order.UserId = lastUser.UserId;
            db.Orders.Add(order);
            db.SaveChanges();
            return View();
        }
    }
}