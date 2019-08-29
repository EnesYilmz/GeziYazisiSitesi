using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using GeziYazisiSitesi.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core2Identity.Controllers
{
    public class UlkeController : Controller
    {
        private ApplicationIdentityDbContext context;

        public UlkeController(ApplicationIdentityDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Yetki") == 1)
            {
                return View(context.Ulke);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
                
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("Yetki") == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ulke entity)
        {
            if (ModelState.IsValid && HttpContext.Session.GetInt32("Yetki") == 1)
            {
                context.Ulke.Add(entity);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Details(int id)
        {
            var ulke = context.Ulke.SingleOrDefault(i => i.UlkeId == id);
            return View(ulke);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(context.Ulke.FirstOrDefault(i => i.UlkeId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ulke entity)
        {
            if (ModelState.IsValid && HttpContext.Session.GetInt32("Yetki") == 1)
            {

                var ulke = context.Ulke.FirstOrDefault(i => i.UlkeId == entity.UlkeId);

                if (ulke != null)
                {
                    ulke.Ad = entity.Ad;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(context.Ulke.FirstOrDefault(i => i.UlkeId == id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ulke = context.Ulke.FirstOrDefault(i => i.UlkeId == id);
            if (ulke != null && HttpContext.Session.GetInt32("Yetki") == 1)
            {
                context.Ulke.Remove(ulke);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}