using TerminalRpg.Game.Input;
using TerminalRpg.Game.Input.Actions;
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
        private MenuItem WaitUserInteraction(Tile tile, int aliveCount) {
            // Construction du menu
            Menu menu = new Menu();
            foreach (Node node in tile.Nodes) {
                foreach (MenuItem item in node.GenerateHeroActions()) {
                    menu.AddMenuItem(item);
                }
            }

            if (aliveCount <= 0) {
                // Ajout du menu pour quitter la partie
                menu.AddMenuItem(new EndGameMenuItem());
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
            bool playing = true;
            int enemyAlive = 1;
            while (playing) {
                // Affichage de l'état courant
                tile.Display();

                try {
                    WaitUserInteraction(tile, enemyAlive).Execute(_hero);

                    enemyAlive = ApplyEnemyBehavior(tile);

                    if (_hero.Dead) {
                        throw new GameOverException();
                    }
                } catch (EndGameException) {
                    // Arrêt de boucle sans Game Over
                    playing = false;
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
