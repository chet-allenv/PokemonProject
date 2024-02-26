
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