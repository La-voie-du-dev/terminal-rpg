namespace TerminalRpg.Game {
    public class Node {
        /// <summary>
        /// La colonne où le caractère doit être affiché.
        /// </summary>
        public int X = 0;

        /// <summary>
        /// La ligne où le caractère doit être affiché.
        /// </summary>
        public int Y = 0;

        /// <summary>
        /// Le caractère représentant l'entité.
        /// </summary>
        public char Representation;

        public Node(char representation, int X = 0, int Y = 0) {
            this.X = X;
            this.Y = Y;
            Representation = representation;
        }
    }
}
