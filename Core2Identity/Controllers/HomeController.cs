using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core2Identity.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationIdentityDbContext context;

        public HomeController(ApplicationIdentityDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View(Tuple.Create(context.Yazi.Where(i => i.Onay == true).OrderByDescending(i => i.Tarih).Take(6), context.Sehir.ToList()));
        }

        public IActionResult Iletisim()
        {
            return View();
        }
    }
}