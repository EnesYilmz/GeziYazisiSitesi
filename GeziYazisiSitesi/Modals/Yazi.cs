using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Modals
{
    public class Yazi
    {
        public int YaziId { get; set; }

        public string baslik { get; set; }

        public virtual Uye Yazar { get; set; }
    }
}
