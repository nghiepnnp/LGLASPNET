using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;

namespace WebsiteBanMayAnh.Controllers
{
    public class ModuleController : Controller
    {
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();
        // GET: Module
        public ActionResult Header()
        {
            return View("_Header");
        }
        public ActionResult Footer()
        {
            return View("_Footer");
        }
        public ActionResult Auth()
        {
            return View("_Auth");
        }
        public ActionResult Cart()
        {
            return View("_Cart");
        }
        public ActionResult Categories()
        {
            return View("_Categories", db.Categorys.ToList());
        }
        public ActionResult Menu()
        {
            return View("_Menu", db.Menus.ToList());
        }
        public ActionResult Search()
        {
            return View("_Search");
        }
        public ActionResult SlideShow()
        {
            var list = db.Sliders.Where(m => m.Status != 0).ToList();
            return View("_SlideShow",list);
        }
        public ActionResult Listcategory()
        {
            var list = db.Categorys.Where(m => m.ParentId == 0).ToList();
            return View("_Listcategory", list);
        }
        public ActionResult Left_Footer()
        {
            var list = db.Menus.Where(m => m.Status !=0 ).ToList();
            return View("_Left_Footer", list);
        }
        public ActionResult Page_Footer()
        {
            var list = db.Posts.Where(m => m.Status != 0 && m.Type == "page").ToList();
            return View("_Page_Footer", list);
        }
        public ActionResult ListTopic()
        {
            var list = db.Topics.Where(m => m.Status != 0 ).ToList();
            return View("_ListTopic", list);
        }
        public ActionResult List_Menu()
        {
            var list = db.Topics.Where(m => m.Status != 0).ToList();
            return View("_List_Menu", list);
        }
    }
}