using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;



namespace WebsiteBanMayAnh.Areas.Admin.Controllers
{

   
    public class AuthController : System.Web.Mvc.Controller
    {

        //// GET: Admin/Auth
        //public ActionResult Login()
        //{
        //    WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();
        //    int user = db.Users.Where(m => m.Status == 1 && m.Access == 0)
        //        .Count();
        //    if (Session["User"] != null)
        //    {
        //        if(Convert.ToInt32(Session["Role"]) != 0)
        //        {
        //            Response.Redirect("~/Admin");
        //        }
        //        else
        //        {
        //            Response.Redirect("/NotLogin");
        //        }  
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(FormCollection field)
        //{
        //    WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();
        //    //MyString mystr = new MyString();
        //    String username = field["user"];
        //    //String password = mystr.ToMD5(field["password"]);
        //    String password = field["password"];

        //    int count_username = db.Users.Where(m => m.Status == 1 && (m.UserName == username || m.Email == username) && m.Access != 0).Count();
        //    int count_user = db.Users.Where(m => m.Status == 1 && (m.UserName == username || m.Email == username) && m.Access == 0).Count();
        //    if (count_username == 0)
        //    {
        //        ViewBag.Error = "<span class ='text-white' style='color:red'>Tài khoản này không tồn tại!!!</span>";
        //    }
        //    else
        //    {
        //        var user_acount = db.Users
        //        .Where(m => m.Status == 1 && (m.UserName == username || m.Email == username) && m.Access != 0 && m.Password == password);
        //        if (user_acount.Count() == 0)
        //        {
        //            ViewBag.Error = "<span class ='text-white' style='color:red'>Mật khẩu này không đúng!!!</span>";
        //        }
        //        else
        //        {
        //            var user = user_acount.First();
        //            Session["User"] = user.UserName;
        //            Session["User_Id"] = user.Id;
        //            Session["Role"] = user.Access;
        //            Response.Redirect("~/Admin");
        //        }
        //    }
        //    if (count_user == 1)
        //    {

        //        ViewBag.Error = "<span class ='text-white' style='color:red' >Bạn không có quyền truy cập vào quản trị!!!</span>";
        //    }
        //    else
        //    {

        //    }

        //    return View("Login");
        //}
        
        //public ActionResult Logout()
        //{
        //    Session["User"] = null;
        //    Session["User_Id"] = null;
        //    Session["Role"] = null;
        //    Response.Redirect("~/Admin/login");
        //    return null;
        //}

    }
}