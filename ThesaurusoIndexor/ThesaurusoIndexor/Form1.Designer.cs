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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_Research = new System.Windows.Forms.Button();
            this.txb_Research = new System.Windows.Forms.TextBox();
            this.cbResearchLocation = new System.Windows.Forms.ComboBox();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.lblResearchNumber = new System.Windows.Forms.Label();
            this.pbLoad = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btn_Research
            // 
            this.btn_Research.Enabled = false;
            this.btn_Research.Location = new System.Drawing.Point(170, 125);
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
            // cbResearchLocation
            // 
            this.cbResearchLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResearchLocation.FormattingEnabled = true;
            this.cbResearchLocation.Items.AddRange(new object[] {
            "Tout",
            "etml.ch",
            "educanet2.ch",
            "K:\\INF\\Eleves\\Temp"});
            this.cbResearchLocation.Location = new System.Drawing.Point(13, 69);
            this.cbResearchLocation.Name = "cbResearchLocation";
            this.cbResearchLocation.Size = new System.Drawing.Size(121, 21);
            this.cbResearchLocation.TabIndex = 2;
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(170, 165);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(349, 240);
            this.rtbResult.TabIndex = 4;
            this.rtbResult.Text = "";
            // 
            // lblResearchNumber
            // 
            this.lblResearchNumber.AutoSize = true;
            this.lblResearchNumber.Location = new System.Drawing.Point(252, 134);
            this.lblResearchNumber.Name = "lblResearchNumber";
            this.lblResearchNumber.Size = new System.Drawing.Size(22, 13);
            this.lblResearchNumber.TabIndex = 5;
            this.lblResearchNumber.Text = "-----";
            // 
            // pbLoad
            // 
            this.pbLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pbLoad.Location = new System.Drawing.Point(170, 96);
            this.pbLoad.Name = "pbLoad";
            this.pbLoad.Size = new System.Drawing.Size(349, 23);
            this.pbLoad.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 526);
            this.Controls.Add(this.pbLoad);
            this.Controls.Add(this.lblResearchNumber);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.cbResearchLocation);
            this.Controls.Add(this.txb_Research);
            this.Controls.Add(this.btn_Research);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Thesaurus - INDEXOR";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txb_Research;
        private System.Windows.Forms.ComboBox cbResearchLocation;
        public System.Windows.Forms.RichTextBox rtbResult;
        public System.Windows.Forms.Label lblResearchNumber;
        public System.Windows.Forms.ProgressBar pbLoad;
        public System.Windows.Forms.Button btn_Research;
    }
}

