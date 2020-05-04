using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace web.net.labb3.CustomHelpers
{
    public class CPrinter
    {
        public static void PrintInfo(string msg, ConsoleColor color)
        {
            var parts = msg.Split("'");

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                if (part.StartsWith("[") && part.EndsWith("]"))
                {
                    Console.ForegroundColor = color;
                    part = part.Substring(1, part.Length - 2);
                }
                if (part.StartsWith("<") && part.EndsWith(">"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.Write(part);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

    }
}
