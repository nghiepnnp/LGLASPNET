using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;


namespace WebsiteBanMayAnh.Controllers
{
    public class CartController : Controller
    {
        //khởi tạo session
        private const string SessionCart = "SessionCart";

        // GET: Cart
        private WebsiteBanMayAnhDbContext db = new WebsiteBanMayAnhDbContext();

        #region Cart
        public ActionResult Index()
        {
            var cart = Session[SessionCart];

            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        // Đếm sl trong giỏ hàng
        public ActionResult Cart()
        {
            var cart = Session[SessionCart];
            var list = new List<CartItem>();
            if (cart != null)
            {

                list = (List<CartItem>)cart;
                int quantytyyy = 0;
                foreach (var item1 in list)
                {
                    quantytyyy += item1.quantity;
                }
                ViewBag.quantity = quantytyyy;
            }
            return View(list);
        }
        // 
        public RedirectToRouteResult updateitem(long P_SanPhamID, int P_quantity)
        {
            var cart = Session[SessionCart];
            var list = (List<CartItem>)cart;
            CartItem itemSua = list.FirstOrDefault(m => m.product.Id == P_SanPhamID);
            if (itemSua != null)
            {
                itemSua.quantity = P_quantity;
            }
            return RedirectToAction("Index");
        }
        // xóa sản phẩm trong giỏ hàng
        public RedirectToRouteResult deleteitem(long productID)
        {
            var cart = Session[SessionCart];
            var list = (List<CartItem>)cart;

            CartItem itemXoa = list.FirstOrDefault(m => m.product.Id == productID);
            if (itemXoa != null)
            {
                list.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
        // thêm vào giỏ hàng từ chi tiết sản phẩm qua FormCollection
        [HttpPost]
        public ActionResult Additem(long productID, int? quantity, FormCollection qty)
        {
            int Sluong = Convert.ToInt32(qty["qty"]);
            var item = new CartItem();

            modelProduct product = db.Products.Find(productID);
            var cart = Session[SessionCart];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(m => m.product.Id == productID))
                {
                    int quantity1 = 0;
                    foreach (var item1 in list)
                    {
                        if (item1.product.Id == productID)
                        {
                            item1.quantity += quantity ?? Sluong;
                            quantity1 = item1.quantity;
                        }
                    }
                    int priceTotol = 0;

                    int price = 0;
                    foreach (var item1 in list)
                    {
                        int temp = (int)item1.product.Price * (int)item1.quantity;
                        priceTotol += temp;

                        price = (int)item1.product.Price;
                    }
                    return RedirectToAction("Index");

                }
                else
                {
                    item.product = product;
                    item.quantity = quantity ?? Sluong;
                    list.Add(item);
                    item.countCart = list.Count();
                    item.meThod = "cartExist";
                    int priceTotol = 0;
                    foreach (var item1 in list)
                    {
                        int temp = (int)item1.product.Price * (int)item1.quantity;
                        priceTotol += temp;
                    }
                    item.priceTotal = priceTotol;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                item.product = product;
                item.quantity = quantity ?? Sluong;
                item.meThod = "cartEmpty";
                item.countCart = 1;
                item.priceTotal = (int)product.Price;
                var list = new List<CartItem>();
                list.Add(item);
                Session[SessionCart] = list;

            }
            return RedirectToAction("Index");
        }
        // thêm sản phẩm từ trang chủ
        public ActionResult Additem(long productID, int? quantity)
        {
            var item = new CartItem();

            modelProduct product = db.Products.Find(productID);
            var cart = Session[SessionCart];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(m => m.product.Id == productID))
                {
                    int quantity1 = 0;
                    foreach (var item1 in list)
                    {
                        if (item1.product.Id == productID)
                        {
                            item1.quantity += quantity ?? 1;
                            quantity1 = item1.quantity;
                        }
                    }
                    int priceTotol = 0;

                    int price = 0;
                    foreach (var item1 in list)
                    {
                        int temp = (int)item1.product.Price * (int)item1.quantity;
                        priceTotol += temp;

                        price = (int)item1.product.Price;
                    }
                    return RedirectToAction("Index");

                }
                else
                {
                    item.product = product;
                    item.quantity = quantity ?? 1;
                    list.Add(item);
                    item.countCart = list.Count();
                    item.meThod = "cartExist";
                    int priceTotol = 0;
                    foreach (var item1 in list)
                    {
                        int temp = (int)item1.product.Price * (int)item1.quantity;
                        priceTotol += temp;
                    }
                    item.priceTotal = priceTotol;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                item.product = product;
                item.quantity = quantity ?? 1;
                item.meThod = "cartEmpty";
                item.countCart = 1;
                item.priceTotal = (int)product.Price;
                var list = new List<CartItem>();
                list.Add(item);
                Session[SessionCart] = list;

            }
            return RedirectToAction("Index");
        }
        #endregion

        

    }
}