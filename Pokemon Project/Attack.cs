
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace PokemonProject {

    class Attack { 

        public int power;
        public int accuracy; 

        public string name;
        public string moveType;

        public readonly PokemonType pt = new();
        
        public Attack(int power, int accuracy, string name, string moveType) {
            this.power = power;
            this.accuracy = accuracy;
            this.name = name;
            this.moveType = moveType;

            pt.InitializeTypeMatchups();
        }
        
        public virtual void Use(Pokemon user, Pokemon target) { Console.WriteLine("Attack used"); }
        public virtual void Use(Pokemon user) { Console.WriteLine("Attack Used"); }

        public double CalculateTypeEffectiveness(Pokemon target) {

            pt.strengthDict.TryGetValue(target.type, out List<string>? targetStrengths);
            pt.weaknessDict.TryGetValue(target.type, out List<string>? targetWeaknesses);

            targetStrengths ??= [];
            targetWeaknesses ??= [];

            double effectiveness = 1.0;

            if (targetStrengths.Contains(moveType) || target.type.Equals(moveType)) { effectiveness = 0.5; Console.WriteLine("It's not very effective"); }
            else if (targetWeaknesses.Contains(moveType)) { effectiveness = 2.0; Console.WriteLine("It's super effective!"); }
            
            return effectiveness;
        }

    }

    class PhysicalAttack : Attack{

        public string attackType = "physical";

        public PhysicalAttack(int power, int accuracy, string type, string name) : base(power, accuracy, name, type) {}
        public readonly Random rng = new();

        
        public override void Use(Pokemon user, Pokemon target) {
            
            int testAccuracy = rng.Next(0, 101);

            Console.WriteLine($"{user.name} used {this.name}!");

            if (testAccuracy <= accuracy) {

                double STAB = moveType.Equals(user.type) ? 1.5 : 1.0;
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

        
    }

    class SpecialAttack : Attack {

        public string attackType = "special";

        public readonly Random rng = new();

        public SpecialAttack(int power, int accuracy, string type, string name) : base(power, accuracy, name, type) {}

        public override void Use(Pokemon user, Pokemon target) {
            
            int testAccuracy = rng.Next(0, 101);

            Console.WriteLine($"{user.name} used {this.name}!");

            if (testAccuracy <= accuracy) {

                double STAB = moveType.Equals(user.type) ? 1.5 : 1.0;
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
    }

    class TackleAttack : PhysicalAttack {

        public TackleAttack() : base(30, 80, "Normal", "Tackle") {}
    }

    class EmberAttack : SpecialAttack {
        
        public EmberAttack() : base(30, 95, "Fire", "Ember") {}
    }

    class Flamethrower : SpecialAttack {

        public Flamethrower() : base(70, 80, "Fire", "Flamethrower") {}
    }

    class BubbleAttack : SpecialAttack {
        public BubbleAttack() : base(30, 100, "Water", "Bubble") {}
    }

    class VineWhipAttack : SpecialAttack {

        public VineWhipAttack() : base(30, 90, "Grass", "Vine Whip") {}
    }

    class HealMove : PhysicalAttack {

        public HealMove() : base(25, 100, "Normal", "Heal") {}

        public override void Use(Pokemon user, Pokemon user2)
        {
            Console.WriteLine($"{user.name} healed themself.");

            int damage = this.CalculateDamage(user);

            if (user.health + damage > user.originalHealth) { user.health = user.originalHealth; }
            else { user.health += damage; }
        }

        public int CalculateDamage(Pokemon user) {

            int randomNumber = rng.Next(217, 256);

            double d = ((2 * user.level / 5 + 2) * user.special * power / user.special / 50 + 2) * randomNumber / 100;
            int damage = (int) d;

            return damage;
        }
    }
}