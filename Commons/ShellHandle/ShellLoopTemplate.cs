using System;
using System.Collections.Generic;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;
namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public class PromptSegment
    {
        public string Text { get; set; } = string.Empty;
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
    }

    public static class ShellLoopTemplate
    {

        public static void Run(List<PromptSegment> segments, List<ICommand> commands)
        {
            bool isRunning = true;

            while (isRunning)
            {
                try
                {

                                        foreach (var segment in segments)
                    {
                        LogConsole.ForegroundColor = segment.Color;
                        LogConsole.Write(segment.Text);
                    }
                    LogConsole.ResetColor();

                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (string.IsNullOrEmpty(input))
                        continue;

                    CommandProcessorTemplate.Process(input, commands);
                }
                catch (Exception ex)
                {
                    LogConsole.ForegroundColor = ConsoleColor.Red;
                    LogConsole.WriteLine($"[X] Shell Loop encountered an error: {ex.Message}");
                    LogConsole.ResetColor();
                }
            }
        }
    }
}