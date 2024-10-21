using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KAMStoreNew.ViewModel
{
    public class RegisterVM
    {
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [RegularExpression(@"^[A-Za-z0-9]*$", 
                               ErrorMessage = "Tên đăng nhập không được chứa ký tự đặc biệt!")]
        public string Username { get; set; }

        [MinLength(6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự!")]
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        [Compare("Password", ErrorMessage = "Không giống với mật khẩu đã nhập!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }
    }
}