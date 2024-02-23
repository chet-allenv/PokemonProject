
namespace PokemonProject {

    /// <summary>
    /// The parent class that will be used to create every pokemon
    /// </summary>
    class Pokemon {

        // Variable Declaration
        public string name; // Pokemon's name. MUST BE 10 CHARACTERS OR LESS IN LENGTH.
        public int attack; // Pokemon's attack stat
        public int defence; // Pokemon's defence stat
        public int special; // Pokemon's special stat
        public int speed; // Pokemon's speed stat
        public int health; // Pokemon's health that can be changed
        public readonly int originalHealth; // The orignal health stat of the pokemon
        public int level; // The level of the pokemon
        public string type; // The type of the Pokemon

        // The array of moves the pokemon can use
        // CAN ONLY HOLD 2 Attack INSTANCES
        public Attack[] moveSet; 

        // Random instance that will be used to generate random numbers
        public readonly Random rng = new();

        // CONSTRUCTORS

        // Constructor WITH level
        public Pokemon(string name, string type, int level) {

            this.name = name;
            this.attack = GenerateStat();
            this.defence = GenerateStat();
            this.special = GenerateStat();
            this.speed = GenerateStat();
            this.health = GenerateStat();
            this.originalHealth = health;
            moveSet = new Attack[2];
            this.type = type;
            this.level = level;
        }
        
        // Constructor WITHOUT level
        public Pokemon(string name, string type) {

            this.name = name;
            this.attack = GenerateStat();
            this.defence = GenerateStat();
            this.special = GenerateStat();
            this.speed = GenerateStat();
            this.health = GenerateStat(30, 70);
            this.originalHealth = health;
            moveSet = new Attack[2];
            this.type = type;
            this.level = 10;
        }

        // Displays the stats of the pokemon
        public string DisplayStats() {

            return $"Pokemon: {this.name}\nType: {this.type}\nBase HP: {this.originalHealth}\nCurrent HP: {this.health}\nAttack: {this.attack}\nDefence: {this.defence}\nSpecial: {this.special}\nSpeed: {this.speed}";
        }

        /// <summary>
        /// Used to generate random numbers with the given low and high
        /// </summary>
        /// <param name="low"> the low boundary; If none given it is 5 </param>
        /// <param name="high"> the high boundary; If none given it is 25 </param>
        /// <returns> the int that was generated </returns>
        public int GenerateStat(int low = 5, int high = 25) {
            
            return rng.Next(low, high) + 2 * (2 * level / 5 + 2);
        }

        // Checks if the pokemon's health is above zero
        public bool IsAlive() { return health > 0; }

        /// <summary>
        /// Attack method that uses the attack that relates to the given index
        /// </summary>
        /// <param name="attackNumber"> the number of attack used </param>
        /// <param name="target"> the target of the attack </param>
        public void Attack(int attackNumber, Pokemon target) {
            moveSet[attackNumber].Use(this, target);
        }

    }

    /// <summary>
    /// This is the Charmander Class that is a fire type pokemon and has a special
    /// move called Ember
    /// </summary>
    class Charmander : Pokemon {

        public Charmander() : base("Charmander", "Fire") { moveSet = [new TackleAttack(), new EmberAttack()]; }
        public Charmander(int level) : base("Charmander", "Fire", level) { moveSet = [new TackleAttack(), new EmberAttack()]; }

    }

    /// <summary>
    /// This is the Squirtle Class that is a water type pokemon and has a special
    /// move called Bubble
    /// </summary>
    class Squirtle : Pokemon {

        public Squirtle() : base("Squirtle", "Water") { moveSet = [new TackleAttack(), new BubbleAttack()]; }
        public Squirtle(int level) : base("Squirtle", "Water", level) { moveSet = [new TackleAttack(), new BubbleAttack()]; }
    }

    /// <summary>
    /// This is the Bulbasaur Class that is a grass type pokemon and has a special
    /// move called Vine Whip
    /// </summary>
    class Bulbasaur : Pokemon {

        public Bulbasaur() : base("Bulbasaur" , "Grass") { moveSet = [new TackleAttack(), new VineWhipAttack()]; }
        public Bulbasaur(int level) : base("Bulbasaur", "Grass", level) { moveSet = [new TackleAttack(), new VineWhipAttack()]; }
    }
}