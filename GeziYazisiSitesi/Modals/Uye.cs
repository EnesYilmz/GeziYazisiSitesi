using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Modals
{
    public class Uye
    {
        public int UyeID { get; set; }

        public string Ad { get; set; }

        public virtual ICollection<Yazi> Yazilar { get; set; }
    }
}
