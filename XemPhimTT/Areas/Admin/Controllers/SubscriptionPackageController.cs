using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class SubscriptionPackageController : Controller
    {
        // GET: SubscriptionPackage
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            List<SubscriptionPackage> ListSubscriptionPackage = db.SubscriptionPackages.ToList();
            db.Dispose();

            return View(ListSubscriptionPackage);
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add(SubscriptionPackage c)
        {
            WebMovieEntities db = new WebMovieEntities();
            db.SubscriptionPackages.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "SubscriptionPackage");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            SubscriptionPackage e = db.SubscriptionPackages.Where(i => i.subscriptionpackageid == Id).FirstOrDefault();
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(SubscriptionPackage c)
        {
            WebMovieEntities db = new WebMovieEntities();
            SubscriptionPackage e = db.SubscriptionPackages.Where(i => i.subscriptionpackageid == c.subscriptionpackageid).FirstOrDefault();

            e.name = c.name;
            e.price = c.price;
            e.expiry = c.expiry;
            e.description = c.description;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "SubscriptionPackage");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            SubscriptionPackage e = db.SubscriptionPackages.Where(i => i.subscriptionpackageid == Id).FirstOrDefault();

            db.Dispose();
            return View(e);
        }

        public ActionResult Remove(SubscriptionPackage c)
        {
            WebMovieEntities db = new WebMovieEntities();
            SubscriptionPackage e = db.SubscriptionPackages.Where(i => i.subscriptionpackageid == c.subscriptionpackageid).FirstOrDefault();

            db.SubscriptionPackages.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "SubscriptionPackage");
        }
    }
}