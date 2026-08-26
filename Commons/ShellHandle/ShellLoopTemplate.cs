using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
            string shellName,
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
                    StringBuilder inputBuilder = new StringBuilder();

                    lock (LogManager.RenderLock)
                    {
                        LogManager.SetActiveContext(segments, string.Empty);
                        foreach (var segment in segments)
                        {
                            LogConsole.ForegroundColor = segment.Color;
                            LogConsole.Write(segment.Text);
                        }
                        LogConsole.ResetColor();
                    }

                    bool hasInput = false;

                    while (!hasInput)
                    {
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                            lock (LogManager.RenderLock)
                            {
                                if (keyInfo.Key == ConsoleKey.Enter)
                                {
                                    Console.WriteLine();
                                    hasInput = true;
                                }
                                else if (keyInfo.Key == ConsoleKey.Backspace)
                                {
                                    if (inputBuilder.Length > 0)
                                    {
                                        inputBuilder.Remove(inputBuilder.Length - 1, 1);
                                        LogManager.SetActiveContext(segments, inputBuilder.ToString());
                                        int currentTop = Console.CursorTop;
                                        Console.SetCursorPosition(0, currentTop);
                                        Console.Write(new string(' ', Math.Max(0, Console.WindowWidth - 1)));
                                        Console.SetCursorPosition(0, currentTop);

                                        foreach (var segment in segments)
                                        {
                                            LogConsole.ForegroundColor = segment.Color;
                                            LogConsole.Write(segment.Text);
                                        }
                                        LogConsole.ResetColor();
                                        Console.Write(inputBuilder.ToString());
                                    }
                                }
                                else if (!char.IsControl(keyInfo.KeyChar))
                                {
                                    inputBuilder.Append(keyInfo.KeyChar);
                                    LogManager.SetActiveContext(segments, inputBuilder.ToString());
                                    Console.Write(keyInfo.KeyChar.ToString());
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(20);
                        }
                    }

                    string input = inputBuilder.ToString().Trim();
                    if (string.IsNullOrEmpty(input)) continue;

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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[X] Shell Loop error: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }
    }
}