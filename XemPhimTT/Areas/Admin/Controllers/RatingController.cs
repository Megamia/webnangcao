using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class RatingController : Controller
    {
        // GET: Rating
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            List<Rating> ListRating = db.Ratings.ToList();
            db.Dispose();

            return View(ListRating);
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add(Rating c)
        {
            WebMovieEntities db = new WebMovieEntities();
            db.Ratings.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Rating");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Rating e = db.Ratings.Where(i => i.ratingid == Id).FirstOrDefault();
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(Rating c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Rating e = db.Ratings.Where(i => i.ratingid == c.ratingid).FirstOrDefault();

            e.rating1 = c.rating1;
            e.movieid = c.movieid;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Rating");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Rating e = db.Ratings.Where(i => i.ratingid == Id).FirstOrDefault();

            db.Dispose();
            return View(e);
        }

        public ActionResult Remove(Rating c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Rating e = db.Ratings.Where(i => i.ratingid == c.ratingid).FirstOrDefault();

            db.Ratings.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Rating");
        }
    }
}