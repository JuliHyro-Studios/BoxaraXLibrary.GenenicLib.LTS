using System;
using System.Collections.Generic;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public static class ExternalCommandManager
    {
        private static readonly List<ICommand> _externalCommands = new List<ICommand>();
        private static readonly object _lock = new object();

        public static void RegisterExternalCommands(IEnumerable<ICommand> commands)
        {
            lock (_lock)
            {
                _externalCommands.Clear();
                if (commands != null)
                {
                    _externalCommands.AddRange(commands);
                }
            }
        }
        public static int GetTotalCommandCount(List<ICommand> coreCommands)
        {
            int coreCount = coreCommands?.Count ?? 0;
            int externalCount;
            lock (_lock)
            {
                externalCount = _externalCommands.Count;
            }
            return coreCount + externalCount;
        }

        public static (int CoreCount, int ExternalCount, int TotalCount) GetCommandCountInfo(List<ICommand> coreCommands)
        {
            int coreCount = coreCommands?.Count ?? 0;
            int externalCount;
            lock (_lock)
            {
                externalCount = _externalCommands.Count;
            }
            return (coreCount, externalCount, coreCount + externalCount);
        }

        public static string GetLoadedMessage(List<ICommand> coreCommands)
        {
            var info = GetCommandCountInfo(coreCommands);
            return $"Loaded {info.TotalCount} commands (Core: {info.CoreCount}, Other: {info.ExternalCount}).";
        }
        public static List<ICommand> GetExternalCommands()
        {
            lock (_lock)
            {
                return new List<ICommand>(_externalCommands);
            }
        }

        public static List<ICommand> MergeCommands(List<ICommand> coreCommands)
        {
            var all = new List<ICommand>();
            all.AddRange(coreCommands);
            lock (_lock)
            {
                all.AddRange(_externalCommands);
            }
            return all;
        }

        public static void ClearExternalCommands()
        {
            lock (_lock)
            {
                _externalCommands.Clear();
            }
        }
    }
}