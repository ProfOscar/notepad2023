﻿namespace Notepad2023
{
    partial class FormBaseFindReplace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTrova = new System.Windows.Forms.Label();
            this.txtTrova = new System.Windows.Forms.TextBox();
            this.btnTrova = new System.Windows.Forms.Button();
            this.btnAnnulla = new System.Windows.Forms.Button();
            this.chkMaiuscMinusc = new System.Windows.Forms.CheckBox();
            this.chkParolaIntera = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTrova
            // 
            this.lblTrova.AutoSize = true;
            this.lblTrova.Location = new System.Drawing.Point(3, 15);
            this.lblTrova.Name = "lblTrova";
            this.lblTrova.Size = new System.Drawing.Size(38, 13);
            this.lblTrova.TabIndex = 0;
            this.lblTrova.Text = "Trova:";
            // 
            // txtTrova
            // 
            this.txtTrova.Location = new System.Drawing.Point(85, 12);
            this.txtTrova.Name = "txtTrova";
            this.txtTrova.Size = new System.Drawing.Size(147, 20);
            this.txtTrova.TabIndex = 1;
            this.txtTrova.TextChanged += new System.EventHandler(this.txtTrova_TextChanged);
            // 
            // btnTrova
            // 
            this.btnTrova.Location = new System.Drawing.Point(243, 8);
            this.btnTrova.Name = "btnTrova";
            this.btnTrova.Size = new System.Drawing.Size(100, 23);
            this.btnTrova.TabIndex = 2;
            this.btnTrova.Text = "Trova successivo";
            this.btnTrova.UseVisualStyleBackColor = true;
            this.btnTrova.Click += new System.EventHandler(this.btnTrova_Click);
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnulla.Location = new System.Drawing.Point(243, 37);
            this.btnAnnulla.Name = "btnAnnulla";
            this.btnAnnulla.Size = new System.Drawing.Size(100, 23);
            this.btnAnnulla.TabIndex = 3;
            this.btnAnnulla.Text = "Annulla";
            this.btnAnnulla.UseVisualStyleBackColor = true;
            this.btnAnnulla.Click += new System.EventHandler(this.btnAnnulla_Click);
            // 
            // chkMaiuscMinusc
            // 
            this.chkMaiuscMinusc.AutoSize = true;
            this.chkMaiuscMinusc.Location = new System.Drawing.Point(6, 68);
            this.chkMaiuscMinusc.Name = "chkMaiuscMinusc";
            this.chkMaiuscMinusc.Size = new System.Drawing.Size(126, 17);
            this.chkMaiuscMinusc.TabIndex = 4;
            this.chkMaiuscMinusc.Text = "Maiuscole/minuscole";
            this.chkMaiuscMinusc.UseVisualStyleBackColor = true;
            // 
            // chkParolaIntera
            // 
            this.chkParolaIntera.AutoSize = true;
            this.chkParolaIntera.Location = new System.Drawing.Point(6, 91);
            this.chkParolaIntera.Name = "chkParolaIntera";
            this.chkParolaIntera.Size = new System.Drawing.Size(86, 17);
            this.chkParolaIntera.TabIndex = 5;
            this.chkParolaIntera.Text = "Parola Intera";
            this.chkParolaIntera.UseVisualStyleBackColor = true;
            // 
            // FormBaseFindReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 112);
            this.Controls.Add(this.chkParolaIntera);
            this.Controls.Add(this.chkMaiuscMinusc);
            this.Controls.Add(this.btnAnnulla);
            this.Controls.Add(this.btnTrova);
            this.Controls.Add(this.txtTrova);
            this.Controls.Add(this.lblTrova);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormBaseFindReplace";
            this.Text = "FormBaseFindReplace";
            this.Load += new System.EventHandler(this.FormBaseFindReplace_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTrova;
        private System.Windows.Forms.Button btnTrova;
        protected System.Windows.Forms.Button btnAnnulla;
        protected System.Windows.Forms.CheckBox chkMaiuscMinusc;
        protected System.Windows.Forms.CheckBox chkParolaIntera;
        protected System.Windows.Forms.TextBox txtTrova;
    }
}