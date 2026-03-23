using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
