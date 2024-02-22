
namespace PokemonProject {

    class Pokemon {

        public string name;
        public int attack;
        public int defence;
        public int special;
        public int speed;
        public int health;
        public readonly int originalHealth;
        public int level;
        public string type;
        public Attack[] moveSet;

        public readonly Random rng = new();

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

        public Pokemon(string name, string type) {

            this.name = name;
            this.attack = GenerateStat();
            this.defence = GenerateStat();
            this.special = GenerateStat();
            this.speed = GenerateStat();
            this.health = GenerateStat();
            this.originalHealth = health;
            moveSet = new Attack[2];
            this.type = type;
            this.level = 5;
        }

        public void DisplayStats() {

            Console.WriteLine($"Pokemon: {this.name}\nType: {this.type}\nHP: {this.health}\nAttack: {this.attack}\nDefence: {this.defence}\nSpecial: {this.special}\nSpeed: {this.speed}");
        }

        public int GenerateStat() {
            
            return rng.Next(10, 25) + 1 * (2 * level / 5 + 2);
        }

        public bool IsAlive() { return health >= 0; }

        public virtual void Attack(Pokemon target) { Console.WriteLine("ATTACK IS USED"); }
        public virtual void Attack(int attackNumber, Pokemon target) { Console.WriteLine("ATTACK IS USED"); }

    }
    class Charmander : Pokemon {

        public Charmander() : base("Charmander", "Fire") { moveSet = [new TackleAttack(), new EmberAttack()]; }
        public Charmander(int level) : base("Charmander", "Fire", level) { moveSet = [new TackleAttack(), new EmberAttack()]; }

        public override void Attack(Pokemon target) {

            int num = rng.Next(0, moveSet.Length);

            moveSet[num].Use(this, target);
        }
    }

    class Squirtle : Pokemon {

        public Squirtle() : base("Squirtle", "Water") { moveSet = [new TackleAttack(), new BubbleAttack()]; }
        public Squirtle(int level) : base("Squirtle", "Water", level) { moveSet = [new TackleAttack(), new BubbleAttack()]; }

        public override void Attack(Pokemon target) {

            int num = rng.Next(0, moveSet.Length);

            moveSet[num].Use(this, target);
        }
        public override void Attack(int attackNumber, Pokemon target) {
            moveSet[attackNumber].Use(this, target);
        }
    }

    class Bulbasaur : Pokemon {

        public Bulbasaur() : base("Bulbasaur" , "Grass") { moveSet = [new TackleAttack(), new VineWhipAttack()]; }
        public Bulbasaur(int level) : base("Bulbasaur", "Grass", level) { moveSet = [new TackleAttack(), new VineWhipAttack()]; }

        public override void Attack(Pokemon target) {

            int num = rng.Next(0, moveSet.Length);

            moveSet[num].Use(this, target);
        }
    }
    
    class Healymon : Pokemon {

        public Healymon() : base("Healymon", "Normal") { moveSet = [new TackleAttack(), new HealMove()]; }
        public Healymon(int level) : base("Healymon", "Normal", level) { moveSet = [new TackleAttack(), new HealMove()]; }

        public override void Attack(Pokemon target) {

            int num = rng.Next(0, moveSet.Length);

            if (moveSet[num].name.Equals("Heal")) {
                moveSet[num].Use(this, this);
            }
            else { moveSet[num].Use(this, target); }
        }
    }
}