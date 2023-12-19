using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad2023
{
    public partial class FormBaseFindReplace : Form
    {
        public FormBaseFindReplace()
        {
            InitializeComponent();
        }

        private void FormBaseFindReplace_Load(object sender, EventArgs e)
        {
            txtTrova.Text = FindReplaceClass.Parameters.TextToFind;
            chkMaiuscMinusc.Checked = FindReplaceClass.Parameters.IsCaseSensitive;
            chkParolaIntera.Checked = FindReplaceClass.Parameters.IsWholeWord;
        }

        private void btnTrova_Click(object sender, EventArgs e)
        {
            FindReplaceClass.Parameters.TextToFind = txtTrova.Text;
            FindReplaceClass.Parameters.IsCaseSensitive = chkMaiuscMinusc.Checked;
            FindReplaceClass.Parameters.IsWholeWord = chkParolaIntera.Checked;
            if (FindReplaceClass.Find() == -1)
            {
                this.Focus();
                FindReplaceClass.ShowNotFoundMessage();
            }
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
