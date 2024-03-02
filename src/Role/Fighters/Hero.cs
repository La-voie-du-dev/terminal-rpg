using Microsoft.Extensions.Logging;
using TerminalRpg.Game;
using TerminalRpg.Game.Input;
using TerminalRpg.Game.Input.Actions;
using TerminalRpg.Game.Logging;

namespace TerminalRpg.Role.Fighters
{
    public class Hero : Fighter
    {
        private static readonly ILogger log =
            LogFactory.GetLogger<Hero>();

        private const int LIFE_POTION_POINTS = 50;
        private const int MANA_POTION_POINTS = 40;

        private const int MAGIC_ATTACK_MANA_COST = 20;

        private int _mana;

        public int LifePotion { get; private set; } = 0;
        public int ManaPotion { get; private set; } = 0;

        /// <summary>Mana du héros.</summary>
        public int Mana {
            get => _mana;
            private set => _mana = IntInRange(value, STAT_MIN, STAT_MAX);
        }

        /// <summary>Indique si une attaque magique est possible</summary>
        public bool CanPerformMagicAttack {
            get => _mana >= MAGIC_ATTACK_MANA_COST;
        }

        public Hero(
            int health, int mana, int armor, int damage,
            int X = 0, int Y = 0
        ) : base(health, armor, damage, X, Y)
        {
            Mana = mana;
        }

        public override string GetDescription()
        {
            // Définition obligatoire de la méthode abstraite
            return "Je suis le héros de l'histoire, je vaincrai !";
        }

        public override void Attack(Fighter fighter)
        {
            // Affichage des infos pour suivre le scénario
            Console.WriteLine("{0}: Pour la justice !", Name);

            base.Attack(fighter);
        }

        public override List<MenuItem> GenerateHeroActions()
        {
            return new List<MenuItem> {
                new UseLifePotionMenuItem(this),
                new UseManaPotionMenuItem(this)
            };
        }

        /// <summary>
        /// Réalise une attaque magique sur un autre combattant.
        /// </summary>
        /// <param name="fighter">Le combattant frappé.</param>
        public void MagicAttack(Fighter fighter) {
            if (CanPerformMagicAttack) {
                // Affichage des infos pour suivre le scénario
                Console.WriteLine(
                    "{0}: Invocation de la lame céleste.", Name
                );

                _mana -= MAGIC_ATTACK_MANA_COST;
                fighter.SufferFromMagicalDamage(Damage);
            } else {
                throw new GameException(
                    "Mana insuffisant pour une attaque magique"
                );
            }
        }

        /// <summary>Dérobe l'inventaire du combattant mort.</summary>
        /// <param name="fighter">Le combattant à dépouiller.</param>
        public void RobInventoryOf(Fighter fighter) {
            // On vole l'arme et l'armure si elles sont plus
            //  performantes
            log.LogInformation("Le héros fouille le combattant");
            log.LogDebug(
                "Stat du héros dommage={Damage} armure={Armor}",
                Damage, Armor
            );
            log.LogDebug(
                "Stat de l'ennemi dommage={Damage} armure={Armor}",
                fighter.Damage, fighter.Armor
            );
            if (fighter.Damage > Damage) {
                log.LogInformation("Le héros remplace son arme");
                Damage = fighter.RobWeapon();
            }
            if (fighter.Armor > 0) {
                log.LogInformation("Le héros répare son armure");
                Armor += fighter.RobArmor();
            }
        }

        /// <summary>Permet de récupérer des potions de vie.</summary>
        /// <param name="count">Nombre de potions.</param>
        public void TakeLifePotion(int count) {
            LifePotion += count;
        }

        /// <summary>Permet de récupérer des potions de mana.</summary>
        /// <param name="count">Nombre de potions.</param>
        public void TakeManaPotion(int count) {
            ManaPotion += count;
        }

        /// <summary>Consomme une potion de vie.</summary>
        public void UseLifePotion() {
            if (LifePotion <= 0) {
                throw new GameException("Pas assez de potions de vie");
            } else {
                LifePotion--;

                Health = Math.Min(
                    Health + LIFE_POTION_POINTS, STAT_MAX
                );
            }
        }

        /// <summary>Consomme une potion de mana.</summary>
        public void UseManaPotion() {
            if (ManaPotion <= 0) {
                throw new GameException("Pas assez de potions de mana");
            } else {
                ManaPotion--;

                _mana = Math.Min(_mana + MANA_POTION_POINTS, STAT_MAX);
            }
        }

        /// <summary>Récupération des statistiques.</summary>
        /// <returns>Statistiques sous forme de string.</returns>
        public string GetStatistics() {
            return string.Format(
                "Vie: {1}/{0}  Mana: {2}/{0}  Armure: {3}  Dégâts: {4}",
                STAT_MAX,
                Health,
                Mana,
                Armor,
                Damage
            );
        }
    }
}
