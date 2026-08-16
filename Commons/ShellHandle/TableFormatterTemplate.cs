using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public class TableFormatterTemplate
    {
        private readonly List<ColumnDefinition> _columns = new List<ColumnDefinition>();
        private readonly List<string[]> _rows = new List<string[]>();
        private int _totalWidth = 0;

        private string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public class ColumnDefinition
        {
            public string Header { get; set; } = string.Empty;
            public ConsoleColor? Color { get; set; }
            public int Width { get; set; }
        }

        public TableFormatterTemplate AddColumn(string header, ConsoleColor? color = null, int? fixedWidth = null)
        {
            if (_columns.Count >= 20)
                throw new InvalidOperationException("Maximum 20 columns allowed.");

            _columns.Add(new ColumnDefinition
            {
                Header = header,
                Color = color,
                Width = fixedWidth ?? header.Length
            });
            return this;
        }

        public TableFormatterTemplate AddRow(params string[] values)
        {
            if (values.Length != _columns.Count)
                throw new InvalidOperationException($"Row must have exactly {_columns.Count} values.");

            _rows.Add(values);
            UpdateColumnWidths(values);
            return this;
        }

        private void UpdateColumnWidths(string[] values)
        {
            for (int i = 0; i < values.Length && i < _columns.Count; i++)
            {
                int length = values[i]?.Length ?? 0;
                if (length > _columns[i].Width)
                {
                    _columns[i].Width = length;
                }
            }
        }

        public void Render()
        {
            string time = GetCurrentTime();
            int contentWidth = _columns.Sum(c => c.Width) + (_columns.Count * 2) + ((_columns.Count - 1) * 3);
            _totalWidth = contentWidth;

            string horizontalBorder = new string('═', _totalWidth);

                        LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine("╔" + horizontalBorder + "╗", time);
            LogConsole.ResetColor();

                        LogConsole.Write("║", time);
            for (int i = 0; i < _columns.Count; i++)
            {
                var col = _columns[i];
                if (col.Color.HasValue)
                    LogConsole.ForegroundColor = col.Color.Value;
                else
                    LogConsole.ForegroundColor = ConsoleColor.Cyan;

                string header = Truncate(col.Header, col.Width);
                LogConsole.Write(" " + header.PadRight(col.Width) + " ", time);
                LogConsole.ResetColor();

                if (i < _columns.Count - 1)
                {
                    LogConsole.ForegroundColor = ConsoleColor.Cyan;
                    LogConsole.Write("║", time);
                    LogConsole.ResetColor();
                }
            }
            LogConsole.Write("║", time);
            LogConsole.WriteLine(time);

                        LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine("╠" + horizontalBorder + "╣", time);
            LogConsole.ResetColor();

                        foreach (var row in _rows)
            {
                LogConsole.Write("║", time);
                for (int i = 0; i < row.Length && i < _columns.Count; i++)
                {
                    var col = _columns[i];
                    if (col.Color.HasValue)
                        LogConsole.ForegroundColor = col.Color.Value;
                    else
                        LogConsole.ResetColor();

                    string value = Truncate(row[i] ?? "", col.Width);
                    LogConsole.Write(" " + value.PadRight(col.Width) + " ", time);
                    LogConsole.ResetColor();

                    if (i < _columns.Count - 1)
                    {
                        LogConsole.ForegroundColor = ConsoleColor.Cyan;
                        LogConsole.Write("║", time);
                        LogConsole.ResetColor();
                    }
                }
                LogConsole.Write("║", time);
                LogConsole.WriteLine(time);
            }

                        LogConsole.ForegroundColor = ConsoleColor.Cyan;
            LogConsole.WriteLine("╚" + horizontalBorder + "╝", time);
            LogConsole.ResetColor();
        }

        public string RenderToString()
        {
            using (var writer = new System.IO.StringWriter())
            {
                var originalOut = Console.Out;
                Console.SetOut(writer);
                Render();
                Console.SetOut(originalOut);
                return writer.ToString();
            }
        }

        private string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            if (text.Length <= maxLength) return text;
            if (maxLength <= 3) return text.Substring(0, maxLength);
            return text.Substring(0, maxLength - 3) + "...";
        }
    }
}