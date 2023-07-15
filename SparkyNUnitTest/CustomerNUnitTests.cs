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
            Assert.That(greeting, Is.EqualTo("Hello, Ben Spark."));
            //Assert.AreEqual("Hello, Ben Spark.", greeting); // Same as above
            Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark."));

            Assert.That(greeting, Does.Contain(","));
            Assert.That(greeting, Does.StartWith("Hello"));
            Assert.That(greeting, Does.EndWith("Spark."));
            Assert.That(greeting, Does.Contain("ben spark").IgnoreCase);
            Assert.That(greeting, Does.Match("Hello, [A-Z][a-z]+ [A-Z][a-z]+."));
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
    }
}
