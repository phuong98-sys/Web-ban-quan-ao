using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNW_WebBanQuanAo.Models;
using System.Web.Mvc;
using PagedList;

namespace CNW_WebBanQuanAo.Controllers
{
    public class ProductController : Controller
    {
        MyContext context = new MyContext();
        // GET: Product
        public ActionResult Index(string sortOrder, string searchString, int? page, int MaNSX = -1)
        {
            ViewBag.CurrentSort = sortOrder;

            var model = context.MATHANG.Where(m => m.MaMH != null);
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.TenMH.Contains(searchString));
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

            var mh = context.MATHANG.Find(id);
            var m = new CTMATHANG();
            m.mATHANG = mh;
            m.SpList = context.TTSANPHAM.Where(s => s.MaMH == id).ToList();
            //Console.WriteLine(mh.MaMH);

            return View(m);
        }

        void Test()
        {
            Console.WriteLine("abcd");
        }
    }
}