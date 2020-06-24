using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNW_WebBanQuanAo.Models;
using PagedList;

namespace CNW_WebBanQuanAo.Areas.Admin.Controllers
{
    public class MATHANGController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/MATHANG
        public ActionResult Index(int? page)
        {
            var mATHANG = db.MATHANG.Include(m => m.NHASANXUAT);

            mATHANG = mATHANG.OrderBy(s => s.MaNSX);

            int pageSize = 6;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var model = mATHANG.ToPagedList(pageIndex, pageSize);

            return View(model);
        }

        // GET: Admin/MATHANG/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATHANG mATHANG = db.MATHANG.Find(id);
            if (mATHANG == null)
            {
                return HttpNotFound();
            }
            return View(mATHANG);
        }

        // GET: Admin/MATHANG/Create
        public ActionResult Create()
        {
            ViewBag.MaNSX = new SelectList(db.NHASANXUAT, "MaNSX", "TenNSX");
            return View();
        }

        // POST: Admin/MATHANG/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMH,TenMH,UrlAnh,KieuDang,ChatLieu,MaNSX,GiaBan")] MATHANG mATHANG)
        {
            if (ModelState.IsValid)
            {
                db.MATHANG.Add(mATHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNSX = new SelectList(db.NHASANXUAT, "MaNSX", "TenNSX", mATHANG.MaNSX);
            return View(mATHANG);
        }

        // GET: Admin/MATHANG/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATHANG mATHANG = db.MATHANG.Find(id);
            if (mATHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNSX = new SelectList(db.NHASANXUAT, "MaNSX", "TenNSX", mATHANG.MaNSX);
            return View(mATHANG);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMH,TenMH,UrlAnh,KieuDang,ChatLieu,MaNSX,GiaBan")] MATHANG mATHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mATHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNSX = new SelectList(db.NHASANXUAT, "MaNSX", "TenNSX", mATHANG.MaNSX);
            return View(mATHANG);
        }

        // GET: Admin/MATHANG/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATHANG mATHANG = db.MATHANG.Find(id);
            if (mATHANG == null)
            {
                return HttpNotFound();
            }
            return View(mATHANG);
        }

        // POST: Admin/MATHANG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MATHANG mATHANG = db.MATHANG.Find(id);
            db.MATHANG.Remove(mATHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
