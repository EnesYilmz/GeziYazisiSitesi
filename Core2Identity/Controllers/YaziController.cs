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
            return View(context.Yazi.OrderByDescending(i => i.Tarih));
        }

        [HttpGet]
        public IActionResult Yaz()
        {
            ViewBag.Ulkeler = context.Ulke.ToList();
            ViewBag.Sehirler = context.Sehir.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Yaz(Yazi entity, IFormFile file)
        {
            ViewBag.Ulkeler = context.Ulke.ToList();
            ViewBag.Sehirler = context.Sehir.ToList();
            if (ModelState.IsValid)
            {
                if (file != null)
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
                    entity.UyeId = context.Uye.FirstOrDefault().UyeId; //Kayıtlı bir üyenin idsi kullanılıyor.
                    context.Yazi.Add(entity);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ResimHata = "Resim Ekleyiniz...";
            }
            return View(entity);
        }

        public IActionResult Yazilar()
        {
            return View(Tuple.Create(context.Yazi.Where(i => i.Onay == true).OrderByDescending(i => i.Tarih), context.Sehir.ToList()));
        }

        public IActionResult Oku(int id)
        {
            var yazi = context.Yazi.SingleOrDefault(i => i.YaziId == id);
            ViewBag.Sehir = context.Sehir.FirstOrDefault(i => i.SehirId == yazi.SehirId).Ad;
            ViewBag.Yazar = context.Uye.FirstOrDefault(i => i.UyeId == yazi.UyeId).Ad;
            return View(Tuple.Create(yazi,context.Yazi.Where(i => i.SehirId == yazi.SehirId && i.YaziId != yazi.YaziId && i.Onay == true).OrderByDescending(i => i.Tarih).Take(3)));
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
                    if (file != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        yazi.Resim = file.FileName;
                    }
                    yazi.Baslik = entity.Baslik;
                    yazi.Icerik = entity.Icerik;                   
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

        public IActionResult Onayla(int id)
        {
            var yazi = context.Yazi.SingleOrDefault(i => i.YaziId == id);
            yazi.Onay = true;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Reddet(int id)
        {
            var yazi = context.Yazi.SingleOrDefault(i => i.YaziId == id);
            yazi.Onay = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}