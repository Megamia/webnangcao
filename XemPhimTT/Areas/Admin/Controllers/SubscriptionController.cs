using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;

namespace XemPhimTT.Areas.Admin.Controllers
{
    public class SubscriptionController : Controller
    {
        // GET: Subscription
        public ActionResult Index()
        {
            WebMovieEntities db = new WebMovieEntities();
            List<Subscription> ListSubscription = db.Subscriptions.ToList();
            return View(ListSubscription);
        }

        public ActionResult Create()
        {
            WebMovieEntities db = new WebMovieEntities();
            var users = db.Users.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var userListItems = users.Select(c => new SelectListItem
            {
                Value = c.userid.ToString(),
                Text = c.username
            });
            ViewData["userid"] = userListItems;

            var SubscriptionPackageList = db.SubscriptionPackages.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var SubscriptionPackageListItems = SubscriptionPackageList.Select(m => new SelectListItem
            {
                Value = m.subscriptionpackageid.ToString(),
                Text = m.name
            });
            ViewData["subscriptionpackageid"] = SubscriptionPackageListItems;

            return View();
        }
        public ActionResult Add(Subscription c)
        {
            WebMovieEntities db = new WebMovieEntities();       
            db.Subscriptions.Add(c);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Subscription");
        }
        public ActionResult Edit(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Subscription e = db.Subscriptions.Where(i => i.subscriptionid == Id).FirstOrDefault();

            var users = db.Users.ToList();
            var subs = db.SubscriptionPackages.ToList();

            // Chuyển đổi danh sách Categories và Movies thành SelectListItems
            var userListItems = users.Select(c => new SelectListItem
            {
                Value = c.userid.ToString(),
                Text = c.username
            }).ToList();

            // Chèn giá trị từ đối tượng e lên đầu danh sách
            var selectedUserId = e.userid.ToString();
            var selectedusername = e.User.username;
            userListItems = userListItems.Prepend(new SelectListItem
            {
                Value = selectedUserId,
                Text = selectedusername
            }).ToList();

            var subListItems = subs.Select(m => new SelectListItem
            {
                Value = m.subscriptionpackageid.ToString(),
                Text = m.name
            }).ToList();

            // Chèn giá trị từ đối tượng e lên đầu danh sách
            var selectedsubId = e.subscriptionpackageid.ToString();
            var selectedsubname = e.SubscriptionPackage.name;
            subListItems = subListItems.Prepend(new SelectListItem
            {
                Value = selectedsubId,
                Text = selectedsubname
            }).ToList();

            // Đưa danh sách SelectListItems vào ViewData
            ViewData["userid"] = userListItems;
            ViewData["subscriptionpackageid"] = subListItems;
            db.Dispose();
            return View(e);
        }

        public ActionResult Save(Subscription c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Subscription e = db.Subscriptions.Where(i => i.subscriptionid == c.subscriptionid).FirstOrDefault();

            e.userid = c.userid;
            e.startdate = c.startdate;
            e.enddate = c.enddate;
            e.state = c.state;
            e.subscriptionpackageid = c.subscriptionpackageid;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Subscription");
        }
        public ActionResult Delete(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            Subscription e = db.Subscriptions.Where(i => i.subscriptionid == Id).FirstOrDefault();
            return View(e);
        }

        public ActionResult Remove(Subscription c)
        {
            WebMovieEntities db = new WebMovieEntities();
            Subscription e = db.Subscriptions.Where(i => i.subscriptionid == c.subscriptionid).FirstOrDefault();

            db.Subscriptions.Remove(e);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Subscription");
        }
    }
}