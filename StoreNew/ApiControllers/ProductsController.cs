using KAMStoreNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KAMStoreNew.ApiControllers
{
    public class ProductsController : ApiController
    {
        public List<Product> Get()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.ToList();
            return products;
        }
        public Product GetProductByID(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return product;
        }
    }
}
