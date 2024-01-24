using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input
{
    public abstract class MenuItem {
        /// <summary>Le libellé du menu.</summary>
        public string Title { get; private set; }

        public MenuItem(string title) {
            Title = title;
        }

        /// <summary>Execution de l'action du menu item.</summary>
        /// <param name="hero">Le héros du joueur.</param>
        public abstract void Execute(Hero hero);
    }
}
