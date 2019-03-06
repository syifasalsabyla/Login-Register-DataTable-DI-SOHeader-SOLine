using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace latihanLogSig9.Models
{
    public class SOHeader
    {
        public SOHeader()
        {
            DateTime Tanggal = DateTime.Now;
        }
        [Display(Name = "ID SOHeader")]
        public int SOHeaderID { get; set; }

        //Lookup Member
        [Display(Name = "ID Member")]
        public int MemberID { get; set; }
        public Member Member { get; set; }

        [Display(Name = "Nama Member")]
        public string NamaMember { get; set; }

        [Display(Name = "Tanggal Pemesanan")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Tanggal { get; set; }
    }
}
