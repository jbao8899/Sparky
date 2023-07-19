using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class ProductNUnitTests
    {
        private Product product;

        [SetUp]
        public void SetUp()
        {
            // Assemble
            product = new Product();    
        }

        [Test]
        public void GetPrice_PlatinumCustomer_Gives20PercentDiscount()
        {
            // Assemble
            product.Price = 50.0;

            // Act
            double price = product.GetPrice(new Customer() { IsPlatinum = true });

            Assert.That(price, Is.EqualTo(40));

        }

        [Test]
        public void MoqAbuse_GetPrice_PlatinumCustomer_Gives20PercentDiscount()
        {
            // Don't create an interface just for Moq

            // Assemble
            product.Price = 50.0;
            Mock<ICustomer> mockCustomer = new Mock<ICustomer>();
            mockCustomer.Setup(u => u.IsPlatinum).Returns(true);

            // Act
            double price = product.GetPrice(mockCustomer.Object);

            Assert.That(price, Is.EqualTo(40));

        }
    }
}
