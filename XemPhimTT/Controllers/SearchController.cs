using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Controllers
{
    public class SearchController : Controller
    {
        private WebMovieEntities db = new WebMovieEntities();
        [HttpGet]
        public JsonResult GetSearchResults(string keyword)
        {
            var results = db.Movies.Where(m => m.moviename.Contains(keyword))
                                   .Select(m => new { id = m.movieid, name = m.moviename })
                                   .ToList();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}