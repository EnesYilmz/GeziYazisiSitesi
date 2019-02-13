using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Modals
{
    public class Ulke
    {
        public int UlkeId { get; set; }

        [Required(ErrorMessage = "Ülke Adını Giriniz...")]
        public string Ad { get; set; }
    }
}
