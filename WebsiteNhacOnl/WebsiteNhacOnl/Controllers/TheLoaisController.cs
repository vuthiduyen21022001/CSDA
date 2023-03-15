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
    public class TheLoaisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TheLoais
        public ActionResult Index( )
        {
 
            return View(db.TheLoais.ToList());
        }

        // GET: TheLoais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        
    }
}
