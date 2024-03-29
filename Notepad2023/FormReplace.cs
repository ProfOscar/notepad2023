﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Notepad2023
{
    public partial class FormReplace : Notepad2023.FormBaseFindReplace
    {
        public FormReplace()
        {
            InitializeComponent();
            btnSostituisci.Enabled = btnSostTutto.Enabled = txtTrova.TextLength > 0;
        }

        private void txtTrova_TextChanged(object sender, EventArgs e)
        {
            btnSostituisci.Enabled = btnSostTutto.Enabled = txtTrova.TextLength > 0;
        }

        private void btnSostituisci_Click(object sender, EventArgs e)
        {
            FindReplaceClass.Parameters.TextToFind = txtTrova.Text;
            FindReplaceClass.Parameters.TextToReplace = txtSost.Text;
            FindReplaceClass.Parameters.IsCaseSensitive = chkMaiuscMinusc.Checked;
            FindReplaceClass.Parameters.IsWholeWord = chkParolaIntera.Checked;
            if (FindReplaceClass.Replace() == -1)
            {
                this.Focus();
                FindReplaceClass.ShowNotFoundMessage();
            }
        }

        private void btnSostTutto_Click(object sender, EventArgs e)
        {
            FindReplaceClass.Parameters.TextToFind = txtTrova.Text;
            FindReplaceClass.Parameters.TextToReplace = txtSost.Text;
            FindReplaceClass.Parameters.IsCaseSensitive = chkMaiuscMinusc.Checked;
            FindReplaceClass.Parameters.IsWholeWord = chkParolaIntera.Checked;
            FindReplaceClass.ReplaceAll();
        }
    }
}
