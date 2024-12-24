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

        public System.Web.Mvc.ActionResult Homepage()
        {
            return View();
        }

        public System.Web.Mvc.ActionResult LoginPage()
        {
            return View();
        }
        public System.Web.Mvc.ActionResult FindBook()
        {
            return View();
        }

        public System.Web.Mvc.ActionResult UserLogin()
        {
            return View();
        }

        public System.Web.Mvc.ActionResult RequestBook()
        {
            return View();
        }

        public System.Web.Mvc.ActionResult RegisterPage()
        {
            return View();
        }
        public System.Web.Mvc.ActionResult Dashboard()
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

        [HttpPost]
        public string saveUser([FromBody] users_tblModel e)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var sqlStatement = string.Format("INSERT INTO users_tbl (firstName, lastName, email, PASSWORD) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", e.firstName, e.lastName, e.email, e.password);
                    // Add if condition, if e.userId is = 0, Insert (because user is not yet existing), > 0 is Update
                    MySqlCommand command = new MySqlCommand(sqlStatement, conn);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                    if (e.userID > 0)
                        return "Record has been successfully updated.";
                    else
                        return "Record has been successfully created.";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        // Login Function for User + Admin
        [HttpPost]
        public JsonResult LogData(LoginModel loginData)
        {
            using (var db = new inkling_dbContext())
            {
                //Find user by username or email
                var existingUser = db.users_tbl
                    .FirstOrDefault(u =>
                        (u.email.ToLower() == loginData.emailLogin.ToLower())
                        );

                if (existingUser != null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                //Check if password is correct
                bool isPassword = BCrypt.Net.BCrypt.Verify(loginData.passwordLogin, existingUser.password);
                if (!isPassword)
                {
                    return Json(new { success = false, message = "Invalid password." });
                }

                //Login successful = save cookies
                HttpCookie authCookie = new HttpCookie("UserSession");
                authCookie.Value = existingUser.userID.ToString(); //Stores user ID/session token
                authCookie.Expires = DateTime.Now.AddHours(1); //Cookie expires in 1 hour
                Response.Cookies.Add(authCookie);

                //Login successful
                return Json(new { success = true, message = "Login successful." });

            }
        }


        /*
        [HttpPost]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate old password and update new password logic here
                bool isPasswordUpdated = PasswordService.UpdatePassword(User.Identity.Name, model.OldPassword, model.NewPassword);

                if (isPasswordUpdated)
                {
                    return Ok(new { message = "Password changed successfully." });
                }
                else
                {
                    return BadRequest(new { message = "Invalid old password." });
                }
            }

            return BadRequest(new { message = "Invalid request." });
        }

        public class ChangePasswordModel
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }
        */


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

