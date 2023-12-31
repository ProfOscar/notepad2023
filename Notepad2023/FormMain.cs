﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        const string WIN = "Windows (CRLF)";
        const string MAC = "Macintosh (CR)";
        const string LIN = "Unix (LF)";

        string filePath;
        string fileName;
        Encoding fileEncoding;

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
            cercaConBingToolStripMenuItem.Enabled = false;
            vaiAToolStripMenuItem.Enabled = true;
            aCapoAutomaticoToolStripMenuItem.Checked = false;
            aCapoAutomaticoToolStripMenuItem.CheckOnClick = true;
            rtbMain.WordWrap = aCapoAutomaticoToolStripMenuItem.Checked;
            rtbMain.Font = new Font("Consolas", 11, FontStyle.Regular);
            rtbMain.MouseWheel += rtbMain_MouseWheel;
            fontDialogMain.Font = rtbMain.Font;
            barradistatoToolStripMenuItem.Checked = true;
            barradistatoToolStripMenuItem.CheckOnClick = true;
            statusStripBottom.Visible = barradistatoToolStripMenuItem.Checked;
            FindReplaceClass.Target = rtbMain;
            writeZoomInStatusBar();
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
            trovaToolStripMenuItem.Enabled = trovaPrecedenteToolStripMenuItem.Enabled =
                trovaSuccessivoToolStripMenuItem.Enabled = sostituisciToolStripMenuItem.Enabled =
                rtbMain.Text.Length > 0;
        }

        private void rtbMain_SelectionChanged(object sender, EventArgs e)
        {
            copiaToolStripMenuItem.Enabled =
                tagliaToolStripMenuItem.Enabled =
                eliminaToolStripMenuItem.Enabled =
                cercaConBingToolStripMenuItem.Enabled =
                rtbMain.SelectionLength > 0;
            writeLineColumnInStatusBar();
        }

        private void rtbMain_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                writeZoomInStatusBar();
            }
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
            toolStripStatusLabelLineColumn.Text = "Linea 1, colonna 1";
            toolStripStatusLabelLineEnding.Text = WIN;
            fileEncoding = Encoding.UTF8;
            toolStripStatusLabelEncoding.Text = fileEncoding.BodyName.ToUpper();
            trovaToolStripMenuItem.Enabled = trovaPrecedenteToolStripMenuItem.Enabled =
                trovaSuccessivoToolStripMenuItem.Enabled = sostituisciToolStripMenuItem.Enabled = 
                false;
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
                string lineTerminator = 
                    toolStripStatusLabelLineEnding.Text == WIN ? "\r\n" :
                    toolStripStatusLabelLineEnding.Text == MAC ? "\r" : 
                    "\n";
                string contentToSave = rtbMain.Text.Replace("\n", lineTerminator);
                using (StreamWriter writer = new StreamWriter(path, false, fileEncoding))
                {
                    writer.Write(contentToSave);
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
                    string rawText = reader.ReadToEnd();
                    if (rawText.Contains("\r\n")) toolStripStatusLabelLineEnding.Text = WIN;
                    else if (rawText.Contains("\r")) toolStripStatusLabelLineEnding.Text = MAC;
                    else if (rawText.Contains("\n")) toolStripStatusLabelLineEnding.Text = LIN;
                    fileEncoding = reader.CurrentEncoding;
                    toolStripStatusLabelEncoding.Text = fileEncoding.BodyName.ToUpper();
                    rtbMain.Text = rawText;
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

        private void writeLineColumnInStatusBar()
        {
            // Questa if serve per un bug sull'evento SelectionChanged del RichTextBox
            // che si verifica quando si attiva il word-wrap
            if (rtbMain.Text.Length > 0)
            {
                string stTextPart = rtbMain.Text.Substring(0, rtbMain.SelectionStart);
                int line = Regex.Matches(stTextPart, @"\n").Count + 1;
                // int line = rtbMain.GetLineFromCharIndex(rtbMain.SelectionStart) + 1;
                int column = rtbMain.SelectionStart - stTextPart.LastIndexOf('\n');
                // int column = rtbMain.SelectionStart - rtbMain.GetFirstCharIndexOfCurrentLine() + 1;
                string stToWrite = $"Linea {line}, colonna {column}";
                toolStripStatusLabelLineColumn.Text = stToWrite;
            }
            else
            {
                toolStripStatusLabelLineColumn.Text = "Linea 1, colonna 1";
            }
        }

        private void writeZoomInStatusBar()
        {
            int n = (int)(rtbMain.ZoomFactor * 10);
            toolStripStatusLabelZoom.Text = n + "0%";
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

        private void cercaConBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://www.bing.com/search?q=";
            // TODO: comporre la chiave di ricerca
            string key = rtbMain.SelectedText.Trim().Substring(0, 2000);
            Process.Start(url + key);
        }

        private void trovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.SelectionLength > 0)
                FindReplaceClass.Parameters.TextToFind = rtbMain.SelectedText;
            FormFind f = new FormFind();
            f.TopMost = true;
            f.Show();
        }

        private void trovaSuccessivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindReplaceClass.Parameters.TextToFind == "")
            {
                trovaToolStripMenuItem_Click(sender, e);
            }
            else
            {
                bool originalValue = FindReplaceClass.Parameters.IsUp;
                FindReplaceClass.Parameters.IsUp = false;
                if (FindReplaceClass.Find() == -1) FindReplaceClass.ShowNotFoundMessage();
                FindReplaceClass.Parameters.IsUp = originalValue;
            }
        }

        private void trovaPrecedenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindReplaceClass.Parameters.TextToFind == "")
            {
                trovaToolStripMenuItem_Click(sender, e);
            }
            else
            {
                bool originalValue = FindReplaceClass.Parameters.IsUp;
                FindReplaceClass.Parameters.IsUp = true;
                if (FindReplaceClass.Find() == -1) FindReplaceClass.ShowNotFoundMessage();
                FindReplaceClass.Parameters.IsUp = originalValue;
            }
        }

        private void sostituisciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReplace f = new FormReplace();
            f.TopMost = true;
            f.Show();
        }

        private void vaiAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGotoLine frmGotoLine = new FormGotoLine(rtbMain.Lines.Length);
            if (frmGotoLine.ShowDialog() == DialogResult.OK)
            {
                int charToGo = rtbMain.GetFirstCharIndexFromLine(frmGotoLine.nLine - 1);
                rtbMain.SelectionStart = charToGo;
            }
        }

        private void selezionatuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectAll();
        }

        private void oraDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectedText = DateTime.Now.ToString("H:mm dd/MM/yyyy");
        }

        private void aCapoAutomaticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.WordWrap = aCapoAutomaticoToolStripMenuItem.Checked;
            vaiAToolStripMenuItem.Enabled = !rtbMain.WordWrap;
            writeLineColumnInStatusBar();
        }

        private void carattereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialogMain.ShowDialog() == DialogResult.OK)
                rtbMain.Font = fontDialogMain.Font;
        }

        private void zoomAvantiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.ZoomFactor < 5) { 
                rtbMain.ZoomFactor += (float)0.1;
                writeZoomInStatusBar();
            }
        }

        private void zoomIndietroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.ZoomFactor >= 0.2) { 
                rtbMain.ZoomFactor -= 0.1f;
                writeZoomInStatusBar();
            }
        }

        private void ripristinaZoomPredefinitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.ZoomFactor = 1;
            writeZoomInStatusBar();
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

        #endregion

    }
}
