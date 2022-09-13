using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Faturalars.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar p)
        {
            c.Faturalars.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int id)
        {
            var degerler = c.Faturalars.Find(id);
            return View("FaturaGetir",degerler);
        }

        public ActionResult FaturaGuncelle(Faturalar p)
        {
            var degerler = c.Faturalars.Find(p.Faturaid);
            degerler.FaturaSeriNo = p.FaturaSeriNo;
            degerler.FaturaSıraNo = p.FaturaSıraNo;
            degerler.VergiDairesi = p.VergiDairesi;
            degerler.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            degerler.Saat = p.Saat;
            degerler.TeslimEden = p.TeslimEden;
            degerler.TeslimAlan = p.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs);
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan, string Toplam,
        FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach(var item in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = item.Aciklama;
                fk.BirimFiyat = item.BirimFiyat;
                fk.Faturaid = item.FaturaKalemid;
                fk.Miktar = item.Miktar;
                fk.Tutar = item.Tutar;
                c.FaturaKalems.Add(fk);

            }
            c.SaveChanges();
            return Json("İşlem başarılı",JsonRequestBehavior.AllowGet);
        }
    }
}