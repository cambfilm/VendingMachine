﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Capstone.Classes
{
    public class PurchaseLog
    {

        public string Path { get; }

        public PurchaseLog (string path)
        {
            Path = path;
        }

        public void PrintLog(string line)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path, true))
                {
                    sw.WriteLine(line);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("File could not be created.");
            }
        }
        public string PrintFeedMoney(int dollars, decimal balance)
        {
            return ($"{DateTime.Now} \t FEED MONEY \t {dollars.ToString("C")}\t {balance.ToString("C")}");  
        }
        public string PrintPurchase (string itemName, decimal price, decimal balance)
        {
            return ($"{DateTime.Now} \t {itemName} \t {price.ToString("C")}\t {balance.ToString("C")}");

        }
        public string PrintChange (decimal balance)
        {
            return ($"{DateTime.Now} \t GIVE CHANGE \t {balance.ToString("C")}\t {0.00.ToString("C")}");

        }
    }
    
}
