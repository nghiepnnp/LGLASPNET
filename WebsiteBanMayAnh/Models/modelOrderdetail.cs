namespace WebsiteBanMayAnh.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderDetails")]
    public class modelOrderdetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float Amount { get; set; }

        [ForeignKey("ProductId")]
        public virtual modelProduct modelProduct { get; set; }

        [ForeignKey("OrderId")]
        public virtual modelOrder modelOrder { get; set; }
    }
}