using System.Collections.Generic;

namespace BankApp
{
    public class Bank
    {
        public Bank()
        {
        }

        /// <summary>
        /// A list holding the record of all customers in the bank
        /// </summary>
        public static List<Customer> Customers = new List<Customer>();

        /// <summary>
        /// A list holding the record of all accounts in the bank
        /// </summary>
        public static List<Account> Accounts = new List<Account>();
    }
}