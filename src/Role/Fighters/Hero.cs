using TerminalRpg.Game;

namespace TerminalRpg.Role.Fighters
{
    public class Hero : Fighter
    {
        private const int LIFE_POTION_POINTS = 50;
        private const int MANA_POTION_POINTS = 40;

        private const int MAGIC_ATTACK_MANA_COST = 20;

        private int _mana;
        private int _lifePotion = 0;
        private int _manaPotion = 0;

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
            if (fighter.Health > 0) {
                throw new GameException(
                    "Impossible de voler un combattant vivant"
                );
            } else {
                // On vole l'arme et l'armure si elles sont plus
                //  performantes
                if (fighter.Damage > Damage) {
                    Damage = fighter.Damage;
                }
                if (fighter.Armor > 0) {
                    Armor += fighter.Armor;
                }
            }
        }

        /// <summary>Permet de récupérer des potions de vie.</summary>
        /// <param name="count">Nombre de potions.</param>
        public void TakeLifePotion(int count) {
            _lifePotion += count;
        }

        /// <summary>Permet de récupérer des potions de mana.</summary>
        /// <param name="count">Nombre de potions.</param>
        public void TakeManaPotion(int count) {
            _manaPotion += count;
        }

        /// <summary>Consomme une potion de vie.</summary>
        public void UseLifePotion() {
            if (_lifePotion <= 0) {
                throw new GameException("Pas assez de potions de vie");
            } else {
                _lifePotion--;

                Health = Math.Min(
                    Health + LIFE_POTION_POINTS, STAT_MAX
                );
            }
        }

        /// <summary>Consomme une potion de mana.</summary>
        public void UseManaPotion() {
            if (_manaPotion <= 0) {
                throw new GameException("Pas assez de potions de mana");
            } else {
                _manaPotion--;

                _mana = Math.Min(_mana + MANA_POTION_POINTS, STAT_MAX);
            }
        }
    }
}
