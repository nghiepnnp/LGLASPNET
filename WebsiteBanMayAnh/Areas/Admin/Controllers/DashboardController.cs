using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;

namespace WebsiteBanMayAnh.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();
        // GET: Admin/Dashboard

        public ActionResult Index()
        {
            ViewBag.demcate = db.Categorys.Where(m => m.Status != 0).Count();
            ViewBag.dempro = db.Products.Where(m => m.Status != 0).Count();

            return View();
        }   
    }
}