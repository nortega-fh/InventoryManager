using InventoryManager.Inventory;
using InventoryManager.Products;

namespace InventoryManagerTests
{
    public class InventoryTest
    {
        [Fact]
        public void TestAddProduct() {
            Product testProduct = new("test1", 25.1f, 2);

            Inventory.AddProduct(testProduct);

            Assert.NotNull(Inventory.GetProducts());
            Assert.True(Inventory.GetProducts().Count != 0);
            Assert.Equal(Inventory.GetProducts().ElementAt(0), testProduct);
        }
    }
}
