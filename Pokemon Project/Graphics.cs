namespace PokemonProject {
    
    class Graphics {

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
        public Graphics() {

            battleSceneStringLines = File.ReadAllLines(battleSceneFile);
            menuBoxLines = File.ReadAllLines(menuBoxFile);
            attackBoxLines = File.ReadAllLines(attackBoxFile);
            messageBoxLines = File.ReadAllLines(messageBoxFile);
            bagBoxLines = File.ReadAllLines(bagBoxFile);
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
                    
                    if(potionNumberToString.Length < 8 ) { for(int j = 0; j < 8 - potionNumberToString.Length; j++) { spaces += " "; } } 

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
        public void DisplayBattleScreen(int inventory, int turnNum, Pokemon user, Pokemon enemy, int menuCase = -1, Attack? usedMove = null, bool attackMissed = false) {

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
            else if (menuCase == 2 && usedMove == null) {
                DisplayMessageBox(null, null);
            }
            else if (menuCase == 3 ) { DisplayBagScreen(inventory); }
        }
    }
}