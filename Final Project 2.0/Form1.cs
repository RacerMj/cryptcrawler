using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.Media;
using System.Security.Cryptography;
using System.Web;
using System.Windows.Forms.Design;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Final_Project_2._0
{
    /*The type can be Fire, Earth, Water, and Air. Determines the monsters that spawn within each level fire = 0, earth = 1, water = 2, air = 3
     *Chance of treasure is 1-100, representing a percent chance */



    public partial class DungeonForm : Form
    {
        //Subsitiuiton for length max value of random in room function (max value should be length of creatureArray)
        public const int NUMCREATURES = 16;
        public const int MINROOMS = 10;
        public const int MAXROOMS = 20;

        public Creature[] creatureArray = new Creature[20];
        public Decoration[] decorationList = new Decoration[20];
        public Treasure[] treasures = new Treasure[3];
        public Weapon[] weaponArray = new Weapon[3];
        public Point[] pointList = new Point[4];
        public string[] doorDirections = new string[4];
        public string[] roomDescriptions = new string[30];
        public string[] doorDescriptions = new string[3];
        public string[] playerHitDescription = new string[5];
        public string[] playerMissDescription = new string[5];
        public string[] monsterHitDescription = new string[5];
        public string[] monsterMissDescription = new string[5];
        public string[] monsterDeathDescription = new string[5];
         Random random = new Random();
        int whatLevelPlayerIsOn = 0;
        Room whatRoomPlayerIsIn = null;
        Coords playerCoords = new Coords(0,0,0);
        int randomInt = 0;
        public Dungeon dungeon;
        Creature player;
        public DungeonForm()
        {
            InitializeComponent();

            //example
            //Room r = new Room();
            //bool[] d = r.doors;
            //Room r2 = r;

            //Room Description Array
            roomDescriptions[0] = "a dimly lit space adorned with faded tapestries, revealing the dungeon's long-forgotten history.";
            roomDescriptions[1] = "a crowded room filled with makeshift beds, where goblins scheme and quarrel.";
            roomDescriptions[2] = "a room with a seemingly innocuous floor that conceals a deep pit filled with deadly spikes.";
            roomDescriptions[3] = "a room Guarded by animated suits of armor, this chamber houses glittering treasures, tempting and dangerous.";
            roomDescriptions[4] = "a room pulsating with dark energy, its center harboring a portal to a nightmarish abyss.";
            roomDescriptions[5] = "a room with intricate runes covering the walls, presenting a puzzle that must be deciphered to proceed.";
            roomDescriptions[6] = "a vast library where forbidden knowledge is stored, protected by guardian statues.";
            roomDescriptions[7] = "a alchemy lab filled with bubbling cauldrons and shelves of mysterious ingredients, guarded by alchemical constructs.";
            roomDescriptions[8] = "a room shrouded in magical darkness, concealing a what could be cache of powerful weapons and armor.";
            roomDescriptions[9] = "an expansive cave illuminated by glowing crystals, revealing hidden paths and dangerous creatures.";
            roomDescriptions[10] = "a room draped in webs, a previous home to a colossal spider queen and her arachnid minions.";
            roomDescriptions[11] = "a room with deactivated portal, that once lead to other realms and dimensions.";
            roomDescriptions[12] = "a burial chamber chilled to freezing temperatures, guarded by icy undead.";
            roomDescriptions[13] = "a room suspended in a timeless dimension, featuring a ghostly congregation of spirits.";
            roomDescriptions[14] = "a room cluttered with gears and mechanisms, where devious traps are created and maintained.";
            roomDescriptions[15] = "an enchanted room housing mechanical creatures that come to life when disturbed.";
            roomDescriptions[16] = "a room containing a mystical chalice, guarded by illusions and phantasmal defenders.";
            roomDescriptions[17] = "a fiery chamber where demonic blacksmiths craft weapons of infernal power.";
            roomDescriptions[18] = "a confusing labyrinth of mirrors that disorients and confounds intruders.";
            roomDescriptions[19] = "a room inhabited by a mysterious oracle, offering cryptic prophecies to those who seek answers.";
            roomDescriptions[20] = "a domed room with a telescope pointing towards the heavens, revealing celestial secrets.";
            roomDescriptions[21] = "a grand arena where shadow creatures engage in eternal battles.";
            roomDescriptions[22] = "a room enchanted with the essence of the Feywild, complete with mystical flora and fauna.";
            roomDescriptions[23] = "a chamber where illusions abound, challenging adventurers to discern friend from foe.";
            roomDescriptions[24] = "a room filled with magical experiments and animated spellbooks.";
            roomDescriptions[25] = "a hot and noisy room where steam-powered machinery creates constructs and warforged.";
            roomDescriptions[26] = "a dark room where the gaze of a medusa turns intruders to stone.";
            roomDescriptions[27] = "a room with shelves of ancient tomes on necromancy and undead guardians.";
            roomDescriptions[28] = "a partially submerged chamber, revealing ancient murals and guarded by aquatic creatures.";
            roomDescriptions[29] = "a whimsical room where mischievous imps play pranks and tricks on those who enter.";
            //Assigns descriptions to all of the array

            //Decoration array.
            decorationList[0] = new Decoration(10, "A dusty chair, aged by the centures, has seen better days");
            decorationList[1] = new Decoration(10, "A table");
            decorationList[2] = new Decoration(50, "A chest");
            decorationList[3] = new Decoration(20, "A throne");
            decorationList[4] = new Decoration(12, "A bookshelf");
            decorationList[5] = new Decoration(12, "A pillar");
            decorationList[6] = new Decoration(12, "A crystal formation");
            decorationList[7] = new Decoration(12, "A spiders web");
            decorationList[8] = new Decoration(12, "A altar");
            decorationList[9] = new Decoration(12, "A cauldron");
            decorationList[10] = new Decoration(12, "A adventurers skeleton");
            decorationList[11] = new Decoration(12, "A portrait");
            decorationList[12] = new Decoration(12, "A display case");
            decorationList[13] = new Decoration(12, "A storage jar");
            decorationList[14] = new Decoration(12, "A coffin");
            decorationList[15] = new Decoration(12, "A sarcophogus");
            decorationList[16] = new Decoration(12, "A barrel");
            decorationList[17] = new Decoration(12, "A statue");
            decorationList[18] = new Decoration(12, "A fountain");
            decorationList[19] = new Decoration(12, "A notch");

            weaponArray[0] = new Weapon(5,"dagger", "Small and agile, this is a cheap weapon for beginner adventures");
            weaponArray[1] = new Weapon(10, "sword", " not the best weapon, but useful and reliable");
            weaponArray[2] = new Weapon(15, "greatsword", " will just as easily crush bones as cut through them. Avoid being hit");

            playerHitDescription[0] = "You swing wildly, and catch the monster in the stomach.";
            playerHitDescription[1] = "You hit the monster with a crushing blow, removing any hint of armour.";
            playerHitDescription[2] = "You hit the monster with a fierce swing,and your blade connects squarely with the monster's flank, leaving a deep gash.";
            playerHitDescription[3] = "You attack swiftly, and your weapon finds its mark, slicing through the monster's defenses.";
            playerHitDescription[4] = "You deliver a crushing blow to the monster's head, momentarily stunning it.";

            playerMissDescription[0] = "Your wild swing narrowly misses the monster's stomach, leaving you momentarily off balance.";
            playerMissDescription[1] = "Your dagger grazes the monster's side, failing to penetrate its tough hide.";
            playerMissDescription[2] = "Your weapon misses its mark, as the monster deftly dodges your swift and nimble strikes.";
            playerMissDescription[3] = "Your wild swing narrowly misses the monster's stomach, leaving you momentarily off balance.";
            playerMissDescription[4] = "Your swing falls short, the monster agilely evading the attack and maintaining its defense.";

            monsterHitDescription[0] = " grabs onto you, and claws your shoulder. ";
            monsterHitDescription[1] = " hits you with a crushing blow. ";
            monsterHitDescription[2] = " slashes deep into your side. ";
            monsterHitDescription[3] = " breaks away, and hits you.";
            monsterHitDescription[4] = " slashes you with its claws.";

            monsterMissDescription[0] = " trys to grab onto you, but misses, due to your cowardly dodges.";
            monsterMissDescription[1] = " lunges wildly at you, but by some feat of superhuman luck, you dodge every blow.";
            monsterMissDescription[2] = " attemps to stab you, but with some excellent display of skill, each attack is blocked.";
            monsterMissDescription[3] = " it attempts to hit you with its weapon, but you notice, and duck under its blow.";
            monsterMissDescription[4] = " it really, really, says you can make up with a good hug, but you notice the dagger.";

            monsterDeathDescription[0] = " dies with a pitiful look in its eyes as it keels over.";
            monsterDeathDescription[1] = " screams in pain, and slumps to the ground to bleed out.";
            monsterDeathDescription[2] = " drops with a soft gurgle, as its life force fades.";
            monsterDeathDescription[3] = " explodes with a shatter of, bone, as your last blow was so powerful, it could not be handled.";
            monsterDeathDescription[4] = " quietly leans against the wall, and ignores you in its final moments.";

            doorDescriptions[0] = " A large oaken door, heading ";
            doorDescriptions[1] = " A flimsy cloth, covering an opening in the wall going ";
            doorDescriptions[2] = " A shoddy wooden door, made by a poor craftsmen, going ";

            treasures[0] = new Treasure(10, "shiny golden globe");
            treasures[1] = new Treasure(1, "fancy gold piece");
            treasures[2] = new Treasure(5, "wonderfully crafted silver mug");

            //Creatue Array 
            //For level 0 equals
            creatureArray[0] = new Creature(5, 0, "rat", 2, "large, furry, and stinking, and these creatures can be found just about  anywhere.", 20, 2, 0, 1,3,false);
            creatureArray[1] = new Creature(15, 5, "Skeleton", 4, "versatile and common, and these undead are a favourite of necromancers everywhere.", 3, 7, 0, 1,9, true);
            creatureArray[2] = new Creature(30, 5, "Zombie", 4, "tough but slow, and these creatures are sadistic and violent in death.", 3, 10, 0, 1,7, false);
            creatureArray[3] = new Creature(12, 3, "Goblin", 3, "common and weak, and these creatues serve a fodder or food for their stronger companions.", 7, 4, 0, 1,11, true);
            creatureArray[4] = new Creature(10, 0, "Giant-Spider", 5, "fast and deadly in numbers, and these creatures promise a slow death to any prey.", 6, 4, 1, 1,12, false);
            creatureArray[5] = new Creature(15, 5, "Harpy", 5, "these half-woman-half-bird creatures control the sky, but are mostly eradictated in populous areas.", 5, 5, 0, 3,10,true);
            creatureArray[6] = new Creature(30,8,"Animated-Armour", 6, "glowing slightly, these intricate armour suits are guarding something long gone",2,5,1,3,16,true);
            creatureArray[7] = new Creature(25,3, "Werewolf",7,"vicious, fast, and full off teeth, no adventurer not ever wants to encounter these beasts.", 3,15,0,1,10,false);
            creatureArray[8] = new Creature(10, 5, "Kobold", 6,"the lesser version of goblins, these reptilian creatures show very little remorse or connection with eachother.", 5,6,0,1,6,true);
              creatureArray[9] = new Creature(15,7,"Siren",4, "enchanting and aquatic, these beings lure adventurers with haunting melodies.",3,10,0,2,10,true);
              creatureArray[10] = new Creature(30,6,"Gnoll", 5, "these hyena-headed humanoids are known for their brutality and savagery.", 2, 12,0,1,12,true);
              creatureArray[11] = new Creature(10,0,"Magma-Nephid", 3, "formed from pure flowing fire, these creatures are not known for good hugs, a fact they often refute.",3, 5, 0, 0, 8, false);
              creatureArray[12] = new Creature(15,5, "Saughin", 3, "a life under the sea in a militaristic has bread these creatures for war, but I believe in you!", 4, 10, 0, 2, 13, true);
              creatureArray[13] = new Creature(20,0, "Sea-Serpent", 5, "these creature have been around forever, and will be here long after humanity. Be weary of the deep.", 3,15,0,2,12, false);
              creatureArray[14] = new Creature(50,10, "Giant-Clam", 1, "Tough as nails, these creatures have been known to collect treasure. Perhaps you should give it a shot?",1,5,0,2,17, false);
              creatureArray[15] = new Creature(30, 0, "Fire-Elemental", 7, "fast, hard-to-hit, and mean, many mountain travelers have been claimed by them.", 2, 13, 0,3,15, false);
              creatureArray[16] = new Creature(10, 6, "Kenku", 5, "shifty little bird-people, most find Kenku annoying, if not outright repulsive.", 4,6,0,3,12, true);
            /*creatureArray[17] = new Creature();
            creatureArray[18] = new Creature();
            creatureArray[19] = new Creature(); */

            doorDirections[0] = "North";
            doorDirections[1] = "East";
            doorDirections[2] = "South";
            doorDirections[3] = "West";

            //Fills pointList
            //North
            pointList[0] = new Point(0, 1);
            //East
            pointList[1] = new Point(1, 0);
            //South
            pointList[2] = new Point(0, -1);
            //West
            pointList[3] = new Point(-1, 0);

            //Creates the player creature
            createPlayer();

            //Create Dungeon
            dungeon = new Dungeon();
            dungeon.createLevels(this, 2, "The Dungeon");

            whatRoomPlayerIsIn = (dungeon.getLevel(whatLevelPlayerIsOn).rooms.First());
            textBox2.AppendText("You have entered the dungeon." + System.Environment.NewLine);
            //Get current level
            //Get current room
            //Describe room
            textBox2.AppendText(whatRoomPlayerIsIn.getDescription());
            textBox2.AppendText(System.Environment.NewLine + System.Environment.NewLine + ">>> What would you like to do?");

            //Describe monsters/decorations

        }

        public void createPlayer() 
        {
            this.player = new Creature(100, 0, "Player", 2, "Player character. This will never be seen", 0, 10, -1, -1, 15, true);
            player.weapon = weaponArray[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doCommand();
        }

        private void doCommand()
        {
            // Disable button to prevent interface issues
            this.button1.Enabled = false;

            //Read what is in the command box
            string box1Text = this.textBox1.Text.Trim();

            //Splits the command line on  
            string[] split = box1Text.Split(' ');

            textBox2.AppendText("*" + box1Text + "*" + System.Environment.NewLine + System.Environment.NewLine);

            //Depending on command do thing
            if (player.currentHitpoints < 0) 
            {
                if (split[0] == "QUIT")
                {
                    System.Environment.Exit(0);
                }
                else 
                {
                    textBox2.AppendText("You are dead. You can do nothing. Leave the game." + System.Environment.NewLine);
                }
            }
            else
            {
                if (split[0] == "LOOK")
                {

                    //Decribe the room.
                    textBox2.AppendText(whatRoomPlayerIsIn.getDescription());

                }
                else if (split[0] == "MOVE")
                {
                    //MOVE TO THE NEXT ROOM
                    //Switch var WhatRoomPlayerIsIn to +1 

                    //Determine if split is greater than 0
                    //Loop through list of directions
                    //If a match to split[1] is found move in the direction associated
                    //Else say that isn't a direction

                    if (split.Length > 1)//&& whatRoomPlayerIsIn != 0 && split[1] == "BACK")
                    {

                        int wall = Utilities.getDirectionIndex(split[1]);

                        if (wall > -1 && whatRoomPlayerIsIn.doors[wall] != null)
                        {
                            whatRoomPlayerIsIn = whatRoomPlayerIsIn.doors[wall].room;
                            if (whatRoomPlayerIsIn.xyz.Z < 0)
                            {
                                System.Environment.Exit(0);
                            }
                            textBox2.AppendText(whatRoomPlayerIsIn.getDescription());
                        }

                        else
                        {
                            textBox2.AppendText("You cannot go that way." + System.Environment.NewLine);

                        }

                    }
                    else
                    {
                        textBox2.AppendText("Where would you like to go?" + System.Environment.NewLine);
                    }
                }
                else if (split[0] == "ATTACK")
                {

                    // get the object string and see if such a creature exists in the room
                    // if that type of creature exists
                    // Loops through creature list to attack first live one
                    List<Creature> monsters = whatRoomPlayerIsIn.monsters;
                    //Checks to make sure we are actually attacking something
                    if (split.Count() == 1)
                    {
                        textBox2.AppendText("What would you like to attack?");
                    }



                    else if (monsters != null)
                    {
                        Creature targetCreature = null;
                        for (int i = 0; i < monsters.Count; i++)
                        {
                            if (split[1] == monsters[i].id.ToUpper())
                            {
                                targetCreature = monsters[i];
                            }
                        }

                        if (targetCreature == null)
                        {
                            textBox2.AppendText("There is no such creature. Specify the monster, e.g. RAT1");
                            return;
                        }


                        //for (int i = 0; i < monsters.Count; i++)
                        //{
                        //Is it alive
                        if (targetCreature.currentHitpoints >= 0)
                        {
                            //hit it(?)
                            //Subtract damage
                            if (random.Next(1, 20) < targetCreature.armourClass)
                            {
                                //Randomizes the strings for a more interesting game.
                                textBox2.AppendText(playerMissDescription[random.Next(0,4)] + System.Environment.NewLine);
                                monsterAttack();
                            }
                            else
                            {
                                if (player.weapon != null)
                                {
                                    targetCreature.currentHitpoints -= player.weapon.weaponDamage;

                                    textBox2.AppendText(playerHitDescription[random.Next(0,4)]+ " You deal " + player.weapon.weaponDamage + " points of damage to the foul beast." + System.Environment.NewLine);
                                }
                                else
                                {
                                    textBox2.AppendText(" You deal " + player.creatureDamage + " points of damage to the foul beast." + System.Environment.NewLine);
                                }
                                if (targetCreature.currentHitpoints < 1)
                                {
                                    textBox2.AppendText(System.Environment.NewLine + " The " + targetCreature.creatureName + monsterDeathDescription[random.Next(0,4)]);
                                    Decoration deadMonster = new Decoration(targetCreature.treasureChance, "Dead " + targetCreature.id, targetCreature.id);
                                    whatRoomPlayerIsIn.decorations.Add(deadMonster);

                                    //Checks if monster has weapon then adds it to the rooms weapon list
                                    if (targetCreature.weapon != null)
                                    {
                                        whatRoomPlayerIsIn.roomWeapons.Add(targetCreature.weapon);
                                    }

                                    //This may create a problem with iterating the loop
                                    monsters.Remove(targetCreature);
                                }
                                else
                                {
                                    monsterAttack();
                                }
                            }
                        }
                        // i++;
                        //}
                        if (monsters.Count == 0)
                        {
                            whatRoomPlayerIsIn.monsters = null;
                        }
                    }

                    else
                    {
                        textBox2.AppendText("There are no monsters to attack.");
                    }
                    // if one of the creatures is alive
                    // determine if the player hit
                    // if the player hit
                    // determine how much damage is done
                    // if the creature is killed
                    // tell the player
                    // update the monster status
                    // else tell the player that they missed
                    // else tell the player that nothing is alive in the room
                    // else tell the player that there are no such creatures in the room
                    // funsies: let the player attack and destroy decorations, automatic hit of 1 hp, decoration has 1-4 hp
                }
                else if (split[0] == "EQUIP")
                {
                    //Creates a reference for the room weapon list for ease of access
                    List<Weapon> weapons = whatRoomPlayerIsIn.roomWeapons;

                    //Find what playe can equip and check name
                    if (weapons != null)
                    {
                        if (split.Length > 1)
                        {
                            for (int i = 0; i < weapons.Count; i++)
                            {
                                if (split[1] == weapons[i].weaponName.ToUpper())
                                {
                                    whatRoomPlayerIsIn.roomWeapons.Add(player.weapon);
                                    player.weapon = weapons[i];
                                    whatRoomPlayerIsIn.roomWeapons.Remove(weapons[i]);
                                    textBox2.AppendText("You have equipped a " + player.weapon.weaponName + ".");
                                    break;
                                }
                                else
                                {
                                    textBox2.AppendText("Uh uh");
                                }

                            }
                        }
                        else
                        {
                            textBox2.AppendText(System.Environment.NewLine + "What would you like to equip?");
                        }
                    }
                    else
                    {
                        textBox2.AppendText(System.Environment.NewLine + "There are no weapons to equip here.");
                    }
                    //If name == one of the weapons in room
                    //Give the weapon to the playe


                }
                else if (split[0] == "LOOT")
                {

                    if (split.Length > 1)
                    {
                        Decoration targetCreature = null;
                        for (int i = 0; i < whatRoomPlayerIsIn.decorations.Count; i++)
                        {
                            if (split[1] == whatRoomPlayerIsIn.decorations[i].id.ToUpper())
                            {
                                targetCreature = whatRoomPlayerIsIn.decorations[i];
                            }
                        }

                        if (targetCreature == null)
                        {
                            textBox2.AppendText("There is no such things to loot. Specify the thing, e.g. RAT1");
                        }

                        else if (targetCreature.isLooted)
                        {
                            textBox2.AppendText("This thing has already been looted.");
                        }
                        else
                        {
                            //Creates a random to determine if decoration/monster will have treasure
                            int treasureChance = random.Next(0, 10);
                            if (treasureChance <= targetCreature.chanceOfTreasure)
                            {
                                //Give random treasure to player
                                Treasure t = this.treasures[random.Next(0, this.treasures.Length - 1)];
                                player.creatureTreasures.Add(t);
                                textBox2.AppendText("You found a " + t.description + " worth " + t.value + " gold pieces." + System.Environment.NewLine);
                            }
                            else
                            {
                                textBox2.AppendText("You found nothing" + System.Environment.NewLine);

                            }
                            targetCreature.isLooted = true;
                        }
                    }
                    else
                    {
                        textBox2.AppendText("What would you like to loot?");
                    }
                }
                else if (split[0] == "INVENTORY")
                {
                    if (player.creatureTreasures != null)
                    {
                        if (player.creatureTreasures.Count == 0)
                        {
                            textBox2.AppendText("You have nothing" + System.Environment.NewLine);
                        }
                        else
                        {
                            textBox2.AppendText("You have:" + System.Environment.NewLine);
                            for (int i = 0; i < player.creatureTreasures.Count; i++)
                            {
                                textBox2.AppendText("A" + player.creatureTreasures[i].description + System.Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        textBox2.AppendText("You have nothing" + System.Environment.NewLine);
                    }

                }
                else if (split[0] == "HELP")
                {
                    textBox2.AppendText("You have the following commands:" + System.Environment.NewLine);
                    textBox2.AppendText("LOOK - Describes your environment" + System.Environment.NewLine);
                    textBox2.AppendText("MOVE <DIRECTION> - Moves your character to a new room" + System.Environment.NewLine);
                    textBox2.AppendText("ATTACK <CREATUREnum> - Attacks a creature" + System.Environment.NewLine);
                    textBox2.AppendText("EQUIP <WEAPONNAME> - Equips a weapon from the floor" + System.Environment.NewLine);
                    textBox2.AppendText("LOOT <THING> - Loots a decoration for a treasure" + System.Environment.NewLine);
                    textBox2.AppendText("INVENTORY - Displays your treasures" + System.Environment.NewLine);
                    textBox2.AppendText("STATUS - Displays your hp and weapon" + System.Environment.NewLine);
                    textBox2.AppendText("QUIT - Immediately quits the game" + System.Environment.NewLine);
                    textBox2.AppendText("HELP - Lists commands" + System.Environment.NewLine);
                }
                else if (split[0] == "STATUS")
                {
                    textBox2.AppendText(System.Environment.NewLine + "You have " + player.currentHitpoints + "/" + player.maxHitpoints + " hitpoints.");
                    textBox2.AppendText(System.Environment.NewLine + "You have a " + player.weapon.getNameAndDescription() + System.Environment.NewLine + " that does 1-" + player.weapon.weaponDamage + " points of damage.");
                }
                else if (split[0] == "QUIT") 
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    textBox2.AppendText("I do not understand that command.");
                }
            }


            textBox2.AppendText(System.Environment.NewLine + System.Environment.NewLine + ">>> What would you like to do? ");

            //Reenables button
            this.textBox1.Text = "";
            textBox1.Focus();
            this.button1.Enabled = true;
        }

        public void monsterAttack() 
        {
            //Get monsters from room + store
            List<Creature> monsters = whatRoomPlayerIsIn.monsters;
            if (monsters == null) { return; }

            //Find out how many monsters are alive loop and attack
            for (int i = 0; i < monsters.Count; i++) 
            {
                if (monsters[i].currentHitpoints > 0) 
                {
                    //Check for hit
                    if (random.Next(1, 20) > player.armourClass)
                    {
                        //Checks if creatueWeapon isn't null
                        if (monsters[i].weapon != null)
                        {
                            //Hit player for creature damage ( + weapon damage)
                            player.currentHitpoints -= monsters[i].weapon.weaponDamage;
                            textBox2.AppendText("The " + monsters[i].creatureName + monsterHitDescription[random.Next(0, 4)] + " You take " + monsters[i].weapon.weaponDamage + " points of damage" + System.Environment.NewLine);
                            if (player.currentHitpoints <= 0) 
                            {
                                textBox2.AppendText("You have been killed. The Dungeon Remains unconquered." + System.Environment.NewLine);
                            }
                        }
                        else
                        {
                            //Hit player for creature damage
                            player.currentHitpoints -= monsters[i].creatureDamage;
                            textBox2.AppendText("The " + monsters[i].creatureName + " lashes out at you, and connects. You take " + monsters[i].creatureDamage + " points of damage" + System.Environment.NewLine);
                        }
                    }
                       
                    else 
                    {
                        textBox2.AppendText("The " + monsters[i].creatureName + monsterMissDescription[random.Next(0, 4)] + System.Environment.NewLine);
                    }
                }
            }
            //if monster is alive, attack.
        }

        public void enterHandler(object sender, KeyEventArgs e) 
        {
            if (e.KeyCode == Keys.Enter) 
            {
                doCommand();
            }
        }


        public Dungeon getDungeon()
        {
            return this.dungeon;
        }


        public void printString(string s) { textBox2.AppendText(s + System.Environment.NewLine); }
    }


    

    public class Action;
    //Possible Actions: 
    //Inventory
    //Move
    //Attack
    //Look
    //Take
    //Flee
    //Equip/Swap weapon
    //?

}
