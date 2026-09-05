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

        public static bool Process(
            string input,
            List<ICommand> commands,
            Action<string, string[]>? preAction = null,
            Action<string, string[], bool>? postAction = null
        )
        {
            string time = GetCurrentTime();

            if (string.IsNullOrWhiteSpace(input))
                return false;

            try
            {
                var splitInput = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string commandName = splitInput[0].ToLower();
                string[] args = splitInput.Skip(1).ToArray();

                preAction?.Invoke(commandName, args);

                var allCommands = ExternalCommandManager.MergeCommands(commands);

                var cmd = allCommands.FirstOrDefault(c =>
                    string.Equals(c.Name, commandName, StringComparison.OrdinalIgnoreCase)
                    || (
                        c.Aliases != null
                        && c.Aliases.Any(a =>
                            string.Equals(a, commandName, StringComparison.OrdinalIgnoreCase)
                        )
                    )
                );

                bool result = false;

                if (cmd != null)
                {
                    try
                    {
                        if (args.Length > 0 && (cmd.Parameter == null || cmd.Parameter.Length == 0))
                        {
                            ErrorShellTemplate.ShowCommandInvalidParameter(
                                cmd.Name,
                                "This command does not accept any parameters."
                            );
                            result = true;
                        }
                        else
                        {
                            LogConsole.ForegroundColor = ConsoleColor.DarkGray;
                            LogConsole.WriteLine(
                                $"[WAIT] Waiting for command '{cmd.Name}' response...",
                                time
                            );
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
                            LogConsole.WriteLine(
                                $"[OK] Command '{cmd.Name}' executed successfully.",
                                time
                            );
                            LogConsole.ResetColor();

                            result = true;
                        }
                    }
                    catch (ArgumentException argEx)
                    {
                        ErrorShellTemplate.ShowCommandInvalidParameter(cmd.Name, argEx.Message);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        LogConsole.ForegroundColor = ConsoleColor.Red;
                        LogConsole.WriteLine(
                            $"[X] Error executing command '{cmd.Name}': {ex.Message}",
                            time
                        );
                        LogConsole.ResetColor();
                        result = true;
                    }
                }
                else
                {
                    var prefixMatches = commands
                        .Where(c =>
                            c.Name.StartsWith(commandName, StringComparison.OrdinalIgnoreCase)
                        )
                        .ToList();

                    if (prefixMatches.Count > 0)
                    {
                        ErrorShellTemplate.ShowPrefixMatches(
                            commandName,
                            prefixMatches,
                            allCommands
                        );
                        result = true;
                    }
                    else
                    {
                        ErrorShellTemplate.ShowCommandNotFound(commandName, allCommands);
                        result = false;
                    }
                }

                postAction?.Invoke(commandName, args, result);

                return result;
            }
            catch (Exception ex)
            {
                LogConsole.ForegroundColor = ConsoleColor.Red;
                LogConsole.WriteLine(
                    $"[X] Critical error in CommandProcessor: {ex.Message}, skipping execution...",
                    time
                );
                LogConsole.ResetColor();
                return false;
            }
        }
    }
}
