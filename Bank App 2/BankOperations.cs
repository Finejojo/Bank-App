using System;

namespace BankApp
{
    public class BankOperations
    {
        /// <summary>
        /// for creating new customer profile
        /// </summary>
        public static void CreateCustomer()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Creating a new customer profile");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter your First Name");
            var firstName = GetString();
            Console.WriteLine("Enter your Second Name");
            var lastName = GetString();
            Console.WriteLine("Enter your Email");
            var email = GetString();
            Console.WriteLine("Enter your User Name");
            var userName = GetString();

            while (ConfirmCustomer(userName) != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The user name you entered already exist, choose another one");
                Console.ForegroundColor = ConsoleColor.White;
                userName = GetString();
            }
            Console.WriteLine("Enter your Password");
            var passWard = GetString();

            var newCustomer = new Customer(firstName, lastName, email, userName, passWard);
        }

        /// <summary>
        /// for logging a customer in
        /// </summary>
        public static void LogIn()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Attempting to log a customer in");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter your User Name");
            var userName = GetString();
            Console.WriteLine("Enter your Password");
            var password = GetString();

            GetCustomer(userName).LogIn(userName, password);
        }

        /// <summary>
        /// for logging a customer out
        /// </summary>
        public static void LogOut()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Attempting to log a customer out");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Type in the ID of the customer you want to log out and press enter");
            GetCustomer().LogOut();
        }

        /// <summary>
        /// for creating a new bank account for a customer
        /// </summary>
        public static void CreateAccount()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Attempting to create a bank account for a customer");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Type in the ID of the customer that you want to create an account for and press enter");
            var customer = GetCustomer();

            Console.WriteLine("Type in \"S\" for savings account or \"C\" for current account ");
            var accountType = GetString();

            Console.WriteLine("Type in the initial amount you want to use to create the account");
            var amount = GetNumber();

            if (accountType == null) return;
            var type = accountType.Trim().ToLower() switch
            {
                "s" => AccountType.Savings,
                "c" => AccountType.Current,
                _ => AccountType.Savings
            };

            var aNewAccount = new Account(customer, customer.FullName, customer.CustomerId,
                    $"New account being created for {customer.FullName}", DateTime.Now, type, amount);
        }

        /// <summary>
        /// Show the information of all account owned by a customer
        /// </summary>
        public static void ShowAllMyAccountInfo()
        {
            Console.WriteLine("Type in the ID of the customer that you want to show All accounts info and press enter");
            GetCustomer().GetMyAccountsInfo();
        }

        /// <summary>
        /// for crediting a bank account
        /// </summary>
        public static void Deposit()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Attempting to credit a customer's account");
            Console.ForegroundColor = ConsoleColor.White;

            var account = GetAccount("credit to");

            Console.WriteLine("Type in the amount that you want to credit into this account");
            var amount = GetNumber();

            account.MakeDeposit(amount, DateTime.Now, $"Crediting {amount} into the account");
        }

        /// <summary>
        /// for debiting an account
        /// </summary>
        public static void WithDraw()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Attempting to debit a customer's account");
            Console.ForegroundColor = ConsoleColor.White;

            var account = GetAccount("withdraw from");

            Console.WriteLine("Type in the amount that you want to credit into this account");
            var amount = GetNumber();

            account.MakeWithdrawal(amount, DateTime.Now, $"Debiting {amount} from this account");
        }

        /// <summary>
        /// for transferring funds between accounts
        /// </summary>
        public static void Transfer()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Attempting to transfer funds between accounts");
            Console.ForegroundColor = ConsoleColor.White;

            var sender = GetAccount("withdraw from");
            var beneficiary = GetAccount("credit to");

            Console.WriteLine("Type in the amount that you want to transfer");
            var amount = GetNumber();

            sender.TransferMoney(beneficiary, amount, DateTime.Now, $"Transferred {amount} to the account {beneficiary.AccountNumber} owned by {beneficiary.OwnerName}");
        }

        /// <summary>
        /// A method used to display the balance in an account
        /// </summary>
        public static void GetBalance()
        {
            Console.WriteLine(GetAccount("display is balance").Balance);
        }

        /// <summary>
        /// for displaying statement of a bank account
        /// </summary>
        public static void PrintStatement()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Attempting to print a statement of a bank account");
            Console.ForegroundColor = ConsoleColor.White;

            var account = GetAccount("display its statement");

            account.GetAccountHistory();
        }

        #region Operation Methods

        /// <summary>
        /// Getting a customer instance with his user name
        /// </summary>
        /// <param name="userName">The user name to check for a match</param>
        /// <returns>The return customer</returns>
        public static Customer GetCustomer(string userName)
        {
            var customer =
                Bank.Customers.Find(customer1 => customer1.UserName == userName);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                throw new Exception("A customer with that user name does not exist");
            }
        }

        /// <summary>
        /// Getting a customer instance with his ID
        /// </summary>
        /// <returns>The returned customer</returns>
        public static Customer GetCustomer()
        {
            var customerId = (int)GetNumber();
            var customer =
                Bank.Customers.Find(customer1 => customer1.CustomerId == customerId);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                throw new Exception("no such customer found");
            }
        }

        /// <summary>
        /// Confirm if a customer instance with his user name
        /// </summary>
        /// <returns>The return customer</returns>
        public static Customer ConfirmCustomer(string userName)
        {
            return Bank.Customers.Find(customer1 => customer1.UserName == userName)!;
        }

        /// <summary>
        /// A helper method used for return an instance of an account using the account number provided on the console
        /// </summary>
        /// <param name="note">a short note</param>
        /// <returns>The return account</returns>
        public static Account GetAccount(string note)
        {
            Console.WriteLine($"Type in the account number that you intend to {note}");
            var accountNumber = GetString();
            var account =
                Bank.Accounts.Find(account1 => account1.AccountNumber == accountNumber);
            if (account != null)
            {
                return account;
            }
            else
            {
                throw new Exception("no such account found");
            }
        }

        /// <summary>
        /// A helper method used for returning strings from the console
        /// </summary>
        /// <returns>The returned string value</returns>
        public static string GetString() => Console.ReadLine()!;

        /// <summary>
        /// A helper method used for return amounts from the console
        /// </summary>
        /// <returns>The returned amount</returns>
        public static decimal GetNumber()
        {
            decimal.TryParse(GetString(), out var theAmount);
            return theAmount;
        }

        #endregion Operation Methods
    }
}