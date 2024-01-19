using InventoryManager.Exceptions;
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

        public static void EditProduct(Product product)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == product.Name)
                {
                    products[i] = product;
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
