using KAMStoreNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KAMStoreNew.ApiControllers
{
    public class CartsController : ApiController
    {
        public List<Cart> GetCartByID(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Cart> carts = db.Carts.Where(row => row.OrderId == id).ToList();
            return carts;
        }
    }
}
