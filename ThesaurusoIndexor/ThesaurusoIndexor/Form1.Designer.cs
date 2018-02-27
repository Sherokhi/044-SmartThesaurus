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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_Research = new System.Windows.Forms.Button();
            this.txb_Research = new System.Windows.Forms.TextBox();
            this.cbResearchLocation = new System.Windows.Forms.ComboBox();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.lblResearchNumber = new System.Windows.Forms.Label();
            this.pbLoad = new System.Windows.Forms.ProgressBar();
            this.rtbResultData = new System.Windows.Forms.RichTextBox();
            this.timerButton = new System.Windows.Forms.Timer(this.components);
            this.btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Research
            // 
            this.btn_Research.Location = new System.Drawing.Point(884, 9);
            this.btn_Research.Name = "btn_Research";
            this.btn_Research.Size = new System.Drawing.Size(75, 53);
            this.btn_Research.TabIndex = 0;
            this.btn_Research.Text = "Rechercher";
            this.btn_Research.UseVisualStyleBackColor = true;
            this.btn_Research.Click += new System.EventHandler(this.btn_Research_Click);
            // 
            // txb_Research
            // 
            this.txb_Research.Location = new System.Drawing.Point(174, 12);
            this.txb_Research.Name = "txb_Research";
            this.txb_Research.Size = new System.Drawing.Size(704, 20);
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
            this.cbResearchLocation.Location = new System.Drawing.Point(17, 12);
            this.cbResearchLocation.Name = "cbResearchLocation";
            this.cbResearchLocation.Size = new System.Drawing.Size(121, 21);
            this.cbResearchLocation.TabIndex = 2;
            // 
            // rtbResult
            // 
            this.rtbResult.BackColor = System.Drawing.Color.White;
            this.rtbResult.Location = new System.Drawing.Point(13, 95);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.ReadOnly = true;
            this.rtbResult.Size = new System.Drawing.Size(492, 310);
            this.rtbResult.TabIndex = 4;
            this.rtbResult.Text = "";
            // 
            // lblResearchNumber
            // 
            this.lblResearchNumber.AutoSize = true;
            this.lblResearchNumber.Location = new System.Drawing.Point(14, 49);
            this.lblResearchNumber.Name = "lblResearchNumber";
            this.lblResearchNumber.Size = new System.Drawing.Size(22, 13);
            this.lblResearchNumber.TabIndex = 5;
            this.lblResearchNumber.Text = "-----";
            // 
            // pbLoad
            // 
            this.pbLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pbLoad.Location = new System.Drawing.Point(174, 39);
            this.pbLoad.Name = "pbLoad";
            this.pbLoad.Size = new System.Drawing.Size(704, 23);
            this.pbLoad.TabIndex = 6;
            // 
            // rtbResultData
            // 
            this.rtbResultData.BackColor = System.Drawing.Color.White;
            this.rtbResultData.Location = new System.Drawing.Point(521, 95);
            this.rtbResultData.Name = "rtbResultData";
            this.rtbResultData.ReadOnly = true;
            this.rtbResultData.Size = new System.Drawing.Size(492, 310);
            this.rtbResultData.TabIndex = 7;
            this.rtbResultData.Text = "";
            // 
            // timerButton
            // 
            this.timerButton.Tick += new System.EventHandler(this.timerButton_Tick);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(13, 414);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(1000, 23);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "Charger la base de données";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 449);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.rtbResultData);
            this.Controls.Add(this.pbLoad);
            this.Controls.Add(this.lblResearchNumber);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.cbResearchLocation);
            this.Controls.Add(this.txb_Research);
            this.Controls.Add(this.btn_Research);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Thesaurus - INDEXOR";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        public System.Windows.Forms.RichTextBox rtbResultData;
        private System.Windows.Forms.Timer timerButton;
        private System.Windows.Forms.Button btnLoad;
    }
}

