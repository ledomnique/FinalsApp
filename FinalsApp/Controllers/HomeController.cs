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

        public System.Web.Mvc.ActionResult AdminDashboard()
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
            public string userName { get; set; }
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

                    var sqlStatement = string.Format("INSERT INTO users_tbl (firstName, lastName, userName, email, PASSWORD) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", e.firstName, e.lastName, e.userName, e.email, e.password);
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
                        (u.userName.ToLower == loginData.usernameLogin.ToLower())
                        );

                if (existingUser != null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                //Check if password is correct
                bool isPassword = BCrypt.Net.BCrypt.Verify(loginData.password, existingUser.password);
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










    }
}

