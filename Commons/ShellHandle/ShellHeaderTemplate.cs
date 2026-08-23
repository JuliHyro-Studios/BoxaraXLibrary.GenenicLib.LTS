using System;
using System.Collections.Generic;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public enum HeaderStyle
    {
        Classic,                    
        DoubleLine,                 
        StarBorder,                 
        Boxed,                      
        Minimal,                    
        Clean,                      
        Fancy,                      
        Banner,                     
        AsciiArt,                   
        Cyber,                      
        Neon,                       
        Retro,                      
        Matrix,                     
        Minimalist,                 
        Modern,                     
        Elegant,                
    }

    public static class ShellHeaderTemplate
    {
        private const int DEFAULT_WIDTH = 70;

        private static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public static void Show(List<ICommand> commands, string customWelcomeMessage, HeaderStyle style = HeaderStyle.Classic, string appName = "BoxaraHS", string appVersion = "1.0.0")
        {
            string time = GetCurrentTime();
            string name = appName;
            string version = appVersion;
            string commandsLoaded = ExternalCommandManager.GetLoadedMessage(commands);

            LogConsole.Clear(time);

            switch (style)
            {
                case HeaderStyle.Classic:
                    ShowClassic(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.DoubleLine:
                    ShowDoubleLine(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.StarBorder:
                    ShowStarBorder(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Boxed:
                    ShowBoxed(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Minimal:
                    ShowMinimal(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Clean:
                    ShowClean(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Fancy:
                    ShowFancy(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Banner:
                    ShowBanner(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.AsciiArt:
                    ShowAsciiArt(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Cyber:
                    ShowCyber(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Neon:
                    ShowNeon(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Retro:
                    ShowRetro(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Matrix:
                    ShowMatrix(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Minimalist:
                    ShowMinimalist(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Modern:
                    ShowModern(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                case HeaderStyle.Elegant:
                    ShowElegant(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
                default:
                    ShowClassic(name, version, commandsLoaded, customWelcomeMessage, time);
                    break;
            }
        }
        public static void ShowCustom(Action renderHeader)
        {
            string time = GetCurrentTime();
            LogConsole.Clear(time);
            renderHeader();
        }

        private static void ShowClassic(string name, string version, string commandsLoaded, string welcome, string time)
        {
            string line = new string('=', DEFAULT_WIDTH);
            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine(line, time);
            LogConsole.WriteLine($"{name} - {version}", time);
            LogConsole.WriteLine(line, time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowDoubleLine(string name, string version, string commandsLoaded, string welcome, string time)
        {
            string line = new string('═', DEFAULT_WIDTH);
            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine($"╔{line}╗", time);
            LogConsole.WriteLine($"║ {name} - {version} ".PadRight(DEFAULT_WIDTH + 2) + "║", time);
            LogConsole.WriteLine($"╚{line}╝", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowStarBorder(string name, string version, string commandsLoaded, string welcome, string time)
        {
            string line = new string('*', DEFAULT_WIDTH);
            LogConsole.ForegroundColor = ConsoleColor.Magenta;
            LogConsole.WriteLine(line, time);
            LogConsole.WriteLine($"*** {name} - {version} ***", time);
            LogConsole.WriteLine(line, time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowBoxed(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine("┌──────────────────────────────────────────────────────────────────────┐", time);
            LogConsole.WriteLine($"│ {name} - {version}".PadRight(70) + "│", time);
            LogConsole.WriteLine("├──────────────────────────────────────────────────────────────────────┤", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine("└──────────────────────────────────────────────────────────────────────┘", time);
            LogConsole.ResetColor();
        }

        private static void ShowMinimal(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Green;
            LogConsole.WriteLine($"{name} v{version}", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowClean(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine($"[ {name} - {version} ]", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowFancy(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Magenta;
            LogConsole.WriteLine($"✦ {name} - {version} ✦", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowBanner(string name, string version, string commandsLoaded, string welcome, string time)
        {
            string banner = new string('█', DEFAULT_WIDTH);
            LogConsole.ForegroundColor = ConsoleColor.DarkYellow;
            LogConsole.WriteLine(banner, time);
            LogConsole.WriteLine($"██ {name} - {version} ".PadRight(DEFAULT_WIDTH - 2) + "██", time);
            LogConsole.WriteLine(banner, time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowAsciiArt(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine("  ╔══════════════════════════════════════════════════════════════════╗", time);
            LogConsole.WriteLine("  ║                                                                  ║", time);
            LogConsole.WriteLine($"  ║    {name} - {version} ".PadRight(66) + "║", time);
            LogConsole.WriteLine("  ║                                                                  ║", time);
            LogConsole.WriteLine("  ╚══════════════════════════════════════════════════════════════════╝", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowCyber(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Magenta;
            LogConsole.WriteLine("╔══════════════════════════════════════════════════════════════════╗", time);
            LogConsole.WriteLine("║  ██████╗  ██████╗ ██╗  ██╗ █████╗ ██████╗  █████╗ ██╗  ██╗    ║", time);
            LogConsole.WriteLine($"║  {name} - {version} ".PadRight(66) + "║", time);
            LogConsole.WriteLine("║  ██╔══██╗██╔══██╗██║  ██║██╔══██╗██╔══██╗██╔══██╗██║  ██║    ║", time);
            LogConsole.WriteLine("╚══════════════════════════════════════════════════════════════════╝", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowNeon(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine("███╗   ██╗███████╗ ██████╗ ███╗   ██╗", time);
            LogConsole.WriteLine($" {name} - {version} ", time);
            LogConsole.WriteLine("██║   ██║██╔════╝██╔════╝ ██║   ██║", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowRetro(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Yellow;
            LogConsole.WriteLine("▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒", time);
            LogConsole.WriteLine($"▒ {name} - {version} ".PadRight(70) + "▒", time);
            LogConsole.WriteLine("▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowMatrix(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Green;
            LogConsole.WriteLine("┌─────────────────────────────────────────────────────────────────────┐", time);
            LogConsole.WriteLine("│  ███╗   ███╗ █████╗ ████████╗██████╗ ██╗██╗  ██╗         │", time);
            LogConsole.WriteLine($"│  {name} - {version} ".PadRight(69) + "│", time);
            LogConsole.WriteLine("│  ████╗ ████║██╔══██╗╚══██╔══╝██╔══██╗██║╚██╗██╔╝         │", time);
            LogConsole.WriteLine("└─────────────────────────────────────────────────────────────────────┘", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowMinimalist(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.DarkGray;
            LogConsole.WriteLine($"--- {name} ---", time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowModern(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine($"▸ {name} v{version}", time);
            LogConsole.WriteLine("▸ " + new string('─', 60), time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }

        private static void ShowElegant(string name, string version, string commandsLoaded, string welcome, string time)
        {
            LogConsole.ForegroundColor = ConsoleColor.Magenta;
            LogConsole.WriteLine($"❖ {name} — {version} ❖", time);
            LogConsole.WriteLine(new string('─', 60), time);
            LogConsole.ResetColor();
            LogConsole.WriteLine(commandsLoaded, time);
            LogConsole.WriteLine(welcome, time);
        }
    }
}