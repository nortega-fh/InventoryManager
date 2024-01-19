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
        
        public static void AddProduct(Product product) => products.Add(product);
        
    }
}
