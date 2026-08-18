using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;
using System;
using System.Runtime.InteropServices;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.Log
{
    public static class LogConsole
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput, COORD dwCursorPosition);

        [DllImport("kernel32.dll")]
        private static extern bool FillConsoleOutputCharacter(
            IntPtr hConsoleOutput,
            char cCharacter,
            uint nLength,
            COORD dwWriteCoord,
            out uint lpNumberOfCharsWritten
        );

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CONSOLE_SCREEN_BUFFER_INFO
        {
            public COORD dwSize;
            public COORD dwCursorPosition;
            public ushort wAttributes;
            public SMALL_RECT srWindow;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        
        public static void Clear(string DateTimeCalled = "nodate", bool IsShowShell = false)
        {
            try
            {
                IntPtr hConsole = GetStdHandle(-11);
                if (hConsole == IntPtr.Zero)
                {
                    Console.Clear();
                    if (IsShowShell)
                    {
                        ShowCurrentShell();
                    }
                    return;
                }

                GetConsoleScreenBufferInfo(hConsole, out var bufferInfo);

                int width = bufferInfo.dwSize.X;
                int height = bufferInfo.dwSize.Y;

                COORD topLeft = new COORD { X = 0, Y = 0 };

                FillConsoleOutputCharacter(
                    hConsole,
                    ' ',
                    (uint)(width * height),
                    topLeft,
                    out _
                );

                SetConsoleCursorPosition(hConsole, topLeft);

                if (IsShowShell)
                {
                    ShowCurrentShell();
                }
            }
            catch
            {
                Console.Clear();
                if (IsShowShell)
                {
                    ShowCurrentShell();
                }
            }
        }

        private static void ShowCurrentShell()
        {
            try
            {
                string currentShellName = ReflectionShellTemplate.GetCurrentShell();

                if (!string.IsNullOrEmpty(currentShellName))
                {
                    int openResult = ShelliftAPIBuild.OpenShellWithResult(currentShellName);
                    if (openResult != codeint.SUCESS)
                    {
                        Console.WriteLine($"[WARN] Cannot open shell '{currentShellName}'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to show current shell: {ex.Message}");
            }
        }

        
        public static void WriteLine(string DateTimeCalled = "nodate")
        {
            Console.WriteLine();
        }

        public static void WriteLine(string value, string DateTimeCalled = "nodate")
        {
            Console.WriteLine(value);
        }

        public static void Write(string value, string DateTimeCalled = "nodate")
        {
            Console.Write(value);
        }

        public static void Write(object value, string DateTimeCalled = "nodate")
        {
            Console.Write(value);
        }

        public static void ResetColor(string DateTimeCalled = "nodate")
        {
            Console.ResetColor();
        }


        public static ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public static ConsoleColor BackgroundColor
        {
            get => Console.BackgroundColor;
            set => Console.BackgroundColor = value;
        }
    }
}