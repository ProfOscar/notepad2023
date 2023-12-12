namespace Notepad2023
{
    partial class FormReplace
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSost = new System.Windows.Forms.TextBox();
            this.btnSostituisci = new System.Windows.Forms.Button();
            this.btnSostTutto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Location = new System.Drawing.Point(243, 97);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Sostituisci:";
            // 
            // txtSost
            // 
            this.txtSost.Location = new System.Drawing.Point(66, 42);
            this.txtSost.Name = "txtSost";
            this.txtSost.Size = new System.Drawing.Size(166, 20);
            this.txtSost.TabIndex = 7;
            // 
            // btnSostituisci
            // 
            this.btnSostituisci.Location = new System.Drawing.Point(243, 39);
            this.btnSostituisci.Name = "btnSostituisci";
            this.btnSostituisci.Size = new System.Drawing.Size(100, 23);
            this.btnSostituisci.TabIndex = 8;
            this.btnSostituisci.Text = "Sostituisci";
            this.btnSostituisci.UseVisualStyleBackColor = true;
            // 
            // btnSostTutto
            // 
            this.btnSostTutto.Location = new System.Drawing.Point(243, 68);
            this.btnSostTutto.Name = "btnSostTutto";
            this.btnSostTutto.Size = new System.Drawing.Size(100, 23);
            this.btnSostTutto.TabIndex = 9;
            this.btnSostTutto.Text = "Sostituisci tutto";
            this.btnSostTutto.UseVisualStyleBackColor = true;
            // 
            // FormReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(351, 133);
            this.Controls.Add(this.btnSostTutto);
            this.Controls.Add(this.btnSostituisci);
            this.Controls.Add(this.txtSost);
            this.Controls.Add(this.label1);
            this.Name = "FormReplace";
            this.Controls.SetChildIndex(this.btnAnnulla, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtSost, 0);
            this.Controls.SetChildIndex(this.btnSostituisci, 0);
            this.Controls.SetChildIndex(this.btnSostTutto, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSost;
        private System.Windows.Forms.Button btnSostituisci;
        private System.Windows.Forms.Button btnSostTutto;
    }
}
