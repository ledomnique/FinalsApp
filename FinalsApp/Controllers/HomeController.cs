using FinalsApp.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionResult = System.Web.Mvc.ActionResult;
using HttpDeleteAttribute = System.Web.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace FinalsApp.Controllers
{
    public class HomeController : Controller
    {
        public string MySQLConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["inkling_db"].ToString(); }
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

        public ActionResult RequestBook()
        {
            return View();
        }

        public ActionResult RegisterPage()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public System.Web.Mvc.ActionResult ManageUsers()
        {
            return View();
        }
        public System.Web.Mvc.ActionResult ManageBooks()
        {
            return View();
        }
        public System.Web.Mvc.ActionResult SelectedBook()
        {
            return View();
        }
        public System.Web.Mvc.ActionResult AboutUs()
        {
            return View();
        }
        public System.Web.Mvc.ActionResult Terms()
        {
            return View();
        }
        public System.Web.Mvc.ActionResult Contact()
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
         return newBook.bookreqID;
     }
 }

        //Lewis' Code
        [HttpGet]
        public System.Web.Mvc.ActionResult Admin()
        {


            ViewBag.Message = "Your application description page.";

            //ViewBag.adminData = table;






            return View();
        }

        public class UserEntity
        {
            public int userID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public DateTime joined_on { get; set; }

        }
        [HttpGet]
        public string GetUsers()
        {
            string result = string.Empty;
            //MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=inkling_db;Uid=root;");
            MySqlConnection conn = new MySqlConnection(MySQLConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM users_tbl", conn);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new CustomDataSetConverter());
            settings.Formatting = Formatting.Indented;
            settings.MaxDepth = Int32.MaxValue;
            string jsonObject = JsonConvert.SerializeObject(table, settings);
            result = jsonObject;
            return result;
        }

        public class BookEntity
        {
            public int bookID { get; set; }
            public string title { get; set; }
            public string author { get; set; }
            public int genreID { get; set; }
            public DateTime published_on { get; set; }
            public string description { get; set; }
        }
        [HttpGet]
        public string GetBooks()
        {
            string result = string.Empty;

            //MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=inkling_db;Uid=root;");
            MySqlConnection conn = new MySqlConnection(MySQLConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM books_tbl", conn);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new CustomDataSetConverter());
            settings.Formatting = Formatting.Indented;
            settings.MaxDepth = Int32.MaxValue;

            string jsonObject = JsonConvert.SerializeObject(table, settings);

            result = jsonObject;

            return result;


        }

        public class AdminEntity
        {
            public int adminId { get; set; }
            public string userName { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public DateTime created_on { get; set; }
        }
        [HttpGet]
        public string GetAdmins()

        {
            string result = string.Empty;

            //MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=inkling_db;Uid=root;");
            MySqlConnection conn = new MySqlConnection(MySQLConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM admin_tbl", conn);

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new CustomDataSetConverter());
            settings.Formatting = Formatting.Indented;
            settings.MaxDepth = Int32.MaxValue;

            string jsonObject = JsonConvert.SerializeObject(table, settings);

            result = jsonObject;

            return result;


        }

        [HttpDelete]
        public JsonResult DeleteUser(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string query = "DELETE FROM inkling_db.users_tbl WHERE userID = @userID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userID", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No rows affected." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}

