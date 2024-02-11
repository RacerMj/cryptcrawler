using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_2._0
{
    public class Coords 
    {
        public int X;
        public int Y;
        public int Z;

        public Coords(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Coords(Coords c) 
        {
            X = c.X;
            Y = c.Y;
            Z = c.Z;
        }

        public string toString() { return "(" + X + ", " + Y + ", " + Z + ")";}
    }
}
