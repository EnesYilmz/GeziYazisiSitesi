using GeziYazisiSitesi.Modals;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {

        }

        public DbSet<Yazi> Yazi { get; set; }

        public DbSet<Ulke> Ulke { get; set; }

        public DbSet<Sehir> Sehir { get; set; }

    }

}
