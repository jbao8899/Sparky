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
        private Calculator calculator;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            calculator = new Calculator();
        }

        [Test]
        public void AddIntegers_InputTwoInt_GetCorrectAddition()
        {
            // Act
            int result = calculator.AddIntegers(10, 20);

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
            // Act
            bool isOdd = calculator.IsOddNumber(a);

            // Assert
            Assert.IsFalse(isOdd);
        }

        // If logic is different, keep these separate
        // If logic is similar (as it is here), combine the test cases
        [Test]
        [TestCase(int.MinValue + 1, ExpectedResult = true)]
        [TestCase(-101, ExpectedResult = true)]
        [TestCase(-5, ExpectedResult = true)]
        [TestCase(-1, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(7, ExpectedResult = true)]
        [TestCase(237, ExpectedResult = true)]
        [TestCase(int.MaxValue, ExpectedResult = true)]
        [TestCase(int.MinValue, ExpectedResult = false)]
        [TestCase(-102, ExpectedResult = false)]
        [TestCase(-6, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(8, ExpectedResult = false)]
        [TestCase(338, ExpectedResult = false)]
        [TestCase(int.MaxValue - 1, ExpectedResult = false)]
        public bool IsOddNumber_InputInt_ReturnTrueIfOddAndFalseAsEven(int a)
        {
            // Act
            bool isOdd = calculator.IsOddNumber(a);

            // Assert
            return isOdd;
        }


        [Test]
        [TestCase(1.5, 2.3, ExpectedResult = 3.8)]
        [TestCase(113.9, 0.0, ExpectedResult = 113.9)]
        [TestCase(5.2, -8.3, ExpectedResult = -3.1)]
        [TestCase(0.0, 2.3, ExpectedResult = 2.3)]
        [TestCase(0.0, 0.0, ExpectedResult = 0.0)]
        [TestCase(0.0, -124389.323, ExpectedResult = -124389.323)]
        [TestCase(-1.5, 2.3, ExpectedResult = 0.8)]
        [TestCase(-113.9, 0.0, ExpectedResult = -113.9)]
        [TestCase(-5.2, -8.3, ExpectedResult = -13.5)]

        [DefaultFloatingPointTolerance(0.001)]
        public double AddDoubles_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            // Act
            double result = calculator.AddDoubles(a, b);

            // Assert
            return result;
        }

        [Test]
        public void GetOddRange_InputMinAndMaxInts_ReturnsCorrectListOfOddIntegersBetweenMinAndMax()
        {
            // More assemble
            List<int> expectedOddRange = new List<int>() { 5, 7, 9 };

            // Act
            List<int> result = calculator.GetOddRange(4, 9);

            // Assert
            // Assert.Multiple will run all of these things inside,
            // instead of stopping at the first failed asset
            Assert.Multiple(() => 
            {
                Assert.That(result, Is.EquivalentTo(expectedOddRange));
                Assert.AreEqual(expectedOddRange, result); // equivalent

                Assert.That(result, Does.Contain(7));
                Assert.Contains(7, expectedOddRange);

                Assert.That(result, Is.Not.Empty);

                Assert.That(result.Count, Is.EqualTo(3));

                Assert.That(result, Has.No.Member(6));

                Assert.That(result, Is.Ordered.Ascending);

                Assert.That(result, Is.Unique); //All elements are unique
            });
        }
    }
}
