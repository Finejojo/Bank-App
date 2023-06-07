using NUnit.Framework;
using Bank_App_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_App_2.Tests
{
    [TestFixture]
    public class BankAppMode
    {
        [Test]
        public void Login(string UserName, string Password)
        {
            var sut = new BankOperations();
        }
    }
}
