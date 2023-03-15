using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteNhacOnl.Models
{
    public class TaiNhac
    {
        public int Id { get; set; }

        [Display(Name = "Tên bài hát:")]
        public string TenNhac { get; set; }

        public IEnumerable<HttpPostedFileBase> files { get; set; }
        public string File { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
    }
}