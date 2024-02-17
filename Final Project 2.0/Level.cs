using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_2._0
{
    public class Level
    {
        Random random = new Random();
        int levelType = 0;
        int levelDifficulty = 0;
        int maxNumberOfRooms = 0;
        int maxMonsters = 0;
        int minMonsters = 0;
        int maxTreasure = 0;
        int minTreasure = 0;
        List<Door> doorsToPotentialRooms = new List<Door>();

        public List<Room> rooms = new List<Room>();
        int[,] doorsInRoom = {{1,0,0}, {2,0,0}, {3,0,0}, {1,2,0}, { 1, 3, 0 }, {2,3,0}, {1,2,3}};
        Game form;

        
        public Level(Game f, int type, int difficulty, int maxRoomNum, Room lastRoom)
        {
            levelType = type;
            levelDifficulty = difficulty;
            maxNumberOfRooms = maxRoomNum;
            this.form = f;
            Room toRoom;
            
            //Create the door pointing to last door with the random door wall
            Door entranceDoor = new Door(lastRoom, (levelDifficulty == 0)? " door leading back outside,": " stairwell leading back up to the previous level");
            Debug.WriteLine("Entrance door  is " +  entranceDoor.toString());

            //create the room with the first door
            toRoom = makeRoom(new Coords(0,0, levelDifficulty));

            //Gets index into point list to randomly choose door wall
            int index = random.Next(0, 3);
            toRoom.doors[index] = entranceDoor;

            //add to the list  of  rooms on this level
            rooms.Add(toRoom);

            //Make a door in the previous level if level is greater than zero
            if (levelDifficulty > 0)
            {
                Door door = new Door(toRoom, " stairway to next level");
                int wall = lastRoom.getFirstAvailableWall();
                lastRoom.doors[wall] = door;
            }

            List<Room> newRooms = new List<Room>();
            newRooms.Add(toRoom);

            Debug.WriteLine("Need more rooms, adding doors");
            while (newRooms.Count > 0)
            {
                //get the room
                Room fromRoom = newRooms.First();
                //add  doors to it
                List<Coords> potentialRooms= this.getPotentialRooms(fromRoom);

                while (potentialRooms.Count > 0 && this.rooms.Count < this.maxNumberOfRooms)
                {
                    Debug.WriteLine("Adding room");
                    Coords c = potentialRooms.First();
                    toRoom = makeRoom(c);

                    rooms.Add(toRoom);

                    //Make a new door to new room
                    Door doorToNewRoom = new Door(toRoom, "door");
                    fromRoom.doors[getWallIndex(fromRoom.xyz, toRoom.xyz)] = doorToNewRoom;

                    // add the new room to the new rooms list
                    newRooms.Add(toRoom);

                    //TODO: Get a door description
                    //Make door from the new room
                    Door newRoomEntranceDoor = new Door(fromRoom, "door");

                    //Get wall index backward <----
                    toRoom.doors[getWallIndex(toRoom.xyz, fromRoom.xyz)] = newRoomEntranceDoor;

                    Debug.WriteLine("Added room with " + toRoom.toString());
                    potentialRooms.Remove(c);
                }

                //remove from list of new rooms
                newRooms.Remove(fromRoom);
            }

        }



        // check if the room space is  available and if the door wall is available
        bool isBlocked(Coords d) 
        {
            for (int i = 0; i < rooms.Count; i++) 
            {
                if ((rooms[i].xyz.X == d.X && rooms[i].xyz.Y == d.Y))
                {
                    return true;
                }
            }
            return false;
        }


        public Room getRoom(int n)
        {
            return rooms[n];
        }


        public Room getRoom(Door door) 
        {
            //Return door with coordinates at c2
            return null;
        }

        public Room makeRoom(Coords firstDoor)
        {
            //Create the room with location and the entrance door + other stuff
            Room resultRoom = new Room(this.form, this.levelDifficulty, levelType, firstDoor);

            return resultRoom;
        }


        public List<Coords> getPotentialRooms(Room room)
        {
            List<Coords> potentialRooms = new List<Coords>();

            //We should always have one door when the room is created
            int entranceWallIndex = 0;
            for (int i = 0; i < room.doors.Count(); i++) 
            {
                if (room.doors[i] != null) 
                {
                    entranceWallIndex = i; break;
                }
            }

            //Creates a random set of doors
            int x = random.Next(0, 6);
            int[] doorSet = { doorsInRoom[x, 0], doorsInRoom[x, 1], doorsInRoom[x, 2] };

            //Get an index into point set for each random door
            for (int i = 0; i < doorSet.Length; i++)
            {
                // only use values greater than zero
                if (doorSet[i] > 0)
                {
                    int result = entranceWallIndex + doorSet[i];

                    //Keeps index in range of pointList
                    if (result > 3) result -= 4;

                    Coords newRoomCoord = new Coords(room.xyz.X + form.pointList[result].X, room.xyz.Y + form.pointList[result].Y, this.levelDifficulty);
                    potentialRooms.Add(newRoomCoord);
                }
            }

            //Check if potential room coordinates are blocked
Debug.WriteLine("Checking " + potentialRooms.Count + " potential doors");
            for (int i = potentialRooms.Count - 1; i > -1; i--)
            {
                //Determine if room is blocked then remove it from the potential rooms list
                if (isBlocked(potentialRooms[i]))
                {
 Debug.WriteLine("removed blocked door " + potentialRooms[i].toString());
                    potentialRooms.Remove(potentialRooms[i]);
                }
            }

            return potentialRooms;
        }


        public int getWallIndex(Coords c1, Coords c2) 
        {
            if (c1.X == c2.X)
            {
                if (c1.Y > c2.Y)
                {
                    return 2;
                }
                return 0;
                
            }
            else 
            {
                if (c1.X > c2.X)
                {
                    return 3;
                }
                return 1;
            }
        }

    }
}
