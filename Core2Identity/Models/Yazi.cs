using Core2Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Modals
{
    public class Yazi
    {
        public int YaziId { get; set; }

        [Required(ErrorMessage = "Başlık giriniz...")]
        [StringLength(60,ErrorMessage ="Başlık 60 karakterden az olmalıdır.")]
        public string Baslik { get; set; }
        [Required(ErrorMessage = "İçerik giriniz...")]
        [StringLength(30000, ErrorMessage = "İçerik 30000 karakterden az olmalıdır.")]
        public string Icerik { get; set; }
        public string Resim { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Tarih { get; set; }

        public bool Onay { get; set; }

        public int BegenmeSayisi { get; set; }

        public int YorumSayisi { get; set; }

        public int Goruntulenme { get; set; }

        public int SehirId { get; set; }

        public Sehir Sehir { get; set; }

        public int UyeId { get; set; }

        public Uye Uye { get; set; }
    }
}
