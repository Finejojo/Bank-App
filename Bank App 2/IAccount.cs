using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    public interface IAccount
    {
        public void MakeDeposit(decimal amount, DateTime date, string note);

        public void MakeWithdrawal(decimal amount, DateTime date, string note);

        public void TransferMoney(Account destination, decimal amount, DateTime date, string note);

        public void GetAccountHistory();
    }
}