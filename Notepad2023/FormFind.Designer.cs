namespace Notepad2023
{
    partial class FormFind
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpDirezione = new System.Windows.Forms.GroupBox();
            this.radioUp = new System.Windows.Forms.RadioButton();
            this.radioDown = new System.Windows.Forms.RadioButton();
            this.grpDirezione.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDirezione
            // 
            this.grpDirezione.Controls.Add(this.radioDown);
            this.grpDirezione.Controls.Add(this.radioUp);
            this.grpDirezione.Location = new System.Drawing.Point(137, 44);
            this.grpDirezione.Name = "grpDirezione";
            this.grpDirezione.Size = new System.Drawing.Size(95, 44);
            this.grpDirezione.TabIndex = 6;
            this.grpDirezione.TabStop = false;
            this.grpDirezione.Text = "Direzione";
            // 
            // radioUp
            // 
            this.radioUp.AutoSize = true;
            this.radioUp.Location = new System.Drawing.Point(4, 20);
            this.radioUp.Name = "radioUp";
            this.radioUp.Size = new System.Drawing.Size(38, 17);
            this.radioUp.TabIndex = 0;
            this.radioUp.TabStop = true;
            this.radioUp.Text = "Sù";
            this.radioUp.UseVisualStyleBackColor = true;
            // 
            // radioDown
            // 
            this.radioDown.AutoSize = true;
            this.radioDown.Location = new System.Drawing.Point(51, 20);
            this.radioDown.Name = "radioDown";
            this.radioDown.Size = new System.Drawing.Size(41, 17);
            this.radioDown.TabIndex = 1;
            this.radioDown.TabStop = true;
            this.radioDown.Text = "Giù";
            this.radioDown.UseVisualStyleBackColor = true;
            // 
            // FormFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(351, 112);
            this.Controls.Add(this.grpDirezione);
            this.Name = "FormFind";
            this.Text = "Trova";
            this.Controls.SetChildIndex(this.grpDirezione, 0);
            this.grpDirezione.ResumeLayout(false);
            this.grpDirezione.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDirezione;
        private System.Windows.Forms.RadioButton radioDown;
        private System.Windows.Forms.RadioButton radioUp;
    }
}
