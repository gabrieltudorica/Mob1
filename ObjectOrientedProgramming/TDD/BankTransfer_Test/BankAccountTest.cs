using System;
using BankTransfer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//Write some code to transfer a specified amount of money from one bank account (the payer) to another (the payee)
//Write some code to keep a record of the transfer for both bank accounts in a transaction history
//Write some code to query a bank account transaction history for any bank transfers to or from a specific account
/*
 WhenCondition_CUT_Outcome
 TO DO for HW:
- send money - sa ai destui bani in cont
- amount sa nu poate fi setat singur
- pe addmoney se pot da momentan valori negative
 */
namespace BankTransfer_Test
{
    [TestClass]
    public class BankAccountTest
    {
       static string accountNumber = "INGB00009999";
       BankAccount bankAccount = new BankAccount(accountNumber);

        [TestMethod]
        public void WhenAccountIsCreated_Amount_IsZero()
        {
            //act
            decimal amount = bankAccount.Amount;

            //assert
            Assert.AreEqual(0, amount);
        }

        [TestMethod]
        public void AddMoney_InitializeAmountWhithEnteredValue()
        {
            //arrange
            decimal amountAdded = 13;

            //act
            bankAccount.AddMoney(amountAdded);

            //assert
            Assert.AreEqual(amountAdded, bankAccount.Amount);
        }

        [TestMethod]
        public void WhenAccountIsCreated_AccountNumber_IsNotNull()
        {
            //act
            string accountNumber = bankAccount.AccountNumber;

            //Assert
            Assert.IsNotNull(accountNumber);
        }

        [TestMethod]
        public void WhenAccountIsCreatedWithNumber_AccountNumber_IsSet()
        {
            //act
            string actualAccountNumber = bankAccount.AccountNumber;

            //Assert
            Assert.AreEqual(accountNumber, actualAccountNumber);
        }

        [TestMethod]
        public void SendMoney_DecreasesAmountWhithEnteredValue()
        {
            //arrange
            decimal amountSent = 13;
            decimal actualAmount = bankAccount.Amount;
            decimal expectedAmount = actualAmount - amountSent;
            var otherBankAccount = new BankAccount("fsdbfhs");

            //act
            bankAccount.SendMoney(amountSent, otherBankAccount);

            //assert
            Assert.AreEqual(expectedAmount, bankAccount.Amount);
        }

        [TestMethod]
        public void WhenMakingMultipleDeposits_AddMoney_IncreasesAmountWhithEnteredValue()
        {
            //arrange
            decimal amountAdded = 13;
            decimal expected = amountAdded * 2;
            bankAccount.AddMoney(amountAdded);

            //act
            bankAccount.AddMoney(amountAdded);

            //assert
            Assert.AreEqual(expected, bankAccount.Amount);
        }

        [TestMethod]
        public void WhenTransferingMoney_SendMoney_IncreasesAmountWhithEnteredValueToTheOtherAccount()
        {
            //arrange
            var secondaccount = new BankAccount("INGB2");
            decimal amountAdded = 13;

            //act
            bankAccount.SendMoney(amountAdded, secondaccount);

            //assert
            Assert.AreEqual(amountAdded, secondaccount.Amount);
        }
    }
}
