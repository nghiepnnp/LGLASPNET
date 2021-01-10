using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanMayAnh
{
    public class ProductsCategory
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public int ProductStatus { get; set; }
        public string CategoryName { get; set; }
        public string ProductSlug { get; set; }
        public int ProductPrice { get; set; }
        public int ProductPrice_Sale { get; set; }
        public string ProductDetail { get; set; }
    }
}