using System;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace ConsoleApp
{
    public sealed class DuplicatePingCommandA : ICommand
    {
        public string DisplayName => "Ping A";
        public string[] Parameter => new[] { "message" };
        public string Name => "ping";
        public string[] Aliases => new[] { "pa" };
        public string Category => "Testing";
        public string Shell => "TestShell";
        public string Description => "Duplicate Command A";
        public string CommandVersion => "1.0.0";

        public void Execute()
        {
            LogManager.Log("[COMMAND A] Executed without parameters!");
        }

        public void ParameterExecute(string[] args)
        {
            LogManager.Log($"[COMMAND A] Executed with parameters: {string.Join(" ", args)}");
        }
    }

    public sealed class DuplicatePingCommandB : ICommand
    {
        public string DisplayName => "Ping B";
        public string[] Parameter => new[] { "message" };
        public string Name => "ping";
        public string[] Aliases => new[] { "pb" };
        public string Category => "Testing";
        public string Shell => "TestShell";
        public string Description => "Duplicate Command B";
        public string CommandVersion => "1.0.0";

        public void Execute()
        {
            LogManager.Log("[COMMAND B] Executed without parameters!");
        }

        public void ParameterExecute(string[] args)
        {
            LogManager.Log($"[COMMAND B] Executed with parameters: {string.Join(" ", args)}");
        }
    }
}
