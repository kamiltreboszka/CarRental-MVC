using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class UsersController : Controller, ILogger<UsersController>
    {
        // GET: User
        public ActionResult Index()
        {
            this.GetLog().Debug("Home page accessed");

            return View();
        }

        public ActionResult Show()
        {
            if (Session["Worker"] != null)
            {
                this.GetLog().Debug("Show users page accessed");

                var user = Users.Examples();

                return View(user);
            }
            else
            {
                this.GetLog().Debug("User not login as a worker");
                Session["Fail"] = "Musisz być zalogowany na konto pracownicze!";
                return RedirectToAction("LoginFormWorker", "Login");
            }
        }

        public ActionResult AddUser()
        {
            this.GetLog().Debug("Adding user page accessed");

            return View();
        }

        [HttpPost]
        public ActionResult AddUser(Users nUser)
        {
            Users newUser = new Users();


            if (newUser.CheckNew(nUser) && (nUser != null))
            {
                this.GetLog().Debug("User added");

                string result = newUser.AddUser(nUser);
                ViewData["result"] = result;
                Session["Fail"] = "Dodano nowego użytkownika";
                ModelState.Clear();
            }
            else
            {
                this.GetLog().Debug("Fail during added user");

                Session["Fail"] = "Nie udało się dodać użytkownika";
                ModelState.Clear();
            }
            return View();
        }

        public ActionResult UserRegister()
        {
            this.GetLog().Debug("User register page accessed");

            return View();
        }

        [HttpPost]
        public ActionResult UserRegister(Users nUser)
        {
            Users newUser = new Users();

            if (newUser.CheckNew(nUser))
            {
                this.GetLog().Debug("New account created");

                string result = newUser.AddUser(nUser);
                ViewData["result"] = result;
                Session["AddUser"] = nUser;
                Session["Fail"] = "Dodano nowego użytkownika";
                ModelState.Clear();
            }
            else
            {
                this.GetLog().Debug("Account doesnt create");
                Session["Fail"] = "Nie udało się dodać użytkownika";
            }
            return View(nUser);
        }
    }
}