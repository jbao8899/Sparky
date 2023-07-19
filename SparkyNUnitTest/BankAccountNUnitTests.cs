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
    public class BankAccountNUnitTests
    {
        //private BankAccount bankAccount;
        //[SetUp]
        //public void SetUp()
        //{
        //    // Assemble

        //    // Doing this causes this file to not perform unit testing,
        //    // as it uses LogBook in addition to BankAccount
        //    // It is therefore integration testing
        //    //bankAccount = new BankAccount(new LogBook());

        //    // This is unit testing, but creating fake classes takes too long in practice
        //    //bankAccount = new BankAccount(new LogFaker()); 

        //    Mock<ILogBook> mockLogger = new Mock<ILogBook>(); // Does nothing
        //    bankAccount = new BankAccount(mockLogger.Object);
        ////}

        // This is not a unit test, as it uses LogBook in addition to BankAccount!!!!
        // It is an integration test!!!!
        [Test]
        public void Deposit_Input100RealLogger_CorrectlyDeposited()
        {
            // Assemble
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();
            BankAccount bankAccount = new BankAccount(mockLogger.Object);

            // Act
            bool result = bankAccount.Deposit(100);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));
            });
        }

        [Test]
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
            Assert.That(result, Is.True);
        }

        [Test]
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
            Assert.That(result, Is.False);
        }

        [Test]
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
            Assert.That(result, Is.EqualTo(desiredOutput));
        }

        [Test]
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
            Assert.IsTrue(mockLogger.Object.LogWithOutputResult("Ben", out result));
            Assert.That(result, Is.EqualTo(desiredOutput));
        }

        [Test]
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
            Assert.IsTrue(result);
            Assert.IsFalse(mockLogger.Object.LogWithRefObject(ref notUsed)); // ???
        }

        [Test]
        public void BankLogDummy_SetAndGetLogSeverityAndType_SetAndGetThemCorrectly()
        {
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();
            mockLogger.Setup(u => u.LogSeverity).Returns(10);
            mockLogger.Setup(u => u.LogType).Returns("Warning");

            mockLogger.SetupAllProperties(); // allow setting properties
            mockLogger.Object.LogSeverity = 100;
            mockLogger.Object.LogType = "type";

            Assert.That(mockLogger.Object.LogSeverity, Is.EqualTo(100));
            Assert.That(mockLogger.Object.LogType, Is.EqualTo("type"));

            // callback stuff added here
            string logTemp = "Hello, ";
            mockLogger.Setup(u => u.LogToDB(It.IsAny<string>()))
                .Returns(true)
                .Callback((string str)  => logTemp += str);

            mockLogger.Object.LogToDB("Ben");
            Assert.That(logTemp, Is.EqualTo("Hello, Ben"));

            int counter = 5;
            mockLogger.Setup(u => u.LogToDB(It.IsAny<string>()))
                .Callback(() => counter++)
                .Returns(true)
                .Callback(() => counter++); // Can put callback before or after the Returns, or both

            mockLogger.Object.LogToDB("Ben");
            mockLogger.Object.LogToDB("Ben");
            Assert.That(counter, Is.EqualTo(9));
        }

        [Test]
        public void BankLogDummy_Deposit100_MessageCalledTwiceAndLogSeveritySetTo101()
        {
            Mock<ILogBook> mockLogger = new Mock<ILogBook>();
            BankAccount bankAccount = new BankAccount(mockLogger.Object);
            bankAccount.Deposit(100);

            Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));
            
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
