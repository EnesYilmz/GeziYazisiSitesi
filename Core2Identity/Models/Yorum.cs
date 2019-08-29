using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public class Yorum
    {
        public int YorumId { get; set; }

        [Required(ErrorMessage = "İçerik giriniz...")]
        public string Icerik { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Tarih { get; set; }
        
        [ForeignKey("UyeId")]
        public Uye Uye { get; set; }

        public int? UyeId { get; set; }
        
        [ForeignKey("YaziId")]
        public Yazi Yazi { get; set; }

        public int? YaziId { get; set; }
    }
}
