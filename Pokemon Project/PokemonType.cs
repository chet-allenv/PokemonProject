using System.Runtime.CompilerServices;

namespace PokemonProject {

    class PokemonType {

        public string name;
        public List<PokemonType> weakAgainst;
        public List<PokemonType> strongAgainst;

        private Dictionary<PokemonType, List<PokemonType>> weaknessDict = new Dictionary<PokemonType, List<PokemonType>>();
        private Dictionary<PokemonType, List<PokemonType>> strengthDict = new Dictionary<PokemonType, List<PokemonType>>();

        public PokemonType() {
            this.weakAgainst = new List<PokemonType>();
            this.strongAgainst = new List<PokemonType>();
            this.name = "NULL_TYPE";
        }

        public void InitializeTypeMatchups() {

            FireType fire = new();
            WaterType water = new();
            GrassType grass = new();
            NormalType normal = new();

            weaknessDict.Add(fire, new List<PokemonType> {water});
            weaknessDict.Add(water, new List<PokemonType> {grass});
            weaknessDict.Add(grass, new List<PokemonType> {fire});
            weaknessDict.Add(normal, new List<PokemonType> {});

            strengthDict.Add(fire, new List<PokemonType> {grass});
            strengthDict.Add(water, new List<PokemonType> {fire});
            strengthDict.Add(grass, new List<PokemonType> {water});
            strengthDict.Add(normal, new List<PokemonType> {});
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