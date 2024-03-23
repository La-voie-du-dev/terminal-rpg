using TerminalRpg.Game.IO;
using TerminalRpg.Game.Input;
using TerminalRpg.Game.Input.Actions;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Role
{
    public class NPC : Humanoid, IInteractive {
        private IOutputConsole _console;

        public NPC(
            int X = 0, int Y = 0, IOutputConsole? console = null
        ): base(X, Y) {
            _console = console == null ? new OutputConsole() : console;
        }

        public override string GetDescription()
        {
            return "Bonjour, je suis un personnage non-joueur";
        }

        public void InteractWith(Hero hero)
        {
            // Le PNJ se décrit lors de l'interaction
            _console.WriteLine(
                string.Format("{0}: {1}", Name, GetDescription())
            );
        }

        public override List<MenuItem> GenerateHeroActions()
        {
            return new List<MenuItem> {
                new IInteractiveMenuItem(
                    "Discuter avec le personnage non-joueur",
                    this
                )
            };
        }
    }
}
