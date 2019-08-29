using Core2Identity.Models;
using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Core2Identity.Models
{
    public class Uye
    {
        public int UyeId { get; set; }

        [StringLength(25, MinimumLength = 2)]
        public string Ad { get; set; }
        [StringLength(25, MinimumLength = 2)]
        public string SoyAd { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "En az 6 karakter içermelidir...", MinimumLength = 6)]
        public string Sifre { get; set; }


        //[DataType(DataType.Password)]
        //[Compare("Sifre", ErrorMessage = "Parolalar eşleşmedi.Tekrar deneyiniz.")]
        //public string TekrarSifre { get; set; }


        public string FUrl { get; set; }
        public string GUrl { get; set; }
        public string İnUrl { get; set; }
        public string TUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DogumTarih { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Tarih { get; set; }
        
        public string ProfilFoto { get; set; }

       
        public string ÖzBilgi { get; set; }

        public int YetkiId { get; set; }

        public int Onay { get; set; }

        public int YaziSayisi { get; set; }


    }
}
