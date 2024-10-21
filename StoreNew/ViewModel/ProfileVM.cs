using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KAMStoreNew.ViewModel
{
    public class ProfileVM
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Img { get; set; }
    }
}