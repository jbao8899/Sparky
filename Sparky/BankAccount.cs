using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class BankAccount
    {
        private int _balance;
    
        private ILogBook _logBook;

        public BankAccount(ILogBook setLogBook)
        {
            _balance = 0;
            _logBook = setLogBook;
        }

        public bool Deposit(int amount)
        {
            _logBook.Message("Deposit invoked");
            _logBook.Message("Test");
            _logBook.LogSeverity = 101;
            _balance += amount;
            return true;
        }

        public bool Withdrawal(int amount)
        {
            if (amount <= _balance)
            {
                _balance -= amount;
                _logBook.LogToDB($"Withdrawal amount: {amount}");
                return _logBook.LogBalanceAfterWithdrawal(_balance);
            }
            else
            {
                return _logBook.LogBalanceAfterWithdrawal(_balance - amount);
            }
        }

        public int GetBalance()
        {
            return _balance;
        }
    }

}
