using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using banSach.Models;

namespace bansach.Areas.Admin.Controllers
{
    public class ChucVusController : Controller
    {
        private QLBanSachEntities db = new QLBanSachEntities();

        // GET: Admin/ChucVus
        public async Task<ActionResult> Index()
        {
            return View(await db.ChucVus.ToListAsync());
        }

        // GET: Admin/ChucVus/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucVu chucVu = await db.ChucVus.FindAsync(id);
            if (chucVu == null)
            {
                return HttpNotFound();
            }
            return View(chucVu);
        }

        // GET: Admin/ChucVus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChucVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TenCV")] ChucVu chucVu)
        {
            if (ModelState.IsValid)
            {
                var lastCV = db.ChucVus.OrderByDescending(c => c.MaCV).FirstOrDefault();

                string newMaCV = "CV001";

                if (lastCV != null)
                {
                    string lastMa = lastCV.MaCV.Replace("CV", "");
                    int so = int.Parse(lastMa) + 1;
                    newMaCV = "CV" + so.ToString("D3");
                }

                chucVu.MaCV = newMaCV;

                db.ChucVus.Add(chucVu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(chucVu);
        }


        // GET: Admin/ChucVus/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucVu chucVu = await db.ChucVus.FindAsync(id);
            if (chucVu == null)
            {
                return HttpNotFound();
            }
            return View(chucVu);
        }

        // POST: Admin/ChucVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaCV,TenCV")] ChucVu chucVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chucVu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chucVu);
        }

        // GET: Admin/ChucVus/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucVu chucVu = await db.ChucVus.FindAsync(id);
            if (chucVu == null)
            {
                return HttpNotFound();
            }
            return View(chucVu);
        }

        // POST: Admin/ChucVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ChucVu chucVu = await db.ChucVus.FindAsync(id);
            db.ChucVus.Remove(chucVu);
            await db.SaveChangesAsync();
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
