using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Role {
    public interface IInteractive {
        /// <summary>Interagit avec le héros.</summary>
        /// <param name="hero">L'instance du héros concerné.</param>
        public void InteractWith(Hero hero);
    }
}
