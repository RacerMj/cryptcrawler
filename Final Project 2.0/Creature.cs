using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_2._0
{
    public class Creature
    {
        public string id;
        public int maxHitpoints = 0;
        public int creatureDamage = 0;
        public int currentHitpoints = 0;
        int maxEncumbrance = 0;
        public int treasureChance = 0;
        public List<Treasure> creatureTreasures = new List<Treasure>(); //Greenskin can have some
        public Weapon weapon = null;
        public string creatureName = "";
        int creatureIniative = 0;
        string creatureDescription = ""; //Filthy greenskin
        int disposition = 0;
        public bool isWeaponHolder = false;
        public int creatureMaxGroupSize = 0;
        public int creatureLevel = 0;
        public int creatureType = 0;
        public int armourClass = 0;
        public bool isLooted = false;

        public Creature(int hitpoints, int treasures, string name, int iniatiave, string description, int maxGroupSize, int damage, int level, int type, int AC, bool isWeaponCapable) //AC = Armour Class
        {
            maxHitpoints = hitpoints;
            currentHitpoints = hitpoints;
            treasureChance = treasures;
            creatureName = name;
            creatureIniative = iniatiave;
            creatureDescription = description;
            creatureMaxGroupSize = maxGroupSize;
            creatureDamage = damage;
            creatureLevel = level;
            creatureType = type;
            armourClass = AC;
            isWeaponHolder = isWeaponCapable;
        }

        public Creature(Creature c)
        {
            maxHitpoints = c.maxHitpoints;
            currentHitpoints = c.currentHitpoints;
            treasureChance = c.treasureChance;
            creatureName = c.creatureName;
            creatureIniative = c.creatureIniative;
            creatureDescription = c.creatureDescription;
            creatureMaxGroupSize = c.creatureMaxGroupSize;
            creatureDamage = c.creatureDamage;
            creatureLevel = c.creatureLevel;
            creatureType = c.creatureType;
            armourClass = c.armourClass;
            isWeaponHolder = c.isWeaponHolder;
        }

        public string getName()
        {
            return creatureName;
        }

        public string getDescription()
        {
            return creatureDescription;
        }

    }
}
