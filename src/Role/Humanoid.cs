using TerminalRpg.Game;

namespace TerminalRpg.Role {
    public class Humanoid : Node
    {
        public Humanoid(int X = 0, int Y = 0) : base('o', X, Y) { }

        /// <summary>
        /// Retourne la description de l'entité humanoïde.
        /// </summary>
        /// <returns>La description préfixée du nom.</returns>
        public virtual string GetDescription() {
            return "TODO";
        } 
    }
}
