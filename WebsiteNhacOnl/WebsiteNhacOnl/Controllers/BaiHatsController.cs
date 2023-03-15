using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteNhacOnl.Models;

using PagedList;
using PagedList.Mvc;

namespace WebsiteNhacOnl.Controllers
{
    public class BaiHatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BaiHats
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
         
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

             var baiHats = db.BaiHats.AsQueryable();
           

            if (!String.IsNullOrEmpty(searchString))
            {
                baiHats = baiHats.Where(s => s.CaSi.Contains(searchString)
                                       || s.TenBH.Contains(searchString)
                                       || s.TacGia.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    baiHats = baiHats.OrderByDescending(s => s.TenBH);
                    break;
                case "Date":
                    baiHats = baiHats.OrderBy(s => s.NgayCapNhat);
                    break;
                case "date_desc":
                    baiHats = baiHats.OrderByDescending(s => s.NgayCapNhat);
                    break;
                default:
                    baiHats = baiHats.OrderBy(s => s.TenBH);
                    break;
            }

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(baiHats.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult NhacTheoTheLoai(int? id)
        {

            var baiHat = from bh in db.BaiHats where bh.MaTL == id select bh;
            return View(baiHat);
        }


        // GET: BaiHats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiHat baiHat = db.BaiHats.Find(id);
            if (baiHat == null)
            {
                return HttpNotFound();
            }
            return View(baiHat);
        }

        public FileResult Download(string fileName)
        {
            string path = Path.Combine(Server.MapPath("~/Content/music"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
      

    }
}
