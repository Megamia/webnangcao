using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            var ListMovie = db.Movies.Include("Movietype").ToList();
            return View(ListMovie);

        }

        public ActionResult Create()
        {
            WebMovieEntities db = new WebMovieEntities();
            var movietypes = db.Movietypes.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var categoryListItems = movietypes.Select(c => new SelectListItem
            {
                Value = c.movietypeid.ToString(),
                Text = c.movietypename
            });
            ViewData["movietypeid"] = categoryListItems;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Movie c, HttpPostedFileBase fimage, HttpPostedFileBase fbackground)
        {
            WebMovieEntities db = new WebMovieEntities();
            if (fimage != null && fimage.ContentLength > 0)
            {
                c.movieimage = new byte[fimage.ContentLength];
                fimage.InputStream.Read(c.movieimage, 0, fimage.ContentLength);
            }
            if (fbackground != null && fbackground.ContentLength > 0)
            {
                c.moviebackground = new byte[fbackground.ContentLength];
                fbackground.InputStream.Read(c.moviebackground, 0, fbackground.ContentLength);
            }

            db.Movies.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Movie");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movie e = db.Movies.Where(i => i.movieid == Id).FirstOrDefault();
            var movietypes = db.Movietypes.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var categoryListItems = movietypes.Select(c => new SelectListItem
            {
                Value = c.movietypeid.ToString(),
                Text = c.movietypename
            });
            var selectedsubId = e.movietypeid.ToString();
            var selectedsubname = e.Movietype.movietypename;
            categoryListItems = categoryListItems.Prepend(new SelectListItem
            {
                Value = selectedsubId,
                Text = selectedsubname
            }).ToList();

            ViewData["movietypeid"] = categoryListItems;

            
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(Movie c, HttpPostedFileBase fimage, HttpPostedFileBase fbackground)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movie e = db.Movies.Where(i => i.movieid == c.movieid).FirstOrDefault();

            if (fimage != null && fimage.ContentLength > 0)
            {
                c.movieimage = new byte[fimage.ContentLength];
                fimage.InputStream.Read(c.movieimage, 0, fimage.ContentLength);
                e.movieimage = c.movieimage;
            }
            if (fbackground != null && fbackground.ContentLength > 0)
            {
                c.moviebackground = new byte[fbackground.ContentLength];
                fbackground.InputStream.Read(c.moviebackground, 0, fbackground.ContentLength);
                e.moviebackground = c.moviebackground;
            }

            e.moviename = c.moviename;
            e.duration = c.duration;
            e.releaseyear = c.releaseyear;
            e.director = c.director;
            e.description = c.description;
            e.movietypeid = c.movietypeid;

            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Movie");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movie e = db.Movies.Where(i => i.movieid == Id).FirstOrDefault();
            return View(e);
        }

        public ActionResult Remove(Movie c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movie e = db.Movies.Where(i => i.movieid == c.movieid).FirstOrDefault();

            db.Movies.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Movie");
        }
    }
}