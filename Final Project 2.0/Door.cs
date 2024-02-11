using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Final_Project_2._0
{
    public class Door
    {
        //This is where the doors goes
        public Room room = null;

        public string description;



        public Door (Room r, string description) 
        {
            this.room = r;
            this.description = description;
        }


        public string getDoorDescription(Point p) 
        {
            if (p.X == 0 && p.Y == 1) { return "North"; }
            else if (p.X == 1 && p.Y == 0) { return "East"; }
            else if (p.X == 0 && p.Y == -1) { return "South"; }
            else { return "West"; }
        }


        public void setRooms(Room r1) 
        {
            this.room = r1;
        }


        public string toString() 
        { 
            return this.room.toString() + " " + this.description;
        }
    }
}
