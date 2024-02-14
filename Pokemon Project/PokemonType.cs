
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
                weaknessDict.Add("Normal", []);
            }
            
            if (strengthDict.Count == 0) {
                strengthDict.Add("Fire", ["Grass"]);
                strengthDict.Add("Water", ["Fire"]);
                strengthDict.Add("Grass", ["Water"]);
                strengthDict.Add("Normal", []);
            }
        }
    }
    
}