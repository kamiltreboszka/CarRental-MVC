using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class LoginController : Controller, ILogger<LoginController>
    {
        // GET: Login
        public ActionResult Index()
        {
            this.GetLog().Debug("Home page accessed");

            return View();
        }

        public ActionResult LoginForm()
        {
            this.GetLog().Debug("Login page accessed");

            return View();
        }

        [HttpPost]
        public ActionResult PerformLogin(Users user)
        {
            if(user.Verified)
            {
                this.GetLog().Debug("User log in");

                Session["Login"] = user.Email;
                Session["User"] = user.Email;

                user = user.GetUser();

                Session["UserID"] = user.ID;
                Session["UserName"] = user.Name;
                Session["UserSurname"] = user.Surname;

                return Redirect(Url.Action("Index", "Home"));
            }
            else
            {
                this.GetLog().Debug("User not log in");

                Session["Fail"] = "Proba logowania nie powiodła się, przyko nam :( ";
                return Redirect(Url.Action("LoginForm"));
            }
        }

        public ActionResult LoginFormWorker()
        {
            this.GetLog().Debug("Login page for worker accessed");

            return View();
        }

        [HttpPost]
        public ActionResult LoginWorker(Worker user)
        {
            if (user.Verified)
            {
                this.GetLog().Debug("Worker log in");

                Session["Login"] = user.Email;
                Session["Worker"] = user.Email;

                user = user.GetUser();

                Session["UserName"] = user.Name;
                Session["UserSurname"] = user.Surname;

                return Redirect(Url.Action("Index", "Home"));
            }
            this.GetLog().Debug("Worker not log in");

            Session["Fail"] = "Proba logowania nie powiodła się, przyko nam :( ";
            return Redirect(Url.Action("LoginFormWorker"));
        }

        public ActionResult Logout()
        {
            this.GetLog().Debug("Logout");

            Session.Remove("Login");
            Session.Remove("User");
            Session.Remove("Worker");
            Session.Remove("UserName");
            Session.Remove("UserSurname");
            
            return Redirect(Url.Action("Index", "Home"));
        }
    }
}