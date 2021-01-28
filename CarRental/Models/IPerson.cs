using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public interface IPerson
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        string Pesel { get; set; }
        string PhoneNumber { get; set; }

        void UserCheck(string userDetails);
    }
}
