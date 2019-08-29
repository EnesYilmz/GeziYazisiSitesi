using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core2Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CryptoHelper;
using System.Net.Mail;
using System.Net;

namespace Core2Identity.Controllers
{
    public class UyeController : Controller
    {
        private ApplicationIdentityDbContext context;

        public UyeController(ApplicationIdentityDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index(Uye entity)
        {
            if (HttpContext.Session.GetInt32("Yetki") == 1)
            {
                return View(context.Uye.OrderByDescending(i => i.Tarih));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpGet]
        public IActionResult UyeEkle()
        {
            return View();
        }


        [HttpPost]
        public IActionResult UyeEkle(Uye entity)
        {
            var uyes = context.Uye.SingleOrDefault(u => u.Email == entity.Email);
            if (uyes == null)
            {
                if (ModelState.IsValid)
                {
                    string key = Crypto.HashPassword(entity.Email);
                    if (SendMail(entity.Email, "Bizimkiler Geziyor E-Posta Doğrulama", "<a href=\"http://localhost:63079/Uye/MailDogrula/?key=" + key + "\">E-posta adresinizi doğrulamak için tıklayınız</a>"))
                    {
                        entity.ProfilFoto = "profil.png";
                        entity.Sifre = Crypto.HashPassword(entity.Sifre);
                        entity.Tarih = DateTime.Now;
                        entity.Onay = 0;
                        entity.YaziSayisi = 0;
                        context.Uye.Add(entity);
                        context.SaveChanges();
                        return RedirectToAction("MailOnay");
                    }
                    else
                    {
                        ViewBag.mail = "Lütfen sonra tekrar deneyin.";
                        return View(entity);
                    }
                }
            }
            else
            {
                ViewBag.mail = "Aynı mail adresi ile kayıt olmazsınız...";
            }

            return View(entity);
        }

        public IActionResult MailDogrula(string key)
        {
            var uyeListesi = context.Uye.Where(i => i.Onay == 0);
            int uyeId = 0;
            foreach (var uye in uyeListesi)
            {
                if (Crypto.VerifyHashedPassword(key, uye.Email))
                {
                    uyeId = uye.UyeId;
                }
            }
            var uyeOnay = context.Uye.SingleOrDefault(i => i.UyeId == uyeId);
            uyeOnay.Onay = 1;
            context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult MailOnay()
        {
            return View();
        }

        public bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                string senderEmail = "bizimkilergeziyor@gmail.com";
                string senderPassword = "vnkv3rck";
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, content);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Uye uye)
        {
            if (uye.Email != null && uye.Sifre != null)
            {

                var login = context.Uye.SingleOrDefault(m => m.Email == uye.Email);
                if (login != null)
                {
                    if (login.Email == uye.Email && Crypto.VerifyHashedPassword(login.Sifre, uye.Sifre))
                    {
                        HttpContext.Session.SetInt32("UyeId", login.UyeId);
                        HttpContext.Session.SetInt32("Yetki", login.YetkiId);


                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    ViewBag.Uyarı = "Email veya şifrenizi kontrol ediniz";

                }

            }
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UyeId");
            HttpContext.Session.Remove("Yetki");
            return RedirectToAction("Index", "Home");
        }



        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("Yetki") == 1 || HttpContext.Session.GetInt32("Yetki") == 0)
            {
                var uye = context.Uye.Where(u => u.UyeId == id).SingleOrDefault();

                return View(uye);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Uye entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var uyes = context.Uye.Where(u => u.UyeId == entity.UyeId).SingleOrDefault();
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    uyes.ProfilFoto = file.FileName;


                }


                uyes.ÖzBilgi = entity.ÖzBilgi;
                uyes.FUrl = entity.FUrl;
                uyes.GUrl = entity.GUrl;
                uyes.İnUrl = entity.İnUrl;
                uyes.TUrl = entity.TUrl;
                //uyes.Sifre = entity.Sifre;


                //context.Uye.Add(entity);
                //context.SaveChanges();
                //uyes.Ad = entity.Ad;
                //uyes.SoyAd= entity.SoyAd;

                //uyes.Email = entity.Email;
                context.SaveChanges();
                //Session["kullaniciadi"] = uye.KullaniciAdi;
                return RedirectToAction("Index", "Uye", new { id = uyes.UyeId }); //Fotografı aldıkdan sonra "id" ile alıp kaydedicek

            }
            return View();
        }

        [HttpGet]
        public IActionResult Sil(int id, Uye entity)
        {

            if (HttpContext.Session.GetInt32("Yetki") == 1 || HttpContext.Session.GetInt32("Yetki") == 0)
            {
                return View(context.Uye.FirstOrDefault(i => i.UyeId == id));


            }
            else
            {
                return RedirectToAction("Index", "Home");
            }




        }
        [HttpPost, ActionName("Sil")]
        public IActionResult DeleteConfirmed(int id, Uye entity)
        {
            var uye = context.Uye.FirstOrDefault(i => i.UyeId == id);
            if (uye != null)
            {
                context.Uye.Remove(uye);
                context.SaveChanges();


            }

            return RedirectToAction("Index");
        }


        public IActionResult UyeProfil(int id, Uye entity)
        {
            return View(Tuple.Create(context.Uye.SingleOrDefault(u => u.UyeId == id), context.Yazi.Where(i => i.UyeId == id)));
        }
        [HttpGet]
        public ActionResult SifreDegister(int id)
        {

            if (HttpContext.Session.GetInt32("UyeId") != null)
            {
                var uye = context.Uye.Single(u => u.UyeId == id);
                return View(uye);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        public IActionResult SifreDegister(string EskiSifre, string Sifre)
        {
            int id = (int)HttpContext.Session.GetInt32("UyeId");
            var uyes = context.Uye.SingleOrDefault(u => u.UyeId == id);

            if (Crypto.VerifyHashedPassword(uyes.Sifre, EskiSifre))
            {
                if (Sifre == null)
                {
                    ViewBag.Bos = "Şifreniz boş karakter olamaz";
                    return View();
                }
                else
                {
                    uyes.Sifre = Crypto.HashPassword(Sifre);
                    context.SaveChanges();
                }


            }
            else
            {
                ViewBag.Sifre = "Eski şifrenizi kontrol ediniz..";
                return View();
            }


            return RedirectToAction("Index", "Uye", new { id = uyes.UyeId }); //Fotografı aldıkdan sonra "id" ile alıp kaydedicek



        }

        public IActionResult TumUye(string Ara)
        {
            if (Ara == null)
            {
                return View(context.Uye.OrderByDescending(i => i.Tarih));

            }
            else
            {
                var aranan = context.Uye.Where(m => m.Ad.Contains(Ara)).ToList();
                return View(aranan.OrderByDescending(m => m.Tarih)); //Tarihe göre tersten sıralıyor.(Orderbydessindng)



            }


        }
    }

}
