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

        #region Event Handlers

        private void FormMain_Load(object sender, EventArgs e)
        {
            pageSetupDialogMain.EnableMetric = true;
            pageSetupDialogMain.Document = printDocumentMain;
            annullaToolStripMenuItem.Enabled = false;
            copiaToolStripMenuItem.Enabled = false;
            tagliaToolStripMenuItem.Enabled = false;
            eliminaToolStripMenuItem.Enabled = false;
            aCapoAutomaticoToolStripMenuItem.Checked = false;
            aCapoAutomaticoToolStripMenuItem.CheckOnClick = true;
            rtbMain.WordWrap = aCapoAutomaticoToolStripMenuItem.Checked;
            rtbMain.Font = new Font("Consolas", 11, FontStyle.Regular);
            fontDialogMain.Font = rtbMain.Font;
            barradistatoToolStripMenuItem.Checked = true;
            barradistatoToolStripMenuItem.CheckOnClick = true;
            statusStripBottom.Visible = barradistatoToolStripMenuItem.Checked;
            reset();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            incollaToolStripMenuItem.Enabled = Clipboard.ContainsText() || Clipboard.ContainsImage();
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

        private void rtbMain_TextChanged(object sender, EventArgs e)
        {
            SetFormTitle(lastSavedText != rtbMain.Text);
            annullaToolStripMenuItem.Enabled = rtbMain.CanUndo || rtbMain.CanRedo;
        }

        private void rtbMain_SelectionChanged(object sender, EventArgs e)
        {
            copiaToolStripMenuItem.Enabled =
                tagliaToolStripMenuItem.Enabled =
                eliminaToolStripMenuItem.Enabled =
                rtbMain.SelectionLength > 0;
        }

        private int nFirstCharOnPage;
        private void printDocumentMain_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            nFirstCharOnPage = 0;
        }

        private void printDocumentMain_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            nFirstCharOnPage = rtbMain.FormatRange(false,
                                                       e,
                                                       nFirstCharOnPage,
                                                       rtbMain.TextLength);
            // check if there are more pages to print
            e.HasMorePages = nFirstCharOnPage < rtbMain.TextLength;
        }

        private void printDocumentMain_EndPrint(object sender, PrintEventArgs e)
        {
            rtbMain.FormatRangeDone();
        }

        #endregion


        #region Custom Methods

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

        #endregion


        #region Click Handlers

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

        private void nuovaFinestraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(info);
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

        private void salvaconnomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastSaveAsDialogResult = saveFileDialogMain.ShowDialog();
            if (lastSaveAsDialogResult == DialogResult.OK)
            {
                saveFile(saveFileDialogMain.FileName);
            }
        }

        private void impostaPaginaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialogMain.ShowDialog();
        }

        private void stampaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (printDialogMain.ShowDialog() == DialogResult.OK)
                {
                    printDocumentMain.DocumentName = fileName;
                    printDocumentMain.Print();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problemi durante la stampa.\nSe stai stampando su file verifica che il file di destinazione non sia aperto.",
                    "ATTENZIONE!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void annullaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.CanRedo)
                rtbMain.Redo();
            else
                rtbMain.Undo();
        }

        private void tagliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.Cut();
        }

        private void copiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.Copy();
        }

        private void incollaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.Paste();
        }

        private void eliminaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectedText = "";
        }

        private void selezionatuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectAll();
        }

        private void oraDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectedText = DateTime.Now.ToString("H:mm dd/MM/yyyy");
        }

        #endregion

        private void aCapoAutomaticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.WordWrap = aCapoAutomaticoToolStripMenuItem.Checked;
        }

        private void carattereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialogMain.ShowDialog() == DialogResult.OK)
                rtbMain.Font = fontDialogMain.Font;
        }

        private void barradistatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStripBottom.Visible = barradistatoToolStripMenuItem.Checked;
        }

        private void guidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://go.microsoft.com/fwlink/?LinkId=834783");
        }

        private void inviaFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ProfOscar/calculator2023/issues");
        }

        private void informazionisuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxMain aboutForm = new AboutBoxMain();
            aboutForm.ShowDialog();
        }
    }
}
