using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {
        private static List<Product> inventory;

        //static decimal userAmount;
       // static string[][] inventory = new string[48][];
        static void Main(string[] args)
        {
            ISMSGateway gateway = new OrangeSMSGateway();
            VendingMachine vendingMachine = new VendingMachine(gateway);

            inventory = vendingMachine.GetProducts();
            DisplayInventory();

            Console.WriteLine("Please enter money!");
            vendingMachine.AddMoney(Convert.ToDecimal(Console.ReadLine()));
            Console.WriteLine("Entered amount " + vendingMachine.UserAmount);

            Console.WriteLine("Please enter product key or cancel the transaction");
            string purchaseOutput = vendingMachine.PurchaseProduct(Console.ReadLine());

            Console.WriteLine(purchaseOutput);

            decimal change = vendingMachine.GiveChange();
            Console.WriteLine("Your change is: " + change);
            Console.Read();

            /*  while (true)
              {
                  if (userAmount == 0)
                  {
                      Console.WriteLine("Please enter money!");
                      ReceiveMoney();
                      Console.WriteLine("Entered amount " + userAmount);
                  }

                  Console.WriteLine("Please enter product key or cancel the transaction");
                  string selectedProductKey = Console.ReadLine();

                  if (!IsProductKeyValid(selectedProductKey))
                  {
                      GiveChange();
                      continue;
                  }

                  Product selectedProduct = GetProductById(selectedProductKey);

                  if (selectedProduct.Stock == 0)
                  {
                      Console.WriteLine("The selected product is out of stock");
                      continue;
                  }

                  if (!IsAmountEnoughForProduct(selectedProduct.Price))
                  {
                      Console.WriteLine("Not enough money for the selected product");
                      Console.WriteLine("Please enter money or cancel the transaction");
                      ReceiveMoney();

                      continue;
                  }
                  PurchaseProduct(selectedProductKey);
              }*/
        }
        private static void DisplayInventory ()
        {
            foreach (Product product in inventory)
            {                
                   Console.WriteLine(product.Key + " " + product.Name + " " + product.Price + " LEI");
                
            }
        }
        private static void SendSMS(string message)
        {
            string parameter = string.Format("/C \"C:\\Users\\ana\\Downloads\\curl-7.68.0-win64-mingw\\curl-7.68.0-win64-mingw\\bin\\curl --request POST --header \"X-Authorization: {0}\" \"https://app.smso.ro/api/v1/send\" -d \"sender=4\" -d \"to={1}\" -d \"body={2}\"\"",
                "api-key",
                "+40number",
                message);
            Process.Start("cmd.exe", parameter);
        }

    }
}
