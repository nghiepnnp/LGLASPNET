
using System.Data.Entity;

namespace WebsiteBanMayAnh.Models
{
    public class WebsiteBanMayAnhDbContext : DbContext
    {
        public WebsiteBanMayAnhDbContext() : base("name=StrConnn")
        { }
        public virtual DbSet<modelProduct> Products { get; set; }
        public virtual DbSet<modelCategory> Categorys { get; set; }
        public virtual DbSet<modelContact> Contacts { get; set; }
        public virtual DbSet<modelMenu> Menus { get; set; }
        public virtual DbSet<modelOrder> Orders { get; set; }
        public virtual DbSet<modelOrderdetail> Orderdetails { get; set; }
        public virtual DbSet<modelPost> Posts { get; set; }
        public virtual DbSet<modelSlider> Sliders { get; set; }
        public virtual DbSet<modelTopic> Topics { get; set; }
        public virtual DbSet<modelUser> Users { get; set; }
        public virtual DbSet<modelLink> Links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            //modelBuilder.Entity<modelCategory>().HasF;

            base.OnModelCreating(modelBuilder);
        }
    }
}