using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNW_WebBanQuanAo.Models;
using Microsoft.Ajax.Utilities;

namespace CNW_WebBanQuanAo.Areas.Admin.Controllers
{
    public class SANPHAMController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/SANPHAM
        public ActionResult Index()
        {
            var sANPHAM = db.SANPHAM.Include(s => s.MATHANG).Include(s => s.MAU).Include(s => s.SIZE);
            return View(sANPHAM.ToList());
        }

        //[HttpPost]
        public string SayHello()
        {
            return "hello tu controller";
        }

        public int GetSANPHAMQuantity(int idMH, string idSize, int idMau)
        {
            //var sp = db.SANPHAM.Where(s => s.MaMau.Equals(idMau) && s.MaMH.Equals(idMH) && s.MaSize.Equals(idSize)).FirstOrDefault();
            //var sp = db.SANPHAM.Where(s => s.MaMau == idMau).FirstOrDefault();
            var sp = db.SANPHAM.Where(s => s.MaMau == idMau 
                    && s.MaMH == idMH && s.MaSize == idSize).FirstOrDefault();
            if (sp is null)
                return -1;

            return Convert.ToInt32(sp.SoLuong);
        }

        // GET: Admin/SANPHAM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAM/Create
        public ActionResult Create()
        {
            ViewBag.MaMH = new SelectList(db.MATHANG, "MaMH", "TenMH");
            ViewBag.MaMau = new SelectList(db.MAU, "MaMau", "TenMau");
            ViewBag.MaSize = new SelectList(db.SIZE, "MaSize", "MaSize");
            return View();
        }

        // POST: Admin/SANPHAM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQA,MaMH,MaSize,MaMau,SoLuong")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAM.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMH = new SelectList(db.MATHANG, "MaMH", "TenMH", sANPHAM.MaMH);
            ViewBag.MaMau = new SelectList(db.MAU, "MaMau", "TenMau", sANPHAM.MaMau);
            ViewBag.MaSize = new SelectList(db.SIZE, "MaSize", "MaSize", sANPHAM.MaSize);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAM/Edit/5
        public ActionResult Edit(int? id = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            //ViewBag.MaMH = new SelectList(db.MATHANG, "MaMH", "TenMH", sANPHAM.MaMH);
            ViewBag.MaMH = db.MATHANG.ToList();
            ViewBag.MaMau = new SelectList(db.MAU, "MaMau", "TenMau", sANPHAM.MaMau);
            //ViewBag.MaSize = new SelectList(db.SIZE, "MaSize", "MaSize", sANPHAM.MaSize);
            ViewBag.MaSize = db.SIZE.ToList();
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQA,MaMH,MaSize,MaMau,SoLuong")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMH = new SelectList(db.MATHANG, "MaMH", "TenMH", sANPHAM.MaMH);
            ViewBag.MaMau = new SelectList(db.MAU, "MaMau", "TenMau", sANPHAM.MaMau);
            ViewBag.MaSize = new SelectList(db.SIZE, "MaSize", "MaSize", sANPHAM.MaSize);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAM/Delete/5
        public ActionResult Delete(int? id=1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            db.SANPHAM.Remove(sANPHAM);
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

        [HttpPost]
        public string GetMHURL(int id = 1)
        {
            var url = db.MATHANG.Find(id).UrlAnh;

            return url;
        }
    }
}
