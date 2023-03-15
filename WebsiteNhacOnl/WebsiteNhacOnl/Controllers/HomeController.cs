using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using WebsiteNhacOnl.Models;
using System.Net;

namespace WebsiteNhacOnl.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private List<BaiHat> LayBatHatMoi(int count)
        {
            return db.BaiHats.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index()
        {

            //var baiHats = db.BaiHats.Include(b => b.TheLoai);
            var baiHats = LayBatHatMoi(5);
            return View(baiHats.ToList());
        }

        public ActionResult HienThiTheLoai()
        {
            return View(db.TheLoais.ToList());
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