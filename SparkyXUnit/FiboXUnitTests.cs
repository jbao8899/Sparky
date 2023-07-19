using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class FiboXUnitTests
    {
        private Fibo fibo;

        public FiboXUnitTests()
        {
            // Assemble
            fibo = new Fibo();
        }

        [Fact]
        public void FiboGetFiboSeries_Range1_ReturnsListContainingOnly0()
        {
            // Assemble
            fibo.Range = 1;

            // Act
            List<int> fiboSeries = fibo.GetFiboSeries();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotEmpty(fiboSeries);
                Assert.Equal(fiboSeries.OrderBy(u => u), fiboSeries);
                Assert.Equal(new List<int> { 0 }, fiboSeries);
            });
        }

        [Fact]
        public void FiboGetFiboSeries_Range6_ReturnsCorrectSeries()
        {
            // Assemble
            fibo.Range = 6;

            // Act
            List<int> fiboSeries = fibo.GetFiboSeries();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Contains(3, fiboSeries);
                Assert.Equal(6, fiboSeries.Count);
                Assert.DoesNotContain(4, fiboSeries);
                Assert.Equal(new List<int> { 0, 1, 1, 2, 3, 5 }, fiboSeries);
            });
        }
    }
}
