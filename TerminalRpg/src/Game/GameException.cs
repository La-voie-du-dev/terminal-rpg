namespace TerminalRpg.Game
{
    public class GameException : Exception {
        public GameException(string? message): base(message) { }
    }
}
