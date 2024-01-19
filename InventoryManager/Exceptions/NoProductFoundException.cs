using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Exceptions
{
    public class NoProductFoundException(string name) : Exception($"There is no product with the following name: {name}")
    {
    }
}
