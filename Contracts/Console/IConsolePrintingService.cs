using System.Runtime.InteropServices;
using ConsoleTables;

namespace Contracts.Console
{
    public interface IConsolePrintingService
    {
        void WriteLine([Optional] string content);
        void Write([Optional] string content);
        void Clear();
        void WriteTable(ConsoleTable table, Format tableFormat);
    }
}