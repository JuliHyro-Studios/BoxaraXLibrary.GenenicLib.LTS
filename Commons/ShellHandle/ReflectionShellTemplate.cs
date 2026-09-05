using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public static class ReflectionShellTemplate
    {
        private static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        private static string _currentShellName = "MainShell";
        private static readonly object _shellLock = new object();

        public static List<IShell> LoadAllShells()
        {
            var shellList = new List<IShell>();

            try
            {
                var shellTypes = AppDomain
                    .CurrentDomain.GetAssemblies()
                    .SelectMany(s =>
                    {
                        try
                        {
                            return s.GetTypes();
                        }
                        catch
                        {
                            return Type.EmptyTypes;
                        }
                    })
                    .Where(t =>
                        typeof(IShell).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract
                    );

                foreach (var type in shellTypes)
                {
                    if (Activator.CreateInstance(type) is IShell instance)
                    {
                        shellList.Add(instance);
                    }
                }
            }
            catch (Exception ex)
            {
                LogConsole.WriteLine(
                    $"[X] Error loading shells from template: {ex.Message}",
                    GetCurrentTime()
                );
            }

            return shellList;
        }

        public static IShell? GetShellByName(string shellName)
        {
            try
            {
                var allShells = LoadAllShells();
                return allShells.FirstOrDefault(s =>
                    string.Equals(s.ShellName, shellName, StringComparison.OrdinalIgnoreCase)
                );
            }
            catch
            {
                return null;
            }
        }

        public static List<IShell> GetShellsByCategory(string category)
        {
            var result = new List<IShell>();

            try
            {
                var allShells = LoadAllShells();
                result = allShells
                    .Where(s =>
                        string.Equals(s.Category, category, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                LogConsole.WriteLine(
                    $"[X] Error loading shells by category: {ex.Message}",
                    GetCurrentTime()
                );
            }

            return result;
        }

        public static bool ShellExists(string shellName)
        {
            try
            {
                return GetShellByName(shellName) != null;
            }
            catch
            {
                return false;
            }
        }

        public static string[] GetShellNames()
        {
            try
            {
                var shells = LoadAllShells();
                return shells.Select(s => s.ShellName).ToArray();
            }
            catch
            {
                return Array.Empty<string>();
            }
        }

        public static string GetCurrentShell()
        {
            lock (_shellLock)
            {
                return _currentShellName;
            }
        }

        internal static void SetCurrentShell(string shellName)
        {
            lock (_shellLock)
            {
                if (!string.IsNullOrWhiteSpace(shellName))
                {
                    _currentShellName = shellName;
                }
            }
        }

        public static IShell? GetCurrentShellObject()
        {
            return GetShellByName(GetCurrentShell());
        }
    }
}
