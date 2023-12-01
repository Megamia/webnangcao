using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Controllers
{
    public class DanhMucController : Controller
    {
        public ActionResult Index()
        {
            using (WebMovieEntities db = new WebMovieEntities())
            {
                var categories = db.Categories.Select(c => c.categoryname).ToList();
                ViewBag.Categories = categories;
            }

            return View();
        }
    }
}