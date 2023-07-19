using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{

    [TestFixture]
    public class GradingCalculatorNUnitTests
    {
        private GradingCalculator gradingCalculator;

        [SetUp]
        public void SetUp()
        {
            // Assemble
            gradingCalculator = new GradingCalculator();
        }

        [Test]
        public void GetGrade_Score95Attendance90_ReturnsA()
        {
            // Assemble
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.That(grade, Is.EqualTo("A"));
        }

        [Test]
        public void GetGrade_Score85Attendance90_ReturnsB()
        {
            // Assemble
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.That(grade, Is.EqualTo("B"));
        }

        [Test]
        public void GetGrade_Score65Attendance90_ReturnsC()
        {
            // Assemble
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.That(grade, Is.EqualTo("C"));
        }

        [Test]
        public void GetGrade_Score95Attendance65_ReturnsB()
        {
            // Assemble
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.That(grade, Is.EqualTo("B"));
        }

        [Test]
        [TestCase(95, 55)]
        [TestCase(65, 55)]
        [TestCase(50, 90)]
        public void GetGrade_LowScoreAndOrAttendance_ReturnsF(int setScore, int setAttendancePercentage)
        {
            // Assemble
            gradingCalculator.Score = setScore;
            gradingCalculator.AttendancePercentage = setAttendancePercentage;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.That(grade, Is.EqualTo("F"));
        }

        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GetGrade_InputManyCombinationsOfScoreAndAttendance_ReturnCorrectGrades(int setScore, int setAttendancePercentage)
        {
            // Assemble
            gradingCalculator.Score = setScore;
            gradingCalculator.AttendancePercentage = setAttendancePercentage;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            return grade;
        }
    }
}
