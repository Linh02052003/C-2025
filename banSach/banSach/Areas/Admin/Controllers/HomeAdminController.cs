using banSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banSach.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            if (Session["AdminUser"] == null)
            {
                return RedirectToAction("Index", "Login", new { area = "Admin" });
            }

            var user = Session["AdminUser"] as NhanVien;
            ViewBag.HoTen = user.HoTen;  // hoặc user.Email nếu muốn hiện email


            return View();
        }
    }
}