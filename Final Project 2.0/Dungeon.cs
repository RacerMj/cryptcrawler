using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_2._0
{
    public class Dungeon
    {
        Random random = new Random();
        int numberOfLevels = 0;
        int dungeonIdentity = 0;
        string dungeonName = "";
        public List<Level> levels = new List<Level>();

        public Dungeon()
        {

        }

        public void createLevels(DungeonForm f, int levelNumber, string name)
        {
            numberOfLevels = levelNumber;
            dungeonName = name;
            Room lastRoom = new Room(f, -1, 0, new Coords(0, 0, -1));
            for (int i = 0; i < numberOfLevels; i++)
            {
                Level level = new Level(f, random.Next(0, 3), i, random.Next(DungeonForm.MINROOMS,DungeonForm.MAXROOMS), lastRoom);
                levels.Add(level);
                lastRoom = level.rooms.Last();
            }
        }

        public Level getLevel(int n)
        {
            return levels[n];
        }
    }
}
