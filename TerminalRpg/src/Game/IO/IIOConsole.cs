namespace TerminalRpg.Game.IO
{
    public interface IIOConsole : IOutputConsole {
        /// <summary>
        /// String en entrée, ou null en cas d'erreur ou fin de flux.
        /// </summary>
        /// <returns>Prochaine ligne, ou null</returns>
        public string? ReadLine();
    }
}
