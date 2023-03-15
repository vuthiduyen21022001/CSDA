using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteNhacOnl.Models
{
    public class TheLoai
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên thể loại")]
        public string TenTL { get; set; }

        public ICollection<BaiHat> BaiHats { get; set; }
    }
}