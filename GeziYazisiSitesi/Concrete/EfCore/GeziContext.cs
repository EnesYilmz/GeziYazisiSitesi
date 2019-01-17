using GeziYazisiSitesi.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Concrete.EfCore
{
    public class GeziContext : DbContext
    {
        public GeziContext(DbContextOptions<GeziContext> options)
            : base(options)
        {

        } 
        public DbSet<Uye> Uyes { get; set; }

        public DbSet<Yazi> Yazis { get; set; }
    }
}
