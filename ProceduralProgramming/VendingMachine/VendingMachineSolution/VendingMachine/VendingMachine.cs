﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class VendingMachine
    {
        private List<Product> inventory = new List<Product>();
        public decimal UserAmount { get; private set; }
        private ISMSGateway SMSGateway;
        public VendingMachine(ISMSGateway SMSGateway)
        {
            InitializeInventory();
        }
        private void InitializeInventory()
        {
            string path = "inventory.txt";
            string[] readInventory = File.ReadAllLines(path);
           
            for (int i = 1; i < readInventory.Length; i++)
            {
                string[] productData = readInventory[i].Split(',');
                Product product = new Product(productData[3], productData[0]);
                product.Price = Convert.ToDecimal(productData[1]);
                product.Stock = Convert.ToInt32(productData[2]);
                inventory.Add(product);
            }
        }
        public void AddMoney(decimal amount)
        {       
            UserAmount += amount;        
        }

        public decimal GiveChange()
        {
            decimal change = UserAmount;
            UserAmount = 0;
            return change;
        }

        private  Product GetProductById(string selectedProduct)
        {
            foreach (Product product in inventory)
            {
                if (product.Key == selectedProduct)
                {
                    return product;
                }
            }

            return null;
        }

        private  bool IsAmountEnoughForProduct(decimal productPrice)
        {
            if (productPrice > UserAmount)
            {
                return false;
            }

            return true;
        }

        public string PurchaseProduct(string productKey)
        {           
            Product selectedProduct = GetProductById(productKey);
            string purchaseResult = GetPurchaseResult(selectedProduct);
           if ( purchaseResult!= string.Empty)
            {
                return purchaseResult;
            }
            UserAmount -= selectedProduct.Price;

            selectedProduct.Stock = selectedProduct.Stock - 1;
            
            if (selectedProduct.Stock == 0)
            {
                //SendSMS("You are out of " + selectedProduct.Name);
            }
            return "The transaction is successful. Money left: " + UserAmount;
        }
        public List<Product> GetProducts()
        {
            return inventory;
        }
        private string GetPurchaseResult (Product selectedProduct)
        {
            if (selectedProduct == null)
            {
                return "Error: please enter a valid product key. Money left: " + UserAmount;
            }

            if (!IsAmountEnoughForProduct(selectedProduct.Price))
            {
                return "Not enough money for the selected product. Please enter money or cancel the transaction. Money left " + UserAmount;
            }

            if (selectedProduct.Stock == 0)
            {
                return "The selected product is out of stock. Please choose another product. Money left " + UserAmount;
            }

            return string.Empty;
        }
    }
}
