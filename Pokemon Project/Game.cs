
namespace PokemonProject {

    class Game {

        public readonly string battleSceneFile = "BattleScene.txt";
        public readonly string menuBoxFile = "MenuBox.txt";
        public readonly string attackBoxFile = "AttackBox.txt";
        public readonly string messageBoxFile = "MessageBox.txt";
        public readonly string bagScreenFile = "DisplayBagScreen.txt";
        public string[] battleSceneStringLines;
        public string[] menuBoxLines;
        public string[] attackBoxLines;
        public string[] messageBoxLines;
        public string[] bagScreenLines;
        public Game() {

            battleSceneStringLines = File.ReadAllLines(battleSceneFile);
            menuBoxLines = File.ReadAllLines(menuBoxFile);
            attackBoxLines = File.ReadAllLines(attackBoxFile);
            messageBoxLines = File.ReadAllLines(messageBoxFile);
            bagScreenLines = File.ReadAllLines(bagScreenFile);
        }

        public  void ClearConsole() 
        {   
            Sleep(1);
            Console.Clear();
        }

        public  void Sleep(int sleepTime) {
            sleepTime *= 1000;
            Thread.Sleep(sleepTime);
        }

        public void RunGame() {

            int turnNum = 0;
            
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

            
            
            
            while(p1.IsAlive() && p2.IsAlive()) {
                
                ClearConsole();
                DisplayBattleScreen(p1,p2,0);
                string? actionChoice = Console.ReadLine();

                actionChoice ??= "NULL_ENTRY";
                
                ClearConsole();
                if (actionChoice.Equals("1")) // Attack Box
                {   
                    
                    DisplayBattleScreen(p1, p2, 1);
                    string? attackChoiceString = Console.ReadLine();

                    if (attackChoiceString == "1") {
                        
                        
                        p1.Attack(0, p2);
                        DisplayBattleScreen(p1, p2, 2, p1.moveSet[0]);
                    }

                } 
                else if (actionChoice.Equals("2")) {
                    //DisplayBagScreen(p1, p2,);
                    Console.WriteLine("Choose what Item to use from your bag: [1] = Potion [2] = Back to Menu");

                } 
                else if (actionChoice.Equals("3")) {
                    
                    
                    DisplayBattleScreen(p1,p2,-1);
                    DisplayMessageBox("You can't run, fight like a man!", null);
                }
                else {
                    
                    DisplayBattleScreen(p1,p2,-1);
                    DisplayMessageBox("Please enter a number", null);
                    continue;
                }
            }
            
            
            

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
                bool lineModified = false;

                if (line.Contains("{attackNumber1}")) {
                    
                    string spaces1 = "";
                    string spaces2 = "";

                    if (pokemon.moveSet[0].name.Length < 15) {
                        for (int j = 0; j < 15 - pokemon.moveSet[0].name.Length; j++) { spaces1 += " "; }
                    }

                    if (pokemon.moveSet[1].name.Length < 15) {
                        for (int j = 0; j < 15 - pokemon.moveSet[1].name.Length; j++) { spaces2 += " "; }
                    }

                    attackBoxLines[i] = line.Replace("{attackNumber1}", pokemon.moveSet[0].name + spaces1);
                    attackBoxLines[i] = attackBoxLines[i].Replace("{attackNumber2}", pokemon.moveSet[1].name + spaces2);
                    lineModified = true;
                }

                if (!lineModified) { Console.WriteLine(line); }
                else { Console.WriteLine(attackBoxLines[i]); }
            }
        } 
        
        /*
        public void DisplayBagScreen(string? potion, string? exit)
        {
                for(int i = 0; i < bagScreenLines.Length; i++)
                {
                    string line = bagScreenLines[i];
                    bool isChanged = false;

                    if(line.Contains("{number}")) {
                        string spaces = "";
                        
                        if(potion == null){
                            bagScreenLines[i] = line.Replace("{number}", "                                           ");
                            isChanged = true;
                        } else {
                            if(potion.Length < 43)
                            {
                                for(int j = 0; j < 43 - potion.Length; j++) 
                                {
                                    spaces += " ";
                                } 
                            } else if(potion.Length > 43) {
                                potion = "ITEM MESSAGE IS TOO LONG.                  ";
                            } potion[i] = line.Replace("{number}", potion + spaces);
                            isChanged = true;
                        }
                    }            
                }    
          }
          */
        
        
        public void DisplayMessageBox(string? attackMessage, string? effectivenessMessage) {

            for (int i = 0; i < messageBoxLines.Length; i++ ) {

                string line = messageBoxLines[i];
                bool lineModified = false;

                if (line.Contains("{moveBeingUsedMessage}")) {

                    string spaces = "";

                    if (attackMessage == null) {
                        messageBoxLines[i] = line.Replace("{moveBeingUsedMessage}", "                                           ");
                        lineModified = true;
                    }
                    else {
                        if (attackMessage.Length < 43) {
                            for (int j = 0; j < 43 - attackMessage.Length; j++) { spaces += " "; }
                        }
                        else if (attackMessage.Length > 43) { attackMessage = "ATTACK MESSAGE IS TOO LONG.                 "; }

                        messageBoxLines[i] = line.Replace("{moveBeingUsedMessage}", attackMessage + spaces);
                        lineModified = true;
                    }
                }

                else if (line.Contains("{effectivenessMessage}")) {

                    string spaces = "";

                    if (effectivenessMessage == null) {
                        messageBoxLines[i] = line.Replace("{effectivenessMessage}", "                                           ");
                        lineModified = true;
                    }
                    else {
                        if (effectivenessMessage.Length < 43) {
                            for (int j = 0; j < 43 - effectivenessMessage.Length; j++) { spaces += " "; }
                        }
                        else if (effectivenessMessage.Length > 43) { effectivenessMessage = "EFFECTIVENESS MESSAGE IS TOO LONG.           "; }

                        messageBoxLines[i] = line.Replace("{effectivenessMessage}", effectivenessMessage + spaces);
                        lineModified = true;
                    }
                }


                if (!lineModified) { Console.WriteLine(line); }
                else { Console.WriteLine(messageBoxLines[i]); }
            }
        }

        public void DisplayBattleScreen(Pokemon user, Pokemon enemy, int menuCase = -1, Attack? usedMove = null) {

            for (int i = 0; i < battleSceneStringLines.Length; i++) {

                string line = battleSceneStringLines[i];
                bool lineModified = false;

                if (line.Contains("{enemy.name}")) {

                    string spaces = "";

                    if (enemy.name.Length < 10) {
                        for (int j = 0; j < 10 - enemy.name.Length; j++) {
                            spaces += " ";
                        }
                    }
                    battleSceneStringLines[i] = line.Replace("{enemy.name}", enemy.name + spaces);
                    lineModified = true;
                }

                if (line.Contains("{Game.GetHealthBar(enemy)}")) {

                    battleSceneStringLines[i] = line.Replace("{Game.GetHealthBar(enemy)}", GetHealthBar(enemy));
                    lineModified = true;
                }

                if (line.Contains("{user.name}")) {

                    string spaces = "";

                    if (user.name.Length < 10) {
                        for (int j = 0; j < 10 - user.name.Length; j++) {
                            spaces += " ";
                        }
                    }
                    battleSceneStringLines[i] = line.Replace("{user.name}", user.name + spaces);
                    lineModified = true;
                }
                
                if (line.Contains("{user.health}")) {

                    string spaces = "";
                    string healthToString = user.health.ToString();

                    if (healthToString.Length < 4) {
                        for (int j = 0; j < 4 - healthToString.Length; j++) {
                            spaces += " ";
                        }
                    }
                    battleSceneStringLines[i] = line.Replace("{user.health}", healthToString + spaces);
                    battleSceneStringLines[i] = battleSceneStringLines[i].Replace("{Game.GetHealthBar(user)}", GetHealthBar(enemy));
                    lineModified = true;

                }

                if (!lineModified) { Console.WriteLine(line); }
                else { Console.WriteLine( battleSceneStringLines[i]); }

                
            }
            if (menuCase == -1) {}
            else if (menuCase == 0) { DisplayMenuBox(); }
            else if (menuCase == 1) { DisplayAttackBox(user); }
            else if (menuCase == 2 && usedMove != null) { 

                if (usedMove.name.Equals("Heal")) {
                    string attackMessage = usedMove.Use(user);
                    DisplayMessageBox(attackMessage, "                                           ");
                }
                else {
                    string? attackMessage = usedMove.Use(user, enemy).attackMessage;
                    string? effectivenessMessage = usedMove.Use(user, enemy).effectivenessMessage;

                    DisplayMessageBox(attackMessage, effectivenessMessage);
                }
               
            }
        }

        public static void Main(string[] args) {

            Game g = new(); Random r = new();
            int p1Num = r.Next(0,4); int p2Num = r.Next(0,4);
            Pokemon p1; Pokemon p2;

            if (p1Num == 0) { p1 = new Charmander(); }
            else if (p1Num == 1) { p1 = new Squirtle(); }
            else if (p1Num == 2) { p1 = new Healymon(); }
            else { p1 = new Bulbasaur(); }

            if (p2Num == 0) { p2 = new Charmander(); }
            else if (p2Num == 1) { p2 = new Squirtle(); }
            else if (p2Num == 2) { p2 = new Healymon(); }
            else { p2 = new Bulbasaur(); }
            

            
            g.RunGame();
            

        }
    }
}