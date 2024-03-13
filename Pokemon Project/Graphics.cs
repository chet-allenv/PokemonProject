/*
*   This class handles reading and using the text files to display game screens to the user. There are
* five different text files that are all to be read by this class. Every text file serves a different
* purpose. Four of the five are a variation of the text box at the bottom of the screen and allows for
* the reader to read some essential information, like an attack being used, what keys to enter, etc.
* The fifth file is named BattleScene.txt and that shows the names and health bars of the enemy and
* user's pokemon as well as little images of a creature to give a visual idea of where the pokemon are.
* This class is used by the Game.cs class and holds all functions needed to give information to the
* user.
*/

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
                    if (pokemon.moveSet[0].name.Length < 16) {
                        for (int j = 0; j < 15 - pokemon.moveSet[0].name.Length; j++) { spaces1 += " "; }
                    }

                    // IF the second move of the pokemon is less than 15 characters in length, for every char of space, it adds to spaces2
                    if (pokemon.moveSet[1].name.Length < 16) {
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
        public void DisplayMessageBox(string? messageTop, string? messageBottom) {

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
                    
                    // 10 SPACES IS THE LENGTH OF THE PLACEHOLDER. THE POKEMON'S NAME MUST BE 10 CHARACTERS OR LESS IN LENGTH
                    // Checks if the name is within the scope of allowed lengths
                    if (enemy.name.Length < 11 ) { 

                        // Iterates through every character that is empty and adds a space to spaces
                        for (int j = 0; j < 10 - enemy.name.Length; j++) { spaces += " "; }

                        // Sets the placeholder to the enemy's name plus the amount of spaces.
                        line = battleSceneStringLines[i].Replace("{enemy.name}", enemy.name + spaces);
                    }
                    // FOR DEBUGGING If a pokemon's name is too long it will show NameTooLng.
                    else { line = battleSceneStringLines[i].Replace("{enemy.name}", "NameTooLng"); }
                    
                }

                // If the current line has the placeholder {enemy.health} that means that the enemy pokemon's health bar is to be
                // displayed there.
                if (line.Contains("{enemy.health}")) { 
                    
                    // Variable declaration
                    string spaces = ""; // Creates the spaces variable to keep track of the number of spaces that need to be added
                    string healthToString = enemy.health > 0 ? enemy.health.ToString() : "0"; // Converts the number of remaining health to a string

                    // MAXIMUM LENGTH OF healthToString IS 4 CHARACTERS LONG
                    // Checks if healthToString is within the scope of allowed lengths 
                    if (healthToString.Length < 5) {

                        // Iterates through every character that is empty and adds a space to spaces
                        for (int j = 0; j < 4 - healthToString.Length; j++) { spaces += " "; }

                        // Sets the placeholder to the enemy's healthToString plus the amount of spaces.
                        line = battleSceneStringLines[i].Replace("{enemy.health}", healthToString + spaces);
                    }
                    // FOR DEBUGGING If a pokemon's healthToString is too long it will show LONG.
                    else { line = battleSceneStringLines[i].Replace("{enemy.health}", "LONG");}

                    // Sets the placeholder for the enemy's health bar to its health bar by calling the Graphics.GetHealthBar() method. 
                    // Check Graphics.GetHealthBar() method for furthur documentation
                    line = line.Replace("{Game.GetHealthBar(enemy)}", GetHealthBar(enemy));

                }

                // If the current line has the placeholder {user.name} that means that the user pokemon's name is to be
                // displayed there.
                if (line.Contains("{user.name}")) {

                    // Creates the spaces variable to keep track of the number of spaces that need to be added
                    string spaces = "";

                    // 10 SPACES IS THE LENGTH OF THE PLACEHOLDER. THE POKEMON'S NAME MUST BE 10 CHARACTERS OR LESS IN LENGTH
                    // Checks if the name is within the scope of allowed lengths
                    if (user.name.Length < 11) {

                        // Iterates through every character that is empty and adds a space to spaces
                        for (int j = 0; j < 10 - user.name.Length; j++) { spaces += " "; }
                        
                        // Sets the placeholder to the user's name plus the amount of spaces.
                        line = battleSceneStringLines[i].Replace("{user.name}", user.name + spaces);
                    }
                    // FOR DEBUGGING If a pokemon's name is too long it will show NameTooLng.
                    else { line = battleSceneStringLines[i].Replace("{user.name}", "NameTooLng"); }
                }
                
                // If the current line has the placeholder {user.health} that means that the user pokemon's health bar is to be
                // displayed there.
                if (line.Contains("{user.health}")) {

                    // Variable declaration
                    string spaces = ""; // Creates the spaces variable to keep track of the number of spaces that need to be added
                    string healthToString = user.health > 0 ? user.health.ToString() : "0"; // Converts the number of remaining health to a string

                    // MAXIMUM LENGTH OF healthToString IS 4 CHARACTERS LONG
                    // Checks if healthToString is within the scope of allowed lengths 
                    if (healthToString.Length < 5) {

                        // Iterates through every character that is empty and adds a space to spaces
                        for (int j = 0; j < 4 - healthToString.Length; j++) { spaces += " "; }

                        // Sets the placeholder to the user's healthToString plus the amount of spaces.
                        line = battleSceneStringLines[i].Replace("{user.health}", healthToString + spaces);
                    }
                    // FOR DEBUGGING If a pokemon's healthToString is too long it will show LONG.
                    else { line = battleSceneStringLines[i].Replace("{user.health}", "LONG"); } 

                    // Sets the placeholder for the user's health bar to its health bar by calling the Graphics.GetHealthBar() method. 
                    // Check Graphics.GetHealthBar() method for furthur documentation
                    line = line.Replace("{Game.GetHealthBar(user)}", GetHealthBar(user));
                }

                Console.WriteLine(line);   
            }

            // CHECKING MENU CASES

            if (menuCase == -1) {} // If menuCase is -1, nothing happens
            else if (menuCase == 0) { DisplayMenuBox(); } // if menuCase is 0, Graphics.DisplayMenuBox() is called. See Graphics.DisplayMenuBox() for furthur documentation.
            else if (menuCase == 1) { DisplayAttackBox(user); } // If menuCase is 1, Graphics.DisplayAttackBox() is called. See Graphics.DisplayAttackBox() for furthur documentation.
            else if (menuCase == 2 && usedMove != null) {  // If menuCase is 2 and usedMove is NOT null

                // Variable Declaration

                // Sets attackMessage to the message that is returned from the Attack.GetAttackMessage() method. See Attack.GetAttackMessage() for furthur documentation
                // Depending on the current turn number, it displays the attack message of the user or enemy.
                string attackMessage = usedMove.GetAttackMessage(turnNum % 2 == 0 ? user : enemy); 
                string effectivenessMessage; // String that will be assigned a value later in the code.

                // CHECKS IF THE ATTACK MISSED
                if (!attackMissed) {

                    // Sets the effectiveness to the message that is returned from the Attack.CalculateTypeEffectiveness(). See Attack.CalculateTypeEffectiveness() for furthur documentation
                    // Depending on the current turn number, it displays the attack message of the user or enemy.
                    effectivenessMessage = usedMove.CalculateTypeEffectiveness(turnNum % 2 == 0 ? enemy : user).message;
                }
                else { effectivenessMessage = "Attack Missed"; } // If the attack missed sets the message to say so.

                // Calls the Graphics.DisplayMessageBox() method. See Graphics.DisplayMessageBox() for furthur documentation.
                DisplayMessageBox(attackMessage, effectivenessMessage);

            }
            // If menuCase is 2 and usedMove is null, a blank box is displayed via the Graphics.DisplayMessageBox() method. See Graphics.DisplayMessageBox() for furthur documentation.
            else if (menuCase == 2 && usedMove == null) { DisplayMessageBox(null, null); } 
            else if (menuCase == 3 ) { DisplayBagScreen(inventory); } // If menuCase is 3, Graphics.DisplayBagScreen() is called. See Graphics.DisplayBagScreen() for further documentation.
        }
    }
}