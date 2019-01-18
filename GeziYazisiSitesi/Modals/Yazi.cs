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
        [Required(ErrorMessage = "Resim giriniz...")]
        public string Resim { get; set; }

        public DateTime Tarih { get; set; }

        public bool Onay { get; set; }

        public int BegenmeSayisi { get; set; }

        public int YorumSayisi { get; set; }

        public int Goruntulenme { get; set; }

        public int UyeId { get; set; }

        public Uye Uye { get; set; }
        [Required(ErrorMessage = "Şehir Seçiniz...")]
        public int SehirId { get; set; }

        public Sehir Sehir { get; set; }
    }
}
