using TerminalRpg.Environment;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Test.Environment
{
    public class ChestTest {
        private Hero hero;

        [SetUp]
        public void CreateHero() {
            hero = new Hero(100, 100, 100, 20);
        }

        [Test]
        public void TestRepresentation() {
            Chest chest = new Chest(1, 2);

            Assert.That(chest.Representation, Is.EqualTo('#'));
        }

        [Test]
        public void TestInteractWithWhenEmpty() {
            Chest chest = new Chest(0, 0);
            chest.InteractWith(hero);

            Assert.That(hero.LifePotion, Is.EqualTo(0));
            Assert.That(hero.ManaPotion, Is.EqualTo(0));
        }

        [Test]
        public void TestInteractWithOnlyLifePotions() {
            Chest chest = new Chest(1, 0);
            chest.InteractWith(hero);

            Assert.That(hero.LifePotion, Is.EqualTo(1));
            Assert.That(hero.ManaPotion, Is.EqualTo(0));
        }

        [Test]
        public void TestInteractWithOnlyManaPotions() {
            Chest chest = new Chest(0, 2);
            chest.InteractWith(hero);

            Assert.That(hero.LifePotion, Is.EqualTo(0));
            Assert.That(hero.ManaPotion, Is.EqualTo(2));
        }
    }
}
