using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] pepsi = { "pepsi", "5", "10", "11" };
            string[] cola = { "coca-cola", "5", "9", "12" };
            string[] sicola = { "sicola", "2.5", "5", "13" };
            string[][] vendingMachineStationQ7 = { pepsi, cola, sicola };
            foreach (string[] product in vendingMachineStationQ7)
            {
                Console.WriteLine(product[3] + " " + product[0] + " " + product[1] + " LEI");
            }

            Console.WriteLine("Please enter money");
            decimal userAmount = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Entered amount " + userAmount);
            Console.WriteLine("Please enter product key");
            string selectedProductKey = Console.ReadLine();
            // decimal userChange=

            string[] selectedProduct = GetProductById(selectedProductKey, vendingMachineStationQ7);

            if (decimal.Parse(selectedProduct[1]) > userAmount)
            {
                Console.WriteLine("Not enough money for the selected product");
            }
            else
            {
                userAmount -= decimal.Parse(selectedProduct[1]);
                Console.WriteLine("The transaction is successful. Money left: " + userAmount);
                int productStock = Convert.ToInt32(selectedProduct[2]) - 1;
                selectedProduct[2] = productStock.ToString();
            }


            Console.Read();

        }
         private static string[] GetProductById(string selectedProduct, string [][]inventory)
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
    }
}
