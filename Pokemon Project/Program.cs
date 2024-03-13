/*
*   This program's function is to be the main method of sorts for the project and run the game. It also
* asks the user to play again once the game is over. I ran into a lot of issues while developing this
* project. The first issue I ran into was type matchups. I didn't really have a good way of instantiating
* the type matchups with out creating a new object instance everytime an attack was used. This then led to
* the creation of A LOT of one time used classes. I got over this by only instantiating the class when
* a new Attack class is created. This VASTLY reduced the ammount of one time use PokemonType classes there
* were. As well as this, at first I tried to make types (Fire, Water, Grass, etc.) an instance of a class.
* So there were several different classes for every type that made it difficult to actually compare types.
* They also essentially only carried a predetermined string that I decided to eventually just replace with
* a string. I also ran into LOADS of problems with loading text files. This is something I had very little
* experience with. I wanted the text contained within a text file to change based on different variables.
* I spent a few days trying to come up with how this would happen and I believe that I created a good UI.
* My goals for the final are simply adding a UI and not making the gaem based in a terminal. I would also
* like to create my own pokemon and be able to kinda add my own spin to the pokemon franchise. I dont want
* to just copy what pokemon does, I want to make it my own.
*/
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
