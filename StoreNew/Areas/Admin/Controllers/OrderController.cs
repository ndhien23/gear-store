using KAMStoreNew.Filters;
using KAMStoreNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KAMStoreNew.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Order> orders = db.Orders.ToList();
            return View(orders);
        }

        public ActionResult Delete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Order order = db.Orders.Where(row => row.Id == id).FirstOrDefault();
            db.Orders.Remove(order);
            List<Cart> carts = db.Carts.Where(row => row.OrderId == order.OrderId).ToList();
            foreach(Cart cart in carts)
            {
                db.Carts.Remove(cart);
            }
            InformationUser user = db.InformationUsers.Where(row => row.UserId == order.UserId).FirstOrDefault();
            db.InformationUsers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}