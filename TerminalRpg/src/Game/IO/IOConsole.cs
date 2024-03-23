namespace TerminalRpg.Game.IO
{
    public class IOConsole : OutputConsole, IIOConsole {
        public string? ReadLine() {
            return Console.ReadLine();
        }
    }
}
