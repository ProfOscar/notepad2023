using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Notepad2023
{
    public partial class FormFind : Notepad2023.FormBaseFindReplace
    {
        public FormFind()
        {
            InitializeComponent();
            radioDown.Checked = true;
        }
    }
}
