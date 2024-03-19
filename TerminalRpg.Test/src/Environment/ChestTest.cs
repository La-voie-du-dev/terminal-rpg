using TerminalRpg.Environment;

namespace TerminalRpg.Test.Environment
{
    public class ChestTest {
        [Test]
        public void TestRepresentation() {
            Chest chest = new Chest(1, 2);

            Assert.That(chest.Representation, Is.EqualTo('#'));
        }
    }
}
