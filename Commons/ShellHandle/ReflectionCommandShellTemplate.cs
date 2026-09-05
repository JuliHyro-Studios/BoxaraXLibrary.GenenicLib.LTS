using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public static class ReflectionCommandShellTemplate
    {
                private static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                public static List<ICommand> LoadCommandsForShell(string targetShellName)
        {
            var commandList = new List<ICommand>();

            try
            {
                                var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s =>
                    {
                        try { return s.GetTypes(); }
                        catch { return Type.EmptyTypes; }
                    })
                    .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .Where(t => t.GetCustomAttribute<NonLoadableCommandAttribute>() == null);

                foreach (var type in commandTypes)
                {
                    if (Activator.CreateInstance(type) is ICommand instance)
                    {
                        if (string.IsNullOrWhiteSpace(instance.Shell))
                            continue;

                        var supportedShells = instance.Shell
                            .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim());

                        if (supportedShells.Any(s => string.Equals(s, targetShellName, StringComparison.OrdinalIgnoreCase)))
                        {
                            commandList.Add(instance);
                        }
                    }
                }

                                var externalCommands = ExternalCommandManager.GetExternalCommands();
                foreach (var cmd in externalCommands)
                {
                    if (!string.IsNullOrWhiteSpace(cmd.Shell))
                    {
                        var shells = cmd.Shell
                            .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim());

                        if (shells.Any(s => string.Equals(s, targetShellName, StringComparison.OrdinalIgnoreCase)))
                        {
                            if (!commandList.Any(c => c.Name == cmd.Name))
                            {
                                commandList.Add(cmd);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogConsole.WriteLine($"[X] Error loading commands: {ex.Message}");
            }

            return commandList;
        }

        public static List<ICommand> GetCommandsForShell(string targetShellName)
        {
            return LoadCommandsForShell(targetShellName);
        }

        public static List<ICommand> GetCommandAllInterface()
        {
            var commandList = new List<ICommand>();

            try
            {
                var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s =>
                    {
                        try { return s.GetTypes(); }
                        catch { return Type.EmptyTypes; }
                    })
                    .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .Where(t => t.GetCustomAttribute<NonLoadableCommandAttribute>() == null);

                foreach (var type in commandTypes)
                {
                    try
                    {
                        if (Activator.CreateInstance(type) is ICommand instance)
                        {
                            commandList.Add(instance);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogConsole.WriteLine($"[X] Failed to instantiate command '{type.FullName}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogConsole.WriteLine($"[X] Error loading all commands: {ex.Message}");
            }

                        return ExternalCommandManager.MergeCommands(commandList);
        }
    }
}
