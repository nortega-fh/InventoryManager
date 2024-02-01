﻿using InventoryManager.Exceptions;
using InventoryManager.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Inventory
{
    public class Inventory
    {
        private static List<Product> products = [];

        public static List<Product> GetProducts() => products;

        public static void AddProduct(Product product)
        {
            Product? productInInventory = products.Find(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
            if (productInInventory is null)
            {
                products.Add(product);
                return;
            }
            throw new ProductAlreadyExistsException(product.Name);
        }

        public static void EditProduct(string name, Product product)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == name)
                {
                    products[i] = product;
                    return;
                }
            }
            throw new NoProductFoundException(product.Name);
        }

        public static void DeleteProduct(Product product)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == product.Name)
                {
                    products.RemoveAt(i);
                    return;
                }
            }
            throw new NoProductFoundException(product.Name);
        }
   
        public static List<Product> SearchProduct(string name)
        {
            List<Product> searchedProducts = products.FindAll(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            if(searchedProducts.Count == 0) 
            { 
                throw new NoProductFoundException(name); 
            }
            return searchedProducts;
        }
        
    }
}
