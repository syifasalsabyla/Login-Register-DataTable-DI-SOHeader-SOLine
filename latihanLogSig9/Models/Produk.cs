using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace latihanLogSig9.Models
{
    public class Produk
    {
        [Display(Name = "ID Produk")]
        public int ProdukID { get; set; }

        [Display(Name = "Nama Produk")]
        public string NamaProduk { get; set; }

        [Display(Name = "Keterangan Produk")]
        public string Keterangan { get; set; }
    }
}
