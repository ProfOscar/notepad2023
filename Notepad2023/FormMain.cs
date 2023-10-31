using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad2023
{
    public partial class FormMain : Form
    {
        const char EDITED_MARK = '*';
        const string FORM_TITLE_SEPARATOR = " - ";
        const string SHORT_PROGRAM_NAME = "Blocco note";
        const string PROGRAM_NAME = "Blocco note di Windows";
        string filePath;
        string fileName;

        string lastSavedText;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            rtbMain.Text = string.Empty;
            lastSavedText = string.Empty;
            filePath = string.Empty;
            fileName = "Senza nome";
            SetFormTitle();
        }

        private void SetFormTitle(bool isEdited = false)
        {
            this.Text = isEdited ? EDITED_MARK.ToString() : "";
            this.Text += fileName + FORM_TITLE_SEPARATOR + PROGRAM_NAME;
        }

        private void rtbMain_TextChanged(object sender, EventArgs e)
        {
            SetFormTitle(lastSavedText != rtbMain.Text);
        }

        private void nuovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastSavedText == rtbMain.Text)
            {
                rtbMain.Text = "";
            }
            else
            {
                DialogResult result = CheckIfSave();
                if (result == DialogResult.Yes)
                {
                    if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
                    {
                        saveFile(saveFileDialogMain.FileName);
                        reset();
                    }  
                }
                else if (result == DialogResult.No)
                {
                    reset();
                }
            }
        }

        public DialogResult CheckIfSave()
        {
            if (lastSavedText != rtbMain.Text)
            {
                return ShowSaveQuestionMessage();
            }
            else
            {
                return DialogResult.No;
            }
        }

        private DialogResult ShowSaveQuestionMessage()
        {
            return MessageBox.Show("Salvare le modifiche a " + fileName + "?", SHORT_PROGRAM_NAME,
                    MessageBoxButtons.YesNoCancel);
        }

        private void salvaconnomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                saveFile(saveFileDialogMain.FileName);
            }
        }

        private void saveFile(string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(rtbMain.Text);
                }
                filePath = path;
                fileName = Path.GetFileName(filePath);
                lastSavedText = rtbMain.Text;
                SetFormTitle();
            }
            catch (Exception)
            {
                MessageBox.Show("Problemi durante il salvataggio del file",
                    "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
