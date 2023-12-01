using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Controllers
{
    public class ListMovieController : Controller
    {
        // GET: ListMovie
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            var movie = db.Movies.ToList();
            return View(movie);
        }

        public ActionResult Detail(int Id)
        {
            var viewModel = new DetailMovie();
            WebMovieEntities db = new WebMovieEntities();
            viewModel.Data1  = db.Movies.Where(i => i.movieid == Id).FirstOrDefault();
            viewModel.Data2 = db.CategoryLists.Where(i => i.movieid == Id).ToList();
            viewModel.Data3 = db.Comments.Where(i => i.movieid == Id).ToList();
            viewModel.Data4 = db.Videos.Where(i => i.movieid == Id).ToList();
            viewModel.Data5 = db.Videos.Where(i => i.movieid == Id).FirstOrDefault();
            viewModel.Data6 = "";
            return View(viewModel);      
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comment model)
        {
            if (ModelState.IsValid)
            {
                using (WebMovieEntities db = new WebMovieEntities())
                {
                    db.Comments.Add(model);
                    db.SaveChanges();
                }

                return RedirectToAction("Detail", new { Id = model.movieid });
            }

            // Xử lý lỗi nếu dữ liệu không hợp lệ
            return RedirectToAction("Detail", new { Id = model.movieid });
        }
    }
}