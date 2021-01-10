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
    public class MenuController : BaseController
    {
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.demrac = db.Menus.Where(m => m.Status == 0).Count();
            return View(db.Menus.Where(m=>m.Status!=0).ToList());
        }

        // GET: Admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelMenu modelMenu = db.Menus.Find(id);
            if (modelMenu == null)
            {
                return HttpNotFound();
            }
            return View(modelMenu);
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {
            ViewBag.List = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            ViewBag.Orders = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( modelMenu modelMenu)
        {
            if (ModelState.IsValid)
            {
                if (modelMenu.ParentId == null)
                {
                    modelMenu.ParentId = 0;
                }

                String slug = XString.ToAscii(modelMenu.Name);
                modelMenu.Link = slug;
                modelMenu.Type = "custom";
                modelMenu.TableId = 1;
                modelMenu.Position = "MainMenu";
                modelMenu.Created_at = DateTime.Now;
                modelMenu.Updated_at = DateTime.Now;
                modelMenu.Updated_by = int.Parse(Session["User_Id"].ToString());
                modelMenu.Created_by = int.Parse(Session["User_Id"].ToString());
                db.Menus.Add(modelMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.List = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            ViewBag.Orders = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            return View(modelMenu);
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.List = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            ViewBag.Orders = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelMenu modelMenu = db.Menus.Find(id);
            if (modelMenu == null)
            {
                return HttpNotFound();
            }
            return View(modelMenu);
        }

        // POST: Admin/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( modelMenu modelMenu)
        {
            if (ModelState.IsValid)
            {
                if (modelMenu.ParentId == null)
                {
                    modelMenu.ParentId = 0;
                }
                String slug = XString.ToAscii(modelMenu.Name);
                modelMenu.Link = slug;
                modelMenu.Type = "custom";
                modelMenu.TableId = 1;
                modelMenu.Updated_at = DateTime.Now;

                modelMenu.Updated_by = int.Parse(Session["User_Id"].ToString());
                modelMenu.Created_by = int.Parse(Session["User_Id"].ToString());

                db.Menus.Add(modelMenu);
                // db.SaveChanges();

                db.Entry(modelMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.List = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            ViewBag.Orders = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            return View(modelMenu);
        }
        public ActionResult Undo(int? id)
        {
            modelMenu modelMenu = db.Menus.Find(id);
            if (modelMenu == null)
            {

                return RedirectToAction("Trash");
            }
            modelMenu.Status = 2;

            modelMenu.Updated_at = DateTime.Now;
            modelMenu.Created_by = 1;
            modelMenu.Updated_at = DateTime.Now;
            modelMenu.Updated_by = 1;
            modelMenu.Updated_by = int.Parse(Session["User_Id"].ToString());
            
            db.Entry(modelMenu).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Khôi phục thành công !" + " ID = " + id, "success");
            return RedirectToAction("Trash");

        }
        public ActionResult Status(int? id)
        {
            modelMenu modelMenu = db.Menus.Find(id);
            if (modelMenu == null)
            {

                return RedirectToAction("Index");
            }
            modelMenu.Status = (modelMenu.Status == 1) ? 2 : 1;

            modelMenu.Updated_at = DateTime.Now;
            modelMenu.Created_by = 1;
            modelMenu.Updated_at = DateTime.Now;
            modelMenu.Updated_by = 1;
            modelMenu.Updated_by = int.Parse(Session["User_Id"].ToString());
            
            db.Entry(modelMenu).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!" + " ID = " + id, "success");
            return RedirectToAction("Index");

        }
        public ActionResult Trash()
        {
            var list = db.Menus.Where(m => m.Status == 0).ToList();

            return View(list);
        }
        public ActionResult delTrash(int? id)
        {
            modelMenu modelMenu = db.Menus.Find(id);
            if (modelMenu == null)
            {
                return RedirectToAction("Index");
            }

            modelMenu.Status = 0;

            modelMenu.Updated_at = DateTime.Now;
            modelMenu.Created_by = 1;
            modelMenu.Updated_at = DateTime.Now;
            modelMenu.Updated_by = 1;
            modelMenu.Updated_by = int.Parse(Session["User_Id"].ToString());
            
            db.Entry(modelMenu).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelMenu modelMenu = db.Menus.Find(id);
            if (modelMenu == null)
            {
                return HttpNotFound();
            }
            return View(modelMenu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modelMenu modelMenu = db.Menus.Find(id);
            db.Menus.Remove(modelMenu);
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
