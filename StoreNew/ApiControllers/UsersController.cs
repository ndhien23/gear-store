using KAMStoreNew.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KAMStoreNew.ApiControllers
{
    public class UsersController : ApiController
    {
        public List<AppUser> Get()
        {
            AppDBContext db = new AppDBContext();
            List<AppUser> users = db.Users.ToList();
            return users;
        }
        public AppUser GetUserByID(string id)
        {
            AppDBContext db = new AppDBContext();
            AppUser user = db.Users.Where(row => row.Id == id).FirstOrDefault();
            return user;
        }
    }
}
