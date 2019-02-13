using Core2Identity.Models;
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

        [StringLength(20)]
        public string Ad { get; set; }
        [StringLength(20)]
        public string SoyAd { get; set; }

        [StringLength(25)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Sifre { get; set; }   
        
        public DateTime Tarih { get; set; }

        public string ProfilFoto { get; set; }

        public int YetkiId { get; set; }

        public int Onay { get; set; }


    }
}
