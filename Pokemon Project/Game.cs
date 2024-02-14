using System.Security.Cryptography;

namespace PokemonProject {

    class Game {

        public void RunGame(Pokemon p1, Pokemon p2) {

            int turnNum = 0;
            DisplayHealth(p1,p2);

            while (p1.IsAlive() && p2.IsAlive()) {

                turnNum++;

                Pokemon first;
                Pokemon second;

                if (p1.speed >= p2.speed) { first = p1; second = p2; }
                else { first = p2; second = p1; }

                first.Attack(second);
                DisplayHealth(p1,p2);
                if (!second.IsAlive()) { Console.WriteLine($"{second.name} fainted. {first.name} wins the battle"); break;}

                second.Attack(first);
                DisplayHealth(p1,p2);
                if (!first.IsAlive()) { Console.WriteLine($"{first.name} fainted. {second.name} wins the battle"); }
            }
        }

        public void DisplayHealth(Pokemon p1, Pokemon p2) {

            Console.WriteLine($"\n{p1.name}: {GetHealthBar(p1)}\n{p2.name}: {GetHealthBar(p2)}\n");
        }

        public string GetHealthBar(Pokemon p) {

            if (p.health > 0) {
                double healthPercent = (double)p.health / p.originalHealth * 100;

                int numO = (int)Math.Round(healthPercent / 10);
                int numX = 10 - numO;

                return new string('O', numO) + new string('X', numX);
            }
            else { return "XXXXXXXXXX"; }
        }

        public static void Main(string[] args) {

            Game g = new(); Random r = new();
            int p1Num = r.Next(0,3); int p2Num = r.Next(0,3);
            Pokemon p1; Pokemon p2;

            if (p1Num == 0) { p1 = new Charmander(); }
            else if (p1Num == 1) { p1 = new Squirtle(); }
            else { p1 = new Bulbasaur(); }

            if (p2Num == 0) { p2 = new Charmander(); }
            else if (p2Num == 1) { p2 = new Squirtle(); }
            else { p2 = new Bulbasaur(); }

            g.RunGame(p1,p2);
        }
    }
}