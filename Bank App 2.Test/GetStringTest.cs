using BankApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_App_2.Test
{
    using NUnit.Framework;
    using System;
    using System.IO;

    namespace BankApp.Tests
    {
        [TestFixture]
        public class GetStringTest
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
            public void GetNumber_ShouldReturnValidDecimal()
            {
                // Arrange
                string userInput = "123.45";
                SetUserInput(userInput);

                // Act
                decimal result = BankOperations.GetNumber();

                // Assert
                Assert.AreEqual(123.45m, result, "Returned decimal should match the expected value");
            }
        }
    }

}
