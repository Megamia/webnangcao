using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Controllers
{
    public class PlayController : Controller
    {
        // GET: Play
        public ActionResult DetailVideo(int Id)
        {
            var viewModel = new PlayMovie();
            WebMovieEntities db = new WebMovieEntities();
            viewModel.Data1 = db.Videos.Where(i => i.videoid == Id).FirstOrDefault();
            viewModel.Data2 = db.Videos.Where(i => i.movieid == viewModel.Data1.movieid).ToList();
            viewModel.Data3 = db.Comments.Where(i => i.movieid == viewModel.Data1.movieid).ToList();
            return View(viewModel);
        }
    }

}