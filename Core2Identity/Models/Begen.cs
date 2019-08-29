using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public class Begen
    {
        public int BegenId { get; set; }
        
        [ForeignKey("UyeId")]
        public Uye Uye { get; set; }

        public int? UyeId { get; set; }

        [ForeignKey("YaziId")]
        public Yazi Yazi { get; set; }

        public int? YaziId { get; set; }
    }
}
