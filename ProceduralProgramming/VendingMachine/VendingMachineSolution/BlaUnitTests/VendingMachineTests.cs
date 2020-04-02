using System;
using Bla;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlaUnitTests
{   
    [TestClass]
    public class VendingMachineTests
    {
        ISMSGateway smsGateway;
        VendingMachine vendingMachine;

        [TestInitialize]
        public void TestSetup()
        {
            smsGateway = new SmsTestClass();
            vendingMachine = new VendingMachine(smsGateway);
        }

        [TestCleanup]
        public void TestCleanup()
        { 

        }

        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            int moneyToAdd = 20;

            //act
            vendingMachine.AddMoney(moneyToAdd);

            //assert
            Assert.AreEqual(moneyToAdd, vendingMachine.UserAmount);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //arrange
            int moneyToAdd = -20;

            //act
            vendingMachine.AddMoney(moneyToAdd);

            //assert
            Assert.AreEqual(moneyToAdd, vendingMachine.UserAmount);
        }

        [TestMethod]
        public void WhenClientPurchasesAProductAndStockIsZero_Purchase_SendsSmsToOwner()
        {
            //arrange
            var orangeSms = new OrangeSMSGateway();
            vendingMachine = new VendingMachine(orangeSms);

            int moneyToAdd = 20;
            vendingMachine.AddMoney(moneyToAdd);

            //act
            vendingMachine.PurchaseProduct("11");

            //assert
            Assert.AreEqual(moneyToAdd, vendingMachine.UserAmount);
        }

        [TestMethod]
        public void TestMethod2()
        {
            //arrange
            decimal addedMoney = 12.5M;
            vendingMachine.AddMoney(addedMoney);

            //act
            decimal changeGiven = vendingMachine.GiveChange();

            //assert
            Assert.AreEqual(addedMoney, changeGiven);
        }        
    }
}
