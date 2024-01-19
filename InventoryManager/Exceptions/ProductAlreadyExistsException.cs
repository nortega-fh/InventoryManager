using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Exceptions
{
    public class ProductAlreadyExistsException(string name): Exception($"The product with name {name} already exists")
    {
    }
}
