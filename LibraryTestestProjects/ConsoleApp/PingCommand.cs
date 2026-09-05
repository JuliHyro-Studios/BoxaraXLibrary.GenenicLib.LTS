using System;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace ConsoleApp
{
    public sealed class PingCommand : ICommand
    {
        public string DisplayName => "Ping";

        public string[] Parameter => Array.Empty<string>();

        public string Name => "ping";

        public string[] Aliases => new[] { "p" };

        public string Category => "Testing";

        public string Shell => "TestShell";

        public string Description => "Displays a pong response with the current date and time.";

        public string CommandVersion => "1.0.0";

        public void Execute()
        {
            DateTime now = DateTime.Now;

            LogManager.Log("[PING] Pong!");
            LogManager.Log($"[PING] Current time: {now:yyyy-MM-dd HH:mm:ss}");
        }

        public void ParameterExecute(string[] args)
        {
            Execute();
        }
    }
}
