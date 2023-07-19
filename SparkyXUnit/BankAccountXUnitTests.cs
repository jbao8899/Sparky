using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class BankAccountXUnitTests
    {
        [Fact]
        public void Deposit_Input100RealLogger_CorrectlyDeposited()
        {
            // Assemble
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();
            BankAccount bankAccount = new BankAccount(mockLogger.Object);

            // Act
            bool result = bankAccount.Deposit(100);

            Assert.True(result);
            Assert.Equal(100, bankAccount.GetBalance());
        }

        [Fact]
        public void Withdrawal_Withdraw100With200Balance_ReturnsTrue()
        {
            // assemble
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();

            mockLogger.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x >= 0))).Returns(true);
            BankAccount bankAccount = new BankAccount(mockLogger.Object);

            bankAccount.Deposit(200);

            // Act
            bool result = bankAccount.Withdrawal(100);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Withdrawal_Withdraw300With200Balance_ReturnsFalse()
        {
            // assemble
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();

            // Mock covers for opposite case (x < 0 yielding false) automatically, by default
            mockLogger.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x >= 0))).Returns(true);

            // Equivalent:
            //mockLogger.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            BankAccount bankAccount = new BankAccount(mockLogger.Object);

            bankAccount.Deposit(200);

            // Act
            bool result = bankAccount.Withdrawal(300);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void BankLogDummy_LogMockString_ReturnsTrue()
        {
            // assemble
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();

            string desiredOutput = "hello";

            // When you input any string to MessageWithReturnString(), that string is returned with ToLower()
            mockLogger.Setup(u => u.MessageWithReturnString(It.IsAny<string>())).Returns((string str) => str.ToLower());

            // Act
            string result = mockLogger.Object.MessageWithReturnString("Hello");

            // Assert
            Assert.Equal(desiredOutput, result);
        }

        [Fact]
        public void BankLogDummy_LogMockStringOutStr_ReturnsTrue()
        {
            // assemble
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();

            string desiredOutput = "hello";

            // When you input any string to MessageWithReturnString(), that string is returned with ToLower()
            mockLogger.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);

            // Act
            string result = "";

            // Assert
            Assert.True(mockLogger.Object.LogWithOutputResult("Ben", out result));
            Assert.Equal(desiredOutput, result);
        }

        [Fact]
        public void BankLogDummy_LogRefChecker_ReturnsTrue()
        {
            // assemble
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();
            Customer customer = new Customer();
            Customer notUsed = new Customer();

            string desiredOutput = "hello";

            // When you input any string to MessageWithReturnString(), that string is returned with ToLower()
            mockLogger.Setup(u => u.LogWithRefObject(ref customer)).Returns(true);

            // Act
            bool result = mockLogger.Object.LogWithRefObject(ref customer);

            // Assert
            Assert.True(result);
            Assert.False(mockLogger.Object.LogWithRefObject(ref notUsed)); // ???
        }

        [Fact]
        public void BankLogDummy_SetAndGetLogSeverityAndType_SetAndGetThemCorrectly()
        {
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();
            mockLogger.Setup(u => u.LogSeverity).Returns(10);
            mockLogger.Setup(u => u.LogType).Returns("Warning");

            mockLogger.SetupAllProperties(); // allow setting properties
            mockLogger.Object.LogSeverity = 100;
            mockLogger.Object.LogType = "type";

            Assert.Equal(100, mockLogger.Object.LogSeverity);
            Assert.Equal("type", mockLogger.Object.LogType);

            // callback stuff added here
            string logTemp = "Hello, ";
            mockLogger.Setup(u => u.LogToDB(It.IsAny<string>()))
                .Returns(true)
                .Callback((string str) => logTemp += str);

            mockLogger.Object.LogToDB("Ben");
            Assert.Equal("Hello, Ben", logTemp);

            int counter = 5;
            mockLogger.Setup(u => u.LogToDB(It.IsAny<string>()))
                .Callback(() => counter++)
                .Returns(true)
                .Callback(() => counter++); // Can put callback before or after the Returns, or both

            mockLogger.Object.LogToDB("Ben");
            mockLogger.Object.LogToDB("Ben");
            Assert.Equal(9, counter);
        }

        [Fact]
        public void BankLogDummy_Deposit100_MessageCalledTwiceAndLogSeveritySetTo101()
        {
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();
            BankAccount bankAccount = new BankAccount(mockLogger.Object);
            bankAccount.Deposit(100);

            Assert.Equal(100, bankAccount.GetBalance());

            // Check that we called the logging function twice and set the LogSeverity Property
            mockLogger.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
            mockLogger.VerifySet(u => u.LogSeverity = 101, Times.Exactly(1));
            mockLogger.VerifyGet(u => u.LogSeverity, Times.Exactly(0));
        }
    }

    internal class LogFaker : ILogBook
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }

        public void Message(string message)
        {
        }

        public bool LogToDB(string message)
        {
            return true;
        }

        public bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal)
        {
            return true;
        }

        public string MessageWithReturnString(string message)
        {
            return "";
        }

        public bool LogWithOutputResult(string message, out string outputStr)
        {
            outputStr = "";

            return true;
        }

        public bool LogWithRefObject(ref Customer customer)
        {
            return true;
        }
    }
}
