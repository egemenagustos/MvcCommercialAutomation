using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x=>x.Durum==true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniCari(Cariler p)
        {
            p.Durum = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int id)
        {
            var deger = c.Carilers.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var deger = c.Carilers.Find(id);
            return View("CariGetir",deger);
        }

        public ActionResult CariGuncelle(Cariler p)
        {
            var deger = c.Carilers.Find(p.Cariid);
            deger.CariAd = p.CariAd;
            deger.CariSoyad = p.CariSoyad;
            deger.CariSehir = p.CariSehir;
            deger.CariMail = p.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
    }
}