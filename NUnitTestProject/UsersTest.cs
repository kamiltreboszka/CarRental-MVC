using NUnit.Framework;
using CarRental.Models;

namespace NUnitTestProject
{
    [TestFixture(typeof(Users))]
    public class UsersTests<_Users> where _Users : IPerson, new()
    {
        IPerson tested;

        static object[] userTestSource = new object[]
        {
           new object[]
            {
                new _Users{ Name = "Marek", Surname = "Janicki", Password = "MarJAnBf", Email = "marek.janicki@gmail.com", Pesel = "94062404132", PhoneNumber = "753334508"},
                "Marek\nJanicki\nMarJAnBf\nmarek.janicki@gmail.com\n94062404132\n753334508"
            },
            new object[]
            {
                new _Users{ Name = "Jan", Surname = "Kowalski", Password = "JanKOwS", Email = "jan.kowalski@gmail.com", Pesel = "88112504132", PhoneNumber = "883404623"},
                "Jan\nKowalski\nJanKOwS\njan.kowalski@gmail.com\n88112504132\n883404623 "
            },
            new object[]
            {
                new _Users{ Name = "Daniel", Surname = "Sikorski", Password = "DanSIkEO", Email = "daniel.sikorski@gmail.com", Pesel = "89030504132", PhoneNumber = "802552961"},
                "Daniel\nSikorski\nDanSIkEO\ndaniel.sikorski@gmail.com\n89030504132\n802552961"
            }
        };

        [SetUp]
        public void Setup()
        {
            tested = new _Users();
        }

        [TestCaseSource("userTestSource")]
        public void UserTest(_Users references, string userDetails)
        {
            tested.UserCheck(userDetails);
            Assert.AreEqual(references.Name, tested.Name);
            Assert.AreEqual(references.Surname, tested.Surname);
            Assert.AreEqual(references.Password, tested.Password);
            Assert.AreEqual(references.Email, tested.Email);
            Assert.AreEqual(references.Pesel, tested.Pesel);
            Assert.AreEqual(references.PhoneNumber, tested.PhoneNumber);
        }
    }
}
