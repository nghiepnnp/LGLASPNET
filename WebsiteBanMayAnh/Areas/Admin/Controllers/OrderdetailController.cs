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
    public class OrderdetailController : BaseController
    {
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();

        // GET: Admin/Orderdetail
        public ActionResult Index()
        {
            return View(db.Orderdetails.ToList());
        }

        // GET: Admin/Orderdetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelOrderdetail modelOrderdetail = db.Orderdetails.Find(id);
            if (modelOrderdetail == null)
            {
                return HttpNotFound();
            }
            return View(modelOrderdetail);
        }

        // GET: Admin/Orderdetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Orderdetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,Price,Number,Amount")] modelOrderdetail modelOrderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Orderdetails.Add(modelOrderdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelOrderdetail);
        }

        // GET: Admin/Orderdetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelOrderdetail modelOrderdetail = db.Orderdetails.Find(id);
            if (modelOrderdetail == null)
            {
                return HttpNotFound();
            }
            return View(modelOrderdetail);
        }

        // POST: Admin/Orderdetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,Price,Number,Amount")] modelOrderdetail modelOrderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelOrderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelOrderdetail);
        }

        // GET: Admin/Orderdetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelOrderdetail modelOrderdetail = db.Orderdetails.Find(id);
            if (modelOrderdetail == null)
            {
                return HttpNotFound();
            }
            return View(modelOrderdetail);
        }

        // POST: Admin/Orderdetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modelOrderdetail modelOrderdetail = db.Orderdetails.Find(id);
            db.Orderdetails.Remove(modelOrderdetail);
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
