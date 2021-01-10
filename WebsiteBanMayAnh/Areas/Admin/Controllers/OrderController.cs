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
    public class OrderController : BaseController
    {
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();

        // GET: Admin/Order
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelOrder modelOrder = db.Orders.Find(id);
            if (modelOrder == null)
            {
                return HttpNotFound();
            }
            return View(modelOrder);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,UserId,CreateDate,ExportDate,DeliveryAddress,DeliveryName,DeliveryPhone,DeliveryEmail,Updatedat,Updatedby,Status")] modelOrder modelOrder)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(modelOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelOrder);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelOrder modelOrder = db.Orders.Find(id);
            if (modelOrder == null)
            {
                return HttpNotFound();
            }
            return View(modelOrder);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,UserId,CreateDate,ExportDate,DeliveryAddress,DeliveryName,DeliveryPhone,DeliveryEmail,Updatedat,Updatedby,Status")] modelOrder modelOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelOrder);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelOrder modelOrder = db.Orders.Find(id);
            if (modelOrder == null)
            {
                return HttpNotFound();
            }
            return View(modelOrder);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modelOrder modelOrder = db.Orders.Find(id);
            db.Orders.Remove(modelOrder);
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
