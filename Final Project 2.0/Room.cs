namespace Final_Project_2._0
{
    public class Room
    {
        Random random = new Random();
        public Coords xyz;
        string roomDescription;  //includes treasure
        int levelDifficulty;
        int levelType;
        public List<Decoration> decorations = new List<Decoration>();
        public List<Creature> monsters = null;
        public List<Weapon> roomWeapons = new List<Weapon>();
        public Door[] doors = {null, null, null, null}; //north = 0, east = 1, south = 2, west = 3
        Game form;

        public Room(Game f, int difficulty, int type, Coords currentPosition)
        {
            xyz = currentPosition;
            levelDifficulty = difficulty;
            levelType = type;
            form = f;
            Random random = new Random();
            //Describe room
            roomDescription = "You see " + f.roomDescriptions[random.Next(0, 29)];
            //Decorates room with up to three decorations
            for (int x = 0; x < random.Next(1, 3); x++)
            {
                decorations.Add(f.decorationList[random.Next(0, 11)]);
            }

            //Populate room with monsters 
            //Pick a monster
            int num = random.Next(0, Game.NUMCREATURES);
            /*while (levelType != f.creatureArray[num].creatureType)
            {
                num = random.Next(0, DungeonForm.NUMCREATURES);
            }
            while (levelDifficulty >= f.creatureArray[num].creatureLevel) 
            {
                num = random.Next(0, DungeonForm.NUMCREATURES);
            } */
            Creature roomCreature = f.creatureArray[num];

            //Get max group size
            //roomCreature.creatureMaxGroupSize;
            //Create up to max group size
            for (int x = 0; x < random.Next(0, roomCreature.creatureMaxGroupSize); x++)
            {
                if (monsters == null)
                {
                    monsters = new List<Creature>();
                }

                Creature creature = new Creature(roomCreature);
                creature.id = creature.creatureName + (x + 1);
                //Checks if monster can have weapon
                if (creature.isWeaponHolder)
                {
                    creature.weapon = f.weaponArray[random.Next(0, f.weaponArray.Length -1)];
                }

                //Adds a random weapon
                //Assign to monster
                //Add monster to list.
                monsters.Add(creature);
            }
        }

        public int countDead()
        {
            int deadMonsters = 0;

            for (int b = 0; b < monsters.Count; b++)
            {
                if (monsters[b].currentHitpoints < 1)
                {
                    deadMonsters += 1;
                }
            }


            return deadMonsters;
        }

        public string getDescription()
        {

            int deadMonsters = 0;

            string returnValue = roomDescription;

            if (monsters != null)
            {
                deadMonsters = countDead();
                if (deadMonsters == 0)
                {
                    returnValue += "You see " + monsters.Count + " " + monsters.First().getName() + "(s)" + ". A " + monsters.First().getName() + " is " + monsters.First().getDescription();
                }
                /*else if (monsters.Count == 0 && deadMonsters > 0) 
                {
                    returnvalue += "You see " + deadMonsters + " dead " + monsters.First().getName() + "(s)" + ". A " + monsters.First().getName() + " is " + monsters.First().getDescription();
                }
                //else if (monsters.Count > 0 && deadMonsters > 0)
                //{
                //    returnvalue += "You see " + monsters.Count + " alive " + monsters.First().getName() + "(s) and " + deadMonsters + " dead " + monsters.First().getName() + "(s)" + ". A " + monsters.First().getName() + " is " + monsters.First().getDescription();
                } */
            }

            else
            {
                returnValue += " You don't see any monsters.";
            }

            if (doors.Count() > 0)
            {
                string temp = "";
                int counter = 0;
                for (int i = 0; i < doors.Count(); i++)
                {
                    if (doors[i] != null)
                    {
                        counter++;
                        temp += "A " + doors[i].description + " on the " + Utilities.getDirection(i) + " wall." + System.Environment.NewLine;
                    }
                }
                returnValue += System.Environment.NewLine + "You also see " + counter + " doors:" + System.Environment.NewLine;
                returnValue += temp;
            }

            returnValue += System.Environment.NewLine + " Finally, you see: ";

            Decoration d;
            for (int x = 0; x < decorations.Count(); x++)
            {
                d = decorations.ElementAt(x);
                returnValue += System.Environment.NewLine + d.getDescription();
            }

            if (roomWeapons != null)
            {
                Weapon w;
                for (int b = 0; b < roomWeapons.Count; b++)
                {
                    w = roomWeapons.ElementAt(b);
                    returnValue += System.Environment.NewLine + "A " + w.getNameAndDescription();
                }
            }

            return returnValue;
        }


        public int getFirstAvailableWall() 
        {
            //loops doors arrray 
            for (int i = 0; i < this.doors.Count(); i++) 
            {
                if (this.doors[i] == null) { return i; }
            }
            return -1;
        }


        public string toString()
        {
            return ("Room coords: " + this.xyz.toString());
        }

    }
}
