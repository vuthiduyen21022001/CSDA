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

namespace WebsiteNhacOnl.Controllers
{
    public class TaiNhacsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaiNhacs
        public ActionResult Index()
        {
            return View(db.TaiNhacs.ToList());
        }

        // GET: TaiNhacs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiNhac taiNhac = db.TaiNhacs.Find(id);
            if (taiNhac == null)
            {
                return HttpNotFound();
            }
            return View(taiNhac);
        }

        public FileResult Download(string fileName)
        {
            string path = Path.Combine(Server.MapPath("~/Content/file"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
