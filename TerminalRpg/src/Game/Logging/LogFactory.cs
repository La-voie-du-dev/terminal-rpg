using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace TerminalRpg.Game.Logging
{
    public class LogFactory {
        private static ILoggerFactory ?Factory { set; get; } = null;

        /// <summary>Installe le système de journalisation.</summary>
        public static void Install() {
            Logger logger;
            try {
                // Création de la configuration du logger
                IConfigurationRoot configuration = 
                    new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("log-settings.json")
                        .Build();

                // Création de l'instance en appliquant la configuration
                logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            } catch (FileNotFoundException) {
                // Le fichier de configuration n'existe pas
                //  -> Initialisation du logger sans spécifications
                logger = new LoggerConfiguration()
                    .CreateLogger();
            }

            // Intégration du Logger Serilog au LoggerFactory
            Factory = new LoggerFactory().AddSerilog(logger);
        }

        /// <summary>Crée un logger spécifique à un type.</summary>
        /// <typeparam name="T">
        /// Le type générique associé au Logger.
        /// </typeparam>
        /// <returns>L'instance du Logger.</returns>
        public static Microsoft.Extensions.Logging.ILogger GetLogger<T>()
        {
            return Factory!.CreateLogger<T>();
        }
    }
}
