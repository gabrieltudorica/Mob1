using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {
        static decimal userAmount;
        static string[][] inventory = new string[3][];
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
            string[] pepsi = { "pepsi", "5", "10", "11" };
            string[] cola = { "coca-cola", "5", "9", "12" };
            string[] sicola = { "sicola", "2.5", "5", "13" };

            inventory[0] = pepsi;
            inventory[1] = cola;
            inventory[2] = sicola;

            DisplayInventory();
        }
        private static void DisplayInventory ()
        {
            foreach (string[] product in inventory)
            {
                Console.WriteLine(product[3] + " " + product[0] + " " + product[1] + " LEI");
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
                if (selectedKey == product[3])
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
        }

    }
}
