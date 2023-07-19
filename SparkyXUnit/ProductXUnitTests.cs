using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class ProductXUnitTests
    {
        private Product product;

        public ProductXUnitTests()
        {
            // Assemble
            product = new Product();
        }

        [Fact]
        public void GetPrice_PlatinumCustomer_Gives20PercentDiscount()
        {
            // Assemble
            product.Price = 50.0;

            // Act
            double price = product.GetPrice(new Customer() { IsPlatinum = true });

            Assert.Equal(40, price);

        }

        [Fact]
        public void MoqAbuse_GetPrice_PlatinumCustomer_Gives20PercentDiscount()
        {
            // Don't create an interface just for Moq

            // Assemble
            product.Price = 50.0;
            Mock<ICustomer> mockCustomer = new Mock<ICustomer>();
            mockCustomer.Setup(u => u.IsPlatinum).Returns(true);

            // Act
            double price = product.GetPrice(mockCustomer.Object);

            Assert.Equal(40, price);

        }
    }
}
