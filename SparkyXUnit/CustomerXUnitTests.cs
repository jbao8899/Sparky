using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class CustomerXUnitTests
    {
        private Customer customer;

        public CustomerXUnitTests()
        {
            // Assemble
            customer = new Customer();
        }

        [Fact]
        public void GreetAndCombineNames_InputFirstAndLastNames_ReturnGreetingWithFullName()
        {
            // Act
            string greeting = customer.GreetAndCombineNames("Ben", "Spark");

            // Assert
            Assert.Equal("Hello, Ben Spark.", greeting);
            Assert.Equal("Hello, Ben Spark.", customer.GreetMessage);

            Assert.Contains(",", greeting);
            Assert.StartsWith("Hello", greeting);
            Assert.EndsWith("Spark.", greeting);
            Assert.Contains("ben spark", greeting.ToLower());
            Assert.Matches("Hello, [A-Z][a-z]+ [A-Z][a-z]+.", greeting);
        }

        [Fact]
        public void GreetMessage_NotGreeted_IsNull()
        {
            // Act
            // Nothing is here

            // Assert
            Assert.Null(customer.GreetMessage);
        }

        [Fact]
        public void Discount_DefaultCustomer_DiscountBetween10And25()
        {
            // Already assembled in setup

            // Act
            int result = customer.Discount;

            // Assert
            Assert.InRange(result, 10, 25);
        }

        [Fact]
        public void GreetAndCombineNames_EmptyLastName_SetsCorrectGreeting()
        {
            // Act
            customer.GreetAndCombineNames("Ben", "");

            Assert.Equal("Hello, Ben .", customer.GreetMessage);
        }

        [Fact]
        public void GreetAndCombineNames_EmptyFirstName_ThrowsExceptionWithCorrectMessage()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("   ", "Spark"));
            Assert.Equal("First name should not be null, empty, or only contain whitespace.", exceptionDetails.Message);

            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames(null, "Brown"));
        }

        [Fact]
        public void GreetAndCombineNames_EmptyFirstName_ThrowsException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("   ", "Spark"));

            Assert.Throws< ArgumentException>(() => customer.GreetAndCombineNames("", "Brown"));
        }

        [Fact]
        public void GetCustomerDetails_LessThan100Orders_ReturnsBasicCustomer()
        {
            // Assemble
            customer.OrderTotal = 99;

            // Act
            CustomerType customerType = customer.GetCustomerDetails();

            // Assert
            Assert.IsType<BasicCustomer>(customerType);
        }

        [Fact]
        public void GetCustomerDetails_MoreThan100Orders_ReturnsPlatinumCustomer()
        {
            // Assemble
            customer.OrderTotal = 101;

            // Act
            CustomerType customerType = customer.GetCustomerDetails();

            // Assert
            Assert.IsType<PlatinumCustomer>(customerType);
        }
    }
}
