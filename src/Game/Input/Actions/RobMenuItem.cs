using Microsoft.Extensions.Logging;
using TerminalRpg.Game.Logging;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input.Actions
{
    public class RobMenuItem : MenuItem
    {
        private static readonly ILogger log =
            LogFactory.GetLogger<RobMenuItem>();

        /// <summary>Référence à l'instance ennemie vaincue.</summary>
        private readonly Enemy _enemy;

        public RobMenuItem(Enemy enemy) : base(
            string.Format("Dérober l'inventaire de {0}", enemy.Name)
        ) {
            _enemy = enemy;
        }

        public override void Execute(Hero hero)
        {
            log.LogInformation(
                "Le héros dérobe l'inventaire de {Name}", _enemy.Name
            );
            hero.RobInventoryOf(_enemy);
        }
    }
}
