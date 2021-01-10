using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteBanMayAnh
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            Session["cart"] = "";
            Session["User_Id"] = " 1 ";
            //Admin
            Session["User_Admin"] = "";
            //Session["Role"] = null;
            //User
            Session["User"] = null;
            Session["User_Id"] = null;
            Session["Role"] = null;
            Session["Thongbao"] = "";
        }
    }
}
