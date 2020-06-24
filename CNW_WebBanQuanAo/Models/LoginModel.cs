using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CNW_WebBanQuanAo.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name="Tên tài khoản")]
        [Required(ErrorMessage ="Bạn phải nhập tài khoản")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Ghi nhớ tôi")]
        public bool RememberMe { get; set; }
        
    }
}