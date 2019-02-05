using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using GeziYazisiSitesi.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core2Identity.Controllers
{
    public class YaziController : Controller
    {
        private ApplicationIdentityDbContext context;

        public YaziController(ApplicationIdentityDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            return View(context.Yazi);
        }

        [HttpGet]
        public IActionResult Yaz()
        {
            ViewBag.Sehirler = new SelectList(context.Sehir, "SehirId", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Yaz(Yazi entity, IFormFile file)
        {
            ViewBag.Sehirler = new SelectList(context.Sehir, "SehirId", "Ad");
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                entity.Resim = file.FileName;
                entity.Tarih = DateTime.Now;
                entity.Onay = false;
                entity.BegenmeSayisi = 0;
                entity.Goruntulenme = 0;
                entity.YorumSayisi = 0;
                entity.ApplicationUserId = context.Users.FirstOrDefault().Id; //Kayıtlı bir üyenin idsi kullanılıyor.
                context.Yazi.Add(entity);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Yazilar()
        {
            return View(Tuple.Create(context.Yazi.OrderByDescending(i => i.Tarih), context.Sehir.ToList()));
        }

        public IActionResult Oku(int id)
        {
            var yazi = context.Yazi.SingleOrDefault(i => i.YaziId == id);
            ViewBag.Sehir = context.Sehir.FirstOrDefault(i => i.SehirId == yazi.SehirId).Ad;
            return View(yazi);
        }

        [HttpGet]
        public IActionResult Duzenle(int id)
        {
            return View(context.Yazi.FirstOrDefault(i => i.YaziId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(Yazi entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                var yazi = context.Yazi.FirstOrDefault(i => i.YaziId == entity.YaziId);

                if (yazi != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    yazi.Baslik = entity.Baslik;
                    yazi.Icerik = entity.Icerik;
                    yazi.Resim = file.FileName;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(entity);
        }

        [HttpGet]
        public IActionResult Sil(int id)
        {
            return View(context.Yazi.FirstOrDefault(i => i.YaziId == id));
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var yazi = context.Yazi.FirstOrDefault(i => i.YaziId == id);
            if (yazi != null)
            {
                context.Yazi.Remove(yazi);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}