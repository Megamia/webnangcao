using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XemPhimTT.Models;
using System.Text.RegularExpressions;

namespace XemPhimTT.Controllers
{
    public class AccountController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            if (ViewBag.SuccessMessage != null)
            {
                // Hiển thị thông báo thành công
                ViewBag.IsRegistered = true;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            // Kiểm tra đăng nhập
            if (ValidateUser(user.username, user.password))
            {
                WebMovieEntities db = new WebMovieEntities();
                User e = db.Users.Where(i => i.username == user.username).FirstOrDefault();
                int userId = e.userid;
                string fullname = e.fullname;
                Session["userId"] = userId;
                Session["username"] = user.username;
                Session["fullname"] = fullname;
                // Đăng nhập thành công, thực hiện các hành động bổ sung (ví dụ: lưu phiên đăng nhập, chuyển hướng đến trang chủ, v.v.)
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Sai thông tin đăng nhập, hiển thị thông báo lỗi
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu!");
                return View(user);
            }
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            // Kiểm tra xem tên người dùng đã tồn tại chưa
            if (IsUsernameExists(user.username))
            {
                // Tên người dùng đã tồn tại, hiển thị thông báo lỗi
                ModelState.AddModelError("", "Tài khoản đã tồn tại!");
                return View(user);
            }

            if (IsTextboxNull(user.username, user.password, user.email, user.fullname))
            {
                // Tên người dùng đã tồn tại, hiển thị thông báo lỗi
                ModelState.AddModelError("", "Chưa điền đầy đủ thông tin!");
                return View(user);
            }
            else
            {           
                    if (IsEmailError(user.email))
                    {
                        // Tên người dùng đã tồn tại, hiển thị thông báo lỗi
                        ModelState.AddModelError("", "Email không đúng định dạng!");
                        return View(user);
                    }
            }
           

            if (IsEmailExists(user.email))
            {
                // Tên người dùng đã tồn tại, hiển thị thông báo lỗi
                ModelState.AddModelError("", "Email đã được đăng ký!");
                return View(user);
            }

            // Thêm người dùng vào cơ sở dữ liệu
            CreateUser(user);
            if (TempData.ContainsKey("SuccessMessage"))
            {
                // Lấy thông điệp thành công từ TempData
                string successMessage = TempData["SuccessMessage"].ToString();
                ViewBag.SuccessMessage = successMessage;
            }
            // Đăng ký thành công, thực hiện các hành động bổ sung (ví dụ: chuyển hướng đến trang đăng nhập, v.v.)
            return RedirectToAction("Login");
        }

        private bool ValidateUser(string username, string password)
        {
            // Thực hiện kiểm tra đăng nhập trong cơ sở dữ liệu
            using (WebMovieEntities db = new WebMovieEntities())
            {
                User user = db.Users.FirstOrDefault(u => u.username == username && u.password == password);
                return user != null;
            }
        }
        private bool IsTextboxNull(string username, string password, string email, string fullname)
        {
            if(username == null || password == null || email == null || fullname == null)
            {
                return true;
            }
            return false;
        }
        private bool IsEmailError(string email)
        {
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                // Kiểm tra khớp với mẫu email
                bool isMatch = Regex.IsMatch(email, emailPattern);

                return !isMatch;            
        }

        private bool IsUsernameExists(string username)
        {
            // Kiểm tra xem tên người dùng đã tồn tại trong cơ sở dữ liệu chưa
            using (WebMovieEntities db = new WebMovieEntities())
            {
                User user = db.Users.FirstOrDefault(u => u.username == username);
                return user != null;
            }
        }

        private bool IsEmailExists(string email)
        {
            // Kiểm tra xem tên người dùng đã tồn tại trong cơ sở dữ liệu chưa
            using (WebMovieEntities db = new WebMovieEntities())
            {
                User user = db.Users.FirstOrDefault(u => u.email == email);
                return user != null;
            }
        }

        private void CreateUser(User user)
        {
            // Thêm người dùng vào cơ sở dữ liệu
            using (WebMovieEntities db = new WebMovieEntities())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            TempData["SuccessMessage"] = "Đăng ký thành công!";
        }
        public ActionResult Logout()
        {
            // Xóa giá trị khỏi Session
            Session.Clear();

            // Các xử lý khác (nếu cần)

            return RedirectToAction("Index", "Home");
        }

        public ActionResult InfoAccount(int Id)
        {
            var viewModel = new Account();
            WebMovieEntities db = new WebMovieEntities();
            viewModel.Data1 = db.Users.Where(i => i.userid == Id).FirstOrDefault();
            return View(viewModel);
        }
        public ActionResult EditAccount(int Id)
        {
            WebMovieEntities db = new WebMovieEntities();
            User e = db.Users.Where(i => i.userid == Id).FirstOrDefault();
            return View(e);
        }

        public ActionResult Save(User user)
        {
            WebMovieEntities db = new WebMovieEntities();
            User e = db.Users.Where(i => i.userid == user.userid).FirstOrDefault();

            if (IsUsernameExists(user.username) && e.username!= user.username)
            {
                // Tên người dùng đã tồn tại, hiển thị thông báo lỗi
                ModelState.AddModelError("", "Tài khoản đã tồn tại!");
                return RedirectToAction("EditAccount", "Account", new { id = user.userid });
            }
            if (IsEmailError(user.email))
             {
                    // Tên người dùng đã tồn tại, hiển thị thông báo lỗi
                    ModelState.AddModelError("", "Email không đúng định dạng!");
                    return RedirectToAction("EditAccount", "Account", new { id = user.userid });
             }
            if (IsEmailExists(user.email) && e.email != user.email)
            {
                // Tên người dùng đã tồn tại, hiển thị thông báo lỗi
                ModelState.AddModelError("", "Email đã được đăng ký!");
                return RedirectToAction("EditAccount", "Account", new { id = user.userid });
            }

            e.username = user.username;
            e.fullname = user.fullname;
            e.email = user.email;
            e.password = user.password;
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("InfoAccount","Account", new { id = user.userid });
        }

    }
}