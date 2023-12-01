using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            List<User> ListUser = db.Users.ToList();
            db.Dispose();

            return View(ListUser);
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add(User c)
        {
            WebMovieEntities db = new WebMovieEntities();
            db.Users.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "User");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            User e = db.Users.Where(i => i.userid == Id).FirstOrDefault();
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(User c)
        {
            WebMovieEntities db = new WebMovieEntities();
            User e = db.Users.Where(i => i.userid == c.userid).FirstOrDefault();

            e.username = c.username;
            e.password = c.password;
            e.email = c.email;
            e.fullname = c.fullname;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "User");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            User e = db.Users.Where(i => i.userid == Id).FirstOrDefault();

            db.Dispose();
            return View(e);
        }

        public ActionResult Remove(User c)
        {
            WebMovieEntities db = new WebMovieEntities();
            User e = db.Users.Where(i => i.userid == c.userid).FirstOrDefault();

            db.Users.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "User");
        }
    }
}