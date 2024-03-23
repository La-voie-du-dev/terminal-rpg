using TerminalRpg.Game.IO;

namespace TerminalRpg.Game.Input
{
    public class Menu {
        /// <summary>Liste des éléments du menu.</summary>
        private readonly List<MenuItem> _menu = new List<MenuItem>();
        /// <summary>Implémentation d'une console Input/Output.</summary>
        private IIOConsole _console;

        public Menu(IIOConsole? console = null) {
            _console = console == null ? new IOConsole() : console;
        }

        /// <summary>Ajoute un nouvel élément au menu.</summary>
        /// <param name="item">L'élément du menu à ajouter.</param>
        public void AddMenuItem(MenuItem item) {
            if (!_menu.Contains(item)) {
                _menu.Add(item);
            }
        }

        /// <summary>Récupère la saisie valide de l'utilisateur.</summary>
        /// <param name="maxIndex">L'index "humain" max du menu.</param>
        /// <returns>L'index de la liste choisi.</returns>
        private int GetSafeIndex(int maxIndex) {
            int index;
            do {
                _console.Write(string.Format(
                    "Choisir une action (1-{0}) : ", maxIndex
                ));
                string? nextLine = _console.ReadLine();
                if (nextLine == null) {
                    throw new Exception("Lecture du choix en échec");
                }
                
                try {
                    index = int.Parse(nextLine);

                    // Vérifications sur l'index
                    if (index < 1 || index > maxIndex) {
                        // Index hors limite
                        throw new GameException("Nombre hors limite");
                    }
                } catch (Exception e) {
                    _console.WriteLine(string.Format(
                        "Saisie invalide : {0}", e.Message
                    ));
                    index = -1;
                }
            } while (index == -1);

            return index - 1; // Correction de l'index
        }

        /// <summary>Demande à l'utilisateur de saisir l'indice.</summary>
        /// <returns>L'élément choisi par l'utilisateur.</returns>
        public MenuItem SelectMenuItem() {
            // Affichage du menu
            int id = 0;
            foreach (MenuItem item in _menu) {
                id++;
                _console.WriteLine(string.Format(
                    "{0} - {1}", id, item.Title
                ));
            }

            return _menu[GetSafeIndex(id)];
        }
    }
}
