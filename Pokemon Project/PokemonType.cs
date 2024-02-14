using System.Runtime.CompilerServices;

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

    class FireType : PokemonType {

        public FireType() { this.name = "Fire"; }
    }

    class WaterType : PokemonType {

        public WaterType() { this.name = "Water"; }
    }

    class GrassType : PokemonType {

        public GrassType() { this.name = "Grass"; }
    }

    class NormalType : PokemonType {

        public NormalType() { this.name = "Normal"; }
    }
    
}