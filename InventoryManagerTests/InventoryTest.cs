using InventoryManager.Exceptions;
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
        public void TestAddExistingProduct()
        {
            Product testProduct = new("test1", 25.1f, 2);
            Inventory.AddProduct(testProduct);

            Product existingProduct = new("test1", 30.2f, 5);
            Assert.Throws<ProductAlreadyExistsException>(() => Inventory.AddProduct(existingProduct));
        }

        [Fact]
        public void TestEditExistingProduct()
        {
            Product testProduct = new("test1", 25.1f, 2);
            Inventory.AddProduct(testProduct);

            Product modifiedTestProduct = new("test1", 35.5f, 10);
            Inventory.EditProduct("test1", modifiedTestProduct);

            Product? resultOfEdit = Inventory.GetProducts().Find(p => p.Name.Equals("test1", StringComparison.Ordinal));

            Assert.NotNull(resultOfEdit);
            Assert.Equivalent(modifiedTestProduct, resultOfEdit);
        }

        [Fact]
        public void TestEditNonExistingProduct()
        {
            Product testProduct = new("modifyNotExistingTest2", 25.1f, 2);

            Assert.Throws<NoProductFoundException>(() => Inventory.EditProduct("modifyNotExistingTest2", testProduct));
        }

        [Fact]
        public void TestDeleteExistingProduct()
        {
            Product deletedProduct = new("test1", 25.1f, 2);
            Inventory.AddProduct(deletedProduct);

            Product testProduct = new("test2", 35.5f, 10);
            Inventory.AddProduct(testProduct);

            Inventory.DeleteProduct(deletedProduct);


            Assert.True(Inventory.GetProducts().Count == 1);
            Assert.Null(Inventory.GetProducts().Find(p => p.Name.Equals(deletedProduct.Name)));
        }

        [Fact]
        public void TestDeleteNonExistingProduct()
        {
            Product testProduct = new("modifyNotExistingTest2", 25.1f, 2);

            Assert.Throws<NoProductFoundException>(() => Inventory.DeleteProduct(testProduct));
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
