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
                var splitInput = input.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (splitInput.Length == 0) return false;

                string commandName = splitInput[0].ToLower();
                string rawArgs = splitInput.Length > 1 ? splitInput[1] : string.Empty;
                string[] args = rawArgs.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                preAction?.Invoke(commandName, args);

                var allCommands = ExternalCommandManager.MergeCommands(commands);

                var candidates = allCommands
                    .Where(c =>
                        string.Equals(c.Name, commandName, StringComparison.OrdinalIgnoreCase)
                        || (
                            c.Aliases != null
                            && c.Aliases.Any(a =>
                                string.Equals(a, commandName, StringComparison.OrdinalIgnoreCase)
                            )
                        )
                    )
                    .GroupBy(c => c.GetType())
                    .Select(g => g.First())
                    .ToList();

                bool result = false;

                if (candidates.Count == 0)
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
                else if (candidates.Count == 1)
                {
                    var cmd = candidates[0];
                    try
                    {
                        if (args.Length > 0 && (cmd.Parameter == null || cmd.Parameter.Length == 0))
                        {
                            ErrorShellTemplate.ShowCommandInvalidParameter(
                                cmd.Name,
                                "This command does not accept any parameters.",
                                "Framework"
                            );
                            result = true;
                        }
                        else
                        {
                            string? error = null;
                            LogConsole.ForegroundColor = ConsoleColor.DarkGray;
                            LogConsole.WriteLine(
                                $"[WAIT] Waiting for command '{cmd.Name}' response...",
                                time
                            );
                            LogConsole.ResetColor();

                            string[] processedArgs = ResolveParameters(cmd, rawArgs, out error);

                            if (error != null)
                            {
                                ErrorShellTemplate.ShowCommandInvalidParameter(cmd.Name, error, "Framework");
                                LogConsole.ForegroundColor = ConsoleColor.Red;
                                LogConsole.WriteLine(
                                    $"[FAIL] Command '{cmd.Name}' failed to execute.",
                                    time
                                );
                                LogConsole.ResetColor();
                                result = true;
                            }
                            else if (args.Length > 0)
                            {
                                cmd.ParameterExecute(processedArgs);
                            }
                            else
                            {
                                bool hasRequired = cmd.Parameter != null && cmd.Parameter.Any(p => p.Contains("require:true", StringComparison.OrdinalIgnoreCase));
                                if (hasRequired)
                                {
                                    ErrorShellTemplate.ShowCommandInvalidParameter(cmd.Name, "This command requires parameters to execute.");
                                    LogConsole.ForegroundColor = ConsoleColor.Red;
                                    LogConsole.WriteLine(
                                        $"[FAIL] Command '{cmd.Name}' failed to execute.",
                                        time
                                    );
                                    LogConsole.ResetColor();
                                }
                                else
                                {
                                    cmd.Execute();
                                    LogConsole.ForegroundColor = ConsoleColor.Green;
                                    LogConsole.WriteLine(
                                        $"[OK] Command '{cmd.Name}' executed successfully.",
                                        time
                                    );
                                    LogConsole.ResetColor();
                                }
                            }

                            result = true;
                        }
                    }
                    catch (ArgumentException argEx)
                    {
                        ErrorShellTemplate.ShowCommandInvalidParameter(cmd.Name, argEx.Message, "Command Module");
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
                    ICommand? selectedCmd = null;
                    while (selectedCmd == null)
                    {
                        LogConsole.WriteLine(
                            $"\nMultiple commands found for \"{commandName}\":",
                            time
                        );
                        for (int i = 0; i < candidates.Count; i++)
                        {
                            var c = candidates[i];
                            string assemblyName =
                                c.GetType().Assembly.GetName()?.Name ?? "Unknown Assembly";
                            string aliases =
                                c.Aliases != null && c.Aliases.Length > 0
                                    ? string.Join(", ", c.Aliases)
                                    : "None";

                            LogConsole.WriteLine($"  [{i + 1}] {c.DisplayName}", time);
                            LogConsole.WriteLine($"      Assembly: {assemblyName}", time);
                            LogConsole.WriteLine($"      Type: {c.GetType().FullName}", time);
                            LogConsole.WriteLine($"      Aliases: {aliases}", time);
                            LogConsole.WriteLine($"      Description: {c.Description}", time);
                        }

                        LogConsole.Write(
                            $"\nSelect command [1-{candidates.Count}] (or 'exit'/'cancel' to cancel): "
                        );
                        string? choiceInput = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(choiceInput))
                        {
                            continue;
                        }

                        if (
                            string.Equals(choiceInput, "exit", StringComparison.OrdinalIgnoreCase)
                            || string.Equals(
                                choiceInput,
                                "cancel",
                                StringComparison.OrdinalIgnoreCase
                            )
                        )
                        {
                            LogConsole.WriteLine("Selection cancelled.", time);
                            selectedCmd = null;
                            break;
                        }

                        if (
                            int.TryParse(choiceInput, out int choice)
                            && choice >= 1
                            && choice <= candidates.Count
                        )
                        {
                            selectedCmd = candidates[choice - 1];
                            LogConsole.WriteLine(
                                $"Selected: {selectedCmd.GetType().FullName}",
                                time
                            );
                        }
                        else
                        {
                            LogConsole.ForegroundColor = ConsoleColor.Red;
                            LogConsole.WriteLine(
                                $"[X] Invalid selection. Please enter a number between 1 and {candidates.Count}, or 'exit'/'cancel'.",
                                time
                            );
                            LogConsole.ResetColor();
                        }
                    }

                    if (selectedCmd == null)
                    {
                        return false;
                    }

                    try
                    {
                        if (
                            args.Length > 0
                            && (selectedCmd.Parameter == null || selectedCmd.Parameter.Length == 0)
                        )
                        {
                            ErrorShellTemplate.ShowCommandInvalidParameter(
                                selectedCmd.Name,
                                "This command does not accept any parameters.",
                                "Framework"
                            );
                            result = true;
                        }
                        else
                        {
                            string? error = null;
                            LogConsole.ForegroundColor = ConsoleColor.DarkGray;
                            LogConsole.WriteLine(
                                $"[WAIT] Waiting for command '{selectedCmd.Name}' response...",
                                time
                            );
                            LogConsole.ResetColor();

                            string[] processedArgs = ResolveParameters(selectedCmd, rawArgs, out error);

                            if (error != null)
                            {
                                ErrorShellTemplate.ShowCommandInvalidParameter(selectedCmd.Name, error, "Framework");
                                LogConsole.ForegroundColor = ConsoleColor.Red;
                                LogConsole.WriteLine(
                                    $"[FAIL] Command '{selectedCmd.Name}' failed to execute.",
                                    time
                                );
                                LogConsole.ResetColor();
                                result = true;
                            }
                            else if (args.Length > 0)
                            {
                                selectedCmd.ParameterExecute(processedArgs);
                                LogConsole.ForegroundColor = ConsoleColor.Green;
                                LogConsole.WriteLine(
                                    $"[OK] Command '{selectedCmd.Name}' executed successfully.",
                                    time
                                );
                                LogConsole.ResetColor();
                            }
                            else
                            {
                                bool hasRequired = selectedCmd.Parameter != null && selectedCmd.Parameter.Any(p => p.Contains("require:true", StringComparison.OrdinalIgnoreCase));
                                if (hasRequired)
                                {
                                    ErrorShellTemplate.ShowCommandInvalidParameter(selectedCmd.Name, "This command requires parameters to execute.");
                                    LogConsole.ForegroundColor = ConsoleColor.Red;
                                    LogConsole.WriteLine(
                                        $"[FAIL] Command '{selectedCmd.Name}' failed to execute.",
                                        time
                                    );
                                    LogConsole.ResetColor();
                                }
                                else
                                {
                                    selectedCmd.Execute();
                                    LogConsole.ForegroundColor = ConsoleColor.Green;
                                    LogConsole.WriteLine(
                                        $"[OK] Command '{selectedCmd.Name}' executed successfully.",
                                        time
                                    );
                                    LogConsole.ResetColor();
                                }
                            }

                            result = true;
                        }
                    }
                    catch (ArgumentException argEx)
                    {
                        ErrorShellTemplate.ShowCommandInvalidParameter(
                            selectedCmd.Name,
                            argEx.Message,
                            "Command Module"
                        );
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        LogConsole.ForegroundColor = ConsoleColor.Red;
                        LogConsole.WriteLine(
                            $"[X] Error executing command '{selectedCmd.Name}': {ex.Message}",
                            time
                        );
                        LogConsole.ResetColor();
                        result = true;
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

        private static string[] ResolveParameters(ICommand cmd, string rawArgs, out string? error)
        {
            error = null;
            if (cmd.Parameter == null || cmd.Parameter.Length == 0)
            {
                return rawArgs.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            string[] resultValues = new string[cmd.Parameter.Length];
            List<string> keys = new List<string>();

            for (int i = 0; i < cmd.Parameter.Length; i++)
            {
                string pattern = cmd.Parameter[i].Split('|')[0].Trim();
                int placeholderIndex = pattern.IndexOf("{0}");
                if (placeholderIndex != -1)
                {
                    keys.Add(pattern.Substring(0, placeholderIndex));
                }
            }

            for (int i = 0; i < cmd.Parameter.Length; i++)
            {
                string definition = cmd.Parameter[i];
                string[] parts = definition.Split('|');
                string pattern = parts[0].Trim();
                bool isRequired = parts.Length > 1 && parts[1].Trim().Equals("require:true", StringComparison.OrdinalIgnoreCase);

                int placeholderIndex = pattern.IndexOf("{0}");
                if (placeholderIndex != -1)
                {
                    string key = pattern.Substring(0, placeholderIndex);
                    int startPos = rawArgs.IndexOf(key, StringComparison.OrdinalIgnoreCase);
                    if (startPos != -1)
                    {
                        int valueStart = startPos + key.Length;
                        int nextKeyPos = rawArgs.Length;

                        foreach (var otherKey in keys)
                        {
                            int pos = rawArgs.IndexOf(otherKey, valueStart, StringComparison.OrdinalIgnoreCase);
                            if (pos != -1 && pos < nextKeyPos)
                            {
                                nextKeyPos = pos;
                            }
                        }
                        resultValues[i] = rawArgs.Substring(valueStart, nextKeyPos - valueStart).Trim();
                    }
                }

                if (isRequired && string.IsNullOrEmpty(resultValues[i]))
                {
                    error = $"Missing required parameter: {pattern}";
                    return Array.Empty<string>();
                }
            }

            for (int i = 0; i < resultValues.Length; i++)
            {
                if (resultValues[i] == null) resultValues[i] = string.Empty;
            }

            return resultValues;
        }
    }
}
