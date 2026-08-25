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
        public static void Run(
    List<PromptSegment> segments,
    List<ICommand> commands,
    Func<string>? inputProvider = null,
    Action<string>? preProcessor = null,
    Action<string, bool>? postProcessor = null,
    Func<bool>? exitCondition = null,
    Action<string, string[]>? commandPreAction = null,
    Action<string, string[], bool>? commandPostAction = null)
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

                    string input;
                    if (inputProvider != null)
                    {
                        input = inputProvider() ?? string.Empty;
                    }
                    else
                    {
                        input = Console.ReadLine()?.Trim() ?? string.Empty;
                    }

                    if (string.IsNullOrEmpty(input))
                        continue;

                    preProcessor?.Invoke(input);

                    bool result = CommandProcessorTemplate.Process(
                        input,
                        commands,
                        commandPreAction,
                        commandPostAction
                    );

                    postProcessor?.Invoke(input, result);

                    if (exitCondition != null && exitCondition())
                    {
                        isRunning = false;
                    }
                }
                catch (Exception ex)
                {
                    LogConsole.ForegroundColor = ConsoleColor.Red;
                    LogConsole.WriteLine($"[X] Shell Loop error: {ex.Message}");
                    LogConsole.ResetColor();
                }
            }
        }
    }
    }