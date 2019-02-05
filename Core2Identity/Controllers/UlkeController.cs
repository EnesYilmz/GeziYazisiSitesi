using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using GeziYazisiSitesi.Modals;
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
            return View(context.Ulke);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ulke entity)
        {
            if (ModelState.IsValid)
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
            if (ModelState.IsValid)
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
            if (ulke != null)
            {
                context.Ulke.Remove(ulke);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}