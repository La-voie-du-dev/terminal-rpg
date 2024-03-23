namespace TerminalRpg.Game.IO
{
    public interface IOutputConsole {
        /// <summary>Produit un message en sortie.</summary>
        /// <param name="message">Le message de sortie.</param>
        public void WriteLine(string message);

        /// <summary>Produit un message en sortie.</summary>
        /// <param name="message">Le message de sortie.</param>
        public void Write(string message);
    }
}
