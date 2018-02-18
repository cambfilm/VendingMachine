using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void MakeChangeGivesCorrectChange()
        {
            Transaction trans = new Transaction();

            trans.MoneyGivenInPennies = 80;
            trans.MakeChange();

            string result = trans.ChangeGiven;

            Assert.AreEqual("3 quarters. 1 nickels. ", result);

        }
    }
}
