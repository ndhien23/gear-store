using KAMStoreNew.Filters;
using KAMStoreNew.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KAMStoreNew.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        public ActionResult Index(string SortColumn = "CreatedDate", string IconClass = "fa-sort-down")
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.ToList();

            //Sort
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (SortColumn == "ProductName")
            {
                if (IconClass == "fa-sort-up")
                {
                    products = db.Products.OrderBy(row => row.ProductName).ToList();
                }
                else
                {
                    products = db.Products.OrderByDescending(row => row.ProductName).ToList();
                }
            }
            if (SortColumn == "Price")
            {
                if (IconClass == "fa-sort-up")
                {
                    products = db.Products.OrderBy(row => row.Price).ToList();
                }
                else
                {
                    products = db.Products.OrderByDescending(row => row.Price).ToList();
                }
            }
            if (SortColumn == "CreatedDate")
            {
                if (IconClass == "fa-sort-up")
                {
                    products = db.Products.OrderBy(row => row.CreatedDate).ToList();
                }
                else
                {
                    products = db.Products.OrderByDescending(row => row.CreatedDate).ToList();
                }
            }
            return View(products);
        }

        public ActionResult Create()
        {
            CompanyDBContext db = new CompanyDBContext();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.CreatedDate = DateTime.Now;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p, HttpPostedFileBase file)
        {
            CompanyDBContext db = new CompanyDBContext();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var allowEx = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(file.FileName).ToLower();
                    if (!allowEx.Contains(fileEx))
                    {
                        ModelState.AddModelError("Img", "Chỉ chấp nhận file ảnh jpg hoặc png.");
                        return View();
                    }
                    if (file.ContentLength > 2097152)
                    {
                        ModelState.AddModelError("Img", "Kích thước file phải nhỏ hơn 2MB.");
                        return View();
                    }

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Img"), fileName);
                    file.SaveAs(path);
                    p.Img = fileName;
                }
                else
                {
                    p.Img = "avatar.jpg";
                }    
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }    
        }

        public ActionResult Delete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product p, HttpPostedFileBase file)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Img"), fileName);
                file.SaveAs(path);
                product.Img = fileName;
            }
            product.ProductName = p.ProductName;
            product.Price = p.Price;
            product.AvailabilityStatus = p.AvailabilityStatus;
            product.CreatedDate = p.CreatedDate;
            product.CategoryID = p.CategoryID;
            product.BrandID = p.BrandID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}