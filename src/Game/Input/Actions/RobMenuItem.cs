using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input.Actions
{
    public class RobMenuItem : MenuItem
    {
        /// <summary>Référence à l'instance ennemie vaincue.</summary>
        private readonly Enemy _enemy;

        public RobMenuItem(Enemy enemy) : base(
            string.Format("Dérober l'inventaire de {0}", enemy.Name)
        ) {
            _enemy = enemy;
        }

        public override void Execute(Hero hero)
        {
            hero.RobInventoryOf(_enemy);
        }
    }
}
