using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    internal interface ICustomer
    {
        public void LogIn(string userName, string passWord);

        public void LogOut();

        public void GetMyAccountsInfo();
    }
}