﻿using InventoryManager.Exceptions;
using InventoryManager.Inventory;
using InventoryManager.Products;

namespace InventoryManagerTests
{
    public class InventoryTest: IDisposable
    {
        public void Dispose()
        {
            Inventory.GetProducts().Clear();
        }

        [Fact]
        public void TestAddProduct() {
            Product testProduct = new("test1", 25.1f, 2);

            Inventory.AddProduct(testProduct);

            Assert.NotNull(Inventory.GetProducts());
            Assert.True(Inventory.GetProducts().Count != 0);
            Assert.Equal(Inventory.GetProducts().ElementAt(0), testProduct);
        }

        [Fact]
        public void TestEditExistingProduct()
        {
            Product testProduct = new("test1", 25.1f, 2);
            Inventory.AddProduct(testProduct);

            Product modifiedTestProduct = new("test1", 35.5f, 10);
            Inventory.EditProduct(modifiedTestProduct);

            Product? resultOfEdit = Inventory.GetProducts().Find(p => p.Name.Equals("test1", StringComparison.Ordinal));

            Assert.NotNull(resultOfEdit);
            Assert.Equivalent(modifiedTestProduct, resultOfEdit);
        }

        [Fact]
        public void TestEditNonExistingProduct()
        {
            Product testProduct = new("modifyNotExistingTest2", 25.1f, 2);

            Assert.Throws<NoProductFoundException>(() => Inventory.EditProduct(testProduct));
        }

        [Fact]
        public void TestSearchExistingProduct()
        {
            Product testProduct = new("test1", 25.1f, 2);

            Inventory.AddProduct(testProduct);

            List<Product> searchResult = Inventory.SearchProduct("test");

            Assert.NotNull(searchResult);
            Assert.Contains(testProduct, searchResult);
            Assert.Equivalent(searchResult.ElementAt(0), testProduct);
        }

        [Fact]
        public void TestSearchNonExistingProduct()
        { 
            Assert.Throws<NoProductFoundException>(() => Inventory.SearchProduct("test not existing product"));
        }
    }
}
