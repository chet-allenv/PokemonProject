

namespace PokemonProject {

    class Game {

        public readonly string battleSceneFile = "BattleScene.txt";
        public readonly string menuBoxFile = "MenuBox.txt";
        public readonly string attackBoxFile = "AttackBox.txt";
        public readonly string messageBoxFile = "MessageBox.txt";
        public readonly string bagBoxFile = "BagBox.txt";
        public string[] battleSceneStringLines;
        public string[] menuBoxLines;
        public string[] attackBoxLines;
        public string[] messageBoxLines;
        public string[] bagBoxLines;
        public int inventory;
        public int turnNum;
        public Game() {

            battleSceneStringLines = File.ReadAllLines(battleSceneFile);
            menuBoxLines = File.ReadAllLines(menuBoxFile);
            attackBoxLines = File.ReadAllLines(attackBoxFile);
            messageBoxLines = File.ReadAllLines(messageBoxFile);
            bagBoxLines = File.ReadAllLines(bagBoxFile);
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
            int p2Num = rng.Next(0,4);

            if (p2Num == 0) { p2 = new Charmander(); }
            else if (p2Num == 1) { p2 = new Squirtle(); }
            else if (p2Num == 2) { p2 = new Healymon(); }
            else { p2 = new Bulbasaur(); }
            
            int starterChoiceInt;

            while(true) {

                Console.WriteLine("Enter the number of the Pokemon you would like to use!\n1) Bulbasaur\n2) Charmander\n3) Squirtle\n4) Healymon");
                string? starterChoiceString = Console.ReadLine();

                starterChoiceString ??= "NULL_ENTRY";

                try {

                    starterChoiceInt = Int32.Parse(starterChoiceString);
                    break;
                }
                catch {
                    
                    Console.WriteLine("Please enter a number 1-4");
                    continue;
                }
            }

            if (starterChoiceInt == 1) { p1 = new Bulbasaur(); }
            else if (starterChoiceInt == 2) { p1 = new Charmander(); }
            else if (starterChoiceInt == 3) { p1 = new Squirtle(); }
            else { p1 = new Healymon(); }

            
            turnNum = p1.speed >= p2.speed ? 0 : 1;
            inventory = 2;
            
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

                        if (attackChoiceString == "1") {
                            
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
                        else {

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

                    } 
                    else if (actionChoice.Equals("2")) {
                        
                        DisplayBattleScreen(p1, p2, 3);
                        
                        string? bagChoice = Console.ReadLine();
                        bagChoice ??= "NULL_ENTRY";

                        if ( bagChoice)

                    } 
                    else if (actionChoice.Equals("3")) {
                        
                        
                        DisplayBattleScreen(p1,p2,-1);
                        DisplayMessageBox("You can't run, fight like a man!", null);
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

        public void DisplayHealth(Pokemon p1, Pokemon p2) {

            Console.WriteLine($"\n{p1.name}: {GetHealthBar(p1)}\n{p2.name}: {GetHealthBar(p2)}\n");
        }

        public string GetHealthBar(Pokemon p) {

            if (p.health > 0) {
                double healthPercent = (double)p.health / p.originalHealth * 100;

                int numO = (int)Math.Round(healthPercent / 10);
                int numX = 10 - numO;

                return new string('O', numO) + new string('X', numX);
            }
            else { return "XXXXXXXXXX"; }
        }

        public void DisplayMenuBox() {

            foreach ( string line in menuBoxLines) {

                Console.WriteLine(line);
            }
        }

        public void DisplayAttackBox(Pokemon pokemon) {

            for (int i = 0; i < attackBoxLines.Length; i++) {

                string line = attackBoxLines[i];

                if (line.Contains("{attackNumber1}")) {
                    
                    string spaces1 = "";
                    string spaces2 = "";

                    if (pokemon.moveSet[0].name.Length < 15) {
                        for (int j = 0; j < 15 - pokemon.moveSet[0].name.Length; j++) { spaces1 += " "; }
                    }

                    if (pokemon.moveSet[1].name.Length < 15) {
                        for (int j = 0; j < 15 - pokemon.moveSet[1].name.Length; j++) { spaces2 += " "; }
                    }

                    line = attackBoxLines[i].Replace("{attackNumber1}", pokemon.moveSet[0].name + spaces1);
                    line = line.Replace("{attackNumber2}", pokemon.moveSet[1].name + spaces2);
                }

                Console.WriteLine(line);
            }
        } 
        
        
        public void DisplayBagScreen(int potionNumber) {

            for(int i = 0; i < bagBoxLines.Length; i++)
            {
                string line = bagBoxLines[i];

                if(line.Contains("{number}")) {

                    string spaces = "";
                    string potionNumberToString = potionNumber >= 0 ? potionNumber.ToString() : "0";
                    
                    if(potionNumberToString.Length < 8 ) { for(int j = 0; j < 43 - potionNumberToString.Length; j++) { spaces += " "; } } 

                    line = bagBoxLines[i].Replace("{number}", potionNumberToString + spaces);
                }

                Console.WriteLine(line);            
            }   
        }
          
        
        
        public void DisplayMessageBox(string? attackMessage, string? effectivenessMessage) {

            for (int i = 0; i < messageBoxLines.Length; i++ ) {

                string line = messageBoxLines[i];

                if (line.Contains("{moveBeingUsedMessage}")) {

                    string spaces = "";

                    if (attackMessage == null) {
                        line = messageBoxLines[i].Replace("{moveBeingUsedMessage}", "                                           ");
                    }
                    else {
                        if (attackMessage.Length < 43) {
                            for (int j = 0; j < 43 - attackMessage.Length; j++) { spaces += " "; }
                        }
                        else if (attackMessage.Length > 43) { attackMessage = "ATTACK MESSAGE IS TOO LONG.                 "; }

                        line = messageBoxLines[i].Replace("{moveBeingUsedMessage}", attackMessage + spaces);
                    }
                }

                else if (line.Contains("{effectivenessMessage}")) {

                    string spaces = "";

                    if (effectivenessMessage == null) {
                        line = messageBoxLines[i].Replace("{effectivenessMessage}", "                                           ");
                    }
                    else {
                        if (effectivenessMessage.Length < 43) {
                            for (int j = 0; j < 43 - effectivenessMessage.Length; j++) { spaces += " "; }
                        }
                        else if (effectivenessMessage.Length > 43) { effectivenessMessage = "EFFECTIVENESS MESSAGE IS TOO LONG.           "; }

                        line = messageBoxLines[i].Replace("{effectivenessMessage}", effectivenessMessage + spaces);
                    }
                }

                Console.WriteLine(line);
            }
        }

        public void DisplayBattleScreen(Pokemon user, Pokemon enemy, int menuCase = -1, Attack? usedMove = null, bool attackMissed = false) {

            for (int i = 0; i < battleSceneStringLines.Length; i++) {

                string line = battleSceneStringLines[i];

                if (line.Contains("{enemy.name}")) {

                    string spaces = "";

                    if (enemy.name.Length < 10) {
                        for (int j = 0; j < 10 - enemy.name.Length; j++) {
                            spaces += " ";
                        }
                    }
                    line = battleSceneStringLines[i].Replace("{enemy.name}", enemy.name + spaces);
                }

                if (line.Contains("{enemy.health}")) { 

                    string spaces = "";
                    string healthToString = enemy.health > 0 ? enemy.health.ToString() : "0";

                    if (healthToString.Length < 4) {
                        for (int j = 0; j < 4 - healthToString.Length; j++) {
                            spaces += " ";
                        }
                    }

                    line = battleSceneStringLines[i].Replace("{enemy.health}", healthToString + spaces);
                    line = line.Replace("{Game.GetHealthBar(enemy)}", GetHealthBar(enemy));

                }

                if (line.Contains("{user.name}")) {

                    string spaces = "";

                    if (user.name.Length < 10) {
                        for (int j = 0; j < 10 - user.name.Length; j++) {
                            spaces += " ";
                        }
                    }
                    line = battleSceneStringLines[i].Replace("{user.name}", user.name + spaces);

                }
                
                if (line.Contains("{user.health}")) {

                    string spaces = "";
                    string healthToString = user.health > 0 ? user.health.ToString() : "0";

                    if (healthToString.Length < 4) {
                        for (int j = 0; j < 4 - healthToString.Length; j++) {
                            spaces += " ";
                        }
                    }
                    line = battleSceneStringLines[i].Replace("{user.health}", healthToString + spaces);
                    line = line.Replace("{Game.GetHealthBar(user)}", GetHealthBar(user));

                }

                Console.WriteLine(line);

                
            }
            if (menuCase == -1) {}
            else if (menuCase == 0) { DisplayMenuBox(); }
            else if (menuCase == 1) { DisplayAttackBox(user); }
            else if (menuCase == 2 && usedMove != null) { 

                if (usedMove.name.Equals("Heal")) {
                    string attackMessage = usedMove.GetAttackMessage(turnNum % 2 == 0 ? user : enemy);
                    DisplayMessageBox(attackMessage, "                                           ");
                }
                else {

                    string attackMessage = usedMove.GetAttackMessage(turnNum % 2 == 0 ? user : enemy);
                    string effectivenessMessage;

                    if (!attackMissed) {
                        effectivenessMessage = usedMove.CalculateTypeEffectiveness(turnNum % 2 == 0 ? enemy : user).message;
                    }
                    else {
                        effectivenessMessage = "Attack Missed";
                    }

                    DisplayMessageBox(attackMessage, effectivenessMessage);
                }
            }
            else if (menuCase == 2 && usedMove == null) {
                DisplayMessageBox(null, null);
            }
            else if (menuCase == 3 ) { DisplayBagScreen(); }
        }

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
