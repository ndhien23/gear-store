using KAMStoreNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KAMStoreNew.ApiControllers
{
    public class OrdersController : ApiController
    {
        public List<Order> Get()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Order> orders = db.Orders.ToList();
            return orders;
        }
        public Order GetOrderByID(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Order order = db.Orders.Where(row => row.Id == id).FirstOrDefault();
            return order;
        }
    }
}
