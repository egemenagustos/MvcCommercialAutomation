using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var carimail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == carimail).ToList();
            ViewBag.m = carimail;
            var mailid = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;
            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.satis = toplamsatis;
            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.tutar = toplamtutar;
            var toplamurunsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.urunsayisi = toplamurunsayisi;
            var adsoyad = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var sehirgetir = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.sehir = sehirgetir;
            return View(degerler);
        }

        [Authorize]
        public ActionResult Siparislerim()
        {
            var carimail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == carimail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajId).ToList();
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.g = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x => x.MesajId).ToList();
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.g = gidensayisi;
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d = gelensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajId == id).ToList();
            var mail = (string)Session["CariMail"];
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.g = gidensayisi;
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d = gelensayisi;
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.g = gidensayisi;
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d = gelensayisi;

            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(mesajlar p)
        {
            var mail = (string)Session["CariMail"];
            p.Gonderici = mail;
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(p);
            c.SaveChanges();
            return View();
        }

        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;

            k = k.Where(y => y.TakipKodu.Contains(p));

            //Contains = Belirtilen karşılaştırma kurallarını kullanarak,
            //belirtilen bir karakterin bu dize içinde olup olmadığını gösteren bir değer döndürür.

            return View(k.ToList());
        }

        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public PartialViewResult Partial1()
        {
            var carimail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.Cariid).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("Partial1",caribul);
        }

        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }

        public ActionResult CariBilgiGuncelle(Cariler p)
        {
            var cari = c.Carilers.Find(p.Cariid);
            cari.CariAd = p.CariAd;
            cari.CariSoyad = p.CariSoyad;
            cari.CariSehir = p.CariSehir;
            cari.CariMail = p.CariMail;
            cari.Sifre = p.Sifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}