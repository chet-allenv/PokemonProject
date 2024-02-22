
namespace PokemonProject {

    class PokemonType {

        public string name;

        public Dictionary<string, List<string>> weaknessDict = [];
        public Dictionary<string, List<string>> strengthDict = [];

        public PokemonType() {
            this.name = "NULL_TYPE";
        }

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