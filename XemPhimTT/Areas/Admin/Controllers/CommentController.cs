using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            WebMovieEntities  db = new WebMovieEntities();
            var ListComment = db.Comments.Include("Movie").Include("User").ToList();
            return View(ListComment);
        }

        public ActionResult Create()
        {
            WebMovieEntities db = new WebMovieEntities();

            // Lấy danh sách Categories và Movies từ cơ sở dữ liệu
            var movies = db.Movies.ToList();
            var users = db.Users.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var movieListItems = movies.Select(c => new SelectListItem
            {
                Value = c.movieid.ToString(),
                Text = c.moviename
            });
            var userListItems = users.Select(m => new SelectListItem
            {
                Value = m.userid.ToString(),
                Text = m.username
            });

            // Đưa danh sách SelectListItems vào ViewData
            ViewData["movieid"] = movieListItems;
            ViewData["userid"] = userListItems;

            db.Dispose();
            return View();
        }
        public ActionResult Add(Comment c)
        {
            WebMovieEntities db = new WebMovieEntities();
            db.Comments.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Comment");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Comment e = db.Comments.Where(i => i.commentid == Id).FirstOrDefault();
            var users = db.Users.ToList();
            var movies = db.Movies.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var movieListItems = movies.Select(c => new SelectListItem
            {
                Value = c.movieid.ToString(),
                Text = c.moviename
            }).ToList();

            // Chèn giá trị từ đối tượng e lên đầu danh sách
            var selectedMovieId = e.movieid.ToString();
            var selectedMoviename = e.Movie.moviename;
            movieListItems = movieListItems.Prepend(new SelectListItem
            {
                Value = selectedMovieId,
                Text = selectedMoviename
            }).ToList();

            var userListItems = users.Select(m => new SelectListItem
            {
                Value = m.userid.ToString(),
                Text = m.username
            }).ToList();

            // Chèn giá trị từ đối tượng e lên đầu danh sách
            var selectedUserId = e.userid.ToString();
            var selectedUsername = e.User.username;
            userListItems = userListItems.Prepend(new SelectListItem
            {
                Value = selectedUserId,
                Text = selectedUsername
            }).ToList();

            // Đưa danh sách SelectListItems vào ViewData
            
            ViewData["movieid"] = movieListItems;
            ViewData["userid"] = userListItems;
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(Comment c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Comment e = db.Comments.Where(i => i.commentid == c.commentid).FirstOrDefault();

            e.comment1 = c.comment1;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Comment");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Comment e = db.Comments.Where(i => i.commentid == Id).FirstOrDefault();

            db.Dispose();
            return View(e);
        }

        public ActionResult Remove(Comment c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Comment e = db.Comments.Where(i => i.commentid == c.commentid).FirstOrDefault();

            db.Comments.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Comment");
        }
    }
}