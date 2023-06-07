using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankApp;
using Bank_App_2;

namespace BankApp.Tests
{
    [TestFixture]
    public class GetAccountTest
    {
        private StringWriter consoleOutput;
        private StringReader userInputReader;

        [SetUp]
        public void SetUp()
        {
            // Redirect Console output
            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Set up user input
            userInputReader = null; // Initialize with null
            SetUserInput(""); // Set default empty input
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up Console output redirection
            consoleOutput.Dispose();
            Console.SetOut(Console.Out);

            // Clean up user input redirection
            if (userInputReader != null)
            {
                userInputReader.Dispose();
                Console.SetIn(Console.In);
            }
        }

        private void SetUserInput(string input)
        {
            // Create a StringReader with the desired input
            userInputReader = new StringReader(input);
            // Set Console.ReadLine to read from the StringReader
            Console.SetIn(userInputReader);
        }

        //[Test]
        //public void GetAccount_ExistingAccountNumber_ShouldReturnAccount()
        //{
        //    // Arrange
        //    List<Account> accounts = new List<Account>
        //    {
        //        new Account("John Doe", "A123", 1000),
        //        new Account("Jane Smith", "B456", 2000),
        //        new Account("Bob Johnson", "C789", 3000)
        //    };
        //    Bank.Accounts = accounts;

        //    string accountNumber = "B456";
        //    string userInput = accountNumber;
        //    SetUserInput(userInput);

        //    // Act
        //    Account result = BankOperations.GetAccount("withdraw from");

        //    // Assert
        //    Assert.IsNotNull(result, "Returned account should not be null");
        //    Assert.AreEqual(accountNumber, result.AccountNumber, "Returned account number should match the expected value");
        //}

        //[Test]
        //public void GetAccount_NonExistingAccountNumber_ShouldThrowException()
        //{
        //    // Arrange
        //    List<Account> accounts = new List<Account>
        //    {
        //        new Account("John Doe", "A123", 1000),
        //        new Account("Jane Smith", "B456", 2000),
        //        new Account("Bob Johnson", "C789", 3000)
        //    };
        //    Bank.Accounts = accounts;

        //    string accountNumber = "X999"; // Non-existing account number
        //    string userInput = accountNumber;
        //    SetUserInput(userInput);

        //    // Act & Assert
        //    Assert.Throws<Exception>(() => BankOperations.GetAccount("withdraw from"), "Exception should be thrown for non-existing account number");
        //}

        [Test]
        public void GetString_ShouldReturnUserInput()
        {
            // Arrange
            string userInput = "Test input";
            SetUserInput(userInput);

            // Act
            string result = BankOperations.GetString();

            // Assert
            Assert.AreEqual(userInput, result, "Returned string should match the user input");
        }

            [Test]
            public void GetAccount_ExistingAccountNumber_ReturnsAccount()
            {
                // Arrange
                var accountNumber = "1234567890";
                var account = new Account(/* account initialization parameters */);
                Bank.Accounts.Add(account);

                // Act
                var retrievedAccount = BankOperations.GetAccount("work with");

                // Assert
                Assert.AreEqual(account, retrievedAccount);
            }

            //[Test]
            //public void GetAccount_NonExistingAccountNumber_ThrowsException()
            //{
            //    // Arrange
            //    var accountNumber = "9876543210";

            //    // Act and Assert
            //    Assert.Throws<Exception>(() => BankOperations.GetAccount("work with"));
            //}
        }
    }

