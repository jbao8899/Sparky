using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class CalculatorXUnitTests
    {
        [Fact]
        public void AddIntegers_InputTwoInt_GetCorrectAddition()
        {
            Calculator calculator = new Calculator();

            // Act
            int result = calculator.AddIntegers(10, 20);

            // Assert
            Assert.Equal(30, result);
        }

        [Theory]
        [InlineData(int.MinValue + 1)]
        [InlineData(-101)]
        [InlineData(-5)]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(237)]
        [InlineData(int.MaxValue)]
        public void IsOddNumber_InputOddInt_ReturnTrue(int a)
        {
            Calculator calculator = new Calculator();

            // Act
            bool isOdd = calculator.IsOddNumber(a);

            // Assert
            Assert.True(isOdd);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-102)]
        [InlineData(-6)]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(8)]
        [InlineData(338)]
        [InlineData(int.MaxValue - 1)]
        public void IsOddNumber_InputEvenInt_ReturnFalse(int a)
        {
            Calculator calculator = new Calculator();

            // Act
            bool isOdd = calculator.IsOddNumber(a);

            // Assert
            Assert.False(isOdd);
        }

        // If logic is different, keep these separate
        // If logic is similar (as it is here), combine the test cases
        [Theory]
        [InlineData(int.MinValue + 1, true)]
        [InlineData(-101, true)]
        [InlineData(-5, true)]
        [InlineData(-1, true)]
        [InlineData(1, true)]
        [InlineData(7, true)]
        [InlineData(237, true)]
        [InlineData(int.MaxValue, true)]
        [InlineData(int.MinValue, false)]
        [InlineData(-102, false)]
        [InlineData(-6, false)]
        [InlineData(0, false)]
        [InlineData(2, false)]
        [InlineData(8, false)]
        [InlineData(338, false)]
        [InlineData(int.MaxValue - 1, false)]
        public void IsOddNumber_InputInt_ReturnTrueIfOddAndFalseAsEven(int a, bool expectedResult)
        {
            Calculator calculator = new Calculator();

            // Act
            bool isOdd = calculator.IsOddNumber(a);

            // Assert
            Assert.Equal(expectedResult, isOdd);
        }


        [Theory]
        [InlineData(1.5, 2.3, 3.8)]
        [InlineData(113.9, 0.0, 113.9)]
        [InlineData(5.2, -8.3, -3.1)]
        [InlineData(0.0, 2.3, 2.3)]
        [InlineData(0.0, 0.0, 0.0)]
        [InlineData(0.0, -124389.323, -124389.323)]
        [InlineData(-1.5, 2.3, 0.8)]
        [InlineData(-113.9, 0.0, -113.9)]
        [InlineData(-5.2, -8.3, -13.5)]
        public void AddDoubles_InputTwoDouble_GetCorrectAddition(double a, double b, double expectedResult)
        {
            Calculator calculator = new Calculator();

            // Act
            double result = calculator.AddDoubles(a, b);

            // Assert
            Assert.Equal(expectedResult, result, 3);
        }

        [Fact]
        public void GetOddRange_InputMinAndMaxInts_ReturnsCorrectListOfOddIntegersBetweenMinAndMax()
        {
            // Assemble
            Calculator calculator = new Calculator();
            List<int> expectedOddRange = new List<int>() { 5, 7, 9 };

            // Act
            List<int> result = calculator.GetOddRange(4, 9);

            Assert.Equal(expectedOddRange, result);

            Assert.Contains(7, result);

            Assert.NotEmpty(result);

            Assert.Equal(3, result.Count);

            Assert.DoesNotContain(6, result);

            // The XUnit equivalent of
            // Assert.That(result, Is.Ordered.Ascending);
            // is to see if the result is unaffected by sorting
            Assert.Equal(result.OrderBy(u => u), result);

            //Assert.That(result, Is.Unique); // No direct equivalent in XUnit
        }
    }
}
