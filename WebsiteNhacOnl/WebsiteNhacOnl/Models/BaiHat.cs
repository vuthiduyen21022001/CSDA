using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteNhacOnl.Models
{
    public class BaiHat
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên bài Hát")]
        public string TenBH { get; set; }
        [Display(Name = "Ca sĩ")]
        public string CaSi { get; set; }
        [Display(Name = "Tác giả")]
        public string TacGia { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime NgayCapNhat { get; set; }
        [Display(Name = "Ảnh bài hát")]
        public string HinhAnhBH { get; set; }
        [Display(Name = "URL Bài hát")]
        public string UrlBH { get; set; }
        [Display(Name = "Lời bài hát")]
        public string LoiBH { get; set; }



        [ForeignKey("TheLoai")]
        public int MaTL { get; set; }
        public TheLoai TheLoai { get; set; }
    }
}