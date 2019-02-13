using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using GeziYazisiSitesi.Modals;
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
            return View(context.Sehir);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Ulkeler = new SelectList(context.Ulke, "UlkeId", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sehir entity)
        {
            ViewBag.Ulkeler = new SelectList(context.Ulke, "UlkeId", "Ad");
            if (ModelState.IsValid)
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
            if (ModelState.IsValid)
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
            if (sehir != null)
            {
                context.Sehir.Remove(sehir);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}