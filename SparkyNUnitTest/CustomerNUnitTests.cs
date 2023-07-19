using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CustomerNUnitTests
    {
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            // Assemble
            customer = new Customer();
        }

        [Test]
        public void GreetAndCombineNames_InputFirstAndLastNames_ReturnGreetingWithFullName()
        {
            // Act
            string greeting = customer.GreetAndCombineNames("Ben", "Spark");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(greeting, Is.EqualTo("Hello, Ben Spark."));
                Assert.AreEqual("Hello, Ben Spark.", greeting);
                Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark."));

                Assert.That(greeting, Does.Contain(","));
                Assert.That(greeting, Does.StartWith("Hello"));
                Assert.That(greeting, Does.EndWith("Spark."));
                Assert.That(greeting, Does.Contain("ben spark").IgnoreCase);
                Assert.That(greeting, Does.Match("Hello, [A-Z][a-z]+ [A-Z][a-z]+."));
            });
        }

        [Test]
        public void GreetMessage_NotGreeted_IsNull()
        {
            // Act
            // Nothing is here

            // Assert
            Assert.IsNull(customer.GreetMessage);
            //Assert.That(customer.GreetMessage, Is.Null); // Same
        }

        [Test]
        public void Discount_DefaultCustomer_DiscountBetween10And25()
        {
            // Already assembled in setup

            // Act
            int result = customer.Discount;

            // Assert
            Assert.That(result, Is.InRange(10, 25));
        }

        [Test]
        public void GreetAndCombineNames_EmptyLastName_SetsCorrectGreeting()
        {
            // Act
            customer.GreetAndCombineNames("Ben", "");

            Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben ."));
        }

        [Test]
        public void GreetAndCombineNames_EmptyFirstName_ThrowsExceptionWithCorrectMessage()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("   ", "Spark"));
            Assert.That(exceptionDetails.Message, Is.EqualTo("First name should not be null, empty, or only contain whitespace."));

            Assert.That(() => customer.GreetAndCombineNames(null, "Brown"),
                Throws.ArgumentException);
        }

        [Test]
        public void GreetAndCombineNames_EmptyFirstName_ThrowsException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("   ", "Spark"));

            Assert.That(() => customer.GreetAndCombineNames("", "Brown"),
                Throws.ArgumentException);
        }

        [Test]
        public void GetCustomerDetails_LessThan100Orders_ReturnsBasicCustomer()
        {
            // Assemble
            customer.OrderTotal = 99;

            // Act
            CustomerType customerType = customer.GetCustomerDetails();

            // Assert
            Assert.That(customerType, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void GetCustomerDetails_MoreThan100Orders_ReturnsPlatinumCustomer()
        {
            // Assemble
            customer.OrderTotal = 101;

            // Act
            CustomerType customerType = customer.GetCustomerDetails();

            // Assert
            Assert.That(customerType, Is.TypeOf<PlatinumCustomer>());
        }
    }
}
