/*
*   This creates the type matchups between the several different types. It does so by using Dictionaries
* to store a List of strings that line up with a string key. When an attack is used, it checks both
* the weakness and strength Dictionary and calculates whether or not an attack will do more damage to a
* pokemon through weakness and strength calculations. I would like to add more types to create a more
* more complex game that is more complex than the glorified rock paper scissors it is now. This will be
* implemented in the next version of this program.
*/
namespace PokemonProject {

    // This class handles creating the type matchups for the game
    class PokemonType {


        // Dictionary of the type weaknesses. The key is the current type and the list is what attacks it's weak against
        public Dictionary<string, List<string>> weaknessDict = [];

        // Dictionary of the type strengths. The key is the current type and the list is what attacks it's strong against
        public Dictionary<string, List<string>> strengthDict = [];

        /// <summary>
        /// Initializes all the type match ups and populates the dictionaries if they have not been already
        /// </summary>
        public void InitializeTypeMatchups() {
            
            if (weaknessDict.Count == 0) {

                weaknessDict.Add("Fire", ["Water"]);
                weaknessDict.Add("Water", ["Grass"]);
                weaknessDict.Add("Grass", ["Fire"]);
                weaknessDict.Add("Normal", ["Fighting"]);
                weaknessDict.Add("Fighting", []);
            }
            
            if (strengthDict.Count == 0) {
                strengthDict.Add("Fire", ["Grass", "Fire"]);
                strengthDict.Add("Water", ["Fire", "Water"]);
                strengthDict.Add("Grass", ["Water", "Grass"]);
                strengthDict.Add("Normal", []);
                strengthDict.Add("Fighting", ["Normal"]);
            }
        }
    }
    
}