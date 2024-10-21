using KAMStoreNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KAMStoreNew.ApiControllers
{
    public class BrandsController : ApiController
    {
        public List<Brand> Get(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Brand> brands = db.Brands.Where(row => row.CategoryID == id).ToList();
            return brands;
        }
    }
}
