using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public interface ICarTesting
    {
        int ID { get; set; }
        string Marka { get; set; }
        string Model { get; set; }
        string Rok { get; set; }
        string Paliwo { get; set; }
        double CenaDobowa { get; set; }

        void CarTesting(string CarDetails);
    }
}
