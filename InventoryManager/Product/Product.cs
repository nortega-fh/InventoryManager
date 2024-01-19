using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Products
{
    public class Product(string name, float price, int quantity)
    {
        public static readonly string Header = "\tName\t|\tQuantity\t|\tPrice";
        public string Name { get; set; } = name;
        public float Price { get; set; } = price;
        public int Quantity { get; set; } = quantity;

        public override string ToString() => $"{Name}\t|\t{Quantity}\t\t|\t{Price}";
        
    }
}
