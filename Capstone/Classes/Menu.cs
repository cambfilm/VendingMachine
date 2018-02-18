using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class Menu
    {
        VendingMachine vm = new VendingMachine("vendingmachine.csv");
        PurchaseLog log = new PurchaseLog("Log.txt");

        private void PrintHeader()
        {
            Console.WriteLine("Welcome to our lovely vending machine!!");
            Console.WriteLine();

        }
        public void Display()
        {
            PrintHeader();
            while (true)
            {
                Console.WriteLine("MAIN MENU");
                Console.WriteLine();
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase a Delicious Treat");
                Console.WriteLine("(3) Quit");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    DisplayItems();
                    Console.WriteLine();
                }
                else if (input == "2")
                {
                    Purchase();
                }
                else if (input == "3")
                {
                    break;
                }
            }
        }
        public void Purchase()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("PURCHASE MENU");
                Console.WriteLine();
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine($"Current Balance: {vm.Trans.BalanceInDollars.ToString("C")}");
                string input = Console.ReadLine();
                Console.WriteLine();

                if (input == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("Please insert a bill (we accept $1, $2, $5, or $10)");
                        string deposit = Console.ReadLine();
                        if (IsWholeDollar(deposit))
                        {
                            vm.FeedMoney(int.Parse(deposit));
                            Console.WriteLine();
                            Console.WriteLine($"Balance: {vm.Trans.BalanceInDollars.ToString("C")}");
                            Console.WriteLine();

                            break;
                        }

                    }
                }
                else if (input == "2")
                {
                    while (true)
                    {
                        Console.WriteLine();
                        DisplayItems();
                        Console.WriteLine();
                        Console.Write("Please select your purchase, or press (Q) to go back: ");
                        input = Console.ReadLine();
                        Console.WriteLine();
                        if (input == "Q" || input == "q")
                        {
                            break;
                        }
                        else if (!IsLocationValid(input))
                        {
                            Console.WriteLine("Your location is invalid.");
                            Console.WriteLine();
                        }
                        else if (!IsInStock(input))
                        {
                            Console.WriteLine("The item is out of stock.");
                            Console.WriteLine();
                        }
                        else if (!HasEnoughMoney(input))
                        {
                            Console.WriteLine($"You don't have enough money. Your balance is { vm.Trans.BalanceInDollars.ToString("C")}.");
                            Console.WriteLine();
                        }                       
                        else
                        {
                            vm.PurchaseItem(input);
                            break;
                        }
                    }
                }
                else if (input == "3")
                {
                    Console.WriteLine();
                    Console.WriteLine($"Your change is {vm.Trans.BalanceInDollars.ToString("C")}");
                    vm.Trans.FinishTransaction();
                    Console.WriteLine(vm.Trans.ChangeGiven);
                    ConsumeItems();
                    break;
                }
            }
        }

        private bool HasEnoughMoney(string location)
        {
            if (vm.Slots[location].Items[0].Price > vm.Trans.BalanceInDollars)
            {
                return false;
            }
            return true;
        }

        private bool IsInStock(string location)
        {
            if (vm.Slots[location].IsEmpty)
            {
                return false;
            }

            return true;
        }

        public void DisplayItems()
        {
            foreach (string location in locations)
            {
                Console.WriteLine(vm.GetItemInfo(location));
            }
        }
        public bool IsWholeDollar(string deposit)
        {
            if (deposit != "1" && deposit != "2" && deposit != "5" && deposit != "10")
            {
                return false;
            }
            return true;
        }
        public bool IsLocationValid(string location)
        {
            if (!locations.Contains(location))
            {
                return false;
            }
            return true;

        }

        public void ConsumeItems()
        {
            foreach (Item item in vm.PurchasedItems)
            {
                System.Threading.Thread.Sleep(750);
                Console.WriteLine(item.Consume());
            }
        }

        static string[] locations =
        {
            "A1", "A2", "A3", "A4",
            "B1", "B2", "B3", "B4",
            "C1", "C2", "C3", "C4",
            "D1", "D2", "D3", "D4"
        };

    }
}
