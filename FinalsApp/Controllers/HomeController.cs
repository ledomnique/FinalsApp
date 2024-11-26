﻿using FinalsApp.Models;
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

        public ActionResult RequestBook()
        {
            return View();
        }

        public ActionResult RegisterPage()
        {
            return View();
        }

        public void AddData(RegistrationModel registrationData)
        {
            using (var db = new inkling_dbContext())
            {
                //inserting

            }
        }
    }
}

