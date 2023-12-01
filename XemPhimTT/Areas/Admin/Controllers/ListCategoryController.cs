using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class ListCategoryController : Controller
    {
        // GET: Admin/ListCategory
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            var ListMovie = db.CategoryLists.Include("Movie").Include("Category").ToList();
            return View(ListMovie);
        }
        public ActionResult Create()
        {
            WebMovieEntities db = new WebMovieEntities();

            // Lấy danh sách Categories và Movies từ cơ sở dữ liệu
            var categories = db.Categories.ToList();
            var movies = db.Movies.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var categoryListItems = categories.Select(c => new SelectListItem
            {
                Value = c.categoryid.ToString(),
                Text = c.categoryname
            });
            var movieListItems = movies.Select(m => new SelectListItem
            {
                Value = m.movieid.ToString(),
                Text = m.moviename
            });

            // Đưa danh sách SelectListItems vào ViewData
            ViewData["categoryid"] = categoryListItems;
            ViewData["movieid"] = movieListItems;

            db.Dispose();
            return View();
        }
        public ActionResult Add(CategoryList c)
        {
            WebMovieEntities db = new WebMovieEntities();
            db.CategoryLists.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "ListCategory");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            CategoryList e = db.CategoryLists.Where(i => i.id == Id).FirstOrDefault();

          

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var categories = db.Categories.ToList();
            var movies = db.Movies.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var categoryListItems = categories.Select(c => new SelectListItem
            {
                Value = c.categoryid.ToString(),
                Text = c.categoryname
            }).ToList();

            // Chèn giá trị từ đối tượng e lên đầu danh sách
            var selectedCategoryId = e.categoryid.ToString();
            var selectedCategoryname = e.Category.categoryname;
            categoryListItems = categoryListItems.Prepend(new SelectListItem
            {
                Value = selectedCategoryId,
                Text = selectedCategoryname
            }).ToList();

            var movieListItems = movies.Select(m => new SelectListItem
            {
                Value = m.movieid.ToString(),
                Text = m.moviename
            }).ToList();

            // Chèn giá trị từ đối tượng e lên đầu danh sách
            var selectedMovieId = e.movieid.ToString();
            var selectedMoviename = e.Movie.moviename;
            movieListItems = movieListItems.Prepend(new SelectListItem
            {
                Value = selectedMovieId,
                Text = selectedMoviename
            }).ToList();

            // Đưa danh sách SelectListItems vào ViewData
            ViewData["categoryid"] = categoryListItems;
            ViewData["movieid"] = movieListItems;
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(CategoryList c)
        {
            WebMovieEntities db = new WebMovieEntities();
            CategoryList e = db.CategoryLists.Where(i => i.id == c.id).FirstOrDefault();

            e.categoryid = c.categoryid;
            e.movieid = c.movieid;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "ListCategory");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            CategoryList e = db.CategoryLists.Include("Category").Include("Movie").Where(i => i.id == Id).FirstOrDefault();

            db.Dispose();
            return View(e);
        }

        public ActionResult Remove(CategoryList c)
        {
            WebMovieEntities db = new WebMovieEntities();
            CategoryList e = db.CategoryLists.Where(i => i.id == c.id).FirstOrDefault();

            db.CategoryLists.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "ListCategory");
        }
    }
}