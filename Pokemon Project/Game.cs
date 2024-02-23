

namespace PokemonProject {

    class Game {

        public int inventory;
        public int turnNum;
        public readonly Graphics graphics = new();
        public Game() {

            turnNum = 0;
            inventory = 0;
        }

        public  void ClearConsole(double sleepTime = 1.5) 
        {   
            Sleep(sleepTime);
            Console.Clear();
        }

        public void Sleep(double sleepTime) {
            sleepTime *= 1000;
            Thread.Sleep((int)sleepTime);
        }

        public void RunGame() {
            

            Pokemon p1;
            Pokemon p2;

            Random rng = new();
            int p2Num = rng.Next(0,3);

            if (p2Num == 0) { p2 = new Charmander(); }
            else if (p2Num == 1) { p2 = new Squirtle(); }
            else { p2 = new Bulbasaur(); }
            
            int starterChoiceInt;

            while(true) {

                Console.WriteLine("Enter the number of the Pokemon you would like to use!\n1) Bulbasaur\n2) Charmander\n3) Squirtle");
                string? starterChoiceString = Console.ReadLine();

                starterChoiceString ??= "NULL_ENTRY";

                try {

                    starterChoiceInt = Int32.Parse(starterChoiceString);
                    break;
                }
                catch {
                    
                    Console.WriteLine("Please enter a number 1-3");
                    continue;
                }
            }

            if (starterChoiceInt == 1) { p1 = new Bulbasaur(); }
            else if (starterChoiceInt == 2) { p1 = new Charmander(); }
            else  { p1 = new Squirtle(); }

            
            turnNum = p1.speed >= p2.speed ? 0 : 1;
            inventory = rng.Next(1,4);
            
            ClearConsole();

            if (turnNum == 1) { DisplayBattleScreen(p1, p2); DisplayMessageBox($"Enemy {p2.name} is faster, they go first", null); }
            else { DisplayBattleScreen(p1, p2); DisplayMessageBox($"Your {p1.name} is faster, you go first", null); }
            
            while(p1.IsAlive() && p2.IsAlive()) {

                ClearConsole();

                if (turnNum % 2 == 0) {

                    DisplayBattleScreen(p1,p2,0);

                    string? actionChoice = Console.ReadLine();

                    actionChoice ??= "NULL_ENTRY";
                    
                    ClearConsole(1);

                    if (actionChoice.Equals("1")) // Attack Box
                    {   
                        
                        DisplayBattleScreen(p1, p2, 1);
                        string? attackChoiceString = Console.ReadLine();

                        attackChoiceString ??= "NULL_ENTRY";

                        if (attackChoiceString.Equals("1")) {
                            
                            ClearConsole(1);

                            int tempHealth = p2.health;
                            p1.Attack(0, p2);
                            if (tempHealth == p2.health) {

                                DisplayBattleScreen(p1, p2, 2, p1.moveSet[0], true);
                            }
                            else {

                                DisplayBattleScreen(p1, p2, 2, p1.moveSet[0]);
                            }
                            
                        }
                        else if (attackChoiceString.Equals("2")) {

                            ClearConsole(1);

                            int tempHealth = p2.health;
                            p1.Attack(1, p2);
                            if (tempHealth == p2.health) {

                                DisplayBattleScreen(p1, p2, 2, p1.moveSet[1], true);
                            }
                            else {

                                DisplayBattleScreen(p1, p2, 2, p1.moveSet[1]);
                            }
                        }
                        else {
                            DisplayBattleScreen(p1,p2,-1);
                            DisplayMessageBox("Please enter 1 or 2", null);
                        }

                    } 
                    else if (actionChoice.Equals("2")) {
                        
                        DisplayBattleScreen(p1, p2, 3);
                        
                        string? bagChoice = Console.ReadLine();
                        bagChoice ??= "NULL_ENTRY";

                        if ( bagChoice.Equals("1")) {
                            
                            ClearConsole();

                            int healthHealed = Potion.Use(inventory, p1).healthHealed;
                            string messageTop = Potion.Use(inventory, p1).messageTop;
                            string messageBottom = Potion.Use(inventory, p1).messageBottom;

                            DisplayBattleScreen(p1, p2, -1);
                            if (!messageBottom.Equals("")) {

                                p1.health += healthHealed;
                                inventory--;
                                DisplayMessageBox(messageTop, messageBottom);
                            }
                            else {

                                DisplayMessageBox(messageTop, messageBottom);
                                continue;
                            }
                            
                        }
                        else if (bagChoice.Equals("2")) { continue; }
                        else {
                            DisplayBattleScreen(p1,p2,-1);
                            DisplayMessageBox("Please enter 1 or 2", null);
                        }
                    } 
                    else if (actionChoice.Equals("3")) {
                        
                        
                        DisplayBattleScreen(p1,p2,-1);
                        DisplayMessageBox("You can't run, fight like a man!", null);
                        continue;
                    }
                    else if (actionChoice.Equals("d")) { // secret stat menu 

                        Console.WriteLine($"ENEMY STATS:\n{p2.DisplayStats()}\n\nUSER STATS:\n{p1.DisplayStats()}\n");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        turnNum--;
                    }
                    else {
                        
                        DisplayBattleScreen(p1,p2,-1);
                        DisplayMessageBox("Please enter a number", null);
                        continue;
                    }
                    turnNum++;
                }
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
            if (!p1.IsAlive()) { DisplayMessageBox($"Your {p1.name} fainted. You lose.", null); }
            else if (!p2.IsAlive()) { DisplayMessageBox($"Enemy {p2.name} fainted. You Win!", null); }
            

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
