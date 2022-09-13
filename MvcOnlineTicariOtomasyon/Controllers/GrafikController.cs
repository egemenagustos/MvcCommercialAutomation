using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            var grafikciz = new Chart(500,500);
            grafikciz.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok")
            .AddSeries("Değerler", xValue: new[] { "Beyaz Eşya", "Telefon", "Küçük Ev Aleti", "Bilgisayar", "Tablet", "Büyük Ev Aleti", "Telefon" },
            yValues: new[] { 10, 10, 10, 20, 20, 20, 20 }).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");

        }

        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(x => yvalue.Add(x.Stok));
            var grafik = new Chart(width: 500, height: 500).AddTitle("Stoklar")
            .AddSeries(chartType: "Column", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult()
        {
            return Json(urunlistesi(), JsonRequestBehavior.AllowGet);
        }

        public List<Sinif1> urunlistesi()
        {
            List<Sinif1> snf = new List<Sinif1>();
            snf.Add(new Sinif1()
            {
                urunad = "bilgisayar",
                stok = 120
            });
            snf.Add(new Sinif1()
            {
                urunad = "beyaz eşya",
                stok = 150
            }) ;
            snf.Add(new Sinif1()
            {
                urunad = "mobilya",
                stok = 70
            });
            snf.Add(new Sinif1()
            {
                urunad = "küçük ev aletleri",
                stok = 180
            });
            snf.Add(new Sinif1()
            {
                urunad = "mobil cihazlar",
                stok = 90
            });
            return snf;
        }

        public ActionResult Index5()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        {
            return Json(urunlistesi2(), JsonRequestBehavior.AllowGet);
        }

        public List<sinif2> urunlistesi2()
        {
            List<sinif2> snf = new List<sinif2>();
            using (var c = new Context())
            {
                snf = c.Uruns.Select(x => new sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }
            return snf;
        }

        public ActionResult Index6()
        {
            return View();
        }

        public ActionResult Index7()
        {
            return View();
        }
    }
}