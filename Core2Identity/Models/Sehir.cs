using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Modals
{
    public class Sehir
    {
        public int SehirId { get; set; }

        public string Ad { get; set; }

        public int UlkeId { get; set; }

        public Ulke Ulke { get; set; }
    }
}
