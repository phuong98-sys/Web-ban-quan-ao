using CNW_WebBanQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;

namespace CNW_WebBanQuanAo.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private MyContext db = new MyContext();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            var hOADON = db.HOADON.Include(m => m.TAIKHOAN).Include(m => m.GIAODICH);

            return View(hOADON.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var test = db.HOADON.Include(d => d.GIAODICH.Select(g => g.SANPHAM))
                .Include(d => d.TAIKHOAN)
                .Single(h => h.MaHD == id);

            //var hOADON = (from hoadon in db.HOADON
            //              join giaodich in db.GIAODICH
            //              on hoadon.MaHD equals giaodich.MaHD
            //              where hoadon.MaHD == id
            //              join sanpham in db.SANPHAM
            //              on giaodich.MaQA equals sanpham.MaQA
            //              select new CTHOADON
            //              {
            //                  MaHD = hoadon.MaHD,
            //                  MaQA = giaodich.MaQA,
            //                  MaKH = hoadon.MaKH,
            //                  NgayDat = hoadon.NgayDat,
            //                  NgayGiao = hoadon.NgayGiao,
            //                  NguoiChot = hoadon.NguoiChot,
            //                  TrangThai = hoadon.TrangThai,
            //                  TenKhach = hoadon.TenKhach,
            //                  DiaChiKhach = hoadon.DiaChiKhach,
            //                  SoLuong = giaodich.SoLuong,
            //                  MaMH = sanpham.MaMH,
            //                  MaMau = sanpham.MaMau,
            //                  MaSize = sanpham.MaSize
            //              }).Include(h => h.MAU).Include(h => h.SIZE).Include(h => h.MATHANG).Include(h => h.TAIKHOAN); 
            
            //foreach (var item in hd)
            //{
            //    //    Console.WriteLine(item.MaHD);
            //    //    Console.WriteLine(item.MaSP);
            //    System.Diagnostics.Debug.WriteLine(item.MaQA);
            //}
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
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
                        user.isAdmin = 1;
                        user.SDT = model.SDT;
                        user.HoTen = model.HoTen;
                        user.Password = model.Password;
                        user.DiaChi = model.DiaChi;
                        user.Email = model.Email;
                        var result = db.TAIKHOAN.Add(user);
                        if (result != null)
                        {
                            ViewBag.Success = " Đăng kí thành công";
                            model = new RegisterModel();
                          
                        }
                        else
                        {
                            ModelState.AddModelError("", " Đăng kí thất bại");

                        }
                        db.SaveChanges();
                    }


                }
            }
          
            return View(model);
            
        }
      

        public bool CheckUserName(string Username)

        {
            return db.TAIKHOAN.Count(x => x.Username == Username) > 0;
        }

        public bool CheckEmail(string Email)

        {
            return db.TAIKHOAN.Count(x => x.Email == Email) > 0;
        }
        public ActionResult Logout()
        {
            Session["AdminLogin"] = null;
            return Redirect("~/Admin/Admin/Index");
          
        }
    }
}