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
            bankAccount.AddMoney(amountSent);
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
            bankAccount.AddMoney(amountAdded);

            //act
            bankAccount.SendMoney(amountAdded, secondaccount);

            //assert
            Assert.AreEqual(amountAdded, secondaccount.Amount);
        }

        [TestMethod]
        public void WhenAmmountIsZero_SendMoney_InitialAmountIsNotChanged()
        {
            //arrange
            var secondaccount = new BankAccount("INGB2");
            decimal initialAmount = bankAccount.Amount;
            decimal amountSent = 3;

            //act
            bankAccount.SendMoney(amountSent, secondaccount);

            //assert
            Assert.AreEqual(initialAmount, bankAccount.Amount);
        }

        [TestMethod]
        public void WhenAmmountSentIsLargerThanAmmount_SendMoney_InitialAmountIsNotChanged()
        {
            //arrange
            var secondaccount = new BankAccount("INGB2");
            bankAccount.AddMoney(2);
            decimal initialAmount = bankAccount.Amount;
            decimal amountSent = 3;

            //act
            bankAccount.SendMoney(amountSent, secondaccount);

            //assert
            Assert.AreEqual(initialAmount, bankAccount.Amount);
        }

        [TestMethod]
        public void WhenSufficientFunds_SendMoney_ReturnsSuccessfulMessage()
        {
            //arrange
            var secondaccount = new BankAccount("INGB2");
            bankAccount.AddMoney(4);
            decimal amountSent = 3;

            //act
            string messageReturned = bankAccount.SendMoney(amountSent, secondaccount);

            //assert
            Assert.AreEqual("Your transaction is successful", messageReturned);
        }

        [TestMethod]
        public void WhenInsufficientFunds_SendMoney_ReturnsUnsuccessfulMessage()
        {
            //arrange
            var secondaccount = new BankAccount("INGB2");
            bankAccount.AddMoney(2);
            decimal amountSent = 3;

            //act
            string messageReturned = bankAccount.SendMoney(amountSent, secondaccount);

            //assert
            Assert.AreEqual("Your transaction was declined due to unsufficient funds", messageReturned);
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeOrZeroAmountException))]
        public void WhenAddingNegativeAmmount_AddMoney_ThrowsNegativeOrZeroAmmountException()
        {
            //act
            bankAccount.AddMoney(-3);
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeOrZeroAmountException))]
        public void WhenAddingZeroAmmount_AddMoney_ThrowsNegativeOrZeroAmmountException()
        {
            //act
            bankAccount.AddMoney(0);
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeOrZeroAmountException))]
        public void WhenSendingNegativeAmmount_SendMoney_ThrowsNegativeOrZeroAmmountException()
        {
            //arrange
            var secondaccount = new BankAccount("INGB2");

            //act
            bankAccount.SendMoney(-3, secondaccount);
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeOrZeroAmountException))]
        public void WhenSendingZeroAmmount_SendMoney_ThrowsNegativeOrZeroAmmountException()
        {
            //arrange
            var secondaccount = new BankAccount("INGB2");

            //act
            bankAccount.SendMoney(0, secondaccount);
        }


        [TestMethod]

        public void WhenBankAccountIsInitialize_Transactions_IsEmpty()
        {
            //Act
            int transactionsCount = bankAccount.Transactions.Count;

            //Assert
            Assert.AreEqual(0, transactionsCount);

        }
        
        
        [TestMethod]

        public void AddMoney_AddsTransactionToHistory()
        {
            
           int currentTransactionsNumber = bankAccount.Transactions.Count;
            int expectedTransactionsNumber = currentTransactionsNumber + 1;
            decimal addedAmount = 5;

            //act
            bankAccount.AddMoney(addedAmount);

            //assert
            Assert.AreEqual(expectedTransactionsNumber, bankAccount.Transactions.Count);
            Assert.AreEqual(addedAmount + " was added to your account", bankAccount.Transactions[0]);
        }

        [TestMethod]
        public void SendMoney_AddsTransactionToHistoryToBothAccounts()
        {
            //arrange
            var secondaccount = new BankAccount("INGB2");
            bankAccount.AddMoney(10);

            int currentTransactionsFirstAccount = bankAccount.Transactions.Count;
            int expectedTransactionsFirstAccount = currentTransactionsFirstAccount + 1;
            int currentTransactionsSecondAccount = secondaccount.Transactions.Count;
            int expectedTransactionsSecondAccount = currentTransactionsSecondAccount + 1;
            decimal addedAmount = 5;

            //act
            bankAccount.SendMoney(addedAmount, secondaccount);

            //assert
            Assert.AreEqual(expectedTransactionsSecondAccount, secondaccount.Transactions.Count);
            Assert.AreEqual(addedAmount + " was added to your account", secondaccount.Transactions[0]);
            Assert.AreEqual(expectedTransactionsFirstAccount, bankAccount.Transactions.Count);
            Assert.AreEqual(10 + " was added to your account", bankAccount.Transactions[0]);
            Assert.AreEqual(addedAmount + " was taken from your account", bankAccount.Transactions[1]);
        }
    }
}
