using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core2Identity.Controllers
{
    public class UyeController : Controller
    {
        private ApplicationIdentityDbContext context;

        public UyeController(ApplicationIdentityDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View(context.Uye);
        }

        [HttpGet]
        public IActionResult UyeEkle()
        {

            return View();
        }

        [HttpPost]
        public IActionResult UyeEkle(Uye entity)
        {
            if (ModelState.IsValid)
            {
                entity.Tarih = DateTime.Now;
                context.Uye.Add(entity);
                context.SaveChanges();
                   return RedirectToAction("Index");
            }
            return View(entity);
        }
        [AllowAnonymous] /*Busayfayı giriş yapmauan kişi görecek*/
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous] /*Busayfayı giriş yapmauan kişi görecek*/
        [ValidateAntiForgeryToken]
        public ActionResult Login(Uye uye)
        {
            if(uye.Email != null && uye.Sifre!= null ) { 
                var login = context.Uye.Where(m => m.Email == uye.Email).SingleOrDefault();
                if (login.Email == uye.Email && login.Sifre == uye.Sifre)
                {
                    
                    
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Uyarı = "Kullanıcının adı ,mail veya şifresi kontrol ediniz";
                   
                }
         
            }
 return View();
        }
        public ActionResult Logout()
        {
            
            //işlemi sonlandırmaya yarıyor ABANDON..


            return RedirectToAction("Index", "Home");
        }





    }
}