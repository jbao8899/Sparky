using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{

    public class GradingCalculatorXUnitTests
    {
        private GradingCalculator gradingCalculator; // _ before name?

        public GradingCalculatorXUnitTests()
        {
            gradingCalculator = new GradingCalculator();
        }

        [Fact]
        public void GetGrade_Score95Attendance90_ReturnsA()
        {
            // Assemble
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("A", grade);
        }

        [Fact]
        public void GetGrade_Score85Attendance90_ReturnsB()
        {
            // Assemble
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("B", grade);
        }

        [Fact]
        public void GetGrade_Score65Attendance90_ReturnsC()
        {
            // Assemble
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("C", grade);
        }

        [Fact]
        public void GetGrade_Score95Attendance65_ReturnsB()
        {
            // Assemble
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("B", grade);
        }

        [Theory]
        [InlineData(95, 55)]
        [InlineData(65, 55)]
        [InlineData(50, 90)]
        public void GetGrade_LowScoreAndOrAttendance_ReturnsF(int setScore, int setAttendancePercentage)
        {
            // Assemble
            gradingCalculator.Score = setScore;
            gradingCalculator.AttendancePercentage = setAttendancePercentage;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal("F", grade);
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void GetGrade_InputManyCombinationsOfScoreAndAttendance_ReturnCorrectGrades(int setScore, int setAttendancePercentage, string expectedResult)
        {
            // Assemble
            gradingCalculator.Score = setScore;
            gradingCalculator.AttendancePercentage = setAttendancePercentage;

            // Act
            string grade = gradingCalculator.GetGrade();

            // Assert
            Assert.Equal(expectedResult, grade);
        }
    }
}
