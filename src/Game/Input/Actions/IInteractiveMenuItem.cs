using TerminalRpg.Role;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input.Actions
{
    public class IInteractiveMenuItem : MenuItem
    {
        /// <summary>Référence de l'instance interactive.</summary>
        private readonly IInteractive _interactive;

        public IInteractiveMenuItem(
            string title, IInteractive interactive
        ) : base(title) {
            _interactive = interactive;
        }

        public override void Execute(Hero hero)
        {
            _interactive.InteractWith(hero);
        }
    }
}
