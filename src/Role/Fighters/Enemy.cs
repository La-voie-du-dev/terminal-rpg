namespace TerminalRpg.Role.Fighters
{
    public class Enemy : Fighter
    {
        public Enemy(
            int health, int armor, int damage,
            int X = 0, int Y = 0
        ) : base(health, armor, damage, X, Y) { }

        public override string GetDescription()
        {
            return "Je suis un barbare assoiffé de sang !";
        }

        public override void Attack(Fighter fighter)
        {
            // Affichage des infos pour suivre le scénario
            Console.WriteLine("{0}: Goûte à ma lame !", Name);

            base.Attack(fighter);
        }
    }
}
