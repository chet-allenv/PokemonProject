
namespace PokemonProject {

    class Game {

        public readonly string battleSceneFile = "BattleScene.txt";
        public readonly string menuBoxFile = "MenuBox.txt";
        public readonly string attackBoxFile = "AttackBox.txt";
        public readonly string messageBoxFile = "MessageBox.txt";
        public string[] battleSceneStringLines;
        public string[] menuBoxLines;
        public string[] attackBoxLines;
        public string[] messageBoxLines;
        public Game() {

            battleSceneStringLines = File.ReadAllLines(battleSceneFile);
            menuBoxLines = File.ReadAllLines(menuBoxFile);
            attackBoxLines = File.ReadAllLines(attackBoxFile);
            messageBoxLines = File.ReadAllLines(messageBoxFile);
        }

        public void RunGame(Pokemon p1, Pokemon p2) {

            int turnNum = 0;
            DisplayHealth(p1,p2);

            while (p1.IsAlive() && p2.IsAlive()) {

                turnNum++;

                Pokemon first;
                Pokemon second;

                if (p1.speed >= p2.speed) { first = p1; second = p2; }
                else { first = p2; second = p1; }

                first.Attack(second);
                DisplayHealth(p1,p2);
                if (!second.IsAlive()) { Console.WriteLine($"{second.name} fainted. {first.name} wins the battle"); break;}

                second.Attack(first);
                DisplayHealth(p1,p2);
                if (!first.IsAlive()) { Console.WriteLine($"{first.name} fainted. {second.name} wins the battle"); }
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

        public void DisplayBattleScreen(Pokemon user, Pokemon enemy, int menuCase, Attack? usedMove = null) {

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

                    battleSceneStringLines[i] = line.Replace("{Game.GetHealthBar(enemy)}", this.GetHealthBar(enemy));
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
                    battleSceneStringLines[i] = battleSceneStringLines[i].Replace("{Game.GetHealthBar(user)}", this.GetHealthBar(enemy));
                    lineModified = true;

                }

                if (!lineModified) { Console.WriteLine(line); }
                else { Console.WriteLine( battleSceneStringLines[i]); }

                
            }

            if (menuCase == 0) { DisplayMenuBox(); }
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
            

            g.DisplayBattleScreen(p1,p2, 2, p1.moveSet[1]);
        }
    }
}