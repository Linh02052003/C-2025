using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using banSach.Models;
using Newtonsoft.Json;

namespace banSach.Areas.Admin.Controllers
{
    public class DonDatHangsController : Controller
    {
        private QLBanSachEntities db = new QLBanSachEntities();

        // GET: Admin/DonDatHangs
        public ActionResult Index()
        {
            var donDatHangs = db.DonDatHangs.Include(d => d.ChiTietDonHangs).ToList();
            return View(donDatHangs);
        }


        // GET: Admin/DonDatHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Create
        public ActionResult Create()
        {
            // Fetch books with Status = 1
            var sachList = db.Saches
                .Where(s => s.Status == 1)
                .Select(s => new { s.MaSach, s.TenSach, s.GiaBan })
                .ToList();

            if (!sachList.Any())
            {
                System.Diagnostics.Debug.WriteLine("No books found with Status = 1");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Found {sachList.Count} books with Status = 1");
                foreach (var sach in sachList)
                {
                    System.Diagnostics.Debug.WriteLine($"Book: {sach.TenSach}, Price: {sach.GiaBan}");
                }
            }

            ViewBag.SachListJson = JsonConvert.SerializeObject(sachList, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return View();
        }

        // POST: Admin/DonDatHangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DonDatHang model)
        {
            if (model.ChiTietDonHangs == null || !model.ChiTietDonHangs.Any())
            {
                ModelState.AddModelError("", "Vui lòng thêm ít nhất một cuốn sách vào đơn hàng.");
            }

            if (ModelState.IsValid)
            {
                var donHang = new DonDatHang
                {
                    MaDonHang = "DH" + DateTime.Now.Ticks,
                    HoTen = model.HoTen,
                    SoDienThoai = model.SoDienThoai,
                    Email = model.Email,
                    DiaChi = model.DiaChi,
                    NgayDat = DateTime.Now,
                    TrangThai = string.IsNullOrEmpty(model.TrangThai) ? "Đã thanh toán" : model.TrangThai
                };

                db.DonDatHangs.Add(donHang);

                if (model.ChiTietDonHangs != null)
                {
                    foreach (var item in model.ChiTietDonHangs)
                    {
                        var chiTiet = new ChiTietDonHang
                        {
                            MaDonHang = donHang.MaDonHang,
                            MaSach = item.MaSach,
                            SoLuong = item.SoLuong,
                            DonGia = item.DonGia
                        };
                        db.ChiTietDonHangs.Add(chiTiet);
                    }
                }

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database Error: {ex.Message}");
                    ModelState.AddModelError("", "Lỗi khi lưu đơn hàng. Vui lòng thử lại.");
                }
            }

            ViewBag.SachListJson = JsonConvert.SerializeObject(
                db.Saches.Where(s => s.Status == 1).Select(s => new { s.MaSach, s.TenSach, s.GiaBan }).ToList(),
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return View(model);
        }
        // GET: Admin/DonDatHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // POST: Admin/DonDatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,HoTen,SoDienThoai,Email,DiaChi,NgayDat,TrangThai")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // POST: Admin/DonDatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            db.DonDatHangs.Remove(donDatHang);
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
