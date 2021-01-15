namespace WebsiteBanMayAnh.Models
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


    [Table("Users")]
    public class modelUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

        public int Gender { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Img { get; set; }
        public int Access { get; set; }
        public DateTime? Created_at { get; set; }
        public int? Created_by { get; set; }
        public DateTime? Updated_at { get; set; }
        public int? Updated_by { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<modelOrder> modelOrder { get; set; }
    }
}