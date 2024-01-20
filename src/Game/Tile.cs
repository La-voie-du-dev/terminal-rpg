using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Game {
    public class Tile {
        // Constantes de classe
        private const char EMPTY = ' ';

        // Attributs de classe
        public static char Horizontal { get; set; } = '─';
        public static char Vertical { get; set; } = '│';
        public static char TopLeftCorner { get; set; } = '┌';
        public static char TopRightCorner { get; set; } = '┐';
        public static char BottomLeftCorner { get; set; } = '└';
        public static char BottomRightCorner { get; set; } = '┘';

        // Attributs d'instance
        public int Lines { get; }
        public int Columns { get; }

        private readonly List<Node> _nodes;

        public Tile(int lines = 5, int columns = 5) {
            Lines = lines;
            Columns = columns;

            _nodes = new List<Node>();
        }

        /// <summary>
        /// Affiche une ligne horizontale avec les caractères de
        /// coin left et right.
        /// </summary>
        /// <param name="left">Le caractère coin gauche.</param>
        /// <param name="right">Le caractère coin droit.</param>
        private void DisplayHorizontalBorder(char left, char right) {
            // Affichage du coin gauche
            Console.Write(left);

            // Affichage des caractères horizontaux
            for (int index = 0; index < Columns; index++) {
                Console.Write("{0}{0}", Horizontal);
            }

            // Affichage du coin droit + retour à la ligne
            Console.WriteLine("{0}{1}", Horizontal, right);
        }

        /// <summary>
        /// Affiche la tuile dans le terminal en utilisant les
        /// préférences d'affichages.
        /// </summary>
        public void Display() {
            // Affichage des statistiques
            foreach (Node node in _nodes) {
                if (node is Hero) {
                    Hero hero = (Hero) node;

                    Console.WriteLine(
                        "{0}  {1}", hero.Name, hero.GetStatistics()
                    );
                }
            }

            // Affichage du premier séparateur
            DisplayHorizontalBorder(TopLeftCorner, TopRightCorner);

            // Affichage du contenu de la tuile
            for (int line = 0; line < Lines; line++) {
                // Affichage de la bordure verticale
                Console.Write(Vertical);

                // Affichage des noeuds de la ligne ou ' '
                for (int column = 0; column < Columns; column++) {
                    Node? node = FindNodeByCoordinate(column, line);
                    Console.Write(
                        " {0}",
                        node != null ? node.Representation : EMPTY
                    );
                }

                // Affichage de la bordure verticale
                //  + retour à la ligne
                Console.WriteLine(" {0}", Vertical);
            }

            // Affichage du dernier séparateur
            DisplayHorizontalBorder(BottomLeftCorner, BottomRightCorner);
        }

        /// <summary>
        /// Trouve un noeud aux coordonnées souhaitées ou
        /// retourne null.
        /// </summary>
        /// <param name="x">La colonne.</param>
        /// <param name="y">La ligne.</param>
        /// <returns>Le noeud ou null.</returns>
        private Node? FindNodeByCoordinate(int x, int y) {
            foreach (Node node in _nodes) {
                if (node.X == x && node.Y == y) {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Essai d'ajouter un nouveau noeud à la tuile.
        /// </summary>
        /// <param name="node">Le noeud à ajouter.</param>
        public void AddNode(Node node) {
            if (
                node.X < 0 || node.X >= Columns
                || node.Y < 0 || node.Y >= Lines
            ) {
                throw new Exception("Coordonnées invalide");
            }

            if (_nodes.Contains(node)) {
                throw new Exception("Noeud déjà ajouté");
            }

            if (FindNodeByCoordinate(node.X, node.Y) == null) {
                _nodes.Add(node);
            } else {
                throw new Exception(
                    "Un autre noeud occupe déjà cette place"
                );
            }
        }
    }
}
