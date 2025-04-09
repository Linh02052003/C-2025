using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanSach.Models;

namespace BanSach.Areas.Admin.Controllers
{
    public class TACGIAsController : Controller
    {
        private QLBanSachEntities db = new QLBanSachEntities();

        // GET: Admin/TACGIAs
        public ActionResult Index()
        {
            return View(db.TACGIAs.ToList());
        }

        // GET: Admin/TACGIAs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tACGIA = db.TACGIAs.Find(id);
            if (tACGIA == null)
            {
                return HttpNotFound();
            }
            return View(tACGIA);
        }

        // GET: Admin/TACGIAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TACGIAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTG,TenTG,DiaChi,TieuSu,DienThoai")] TACGIA tACGIA)
        {
            if (ModelState.IsValid)
            {
                db.TACGIAs.Add(tACGIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tACGIA);
        }

        // GET: Admin/TACGIAs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tACGIA = db.TACGIAs.Find(id);
            if (tACGIA == null)
            {
                return HttpNotFound();
            }
            return View(tACGIA);
        }

        // POST: Admin/TACGIAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTG,TenTG,DiaChi,TieuSu,DienThoai")] TACGIA tACGIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tACGIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tACGIA);
        }

        // GET: Admin/TACGIAs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tACGIA = db.TACGIAs.Find(id);
            if (tACGIA == null)
            {
                return HttpNotFound();
            }
            return View(tACGIA);
        }

        // POST: Admin/TACGIAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TACGIA tACGIA = db.TACGIAs.Find(id);
            db.TACGIAs.Remove(tACGIA);
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
