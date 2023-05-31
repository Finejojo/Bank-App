using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    /// <summary>
    /// A blueprint to hold customer details in the Bank
    /// </summary>
    public class Customer : ICustomer
    {
        public int CustomerId { get; }
        private readonly string _firstname;
        private readonly string _lastName;
        public string CustomerEmail { get; }
        public string FullName => _firstname + " " + _lastName;
        public string UserName { get; }
        public string PassWord { get; }
        public bool IsLoggedIn { get; private set; }

        private readonly List<Account> _allAccounts = new List<Account>();
        private static int _id = 0;

        /// <summary>
        /// Constructor for the Customer
        /// </summary>
        /// /// <param name="firstName">Customer First Name</param>
        /// <param name="lastName">Customer Last Name</param>
        /// <param name="email">Customer Email</param>
        /// <param name="userName">Customer UserName</param>
        /// <param name="passWord">Customer PassWord</param>
        public Customer(string firstName, string lastName, string email, string userName, string passWord)
        {
            CustomerId = ++_id;
            _firstname = firstName;
            _lastName = lastName;
            CustomerEmail = email;
            UserName = userName;
            PassWord = passWord;

            //Adding this customer to bank's customer list
            Bank.Customers.Add(this);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"A Customer profile has been created for {FullName}. The ID of the customer is {this.CustomerId}");
        }

        /// <summary>
        /// A method used to log Customers into their account
        /// </summary>
        /// <param name="userName">Customer UserName</param>
        /// <param name="passWord">Customer PassWord</param>
        public void LogIn(string userName, string passWord)
        {
            if (this.UserName == userName && this.PassWord == passWord)
            {
                this.IsLoggedIn = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{this.FullName} has logged in");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The logIn credentials you provided are not correct");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// A method used to log Customers out of their account
        /// </summary>
        public void LogOut()
        {
            this.IsLoggedIn = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{this.FullName} has logged out");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// A method used to an account to the customer's account List
        /// </summary>
        /// <param name="anAccount">An account to add to customer's account List</param>
        public void AddAccount(Account anAccount)
        {
            _allAccounts.Add(anAccount);
        }

        public IEnumerable<Account> FetchAccounts() => _allAccounts;

        /// <summary>
        /// A method for transferring funds between different accounts
        /// </summary>
        /// <param name="from">The account transferring the fund</param>
        /// <param name="destination">The account receiving the fund</param>
        /// <param name="amount">The amount to be transferred</param>
        /// <param name="date">Date of transaction</param>
        /// <param name="note">A short description of the transaction</param>
        public void TransferMoney(Account from, Account destination, decimal amount, DateTime date, string note)
        {
            if (from.OwnerId != CustomerId)
                throw new InvalidOperationException("You're attempting to make a withdrawal from an account that doesn't belong to this customer");

            if (from.AccountNumber == destination.AccountNumber)
                throw new InvalidOperationException("You're trying to send funds to the same account as you're transferring from");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Transferring {amount:N} from {from.AccountOwner.FullName}'s account {from.AccountNumber} to {destination.AccountOwner.FullName}'s account {destination.AccountNumber}");

            from.MakeWithdrawal(amount, date, note);
            destination.MakeDeposit(amount, date, note);
        }

        /// <summary>
        /// A method used to get the list of all accounts owned by a customer
        /// </summary>
        public void GetMyAccountsInfo()
        {
            if (!this.IsLoggedIn)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to log in first");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            var report = new StringBuilder();
            report.AppendLine("Account Number\t\tAccount Type\tDate Created\tBalance");

            _allAccounts.ForEach(item =>
            {
                const string savings = "Savings";
                const string current = "Current";
                report.AppendLine($"{item.AccountNumber}\t{(item.Type == AccountType.Current ? current : savings)}\t{item.DateCreated.ToShortDateString()}\t{item.Balance:N}");
            });

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Printing the infos of all accounts owned by {this.FullName}");
            Console.WriteLine(report.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}