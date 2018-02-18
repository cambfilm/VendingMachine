using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class Stocker
    {
        private const int Col_Name = 1;
        private const int Col_Price = 2;

        public string Path { get; set; }

        public Stocker(string path)
        {
            Path = path;
        }

        public List<Item> CreateStockList()
        {
            List<Item> items = new List<Item>();

            try
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        switch (line[0])
                        {
                            case 'A':
                                items.Add(CreateChip(line));
                                break;
                            case 'B':
                                items.Add(CreateCandy(line));
                                break;
                            case 'C':
                                items.Add(CreateDrink(line));
                                break;
                            case 'D':
                                items.Add(CreateGum(line));
                                break;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("You need to input a correct path.");
                Console.WriteLine(e.Message);
            }


            return items;
        }

        private Chip CreateChip(string line)
        {
            string[] items = line.Split('|');
            string name = items[Col_Name];
            decimal price = decimal.Parse(items[Col_Price]);

            Chip chip = new Chip(name, price);

            return chip;
        }

        private Candy CreateCandy(string line)
        {
            string[] items = line.Split('|');
            string name = items[Col_Name];
            decimal price = decimal.Parse(items[Col_Price]);

            Candy candy = new Candy(name, price);

            return candy;
        }

        private Drink CreateDrink(string line)
        {
            string[] items = line.Split('|');
            string name = items[Col_Name];
            decimal price = decimal.Parse(items[Col_Price]);

            Drink drink = new Drink(name, price);

            return drink;
        }

        private Gum CreateGum(string line)
        {
            string[] items = line.Split('|');
            string name = items[Col_Name];
            decimal price = decimal.Parse(items[Col_Price]);

            Gum gum = new Gum(name, price);

            return gum;
        }

    }
}
