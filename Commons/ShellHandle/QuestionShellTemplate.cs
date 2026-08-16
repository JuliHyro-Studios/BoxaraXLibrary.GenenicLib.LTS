using System;
using System.Threading;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public static class QuestionShellTemplate
    {
        private static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public static bool ShowQuestion(
            string message,
            string confirmText = "Y",
            string cancelText = "N",
            int timeoutSeconds = -1,
            string timeoutMessage = "Operation timed out. Defaulting action.",
            bool continueOnTimeout = false)
        {
            string time = GetCurrentTime();

            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.Write($"[?] {message} [{confirmText}/{cancelText}]: ", time);
            LogConsole.ResetColor();

            if (timeoutSeconds <= 0)
            {
                while (true)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    char pressedChar = char.ToUpper(keyInfo.KeyChar);
                    char confirmChar = char.ToUpper(confirmText[0]);
                    char cancelChar = char.ToUpper(cancelText[0]);

                    if (pressedChar == confirmChar || keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine(confirmText);
                        return true;
                    }
                    else if (pressedChar == cancelChar || keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine(cancelText);
                        return false;
                    }
                }
            }

            DateTime startTime = DateTime.Now;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    char pressedChar = char.ToUpper(keyInfo.KeyChar);
                    char confirmChar = char.ToUpper(confirmText[0]);
                    char cancelChar = char.ToUpper(cancelText[0]);

                    if (pressedChar == confirmChar || keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine(confirmText);
                        return true;
                    }
                    else if (pressedChar == cancelChar || keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine(cancelText);
                        return false;
                    }
                }

                if ((DateTime.Now - startTime).TotalSeconds >= timeoutSeconds)
                {
                    Console.WriteLine();
                    string msg = string.IsNullOrWhiteSpace(timeoutMessage)
                        ? $"[!] Question timed out after {timeoutSeconds}s."
                        : timeoutMessage;

                    LogConsole.ForegroundColor = ConsoleColor.Yellow;
                    LogConsole.WriteLine(msg, time);
                    LogConsole.ResetColor();

                    return continueOnTimeout;
                }

                Thread.Sleep(50);
            }
        }
    }
}