using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_2._0
{
    internal class Utilities
    {
        static public string getDirection(int n)
        {
            switch (n)
            {
                case 0: return "North";
                case 1: return "East";
                case 2: return "South";
                case 3: return "West";
            }
            return "Bad Direction";
        }

        static public int getDirectionIndex(string dir) 
        {
            string n = dir.ToUpper();
            switch(n) 
            {
                case "NORTH": return 0;
                case "EAST": return 1;
                case "SOUTH": return 2;
                case "WEST": return 3;
            }
            return -1;
        }

        static public string getNumberAffix(int n) 
        {
            switch (n) 
            {
                case 1: return "1st";
                case 2: return "2nd"; 
                case 3: return "3rd";
                case 4: return "4th";
                case 5: return "5th";
                case 6: return "6th";
                case 7: return "7th";
                case 8: return "8th";
                case 9: return "9th";
                case 10: return "10th";
            }
            return "1st";
        }
    }
}
