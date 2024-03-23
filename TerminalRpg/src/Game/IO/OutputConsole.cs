namespace TerminalRpg.Game.IO
{
    public class OutputConsole: IOutputConsole {
        public void WriteLine(string message) {
            Console.WriteLine(message);
        }

        public void Write(string message) {
            Console.Write(message);
        }
    }
}
