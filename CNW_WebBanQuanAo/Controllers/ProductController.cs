using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNW_WebBanQuanAo.Models;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;

namespace CNW_WebBanQuanAo.Controllers
{
    public class ProductController : Controller
    {
        MyContext context = new MyContext();
        // GET: Product
        public ActionResult Index(string sortOrder, string searchString, int? minPrice, int? maxPrice, int? page, int MaNSX = -1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NSX = MaNSX;

            var model = context.MATHANG.Where(m => m.MaMH > -1);
            //var model = context.MATHANG;
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.TenMH.Contains(searchString));
            }
            if (MaNSX != -1)
            {
                model = model.Where(s => s.MaNSX == MaNSX);
            }

            if (minPrice.HasValue)
            {
                ViewBag.minPrice = minPrice;
                model = model.Where(s => s.GiaBan >= minPrice);
            }
            else if (!(ViewBag.minPrice is null))
            {
                minPrice = ViewBag.minPrice;
                model = model.Where(s => s.GiaBan >= minPrice);
            }


            if (maxPrice.HasValue)
            {
                ViewBag.maxPrice = maxPrice;
                model = model.Where(s => s.GiaBan <= maxPrice);
            }
            else if (!(ViewBag.maxPrice is null))
            {
                maxPrice = ViewBag.maxPrice;
                model = model.Where(s => s.GiaBan <= maxPrice);
            }


            switch (sortOrder)
            {
                case "asc":
                    model = model.OrderBy(s => s.GiaBan);
                    break;
                case "desc":
                    model = model.OrderByDescending(s => s.GiaBan);
                    break;
                default:
                    model = model.OrderBy(s => s.TenMH);
                    break;
            }

            int pageSize = 6;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<MATHANG> mh = null;

            //model.OrderBy(s => s.GiaBan).Skip(pageIndex * pageSize).Take(pageSize);
            mh = model.ToPagedList(pageIndex, pageSize);

            return View(mh);
        }
        public ActionResult Detail(int id = 1)
        {
            //if (id is null)
            //    return View();

            //var mh = context.MATHANG.Find(id);
            var prods = context.MATHANG.Include(s => s.SANPHAM.Select(g => g.MAU)).Include(s => s.NHASANXUAT)
                .SingleOrDefault(h => h.MaMH == id);

            var colors = from sp in prods.SANPHAM
                         group sp by sp.MaMau into mau
                         select mau;

            //var m = new CTMATHANG();
            //m.mATHANG = mh;
            //m.SpList = context.TTSANPHAM.Where(s => s.MaMH == id).ToList();
            //Console.WriteLine(mh.MaMH);

            return View(prods);
        }
        public JsonResult SizeByColor(string colorID, int prodID)
        {
            //var sizeList = context.SANPHAM.Where(s => s.MaMH == prodID && s.MaMau == colorID)
            //    .ToList();

            var sizeList = from sp in context.SANPHAM
                           where sp.MaMau == colorID && sp.MaMH == prodID
                           select new
                           {
                               MaQA = sp.MaQA,
                               MaMau = sp.MaMau,
                               MaSize = sp.MaSize
                           };

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //return Json(jss.Serialize(sizeList), JsonRequestBehavior.AllowGet);
            return Json(sizeList.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductsByBrand()
        {
            //List<MATHANG> products = context.MATHANG.Where(m => m.MaNSX == 1).ToList();

            var products = from mh in context.MATHANG
                           where mh.MaNSX == 1
                           select new
                           {
                               mh.MaMH,
                               mh.TenMH,
                               mh.UrlAnh,
                               mh.GiaBan
                           };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return Json(jss.Serialize(products), JsonRequestBehavior.AllowGet);
        }
    }
}