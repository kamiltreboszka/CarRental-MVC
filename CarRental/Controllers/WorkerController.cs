using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class WorkerController : Controller, ILogger<WorkerController>
    {
        // GET: Worker
        public ActionResult Index()
        {
            this.GetLog().Debug("Home page accessed");
            return View();
        }

        public ActionResult Show()
        {
            if (Session["Worker"] != null)
            {
                this.GetLog().Debug("Show page accessed");

                var workers = Worker.Examples();
                return View(workers);
            }
            else
            {
                this.GetLog().Debug("User not login as a worker");
                Session["Fail"] = "Musisz być zalogowany na konto pracownicze!";
                return RedirectToAction("LoginFormWorker", "Login");
            }
        }

        public ActionResult AddWorker()
        {
            if (Session["Worker"] != null)
            {
                this.GetLog().Debug("Adding worker page accessed");
                return View();
            }
            else
            {
                this.GetLog().Debug("User not login as a worker");
                Session["Fail"] = "Musisz być zalogowany na konto pracownicze!";
                return RedirectToAction("LoginFormWorker", "Login");
            }
        }

        [HttpPost]
        public ActionResult AddWorker(Worker nWorker)
        {
            Worker newWorker = new Worker();

            if (newWorker.CheckNew(nWorker))
            {
                this.GetLog().Debug("New worker added");
                string result = newWorker.AddWorker(nWorker);
                ViewData["result"] = result;
                Session["Fail"] = "Dodano nowe konto pracownika";
                ModelState.Clear();
            }
            else
            {
                this.GetLog().Debug("Failed adding new worker");
                Session["Fail"] = "Nie udało się dodać konta pracownika";
            }
            return View(nWorker);
        }
    }
}