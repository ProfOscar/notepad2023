using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad2023
{
    static class FindReplaceClass
    {
        public static RichTextBoxEx Target;

        public struct Parameters
        {
            public static string TextToFind = "";
            public static bool IsUp = false;
            public static bool IsCaseSensitive = false;
            public static bool IsTextAround = false;
        }

        public static int Find()
        {
            return -1;
        }
    }
}
