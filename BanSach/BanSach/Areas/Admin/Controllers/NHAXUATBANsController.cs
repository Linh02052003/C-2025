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
    public class NHAXUATBANsController : Controller
    {
        private QLBanSachEntities db = new QLBanSachEntities();

        // GET: Admin/NHAXUATBANs
        public ActionResult Index()
        {
            return View(db.NHAXUATBANs.ToList());
        }

        // GET: Admin/NHAXUATBANs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);
            if (nHAXUATBAN == null)
            {
                return HttpNotFound();
            }
            return View(nHAXUATBAN);
        }

        // GET: Admin/NHAXUATBANs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NHAXUATBANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNXB,TenNXB,DiaChi")] NHAXUATBAN nHAXUATBAN)
        {
            if (ModelState.IsValid)
            {
                db.NHAXUATBANs.Add(nHAXUATBAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHAXUATBAN);
        }

        // GET: Admin/NHAXUATBANs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);
            if (nHAXUATBAN == null)
            {
                return HttpNotFound();
            }
            return View(nHAXUATBAN);
        }

        // POST: Admin/NHAXUATBANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNXB,TenNXB,DiaChi")] NHAXUATBAN nHAXUATBAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHAXUATBAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHAXUATBAN);
        }

        // GET: Admin/NHAXUATBANs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);
            if (nHAXUATBAN == null)
            {
                return HttpNotFound();
            }
            return View(nHAXUATBAN);
        }

        // POST: Admin/NHAXUATBANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);
            db.NHAXUATBANs.Remove(nHAXUATBAN);
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
