using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Web;
using System.Web.Mvc;
using WebsiteBanMayAnh.Models;
using WebsiteBanMayAnh.Models.Enums;


namespace WebsiteBanMayAnh.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new WebsiteBanMayAnhDbContext())
            {
                var list = db.Categorys.Where(m => m.Status != 0).ToList();

                ViewBag.demrac = db.Categorys.Where(m => m.Status == 0).Count();

                foreach (var row in list)
                {
                    var temp_link = db.Links
                        .Where(m => m.Type == "category" && m.TableId == row.Id);
                    if (temp_link.Count() > 0)
                    {
                        var row_link = temp_link.First();
                        row_link.Name = row.Name;
                        row_link.Slug = row.Slug;
                        db.Entry(row_link).State = EntityState.Modified;
                    }
                    else
                    {
                        var row_link = new modelLink();
                        row_link.Name = row.Name;
                        row_link.Slug = row.Slug;
                        row_link.Type = "category";
                        row_link.TableId = row.Id;
                        db.Links.Add(row_link);
                    }
                }
                db.SaveChanges();
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(modelCategory category)
        {
            try
            {
                using (var db = new WebsiteBanMayAnhDbContext())
                {
                    if (category.Id == 0)
                    {
                        category.Slug = XString.ToAscii(category.Name);
                        category.Created_at = DateTime.Now;
                        category.Created_by = 1;
                        category.Status = (short)EStatus.Active;
                        db.Categorys.Add(category);
                    }
                    else
                    {
                        var OldCategory = db.Categorys.Find(category.Id);
                        OldCategory.Slug = XString.ToAscii(category.Name);
                        OldCategory.Name = category.Name;
                        OldCategory.ParentId = category.ParentId;
                        OldCategory.Updated_at = DateTime.Now;
                        OldCategory.Updated_by = 1;
                        db.Entry(OldCategory).State = EntityState.Modified;
                    }
                    return Json(db.SaveChanges() > 0 ? 0 : 1);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //modelCategory.Updated_by = int.Parse(Session["User_Id"].ToString());
        [HttpPost]
        public ActionResult UpdateStatus(int id, EStatus eStatus)
        {
            using (var db = new WebsiteBanMayAnhDbContext())
            {
                modelCategory modelCategory = db.Categorys.Find(id);
                modelCategory.Status = (short)eStatus;
                modelCategory.Updated_at = DateTime.Now;
                modelCategory.Updated_by = 1;
                db.Entry(modelCategory).State = EntityState.Modified;
                return Json(db.SaveChanges() > 0 ? 0 : 1);
            }
        }

        public ActionResult GetListTrashCategory()
        {
            using (var db = new WebsiteBanMayAnhDbContext())
            {
                return PartialView("_PartialListTrashCategory", db.Categorys
                    .Where(m => m.Status == (short)EStatus.IsTrash)
                    .OrderByDescending(m => m.Id)
                    .Include(m => m.Category)
                    .ToList());
            }
        }

        public ActionResult GetListCategory()
        {
            using (var db = new WebsiteBanMayAnhDbContext())
            {
                return PartialView("_PartialListCategory", db.Categorys
                .Where(m => m.Status == (short)EStatus.Active || m.Status == (short)EStatus.Hidden)
                .OrderByDescending(m => m.Id)
                .Include(m => m.Category)
                .ToList());
            }
        }
    }
}

