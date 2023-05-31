using System;
using BankApp;

namespace BankApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.Beep();
            Console.WriteLine("==========================================");
            Console.WriteLine("                 WELCOME");
            Console.WriteLine("==========================================");

            var running = true;

            try
            {
                while (running)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Type X and press Enter to terminate all operation \nType C and press Enter to register a customer \nType L and press Enter to LogIn a customer \nType LO and press Enter to LogOut a customer \nType A and press Enter to create a bank account for a customer \nType AC and press Enter to show details all accounts owned by a customer \nType D and press Enter to make deposit into an account \nType W and press Enter to make a withdrawal form an customer \nType T and press Enter to transfer funds to an account \nType S and press Enter to print the statement of an account \nType G and press Enter to print the statement of an account \n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    var response = Console.ReadLine();
                    if (response != null)
                    {
                        switch (response)
                        {
                            case "x":
                                running = false;
                                break;

                            case "c":
                                BankOperations.CreateCustomer();
                                break;

                            case "l":
                                BankOperations.LogIn();
                                break;

                            case "lo":
                                BankOperations.LogOut();
                                break;

                            case "a":
                                BankOperations.CreateAccount();
                                break;

                            case "ac":
                                BankOperations.ShowAllMyAccountInfo();
                                break;

                            case "d":
                                BankOperations.Deposit();
                                break;

                            case "w":
                                BankOperations.WithDraw();
                                break;

                            case "t":
                                BankOperations.Transfer();
                                break;

                            case "s":
                                BankOperations.PrintStatement();
                                break;

                            case "g":
                                BankOperations.GetBalance();
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}