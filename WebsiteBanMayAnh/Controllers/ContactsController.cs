using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;

namespace WebsiteBanMayAnh.Controllers
{
    public class ContactsController : Controller
    {
        WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(modelContact contact)
        {
            if(ModelState.IsValid)
            {
                contact.Created_at = DateTime.Now;
                contact.Updated_at = DateTime.Now;
                contact.Updated_by = 1;
                contact.Status = 1;
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index", "Site");
            }
            return View();
        }
    }
}