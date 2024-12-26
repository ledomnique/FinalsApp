using FinalsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalsApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Homepage()
        {
            return View();
        }

        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult FindBook()
        {
            return View();
        }

        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RequestBook()
        {
            return View();
        }

        public ActionResult RegisterPage()
        {
            return View();
        }

        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }


        /* public JsonResult LoadBookInfo()
        {
            using (var db = new inkling_dbContext())
            {
                var userData = (from bData in db.books_tbl
                                join gdata in db.genres_tbl on bData.genreID equals gdata.genreID
                                select new { bData, gdata }).ToList();
                return Json(userData, JsonRequestBehavior.AllowGet);
            }
        } */

        public JsonResult LoadUsersData()
        {
            using (var db = new inkling_dbContext())
            {
                var empData = (from eData in db.users_tbl
                               select new { eData }).ToList();
                return Json(empData, JsonRequestBehavior.AllowGet);
            }
        }

        public int SignupUser(users_tblModel newUser)
        {
            using (var db = new inkling_dbContext())
            {
                newUser.joined_on = DateTime.Now;
                db.users_tbl.Add(newUser);
                db.SaveChanges();
                return newUser.userID;
            }
        }

        public int LoginUser(string email, string password)
        {
            using (var db = new inkling_dbContext())
            {
                try
                {
                    var user = db.users_tbl.FirstOrDefault(x => x.email == email && x.password == password);
                    return user?.userID ?? 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public JsonResult GetUserById(int userId)
        {
            using (var db = new inkling_dbContext())
            {
                var user = db.users_tbl
                    .Where(x => x.userID == userId)
                    .Select(x => new
                    {
                        x.userID,
                        x.firstName,
                        x.lastName,
                        x.email
                    })
                    .FirstOrDefault();

                if (user == null)
                {
                    return Json(new { error = "User not found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }

        // New ChangePassword Action
        public JsonResult ChangePassword(int userId, string currentPassword, string newPassword)
        {
            using (var db = new inkling_dbContext())
            {
                try
                {
                    var user = db.users_tbl.FirstOrDefault(x => x.userID == userId);

                    if (user == null)
                    {
                        return Json(new { success = false, message = "User not found." });
                    }

                    // Ensure the current password matches the database record
                    if (user.password != currentPassword)
                    {
                        return Json(new { success = false, message = "Current password is incorrect." });
                    }

                    // Update password in the database
                    user.password = newPassword;
                    db.SaveChanges();

                    return Json(new { success = true, message = "Password updated successfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "An error occurred while updating the password.", error = ex.Message });
                }
            }
        }

        public int RequestBook(bookrequest_tblModel newBook)
        {
            using (var db = new inkling_dbContext())
            {
                newBook.created_on = DateTime.Now;
                db.bookrequest_tbl.Add(newBook);
                db.SaveChanges();
                return newBook.bookreqID; // Return the bookreqID to Angular
            }
        }

    }
}
