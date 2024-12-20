using FinalsApp.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalsApp.Controllers
{
    public class HomeController : Controller
    {
        public string MySQLConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["inkling_db"].ToString(); }
        }
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


        [HttpGet]
        public ActionResult Admin()
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
            public int AdminId { get; set; }
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

 








    }
}

