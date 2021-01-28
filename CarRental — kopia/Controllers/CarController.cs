using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class CarController : Controller, ILogger<CarController>
    {
        // GET: Car
        public ActionResult Index()
        {
            this.GetLog().Debug("Home page accessed");
            return View();
        }

        public ActionResult Show()
        {
            this.GetLog().Debug("Show car page accessed");

            var cars = Car.Examples();
            return View(cars);
        }

        public ActionResult ShowForWorker()
        {
            if (Session["Worker"] != null)
            {
                this.GetLog().Debug("Show car page for worker accessed");

                var cars = Car.Examples();
                return View(cars);
            }
            else
            {
                this.GetLog().Debug("User not login as a worker");
                Session["Fail"] = "Musisz być zalogowany na konto pracownicze!";
                return RedirectToAction("LoginFormWorker", "Login");
            }
        }

        public ActionResult ShowForUser()
        {
            if (Session["User"] != null)
            {
                this.GetLog().Debug("Show car page for user accessed");

                var cars = Car.Examples();
                return View(cars);
            }
            else
            {
                this.GetLog().Debug("User not login");
                Session["Fail"] = "Musisz być zalogowany na swoje konto!";
                return RedirectToAction("LoginForm", "Login");
            }
        }

        public ActionResult ShowSelect()
        {
            if (Session["Worker"] != null || Session["User"] != null)
            {
                this.GetLog().Debug("Show select cars page accessed");

                return View();
            }
            else
            {
                this.GetLog().Debug("User not login");
                Session["Fail"] = "Musisz być zalogowany na konto!";
                return RedirectToAction("LoginForm", "Login");
            }
        }

        [HttpPost]
        public ActionResult ShowSelect(Car nCar)
        {
            List<Car> newCar = new List<Car>();
            newCar = Car.SelectCar(nCar);

            ViewData["result"] = newCar;
            ModelState.Clear();

            return View();
        }

        public ActionResult AddCar()
        {
            if (Session["Worker"] != null)
            {
                this.GetLog().Debug("Adding car page accessed");

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
        public ActionResult AddCar(Car nCar)
        {
            Car newCar = new Car();

            if (newCar.CheckNew(nCar))
            {
                this.GetLog().Debug("Car added");

                string result = newCar.AddCar(nCar);
                ViewData["result"] = result;
                Session["Fail"] = "Dodano nowy samochod do wypożyczalni";
                ModelState.Clear();
            }
            else
            {
                this.GetLog().Debug("Failed adding car");
                Session["Fail"] = "Nie dodano nowego samochodu do wypożyczalni";
            }
            return View();
        }
    }
}