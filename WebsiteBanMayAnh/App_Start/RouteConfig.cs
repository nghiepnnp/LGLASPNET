using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteBanMayAnh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            // Checkout
            routes.MapRoute(
        name: "thanh-toan",
        url: "thanh-toan",
        defaults: new { controller = "Checkout", action = "Index", id = UrlParameter.Optional }
        );

            routes.MapRoute(
                name: "SPKMai",
                url: "san-pham-khuyen-mai",
                defaults: new { controller = "Site", action = "ProductSale", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "xoa gio hang",
                url: "xoa-gio-hang",
                defaults: new { controller = "Cart", action = "deleteitem", id = UrlParameter.Optional }
                );
            //chưa làm ra
            //routes.MapRoute(
            //    name: "capnhat",
            //    url: "cap-nhat",
            //    defaults: new { controller = "Cart", action = "updateitem", id = UrlParameter.Optional }
            //    );
            routes.MapRoute(
                name: "them vao gio hang",
                url: "them-sp-giohang",
                defaults: new { controller = "Cart", action = "Additem", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "gio hang",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "HomePost",
               url: "tin-tuc",
               defaults: new { controller = "Site", action = "HomePost", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "TatCaTinTuc",
               url: "tat-ca-tin-tuc",
               defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "LienHe",
               url: "lien-he",
              defaults: new { Controller = "Contacts", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "notlogin",
               url: "notlogin",
              defaults: new { Controller = "AuthUser", action = "NotLogin", id = UrlParameter.Optional }
            );
            //làm chưa xong
            //routes.MapRoute(
            //   name: "Register",
            //   url: "register",
            //  defaults: new { Controller = "AuthUser", action = "Register", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
               name: "Logout",
               url: "logout",
              defaults: new { Controller = "AuthUser", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Login",
               url: "login",
              defaults: new { Controller = "AuthUser", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Search Product",
               url: "search",
               defaults: new { controller = "Site", action = "Search", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TatCaSP",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "SiteSlug",
                url: "{slug}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
