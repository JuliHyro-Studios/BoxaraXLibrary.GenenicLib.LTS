using System;
using System.Collections.Generic;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public enum PromptStyle
    {
        Default,                Linux,                  Powerline,              Minimal,                FullInfo,               Dark,                   SimpleArrow,            Brackets,               DoubleArrow,            Custom              }

    public static class CommandPromptTemplate
    {
        public static List<PromptSegment> GetPrompt(PromptStyle style, string customShellName = "BoxaraHS")
        {
            string username = Environment.UserName;
            string hostname = Environment.MachineName;

            return style switch
            {
                PromptStyle.Default => new List<PromptSegment>
                {
                    new PromptSegment { Text = customShellName, Color = ConsoleColor.Cyan },
                    new PromptSegment { Text = "> ", Color = ConsoleColor.Green }
                },

                PromptStyle.Linux => new List<PromptSegment>
                {
                    new PromptSegment { Text = "[", Color = ConsoleColor.DarkGray },
                    new PromptSegment { Text = username, Color = ConsoleColor.Green },
                    new PromptSegment { Text = "@", Color = ConsoleColor.DarkGray },
                    new PromptSegment { Text = hostname, Color = ConsoleColor.Cyan },
                    new PromptSegment { Text = "] $ ", Color = ConsoleColor.Yellow }
                },

                PromptStyle.Powerline => new List<PromptSegment>
                {
                    new PromptSegment { Text = " ", Color = ConsoleColor.Cyan },
                    new PromptSegment { Text = customShellName, Color = ConsoleColor.Magenta },
                    new PromptSegment { Text = "  ", Color = ConsoleColor.DarkGray }
                },

                PromptStyle.Minimal => new List<PromptSegment>
                {
                    new PromptSegment { Text = "$ ", Color = ConsoleColor.Green }
                },

                PromptStyle.FullInfo => new List<PromptSegment>
                {
                    new PromptSegment { Text = "[", Color = ConsoleColor.DarkGray },
                    new PromptSegment { Text = username, Color = ConsoleColor.Green },
                    new PromptSegment { Text = "@", Color = ConsoleColor.DarkGray },
                    new PromptSegment { Text = hostname, Color = ConsoleColor.Cyan },
                    new PromptSegment { Text = " ", Color = ConsoleColor.DarkGray },
                    new PromptSegment { Text = customShellName, Color = ConsoleColor.Yellow },
                    new PromptSegment { Text = "]> ", Color = ConsoleColor.DarkGray }
                },

                PromptStyle.Dark => new List<PromptSegment>
                {
                    new PromptSegment { Text = "█ ", Color = ConsoleColor.DarkGray },
                    new PromptSegment { Text = customShellName, Color = ConsoleColor.Cyan },
                    new PromptSegment { Text = " █ ", Color = ConsoleColor.DarkGray }
                },

                PromptStyle.SimpleArrow => new List<PromptSegment>
                {
                    new PromptSegment { Text = "➜ ", Color = ConsoleColor.Cyan },
                    new PromptSegment { Text = customShellName, Color = ConsoleColor.Green },
                    new PromptSegment { Text = " $ ", Color = ConsoleColor.DarkGray }
                },

                PromptStyle.Brackets => new List<PromptSegment>
                {
                    new PromptSegment { Text = "[", Color = ConsoleColor.DarkGray },
                    new PromptSegment { Text = customShellName, Color = ConsoleColor.Cyan },
                    new PromptSegment { Text = "]> ", Color = ConsoleColor.Green }
                },

                PromptStyle.DoubleArrow => new List<PromptSegment>
                {
                    new PromptSegment { Text = ">> ", Color = ConsoleColor.DarkGray },
                    new PromptSegment { Text = customShellName, Color = ConsoleColor.Magenta },
                    new PromptSegment { Text = " >> ", Color = ConsoleColor.DarkGray }
                },

                _ => new List<PromptSegment>
                {
                    new PromptSegment { Text = customShellName, Color = ConsoleColor.Cyan },
                    new PromptSegment { Text = "> ", Color = ConsoleColor.Green }
                }
            };
        }

        public static List<PromptSegment> GetCustomPrompt(Func<string> promptGenerator, ConsoleColor color = ConsoleColor.Cyan)
        {
            return new List<PromptSegment>
    {
        new PromptSegment { Text = promptGenerator(), Color = color },
        new PromptSegment { Text = " ", Color = ConsoleColor.White }
    };
        }
    }
}