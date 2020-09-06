using System;
using System.Runtime.InteropServices;
using ConsoleTables;
using Contracts.Console;

namespace ChannelEngineConsoleApp.Services
{
    public class ConsolePrintingService : IConsolePrintingService
    {
        public void WriteLine([Optional] string content)
        {
            Console.WriteLine(content);
        }

        public void Write([Optional] string content)
        {
            Console.Write(content);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void WriteTable(ConsoleTable table, Format tableFormat)
        {
            table.Write(tableFormat);
        }
    }
}