using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebsiteBanMayAnh.Models
{
    [Table("Topics")]
    public class modelTopic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slug { get; set; }

        public int? ParentId { get; set; }

        public int? Orders { get; set; }

        public string Metakey { get; set; }

        public string Metadesc { get; set; }

        public DateTime? Created_at { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int? Updated_by { get; set; }

        public int? Status { get; set; }

        public virtual ICollection<modelPost> modelPost { get; set; }
    }
}