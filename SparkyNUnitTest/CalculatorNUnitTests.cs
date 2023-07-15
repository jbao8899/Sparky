using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            int result = calculator.AddNumbers(10, 20);

            // Assert
            Assert.AreEqual(30, result);
        }

        [Test]
        [TestCase(int.MinValue + 1)]
        [TestCase(-101)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(7)]
        [TestCase(237)]
        [TestCase(int.MaxValue)]
        public void IsOddNumber_InputOddInt_ReturnTrue(int a)
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            bool isOdd = calculator.IsOddNumber(a);

            // Assert
            Assert.IsTrue(isOdd);
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(-102)]
        [TestCase(-6)]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(8)]
        [TestCase(338)]
        [TestCase(int.MaxValue - 1)]
        public void IsOddNumber_InputEvenInt_ReturnFalse(int a)
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            bool isOdd = calculator.IsOddNumber(a);

            // Assert
            Assert.IsFalse(isOdd);
        }
    }
}
