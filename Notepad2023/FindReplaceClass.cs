using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            public static string TextToReplace = "";
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

        public static int Replace()
        {
            //if (Parameters.IsCaseSensitive)
            //{ 
            //    if (Target.SelectedText == Parameters.TextToFind)
            //        Target.SelectedText = Parameters.TextToReplace;
            //}
            //else
            //{
            //    if (Target.SelectedText.ToLower() == Parameters.TextToFind.ToLower())
            //        Target.SelectedText = Parameters.TextToReplace;
            //}
            StringComparison comparison = Parameters.IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            int stComp = String.Compare(Target.SelectedText, Parameters.TextToFind, comparison);
            if (stComp == 0) Target.SelectedText = Parameters.TextToReplace;
            return Find();
        }

        public static void ReplaceAll()
        {
            RegexOptions options = Parameters.IsCaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
            string textToFind = Parameters.IsWholeWord ? $@"\b{Parameters.TextToFind}\b": Parameters.TextToFind;
            Target.Text = Regex.Replace(Target.Text, textToFind, Parameters.TextToReplace, options);
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
