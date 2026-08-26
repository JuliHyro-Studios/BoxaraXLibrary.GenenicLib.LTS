using System;
using System.Collections.Generic;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.Log
{
    public static class LogManager
    {
        private static readonly Queue<string> _logQueue = new Queue<string>();
        public static readonly object RenderLock = new object();

        private static string _currentInput = string.Empty;
        private static List<PromptSegment> _activeSegments = new List<PromptSegment>();

        public static void SetActiveContext(List<PromptSegment> segments, string currentInput)
        {
            lock (RenderLock)
            {
                _activeSegments = segments ?? new List<PromptSegment>();
                _currentInput = currentInput ?? string.Empty;
            }
        }

        public static void Log(string message)
        {
            lock (RenderLock)
            {
                _logQueue.Enqueue(message);
                FlushLogsInternal();
            }
        }

        public static void FlushLogs(List<PromptSegment> segments)
        {
            lock (RenderLock)
            {
                if (segments != null)
                {
                    _activeSegments = segments;
                }
                FlushLogsInternal();
            }
        }

        private static void FlushLogsInternal()
        {
            if (_logQueue.Count == 0) return;

            int currentTop = Console.CursorTop;
            Console.SetCursorPosition(0, currentTop);
            Console.Write(new string(' ', Math.Max(0, Console.WindowWidth - 1)));
            Console.SetCursorPosition(0, currentTop);

            while (_logQueue.Count > 0)
            {
                string log = _logQueue.Dequeue();
                Console.WriteLine(log);
            }

            foreach (var segment in _activeSegments)
            {
                LogConsole.ForegroundColor = segment.Color;
                LogConsole.Write(segment.Text);
            }
            LogConsole.ResetColor();

            Console.Write(_currentInput);
        }
    }
}