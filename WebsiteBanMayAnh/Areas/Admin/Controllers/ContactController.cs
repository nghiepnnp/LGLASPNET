using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;

namespace WebsiteBanMayAnh.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            ViewBag.demrac = db.Contacts.Where(m => m.Status == 0).Count();
            return View(db.Contacts.Where(m=>m.Status != 0).ToList());
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelContact modelContact = db.Contacts.Find(id);
            if (modelContact == null)
            {
                return HttpNotFound();
            }
            return View(modelContact);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( modelContact modelContact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(modelContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelContact);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelContact modelContact = db.Contacts.Find(id);
            if (modelContact == null)
            {
                return HttpNotFound();
            }
            return View(modelContact);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( modelContact modelContact)
        {
            if (ModelState.IsValid)
            {
                modelContact.Updated_at = DateTime.Now;
                modelContact.Updated_by = 1;
                modelContact.Updated_at = DateTime.Now;
                modelContact.Updated_by = int.Parse(Session["User_Id"].ToString());
                db.Entry(modelContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelContact);
        }
        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelContact modelContact = db.Contacts.Find(id);
            if (modelContact == null)
            {
                return HttpNotFound();
            }
            return View(modelContact);
        }
        //doi trang thai
        public ActionResult Status(int? id)
        {
            modelContact modelContact = db.Contacts.Find(id);
            if (modelContact == null)
            {

                return RedirectToAction("Index");
            }
            modelContact.Status = (modelContact.Status == 1) ? 2 : 1;

            modelContact.Updated_at = DateTime.Now;

            modelContact.Updated_at = DateTime.Now;
            modelContact.Updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(modelContact).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        //khôi phục
        public ActionResult Undo(int? id)
        {
            modelContact modelContact = db.Contacts.Find(id);
            if (modelContact == null)
            {
                return RedirectToAction("Trash");
            }
            modelContact.Status = 2;

            modelContact.Updated_at = DateTime.Now;
           
            modelContact.Updated_at = DateTime.Now;
            modelContact.Updated_by = 1;
            db.Entry(modelContact).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash");

        }
        //thùng rác
        public ActionResult Trash()
        {
            var list = db.Contacts.Where(m => m.Status == 0).ToList();

            return View(list);
        }
        public ActionResult delTrash(int id)
        {
            modelContact modelContact = db.Contacts.Find(id);
           
            if (modelContact == null)
            {
                return RedirectToAction("Index");
            }
            modelContact.Status = 0;
            modelContact.Updated_at = DateTime.Now;
            modelContact.Updated_by = 1;
            db.Entry(modelContact).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("Index", "Contact");
        }
        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modelContact modelContact = db.Contacts.Find(id);
            db.Contacts.Remove(modelContact);
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
