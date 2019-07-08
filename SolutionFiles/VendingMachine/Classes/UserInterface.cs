using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {

        public VendingMachine vendingMachine = new VendingMachine();
        private bool Done = false;

        public void RunInterface()
        {
            while (!Done)
            {
                MenuOne();
            }
        }

        public void MenuOne()
        {
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) End");
            Console.WriteLine();

            string answer = Console.ReadLine();
            Console.WriteLine();

            switch (answer)
            {
                case "1":
                    Console.WriteLine(vendingMachine.Display());
                    Console.WriteLine();
                    break;
                case "2":
                    MenuTwo();
                    break;
                case "3":
                    Done = true;
                    break;
                case "9":
                    vendingMachine.PrintSalesReport();
                    break;
                default:
                    Console.WriteLine("Invalid input, please try again.");
                    Console.WriteLine();
                    break;
            }
        }

        public void MenuTwo()
        {
            bool menuTwoDone = false;

            while (!menuTwoDone)
            {
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine($"Current Money Provided: ${vendingMachine.Balance}");
                Console.WriteLine();

                string answer = Console.ReadLine();
                Console.WriteLine();

                switch (answer)
                {
                    case "1":
                        FeedMoney(answer);
                        break;

                    case "2":
                        Purchase(answer);
                        break;

                    case "3":
                        menuTwoDone = true;

                        Console.WriteLine(vendingMachine.ReturnChange(vendingMachine.Balance));
                        Console.WriteLine();

                        vendingMachine.PrintAudit();
                        break;

                    default:
                        Console.WriteLine("Invalid input, please try again.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        public void FeedMoney(string answer)
        {
            bool donePaying = false;
            bool isValid = false;

            while (!donePaying)
            {
                Console.WriteLine("INSERT DOLLAR BILL$ NOW (1, 2, 5, or 10)");

                answer = Console.ReadLine();
                Console.WriteLine();

                isValid = vendingMachine.FeedMoney(answer);

                if (isValid)
                {
                    Console.Write("Would you like to add more funds? (Y/N) ");
                    Console.WriteLine();

                    answer = Console.ReadLine().ToLower();
                    Console.WriteLine();

                    if (answer == "y")
                    {
                        donePaying = false;
                    }
                    else if (answer == "n")
                    {
                        donePaying = true;
                    }
                    else
                    {
                        donePaying = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry, please try again.");
                    Console.WriteLine();
                }
            }
        }

        public void Purchase(string answer)
        {
            Console.WriteLine(vendingMachine.Display());

            Console.WriteLine("Please enter the slot identifier: ");
            answer = Console.ReadLine().ToLower();
            Console.WriteLine();

            string purchaseMessage = vendingMachine.Purchase(answer);
            Console.WriteLine(purchaseMessage);

            if (!purchaseMessage.Contains('!'))
            {
                Console.WriteLine(vendingMachine.ConsumptionMessage(answer));
            }

            Console.WriteLine();
        }
    }


}

