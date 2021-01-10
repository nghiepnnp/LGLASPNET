using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;

namespace WebsiteBanMayAnh.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();

        // GET: Admin/User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelUser modelUser = db.Users.Find(id);
            if (modelUser == null)
            {
                return HttpNotFound();
            }
            return View(modelUser);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( modelUser modelUser)
        {
            if (ModelState.IsValid)
            {
                String Slug = XString.ToAscii(modelUser.FullName);
                modelUser.Password = XString.ToMD5(modelUser.Password);
                modelUser.Created_at = DateTime.Now;
                modelUser.Updated_at = DateTime.Now;
                modelUser.Updated_by = 1;
                modelUser.Created_by = 1;

                var f = Request.Files["Img"];
                if (f != null & f.ContentLength > 0)
                {
                    String fileName = Slug + f.FileName.Substring(f.FileName.LastIndexOf("."));
                    modelUser.Img = fileName;
                    String Strpath = Path.Combine(Server.MapPath("~/Public/user/img/user"), fileName);
                    f.SaveAs(Strpath);
                }
                db.Users.Add(modelUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modelUser);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelUser modelUser = db.Users.Find(id);
            if (modelUser == null)
            {
                return HttpNotFound();
            }
            return View(modelUser);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( modelUser modelUser)
        {
            //MyString mystr = new MyString();

            if (ModelState.IsValid)
            {
                modelUser.Updated_at = DateTime.Now;

                modelUser.Updated_by = int.Parse(Session["User_Id"].ToString());
                modelUser.Created_by = int.Parse(Session["User_Id"].ToString());
                db.Users.Add(modelUser);
                db.Entry(modelUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelUser);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelUser modelUser = db.Users.Find(id);
            if (modelUser == null)
            {
                return HttpNotFound();
            }
            return View(modelUser);
        }
        public ActionResult delTrash(int? id)
        {
            modelUser modelUser = db.Users.Find(id);
            if (modelUser == null)
            {
                return RedirectToAction("Index");
            }

            modelUser.Status = 0;

            modelUser.Updated_at = DateTime.Now;
            modelUser.Created_by = 1;
            modelUser.Updated_at = DateTime.Now;
            modelUser.Updated_by = 1;
            modelUser.Updated_by = int.Parse(Session["User_Id"].ToString());

            db.Entry(modelUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Undo(int? id)
        {
            modelUser modelUser = db.Users.Find(id);
            if (modelUser == null)
            {

                return RedirectToAction("Trash");
            }
            modelUser.Status = 2;

            modelUser.Updated_at = DateTime.Now;
            modelUser.Created_by = 1;
            modelUser.Updated_at = DateTime.Now;
            modelUser.Updated_by = 1;
            modelUser.Updated_by = int.Parse(Session["User_Id"].ToString());

            db.Entry(modelUser).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Khôi phục thành công !" + " ID = " + id, "success");
            return RedirectToAction("Trash");

        }
        //trash
        public ActionResult Trash()
        {
            var list = db.Users.Where(m => m.Status == 0).ToList();

            return View(list);
        }
        //doi trang thai
        public ActionResult Status(int? id)
        {
            modelUser modelUser = db.Users.Find(id);
            if (modelUser == null)
            {

                return RedirectToAction("Index");
            }
            modelUser.Status = (modelUser.Status == 1) ? 2 : 1;

            modelUser.Updated_at = DateTime.Now;
            modelUser.Created_by = 1;
            modelUser.Updated_at = DateTime.Now;
            modelUser.Updated_by = 1;
            modelUser.Updated_by = int.Parse(Session["User_Id"].ToString());

            db.Entry(modelUser).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!" + " ID = " + id, "success");
            return RedirectToAction("Index");

        }
        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modelUser modelUser = db.Users.Find(id);
            db.Users.Remove(modelUser);
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
