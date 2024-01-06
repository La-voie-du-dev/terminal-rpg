namespace TerminalRpg.Game {
    public class Node {
        private int _x;
        /// <summary>
        /// La colonne où le caractère doit être affiché.
        /// </summary>
        public int X {
            get { return _x; }
            set {
                if (value < 0) {
                    throw new Exception(
                        "La valeur minimale acceptée est 0");
                }
                _x = value;
            }
        }

        private int _y;
        /// <summary>
        /// La ligne où le caractère doit être affiché.
        /// </summary>
        public int Y {
            get => _y;
            set {
                if (value < 0) {
                    throw new Exception(
                        "La valeur minimale acceptée est 0");
                }
                _y = value;
            }
        }

        /// <summary>
        /// Le caractère représentant l'entité.
        /// </summary>
        public char Representation { get; protected set; }

        /// <summary>
        /// Nom de l'instance
        /// </summary>
        /// <returns>Le nom généré de l'instance</returns>
        public string Name {
            get => string.Format("{0}[{1}]", this, GetHashCode());
        }

        public Node(char representation, int X = 0, int Y = 0) {
            this.X = X;
            this.Y = Y;
            Representation = representation;
        }
    }
}
