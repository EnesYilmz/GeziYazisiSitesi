using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeziYazisiSitesi.Abstract;
using GeziYazisiSitesi.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeziYazisiSitesi.Controllers
{
    public class YaziController : Controller
    {
        private IYaziRepository _yaziRepository;
        private IUlkeRepository _ulkeRepository;
        private ISehirRepository _sehirRepository;

        public YaziController(IYaziRepository yaziRepo, IUlkeRepository ulkeRepo, ISehirRepository sehirRepo)
        {
            _yaziRepository = yaziRepo;
            _ulkeRepository = ulkeRepo;
            _sehirRepository = sehirRepo;
        }
        public IActionResult Index()
        {
            return View(_yaziRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Yaz()
        {
            ViewBag.Sehirler = new SelectList(_sehirRepository.GetAll(), "SehirId", "Ad");
            return View();
        }
        [HttpPost]
        public IActionResult Yaz(Yazi entity)
        {
            ViewBag.Sehirler = new SelectList(_sehirRepository.GetAll(), "SehirId", "Ad");
            if (ModelState.IsValid)
            {
                entity.Tarih = DateTime.Now;
                entity.Onay = false;
                entity.BegenmeSayisi = 0;
                entity.Goruntulenme = 0;
                entity.YorumSayisi = 0;
                entity.UyeId = 1;
                _yaziRepository.AddYazi(entity);
                return RedirectToAction("Yazilar");
            }
            return View(entity);
        }

        public IActionResult Yazilar()
        {
            return View(_yaziRepository.GetAll());
        }
    }
}