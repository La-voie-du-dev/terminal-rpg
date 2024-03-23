using Moq;
using TerminalRpg.Environment;
using TerminalRpg.Game.Input;
using TerminalRpg.Game.IO;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Test.Game.Input
{
    public class MenuTest {
        [Test]
        public void TestValidMenuSelection() {
            Mock<IIOConsole> mock = new Mock<IIOConsole>();

            // Création du menu
            List<MenuItem> items = new List<MenuItem>();
            items.AddRange(
                new Hero(100, 100, 100, 20).GenerateHeroActions()
            );
            items.AddRange(new Chest(1, 1).GenerateHeroActions());

            Menu menu = new Menu(mock.Object);
            foreach (MenuItem item in items) {
                menu.AddMenuItem(item);
            }

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
    }
}
