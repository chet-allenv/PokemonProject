using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace PokemonProject {

    class Attack {

        public int power;
        public int accuracy;

        public string name;
        
        public Attack(int power, int accuracy, string name) {
            this.power = power;
            this.accuracy = accuracy;
            this.name = name;
        }
    }

    class PhysicalAttack : Attack{

        public string attackType = "physical";
        public PokemonType moveType;

        public PhysicalAttack(int power, int accuracy, PokemonType type, string name) : base(power, accuracy, name) { this.moveType = type; } 

        public void Use(Pokemon user, Pokemon target) {
            
            Random rng = new();

            int testAccuracy = rng.Next(0, 101);

            Console.WriteLine($"{user.name} used {this.name}!");

            if (testAccuracy <= accuracy) {

                double STAB = (moveType.GetType() == user.type.GetType()) ? 1.5 : 1.0;
                double typeEffectiveness = 1.0;
                int randomNumber = rng.Next(217, 256);

                if (moveType.GetType() == target.type.GetType() || target.type.strongAgainst.Contains(moveType)) {
                    typeEffectiveness = 0.5;
                    Console.WriteLine("It's not very effective...");
                }
                else if (target.type.weakAgainst.Contains(moveType)) {
                    typeEffectiveness = 2.0;
                    Console.WriteLine("It's super effective!");
                }

                double d = ((2 * user.level / 5 + 2) * user.attack * power / target.defence / 50 + 2) * STAB * typeEffectiveness * randomNumber / 100;
                int damage = (int) d;

                target.health -= damage;

            }

            else {
                Console.WriteLine("Attack Missed.");
            }
        }
    }

    class SpecialAttack : Attack {

        public string attackType = "special";
        public PokemonType moveType;

        public readonly Random rng = new();

        public SpecialAttack(int power, int accuracy, PokemonType type, string name) : base(power, accuracy, name) { this.moveType = type; }

        public void Use(Pokemon user, Pokemon target) {
            
            int testAccuracy = rng.Next(0, 101);

            Console.WriteLine($"{user.name} used {this.name}!");

            if (testAccuracy <= accuracy) {

                double STAB = (moveType.GetType() == user.type.GetType()) ? 1.5 : 1.0;
                double typeEffectiveness = CalculateTypeEffectiveness(target);

                int damage = CalculateDamage(user, target, STAB, typeEffectiveness);

                target.health -= damage;

            }

            else {
                Console.WriteLine("Attack Missed.");
            }
        }

        public int CalculateDamage(Pokemon user, Pokemon target, double STAB, double typeEffectiveness) {

            int randomNumber = rng.Next(217, 256);

            double d = ((2 * user.level / 5 + 2) * user.special * power / target.special / 50 + 2) * STAB * typeEffectiveness * randomNumber / 100;
            int damage = (int) d;

            return damage;
        }

        public double CalculateTypeEffectiveness(Pokemon target) {

            PokemonType pt = new();
            pt.InitializeTypeMatchups();
            
            double effectiveness = 1.0;

            foreach (var type in target.type.strongAgainst) {
                
                if (type.name.Equals(moveType.name)) {
                    Console.WriteLine("It's not very effective...");
                    effectiveness = 0.5;
                }
            }
            
            foreach (var type in target.type.weakAgainst) {
                
                if (type.name.Equals(moveType.name)) {

                    Console.WriteLine("It's super effective!");
                    effectiveness = 2.0;
                }
            }

            if (target.type.name.Equals(moveType.name)) {
                effectiveness = 0.5;
            }

            return effectiveness;
        }
    }

    class TackleAttack : PhysicalAttack {

        public TackleAttack() : base(30, 80, new NormalType(), "Tackle") {}
    }

    class EmberAttack : SpecialAttack {
        
        public EmberAttack() : base(30, 95, new FireType(), "Ember") {}
    }

    class BubbleAttack : SpecialAttack {
        public BubbleAttack() : base(30, 100, new WaterType(), "Bubble") {}
    }

    class VineWhipAttack : SpecialAttack {

        public VineWhipAttack() : base(30, 90, new GrassType(), "Vine Whip") {}
    }
}