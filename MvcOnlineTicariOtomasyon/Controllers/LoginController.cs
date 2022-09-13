using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Partial1(Cariler p)
        {
            c.Carilers.Add(p);
            c.SaveChanges();
            return PartialView();
        }

        [HttpGet]
        public ActionResult CariLogin1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariLogin1(Cariler p)
        {
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == p.CariMail && x.Sifre == p.Sifre);
            if(degerler != null)
            {
                FormsAuthentication.SetAuthCookie(degerler.CariMail, false);
                Session["CariMail"] = degerler.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var degerler = c.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);
            if(degerler != null)
            {
                FormsAuthentication.SetAuthCookie(degerler.KullaniciAd, false);
                Session["KullaniciAd"]=degerler.KullaniciAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}