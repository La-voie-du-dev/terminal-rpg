using Moq;
using TerminalRpg.Environment;
using TerminalRpg.Game.Input;
using TerminalRpg.Game.IO;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Test.Game.Input
{
    public class MenuTest {
        private Mock<IIOConsole> mock;
        private List<MenuItem> items;
        private Menu menu;

        [SetUp]
        public void SetupMenu() {
            mock = new Mock<IIOConsole>();

            // Création du menu
            items = new List<MenuItem>();
            items.AddRange(
                new Hero(100, 100, 100, 20).GenerateHeroActions()
            );
            items.AddRange(new Chest(1, 1).GenerateHeroActions());

            menu = new Menu(mock.Object);
            foreach (MenuItem item in items) {
                menu.AddMenuItem(item);
            }
        }

        [Test]
        public void TestValidMenuSelectionInput() {
            // Exécution du menu : tester tous les choix valides
            for (int index = 0; index < items.Count; index++) {
                // Bouchons retournant l'index humain (index + 1)
                mock.Setup(o => o.ReadLine())
                    .Returns((index + 1).ToString());
                
                Assert.That(
                    menu.SelectMenuItem(),
                    Is.SameAs(items[index]),
                    "ReadLine() => \"{0}\"",
                    index + 1
                );
            }
        }

        [Test]
        public void TestValidMenuSelectionOutput() {
            // Simulation d'une saisie valide
            mock.Setup(o => o.ReadLine()).Returns("1");
            menu.SelectMenuItem();

            // Chaque élément est affiché avec l'index humain (1 fois)
            for (int index = 0; index < items.Count; index++) {
                mock.Verify(
                    o => o.WriteLine(string.Format(
                        "{0} - {1}", index + 1, items[index].Title
                    )),
                    Times.Once
                );
            }
            mock.Verify(
                o => o.Write(string.Format(
                    "Choisir une action (1-{0}) : ", items.Count
                )),
                Times.Once
            );
        }

        /// <summary>
        /// Ce test valide le comportement du programme en cas d'erreur
        /// sur le flux d'entrée.
        /// Il permet notamment de simuler le cas d'une fermeture
        /// prématurée de ce flux.
        /// </summary>
        [Test]
        public void TestMenuSelectionReadLineFailed() {
            // Nous devons spécifier le type des templates de la méthode
            //  `Returns`, car C# identifie une utilisation ambiguë entre
            //  2 méthodes lié à l'utilisation du paramètre `null`
            //  (CS0121).
            //  Nous ajoutons donc <IIOConsole, string> pour indiquer que
            //  `null` doit être traité comme un type `string`.
            mock.Setup(o => o.ReadLine())
                .Returns<IIOConsole, string>(null);

            // Nous ne pouvons pas utiliser `Assert.That` car la méthode
            //  `Menu.SelectMenuItem` lève une exception de type
            //  `Exception`.
            //  La solution est d'utiliser la méthode `Assert.Throws`,
            //  qui s'attend à ce que l'expression lambda lève une
            //  exception lors de son exécution.
            Assert.Throws<Exception>(() => menu.SelectMenuItem());
        }

        /// <summary>
        /// Ce test vérifie le comportement de la classe en cas de
        /// saisie numérique en dehors des choix possibles.
        /// Ici, nous validons le cas d'un choix inférieur aux limites.
        /// </summary>
        [Test]
        public void TestMenuSelectionLowerChoice() {
            // Nous utilisons la méthode `SetupSequence` plutôt que
            //  `Setup` pour pouvoir gérer le cas de la boucle infinie.
            //  Ici, `mock` est configuré pour retourner "0" lors de
            //  premier appel à `ReadLine`, puis "1" pour le second.
            //  Pour information, les appels suivants retournent `null`,
            //  cependant "1" permet d'arrêter la boucle. `null` n'est
            //  donc jamais retourné.
            mock.SetupSequence(o => o.ReadLine())
                .Returns("0")
                .Returns("1");
            menu.SelectMenuItem();

            mock.Verify(
                o => o.WriteLine("Saisie invalide : Nombre hors limite"),
                Times.Once
            );
        }

        /// <summary>
        /// Même vérification que le test précédent mais pour un choix
        /// supérieur aux limites.
        /// </summary>
        [Test]
        public void TestMenuSelectionUpperChoice() {
            // Ici, `SetupSequence` retourne le premier entier hors
            //  limite, puis "1" pour arrêter la boucle.
            mock.SetupSequence(o => o.ReadLine())
                .Returns((items.Count + 1).ToString())
                .Returns("1");
            menu.SelectMenuItem();

            mock.Verify(
                o => o.WriteLine("Saisie invalide : Nombre hors limite"),
                Times.Once
            );
        }


        /// <summary>
        /// Ce test vérifie le comportement de la classe en cas de
        /// saisie non numérique.
        /// </summary>
        [Test]
        public void TestMenuSelectionNotNumber() {
            // En configurant la valeur "a", nous gérons le cas d'erreur
            //  de la méthode `int.Parse`. Cette méthode lève une
            //  `FormatException` car "a" n'est pas convertible en entier.
            //  De nouveau, "1" permet d'arrêter la boucle.
            mock.SetupSequence(o => o.ReadLine())
                .Returns("a")
                .Returns("1");
            menu.SelectMenuItem();

            // Nous configurons la vérification via `It.Is<string>()`
            //  afin de filtrer précisément l'appel à la méthode
            //  `WriteLine` du `mock`.
            //
            //  Dans ce test, la méthode `WriteLine` est appelée plusieurs
            //  fois pour afficher le menu et le message d'erreur.
            //  L'expression lambda `(msg) => ...` permet de filtrer les
            //  appels à la méthode `WriteLine` lorsque le message en
            //  paramètre commence avec la chaîne "Saisie invalide : ".
            //
            //  Le test induit normalement la production d'un unique
            //  message d'erreur de saisie. D'où la vérification avec
            //  `Times.Once`.
            mock.Verify(
                o => o.WriteLine(It.Is<string>(
                    (msg) => msg.StartsWith("Saisie invalide : ")
                )),
                Times.Once
            );
        }
    }
}
