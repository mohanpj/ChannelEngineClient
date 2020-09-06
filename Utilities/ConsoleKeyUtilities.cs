using System;

namespace Utilities
{
    public static class ConsoleKeyUtilities
    {
        public static int GetIntFromDigitKey(ConsoleKeyInfo key)
        {
            return int.Parse(key.KeyChar.ToString());
        }
    }
}