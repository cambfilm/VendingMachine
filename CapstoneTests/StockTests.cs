using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void StockListIsProperlyCreatedFromTextFile()
        {
            Stocker stock = new Stocker("vendingmachine.csv");

            List<Item> items = stock.CreateStockList();

            string itemName = items[6].Name;
            decimal itemPrice = items[6].Price;

            Assert.AreEqual("Wonka Bar", itemName);
            Assert.AreEqual((decimal)1.50, itemPrice);

        }
    }
}
