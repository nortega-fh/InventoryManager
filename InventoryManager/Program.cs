using InventoryManager.Exceptions;
using InventoryManager.Products;

namespace InventoryManager
{
    public class Program
    {
        private static void PrintSeeAllProducts()
        {
            Console.WriteLine("*****************************");
            if(Inventory.Inventory.GetProducts().Count > 0)
            {
                Console.WriteLine("\tProducts table\t");
                Console.WriteLine(Product.Header);
                Inventory.Inventory.GetProducts().ForEach(product => Console.WriteLine(product.ToString()));
            }
            else
            {
                Console.WriteLine("There are no products registered yet.");
            }
            Console.WriteLine("*****************************");
            Console.ReadLine();
            Console.Clear();
        }

        private static void PrintAddProductFlow() 
        {
            Console.WriteLine("Registering a new product");
            Console.WriteLine("Please type the product's name:");
            string? name = Console.ReadLine();
            while(name is null)
            {
                Console.WriteLine("Input is invalid, please try again.");
                Console.WriteLine("Please type the product's name:");
                name = Console.ReadLine();
            }
            Console.WriteLine("Please type the product's price");
            Console.WriteLine("Only type positive decimal numbers, write a comma for decimals i.e 11,11");
            float price = -1f;
            while(price <= 0f)
            {
                try
                {
                    string? priceString = Console.ReadLine();
                    while (priceString is null)
                    {
                        Console.WriteLine("Input is invalid, please try again.");
                        Console.WriteLine("Please type the product's price:");
                        priceString = Console.ReadLine();
                    }
                    price = float.Parse(priceString);
                } 
                catch (InvalidCastException)
                {
                    Console.WriteLine("The input couldn't be recognized as a number. Please try again.");
                }
            }

            Console.WriteLine("Please type the product's quantity in stock");
            Console.WriteLine("Only type positive, integer numbers");
            int quantity = -1;
            while (quantity < 0)
            {
                
                string? quantityString = Console.ReadLine();
                while (quantityString is null)
                {
                    Console.WriteLine("Input is invalid, please try again.");
                    Console.WriteLine("Please type the product's quantity in stock:");
                    quantityString = Console.ReadLine();
                }
                try
                {
                    quantity = int.Parse(quantityString);
                    if(quantity < 0)
                    {
                        Console.WriteLine("The quantity can't be negative, please try again");
                        continue;
                    }
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("The input couldn't be recognized as a number. Please try again.");
                }
            }
            Console.WriteLine("Creating a product with the following details:");
            Console.WriteLine($"Name: {name}, Price: {price}, Quantity: {quantity}");
            Console.WriteLine("Do you wish to continue? Y/N");
            string? answer = Console.ReadLine();
            while (true)
            {
                switch (answer)
                {
                    case "Y":
                    case "y":
                        try
                        {
                            Inventory.Inventory.AddProduct(new Product(name, price, quantity));
                            Console.WriteLine("Product registered succesfully");
                        }
                        catch (ProductAlreadyExistsException paee)
                        {
                            Console.WriteLine(paee.Message);
                        }
                        finally
                        {
                            Console.ReadLine();
                            Console.Clear();
                        }
                        return;
                    case "N":
                    case "n":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Please write y or Y for yes, n or N for no");
                        answer = Console.ReadLine();
                        break;
                }
            }
        }

        private static Product? ObtainProductFromSearch(string action)
        {
            Console.WriteLine($"Please type the name of the product that you want to {action}:");
            string? productName = Console.ReadLine();
            while (productName is null)
            {
                Console.WriteLine("Please write a product name");
                productName = Console.ReadLine();
            }
            try
            {
                List<Product> productsWithSimilarName = Inventory.Inventory.SearchProduct(productName);
                if(productsWithSimilarName.Count == 1)
                {
                    return productsWithSimilarName[0];
                }
                Console.WriteLine("Please type the number of the product you want to select:");
                for (int i = 1; i < productsWithSimilarName.Count + 1; i++)
                {
                    Console.WriteLine($"{i}. {productsWithSimilarName[i-1].Name}");
                }
                int selectedProduct = -1;
                while (selectedProduct <= 0)
                {
                    string? selectedProductString = Console.ReadLine();
                    while (selectedProductString is null)
                    {
                        Console.WriteLine("Please type a number to select a product");
                        selectedProductString = Console.ReadLine();
                    }
                    try
                    {
                        selectedProduct = int.Parse(selectedProductString);
                        if (selectedProduct > productsWithSimilarName.Count || selectedProduct <= 0)
                        {
                            Console.WriteLine($"There is no product associated to the index {selectedProduct} please try again");
                            selectedProduct = -1;
                            continue;
                        }

                    }
                    catch (InvalidCastException)
                    {
                        Console.WriteLine("The input couldn't be recognized as a number, please try again");
                        continue;
                    }
                }
                return productsWithSimilarName[selectedProduct-1];
            }
            catch (NoProductFoundException npfe)
            {
                Console.WriteLine(npfe.Message);
                return null;
            }
        }

        private static void PrintSearchProductByName()
        {
            Product? product = ObtainProductFromSearch("search");
            if(product != null) 
            {
                Console.WriteLine(Product.Header);
                Console.WriteLine(product.ToString());
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void PrintEditProductFlow()
        {
            Product? productToEdit = ObtainProductFromSearch("edit");
            if(productToEdit != null)
            {
                Console.WriteLine("Do you wish to edit the name of the product? Y/N");
                string? answer = Console.ReadLine();
                while (answer is null) 
                {
                    Console.WriteLine("Invalid answer. Please type Y or y for yes, N or n for no");
                    answer = Console.ReadLine();
                }
                string name = string.Empty;
                switch (answer)
                {
                    case "Y":
                    case "y":
                        Console.WriteLine("Please type the new name of the product");
                        string? newName = Console.ReadLine();
                        while (newName is null)
                        {
                            Console.WriteLine("Invalid answer. Please type Y or y for yes, N or n for no");
                            newName = Console.ReadLine();
                        }
                        name = newName;
                        break;
                    case "N":
                    case "n":
                        name = productToEdit.Name;
                        break;
                }

                Console.WriteLine("Do you wish to edit the price of the product? Y/N");
                answer = Console.ReadLine();
                while (answer is null)
                {
                    Console.WriteLine("Invalid answer. Please type Y or y for yes, N or n for no");
                    answer = Console.ReadLine();
                }
                float price = -1f;
                switch (answer)
                {
                    case "Y":
                    case "y":
                        Console.WriteLine("Please type the new price of the product. If decimal, separate the decimal part with a comma");
                        string? newPrice = Console.ReadLine();
                        while (newPrice is null)
                        {
                            Console.WriteLine("Invalid answer. Please try agan");
                            newPrice = Console.ReadLine();
                        }
                        price = float.Parse(newPrice);
                        while (price < 0f)
                        {
                            try
                            {
                                Console.WriteLine("Please type a positive number");
                                price = float.Parse(Console.ReadLine() ?? "-1");
                            }
                            catch (InvalidCastException)
                            {
                                Console.WriteLine("The input couldn't be recognized as a number, please try again");
                                continue;
                            }
                        }
                        break;
                    case "N":
                    case "n":
                        price = productToEdit.Price;
                        break;
                }

                Console.WriteLine("Do you wish to edit the quantity of the product? Y/N");
                answer = Console.ReadLine();
                while (answer is null)
                {
                    Console.WriteLine("Invalid answer. Please type Y or y for yes, N or n for no");
                    answer = Console.ReadLine();
                }
                int quantity = -1;
                switch (answer)
                {
                    case "Y":
                    case "y":
                        Console.WriteLine("Please type the new quantity of the product");
                        string? newQuantity = Console.ReadLine();
                        while (newQuantity is null)
                        {
                            Console.WriteLine("Invalid answer. Please try again");
                            newQuantity = Console.ReadLine();
                        }
                        quantity = int.Parse(newQuantity);
                        while (quantity < 0)
                        {
                            try
                            {
                                Console.WriteLine("Please type a positive number");
                                quantity = int.Parse(Console.ReadLine() ?? "-1");
                            }
                            catch (InvalidCastException)
                            {
                                Console.WriteLine("The input couldn't be recognized as a number, please try again");
                                continue;
                            }
                        }
                        break;
                    case "N":
                    case "n":
                        quantity = productToEdit.Quantity;
                        break;
                }
                Inventory.Inventory.EditProduct(productToEdit.Name, new Product(name, price, quantity));
                Console.WriteLine("Product has been edited correctly");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void PrintDeleteProductFlow()
        {
            Product? productToDelete = ObtainProductFromSearch("delete");
            if (productToDelete != null)
            {
                Inventory.Inventory.DeleteProduct(productToDelete);
                Console.WriteLine("Product has been removed correctly");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public static void Main()
        {
            while (true)
            {
                Console.Write("*******************************************************\n" +
                    "\tInventory Managament\n" +
                    "Welcome, please select one of the following options:\n" +
                    "1. See all products\n" +
                    "2. Search product by name\n" +
                    "3. Add a new product.\n" +
                    "4. Edit an existing product.\n" +
                    "5. Delete an existing product.\n" +
                    "6. Exit.\n" +
                    "*******************************************************\n");
                string? answer = Console.ReadLine();
                Console.Clear();
                switch (answer)
                {
                    case "1":
                        PrintSeeAllProducts();
                        break;
                    case "2":
                        PrintSearchProductByName();
                        break;
                    case "3":
                        PrintAddProductFlow();
                        break;
                    case "4":
                        PrintEditProductFlow();
                        break;
                    case "5":
                        PrintDeleteProductFlow();
                        break;
                    case "6":
                        Console.WriteLine("Thank you for using the inventory management system!");
                        return;
                    default:
                        Console.WriteLine("Input not valid, please try again.");
                        Console.Clear();
                        break;
                }
            }
        }
    }
}