using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class RentController : Controller, ILogger<RentController>
    {
        // GET: Rent
        public ActionResult Index()
        {
            this.GetLog().Debug("Home page accessed");

            return View();
        }

        public ActionResult Show()
        {
            if (Session["Worker"] != null)
            {
                this.GetLog().Debug("Show rents page accessed");

                var rent = Rent.Examples();

                return View(rent);
            }
            else
            {
                this.GetLog().Debug("User not login as a worker");
                Session["Fail"] = "Musisz być zalogowany na konto pracownicze!";
                return RedirectToAction("LoginFormWorker", "Login");
            }
        }

        public ActionResult UserRentShow()
        {
            if (Session["User"] != null)
            {
                this.GetLog().Debug("User rents page accessed");

                int id = Int32.Parse((Session["UserID"].ToString()));
                var rent = Rent.GetRent(id);

                return View(rent);
            }
            else
            {
                this.GetLog().Debug("User not login ");
                Session["Fail"] = "Musisz być zalogowany na swoje konto!";
                return RedirectToAction("LoginForm", "Login");
            }
        }

        public ActionResult AddRent()
        {
            this.GetLog().Debug("Adding rent page accessed");

            return View();
        }

        [HttpPost]
        public ActionResult AddRent(Rent nRent)
        {
            Rent newRent = new Rent();
            Car newCar = new Car();
            Users newUser = new Users();

            foreach(var samochod in Car.CarList())
            {
                var sprawdz = samochod.list.Substring(0, 1);
                if(sprawdz.Equals(nRent.IdSamochodu))
                {
                    nRent.IdSamochodu = Int32.Parse(newCar.list.Substring(0, 1));
                }
                
            }
            newCar = newCar.GetCar(nRent.IdSamochodu);
            
            foreach(var osoba in Users.UserList())
            {
                var sprawdz = osoba.list.Substring(0, 1);
                if(sprawdz.Equals(nRent.IdOsoby))
                {
                    nRent.IdOsoby = Int32.Parse(newUser.list.Substring(0, 1));
                }
            }
            newUser = newUser.GetUser(nRent.IdOsoby);

            //nRent.IdSamochodu = newCar.ID;
            //nRent.Kwota = newCar.CenaDobowa;
            //nRent.IdOsoby = newUser.ID;

            if (newRent.CheckNew(nRent, newCar, newUser))
            {
                this.GetLog().Debug("New car rent added");

                string result = newRent.AddRent(nRent, newCar, newUser);
                ViewData["result"] = result;
                Session["Fail"] = "Wypozyczenie dodano pomyślnie";
                ModelState.Clear();
            }
            else
            {
                this.GetLog().Debug("Cant add new car rent");
                Session["Fail"] = "Wypozyczenie nie zostało dodane";
            }
            return View(nRent);
        }

        public ActionResult AddRentForUser(int AutoID)
        {
            this.GetLog().Debug("Car rent page for users accessed");

            Rent newRent = new Rent();
            newRent.IdSamochodu = AutoID;
            newRent.DataWypozyczenia = DateTime.UtcNow.ToLocalTime();
            newRent.DataOddania = DateTime.UtcNow.ToLocalTime();
            
            return View(newRent);
        }

        [HttpPost]
        public ActionResult AddRentForUser(Rent nRent, int AutoID)
        {
            Rent newRent = new Rent();
            Car newCar = new Car();
            Users newUser = new Users();

            nRent.IdSamochodu = AutoID;
            newCar = newCar.GetCar(nRent.IdSamochodu);

            nRent.IdOsoby = Int32.Parse((Session["UserID"].ToString()));
            newUser = newUser.GetUser(nRent.IdOsoby);

            if (newRent.CheckNew(nRent, newCar, newUser))
            {
                this.GetLog().Debug("User rented car");

                string result = newRent.AddRent(nRent, newCar, newUser);
                ViewData["result"] = result;
                Session["Fail"] = "Wypozyczenie dodano pomyślnie";
                ModelState.Clear();
            }
            else
            {
                this.GetLog().Debug("Some problem with car rent");

                Session["Fail"] = "Wypozyczenie nie zostało dodane";
            }
            return View(nRent);
        }

        public ActionResult AddRentForWorker(int AutoID)
        {
            this.GetLog().Debug("Car rent page for worker accessed");

            Rent newRent = new Rent();
            newRent.IdSamochodu = AutoID;
            newRent.DataWypozyczenia = DateTime.UtcNow.ToLocalTime();
            newRent.DataOddania = DateTime.UtcNow.ToLocalTime();

            return View(newRent);
        }

        [HttpPost]
        public ActionResult AddRentForWorker(Rent nRent, int AutoID)
        {
            Rent newRent = new Rent();
            Car newCar = new Car();
            Users newUser = new Users();

            nRent.IdSamochodu = AutoID;
            newCar = newCar.GetCar(nRent.IdSamochodu);

            foreach (var osoba in Users.UserList())
            {
                var sprawdz = osoba.list.Substring(0, 1);
                if (sprawdz.Equals(nRent.IdOsoby))
                {
                    nRent.IdOsoby = Int32.Parse(newUser.list.Substring(0, 1));
                }
            }
            newUser = newUser.GetUser(nRent.IdOsoby);

            if (newRent.CheckNew(nRent, newCar, newUser))
            {
                this.GetLog().Debug("Rented car");

                string result = newRent.AddRent(nRent, newCar, newUser);
                ViewData["result"] = result;
                Session["Fail"] = "Wypozyczenie dodano pomyślnie";
                ModelState.Clear();
            }
            else
            {
                this.GetLog().Debug("Some problem with car rent");

                Session["Fail"] = "Wypozyczenie nie zostało dodane";
            }
            return View(nRent);
        }
    }
}