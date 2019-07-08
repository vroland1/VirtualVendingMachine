using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTest
    {
        VendingMachine newObject;
        VendingMachineItem newItem;

        [TestInitialize]
        public void Initialize()
        {
            newObject = new VendingMachine(@"C:\Users\georgd\georgdeckner-c-sharp-material\team5-c-sharp-week4-pair-exercises\19_Mini-Capstone\etc\vendingmachine.csv");
            newItem = new VendingMachineItem();
        }

        
        [TestMethod]
        public void FeedMoneyTest()
        {
            Assert.AreEqual(false, newObject.FeedMoney("f"), "Expected false, returned true");
            Assert.AreEqual(false, newObject.FeedMoney("54"), "Expected false, returned true");
            Assert.AreEqual(false, newObject.FeedMoney("2.45"), "Expected false, returned true");
            Assert.AreEqual(true, newObject.FeedMoney("1.00"), "Expected true, returned false");
            Assert.AreEqual(true, newObject.FeedMoney("1"), "Expected true, returned false");
            Assert.AreEqual(true, newObject.FeedMoney("2.00"), "Expected true, returned false");
            Assert.AreEqual(true, newObject.FeedMoney("2"), "Expected true, returned false");
            newObject.Balance = 0;
            newObject.FeedMoney("5");
           
            Assert.AreEqual(5, newObject.Balance, "Expected 5 but returned " + newObject.Balance);
            newObject.FeedMoney("10");
            Assert.AreEqual(15, newObject.Balance, "Expected 15 but returned " + newObject.Balance);

            
        }

        [TestMethod]
        public void ReturnChange()
        {
            Assert.AreEqual("Your change is 22 quarters, 0 dimes, 0 nickels, and 0 pennies, for a total of $5.5", newObject.ReturnChange((decimal)5.5));
            Assert.AreEqual("Your change is 13 quarters, 2 dimes, 0 nickels, and 0 pennies, for a total of $3.45", newObject.ReturnChange((decimal)3.45));
            Assert.AreEqual("Your change is 1 quarters, 1 dimes, 1 nickels, and 0 pennies, for a total of $0.4", newObject.ReturnChange((decimal)0.4));
            Assert.AreEqual("Your change is 5 quarters, 2 dimes, 0 nickels, and 4 pennies, for a total of $1.49", newObject.ReturnChange((decimal)1.49));
        }
        
        [TestMethod]
        public void PurchaseTest()
        {
            newObject.Balance = 0;

            Assert.AreEqual("Not enough funds!", newObject.Purchase("a2"));
            newObject.Balance = 10;
            Assert.AreEqual("You have successfully purchased Potato Crisps", newObject.Purchase("a1"));
            newObject.Balance = 10;
            newObject.clone[0].Quantity = 0;
            Assert.AreEqual("Item is sold out!", newObject.Purchase("a1"));
            Assert.AreEqual("Item not found!", newObject.Purchase("a6"));
            newObject.clone[0].Quantity = 5;
            newObject.Purchase("a1");
            Assert.AreEqual(4, newObject.clone[0].Quantity);
            //newObject.

            //Assert.AreEqual("You have successfully purchased Potato Crisps", newObject.Purchase("A1"));
        }

        [TestMethod]
        public void ConsumptionMethod()
        {
            Assert.AreEqual("Crunch Crunch, Yum!", newObject.ConsumptionMessage("A1"));
            Assert.AreEqual("Munch Munch, Yum!", newObject.ConsumptionMessage("B3"));
            Assert.AreEqual("Glug Glug, Yum!", newObject.ConsumptionMessage("c1"));
            Assert.AreEqual("Chew Chew, Yum!", newObject.ConsumptionMessage("d2"));
        }
    }
}
