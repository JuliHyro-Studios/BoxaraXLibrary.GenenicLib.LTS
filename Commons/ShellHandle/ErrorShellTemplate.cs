using System;
using System.Collections.Generic;
using System.Linq;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public static class ErrorShellTemplate
    {
        private static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                public static void ShowCommandNotFound(string input, List<ICommand> allCommands)
        {
            string time = GetCurrentTime();

            if (allCommands == null || allCommands.Count == 0)
            {
                LogConsole.ForegroundColor = ConsoleColor.Red;
                LogConsole.WriteLine($"[X] Command '{input}' not found.", time);
                LogConsole.ResetColor();
                return;
            }

                        var matches = allCommands
                .Where(c => c.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Count > 0)
            {
                                ShowPrefixMatches(input, matches, allCommands);
                return;
            }

            LogConsole.ForegroundColor = ConsoleColor.Red;
            LogConsole.WriteLine($"[X] Command '{input}' not found. Type 'help' to see available commands.", time);
            LogConsole.ResetColor();
        }

                public static void ShowPrefixMatches(string input, List<ICommand> prefixMatches, List<ICommand> allCommands)
        {
            string time = GetCurrentTime();
            LogConsole.ForegroundColor = ConsoleColor.Yellow;
            LogConsole.WriteLine($"[!] '{input}' is not a complete command. Did you mean:", time);

            foreach (var match in prefixMatches)
            {
                LogConsole.WriteLine($"  - {match.Name} ({match.Description})", time);
            }

                        LogConsole.WriteLine($"[i] Total available commands: {allCommands.Count}", time);
            LogConsole.ResetColor();
        }

                public static void ShowCommandNotFound(string input)
        {
            string time = GetCurrentTime();
            LogConsole.ForegroundColor = ConsoleColor.Red;
            LogConsole.WriteLine($"[X] Command '{input}' not found. Type 'help' to see available commands.", time);
            LogConsole.ResetColor();
        }

        public static void ShowCommandInvalidParameter(string commandName, string details = "")
        {
            string time = GetCurrentTime();
            LogConsole.ForegroundColor = ConsoleColor.Red;
            LogConsole.WriteLine($"[X] Invalid parameter(s) for command '{commandName}'. [Command Module Response: {details}]".TrimEnd(), time);
            LogConsole.WriteLine($"[i] Type 'help' or check command usage for more details.", time);
            LogConsole.ResetColor();
        }
    }
}