using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Notepad2023
{
    public partial class FormGotoLine : Form
    {
        public int nLine = int.MinValue;

        private int totLines;
        private bool skipFormClosing = false;

        public FormGotoLine(int tl)
        {
            totLines = tl;
            InitializeComponent();
        }

        private void txtNumeroRiga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                errorProviderMain.SetError(txtNumeroRiga, "Carattere non ammesso");
                e.Handled = true;
            }
            else
            {
                errorProviderMain.Clear();
            }
        }

        private void btnVaiA_Click(object sender, EventArgs e)
        {
            int.TryParse(txtNumeroRiga.Text, out nLine);
            if (nLine > totLines || nLine < 1)
            {
                MessageBox.Show("Numero di riga maggiore del numero di righe totale",
                    "Blocco note. Vai alla riga");
                txtNumeroRiga.Focus();
                txtNumeroRiga.Text = totLines.ToString();
                txtNumeroRiga.SelectAll();
                skipFormClosing = true;
            }
        }

        private void FormGotoLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = skipFormClosing;
            skipFormClosing = false;
        }
    }
}
