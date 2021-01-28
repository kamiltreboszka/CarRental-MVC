using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Car
    {
        public int ID { get; set; }
        [Display(Name = "MARKA")]
        public string Marka { get; set; }
        [Display(Name = "MODEL")]
        public string Model { get; set; }
        [Display(Name = "ROK")]
        public string Rok { get; set; }
        [Display(Name = "PALIWO")]
        public string Paliwo { get; set; }
        [Display(Name = "CENA DOBOWA")]
        public double CenaDobowa { get; set; }

        public static int i = 0;
        public string list { get; set; }

        public static List<Car> Examples()
        {
            List<Car> ListCar = new List<Car>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM samochod";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string marka = reader.GetString(1);
                string model = reader.GetString(2);
                string rok = reader.GetString(3);
                string paliwo = reader.GetString(4);
                double cenaDobowa = reader.GetDouble(5);
                ListCar.Add(new Car { ID = id, Marka = marka, Model = model, Rok = rok, Paliwo = paliwo, CenaDobowa = cenaDobowa });
                i++;
            }

            reader.Close();
            return ListCar;
        }

        public static List<Car> CarList()
        {
            List<Car> CarList = new List<Car>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM samochod";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string marka = reader.GetString(1);
                string model = reader.GetString(2);
                string rok = reader.GetString(3);
                string paliwo = reader.GetString(4);
                double cenaDobowa = reader.GetDouble(5);

                string dane = (id + " | " + marka + " | " + model + " | " + rok + " | " + paliwo + " | " + cenaDobowa);
                
                CarList.Add(new Car { ID = id, list = dane });
            }

            reader.Close();
            return CarList;
        }

        public static List<Car> CarName()
        {
            List<Car> CarName = new List<Car>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT marka FROM samochod";
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                string marka = reader.GetString(0);

                CarName.Add(new Car { Marka = marka });
            }

            reader.Close();
            return CarName;
        }

        public static List<Car> CarYear()
        {
            List<Car> CarYear = new List<Car>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT rok FROM samochod";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string rok = reader.GetString(0);

                CarYear.Add(new Car { Rok = rok });
            }

            reader.Close();
            return CarYear;
        }

        public static List<Car> CarPetrol()
        {
            List<Car> CarPetrol = new List<Car>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT paliwo FROM samochod";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string paliwo = reader.GetString(0);

                CarPetrol.Add(new Car { Paliwo = paliwo });
            }

            reader.Close();
            return CarPetrol;
        }

        public static List<Car> PricePerDay()
        {
            List<Car> PricePerDay = new List<Car>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT cenadobowa FROM samochod";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                double cena = reader.GetDouble(0);

                PricePerDay.Add(new Car { CenaDobowa = cena });
            }

            reader.Close();
            return PricePerDay;
        }

        public static List<Car> SelectCar(Car car)
        {
            List<Car> CarsSelected = new List<Car>();

            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM samochod WHERE marka=('" + car.Marka + "') AND rok>=('" + car.Rok + "') AND paliwo=('" + car.Paliwo + "') AND cenadobowa>=('" + car.CenaDobowa + "')";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string marka = reader.GetString(1);
                string model = reader.GetString(2);
                string rok = reader.GetString(3);
                string paliwo = reader.GetString(4);
                double cenaDobowa = reader.GetDouble(5);
                CarsSelected.Add(new Car { ID = id, Marka = marka, Model = model, Rok = rok, Paliwo = paliwo, CenaDobowa = cenaDobowa });
            }

            reader.Close();

            return CarsSelected;
        }

        public Car GetCar(int id)
        {
            Car GetCar = new Car();

            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM samochod WHERE id=('" + id + "')";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                GetCar.ID = reader.GetInt32(0);
                GetCar.Marka = reader.GetString(1);
                GetCar.Model = reader.GetString(2);
                GetCar.Rok = reader.GetString(3);
                GetCar.Paliwo = reader.GetString(4);
                GetCar.CenaDobowa = reader.GetDouble(5);
            }

            reader.Close();

            return GetCar;
        }

        public string AddCar(Car newCar)
        {
            i = 0;
            Car.Examples();
            ++i;

            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO samochod VALUES('" + Car.i + "','" + newCar.Marka.ToUpper() + "','" + newCar.Model.ToUpper() + "','" + newCar.Rok + "' ,'" + newCar.Paliwo.ToUpper() + "','" + newCar.CenaDobowa + "')";
            cmd.Connection = conn;
            try
            {
                cmd.ExecuteScalar();
                return "Success";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckNew(Car checkCar)
        {
            if ((checkCar.Marka != null) && (checkCar.Model != null) && (checkCar.Rok != null) && (checkCar.Paliwo != null))
            {
                int MarkaLength = checkCar.Marka.Length;
                int ModelLenght = checkCar.Model.Length;
                int RokLenght = checkCar.Rok.Length;
                int PaliwoLenght = checkCar.Paliwo.Length;

                if ((MarkaLength > 1) && (ModelLenght > 1) && (RokLenght <= 8) && (PaliwoLenght > 1) && (checkCar.CenaDobowa > 1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}