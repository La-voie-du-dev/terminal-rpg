using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input.Actions
{
    public class PhysicalAttackMenuItem : MenuItem
    {
        /// <summary>Référence à l'instance ennemie ciblée.</summary>
        private readonly Enemy _enemy;

        public PhysicalAttackMenuItem(Enemy enemy) : base(string.Format(
            "Attaque physique sur {0} ({1}/100)", enemy.Name, enemy.Health
        )) {
            _enemy = enemy;
        }

        public override void Execute(Hero hero)
        {
            hero.Attack(_enemy);
        }
    }
}
