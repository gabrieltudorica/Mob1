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
        static decimal userAmount;
        static string[][] inventory = new string[48][];
        static void Main(string[] args)
        {
            InitializeInventory();
            while (true)
            {
                if (userAmount == 0)
                {
                    Console.WriteLine("Please enter money!");
                    ReceiveMoney();
                }

                Console.WriteLine("Please enter product key or cancel the transaction");
                string selectedProductKey = Console.ReadLine();

                if (!IsProductKeyValid(selectedProductKey))
                {
                    GiveChange();
                    continue;
                }

                string[] selectedProduct = GetProductById(selectedProductKey);

                if (selectedProduct[2] == "0")
                {
                    Console.WriteLine("The selected product is out of stock");
                    continue;
                }

                if (!IsAmountEnoughForProduct(selectedProduct[1]))
                {
                    Console.WriteLine("Not enough money for the selected product");
                    Console.WriteLine("Please enter money or cancel the transaction");
                    ReceiveMoney();

                    continue;
                }
                PurchaseProduct(selectedProductKey);
            }
        }
        private static void InitializeInventory ()
        {
            string path = "inventory.txt";
            string[] readInventory = File.ReadAllLines(path);
            for(int i = 1; i < readInventory.Length; i++)
            {
                inventory[i-1] = readInventory[i].Split(',');
            }

            DisplayInventory();
        }
        private static void DisplayInventory ()
        {
            foreach (string[] product in inventory)
            {
                if(product != null)
                {
                   Console.WriteLine(product[3] + " " + product[0] + " " + product[1] + " LEI");
                }
            }
        }

        private static void ReceiveMoney()
        {
            decimal input;
            if (!decimal.TryParse(Console.ReadLine(), out input))
            {
                GiveChange();
                return;
            }

            userAmount += input;
            Console.WriteLine("Entered amount " + userAmount);
        }

        private static void GiveChange()
        {
            Console.WriteLine("Change given " + userAmount);
            userAmount = 0;
        }

        private static bool IsProductKeyValid(string selectedKey)
        {
            foreach (string[] product in inventory)
            {
                if (product != null && selectedKey == product[3])
                {
                    return true;
                }
            }

            return false;
        }
         private static string[] GetProductById(string selectedProduct)
        {
            foreach (string[] product in inventory)
            {
                if (product[3] == selectedProduct)
                {
                    return product;
                }
            }

            return null;
        }

        private static bool IsAmountEnoughForProduct(string productPrice)
        {
            if (decimal.Parse(productPrice) > userAmount)
            {
                return false;
            }

            return true;
        }

        private static void PurchaseProduct(string productKey)
        {
            string[] selectedProduct = GetProductById(productKey);
            userAmount -= decimal.Parse(selectedProduct[1]);
                        
            int updatedStock = Convert.ToInt32(selectedProduct[2]) - 1;
            selectedProduct[2] = updatedStock.ToString();
            Console.WriteLine("The transaction is successful. Money left: " + userAmount);

            if (updatedStock == 0)
            {
                SendSMS("You are out of " + selectedProduct[0]);
            }
        }

        private static void SendSMS(string message)
        {
            string parameter = string.Format("/C \"C:\\Users\\anamaria.totan\\Downloads\\curl-7.68.0-win64-mingw\\curl-7.68.0-win64-mingw\\bin\\curl --request POST --header \"X-Authorization: {0}\" \"https://app.smso.ro/api/v1/send\" -d \"sender=4\" -d \"to={1}\" -d \"body={2}\"\"",
                "api-key",
                "+40number",
                message);
            Process.Start("cmd.exe", parameter);
        }

    }
}
