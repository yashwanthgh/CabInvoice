using CabInvoice;
using System.Net.Http.Headers;

namespace CabInvoiceTest
{
    public class InvoiceGeneratorTest
    {
       static  List<IRide> ride1 = new List<IRide>() {
            new Ride (10,30),
            new Ride (15,45)
            };
       static  List<IRide> ride2 = new List<IRide>() {
            new Ride (5,10),
            new Ride (30,30)
            };
        [SetUp]
        public void Setup()
        {

        }

        //Tests multiple test cases for calculating the fare given distance and time.
        [TestCase(10, 30, 130)]
        [TestCase(30, 10, 310)]
        [TestCase(15, 15, 165)]
        [TestCase(45, 12, 462)]
        public void FareCalculator_ReturnTheFareValue(int distance, int time, double expected)
        {
            IInvoiceGenerator generator = new InvoiceGenerator();
            IRide ride = new Ride(distance, time);
            double actual = generator.CalculateFare(ride);
            Assert.That(actual, Is.EqualTo(expected));
        }
        static object[] RideCases =
   {
        new object[] { ride1, 325 },
        new object[] { ride2, 390 }
    };
        [TestCaseSource (nameof(RideCases))]
        public void TotalFareCalculator_ReturnTheTolatFare (List<IRide> rides , double expected )
        {
            IInvoiceGenerator generator = new InvoiceGenerator();

            double actual = generator.CalculateTotalFare(rides);

            Assert.AreEqual(expected, actual);
        }
    }
}