using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace PokemonProject {

    class Attack {

        public int power;
        public int accuracy;

        public string name;

        public readonly PokemonType pt = new();
        
        public Attack(int power, int accuracy, string name) {
            this.power = power;
            this.accuracy = accuracy;
            this.name = name;

            pt.InitializeTypeMatchups();
        }
        
        public virtual void Use(Pokemon user, Pokemon target) { Console.WriteLine("Attack used"); }
    }

    class PhysicalAttack : Attack{

        public string attackType = "physical";
        public PokemonType moveType;

        public PhysicalAttack(int power, int accuracy, PokemonType type, string name) : base(power, accuracy, name) { this.moveType = type; } 
        public readonly Random rng = new();

        
        public override void Use(Pokemon user, Pokemon target) {
            
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

            double d = ((2 * user.level / 5 + 2) * user.attack * power / target.defence / 50 + 2) * STAB * typeEffectiveness * randomNumber / 100;
            int damage = (int) d;

            return damage;
        }

        public double CalculateTypeEffectiveness(Pokemon target) {

            pt.strengthDict.TryGetValue(target.type.name, out List<string>? targetStrengths);
            pt.weaknessDict.TryGetValue(target.type.name, out List<string>? targetWeaknesses);

            targetStrengths ??= [];
            targetWeaknesses ??= [];

            double effectiveness = 1.0;

            if (targetStrengths.Contains(moveType.name) || target.type.name.Equals(moveType.name)) { effectiveness = 0.5; Console.WriteLine("It's not very effective"); }
            else if (targetWeaknesses.Contains(moveType.name)) { effectiveness = 2.0; Console.WriteLine("It's super effective!"); }
            
            return effectiveness;
        }
    }

    class SpecialAttack : Attack {

        public string attackType = "special";
        public PokemonType moveType;

        public readonly Random rng = new();

        public SpecialAttack(int power, int accuracy, PokemonType type, string name) : base(power, accuracy, name) { this.moveType = type; }

        public override void Use(Pokemon user, Pokemon target) {
            
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

            pt.strengthDict.TryGetValue(target.type.name, out List<string>? targetStrengths);
            pt.weaknessDict.TryGetValue(target.type.name, out List<string>? targetWeaknesses);

            targetStrengths ??= [];
            targetWeaknesses ??= [];

            double effectiveness = 1.0;

            if (targetStrengths.Contains(moveType.name) || target.type.name.Equals(moveType.name)) { effectiveness = 0.5; Console.WriteLine("It's not very effective"); }
            else if (targetWeaknesses.Contains(moveType.name)) { effectiveness = 2.0; Console.WriteLine("It's super effective!"); }
            
            return effectiveness;
        }
    }

    class TackleAttack : PhysicalAttack {

        public TackleAttack() : base(30, 80, new NormalType(), "Tackle") {}
    }

    class EmberAttack : SpecialAttack {
        
        public EmberAttack() : base(30, 95, new FireType(), "Ember") {}
    }

    class Flamethrower : SpecialAttack {

        public Flamethrower() : base(70, 80, new FireType(), "Flamethrower") {}
    }

    class BubbleAttack : SpecialAttack {
        public BubbleAttack() : base(30, 100, new WaterType(), "Bubble") {}
    }

    class VineWhipAttack : SpecialAttack {

        public VineWhipAttack() : base(30, 90, new GrassType(), "Vine Whip") {}
    }
}