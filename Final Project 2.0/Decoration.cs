using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_2._0
{
    public class Decoration
    {
        public string id = "";
        Random random = new Random();
        int typeOfDecoration = 0;
        string decorationDescription = "";
        public int chanceOfTreasure = 0;
        Treasure treasure = null;
        public bool isLooted = false;

        public Decoration(int treasureChance, string description)
        {
            decorationDescription = description;
            chanceOfTreasure = treasureChance;
            if (random.Next(0, 25) <= chanceOfTreasure)
            {
                treasure = new Treasure(25, ":)");
            }
        }

        public Decoration(int treasureChance, string description, string id)
        {
            decorationDescription = description;
            chanceOfTreasure = treasureChance;
            if (random.Next(0, 25) <= chanceOfTreasure)
            {
                treasure = new Treasure(25, ":)");
            }
            this.id = id;

        }

        public string getDescription()
        {
            return decorationDescription;
        }
    }
}
