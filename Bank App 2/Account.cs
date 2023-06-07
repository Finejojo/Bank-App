using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;


namespace BankApp
{
    /// <summary>
    /// A class that holds the blueprint of Customer account and all transactions made
    /// </summary>
    public class Account : IAccount
    {
        public string AccountNumber { get; }
        public Customer AccountOwner { get; }
        public string OwnerName { get; }
        public int OwnerId { get; }
        public string Note { get; }
        public DateTime DateCreated { get; }
        public AccountType Type { get; }

        public decimal Balance
        {
            get
            {
                return AllTransactions.Sum(item => item.Amount);
            }
        }

        /// <summary>
        /// Holds the history of all transactions made on an account
        /// </summary>
        public List<Transaction> AllTransactions { get; }

        //private static int _accountNumSeed = 0000000001;

        /// <summary>
        /// Constructor for the Customer account
        /// </summary>
        /// <param name="accountOwner">An instance of the Customer who owns the account</param>
        /// <param name="ownerName"></param>
        /// <param name="ownerId">The Customer ID</param>
        /// <param name="note"> A short note describing the account being created</param>
        /// <param name="dateCreated"> Date the account was created</param>
        /// <param name="accountType">The type of account created</param>
        /// <param name="initialBalance">The Starting Amount for account creation</param>
        public Account(Customer accountOwner, string ownerName, int ownerId, string note, DateTime dateCreated, AccountType accountType, decimal initialBalance)
        {
            Random random = new Random();
            AccountNumber = random.Next().ToString().PadLeft(10, '0');
            //_accountNumSeed++;

            AllTransactions = new List<Transaction>();
            AccountOwner = accountOwner;
            OwnerName = ownerName;
            OwnerId = ownerId;
            Note = note;
            DateCreated = dateCreated;
            Type = accountType;

            switch (accountType)
            {
                // For current account
                case AccountType.Current when initialBalance < 1000:
                    throw new InvalidOperationException($"{nameof(initialBalance)}, Amount too low for creating this type of Account");
                //Console.WriteLine("Amount too low for creating this type of Account");
                //return;
                // For savings account
                case AccountType.Savings when initialBalance < 100:
                    throw new InvalidOperationException($"{nameof(initialBalance)}, Amount too low for creating this type of Account");
                //Console.WriteLine("Amount too low for creating this type of Account");
                //return;

                default:
                    //Adding the account to the Bank's Account List
                    AddThisAccount();

                    //Crediting the initial amount
                    MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
                    break;
            }

            const string savings = "Savings";
            const string current = "Current";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($" A {(accountType == AccountType.Current ? current : savings)} account {AccountNumber} was created for {AccountOwner.FullName} on {dateCreated.ToShortTimeString()} with {initialBalance:N} Naira initial balance. The account number is {this.AccountNumber}");
        }

        public Account()
        {
        }

        /// <summary>
        /// A method used to add a new account to the Customer's Account List
        /// </summary>
        private void AddThisAccount()
        {
            if (!AccountOwner.IsLoggedIn)
            {
                Console.WriteLine("You need to log in first before making this operation");
                return;
                //throw new InvalidOperationException("You need to log in first before making this operation");
            }

            //Adding this account the customer's account list
            AccountOwner.AddAccount(this);
            //Adding this account to the bank's Account list
            Bank.Accounts.Add(this);
        }

        /// <summary>
        /// A Method used to Make a deposit on an account
        /// </summary>
        /// <param name="amount">Amount to deposit</param>
        /// <param name="date">Date of Transaction</param>
        /// <param name="note">A short description of the transaction</param>
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            //Ensure that the customer is logged in
            //if (!AccountOwner.IsLoggedIn)
            //    throw new InvalidOperationException("You need to log in first before making this operation");

            if (amount <= 0)
            {
                //Console.WriteLine("Amount of deposit must be positive");
                //return;
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }

            //Saving the transaction made
            var deposit = new Transaction(AccountOwner, AccountNumber, Type, amount, Balance, Balance + amount, TransactionType.Deposit, date, note);
            AllTransactions.Add(deposit);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"{amount:N} was credited to the account {this.AccountNumber} owned by {this.OwnerName}, current balance is {this.Balance:N} Naira");
        }

        /// <summary>
        /// A Method used to Make a Withdrawal on an account
        /// </summary>
        /// <param name="amount">Amount to withdraw</param>
        /// <param name="date">Date of Transaction</param>
        /// <param name="note">A short description of the transaction</param>
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            //Ensuring that the customer is logged in
            if (!AccountOwner.IsLoggedIn)
            {
                //Console.WriteLine("You need to log in first before making this operation");
                //return;
                throw new InvalidOperationException("You need to log in first before making this operation");
            }

            //Checking for negative values
            if (amount <= 0)
            {
                //Console.WriteLine("Amount of withdrawal must be positive");
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
                //return;
            }

            //Making sure savings account Balance doesn't go below 100
            if (Type == AccountType.Savings && ((Balance - amount) < 100))
            {
                Console.WriteLine("No sufficient funds for this withdrawal");
                return;
                //throw new InvalidOperationException("No sufficient funds for this withdrawal");
            }

            //Making sure the Balance doesn't go below 0(zero)
            if (Balance - amount <= 0)
            {
                //Console.WriteLine("No sufficient funds for this withdrawal");
                //return;
                throw new InvalidOperationException("No sufficient funds for this withdrawal");
            }

            //Saving the transaction made
            var withdrawal = new Transaction(AccountOwner, AccountNumber, Type, -amount, Balance, (Balance + (-amount)), TransactionType.Withdrawal, date, note);
            this.AllTransactions.Add(withdrawal);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{amount:N} was debited from the account {this.AccountNumber} owned by {this.OwnerName}, current balance is {this.Balance:N} Naira");
        }

        /// <summary>
        /// for transferring money to another account
        /// </summary>
        /// <param name="destination">the beneficiary account</param>
        /// <param name="amount">amount to transfer</param>
        /// <param name="date">date of transaction</param>
        /// <param name="note">a brief note</param>
        public void TransferMoney(Account destination, decimal amount, DateTime date, string note)
        {
            //Ensuring that the customer is logged in
            if (!AccountOwner.IsLoggedIn)
            {
                Console.WriteLine("You need to log in first before making this operation");
                return;
                //throw new InvalidOperationException("You need to log in first before making this operation");
            }

            if (this.AccountNumber == destination.AccountNumber)
            {
                Console.WriteLine("You're trying to send funds to the same account as you're transferring from");
                return;
                //throw new InvalidOperationException("You're trying to send funds to the same account as you're transferring from");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Transferring {amount:N} from {AccountOwner.FullName}'s account {this.AccountNumber} to {destination.AccountOwner.FullName}'s account {destination.AccountNumber}");

            this.MakeWithdrawal(amount, date, note);
            destination.MakeDeposit(amount, date, note);
        }

        /// <summary>
        /// A method used to get the statement of the account
        /// </summary>
        public void GetAccountHistory()
        {
            //Ensuring that the customer is logged in
            if (!AccountOwner.IsLoggedIn)
            {
                Console.WriteLine("You need to log in first before making this operation");
                return;
                //throw new InvalidOperationException("You need to log in first before making this operation");
            }

            var report = new StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            //foreach (var item in _allTransactions)
            //{
            //    balance += item.Amount;
            //    report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            //}

            AllTransactions.ForEach(item =>
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount:N}\t{balance:N}\t{item.Notes}");
            });

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Printing the statement of account of {this.OwnerName} on the account {this.AccountNumber}");
            Console.WriteLine(report.ToString());
        }
    }
}