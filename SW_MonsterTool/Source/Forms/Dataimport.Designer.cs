namespace SW_MonsterTool
{
    partial class DataImport
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
            this.SelectMonsterImportButton1 = new System.Windows.Forms.Button();
            this.Import_DataOpenButton1 = new System.Windows.Forms.Button();
            this.Import_MonsterTreeView1 = new System.Windows.Forms.TreeView();
            this.Import_SelectMonsterLabel1 = new System.Windows.Forms.Label();
            this.Import_ReferenceButton1 = new System.Windows.Forms.Button();
            this.Import_DataURLTextBox1 = new System.Windows.Forms.TextBox();
            this.Import_DataLabel1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SelectMonsterImportButton1
            // 
            this.SelectMonsterImportButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectMonsterImportButton1.Location = new System.Drawing.Point(12, 437);
            this.SelectMonsterImportButton1.Name = "SelectMonsterImportButton1";
            this.SelectMonsterImportButton1.Size = new System.Drawing.Size(332, 23);
            this.SelectMonsterImportButton1.TabIndex = 32;
            this.SelectMonsterImportButton1.Text = "選択したモンスターをImport";
            this.SelectMonsterImportButton1.UseVisualStyleBackColor = true;
            this.SelectMonsterImportButton1.Click += new System.EventHandler(this.SelectMonsterImportButton1_Click);
            // 
            // Import_DataOpenButton1
            // 
            this.Import_DataOpenButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Import_DataOpenButton1.Location = new System.Drawing.Point(269, 49);
            this.Import_DataOpenButton1.Name = "Import_DataOpenButton1";
            this.Import_DataOpenButton1.Size = new System.Drawing.Size(75, 23);
            this.Import_DataOpenButton1.TabIndex = 29;
            this.Import_DataOpenButton1.Text = "開く";
            this.Import_DataOpenButton1.UseVisualStyleBackColor = true;
            this.Import_DataOpenButton1.Click += new System.EventHandler(this.Import_DataOpenButton1_Click);
            // 
            // Import_MonsterTreeView1
            // 
            this.Import_MonsterTreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Import_MonsterTreeView1.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Import_MonsterTreeView1.Location = new System.Drawing.Point(12, 87);
            this.Import_MonsterTreeView1.Name = "Import_MonsterTreeView1";
            this.Import_MonsterTreeView1.Size = new System.Drawing.Size(332, 344);
            this.Import_MonsterTreeView1.TabIndex = 28;
            // 
            // Import_SelectMonsterLabel1
            // 
            this.Import_SelectMonsterLabel1.AutoSize = true;
            this.Import_SelectMonsterLabel1.Location = new System.Drawing.Point(10, 72);
            this.Import_SelectMonsterLabel1.Name = "Import_SelectMonsterLabel1";
            this.Import_SelectMonsterLabel1.Size = new System.Drawing.Size(110, 12);
            this.Import_SelectMonsterLabel1.TabIndex = 27;
            this.Import_SelectMonsterLabel1.Text = "Importしたいモンスター";
            // 
            // Import_ReferenceButton1
            // 
            this.Import_ReferenceButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Import_ReferenceButton1.Location = new System.Drawing.Point(188, 49);
            this.Import_ReferenceButton1.Name = "Import_ReferenceButton1";
            this.Import_ReferenceButton1.Size = new System.Drawing.Size(75, 23);
            this.Import_ReferenceButton1.TabIndex = 26;
            this.Import_ReferenceButton1.Text = "参照";
            this.Import_ReferenceButton1.UseVisualStyleBackColor = true;
            this.Import_ReferenceButton1.Click += new System.EventHandler(this.Import_ReferenceButton1_Click);
            // 
            // Import_DataURLTextBox1
            // 
            this.Import_DataURLTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Import_DataURLTextBox1.Location = new System.Drawing.Point(12, 24);
            this.Import_DataURLTextBox1.Name = "Import_DataURLTextBox1";
            this.Import_DataURLTextBox1.Size = new System.Drawing.Size(332, 19);
            this.Import_DataURLTextBox1.TabIndex = 25;
            this.Import_DataURLTextBox1.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Import_DataLabel1
            // 
            this.Import_DataLabel1.AutoSize = true;
            this.Import_DataLabel1.Location = new System.Drawing.Point(12, 9);
            this.Import_DataLabel1.Name = "Import_DataLabel1";
            this.Import_DataLabel1.Size = new System.Drawing.Size(84, 12);
            this.Import_DataLabel1.TabIndex = 24;
            this.Import_DataLabel1.Text = "Importするデータ";
            // 
            // DataImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 467);
            this.Controls.Add(this.SelectMonsterImportButton1);
            this.Controls.Add(this.Import_DataOpenButton1);
            this.Controls.Add(this.Import_MonsterTreeView1);
            this.Controls.Add(this.Import_SelectMonsterLabel1);
            this.Controls.Add(this.Import_ReferenceButton1);
            this.Controls.Add(this.Import_DataURLTextBox1);
            this.Controls.Add(this.Import_DataLabel1);
            this.Name = "DataImport";
            this.Text = "Import";
            this.Deactivate += new System.EventHandler(this.DataImport_Deactivate);
            this.Load += new System.EventHandler(this.DataImport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectMonsterImportButton1;
        private System.Windows.Forms.Button Import_DataOpenButton1;
        public System.Windows.Forms.TreeView Import_MonsterTreeView1;
        private System.Windows.Forms.Label Import_SelectMonsterLabel1;
        private System.Windows.Forms.Button Import_ReferenceButton1;
        private System.Windows.Forms.TextBox Import_DataURLTextBox1;
        private System.Windows.Forms.Label Import_DataLabel1;
    }
}