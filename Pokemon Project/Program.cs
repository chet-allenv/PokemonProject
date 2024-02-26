namespace PokemonProject {
    
    class Program {
        
        /// <summary>
        /// Main function that runs the game.
        /// </summary>
        public static void Main(string[] args) {

            // Creates a new instance of Game    
            Game g = new();
            
            // Creates a while loop to run the game
            while(true) {
                
                // Calls the Game.RunGame() function. See Game.RunGame() for further documentation.
                g.RunGame();

                // Prompts the user if they would like to play it again
                Console.WriteLine("Play again?\n[1] Yes\n[2] No");
                // Stores their input into a possibly null variable.
                string? input = Console.ReadLine();

                // Checks what the user input.
                if (input == "1") { // If the user input 1, that means that the user would like to play again so it continues the loop
                    continue;
                }
                else if (input == "2") { // If the user input 2, that means that the user would like to exit so it breaks the loop
                    break;
                }
                // If the user enter something else, the program exits, thus breaking the loop.
                else { Console.WriteLine("Wrong character entered, exiting the program..."); break; }
            }
            
        }  
    }

}
