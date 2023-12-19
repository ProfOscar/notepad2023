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
            int start = 0;
            int end = Target.TextLength;
            RichTextBoxFinds options = RichTextBoxFinds.None;
            if (Parameters.IsUp)
            {
                options |= RichTextBoxFinds.Reverse;
                end = Target.SelectionStart;
                if (end == 0) return -1;
            }
            else
            {
                start = Target.SelectionStart + Target.SelectionLength;
                if (start >= end) return -1;
            }
            if (Parameters.IsCaseSensitive) options |= RichTextBoxFinds.MatchCase;
            if (Parameters.IsWholeWord) options |= RichTextBoxFinds.WholeWord;
            Target.Focus();
            return Target.Find(Parameters.TextToFind, start, end, options);
        }

        public static void ShowNotFoundMessage()
        {
            MessageBox.Show(
                    $"Impossibile trovare \"{Parameters.TextToFind}\"",
                    "Blocco note",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
        }
    }
}
