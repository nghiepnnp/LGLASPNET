namespace WebsiteBanMayAnh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Orders")]
    public class modelOrder
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public int CustemerId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ExportDate { get; set; }

        public string DeliveryAddress { get; set; }

        public string DeliveryName { get; set; }

        public string DeliveryPhone { get; set; }

        public string DeliveryEmail { get; set; }

        public int? Status { get; set; }
            
        public string DeliveryPaymentMethod { get; set; }
        public int StatusPayment { get; set; }


        [ForeignKey("CustemerId")]
        public virtual modelUser modelUser { get; set; }

    }
}