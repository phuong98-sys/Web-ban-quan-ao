using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CNW_WebBanQuanAo.Models
{
    public class RegisterModel
    {
        [Key]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài tên đăng nhập ít nhất 6 kí tự")]
        [Display(Name="User Name ")]
        [Required(ErrorMessage ="Bạn phải nhập tên đăng nhập")]
        public string Username { set; get; }
        [Display(Name = "Họ tên ")]
        public string HoTen { get; set; }

        [Display(Name = "Số điện thoại ")]
        [Required(ErrorMessage = "Bạn phải nhập số điện thoại")]
        public string SDT { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage ="Bạn phải nhập password")]
        [Display(Name = "Mật khẩu")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Độ dài mật khẩu ít nhất 6 kí tự")]
        public string Password { get; set; }
      
        [Required(ErrorMessage = "Bạn phải nhập lại password để xác thực")]   
        [Display(Name = "Xác nhận mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 kí tự")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Bạn phải nhập Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        //[Display(Name = "Check Admin")]
        //public int isAdmin { get; set; }

    }
}