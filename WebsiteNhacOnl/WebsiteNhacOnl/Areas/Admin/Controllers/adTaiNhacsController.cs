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
    public class adTaiNhacsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/TaiNhacs
        public ActionResult Index()
        {
   

            return View(db.TaiNhacs.ToList());
        }
        public FileResult Download(string fileName)
        {
            string path = Path.Combine(Server.MapPath("~/Content/file"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

      /*  private string GetFileTypeByExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".docx":
                case ".doc":
                    return "Microsoft Word Document";
                case ".xlsx":
                case ".xls":
                    return "Microsoft Excel Document";
                case ".txt":
                    return "Text Document";
                case ".jpg":
                case ".png":
                    return "Image";
                default:
                    return "Unknown";
            }
        }*/
        // GET: Admin/TaiNhacs/Details/5
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

        // GET: Admin/TaiNhacs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TaiNhacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenNhac,File,Size,Type")] TaiNhac taiNhac, HttpPostedFileBase files)
        {
            var path = "";
            var filenhac = "";
            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    filenhac = files.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/file/"), filenhac);
                    files.SaveAs(path);
                    taiNhac.File =filenhac;
                }

                //return RedirectToAction("Index");
                db.TaiNhacs.Add(taiNhac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                
            return View(taiNhac);
        }

        // GET: Admin/TaiNhacs/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Admin/TaiNhacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenNhac,File,Size,Type")] TaiNhac taiNhac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiNhac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taiNhac);
        }

        // GET: Admin/TaiNhacs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/TaiNhacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiNhac taiNhac = db.TaiNhacs.Find(id);
            db.TaiNhacs.Remove(taiNhac);
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
        public ActionResult adreport(string searchString)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Areas/Report"), "CrystalReportTN.rpt"));
            rd.SetDataSource(db.TaiNhacs.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "NhacTai.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}
