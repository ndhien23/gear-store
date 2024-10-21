using KAMStoreNew.Filters;
using KAMStoreNew.Identity;
using KAMStoreNew.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KAMStoreNew.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            AppDBContext db = new AppDBContext();
            List<AppUser> users = db.Users.ToList();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RegisterVM rmv)
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
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(id);
            userManager.Delete(appUser);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(id);
            return View(appUser);
        }
        [HttpPost]
        public ActionResult Edit(string id, EditVM p)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            AppUser appUser = userManager.FindById(id);
            appUser.FullName = p.Fullname;
            appUser.UserName = p.Username;
            appUser.Email = p.Email;
            appUser.Address = p.Address;
            appUser.City = p.City;
            appUser.PhoneNumber = p.PhoneNumber;
            appUser.Birthday = p.Birthday;
            if (!string.IsNullOrEmpty(p.Password))
            {
                var passwordHash = Crypto.HashPassword(p.Password);
                appUser.PasswordHash = passwordHash;
            }    
            userManager.Update(appUser);
            return RedirectToAction("Index");
        }
    }
}