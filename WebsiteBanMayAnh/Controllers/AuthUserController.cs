using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;

namespace WebsiteBanMayAnh.Controllers
{
    public class AuthUserController : Controller
    {

        WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();
        // GET: AuthUser
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection field)
        {
            String username = field["user"];
            //String password = MyString.ToMD5(field["password"]);
            String password =field["password"];

            int count_username = db.Users.Where(m => m.Status == 1 && (m.UserName == username || m.Email == username)).Count();
            if (count_username == 0)
            {
                ViewBag.Error = "<span class ='text-danger'>Tài khoản này không tồn tại!!!</span>";
            }
            else
            {
                var user_acount = db.Users
                .Where(m => m.Status == 1 && (m.UserName == username || m.Email == username) && m.Password == password);

                if (user_acount.Count() == 0)
                {
                    ViewBag.Error = "<span class ='text-danger'>Mật khẩu này không đúng!!!</span>";
                }
                else
                {
                    var user = user_acount.First();

                    Session.Add(Common.CommonConstants.CUSTOMER_SESSION, user);
                    Session["User"] = user.FullName;
                    Session["User_Id"] = user.Id;
                    Session["Role"] = (user.Access != 0) ? user.Access : 0;
                    Response.Redirect("~/");
                }
            }
            return View("Login");
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(modelUser modelUser)
        {
            if (ModelState.IsValid)
            {
                String Slug = XString.ToAscii(modelUser.FullName);
                modelUser.Password = XString.ToMD5(modelUser.Password);
                modelUser.Created_at = DateTime.Now;
                modelUser.Updated_at = DateTime.Now;
                modelUser.Updated_by = null;
                modelUser.Created_by = null;
                modelUser.Access = 0;
                modelUser.Status = 1;


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
        public ActionResult Logout()
        {

            Common.CommonConstants.CUSTOMER_SESSION = "";
            Session["User"] = null;
            Session["User_Id"] = null;
            Session["Role"] = null;
            Response.Redirect("~/");
            return null;
        }
        public ActionResult NotLogin()
        {
           
            return View("NotLogin");
        }
    }
}