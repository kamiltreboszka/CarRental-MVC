using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Rent
    {
        [Display(Name = "ID wypożyczenia")]
        public int IdWypozyczenia { get; set; }
        [Display(Name = "ID samochodu")]
        public int IdSamochodu { get; set; }
        [Display(Name = "ID osoby")]
        public int IdOsoby { get; set; }
        public double Kwota { get; set; }
        [Display(Name = "Wypożyczenie od"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataWypozyczenia { get; set; }
        [Display(Name = "Wypożyczenie do"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataOddania { get; set; }

        public static int i = 0;

        public static List<Rent> Examples()
        {
            List<Rent> ListRent = new List<Rent>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM wypozyczenie";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idWypozyczneia = reader.GetInt32(0);
                int idSamochodu = reader.GetInt32(1);
                int idOsoby = reader.GetInt32(2);
                double kwota = reader.GetDouble(3);
                DateTime wypozyczenie = reader.GetDateTime(4);
                DateTime oddanie = reader.GetDateTime(5);
                ListRent.Add(new Rent { IdWypozyczenia = idWypozyczneia, IdSamochodu = idSamochodu, IdOsoby = idOsoby, Kwota = kwota, DataWypozyczenia = wypozyczenie, DataOddania = oddanie});
                i++;
            }

            reader.Close();
            return ListRent;
        }

        public static List<Rent> GetRent(int ID)
        {
            List<Rent> RentList = new List<Rent>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM wypozyczenie where idosoby=('" + ID + "')";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idWypozyczneia = reader.GetInt32(0);
                int idSamochodu = reader.GetInt32(1);
                int idOsoby = reader.GetInt32(2);
                double kwota = reader.GetDouble(3);
                DateTime wypozyczenie = reader.GetDateTime(4);
                DateTime oddanie = reader.GetDateTime(5);
                RentList.Add(new Rent { IdWypozyczenia = idWypozyczneia, IdSamochodu = idSamochodu, IdOsoby = idOsoby, Kwota = kwota, DataWypozyczenia = wypozyczenie, DataOddania = oddanie });
            }

            reader.Close();
            return RentList;
        }

        public string AddRent(Rent newRent, Car newCar, Users newUsers)
        {
            i = 0;
            Rent.Examples();
            ++i;

            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            
            var oddanie = newRent.DataOddania;
            var wypozyczenie = newRent.DataWypozyczenia;

            var iloscDni =  ((oddanie-wypozyczenie).TotalDays)+1;

            newRent.IdWypozyczenia = i;
            newRent.Kwota = (newCar.CenaDobowa * iloscDni);
            newRent.IdOsoby = newUsers.ID;

            cmd.CommandText = "INSERT INTO wypozyczenie VALUES('" + Rent.i + "','" + newRent.IdSamochodu + "','" + newRent.IdOsoby + "','" + newRent.Kwota + "' ,'" + newRent.DataWypozyczenia.Date + "','" + newRent.DataOddania.Date + "')";
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

        public bool CheckRent(int ID, DateTime wypozyczenie, DateTime oddanie)
        {
            List<Rent> CheckRentList = new List<Rent>();
            var conn = Database.GetConnection(typeof(NpgsqlConnection));
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM wypozyczenie where (idsamochodu=('"+ ID +"') AND (datawypozyczenia between '"+ wypozyczenie +"' AND '"+ oddanie + "') AND (dataoddania between '" + wypozyczenie + "' AND '" + oddanie + "')) OR (idsamochodu=('" + ID + "') AND dataoddania>=('" + wypozyczenie+"'))";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                {
                    int idWypozyczneia = reader.GetInt32(0);
                    int idSamochodu = reader.GetInt32(1);
                    int idOsoby = reader.GetInt32(2);
                    double kwota = reader.GetDouble(3);
                    DateTime _wypozyczenie = reader.GetDateTime(4);
                    DateTime _oddanie = reader.GetDateTime(5);
                    CheckRentList.Add(new Rent { IdWypozyczenia = idWypozyczneia, IdSamochodu = idSamochodu, IdOsoby = idOsoby, Kwota = kwota, DataWypozyczenia = _wypozyczenie, DataOddania = _oddanie });
                }
            }
            reader.Close();

            if(CheckRentList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool CheckNew(Rent checkRent, Car checkCar, Users checkUser)
        {
            if (checkRent.CheckRent(checkCar.ID, checkRent.DataWypozyczenia, checkRent.DataOddania))
            {
                if (!(checkRent.IdSamochodu.Equals(null)) && !(checkUser.ID.Equals(null)) && !(checkCar.CenaDobowa.Equals(null)) && (checkRent.DataWypozyczenia.Date != null) && (checkRent.DataOddania.Date != null))
                {
                    int IDauto = checkRent.IdSamochodu;
                    int IDuser = checkUser.ID;
                    double cena = checkCar.CenaDobowa;
                    DateTime Data = DateTime.Today;

                    if ((IDauto >= 1) && (IDuser >= 1) && (cena > 1) && (checkRent.DataWypozyczenia >= Data) && (checkRent.DataOddania >= Data))
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
            else
            {
                return false;
            }
        }
    }
}