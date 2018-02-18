using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public abstract class Item
    {
        // defines name property
        public string Name { get; set; }

        //defines price property
        public decimal Price { get; set; }

        //Constructor that sets Item with name and price
        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        // Method which consumes the item
        public abstract string Consume();        

    }
}
