using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class MovietypeController : Controller
    {
        // GET: Movietype
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            List<Movietype> ListMovietype = db.Movietypes.ToList();
            return View(ListMovietype);
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add(Movietype c)
        {
            WebMovieEntities db = new WebMovieEntities();
            db.Movietypes.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Movietype");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movietype e = db.Movietypes.Where(i => i.movietypeid == Id).FirstOrDefault();
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(Movietype c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movietype e = db.Movietypes.Where(i => i.movietypeid == c.movietypeid).FirstOrDefault();

            e.movietypename = c.movietypename;

            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Movietype");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movietype e = db.Movietypes.Where(i => i.movietypeid == Id).FirstOrDefault();

            db.Dispose();
            return View(e);
        }

        public ActionResult Remove(Movietype c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movietype e = db.Movietypes.Where(i => i.movietypeid == c.movietypeid).FirstOrDefault();

            db.Movietypes.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Movietype");
        }
    }
}