namespace ThesaurusoIndexor
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Research = new System.Windows.Forms.Button();
            this.txb_Research = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_Research
            // 
            this.btn_Research.Enabled = false;
            this.btn_Research.Location = new System.Drawing.Point(302, 121);
            this.btn_Research.Name = "btn_Research";
            this.btn_Research.Size = new System.Drawing.Size(75, 23);
            this.btn_Research.TabIndex = 0;
            this.btn_Research.Text = "Rechercher";
            this.btn_Research.UseVisualStyleBackColor = true;
            this.btn_Research.Click += new System.EventHandler(this.btn_Research_Click);
            // 
            // txb_Research
            // 
            this.txb_Research.Location = new System.Drawing.Point(170, 69);
            this.txb_Research.Name = "txb_Research";
            this.txb_Research.Size = new System.Drawing.Size(349, 20);
            this.txb_Research.TabIndex = 1;
            this.txb_Research.TextChanged += new System.EventHandler(this.txb_Research_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Tout",
            "etml.ch",
            "educanet2.ch",
            "K:\\INF\\Eleves\\Temp"});
            this.comboBox1.Location = new System.Drawing.Point(13, 69);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 526);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txb_Research);
            this.Controls.Add(this.btn_Research);
            this.Name = "Form1";
            this.Text = "Thesaurus - INDEXOR";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Research;
        private System.Windows.Forms.TextBox txb_Research;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

