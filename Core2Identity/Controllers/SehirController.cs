using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using GeziYazisiSitesi.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core2Identity.Controllers
{
    public class SehirController : Controller
    {
        private ApplicationIdentityDbContext context;

        public SehirController(ApplicationIdentityDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Yetki") == 1)
            {
                return View(context.Sehir);
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
                ViewBag.Ulkeler = new SelectList(context.Ulke, "UlkeId", "Ad");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sehir entity)
        {
            ViewBag.Ulkeler = new SelectList(context.Ulke, "UlkeId", "Ad");
            if (ModelState.IsValid && HttpContext.Session.GetInt32("Yetki") == 1)
            {
                context.Sehir.Add(entity);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Details(int id)
        {
            var sehir = context.Sehir.SingleOrDefault(i => i.SehirId == id);
            ViewBag.Ulke = context.Ulke.SingleOrDefault(i => i.UlkeId == sehir.UlkeId).Ad;
            return View(sehir);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Ulkeler = new SelectList(context.Ulke, "UlkeId", "Ad");
            return View(context.Sehir.FirstOrDefault(i => i.SehirId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Sehir entity)
        {
            ViewBag.Ulkeler = new SelectList(context.Ulke, "UlkeId", "Ad");
            if (ModelState.IsValid && HttpContext.Session.GetInt32("Yetki") == 1)
            {
                var sehir = context.Sehir.FirstOrDefault(i => i.SehirId == entity.SehirId);
                if (sehir != null)
                {
                    sehir.UlkeId = entity.UlkeId;
                    sehir.Ad = entity.Ad;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var sehir = context.Sehir.FirstOrDefault(i => i.SehirId == id);
            ViewBag.Ulke = context.Ulke.SingleOrDefault(i => i.UlkeId == sehir.UlkeId).Ad;
            return View(sehir);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sehir = context.Sehir.FirstOrDefault(i => i.SehirId == id);
            if (sehir != null && HttpContext.Session.GetInt32("Yetki") == 1)
            {
                context.Sehir.Remove(sehir);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}