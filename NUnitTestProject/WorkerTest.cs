using NUnit.Framework;
using CarRental.Models;

namespace NUnitTestProject
{
    [TestFixture(typeof(Worker))]
    public class WorkerTests<_Worker> where _Worker : IPerson, new()
    {
        IPerson tested;

        static object[] userTestSource = new object[]
        {
            new object[]
            {
                new _Worker{ Name = "Kamil", Surname = "Kamil", Password = "KamTrePr", Email = "kamil.treboszka@gmail.com", Pesel = "94062404132", PhoneNumber = "753334508"},
                "Kamil\nKamil\nKamTrePr\nkamil.treboszka@gmail.com\n94062404132\n753334508"
            },
            new object[]
            {
                new _Worker{ Name = "Jarek", Surname = "Kowalski", Password = "JarKowPr", Email = "jarek.kowalski@gmail.com", Pesel = "88112504132", PhoneNumber = "883404623"},
                "Jarek\nKowalski\nJarKowPr\njarek.kowalski@gmail.com\n88112504132\n883404623 "
            },
            new object[]
            {
                new _Worker{ Name = "Łukasz", Surname = "Przybysz", Password = "LukPrzPr", Email = "lukasz.przybysz@gmail.com", Pesel = "89030504132", PhoneNumber = "802552961"},
                "Łukasz\nPrzybysz\nLukPrzPr\nlukasz.przybysz@gmail.com\n89030504132\n802552961"
            }
        };

        [SetUp]
        public void Setup()
        {
            tested = new _Worker();
        }

        [TestCaseSource("userTestSource")]
        public void UserTest(_Worker references, string workerDetails)
        {
            tested.UserCheck(workerDetails);
            Assert.AreEqual(references.Name, tested.Name);
            Assert.AreEqual(references.Surname, tested.Surname);
            Assert.AreEqual(references.Password, tested.Password);
            Assert.AreEqual(references.Email, tested.Email);
            Assert.AreEqual(references.Pesel, tested.Pesel);
            Assert.AreEqual(references.PhoneNumber, tested.PhoneNumber);
        }
    }
}