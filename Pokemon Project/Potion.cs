namespace PokemonProject {

    class Potion {

        public Potion() {}

        public static (string messageTop, string messageBottom) Use(int inventory, Pokemon user) {

            string messageTop;
            string messageBottom = "";
            string healNumber = "20";

            if (inventory > 0) {

                messageTop = $"You used a potion on {user.name}";

                if (user.health + 20 > user.originalHealth) {
                    
                    healNumber =  (user.originalHealth - user.health).ToString();
                }

                messageBottom = $"Healed {healNumber} health to {user.name}";
            }
            else {
                messageTop = "Unable to use potions. You have none";
            }

            return (messageTop, messageBottom);
        }
    }
}