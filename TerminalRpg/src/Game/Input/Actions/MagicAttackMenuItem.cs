using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input.Actions
{
    public class MagicAttackMenuItem : MenuItem
    {
        /// <summary>Référence à l'instance ennemie ciblée.</summary>
        private readonly Enemy _enemy;

        public MagicAttackMenuItem(Enemy enemy) : base(string.Format(
            "Attaque magique sur {0} ({1}/100)", enemy.Name, enemy.Health
        )) {
            _enemy = enemy;
        }

        public override void Execute(Hero hero)
        {
            hero.MagicAttack(_enemy);
        }
    }
}
