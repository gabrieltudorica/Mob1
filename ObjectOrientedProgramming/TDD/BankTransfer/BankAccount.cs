using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankTransfer
{
    public class BankAccount
    {
        public decimal Amount { get; private set; }
        public string AccountNumber { get; private set; }

        public List<string> Transactions;

        public BankAccount(string accountNumber)
        {
            AccountNumber = accountNumber;
            Transactions = new List<string>();
        }
        
        public void AddMoney(decimal amount)
        {
            if (amount <= 0)
            {
                throw new NegativeOrZeroAmountException();
            }

            Amount += amount;
            Transactions.Add(amount + " was added to your account");

        }
        public string SendMoney(decimal amount, BankAccount bankAccount)
        {
            if (amount <= Amount)
            {
                bankAccount.AddMoney(amount);
                Amount -= amount;

                Transactions.Add(amount + " was taken from your account");

                return "Your transaction is successful";
            }

            return "Your transaction was declined due to unsufficient funds";
        }
    }
}
