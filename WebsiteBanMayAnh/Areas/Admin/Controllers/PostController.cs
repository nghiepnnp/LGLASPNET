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
    public class PostController : BaseController
    {
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();

        // GET: Admin/Post
        public ActionResult Index()
        {
            ViewBag.demrac = db.Posts.Where(m => m.Status == 0 && m.Type == "post").Count();
            var list = from p in db.Posts
                       join t in db.Topics
                       on p.Topid equals t.Id
                       where p.Status != 0
                       orderby p.Created_At descending
                       select new PostTopic()
                       {
                           PostId = p.Id,
                           PostImg = p.Img,
                           PostName = p.Title,
                           PostStatus = p.Status,
                           TopicName = t.Name
                       };
            return View(list.ToList());
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelPost modelPost = db.Posts.Find(id);
            if (modelPost == null)
            {
                return HttpNotFound();
            }
            return View(modelPost);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            modelTopic modelTopic = new modelTopic();
            ViewBag.ListTopic = new SelectList(db.Topics.ToList(), "ID", "Name", 0);
            return View();
        }
        // POST: Admin/Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(modelPost modelPost)
        {
            if (ModelState.IsValid)
            {
                String strSlug = XString.ToAscii(modelPost.Title);
                modelPost.Slug = strSlug;
                modelPost.Type = "post";
                modelPost.Created_At = DateTime.Now;
                modelPost.Created_By = int.Parse(Session["User_Id"].ToString());
                modelPost.Updated_At = DateTime.Now;
                modelPost.Updated_By = int.Parse(Session["User_Id"].ToString());
                var file = Request.Files["Img"];
                if (file != null && file.ContentLength > 0)
                {
                    String filename = strSlug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    modelPost.Img = filename;
                    String Strpath = Path.Combine(Server.MapPath("~/Public/user/img/post"), filename);
                    file.SaveAs(Strpath);
                }
                db.Posts.Add(modelPost);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            return View(modelPost);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            modelTopic modelTopic = new modelTopic();
            ViewBag.ListTopic = new SelectList(db.Topics.ToList(), "ID", "Name", 0);
            modelPost modelPost = db.Posts.Find(id);
            if (modelPost == null)
            {
                return RedirectToAction("Index", "Post");
            }
            return View(modelPost);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(modelPost modelPost)
        {
            modelTopic modelTopic = new modelTopic();
            ViewBag.ListTopic = new SelectList(db.Topics.ToList(), "ID", "Name", 0);
            if (ModelState.IsValid)
            {
                String strSlug = XString.ToAscii(modelPost.Title);
                modelPost.Slug = strSlug;
                modelPost.Type = "post";
                modelPost.Updated_At = DateTime.Now;
                modelPost.Updated_By = int.Parse(Session["User_Id"].ToString());
                var file = Request.Files["Img"];
                if (file != null && file.ContentLength > 0)
                {
                    String filename = strSlug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    modelPost.Img = filename;
                    String Strpath = Path.Combine(Server.MapPath("~/Public/user/img/post/"), filename);
                    file.SaveAs(Strpath);
                }

                db.Entry(modelPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modelPost);
        }
        public ActionResult delTrash(int? id)
        {
            modelPost modelPost = db.Posts.Find(id);
            if (modelPost == null)
            {
                return RedirectToAction("Index");
            }

            modelPost.Status = 0;

            modelPost.Updated_At = DateTime.Now;
            modelPost.Created_By = 1;
            modelPost.Updated_At = DateTime.Now;
            modelPost.Updated_By = 1;
            modelPost.Updated_By = int.Parse(Session["User_Id"].ToString());

            db.Entry(modelPost).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Undo(int? id)
        {
            modelPost modelPost = db.Posts.Find(id);
            if (modelPost == null)
            {

                return RedirectToAction("Trash");
            }
            modelPost.Status = 2;

            modelPost.Updated_At = DateTime.Now;
            modelPost.Created_By = 1;
            modelPost.Updated_At = DateTime.Now;
            modelPost.Updated_By = 1;
            modelPost.Updated_By = int.Parse(Session["User_Id"].ToString());

            db.Entry(modelPost).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Khôi phục thành công !" + " ID = " + id, "success");
            return RedirectToAction("Trash");

        }
        //trash
        public ActionResult Trash()
        {
            var list = from p in db.Posts
                       join t in db.Topics
                       on p.Topid equals t.Id
                       where p.Status == 0
                       orderby p.Created_At descending
                       select new PostTopic()
                       {
                           PostId = p.Id,
                           PostImg = p.Img,
                           PostName = p.Title,
                           PostStatus = p.Status,
                           TopicName = t.Name
                       };
            return View(list.ToList());
        }
        //doi trang thai
        public ActionResult Status(int? id)
        {
            modelPost modelPost = db.Posts.Find(id);
            if (modelPost == null)
            {

                return RedirectToAction("Index");
            }
            modelPost.Status = (modelPost.Status == 1) ? 2 : 1;

            modelPost.Updated_At = DateTime.Now;
            modelPost.Created_By = 1;
            modelPost.Updated_At = DateTime.Now;
            modelPost.Updated_By = 1;
            modelPost.Updated_By = int.Parse(Session["User_Id"].ToString());

            db.Entry(modelPost).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!" + " ID = " + id, "success");
            return RedirectToAction("Index");

        }
        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelPost modelPost = db.Posts.Find(id);
            if (modelPost == null)
            {
                return HttpNotFound();
            }
            return View(modelPost);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modelPost modelPost = db.Posts.Find(id);
            db.Posts.Remove(modelPost);
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
