using NUnit.Framework;
using CarRental.Models;

namespace NUnitTestProject
{
    [TestFixture(typeof(Car))]
    public class CarTests<CarTesting> where CarTesting: ICarTesting, new()
    {
        ICarTesting tested;

        static object[] CarTestingTestsSource = new object[]
        {
            new object[]
            {
                new CarTesting{ Marka = "Opel", Model = "Astra", Rok = "2019", Paliwo = "Diesel", CenaDobowa = 150 },
                "Opel\nAstra\n2019\nDiesel\n150"
            },
            new object[]
            {
                new CarTesting{ Marka = "Toyota", Model = "Rav", Rok = "2020", Paliwo = "Diesel", CenaDobowa = 100 },
                "Toyota\nRav\n2020\nDiesel\n100"
            },
            new object[]
            {
                new CarTesting{ Marka = "Mercedes", Model = "Cklasa", Rok = "2020", Paliwo = "Benzyna", CenaDobowa = 200 },
                "Mercedes\nCklasa\n2020\nBenzyna\n200"
            }
        };

        [SetUp]
        public void Setup()
        {
            tested = new CarTesting();
        }

        [TestCaseSource("CarTestingTestsSource")]
        public void CarTestingTest(CarTesting reference, string CarDetails)
        {
            tested.CarTesting(CarDetails);
            Assert.AreEqual(reference.Marka, tested.Marka);
            Assert.AreEqual(reference.Model, tested.Model);
            Assert.AreEqual(reference.Rok, tested.Rok);
            Assert.AreEqual(reference.Paliwo, tested.Paliwo);
            Assert.AreEqual(reference.CenaDobowa, tested.CenaDobowa);
        }
    }
}