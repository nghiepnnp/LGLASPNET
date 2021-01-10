using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace WebsiteBanMayAnh.Models
{
    [Table("Categorys")]
    public class modelCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        //[ForeignKey("Id")]
        //public int? ParentId { get; set; }

        public int? Orders { get; set; }

        public string Metakey { get; set; }

        public string Metadesc { get; set; }

        public DateTime? Created_at { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int? Updated_by { get; set; }

        public int? Status { get; set; }

        // Constructor phải đặt trùng tên với collection
        //[InverseProperty("Category")]


        //public virtual ICollection<modelCategory> Category { get; set; }
        public int? ParentId { set; get; }
        [ForeignKey("ParentId")]
        public virtual modelCategory Category { get; set; }
        public virtual ICollection<modelProduct> modelProduct { get; set; }

    }
}