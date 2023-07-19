using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class FiboNUnitTests
    {
        private Fibo fibo;
        
        [SetUp]
        public void SetUp()
        {
            // Assemble
            fibo = new Fibo();
        }

        [Test]
        public void FiboGetFiboSeries_Range1_ReturnsListContainingOnly0()
        {
            // Assemble
            fibo.Range = 1;

            // Act
            List<int> fiboSeries = fibo.GetFiboSeries();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(fiboSeries, Is.Not.Empty);
                Assert.That(fiboSeries, Is.Ordered.Ascending);
                Assert.That(fiboSeries, Is.EquivalentTo(new List<int> { 0 } ));
            });
        }

        [Test]
        public void FiboGetFiboSeries_Range6_ReturnsCorrectSeries()
        {
            // Assemble
            fibo.Range = 6;

            // Act
            List<int> fiboSeries = fibo.GetFiboSeries();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(fiboSeries, Does.Contain(3));
                Assert.That(fiboSeries.Count, Is.EqualTo(6));
                Assert.That(fiboSeries, Has.No.Member(4));
                Assert.That(fiboSeries, Is.EquivalentTo(new List<int> { 0, 1, 1, 2, 3, 5 } ));
            });
        }
    }
}
