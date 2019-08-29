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
using X.PagedList;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Core;

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
            if (HttpContext.Session.GetInt32("Yetki") == 1)
            {
                return View(context.Yazi.OrderByDescending(i => i.Tarih));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Yaz()
        {
            if (HttpContext.Session.GetInt32("UyeId") != null)
            {
                ViewBag.Ulkeler = context.Ulke.ToList();
                ViewBag.Sehirler = context.Sehir.ToList();
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
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
                    entity.Baslik = entity.Baslik.ToUpper();
                    entity.Resim = file.FileName;
                    entity.Tarih = DateTime.Now;
                    entity.Onay = false;
                    entity.BegenmeSayisi = 0;
                    entity.Goruntulenme = 0;
                    entity.YorumSayisi = 0;
                    entity.UyeId = (int)HttpContext.Session.GetInt32("UyeId");
                    context.Yazi.Add(entity);
                    context.Uye.Single(i => i.UyeId == entity.UyeId).YaziSayisi++;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ResimHata = "Resim Ekleyiniz...";
            }
            return View(entity);
        }

        public IActionResult Yazilar(int? sayfa, int? sehir, string siralama)
        {
            int pageSize = 6;
            int pageNumber = (sayfa ?? 1);
            var yazilar = context.Yazi.Where(i => i.Onay == true);
            if (sehir != null)
            {
                yazilar = yazilar.Where(i => i.Sehir.SehirId == sehir).OrderByDescending(i => i.Tarih);
                ViewBag.SehirId = sehir;
                ViewBag.SehirAdi = context.Sehir.Single(i => i.SehirId == sehir).Ad + " - ";
            }
            if (!String.IsNullOrEmpty(siralama))
            {
                if (siralama == "goruntulenme")
                {
                    yazilar = yazilar.OrderByDescending(i => i.Goruntulenme);
                    ViewBag.Baslık = "EN ÇOK OKUNAN";
                    ViewBag.Siralama = "goruntulenme";
                }
                else if (siralama == "begenme")
                {
                    yazilar = yazilar.OrderByDescending(i => i.BegenmeSayisi);
                    ViewBag.Baslık = "EN ÇOK BEĞENİLEN";
                    ViewBag.Siralama = "begenme";
                }
                else if (siralama == "yorum")
                {
                    yazilar = yazilar.OrderByDescending(i => i.YorumSayisi);
                    ViewBag.Baslık = "EN ÇOK YORUM ALAN";
                    ViewBag.Siralama = "yorum";
                }
            }
            else
            {
                yazilar = yazilar.OrderByDescending(i => i.Tarih);
                ViewBag.Baslık = "EN SON YAYINLANAN";
            }
            return View(Tuple.Create(yazilar.ToPagedList(pageNumber, pageSize), context.Sehir.ToList()));
        }

        public IActionResult YaziAra(string arama)
        {
            if (!String.IsNullOrEmpty(arama))
            {
                var yazilar = context.Yazi.Where(i => i.Onay == true && i.Baslik.ToUpper().Contains(arama.ToUpper())).OrderByDescending(i => i.Tarih);
                ViewBag.Arama = arama;
                return View(Tuple.Create(yazilar, context.Sehir.ToList()));
            }
            else
            {
                return RedirectToAction("Yazilar");
            }
        }

        public IActionResult YaziOku(int id)
        {
            context.Yazi.Single(i => i.YaziId == id).Goruntulenme++;
            context.SaveChanges();

            return RedirectToAction("Oku", new { id });
        }

        public IActionResult Oku(int id)
        {
            var yazi = context.Yazi.SingleOrDefault(i => i.YaziId == id);
            var yorumlar = context.Yorum.Where(i => i.YaziId == yazi.YaziId).OrderByDescending(i => i.Tarih);
            ViewBag.Sehir = context.Sehir.FirstOrDefault(i => i.SehirId == yazi.SehirId).Ad;
            if (context.Begen.FirstOrDefault(i => i.YaziId == id && i.UyeId == context.Uye.SingleOrDefault(y => y.UyeId == HttpContext.Session.GetInt32("UyeId")).UyeId) != null)
            {
                ViewBag.Begenmismi = true;
            }
            else
            {
                ViewBag.Begenmismi = false;
            }
            return View(Tuple.Create(yazi, context.Yazi.Where(i => i.SehirId == yazi.SehirId && i.YaziId != yazi.YaziId && i.Onay == true).OrderByDescending(i => i.Tarih).Take(3), yorumlar, context.Uye.ToList()));
        }

        [HttpPost]
        public IActionResult Oku(int YazıId, string Yorum) //Yorum Yapma Kısmı
        {
            if (Yorum != null && HttpContext.Session.GetInt32("UyeId") != null)
            {
                Yorum yorum = new Yorum();
                yorum.Icerik = Yorum;
                yorum.Tarih = DateTime.Now;
                yorum.YaziId = YazıId;
                yorum.UyeId = HttpContext.Session.GetInt32("UyeId");
                context.Yorum.Add(yorum);
                context.Yazi.Single(i => i.YaziId == YazıId).YorumSayisi++;
                context.SaveChanges();
            }
            return RedirectToAction("Oku", new { YazıId });
        }

        public IActionResult Begen(int id)
        {
            if (HttpContext.Session.GetInt32("UyeId") != null && context.Begen.FirstOrDefault(i => i.YaziId == id && i.UyeId == context.Uye.SingleOrDefault(y => y.UyeId == HttpContext.Session.GetInt32("UyeId")).UyeId) == null)
            {
                Begen begen = new Begen();
                begen.YaziId = id;
                begen.UyeId = HttpContext.Session.GetInt32("UyeId");
                context.Begen.Add(begen);
                context.Yazi.Single(i => i.YaziId == id).BegenmeSayisi++;
                context.SaveChanges();
            }

            return RedirectToAction("Oku", new { id });
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

                if (HttpContext.Session.GetInt32("Yetki") == 1 || HttpContext.Session.GetInt32("UyeId") == yazi.UyeId)
                {

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
                        yazi.Onay = false;
                        context.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
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
                if (HttpContext.Session.GetInt32("Yetki") == 1 || HttpContext.Session.GetInt32("UyeId") == yazi.UyeId)
                {
                    context.Yazi.Remove(yazi);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Onayla(int id)
        {
            if (HttpContext.Session.GetInt32("Yetki") == 1)
            {
                var yazi = context.Yazi.SingleOrDefault(i => i.YaziId == id);
                yazi.Onay = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Reddet(int id)
        {
            if (HttpContext.Session.GetInt32("Yetki") == 1)
            {
                var yazi = context.Yazi.SingleOrDefault(i => i.YaziId == id);
                yazi.Onay = false;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}