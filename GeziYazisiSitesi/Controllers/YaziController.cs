using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeziYazisiSitesi.Abstract;
using GeziYazisiSitesi.Modals;
using Microsoft.AspNetCore.Mvc;

namespace GeziYazisiSitesi.Controllers
{
    public class YaziController : Controller
    {
        private IYaziRepository yaziRepository;

        public YaziController(IYaziRepository repository)
        {
            yaziRepository = repository;
        }
        public IActionResult Index()
        {
            return View(yaziRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Yaz()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Yaz(Yazi entity)
        {
            if (ModelState.IsValid)
            {
                entity.Tarih = DateTime.Now;
                entity.Onay = false;
                entity.BegenmeSayisi = 0;
                entity.Goruntulenme = 0;
                entity.YorumSayisi = 0;
                entity.UyeId = 1;
                yaziRepository.AddYazi(entity);
                return RedirectToAction("Yazilar");
            }
            return View(entity);
        }

        public IActionResult Yazilar()
        {
            return View(yaziRepository.GetAll());
        }
    }
}