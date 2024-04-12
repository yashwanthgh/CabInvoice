using CabInvoice;
using System.Net.Http.Headers;

namespace CabInvoiceTest
{
    public class InvoiceGeneratorTest
    {
<<<<<<< HEAD
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
=======
            [SetUp]
            public void Setup()
            {
>>>>>>> 3c4b9efb9550f59f252b9d4d8cc25a739a238603

            }

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


            static List<IRide> ride1 = [new Ride(10, 30), new Ride(15, 45)];
            static List<IRide> ride2 = [new Ride(5, 10), new Ride(30, 30)];
            static object[] RideCases = [new object[] { ride1, 325 }, new object[] { ride2, 390 }];

            [TestCaseSource(nameof(RideCases))]
            public void TotalFareCalculator_ReturnTheTolatFare(List<IRide> rides, double expected)
            {
                IInvoiceGenerator generator = new InvoiceGenerator();

                double actual = generator.CalculateTotalFare(rides);

                Assert.That(actual, Is.EqualTo(expected));
            }

            private static IEnumerable<object[]> GetTestCases()
            {
                yield return new object[] { 2, 230, 115, new List<Ride> { new Ride(5, 20), new Ride(10, 60) } };
                yield return new object[] { 3, 630, 210, new List<Ride> { new Ride(10, 40), new Ride(15, 60), new Ride(20, 80) } };
            }

            [Test]
            [TestCaseSource(nameof(GetTestCases))]
            public void GenerateInvoice_ForMultipleRides(int expectedTotalRides, double expectedTotalFare, double expectedAverage, IEnumerable<Ride> rides)
            {
                IInvoiceGenerator generator = new InvoiceGenerator();

                Invoice actual = generator.GenerateInvoice(rides);
                Assert.That(actual.TotalRides, Is.EqualTo(expectedTotalRides));
                Assert.That(actual.TotalFare, Is.EqualTo(expectedTotalFare));
                Assert.That(actual.AverageFarePerRide, Is.EqualTo(expectedAverage));
            }
        }
    
}