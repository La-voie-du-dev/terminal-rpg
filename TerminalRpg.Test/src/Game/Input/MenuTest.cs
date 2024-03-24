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
    }
}
