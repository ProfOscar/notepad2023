using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad2023
{
    public partial class FormMain : Form
    {
        ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath);

        const char EDITED_MARK = '*';
        const string FORM_TITLE_SEPARATOR = " - ";
        const string SHORT_PROGRAM_NAME = "Blocco note";
        const string PROGRAM_NAME = "Blocco note di Windows";
        string filePath;
        string fileName;

        string lastSavedText;
        DialogResult lastSaveAsDialogResult;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            pageSetupDialogMain.Document = printDocumentMain;
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
            DialogResult result = CheckIfSave();
            if (result == DialogResult.Yes)
            {
                if (filePath != string.Empty)
                {
                    saveFile(filePath);
                    reset();
                }
                else
                {
                    if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
                    {
                        saveFile(saveFileDialogMain.FileName);
                        reset();
                    }
                }
            }
            else if (result == DialogResult.No)
            {
                reset();
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
            lastSaveAsDialogResult = saveFileDialogMain.ShowDialog();
            if (lastSaveAsDialogResult == DialogResult.OK)
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

        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath == string.Empty)
            {
                salvaconnomeToolStripMenuItem_Click(sender, e);
            }
            else
            {
                saveFile(filePath);
            }
        }

        private void apriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastSaveAsDialogResult = DialogResult.None;
            DialogResult result = CheckIfSave();
            if (result == DialogResult.Yes)
            {
                if (filePath == string.Empty)
                {
                    salvaconnomeToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    saveFile(filePath);
                }
            }
            openFileDialogMain.FileName = "";
            if (result != DialogResult.Cancel && 
                lastSaveAsDialogResult != DialogResult.Cancel && 
                openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                openFile(openFileDialogMain.FileName);
            }
        }

        private void openFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    rtbMain.Text = reader.ReadToEnd();
                }
                filePath = path;
                fileName = Path.GetFileName(filePath);
                lastSavedText = rtbMain.Text;
                SetFormTitle();
            }
            catch (Exception)
            {
                MessageBox.Show("Problemi durante l'apertura del file",
                                    "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = CheckIfSave();
            switch (result)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    if (filePath != string.Empty)
                        saveFile(filePath);
                    else
                    {
                        if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
                            saveFile(saveFileDialogMain.FileName);
                        else
                            e.Cancel = true;
                    }
                    break;
            }
        }

        private void nuovaFinestraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(info);
        }

        private void impostaPaginaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pageSetupDialogMain.ShowDialog() == DialogResult.OK)
            {
                printDocumentMain.PrinterSettings = pageSetupDialogMain.PrinterSettings;
            }
        }

        private void stampaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialogMain.ShowDialog() == DialogResult.OK)
            {
                printDocumentMain.OriginAtMargins = true;
                printDocumentMain.Print();
            }
        }

        private string stringToPrint;
        private void printDocumentMain_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            stringToPrint = rtbMain.Text;
        }

        private void printDocumentMain_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            // Sets the value of charactersOnPage to the number of characters 
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, rtbMain.Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);

            // Draws the string within the bounds of the page
            e.Graphics.DrawString(stringToPrint, rtbMain.Font, new SolidBrush(rtbMain.ForeColor),
                e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint.Substring(charactersOnPage);

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);
        }
    }
}
