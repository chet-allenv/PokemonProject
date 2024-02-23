
namespace PokemonProject {

    // Parent class that is the basis for the attacks in the game
    class Attack { 

        // Variable declaration
        public int power; // The base power of the move. OUT OF 100
        public int accuracy; // The accuracy of the move, OUT OF 100
        public string name; // Name of the move. MUST BE 15 CHARACTERS OR LESS IN LENGTH
        public string moveType; // the type of the move. MUST BE A KEY DECLARED IN THE PokemonType CLASS

        public readonly PokemonType pt = new(); // Creates a new instance of the PokemonType class
        public readonly Random rng = new(); // Random instance that will be used to generate random numbers
        
        /// <summary>
        /// The constructor for the attack class
        /// </summary>
        /// <param name="power"> sets the power of the move </param>
        /// <param name="accuracy"> sets the accuracy of the move </param>
        /// <param name="name"> sets the name of the move </param>
        /// <param name="moveType"> the Type of the move </param>
        public Attack(int power, int accuracy, string name, string moveType) {
            
            // Sets variables
            this.power = power;
            this.accuracy = accuracy;
            this.name = name;
            this.moveType = moveType;

            // Initializes the type matchups
            pt.InitializeTypeMatchups();
        }
        
        // Generic method that is to be overridden by the two child classes of Attack. This uses the attack and calculates the
        // damage of the attack.
        public virtual string Use(Pokemon user, Pokemon target) { return "Attack used"; }

        // Generic method that is to be overridden by the two child classes of Attack. This gets the attack string, the message that
        // displays the attack being used
        public string GetAttackMessage(Pokemon p) { return $"{p.name} used {this.name}!"; }

        // Calculates the effectiveness of the move using the type of the target compared to the attack.
        public (double effectiveness, string message) CalculateTypeEffectiveness(Pokemon target) {

            // This is the message that says whether the attack was super effective or not effective. Will remain blank if it's neither
            string message = ""; 

            // Sets the List of the targets strengths and weaknesses
            pt.strengthDict.TryGetValue(target.type, out List<string>? targetStrengths);
            pt.weaknessDict.TryGetValue(target.type, out List<string>? targetWeaknesses);

            // Assigns the to be blank lists if null
            targetStrengths ??= [];
            targetWeaknesses ??= [];

            // Sets the effectiveness to be 1 by default
            double effectiveness = 1.0;

            // Checks if the target's strengths or weaknesses are the type of the attack. If it is in one, 
            // it sets the effectiveness to its respective value and sets the message to its respective
            // message
            if (targetStrengths.Contains(moveType)) { effectiveness = 0.5; message = "It's not very effective"; }
            else if (targetWeaknesses.Contains(moveType)) { effectiveness = 2.0; message = "It's super effective!"; }
            
            // Returns the effectiveness and the message
            return (effectiveness, message);
        }

    }

    /* PHYSICAL ATTACKS ARE ATTACKS THAT ARE OF THE FOLLOWING TYPES
    *  - Normal
    *  - Fighting
    */
    /// <summary>
    /// The parent class for physical attacks which are of a certain type and calculates damage using the user's attack stat and 
    /// the target's defence stat.
    /// </summary>
    class PhysicalAttack : Attack{

        // Constructor that calls the contructor from Attack
        public PhysicalAttack(int power, int accuracy, string type, string name) : base(power, accuracy, name, type) {}
        
        /// <summary>
        /// Method that uses the move. It calculates damage and subtracts it from the Target's health It then returns the 
        /// attack message that states who used the move and the name of the move.
        /// </summary>
        /// <param name="user"> User of the attack </param>
        /// <param name="target"> Target of the attack </param>
        /// <returns> attackMessage that states the user and the used move </returns>
        public override string Use(Pokemon user, Pokemon target) {
            
            // Used to test the accuracy. As long as it is LOWER than the accuracy of the move, the attack will land
            int testAccuracy = rng.Next(0, 101);

            // Checks the accuracy
            if (testAccuracy <= accuracy) {
                
                // STAB stands for Same Type Attack Boost. Essentially if used move is of the same type as the user, 
                // there is a slight damage increase.
                double STAB = moveType.Equals(user.type) ? 1.5 : 1.0;

                // Gets the effectiveness stat from the parent method CalculateTypeEffectiveness(). See it's documentation for explanation
                double typeEffectiveness = CalculateTypeEffectiveness(target).effectiveness;

                // Sets damage to equal the value returned by the CalculateDamage() method within this class. See it's documentation for explanation
                int damage = CalculateDamage(user, target, STAB, typeEffectiveness);

                // Subtracts damage from the target pokemon's health
                target.health -= damage;

            }

            // Returns the used string.
            return $"{user.name} used {this.name}!";
        }

        /// <summary>
        /// Calculates the damage of the attack. Formula is modified from the Generation 1 formula created by the Game Freak Corporation.
        /// Formula taken from https://bulbapedia.bulbagarden.net/wiki/Damage
        /// </summary>
        /// <param name="user"> User of the move </param>
        /// <param name="target"> Target of the move </param>
        /// <param name="STAB"> Same Type Attack Bonus of the User </param>
        /// <param name="typeEffectiveness"> Type effectiveness of the attack </param>
        /// <returns> the damage of the attack </returns>
        public int CalculateDamage(Pokemon user, Pokemon target, double STAB, double typeEffectiveness) {

            // Calculates a random number from 217 - 255. This is done in the original generation 1 Pokemon Games
            int randomNumber = rng.Next(217, 256);

            // This is the formula for the damage. It takes the user's level, user's attack stat, the attack's power, the target's defence stat
            // the STAB of the user, the type effectiveness of the attack, and the random number from 217 - 256
            double d = ((2 * user.level / 5 + 2) * user.attack * power / target.defence / 50 + 2) * STAB * typeEffectiveness * randomNumber / 100;
            int damage = (int) d; // Converts the double to an int

            return damage; // returns the int damage
        }

        
    }

    class SpecialAttack : Attack {

        public SpecialAttack(int power, int accuracy, string type, string name) : base(power, accuracy, name, type) {}

        public override string  Use(Pokemon user, Pokemon target) {
            
            int testAccuracy = rng.Next(0, 101);


            string attackMessage = $"{user.name} used {this.name}!";

            if (testAccuracy <= accuracy) {

                double STAB = moveType.Equals(user.type) ? 1.5 : 1.0;
                double typeEffectiveness = CalculateTypeEffectiveness(target).effectiveness;

                int damage = CalculateDamage(user, target, STAB, typeEffectiveness);

                target.health -= damage;
                

            }


            return attackMessage;
        }

        public int CalculateDamage(Pokemon user, Pokemon target, double STAB, double typeEffectiveness) {

            int randomNumber = rng.Next(217, 256);

            double d = ((2 * user.level / 5 + 2) * user.special * power / target.special / 50 + 2) * STAB * typeEffectiveness * randomNumber / 100;
            int damage = (int) d;

            return damage;
        }
    }

    class TackleAttack : PhysicalAttack {

        public TackleAttack() : base(30, 70, "Normal", "Tackle") {}
    }

    class EmberAttack : SpecialAttack {
        
        public EmberAttack() : base(30, 80, "Fire", "Ember") {}
    }

    class Flamethrower : SpecialAttack {

        public Flamethrower() : base(70, 80, "Fire", "Flamethrower") {}
    }

    class BubbleAttack : SpecialAttack {
        public BubbleAttack() : base(30, 80, "Water", "Bubble") {}
    }

    class VineWhipAttack : SpecialAttack {

        public VineWhipAttack() : base(30, 80, "Grass", "Vine Whip") {}
    }
}