using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Worker
    {
        public int ID { get; set; }
        [Display(Name = "IMIE")]
        public string Name { get; set; }
        [Display(Name = "NAZWISKO")]
        public string Surname { get; set; }
        [Display(Name = "HASŁO")]
        public string Password { get; set; }
        [Display(Name = "EMAIL")]
        public string Email { get; set; }
        [Display(Name = "PESEL")]
        public string Pesel { get; set; }
        [Display(Name = "NUMER TELEFONU")]
        public string PhoneNumber { get; set; }

        public static int i = 0;

        public static List<Worker> Examples()
        {
            List<Worker> ListWork = new List<Worker>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM pracownik";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string surname = reader.GetString(2);
                string password = reader.GetString(3);
                string email = reader.GetString(4);
                string pesel = reader.GetString(5);
                string phoneNumber = reader.GetString(6);
                ListWork.Add(new Worker { ID = id, Name = name, Surname = surname, Password = password, Email = email, Pesel = pesel, PhoneNumber = phoneNumber });
                i++;
            }

            reader.Close();
            return ListWork;
        }

        public bool Verified
        {
            get
            {
                var con = Database.GetConnection(typeof(NpgsqlConnection));
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT checkworker('" + Email + "','" + Password + "')";

                return (bool)cmd.ExecuteScalar();
            }
        }

        public Worker GetUser()
        {
            Worker GetUser = new Worker();

            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM pracownik WHERE haslo=('" + Password + "')";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                GetUser.ID = reader.GetInt32(0);
                GetUser.Name = reader.GetString(1);
                GetUser.Surname = reader.GetString(2);
                GetUser.Password = reader.GetString(3);
                GetUser.Email = reader.GetString(4);
                GetUser.Pesel = reader.GetString(5);
                GetUser.PhoneNumber = reader.GetString(6);

            }

            reader.Close();

            return GetUser;
        }

        public string AddWorker(Worker newWorker)
        {
            i = 0;
            Worker.Examples();
            ++i;

            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO pracownik VALUES('" + Worker.i + "','" + newWorker.Name + "','" + newWorker.Surname + "','" + newWorker.Password + "' ,'" + newWorker.Email + "','" + newWorker.Pesel + "','" + newWorker.PhoneNumber + "')";
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

        public bool CheckNew(Worker checkWorker)
        {
            int wiek = checkWorker.ObliczWiek(checkWorker.Pesel);

            if ((checkWorker.Name != null) && (checkWorker.Surname != null) && (checkWorker.Password != null) && (checkWorker.Pesel != null) && (checkWorker.PhoneNumber != null) && (wiek >= 18) && checkWorker.EmailCheck(checkWorker.Email))
            {
                int NameLength = checkWorker.Name.Length;
                int SurnameLenght = checkWorker.Surname.Length;
                int PasswordLenght = checkWorker.Password.Length;
                int EmailLenght = checkWorker.Password.Length;
                int PeselLenght = checkWorker.Pesel.Length;
                int PhoneLenght = checkWorker.PhoneNumber.Length;

                if ((NameLength > 1) && (SurnameLenght > 1) && (PasswordLenght <= 8) && (EmailLenght > 1) && (PeselLenght == 11) && (PhoneLenght == 9))
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

        public int ObliczWiek(string pesel)
        {
            DateTime RokUrodzenia;

            int rok1 = Convert.ToInt32(pesel.Substring(0, 1));
            int rok2 = Convert.ToInt32(pesel.Substring(1, 1));
            int msc1 = Convert.ToInt32(pesel.Substring(2, 1));
            int msc2 = Convert.ToInt32(pesel.Substring(3, 1));
            int dzien1 = Convert.ToInt32(pesel.Substring(4, 1));
            int dzien2 = Convert.ToInt32(pesel.Substring(5, 1));

            int Dzien = 0, Miesiac = 0, Rok = 0;
            if (msc1 == 2)
            {
                RokUrodzenia = new DateTime(2000, 1, 1);
                Dzien = dzien1 * 10 + dzien2 - 1;
                Miesiac = (msc1 * 10 + msc2 - 1) - 20;
                Rok = rok1 * 10 + rok2;
            }
            else
            {
                RokUrodzenia = new DateTime(1900, 1, 1);
                Dzien = dzien1 * 10 + dzien2 - 1;
                Miesiac = msc1 * 10 + msc2 - 1;
                Rok = rok1 * 10 + rok2;
            }

            RokUrodzenia = RokUrodzenia.AddDays(Dzien);
            RokUrodzenia = RokUrodzenia.AddMonths(Miesiac);
            RokUrodzenia = RokUrodzenia.AddYears(Rok);

            DateTime ObecnyCzas = DateTime.UtcNow.ToLocalTime();
            int Wiek = (ObecnyCzas.Year - RokUrodzenia.Year);

            return Wiek;
        }

        public bool EmailCheck(string email)
        {
            List<Worker> checkList = new List<Worker>();

            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM pracownik WHERE email=('" + email + "')";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int checkid = reader.GetInt32(0);
                string checkEmail = reader.GetString(4);
                checkList.Add(new Worker { ID = checkid, Email = checkEmail });
            }

            reader.Close();

            if (checkList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}