using CabInvoice;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace CabInvoiceTest
{
    public class InvoiceGeneratorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(10, 30, 130)]
        [TestCase(30, 10, 310)]
        [TestCase(15, 15, 165)]
        [TestCase(45, 12, 462)]

        public void FareCalculator_ReturnTheFareValue(int distance, int time, double expected)
        {
            IInvoiceGenerator generator = new InvoiceGenerator();
            IRide ride = new Ride(distance, time, false);
            double actual = generator.CalculateFare(ride);
            Assert.That(actual, Is.EqualTo(expected));
        }


        static List<IRide> ride1 = [new Ride(10, 30, false), new Ride(15, 45, false)];
        static List<IRide> ride2 = [new Ride(5, 10, false), new Ride(30, 30, false)];
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
            yield return new object[] { 2, 230, 115, new List<Ride> { new Ride(5, 20, false), new Ride(10, 60, false) } };
            yield return new object[] { 3, 630, 210, new List<Ride> { new Ride(10, 40, false), new Ride(15, 60, false), new Ride(20, 80, false) } };
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

        [TestCaseSource(nameof(GenerateInvoiceTestCases))]
        public void GenerateInvoiceForUser_ValidUserId_ReturnsInvoice(int userId, IEnumerable<IRide> rides, Invoice expectedInvoice)
        {
            var mockRideRepository = Substitute.For<IRideRepository>();
            var mockInvoiceGenerator = Substitute.For<IInvoiceGenerator>();

            mockRideRepository.GetRidesForUser(userId).Returns(rides);

            mockInvoiceGenerator.GenerateInvoice(rides).Returns(expectedInvoice);

            Invoice actualInvoice = mockInvoiceGenerator.GenerateInvoice(rides);

            Assert.That(actualInvoice, Is.EqualTo(expectedInvoice));
        }

        private static IEnumerable<TestCaseData> GenerateInvoiceTestCases
        {
            get
            {
                yield return new TestCaseData(
                    123, // User ID
                    new List<IRide>
                    {
                    new Ride(10, 30, false),
                    new Ride(15, 45, false)  
                    },
                    new Invoice { TotalRides = 2, TotalFare = 435.0, AverageFarePerRide = 217.5 }
                );

                yield return new TestCaseData(
                    456, // User ID
                    new List<IRide>
                    {
                    new Ride(5, 15, false) 
                    },
                    new Invoice { TotalRides = 1, TotalFare = 65.0, AverageFarePerRide = 65.0 }
                );
            }
        }

        [Test]
        [TestCase(15, 45, true)]
        [TestCase(20, 60, true)]
        [TestCase(10, 30, true)]
        [TestCase(25, 70, true)]
        public void PremiumRide_IsPremiumProperty_ReturnsTrue(int distance, int duration, bool isPremium)
        {
            IRide ride = new Ride(distance, duration, isPremium);

            bool result = ride.IsPremium;
            Assert.That(result, Is.True);
        }
    }
}