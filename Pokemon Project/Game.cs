

namespace PokemonProject {

    /// <summary>
    /// This class handles all of the game functions.
    /// </summary>
    class Game {

        // VARIABLE DECLARATION
        public int inventory; // The number of potions in the user's inventory
        public int turnNum; // The current turn number
        public readonly Graphics graphics = new(); // Creates an instance of the Graphics class. See Graphics.cs for documentation.

        // Constructor of Game. Sets all variables to zero.
        public Game() {

            turnNum = 0;
            inventory = 0;
        }

        /// <summary>
        /// Clears the console after a certain length of time.
        /// </summary>
        /// <param name="sleepTime"> The length, in seconds, that the program will sleep for. BY DEFAULT IS 1.5 SECONDS </param>
        public  void ClearConsole(double sleepTime = 1.5) 
        {   
            Sleep(sleepTime); // Calls Game.Sleep() function. See Game.Sleep() for furthur documentation
            Console.Clear(); // Clears the console.
        }

        /// <summary>
        /// Pauses the program for a certain length of time.
        /// </summary>
        /// <param name="sleepTime"> The length, in seconds, that the program will sleep for. </param>
        public void Sleep(double sleepTime) {

            sleepTime *= 1000; // Converts sleepTime to milliseconds by multiplying by 1000
            Thread.Sleep((int)sleepTime); // Calls the Thread.Sleep() method that pauses the program for a certain length of time in milliseconds.
        }

        /// <summary>
        /// The function that runs the game
        /// </summary>
        public void RunGame() {
            
            // Calls two instances of the Pokemon class.
            Pokemon p1; // This will become the user's pokemon
            Pokemon p2; // This will become the enemy pokemon

            // Calls a new instance of the Random class to generate random numbers through out the game's runtime
            Random rng = new();

            // Generates a number to randomly select the enemy's pokemon
            int p2Num = rng.Next(0,3);

            // Depending on the value of p2Num, p2 will be assigned a different instance of one of the Pokemon child classes.
            if (p2Num == 0) { p2 = new Charmander(); }
            else if (p2Num == 1) { p2 = new Squirtle(); }
            else { p2 = new Bulbasaur(); }
            
            // Creates an integer that will be assigned a value later.
            int starterChoiceInt;

            // Creates a while loop to give the user a choice of what pokemon they would like to use.
            while(true) {

                // Prompts the user to enter the number of the pokemon that they would like to use
                Console.WriteLine("Enter the number of the Pokemon you would like to use!\n1) Bulbasaur\n2) Charmander\n3) Squirtle");

                // Possible null string created to store the input that the user gave.
                string? starterChoiceString = Console.ReadLine();

                // If starterChoiceString is null, it assigns NULL_ENTRY to it.
                starterChoiceString ??= "NULL_ENTRY";

                // Tries to parse the starterChoiceString to an int. 
                // If able to it assigns the parsed int to starterChoiceInt and breaks the while loop.
                // If NOT able to, it prompts the user to enter a number from 1-3 and continues the loop.
                try {

                    starterChoiceInt = Int32.Parse(starterChoiceString);
                    break;
                }
                catch {
                    
                    Console.WriteLine("Please enter a number 1-3");
                    continue;
                }
            }

            // Depending on the value of starterChoiceInt, p1 will be assigned a different instance of one of the Pokemon child classes.
            if (starterChoiceInt == 1) { p1 = new Bulbasaur(); }
            else if (starterChoiceInt == 2) { p1 = new Charmander(); }
            else  { p1 = new Squirtle(); }

            // Assigning variables
            turnNum = p1.speed >= p2.speed ? 0 : 1; // Depending of if the user is faster or same speed as the enemy, turnNum is 0 or 1.
            inventory = rng.Next(1,4); // Assigns inventory to be a random number from 1-3.
            bool ranAway = false; // Creates a new boolean variable that stores whether or not the user has ran away.
            
            // Calls the Game.ClearConsole() method to clear the console.
            ClearConsole();

            // Depending on the turnNum, a message box is displayed to show who is going first. The screen is displayed
            // using the Game.DisplayBattleScreen() and Game.DisplayMessageBox() functions. See them for furthur
            // documentation.
            if (turnNum == 1) { DisplayBattleScreen(p1, p2); DisplayMessageBox($"Enemy {p2.name} is faster, they go first", null); }
            else { DisplayBattleScreen(p1, p2); DisplayMessageBox($"Your {p1.name} is faster, you go first", null); }
            
            // Creates a while loop that runs while BOTH pokemon are alive using the Pokemon.IsAlive() function. See Pokemon.IsAlive() for furthur documentation
            while(p1.IsAlive() && p2.IsAlive()) {

                // Calls the Game.ClearConsole() method to clear the console.
                ClearConsole();

                // CHECKS WHOSE TURN IT IS

                // If the turnNum is able to be divided by two with no remainders, it is the user's turn.
                if (turnNum % 2 == 0) {
                    
                    // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                    DisplayBattleScreen(p1,p2,0);

                    // Creates a possibly null variable that responds to user input.
                    string? actionChoice = Console.ReadLine();

                    // If actionChoice is null it assigns NULL_ENTRY to it.
                    actionChoice ??= "NULL_ENTRY";
                    
                    // Calls the Game.ClearConsole() method to clear the console.
                    ClearConsole(1);

                    // USER INPUT FUNCTIONS
                    // Checks the value of actionChoice and displays the corresponding box beneath the battle scene.
                    if (actionChoice.Equals("1")) { // If the user put in 1, that means display the attack box.  
                        
                        // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                        DisplayBattleScreen(p1, p2, 1);

                        // Creates a possibly null variable that responds to user input.
                        string? attackChoiceString = Console.ReadLine();

                        // If attackChoiceString is null it assigns NULL_ENTRY to it.
                        attackChoiceString ??= "NULL_ENTRY";

                        // USER INPUT FUNCTIONS
                        // Checks the value of attackStringChoice and uses the corresponding attack
                        if (attackChoiceString.Equals("1")) { // If the user inpur 1, that means use the attack at index 0 of the users moveSet array
                            
                            // Calls the Game.ClearConsole() method to clear the console.
                            ClearConsole(1);
                            
                            // creates a temporary variable that holds the health of the enemy BEFORE the attack is used
                            int tempHealth = p2.health;

                            // Uses the Pokemon.Attack() method to use the attack. See Pokemon.Attack() for furthur documentation. 
                            p1.Attack(0, p2);

                            // Checks if the enemy's health has changed by comparing it to the tempHealth variable that has the value of the enemy's
                            // health BEFORE the attack was used.
                            if (tempHealth == p2.health) { // If they have the same value

                                // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                                DisplayBattleScreen(p1, p2, 2, p1.moveSet[0], true);
                            }
                            else { // If they do NOT have the same value
                                
                                // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                                DisplayBattleScreen(p1, p2, 2, p1.moveSet[0]);
                            }
                            
                        }
                        else if (attackChoiceString.Equals("2")) { // If the user inpur 2, that means use the attack at index 1 of the users moveSet array

                            // Calls the Game.ClearConsole() method to clear the console.
                            ClearConsole(1);

                            // creates a temporary variable that holds the health of the enemy BEFORE the attack is used
                            int tempHealth = p2.health;

                            // Uses the Pokemon.Attack() method to use the attack. See Pokemon.Attack() for furthur documentation. 
                            p1.Attack(1, p2);

                            // Checks if the enemy's health has changed by comparing it to the tempHealth variable that has the value of the enemy's
                            // health BEFORE the attack was used.
                            if (tempHealth == p2.health) { // If they have the same value
                                
                                // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                                DisplayBattleScreen(p1, p2, 2, p1.moveSet[1], true);
                            }
                            else { // If they do NOT have the same value

                                // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                                DisplayBattleScreen(p1, p2, 2, p1.moveSet[1]);
                            }
                        }
                        else { // If the user input anything other than 1 or 2

                            // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                            DisplayBattleScreen(p1,p2,-1);
                            
                            // Calls the Game.DisplayMessageBox() method. See Game.DisplayMessageBox() for further documentation.
                            DisplayMessageBox("Please enter 1 or 2", null);
                        }

                    } 
                    else if (actionChoice.Equals("2")) { // If the user put in 2, that means display the bag box.
                        
                        // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                        DisplayBattleScreen(p1, p2, 3);
                        
                        // Creates a possibly null variable that responds to user input.
                        string? bagChoice = Console.ReadLine();

                        // If bagChoice is null it assigns NULL_ENTRY to it.
                        bagChoice ??= "NULL_ENTRY";

                        // USER INPUT FUNCTIONS
                        // Checks the value of bagChoice and performs the corresponding action.
                        if ( bagChoice.Equals("1")) { // If the user input 1, that means they would like to use a potion
                            
                            // Calls the Game.ClearConsole() method to clear the console.
                            ClearConsole();

                            // Variable Declaration
                            // All variables are returned from the Game.PotionUse() method. See Game.PotionUse() for furthur documentation.
                            int healthHealed = PotionUse(inventory, p1).healthHealed; // Gets the amount of health healed by the potion. Maximum of 20
                            string messageTop = PotionUse(inventory, p1).messageTop; // Gets the top message
                            string messageBottom = PotionUse(inventory, p1).messageBottom; // gets the bottom message. Will be empty if inventory is <= 0

                            // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                            DisplayBattleScreen(p1, p2, -1);

                            // Checks if messageBottom is empty
                            if (!messageBottom.Equals("")) { // If it is empty

                                p1.health += healthHealed; // Adds healthHealed to the user's health
                                inventory--; // Subtracts 1 from the inventory
                                // Displays messages to the user using the Game.DisplayMessageBox() function. See Game.DisplayMessageBox() for furthur documentation.
                                DisplayMessageBox(messageTop, messageBottom);
                            }
                            else { // If it's not empty.

                                // Displays messages to the user using the Game.DisplayMessageBox() function. See Game.DisplayMessageBox() for furthur documentation.
                                DisplayMessageBox(messageTop, messageBottom);
                                continue; // Continues the game loop.
                            }
                            
                        }
                        else if (bagChoice.Equals("2")) { continue; } // Exits the bag screen without performing an action.
                        else { // User input something other than 1 or 2

                            // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                            DisplayBattleScreen(p1,p2,-1);
                            // Displays messages to the user using the Game.DisplayMessageBox() function. See Game.DisplayMessageBox() for furthur documentation.
                            DisplayMessageBox("Please enter 1 or 2", null);
                        }
                    } 
                    else if (actionChoice.Equals("3")) { // If the user put in 3, that means they would like to try to run away.
                        
                        // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                        DisplayBattleScreen(p1,p2,-1);
                        // Displays a blank box using the Game.DisplayMessageBox() function. See Game.DisplayMessageBox() for furthur documentation.
                        DisplayMessageBox(null, null);
                        
                        // Assigns an int value to oddsToEscape from the Game.CalculateRunAway() function. See Game.CalculateRunAway() for furthur documentation.
                        int oddsToEscape = CalculateRunAway(p1, p2);

                        // Using the Random.Next() function, generates a random int from 0 - 255.
                        int randomOdds = rng.Next(0, 256);
                        
                        // Checsks if oddsToEscape is greater than 255 OR the randomOdds are less than oddsToEscape
                        if (oddsToEscape > 255 || randomOdds < oddsToEscape) { // If so
                            ranAway = true; // sets ranAway to true
                            break; // Breaks the while loop
                        }
                        else { // If not true
                            
                            // Calls the Game.ClearConsole() method to clear the console.
                            ClearConsole();

                            // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                            DisplayBattleScreen(p1,p2,-1);
                            // Displays messages to the user using the Game.DisplayMessageBox() function. See Game.DisplayMessageBox() for furthur documentation.
                            DisplayMessageBox("Failed to run away. You've lost your turn", null);
                        }
                    }
                    else if (actionChoice.Equals("d")) { // DEBUGGING MENU, if the user put in d, displays the stats of both pokemon and waits for an input to close

                        // Writes the stats of both pokemon using the Pokemon.DisplayStats() function. See Pokemon.DisplayStats() for furthur documentation
                        Console.WriteLine($"ENEMY STATS:\n{p2.DisplayStats()}\n\nUSER STATS:\n{p1.DisplayStats()}\n");
                        Console.WriteLine("Press any key to continue..."); // Prompts the user to press any key to continue.
                        Console.ReadLine(); // Reads the line
                        turnNum--; // Subtracts 1 from turnNum so that it doesn't cause the user to lose their turn.
                    }
                    else { // If the user put in something that was not expected
                        
                        // Calls the Game.DisplayBattleScreen() method. See Game.DisplayBattleScreen() for further documentation.
                        DisplayBattleScreen(p1,p2,-1);
                        // Displays messages to the user using the Game.DisplayMessageBox() function. See Game.DisplayMessageBox() for furthur documentation.
                        DisplayMessageBox("Please enter a valid input", null);
                        continue; // Continues the while loop.
                    }
                    // Increases turnNum by 1, changing it to the enemy's turn.
                    turnNum++;
                }
                // If the turnNum is NOT able to be divided evenly by 2, it is the enemy's turn.
                else { 

                    int randomAttackNum = rng.Next(0,2);

                    int tempHealth = p1.health;

                    p2.Attack(randomAttackNum, p1);

                    if (tempHealth == p1.health) {
                        DisplayBattleScreen(p1, p2, 2, p2.moveSet[randomAttackNum], true);
                    }
                    else {
                        DisplayBattleScreen(p1, p2, 2, p2.moveSet[randomAttackNum]);
                    }

                    turnNum++;
                }
            }
            
            ClearConsole();
            DisplayBattleScreen(p1,p2,-1);
            if (ranAway == true) { DisplayMessageBox("Successfully ran away!", null); }
            else if (!p1.IsAlive()) { DisplayMessageBox($"Your {p1.name} fainted. You lose.", null); }
            else if (!p2.IsAlive()) { DisplayMessageBox($"Enemy {p2.name} fainted. You Win!", null); }
            

        }

        public static (string messageTop, string messageBottom, int healthHealed) PotionUse(int inventory, Pokemon user) {

            string messageTop;
            string messageBottom = "";
            int healNumber = 20;

            if (inventory > 0) {

                messageTop = $"You used a potion on {user.name}";

                if (user.health + 20 > user.originalHealth) {
                    
                    healNumber =  user.originalHealth - user.health;
                }

                messageBottom = $"Healed {healNumber} health to {user.name}";
            }
            else {
                messageTop = "Unable to use potions. You have none";
            }

            return (messageTop, messageBottom, healNumber);
        }
        
        public int CalculateRunAway(Pokemon p1, Pokemon p2) {

            if (p2.speed / 4 % 256 == 0) {
                return 256;
            }

            int oddsToEscape = (p1.speed * 32 / (p2.speed / 4 % 256)) + 30;

            return oddsToEscape;
        }

        public void DisplayMessageBox(string? topMessage, string? bottomMessage) { graphics.DisplayMessageBox(topMessage, bottomMessage); }

        public void DisplayBattleScreen(Pokemon user, Pokemon enemy, int menuCase = -1, Attack? usedMove = null, bool attackMissed = false) { graphics.DisplayBattleScreen(inventory, turnNum, user, enemy, menuCase, usedMove, attackMissed); }

        public static void Main(string[] args) {

            
            Game g = new();
            
            while(true) {
                g.RunGame();

                Console.WriteLine("Play again?\n[1] Yes\n[2] No");
                string? input = Console.ReadLine();

                if (input == "1") {
                    continue;
                }
                else if (input == "2") {
                    break;
                }
                else { Console.WriteLine("Wrong character entered, exiting the program..."); break; }
            }
            
        }
    }
}
