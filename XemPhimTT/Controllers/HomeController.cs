using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;
using System.Web.UI;

namespace XemPhimTT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            var movie = db.Movies.ToList();
            return View(movie);
        }
       
    }
}