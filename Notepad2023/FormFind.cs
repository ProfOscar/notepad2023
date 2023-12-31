﻿using System;
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
            radioUp.Checked = FindReplaceClass.Parameters.IsUp;
            radioDown.Checked = !radioUp.Checked;
        }

        private void radioUp_CheckedChanged(object sender, EventArgs e)
        {
            FindReplaceClass.Parameters.IsUp = radioUp.Checked;
        }
    }
}
