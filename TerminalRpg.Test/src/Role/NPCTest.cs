using Moq;
using TerminalRpg.Game.IO;
using TerminalRpg.Role;
using TerminalRpg.Role.Fighters;

namespace TerminalRpg.Test.Role
{
    public class NPCTest {
        [Test]
        public void TestInteractWithHero() {
            Mock<IOutputConsole> mock = new Mock<IOutputConsole>();

            NPC npc = new NPC(0, 0, mock.Object);
            npc.InteractWith(new Hero(100, 100, 100, 20));

            mock.Verify(
                o => o.WriteLine(string.Format(
                    "{0}: Bonjour, je suis un personnage non-joueur",
                    npc.Name
                )),
                Times.Once
            );
        }
    }
}
