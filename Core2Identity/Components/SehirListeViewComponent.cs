using Core2Identity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Components
{
    public class SehirListeViewComponent:ViewComponent
    {
        private ApplicationIdentityDbContext context;
        public SehirListeViewComponent(ApplicationIdentityDbContext _context)
        {
            context = _context;
        }

        public IViewComponentResult Invoke()
        {
            var ulkeler = context.Ulke.ToList();
            var sehirler = context.Sehir.ToList();
            return View(Tuple.Create(sehirler, ulkeler));
        }
    }
}
