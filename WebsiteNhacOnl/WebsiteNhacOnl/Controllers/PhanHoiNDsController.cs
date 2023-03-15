using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteNhacOnl.Models;

namespace WebsiteNhacOnl.Controllers
{
    public class PhanHoiNDsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PhanHoiNDs
        public ActionResult Index()
        {
            return View(db.PhanHoiNDs.ToList());
        }

        
        // GET: PhanHoiNDs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhanHoiNDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HoTen,NamSinh,Sdt,Mail,DiaChi,NdPhanHoi")] PhanHoiND phanHoiND)
        {
            if (ModelState.IsValid)
            {
                db.PhanHoiNDs.Add(phanHoiND);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(phanHoiND);
        }

      
    }
}
