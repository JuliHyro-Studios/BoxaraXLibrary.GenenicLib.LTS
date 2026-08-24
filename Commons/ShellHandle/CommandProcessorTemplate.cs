using System;
using System.Collections.Generic;
using System.Linq;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public static class CommandProcessorTemplate
    {
        private static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public static bool Process(string input, List<ICommand> commands)  // 👈 TRẢ VỀ bool
        {
            string time = GetCurrentTime();

            if (string.IsNullOrWhiteSpace(input))
                return false;

            try
            {
                var splitInput = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string commandName = splitInput[0].ToLower();
                string[] args = splitInput.Skip(1).ToArray();

                var allCommands = ExternalCommandManager.MergeCommands(commands);

                var cmd = allCommands.FirstOrDefault(c =>
                    string.Equals(c.Name, commandName, StringComparison.OrdinalIgnoreCase) ||
                    (c.Aliases != null && c.Aliases.Any(a => string.Equals(a, commandName, StringComparison.OrdinalIgnoreCase))));

                if (cmd != null)
                {
                    try
                    {
                        if (args.Length > 0 && (cmd.Parameter == null || cmd.Parameter.Length == 0))
                        {
                            ErrorShellTemplate.ShowCommandInvalidParameter(cmd.Name, "This command does not accept any parameters.");
                            return true;
                        }

                        LogConsole.ForegroundColor = ConsoleColor.DarkGray;
                        LogConsole.WriteLine($"[WAIT] Waiting for command '{cmd.Name}' response...", time);
                        LogConsole.ResetColor();

                        if (args.Length > 0)
                        {
                            cmd.ParameterExecute(args);
                        }
                        else
                        {
                            cmd.Execute();
                        }

                        LogConsole.ForegroundColor = ConsoleColor.Green;
                        LogConsole.WriteLine($"[OK] Command '{cmd.Name}' executed successfully.", time);
                        LogConsole.ResetColor();

                        return true;
                    }
                    catch (ArgumentException argEx)
                    {
                        ErrorShellTemplate.ShowCommandInvalidParameter(cmd.Name, argEx.Message);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        LogConsole.ForegroundColor = ConsoleColor.Red;
                        LogConsole.WriteLine($"[X] Error executing command '{cmd.Name}': {ex.Message}", time);
                        LogConsole.ResetColor();
                        return true;
                    }
                }

                var prefixMatches = commands
                    .Where(c => c.Name.StartsWith(commandName, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (prefixMatches.Count > 0)
                {
                    ErrorShellTemplate.ShowPrefixMatches(commandName, prefixMatches, allCommands);
                    return true;
                }

                ErrorShellTemplate.ShowCommandNotFound(commandName, allCommands);
                return false;
            }
            catch (Exception ex)
            {
                LogConsole.ForegroundColor = ConsoleColor.Red;
                LogConsole.WriteLine($"[X] Critical error in CommandProcessor: {ex.Message}, skipping execution...", time);
                LogConsole.ResetColor();
                return false;
            }
        }
    }
}