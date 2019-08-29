using GeziYazisiSitesi.Modals;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public class ApplicationIdentityDbContext : DbContext
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {

        }

        public DbSet<Uye> Uye { get; set; }

        public DbSet<Yazi> Yazi { get; set; }

        public DbSet<Ulke> Ulke { get; set; }

        public DbSet<Sehir> Sehir { get; set; }

        public DbSet<Yorum> Yorum { get; set; }

        public DbSet<Begen> Begen { get; set; }
    }

}
