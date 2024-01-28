using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input.Actions
{
    public class UseLifePotionMenuItem : MenuItem
    {
        public UseLifePotionMenuItem(Hero hero) : base(string.Format(
            "Utiliser une potion de vie ({0})", hero.LifePotion
        )) { }

        public override void Execute(Hero hero)
        {
            hero.UseLifePotion();
        }
    }
}
