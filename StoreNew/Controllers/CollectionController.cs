using KAMStoreNew.Filters;
using KAMStoreNew.Identity;
using KAMStoreNew.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace KAMStoreNew.Controllers
{
    public class CollectionController : Controller
    {
        // GET: Collection
        public ActionResult AllProduct(string SortButton = "PhoBien", string SortStyle = "", string search = "", int page = 1)
        {

            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.Where(row => row.ProductName.Contains(search)).ToList();
            ViewBag.search = search;
            ViewBag.Content = "Tất cả sản phẩm";
            ViewBag.title = "Tất cả sản phẩm";

            //Quantity
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

            //Sort
            ViewBag.SortButton = SortButton;
            ViewBag.SortStyle = SortStyle;
            if (SortButton == "MoiNhat")
            {
                products = products.OrderByDescending(row => row.CreatedDate).ToList();
            }
            else if (SortButton == "Gia")
            {
                if (SortStyle == "TangDan")
                {
                    products = products.OrderBy(row => row.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(row => row.Price).ToList();
                }
            }

            //Paging
            int NoOfRecordPerPage = 8;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPage = NoOfPages;
            products = products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(products);
        }

        public ActionResult Laptop(int categoryid, string SortButton = "PhoBien", string SortStyle = "", int page = 1)
        {

            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.Where(row => row.CategoryID == categoryid).ToList();
            Category category = db.Categories.Where(row => row.CategoryID == categoryid).FirstOrDefault();
            ViewBag.Content = "Tất cả sản phẩm / " + category.CategoryName;
            ViewBag.title = category.CategoryName;
            ViewBag.categoryid = categoryid;

            //Quantity
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

            //Sort
            ViewBag.SortButton = SortButton;
            ViewBag.SortStyle = SortStyle;
            if (SortButton == "MoiNhat")
            {
                products = products.OrderByDescending(row => row.CreatedDate).ToList();
            }
            else if (SortButton == "Gia")
            {
                if (SortStyle == "TangDan")
                {
                    products = products.OrderBy(row => row.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(row => row.Price).ToList();
                }
            }

            //Paging
            int NoOfRecordPerPage = 8;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            if (NoOfPages > 0)
            {
                ViewBag.NoOfPage = NoOfPages;
            }
            else
            {
                ViewBag.NoOfPage = 5;
            }
            products = products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(products);
        }
        public ActionResult BrandLaptop(int brandid, int categoryid, string SortButton = "PhoBien", string SortStyle = "")
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.Where(row => row.BrandID == brandid && row.CategoryID == categoryid).ToList();
            Brand brand = db.Brands.Where(row => row.CategoryID == categoryid && row.BrandID == brandid).FirstOrDefault();
            ViewBag.Content = "Tất cả sản phẩm / " + brand.Category.CategoryName + " " + brand.BrandName;
            ViewBag.title = brand.Category.CategoryName + " " + brand.BrandName;
            ViewBag.categoryid = categoryid;
            ViewBag.brandid = brandid;

            //Quantity
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

            //Sort
            ViewBag.SortButton = SortButton;
            ViewBag.SortStyle = SortStyle;
            if (SortButton == "MoiNhat")
            {
                products = products.OrderByDescending(row => row.CreatedDate).ToList();
            }
            else if (SortButton == "Gia")
            {
                if (SortStyle == "TangDan")
                {
                    products = products.OrderBy(row => row.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(row => row.Price).ToList();
                }
            }
            return View(products);
        }
        public ActionResult Detail(int id, string data)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());

            CompanyDBContext db = new CompanyDBContext();
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            ViewBag.Content = product.Category.CategoryName + " / " + product.ProductName;
            ViewBag.data = data;
            if (appUser != null)
            {
                ViewBag.S2 = "error";
            }

            //Quantity
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
            return View(product);
        }
    }
}