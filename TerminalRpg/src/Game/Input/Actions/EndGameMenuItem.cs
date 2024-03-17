using TerminalRpg.Game.Engine;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input.Actions
{
    public class EndGameMenuItem : MenuItem
    {
        public EndGameMenuItem() : base(
            "Fin de la partie"
        ) { }

        public override void Execute(Hero hero)
        {
            throw new EndGameException();
        }
    }
}
