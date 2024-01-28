using TerminalRpg.Environment;
using TerminalRpg.Game.Input;
using TerminalRpg.Game.Input.Actions;
using TerminalRpg.Role;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game.Engine
{
    public class TileManager {
        /// <summary>Référence au héros de l'utilisateur.</summary>
        private readonly Hero _hero;

        public TileManager(Hero hero) {
            _hero = hero;
        }

        /// <summary>Construit et gère le menu de la tuile.</summary>
        /// <param name="tile">La tuile sur laquelle jouer.</param>
        /// <returns>Le menu sélectionné par l'utilisateur.</returns>
        private MenuItem WaitUserInteraction(Tile tile) {
            // Construction du menu
            Menu menu = new Menu();
            foreach (Node node in tile.Nodes) {
                if (node is Chest) {
                    menu.AddMenuItem(new IInteractiveMenuItem(
                        "Ouvrir le coffre", (IInteractive) node
                    ));
                } else if (node is NPC) {
                    menu.AddMenuItem(new IInteractiveMenuItem(
                        "Discuter avec le personnage non-joueur",
                        (IInteractive) node
                    ));
                } else if (node is Enemy) {
                    Enemy enemy = (Enemy) node;
                    menu.AddMenuItem(new PhysicalAttackMenuItem(enemy));
                    menu.AddMenuItem(new MagicAttackMenuItem(enemy));
                    menu.AddMenuItem(new RobMenuItem(enemy));
                } else if (node is Hero) {
                    Hero hero = (Hero) node;
                    menu.AddMenuItem(new UseLifePotionMenuItem(hero));
                    menu.AddMenuItem(new UseManaPotionMenuItem(hero));
                }
            }

            return menu.SelectMenuItem();
        }

        /// <summary>Exécute les actions des ennemis.</summary>
        /// <param name="tile">La tuile sur laquelle jouer.</param>
        /// <returns>Le nombre d'ennemis encore vivants.</returns>
        public int ApplyEnemyBehavior(Tile tile) {
            int aliveCount = 0;
            foreach (Node node in tile.Nodes) {
                if (node is Enemy) {
                    Enemy enemy = (Enemy) node;

                    if (!enemy.Dead) {
                        aliveCount++;
                        enemy.Attack(_hero);
                    }
                }
            }

            return aliveCount;
        }

        /// <summary>Gère le jeu sur la tuile en paramètre.</summary>
        /// <param name="tile">La tuile sur laquelle jouer.</param>
        public void Play(Tile tile) {
            int enemyAlive = 1;
            while (enemyAlive > 0) {
                // Affichage de l'état courant
                tile.Display();

                try {
                    WaitUserInteraction(tile).Execute(_hero);

                    enemyAlive = ApplyEnemyBehavior(tile);

                    if (_hero.Dead) {
                        throw new GameOverException();
                    }
                } catch (GameException e) {
                    Console.WriteLine(
                        "Action impossible : {0}",
                        e.Message
                    );
                }
            }
        }
    }
}
