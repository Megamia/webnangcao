using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            List<Category> ListCategory = db.Categories.ToList();
            db.Dispose();

            return View(ListCategory);
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add(Category c)
        {
            WebMovieEntities db = new WebMovieEntities();
            db.Categories.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Category");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Category e = db.Categories.Where(i => i.categoryid == Id).FirstOrDefault();
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(Category c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Category e = db.Categories.Where(i => i.categoryid == c.categoryid).FirstOrDefault();

            e.categoryname = c.categoryname;
            e.categorynamevn = c.categorynamevn;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Category");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Category e = db.Categories.Where(i => i.categoryid == Id).FirstOrDefault();

            db.Dispose();
            return View(e);
        }

        public ActionResult Remove(Category c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Category e = db.Categories.Where(i => i.categoryid == c.categoryid).FirstOrDefault();

            db.Categories.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Category");
        }
    }
}