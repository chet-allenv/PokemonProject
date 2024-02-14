using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace PokemonProject {

    class Pokemon {

        public string name;
        public int attack;
        public int defence;
        public int special;
        public int speed;
        public int health;
        public int level;
        public PokemonType type;

        public readonly Random rng = new();

        public Pokemon(string name, PokemonType type, int level) {

            this.name = name;
            this.attack = GenerateStat();
            this.defence = GenerateStat();
            this.special = GenerateStat();
            this.speed = GenerateStat();
            this.health = GenerateStat();
            this.type = type;
            this.level = level;
        }

        public Pokemon(string name, PokemonType type) {

            this.name = name;
            this.attack = GenerateStat();
            this.defence = GenerateStat();
            this.special = GenerateStat();
            this.speed = GenerateStat();
            this.health = GenerateStat();
            this.type = type;
            this.level = 5;
        }

        public void DisplayStats() {

            Console.WriteLine($"Pokemon: {this.name}\nType: {this.type.name}\nHP: {this.health}\nAttack: {this.attack}\nDefence: {this.defence}\nSpecial: {this.special}\nSpeed: {this.speed}");
        }

        public int GenerateStat() {
            
            return rng.Next(10, 25) + 1 * (2 * level / 5 + 2);
        }

        public static void Main(string[] args) {

            PokemonType pt = new();
            pt.InitializeTypeMatchups();

            BubbleAttack bubble = new();

            Charmander c = new();
            Squirtle s = new();

            c.DisplayStats();
            s.DisplayStats();

            bubble.Use(s, c);

            c.DisplayStats();
            s.DisplayStats();            
        }
    }
    class Charmander : Pokemon {
        public Charmander() : base("Charmander", new FireType()) {}
        public Charmander(int level) : base("Charmander", new FireType(), level) {}
    }

    class Squirtle : Pokemon {
        public Squirtle() : base("Squirtle", new WaterType()) {}
        public Squirtle(int level) : base("Squirtle", new FireType(), level) {}
    }
    //hello chet
}