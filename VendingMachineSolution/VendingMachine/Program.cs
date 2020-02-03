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
            string[] pepsi = { "pepsi", "5","10","11"};
            string[] cola = { "coca-cola", "5", "9", "12"};
            string[] sicola = { "sicola", "2.5", "5", "13" };
            string[][] vendingMachineStationQ7 = { pepsi, cola, sicola };
            foreach (string[] product in vendingMachineStationQ7)
            {
                Console.WriteLine(product[3] + " " + product[0] + " " + product[1] + " LEI");
            }

            Console.WriteLine("Please enter money");
            decimal userAmount = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Entered amount " + userAmount);
        }
    }
}
