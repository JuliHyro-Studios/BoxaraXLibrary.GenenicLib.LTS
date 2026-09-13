using System;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting BoxaraXLibrary.GenenicLib.LTS test...");

            ShelliftAPIBuild.OpenShell("TestShell");
        }
    }

    public sealed class TestprCommand : ICommand
    {
        public string DisplayName => "Testpr";

        public string[] Parameter => new[] { "--hi:{0} | require:true", "--age:{0} | require:true" };

        public string Name => "testpr";

        public string[] Aliases => Array.Empty<string>();

        public string Category => "Testing";

        public string Shell => "TestShell";

        public string Description => "Test one-parameter command using the required 'hi' argument.";

        public string CommandVersion => "1.0.0";

        public void Execute()
        {
            LogManager.Log("[TESTPR] Execute() called without parameters.");
        }

        public void ParameterExecute(string[] args)
        {
            LogManager.Log($"[TESTPR] args count = {args.Length}");

            if (args.Length > 0)
            {
                LogManager.Log($"[TESTPR] firstArg (hi) = '{args[0]}'");
            }
            if (args.Length > 1)
            {
                LogManager.Log($"[TESTPR] secondArg (age) = '{args[1]}'");
            }

            LogManager.Log($"[TESTPR] declared parameter count = {Parameter.Length}");
        }
    }
}
