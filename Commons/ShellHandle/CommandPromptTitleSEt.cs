using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    internal class CommandPromptTitleSEt
    {
        private static readonly object _lockObject = new object();

        public static void SetTitle(string title, string[] Reason, DateTime TimeStamp, [CallerMemberName] string FileName = "")
        {
            if (string.IsNullOrWhiteSpace(title) || Reason == null || Reason.Length == 0 || TimeStamp == default)
            {
                return;
            }
            if (string.IsNullOrEmpty(FileName))
            {
                return;
            }
            string timeStr = TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
            string reasonStr = string.Join(", ", Reason);
            string filenameStr = FileName;
            lock (_lockObject)
            {
                try
                {
                    Console.Title = title;
                    LogConsole.WriteLine($"[INFO] Console title successfully changed to '{title}' by [{filenameStr}] at {timeStr} due to: {reasonStr}");

                }
                catch (Exception ex)
                {
                    LogConsole.WriteLine($"[X] Error setting console title by [{filenameStr}] at {timeStr} due to [{reasonStr}]. Exception: {ex.Message}");
                }
            }
        }
    }
}