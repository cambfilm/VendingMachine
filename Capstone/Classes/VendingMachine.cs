using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        List<Item> stockList = new List<Item>();
        PurchaseLog log = new PurchaseLog("Log.txt");
        public Transaction Trans { get; set; } = new Transaction();

        static string[] locations =
        {
            "A1", "A2", "A3", "A4",
            "B1", "B2", "B3", "B4",
            "C1", "C2", "C3", "C4",
            "D1", "D2", "D3", "D4"
        };

        public Dictionary<string, Slot> Slots { get; set; } = new Dictionary<string, Slot>();

        public List<Item> PurchasedItems { get; set; } = new List<Item>(); // clear list afterwards



        public void FeedMoney(int dollars)
        {
            Trans.MoneyGivenInPennies += 100 * dollars;
            log.PrintFeedMoney(dollars, Trans.BalanceInDollars);// feedmoney log
        }

        public void PurchaseItem(string location)
        {
            if (!Slots.ContainsKey(location))
            {
                throw new VendingMachineException("Invalid slot selection.");
            }
            if (Slots[location].IsEmpty)
            {
                throw new VendingMachineException("Item out of stock.");
            }
            if (Slots[location].Price > Trans.BalanceInDollars)
            {
                throw new VendingMachineException("Not enough money provided.");
            }

            PurchasedItems.Add(GetItemFromSlot(location));
            Trans.TotalPurchasePriceInPennies += 100 * GetItemFromSlot(location).Price;
            log.PrintPurchase(GetItemFromSlot(location).Name, GetItemFromSlot(location).Price, Trans.BalanceInDollars);// purchase log
            Slots[location].RemoveItem();
        }

        private void MakeSlots()
        {
            foreach(string location in locations)
            {
                Slot slot = new Slot(location);
                Slots[location] = slot;
            }
        }

        public VendingMachine(string stockPath)
        {
            Stocker stock = new Stocker(stockPath);
            stockList = stock.CreateStockList();
            MakeSlots();
            StockSlots(stockList);

        }

        private void StockSlots(List<Item> stockList)
        {
            for (int i = 0; i < stockList.Count; i++)
            {
                Slots[locations[i]].AddItem(stockList[i]);
            }
        }

        public Item GetItemFromSlot(string location)
        {
            return Slots[location].Items[0];
        }

        public string GetItemInfo(string location)
        {
            string info = "";
            if (!Slots[location].IsEmpty)
            {
                if (GetItemFromSlot(location).Name.Length < 10)
                {
                    info = $"[{location}] -- {Slots[location].Items[0].Price.ToString("C")} {Slots[location].Items[0].Name}\t\t\t({Slots[location].AmountOfItems})";
                }
                else if (GetItemFromSlot(location).Name.Length >= 9 && GetItemFromSlot(location).Name.Length < 16)
                {
                    info = $"[{location}] -- {Slots[location].Items[0].Price.ToString("C")} {Slots[location].Items[0].Name}\t\t({Slots[location].AmountOfItems})";
                }
                else
                {
                info = $"[{location}] -- {Slots[location].Items[0].Price.ToString("C")} {Slots[location].Items[0].Name}\t({Slots[location].AmountOfItems})";
                }
            }
            else
            {
                info = $"[{location}] -- <<<SOLD OUT>>>";
                
            }

            return info;
        }
        
    }
}
