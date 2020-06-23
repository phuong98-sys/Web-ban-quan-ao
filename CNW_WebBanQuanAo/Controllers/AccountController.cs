using CNW_WebBanQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNW_WebBanQuanAo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MyContext context = new MyContext();
        public ActionResult Index()
        {
            return View();
            ///
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (CheckUserName(model.Username))
                {
                    ModelState.AddModelError("", "Tên này đã tồn tại, vui lòng nhập tên khác");
                }
                else if (CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email này đã được sử dụng");

                }
                else
                {
                    if ((model.Password != model.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "Xác thực mật khẩu không đúng");
                    }
                    else
                    {
                        var user = new TAIKHOAN();
                        user.Username = model.Username;
                        user.SDT = model.SDT;
                        user.HoTen = model.HoTen;
                        user.Password = model.Password;
                        user.DiaChi = model.DiaChi;
                        user.Email = model.Email;
                        var result = context.TAIKHOAN.Add(user);
                        if (result != null)
                        {
                            ViewBag.Success = " Đăng kí thành công";
                            model = new RegisterModel();
                            // return RedirectToAction("Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", " Đăng kí thất bại");

                        }
                        context.SaveChanges();
                    }


                }
            }
            //return RedirectToAction("Login");
            return View(model);
        }

        public bool CheckUserName(string Username)

        {
            return context.TAIKHOAN.Count(x => x.Username == Username) > 0;
        }

        public bool CheckEmail(string Email)

        {
            return context.TAIKHOAN.Count(x => x.Email == Email) > 0;
        }

        public ActionResult DangNhap(TAIKHOAN acc)
        {

            var result = context.TAIKHOAN.Where(a => a.Username.Equals(acc.Username) &&
                                                      a.Password.Equals(acc.Password)).FirstOrDefault();

            if (result != null && result.isAdmin == 0)   // đến trang của người mua 
            {
                Session["dnhap"] = acc;

                if (Session["dnhap"] != null && Session["CartSession"] != null)  // kiểm tra sesion đăng nhập để lúc mua sản phẩm tiếp theo sau khi đăng nhập thì
                {                                                                 // hệ thống không bắt đăng nhập lại để thêm sản phẩm tiếp vào giỏ hàng nữa

                   
                    return Redirect("https://localhost:44332/Home/Index");
                }
                else if (Session["dnhap"] != null && Session["CartSession"] == null)
                {
                   
                    return Redirect("https://localhost:44332/Home/Index");
                }


            }
            else if (result != null && result.isAdmin == 1)
            {
                return Redirect("https://localhost:44332/Ad/AdIndex"); // đến trang admin
            }


            return View();
        }

        [HttpPost]
        public ActionResult LoginPost()
        {
            ViewBag.Message = "User logins theirs account";
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            string t = Request.Form["checkbox"];
            //Response.Write(t);

            //string k = Response.Cookies["LoginCookie"]["username"];
            //string l = Response.Cookies["LoginCookie"]["password"];
            //Response.Write(Response.Cookies["LoginCookie"]["username"]);
            //Response.Write(Response.Cookies["LoginCookie"]["password"]);

            if (!string.IsNullOrEmpty(t) && t.Equals("remember"))
            {
                Response.Cookies["usernameCookie"].Value = username;
                Response.Cookies["passwordCookie"].Value = password;
            }

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var user = context.TAIKHOAN.Where(m => m.Username.Equals(username) && m.Password.Equals(password)).ToList();
                if (user.Count != 0)
                {
                    Response.Write("Dang nhap thanh cong");
                    Session["LoggedinUser"] = user[0];
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            TAIKHOAN tk = new TAIKHOAN();
            tk.Username = Request.Cookies["usernameCookie"].Value;
            tk.Password = Request.Cookies["passwordCookie"].Value;
            //Response.Write("ehe: " + tk.Username + tk.Password);

            return View(tk);
        }

    }
}