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
        public string Baslik { get; set; }
        [Required(ErrorMessage = "İçerik giriniz...")]
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

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
