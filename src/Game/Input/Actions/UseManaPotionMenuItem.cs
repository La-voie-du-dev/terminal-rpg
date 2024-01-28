using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Input.Actions
{
    public class UseManaPotionMenuItem : MenuItem
    {
        public UseManaPotionMenuItem(Hero hero) : base(string.Format(
            "Utiliser une potion de mana ({0})", hero.ManaPotion
        )) { }

        public override void Execute(Hero hero)
        {
            hero.UseManaPotion();
        }
    }
}
