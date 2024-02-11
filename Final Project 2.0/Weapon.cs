using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_2._0
{
    public class Weapon
    {
        public int weaponDamage = 0;
        public string weaponName = "";
        string weaponDescription = "";
        int weaponWeight = 0;
        bool isMagic = false;
        int hitBonus = 0;
        int criticalChance = 0;
        public Weapon(int damage, string name, string description)
        {
            weaponDamage = damage;
            weaponName = name;
            weaponDescription = description;
        }

        public string getNameAndDescription()
        {
            return weaponName + ", " + weaponDescription;
        }
    }
}
