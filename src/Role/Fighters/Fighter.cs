using TerminalRpg.Game;

namespace TerminalRpg.Role.Fighters
{
    public abstract class Fighter : Humanoid
    {
        /// <summary>Caractère représentant un combattant mort.</summary>
        private const char DEAD = '†';

        protected const int STAT_MIN = 0;
        protected const int STAT_MAX = 100;

        private int _health;
        private int _armor;
        private int _damage;

        /// <summary>Vie du combattant.</summary>
        public int Health {
            get => _health;
            protected set => _health = IntInRange(
                value, STAT_MIN, STAT_MAX
            );
        }

        /// <summary>Armure du combattant.</summary>
        public int Armor {
            get => _armor;
            protected set => _armor = IntOver(value, STAT_MIN);
        }

        /// <summary>Dégâts de l'arme du combattant.</summary>
        public int Damage {
            get => _damage;
            protected set => _damage = IntOver(value, STAT_MIN);
        }

        public Fighter(
            int health, int armor, int damage, int X = 0, int Y = 0
        ): base(X, Y) {
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        /// <summary>
        /// Vérifie les stats de vie et mets à jour la représentation
        /// en conséquence.
        /// </summary>
        private void CheckDeath() {
            if (_health <= 0) {
                // Le personnage est mort :
                //  Modification de l'état d'affichage dans la tuile
                Representation = DEAD;
            }
        }

        /// <summary>
        /// Applique les dégâts liés à une attaque physique.
        /// </summary>
        /// <param name="damage">Les dégâts à appliquer.</param>
        public void SufferFromPhysicalDamage(int damage) {
            // Calcul d'absorption des dégâts (moitié des dégâts)
            int blockedDamage = damage / 2;

            // Contrôle de l'armure restante pour le blocage des dégâts
            blockedDamage = Math.Clamp(blockedDamage, 0, _armor);

            // Mise à jour des statistiques
            _armor -= blockedDamage;
            _health -= Math.Clamp(damage - blockedDamage, 0, _health);

            CheckDeath();
        }

        /// <summary>
        /// Applique les dégâts magiques liés à une attaque physique.
        /// </summary>
        /// <param name="damage">Les dégâts magiques à appliquer.</param>
        public void SufferFromMagicalDamage(int damage) {
            // Pas d'absorption de dégâts
            _health -= Math.Clamp(damage, 0, _health);

            CheckDeath();
        }

        /// <summary>
        /// Réalise une attaque physique sur un autre combattant.
        /// </summary>
        /// <param name="fighter">Le combattant frappé.</param>
        public virtual void Attack(Fighter fighter) {
            fighter.SufferFromPhysicalDamage(_damage);
        }

        /// <summary>
        /// Valider la valeur dans l'interval min et max inclus.
        /// </summary>
        /// <param name="value">La valeur à tester et retourner.</param>
        /// <param name="min">La valeur min de l'interval.</param>
        /// <param name="max">La valeur max de l'interval.</param>
        /// <returns>value lorsqu'elle est valide.</returns>
        protected static int IntInRange(int value, int min, int max) {
            if (value < min || value > max) {
                throw new GameException(
                    string.Format(
                        "Valeur {0} hors de l'interval [{1}; {2}]",
                        value, min, max
                    )
                );
            }

            return value;
        }

        /// <summary>
        /// Valide la valeur qui doit être supérieure ou égale à min.
        /// </summary>
        /// <param name="value">La valeur à tester et retourner.</param>
        /// <param name="min">La valeur min autorisée.</param>
        /// <returns>value lorsqu'elle est valide.</returns>
        protected static int IntOver(int value, int min) {
            if (value < min) {
                throw new GameException(
                    string.Format(
                        "Valeur {0} inférieure à {1}", value, min
                    )
                );
            }

            return value;
        }
    }
}
