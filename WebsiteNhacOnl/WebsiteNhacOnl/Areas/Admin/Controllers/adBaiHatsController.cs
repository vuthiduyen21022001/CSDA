using CrystalDecisions.CrystalReports.Engine;
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

namespace WebsiteNhacOnl.Areas.Admin.Controllers
{
    [Authorize]
    public class adBaiHatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/adBaiHats
        //Sắp xếp theo thứ thự
      /*  public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var baiHats = db.BaiHats.AsQueryable();
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

            return View(baiHats.ToList());
        }*/


         public ActionResult Index(string searchString)
         {
            //Tìm kiếm
            var baiHats = db.BaiHats.Include(b => b.TheLoai);
            if (!String.IsNullOrEmpty(searchString))
            {
                baiHats = baiHats.Where(s => s.CaSi.Contains(searchString)
                                       || s.TenBH.Contains(searchString)
                                       ||s.TheLoai.TenTL.Contains(searchString)
                                       || s.TacGia.Contains(searchString));
            }

            return View(baiHats.ToList());
         }


        // GET: Admin/adBaiHats/Details/5
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

        // GET: Admin/adBaiHats/Create
        public ActionResult Create()
        {
            ViewBag.MaTL = new SelectList(db.TheLoais, "Id", "TenTL");
            return View();
        }

        // POST: Admin/adBaiHats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenBH,CaSi,TacGia,NgayCapNhat,HinhAnhBH,UrlBH,LoiBH,MaTL")] BaiHat baiHat, HttpPostedFileBase img, HttpPostedFileBase url)
        {
            var path = "";
            var filename = "";
            var filenhac = "";
            if (ModelState.IsValid)
            {
                //them ảnh
                if (img != null)
                {

                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    img.SaveAs(path);
                    baiHat.HinhAnhBH = "/Content/images/" + filename; //Lưu ý

                }
                else
                {
                    baiHat.HinhAnhBH = "a.jpg";
                }
                //them nhạc
                if (url != null)
                {
                    filenhac = url.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/music/"), filenhac);
                    url.SaveAs(path);
                    baiHat.UrlBH =  filenhac;
                }
                else
                {
                    baiHat.UrlBH = "a.mp3";
                }
                db.BaiHats.Add(baiHat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTL = new SelectList(db.TheLoais, "Id", "TenTL", baiHat.MaTL);
            return View(baiHat);
        }

        // GET: Admin/adBaiHats/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.MaTL = new SelectList(db.TheLoais, "Id", "TenTL", baiHat.MaTL);
            return View(baiHat);
        }

        // POST: Admin/adBaiHats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenBH,CaSi,TacGia,NgayCapNhat,HinhAnhBH,UrlBH,LoiBH,MaTL")] BaiHat baiHat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiHat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTL = new SelectList(db.TheLoais, "Id", "TenTL", baiHat.MaTL);
            return View(baiHat);
        }

        // GET: Admin/adBaiHats/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/adBaiHats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiHat baiHat = db.BaiHats.Find(id);
            db.BaiHats.Remove(baiHat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        //Tải nhạc
        public FileResult Download(string fileName)
        {
            string path = Path.Combine(Server.MapPath("~/Content/music"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //xuất file pdf
        public ActionResult adreport(string searchString)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Areas/Report"), "CrystalReportBH.rpt"));
            rd.SetDataSource(db.BaiHats.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "adBaiHats.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}
