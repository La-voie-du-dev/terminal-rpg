using TerminalRpg.Game;
using TerminalRpg.Game.Input;
using TerminalRpg.Game.Input.Actions;
using TerminalRpg.Role;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Environment
{
    public class Chest : Node, IInteractive
    {
        // Nombre de potions de chaque type
        private int _lifePotions;
        private int _manaPotions;

        public Chest(
            int lifePotions, int manaPotions, int X = 0, int Y = 0
        ) : base('#', X, Y) {
            _lifePotions = lifePotions;
            _manaPotions = manaPotions;
        }

        public void InteractWith(Hero hero)
        {
            if (_lifePotions == 0 && _manaPotions == 0) {
                // Indiquer que le coffre est vide
                Console.WriteLine(
                    "{0}: Pas de chance, le coffre est vide", Name
                );
            } else {
                // Le héros récupère les potions du coffre
                //  On signale les objets récupérés
                if (_lifePotions > 0) {
                    Console.WriteLine(
                        "{0}: Récupération de {1} potion(s) de vie",
                        Name, _lifePotions
                    );
                    hero.TakeLifePotion(_lifePotions);
                    _lifePotions = 0;
                }
                if (_manaPotions > 0) {
                    Console.WriteLine(
                        "{0}: Récupération de {1} potion(s) de mana",
                        Name, _manaPotions
                    );
                    hero.TakeManaPotion(_manaPotions);
                    _manaPotions = 0;
                }
            }
        }

        public override List<MenuItem> GenerateHeroActions()
        {
            return new List<MenuItem> {
                new IInteractiveMenuItem("Ouvrir le coffre", this)
            };
        }
    }
}
