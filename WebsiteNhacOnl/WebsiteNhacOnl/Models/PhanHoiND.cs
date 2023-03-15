using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteNhacOnl.Models
{
    public class PhanHoiND
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Họ và tên:")]
        public string HoTen { get; set; }

        [Display(Name = "Năm sinh:")]
        public string NamSinh { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string Sdt { get; set; }

        [Display(Name = "Mail:")]
        public string Mail { get; set; }

        [Display(Name = "Địa chỉ:")]
        public string DiaChi { get; set; }

        [Display(Name = "Nội dung:")]
        public string NdPhanHoi { get; set; }

    }
}