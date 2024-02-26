namespace PokemonProject {
    
    /// <summary>
    /// This class handles all of the functions that display the screen and boxes during the game. 
    /// </summary>
    class Graphics {

        // Variable Declarations

        // These are strings that hold the file path names, Mmakes it easier when programming.
        public readonly string battleSceneFile = "BattleScene.txt";
        public readonly string menuBoxFile = "MenuBox.txt";
        public readonly string attackBoxFile = "AttackBox.txt";
        public readonly string messageBoxFile = "MessageBox.txt";
        public readonly string bagBoxFile = "BagBox.txt";

        // These are the arrays that store every line of the respective files, line by line
        public string[] battleSceneStringLines;
        public string[] menuBoxLines;
        public string[] attackBoxLines;
        public string[] messageBoxLines;
        public string[] bagBoxLines;

        /// <summary>
        /// Constructor of Graphics that popualtes all of the file string arrays with with every line in the coresponding file
        /// </summary>
        public Graphics() {

            battleSceneStringLines = File.ReadAllLines(battleSceneFile);
            menuBoxLines = File.ReadAllLines(menuBoxFile);
            attackBoxLines = File.ReadAllLines(attackBoxFile);
            messageBoxLines = File.ReadAllLines(messageBoxFile);
            bagBoxLines = File.ReadAllLines(bagBoxFile);
        }
        
        /// <summary>
        /// This creates the healthbar shown during combat. it returns a 10 character long string that represents the 
        /// health of the pokemon with X's and O's. The O's display the percentage of health remaining and the X's show
        /// the missing health
        /// </summary>
        /// <param name="pokemon"> This is the pokemon that we are getting the health of </param>
        /// <returns></returns>
        public string GetHealthBar(Pokemon pokemon) {
            
            // Checks if the Pokemon's health is above 0
            if (pokemon.health > 0) {

                // Gets the percent of health remaining by dividing the current health by the maximum health and multiplying it by 100
                double healthPercent = (double)pokemon.health / pokemon.originalHealth * 100;

                // The number of O's is calculated by divinding the percent by 10 and rounding it to the nearest int
                int numO = (int)Math.Round(healthPercent / 10);
                int numX = 10 - numO; // Number of X's is calculated by subracting numO from 10

                // Returns a string with numO number of char O and numX number of char X
                return new string('O', numO) + new string('X', numX);
            }
            else { return "XXXXXXXXXX"; } // If pokemon has zero or less health displays all X's
        }

        /// <summary>
        /// Displays the MenuBox.txt file
        /// </summary>
        public void DisplayMenuBox() {

            // Iterates through every string in menuBoxLines and prints it to the console
            foreach ( string line in menuBoxLines) { Console.WriteLine(line); }
        }

        /// <summary>
        ///  Displays the AttackBox.txt file and replaces some specific lines with information
        /// </summary>
        /// <param name="pokemon"> the user's pokemon </param>
        public void DisplayAttackBox(Pokemon pokemon) {
            
            // Iterates through every line in attackBoxLines
            for (int i = 0; i < attackBoxLines.Length; i++) {
                
                // Sets the current line to a temporary string variable named line
                string line = attackBoxLines[i];

                // Checks if line has the placeholder text for certain variables

                // If line contains {attackNumber1} that means that the line is trying to display the 1st attack
                // because there is only one line with the placeholders but there is two placeholders in this line,
                // it replaces both.
                if (line.Contains("{attackNumber1}")) {
                    
                    // Creates 2 variables to keep track of the number of spaces that need to be added
                    string spaces1 = "";
                    string spaces2 = "";
                    
                    // 15 is the number of spaces that both {attackNumber1} and {attackNumber2} displace
                    // IF the first move of the pokemon is less than 15 characters in length, for every char of space, it adds to spaces1
                    if (pokemon.moveSet[0].name.Length < 15) {
                        for (int j = 0; j < 15 - pokemon.moveSet[0].name.Length; j++) { spaces1 += " "; }
                    }

                    // IF the second move of the pokemon is less than 15 characters in length, for every char of space, it adds to spaces2
                    if (pokemon.moveSet[1].name.Length < 15) {
                        for (int j = 0; j < 15 - pokemon.moveSet[1].name.Length; j++) { spaces2 += " "; }
                    }

                    // Sets the line to replace the placeholders with the name of the move plus the spaces so that 
                    // it prints without messing up the way the box looks
                    line = attackBoxLines[i].Replace("{attackNumber1}", pokemon.moveSet[0].name + spaces1);
                    line = line.Replace("{attackNumber2}", pokemon.moveSet[1].name + spaces2);
                }

                // Writes the line to the console
                Console.WriteLine(line);
            }
        }

        /// <summary>
        ///  Displays the BagBox.txt file and replaces some specific lines with information
        /// </summary>
        /// <param name="potionNumber"> the ammount of potions the user has left </param>
        public void DisplayBagScreen(int potionNumber) {

            // Iterates through every line in bagBoxLines
            for(int i = 0; i < bagBoxLines.Length; i++) {

                // Sets the current line to a temporary string variable named line
                string line = bagBoxLines[i];

                // If line contains {number} that means that the line is trying to display the number of potions
                if(line.Contains("{number}")) {

                    // Declares the spaces variable and the potionNumber converted to a string. If potionNumber is less zero, it sets the string to "0" 
                    string spaces = "";
                    string potionNumberToString = potionNumber >= 0 ? potionNumber.ToString() : "0";
                    
                    // 8 is the number of spaces that {number} displaces
                    // Checks if the length of potionNumberToString is less than 8. If it is it adds to spaces for however many
                    // spaces less it is
                    if(potionNumberToString.Length < 8 ) { for(int j = 0; j < 8 - potionNumberToString.Length; j++) { spaces += " "; } } 

                    // Sets the line to replace the placeholders with the name of the move plus the spaces so that 
                    // it prints without messing up the way the box looks
                    line = bagBoxLines[i].Replace("{number}", potionNumberToString + spaces);
                }

                // Writes the line to the console
                Console.WriteLine(line);            
            }   
        }

        /// <summary>
        /// Displays the MessageBox.txt file as well as replacing the placeholders with strings
        /// </summary>
        /// <param name="messageTop"> This is the message that is to be displayed at the top. CAN BE NULL </param>
        /// <param name="messageBottom"> this is the message that is to be displayed at the bottom. CAN BE NULL </param>
        public void DisplayMessageBox(string? messageTop, string? effectivenessMessage) {

            // Iterates through every line in messageBoxLines
            for (int i = 0; i < messageBoxLines.Length; i++ ) {

                // Sets the current line to a temporary string variable named line
                string line = messageBoxLines[i];

                // If line contains {moveBeingUsedMessage} that means that the line is trying to display the top message
                if (line.Contains("{moveBeingUsedMessage}")) {
                    
                    // Creates the spaces variable to keep track of the number of spaces that need to be added
                    string spaces = "";

                    // 43 SPACES IS THE LENGTH OF THE PLACEHOLDER
                    // If the top message is null, that means that the message will be empty so it replaces the placeholder with 
                    // 43 empty spaces.
                    if (messageTop == null) { line = messageBoxLines[i].Replace("{moveBeingUsedMessage}", new string(' ', 43)); }
                    else { // if the top message is not null
                        
                        // Checks if the message is less than 43 characters.
                        // If it is, it adds to spaces the amount of space that is to be blank
                        if (messageTop.Length < 43) { for (int j = 0; j < 43 - messageTop.Length; j++) { spaces += " "; } }
                        // If it is too long, it will display the the message is too long. THIS IS FOR DEBUGGING
                        else if (messageTop.Length > 43) { messageTop = "ATTACK MESSAGE IS TOO LONG.                 "; }

                        // Replaces the placeholder with the top message and spaces
                        line = messageBoxLines[i].Replace("{moveBeingUsedMessage}", messageTop + spaces);
                    }
                }
                // If line contains {effectivenessMessage} that means that the line is trying to display the bottom message
                else if (line.Contains("{effectivenessMessage}")) {
                    
                    // Creates the spaces variable to keep track of the number of spaces that need to be added
                    string spaces = "";

                    // 43 SPACES IS THE LENGTH OF THE PLACEHOLDER
                    // If the bottom message is null, that means that the message will be empty so it replaces the placeholder with 
                    // 43 empty spaces.
                    if (messageBottom == null) { line = messageBoxLines[i].Replace("{effectivenessMessage}", new string(' ', 43)); }
                    else { // if the bottom message is not null

                        // Checks if the message is less than 43 characters.
                        // If it is, it adds to spaces the amount of space that is to be blank
                        if (messageBottom.Length < 43) { for (int j = 0; j < 43 - messageBottom.Length; j++) { spaces += " "; } }
                        // If it is too long, it will display the the message is too long. THIS IS FOR DEBUGGING
                        else if (messageBottom.Length > 43) { messageBottom = "EFFECTIVENESS MESSAGE IS TOO LONG.           "; }

                        // Replaces the placeholder with the top message and spaces
                        line = messageBoxLines[i].Replace("{effectivenessMessage}", messageBottom + spaces);
                    }
                }

                // Writes the line to the console
                Console.WriteLine(line);
            }
        }
        /// <summary>
        /// Displays the BattleScene.txt file as well as replacing the placeholders with strings
        /// </summary>
        /// <param name="inventory"> Number of potions in the user's inventory </param>
        /// <param name="turnNum"> the current turn number </param>
        /// <param name="user"> the user's pokemon </param>
        /// <param name="enemy"> the enemy pokemon </param>
        /// <param name="menuCase"> the case that displays the text box. BY DEFAULT IS -1 </param>
        /// <param name="usedMove"> the used attack. BY DEFAULT IS NULL </param>
        /// <param name="attackMissed"> is the attack miss or not. BY DEFAULT IS FALSE </param>
        public void DisplayBattleScreen(int inventory, int turnNum, Pokemon user, Pokemon enemy, int menuCase = -1, Attack? usedMove = null, bool attackMissed = false) {
            
            // Iterates through every line in battleSceneStringLines 
            for (int i = 0; i < battleSceneStringLines.Length; i++) {

                // Sets the current line to a temporary string variable named line
                string line = battleSceneStringLines[i];

                // CHECKING FOR PLACE HOLDERS

                // If the current line has the placeholder {enemy.name} that means that the enemy pokemon's name is to be
                // displayed there.
                if (line.Contains("{enemy.name}")) {

                    // Creates the spaces variable to keep track of the number of spaces that need to be added
                    string spaces = "";
                    
                    // 10 SPACES IS THE LENGTH OF THE PLACEHOLDER
                    if (enemy.name.Length < 11 ) { 
                        for (int j = 0; j < 10 - enemy.name.Length; j++) { 
                            spaces += " "; 
                        }

                        line = battleSceneStringLines[i].Replace("{enemy.name}", enemy.name + spaces);
                    }
                    else { line = battleSceneStringLines[i].Replace("{enemy.name}", "NameTooLng"); }
                    
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