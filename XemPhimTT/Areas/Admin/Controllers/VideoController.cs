using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            var ListVideo = db.Videos.Include("Movie").ToList();
            return View(ListVideo);
        }
        public ActionResult Create()
        {
            WebMovieEntities db = new WebMovieEntities();
            var movies = db.Movies.ToList();
            var movieListItems = movies.Select(m => new SelectListItem
            {
                Value = m.movieid.ToString(),
                Text = m.moviename
            });

            // Đưa danh sách SelectListItems vào ViewData
            ViewData["movieid"] = movieListItems;

            db.Dispose();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Video c, HttpPostedFileBase fvideo)
        {
            WebMovieEntities db = new WebMovieEntities();
            if (fvideo != null && fvideo.ContentLength > 0)
            {
                c.videofile = new byte[fvideo.ContentLength];
                fvideo.InputStream.Read(c.videofile, 0, fvideo.ContentLength);
                string fileName = fvideo.FileName;
                c.videofilename = fileName;
            }
            db.Videos.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Video");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Video e = db.Videos.Where(i => i.videoid == Id).FirstOrDefault();
            var movietypes = db.Movies.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var categoryListItems = movietypes.Select(c => new SelectListItem
            {
                Value = c.movieid.ToString(),
                Text = c.moviename
            });
            var selectedsubId = e.movieid.ToString();
            var selectedsubname = e.Movie.moviename;
            categoryListItems = categoryListItems.Prepend(new SelectListItem
            {
                Value = selectedsubId,
                Text = selectedsubname
            }).ToList();

            ViewData["movieid"] = categoryListItems;
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(Video c, HttpPostedFileBase fvideo)
        {
            WebMovieEntities db = new WebMovieEntities();
            Video e = db.Videos.Where(i => i.videoid == c.videoid).FirstOrDefault();
            if (fvideo != null && fvideo.ContentLength > 0)
            {
                c.videofile = new byte[fvideo.ContentLength];
                fvideo.InputStream.Read(c.videofile, 0, fvideo.ContentLength);
                e.videofile = c.videofile;
                string fileName = fvideo.FileName;
                c.videofilename = fileName;
            }
            e.videofilename = c.videofilename;
            e.videoname = c.videoname;
            e.movieid = c.movieid;

            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Video");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Movie e = db.Movies.Where(i => i.movieid == Id).FirstOrDefault();

            db.Dispose();
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