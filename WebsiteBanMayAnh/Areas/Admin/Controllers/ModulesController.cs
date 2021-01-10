using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanMayAnh.Areas.Admin.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Admin/Modules
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Navbar()
        {
            return View("Navbar");
        }
        public ActionResult Header()
        {
            return View("Header");
        }
        public ActionResult Footer()
        {
            return View("Footer");
        }
    }
}