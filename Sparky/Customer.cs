using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Customer : ICustomer
    {
        public int Discount = 15;

        public int OrderTotal { get; set; }

        public string GreetMessage { get; set; }

        public bool IsPlatinum { get; set; }

        public Customer()
        {
            IsPlatinum = false;
        }

        public string GreetAndCombineNames(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name should not be null, empty, or only contain whitespace.");
            }

            GreetMessage = $"Hello, {firstName} {lastName}.";
            Discount = 20;
            return GreetMessage;
        }

        public CustomerType GetCustomerDetails()
        {
            if (OrderTotal < 100)
            {
                return new BasicCustomer();
            }
            else
            {
                return new PlatinumCustomer();
            }
        }
    }

    public interface ICustomer // Not needed, should not make this interface just for Moq
    {
        public int OrderTotal { get; set; }

        public string GreetMessage { get; set; }

        public bool IsPlatinum { get; set; }

        string GreetAndCombineNames(string firstName, string lastName);

        CustomerType GetCustomerDetails();   
    }
}
