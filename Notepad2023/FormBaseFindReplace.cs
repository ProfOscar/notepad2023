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

        private void btnTrova_Click(object sender, EventArgs e)
        {
            FindReplaceClass.Parameters.TextToFind = txtTrova.Text;
            FindReplaceClass.Parameters.IsCaseSensitive = chkMaiuscMinusc.Checked;
            FindReplaceClass.Parameters.IsTextAround = chkTestoIntorno.Checked;
            if (FindReplaceClass.Find() == -1)
            {
                MessageBox.Show(
                    $"Impossibile trovare \"{txtTrova.Text}\"",
                    "Blocco note",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information
                    );
            }
        }
    }
}
