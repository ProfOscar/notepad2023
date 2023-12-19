using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            public static bool IsWholeWord = false;
        }

        public static int Find()
        {
            RichTextBoxFinds options = RichTextBoxFinds.None;
            if (Parameters.IsCaseSensitive) options |= RichTextBoxFinds.MatchCase;
            if (Parameters.IsWholeWord) options |= RichTextBoxFinds.WholeWord;
            if (Parameters.IsUp) options |= RichTextBoxFinds.Reverse;
            Target.Focus();
            return Target.Find(Parameters.TextToFind, options);
        }
    }
}
