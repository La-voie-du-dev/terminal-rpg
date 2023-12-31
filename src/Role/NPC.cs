namespace TerminalRpg.Role
{
    public class NPC : Humanoid {
        public NPC(int X = 0, int Y = 0): base(X, Y) { }

        public override string GetDescription()
        {
            return "Bonjour, je suis un personnage non-joueur";
        }
    }
}
