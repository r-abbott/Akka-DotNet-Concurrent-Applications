using System;

namespace MovieStreaming
{
    public static class ColorConsole
    {
        public static void WriteLineGreen(string message)
        {
            WriteLineColor(message, ConsoleColor.Green);
        }

        public static void WriteLineYellow(string message)
        {
            WriteLineColor(message, ConsoleColor.Yellow);
        }

        public static void WriteLineRed(string message)
        {
            WriteLineColor(message, ConsoleColor.Red);
        }

        private static void WriteLineColor(string message, ConsoleColor color)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = beforeColor;
        }
    }
}
