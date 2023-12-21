namespace TerminalRpg.Game {
    public class Tile {
        // Constantes de classe
        public const char EMPTY = ' ';

        // Attributs de classe
        public static char Horizontal = '─';
        public static char Vertical = '│';
        public static char TopLeftCorner = '┌';
        public static char TopRightCorner = '┐';
        public static char BottomLeftCorner = '└';
        public static char BottomRightCorner = '┘';

        // Attributs d'instance
        public readonly int Lines;
        public readonly int Columns;

        public List<Node> Nodes;

        public Tile(int lines = 5, int columns = 5) {
            Lines = lines;
            Columns = columns;

            Nodes = new List<Node>();
        }

        /// <summary>
        /// Affiche une ligne horizontale avec les caractères de
        /// coin left et right.
        /// </summary>
        /// <param name="left">Le caractère coin gauche.</param>
        /// <param name="right">Le caractère coin droit.</param>
        public void DisplayHorizontalBorder(char left, char right) {
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
        public Node? FindNodeByCoordinate(int x, int y) {
            foreach (Node node in Nodes) {
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

            if (Nodes.Contains(node)) {
                throw new Exception("Noeud déjà ajouté");
            }

            if (FindNodeByCoordinate(node.X, node.Y) == null) {
                Nodes.Add(node);
            } else {
                throw new Exception(
                    "Un autre noeud occupe déjà cette place"
                );
            }
        }
    }
}
