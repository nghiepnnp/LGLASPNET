using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanMayAnh.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null || Convert.ToInt32(System.Web.HttpContext.Current.Session["Role"]) == 0 )
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/login");
            }
        }
    }
}