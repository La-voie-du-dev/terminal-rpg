using TerminalRpg.Game.Input;
using TerminalRpg.Game.Input.Actions;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Role
{
    public class NPC : Humanoid, IInteractive {
        public NPC(int X = 0, int Y = 0): base(X, Y) { }

        public override string GetDescription()
        {
            return "Bonjour, je suis un personnage non-joueur";
        }

        public void InteractWith(Hero hero)
        {
            // Le PNJ se décrit lors de l'interaction
            Console.WriteLine("{0}: {1}", Name, GetDescription());
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
