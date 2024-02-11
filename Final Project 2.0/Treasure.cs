using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_2._0
{
    public class Treasure
    {
        public string description; //What item is eg. Golden Globe
        public int value; //How much item is worth in gold (pieces)
        float weight; //How much item weighs in kilograms
        public Treasure(int v, string d)
        {
            this.value = v;
            this.description = d;
        }
    }
}
