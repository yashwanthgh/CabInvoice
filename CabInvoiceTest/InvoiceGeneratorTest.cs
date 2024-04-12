using CabInvoice;

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
            IRide ride = new Ride(distance, time);
            double actual = generator.CalculateFare(ride);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}