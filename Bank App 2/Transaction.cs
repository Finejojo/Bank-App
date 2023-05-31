using System;

namespace BankApp
{
    /// <summary>
    /// A class that holds the blueprint of transaction made
    /// </summary>
    public class Transaction
    {
        public Customer Owner { get; }
        public string OwnerFullName { get; }
        public string AccountNumber { get; }
        public AccountType Type { get; }
        public decimal Amount { get; }
        public decimal BalanceBefore { get; }
        public decimal BalanceAfter { get; }
        public DateTime Date { get; }
        public string Notes { get; }
        public TransactionType TypeOfTransaction { get; }

        /// <summary>
        /// A Constructor used to pass to pass values for each transaction made
        /// </summary>
        /// <param name="owner">A reference to the account owner</param>
        /// <param name="accountNumber">Transaction account number</param>
        /// <param name="type">The type of account</param>
        /// <param name="amount">Amount on the transaction</param>
        /// <param name="balance">Balance after transaction</param>
        /// <param name="balanceAfter"></param>
        /// <param name="typeOfTransaction">Type of transaction made (Deposit/Withdrawal)</param>
        /// <param name="date">Date of transaction</param>
        /// <param name="note">A short description of the transaction</param>
        public Transaction(Customer owner, string accountNumber, AccountType type, decimal amount, decimal balance, decimal balanceAfter, TransactionType typeOfTransaction, DateTime date, string note)
        {
            this.Owner = owner;
            OwnerFullName = Owner.FullName;
            this.AccountNumber = accountNumber;
            this.Type = type;
            this.Amount = amount;
            this.BalanceBefore = balance;
            BalanceAfter = balanceAfter;
            TypeOfTransaction = typeOfTransaction;
            this.Date = date;
            this.Notes = note;
        }
    }
}