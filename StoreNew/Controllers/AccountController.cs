using KAMStoreNew.Identity;
using KAMStoreNew.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using KAMStoreNew.ViewModel;

namespace KAMStoreNew.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM rmv)
        {
            if (ModelState.IsValid)
            {
                var appDBContext = new AppDBContext();
                var userStore = new AppUserStore(appDBContext);
                var userManager = new AppUserManager(userStore);
                var passwordHash = Crypto.HashPassword(rmv.Password);
                var user = new AppUser()
                {
                    FullName = rmv.Fullname,
                    UserName = rmv.Username,
                    Email = rmv.Email,
                    PasswordHash = passwordHash,
                    Img = "avatar.jpg",
                    Birthday = DateTime.Now,
                };
                IdentityResult result = userManager.Create(user);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM lvm)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.Find(lvm.Username, lvm.Password);
            if (user != null)
            {
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                if (userManager.IsInRole(user.Id, "Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("New Error", "Tên đăng nhập hoặc mật khẩu sai!");
                return View();
            }
        }

        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyProfile()
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

            ProfileVM profileVM = new ProfileVM()
            {
                Fullname = appUser.FullName,
                Username = appUser.UserName,
                Email = appUser.Email,
                Mobile = appUser.PhoneNumber,
                Birthday = appUser.Birthday,
                Address = appUser.Address,
                City = appUser.City,
                District = appUser.District,
                Ward = appUser.Ward,
                Img = appUser.Img,
            };
            return View(profileVM);
        }
        [HttpPost]
        public ActionResult MyProfile(ProfileVM pvm, HttpPostedFileBase file)
        {

            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());

            ProfileVM profileVM = new ProfileVM()
            {
                Fullname = appUser.FullName,
                Username = appUser.UserName,
                Email = appUser.Email,
                Mobile = appUser.PhoneNumber,
                Birthday = appUser.Birthday,
                Address = appUser.Address,
                City = appUser.City,
                District = appUser.District,
                Ward = appUser.Ward,
                Img = appUser.Img,
            };

            if (file != null && file.ContentLength > 0)
            {
                var allowEx = new[] { ".jpg", ".png" };
                var fileEx = Path.GetExtension(file.FileName).ToLower();
                if (!allowEx.Contains(fileEx))
                {
                    ModelState.AddModelError("Img", "Chỉ chấp nhận file ảnh jpg hoặc png.");
                    return View(profileVM);
                }
                if (file.ContentLength > 2097152)
                {
                    ModelState.AddModelError("Img", "Kích thước file phải nhỏ hơn 2MB.");
                    return View(profileVM);
                }
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Img"), fileName);
                file.SaveAs(path);
                appUser.Img = fileName;
            }
            appUser.FullName = pvm.Fullname;
            appUser.UserName = pvm.Username;
            appUser.Email = pvm.Email;
            appUser.PhoneNumber = pvm.Mobile;
            appUser.Birthday = pvm.Birthday;
            userManager.Update(appUser);
            return RedirectToAction("MyProfile");
        }

        [HttpPost]
        public ActionResult MyProfile2(ProfileVM pvm)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(User.Identity.GetUserId());
            appUser.Address = pvm.Address;
            appUser.City = pvm.City;
            appUser.District = pvm.District;
            appUser.Ward = pvm.Ward;
            userManager.Update(appUser);
            return RedirectToAction("MyProfile");
        }
    }
}