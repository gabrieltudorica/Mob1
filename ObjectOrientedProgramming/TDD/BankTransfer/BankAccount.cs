using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankTransfer
{
    public class BankAccount
    {
        public decimal Amount;
        public string AccountNumber { get; set; }

        public BankAccount(string accountNumber)
        {
            AccountNumber = accountNumber;
        }
        
        public void AddMoney(decimal amount)
        {
            Amount += amount;
        }
        public void SendMoney(decimal amount, BankAccount bankAccount)
        {
            Amount -= amount;
            bankAccount.AddMoney(amount);
        }
    }
}
