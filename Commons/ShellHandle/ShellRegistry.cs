using System;
using System.Collections.Generic;
using System.Linq;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public static class ShellRegistry
    {
        private static List<IShell>? _cachedShells = null;
        private static bool _initialized = false;
        private static readonly object _lock = new object();

        public static int Initialize()
        {
            lock (_lock)
            {
                if (_initialized) return codeint.SUCESS;

                try
                {
                    _cachedShells = ReflectionShellTemplate.LoadAllShells();
                    _initialized = true;
                    return codeint.SUCESS;
                }
                catch
                {
                    _cachedShells = new List<IShell>();
                    return codeint.FAILED;
                }
            }
        }

        public static (int code, IShell? shell) GetShell(string name)
        {
            if (!_initialized)
            {
                int initResult = Initialize();
                if (initResult == codeint.FAILED)
                    return (codeint.FAILED, null);
            }

            try
            {
                var shell = _cachedShells?.FirstOrDefault(s =>
                    string.Equals(s.ShellName, name, StringComparison.OrdinalIgnoreCase));

                if (shell != null)
                    return (codeint.SUCESS, shell);

                                shell = ReflectionShellTemplate.GetShellByName(name);
                if (shell != null)
                {
                    if (_cachedShells != null && !_cachedShells.Contains(shell))
                        _cachedShells.Add(shell);
                    return (codeint.SUCESS, shell);
                }

                return (codeint.FAILED, null);
            }
            catch
            {
                return (codeint.FAILED, null);
            }
        }

        public static (int code, List<IShell> shells) GetAllShells()
        {
            if (!_initialized)
            {
                int initResult = Initialize();
                if (initResult == codeint.FAILED)
                    return (codeint.FAILED, new List<IShell>());
            }

            try
            {
                return (codeint.SUCESS, _cachedShells ?? new List<IShell>());
            }
            catch
            {
                return (codeint.FAILED, new List<IShell>());
            }
        }

        public static (int code, string[] names) GetShellNames()
        {
            if (!_initialized)
            {
                int initResult = Initialize();
                if (initResult == codeint.FAILED)
                    return (codeint.FAILED, Array.Empty<string>());
            }

            try
            {
                var names = _cachedShells?.Select(s => s.ShellName).ToArray() ?? Array.Empty<string>();
                return (codeint.SUCESS, names);
            }
            catch
            {
                return (codeint.FAILED, Array.Empty<string>());
            }
        }

        public static (int code, List<IShell> shells) GetShellsByCategory(string category)
        {
            if (!_initialized)
            {
                int initResult = Initialize();
                if (initResult == codeint.FAILED)
                    return (codeint.FAILED, new List<IShell>());
            }

            try
            {
                var result = ReflectionShellTemplate.GetShellsByCategory(category);
                return (codeint.SUCESS, result);
            }
            catch
            {
                return (codeint.FAILED, new List<IShell>());
            }
        }

        public static (int code, bool exists) ShellExists(string name)
        {
            if (!_initialized)
            {
                int initResult = Initialize();
                if (initResult == codeint.FAILED)
                    return (codeint.FAILED, false);
            }

            try
            {
                bool exists = ReflectionShellTemplate.ShellExists(name);
                return (codeint.SUCESS, exists);
            }
            catch
            {
                return (codeint.FAILED, false);
            }
        }

        public static int OpenShell(string name)
        {
            var (code, shell) = GetShell(name);
            if (code == codeint.FAILED || shell == null)
                return codeint.FAILED;

            try
            {
                shell.Execute();
                return codeint.SUCESS;
            }
            catch
            {
                return codeint.FAILED;
            }
        }
    }
}