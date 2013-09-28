using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.FluentAutomation
{
    public class ConsoleHelper
    {
        public static void DrawMessageBox(int spacesFromLeft, string message, bool maxWidth = false)
        {
            try
            {
                var lines = message.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var longestLine = maxWidth == true ? Console.WindowWidth - 4 : lines.Max(x => x.Length);

                if (spacesFromLeft + longestLine > Console.WindowWidth - 4)
                {
                    longestLine = Console.WindowWidth - spacesFromLeft - 4;
                }

                var paddingLine = "-".PadRight(longestLine, '-');
                lines.Insert(0, paddingLine);
                lines.Add(paddingLine);

                foreach (var line in lines)
                {
                    if (line.Length > longestLine)
                    {
                        var charsAccountedFor = 0;
                        while (charsAccountedFor != line.Length)
                        {
                            var lineSubstring = string.Empty;
                            if (charsAccountedFor + longestLine < line.Length)
                                lineSubstring = line.Substring(charsAccountedFor, longestLine);
                            else
                                lineSubstring = line.Substring(charsAccountedFor);

                            var printedLine = string.Empty;
                            var lastSpaceIndex = lineSubstring.LastIndexOf(' ');
                            var trimmedLastIndex = lineSubstring.TrimStart().LastIndexOf(' ');
                            if (lastSpaceIndex == -1 || lastSpaceIndex != trimmedLastIndex || charsAccountedFor + lineSubstring.Length == line.Length)
                                printedLine = lineSubstring;
                            else
                                printedLine = lineSubstring.Substring(0, lastSpaceIndex);

                            charsAccountedFor += printedLine.Length;

                            // Dirty hack to deal with re-written stack traces that contain spaces instead of tabs
                            printedLine = printedLine.Replace("   ", "\t").Trim(' ').Replace("\t", "   ");

                            Console.Write("".PadRight(spacesFromLeft));
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write(" " + printedLine.PadRight(longestLine) + " ");
                            Console.Write(Environment.NewLine);
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Write("".PadRight(spacesFromLeft));
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" " + line.PadRight(longestLine) + " ");
                        Console.ResetColor();
                        Console.Write(Environment.NewLine);
                    }
                }
            }
            catch (System.IO.IOException) { }
        }

        public static void Clear()
        {
            try
            {
                Console.Clear();
            }
            catch (System.IO.IOException) { }
        }

        private static string GetFillerSpaces(int count)
        {
            return "".PadRight(count);
        }
    }
}
