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
    public class adPhanHoiNDsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/PhanHoiNDs
        public ActionResult Index()
        {
            return View(db.PhanHoiNDs.ToList());
        }

        // GET: Admin/PhanHoiNDs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoiND phanHoiND = db.PhanHoiNDs.Find(id);
            if (phanHoiND == null)
            {
                return HttpNotFound();
            }
            return View(phanHoiND);
        }

     

        // GET: Admin/PhanHoiNDs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoiND phanHoiND = db.PhanHoiNDs.Find(id);
            if (phanHoiND == null)
            {
                return HttpNotFound();
            }
            return View(phanHoiND);
        }

        // POST: Admin/PhanHoiNDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhanHoiND phanHoiND = db.PhanHoiNDs.Find(id);
            db.PhanHoiNDs.Remove(phanHoiND);
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
            rd.Load(Path.Combine(Server.MapPath("~/Areas/Report"), "CrystalReportND.rpt"));
            rd.SetDataSource(db.PhanHoiNDs.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "PhanHoiNguoiDung.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}
