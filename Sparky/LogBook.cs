using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public interface ILogBook
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }

        void Message(string message);

        bool LogToDB(string message);

        bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal);

        string MessageWithReturnString(string message);

        bool LogWithOutputResult(string message, out string outputStr);

        bool LogWithRefObject(ref Customer customer);
    }

    public class LogBook : ILogBook
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public bool LogToDB(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal)
        {
            if (balanceAfterWithdrawal >= 0)
            {
                Console.WriteLine("Success");
                return true;
            }
            else
            {
                Console.WriteLine("Failure");
                return false;
            }
        }

        public string MessageWithReturnString(string message)
        {
            Console.WriteLine(message);
            return message;
        }

        public bool LogWithOutputResult(string message, out string outputStr)
        {
            outputStr = "Hello " + message;

            return true;
        }

        public bool LogWithRefObject(ref Customer customer)
        {
            return true;
        }
    }
}
