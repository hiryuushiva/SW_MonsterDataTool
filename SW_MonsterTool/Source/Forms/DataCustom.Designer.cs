namespace SW_MonsterTool
{
    partial class DataCustom
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
            this.CustomMonsterLabel1 = new System.Windows.Forms.Label();
            this.Select_DataButton1 = new System.Windows.Forms.Button();
            this.Select_DataTextBox1 = new System.Windows.Forms.TextBox();
            this.CustomMonsterLabel2 = new System.Windows.Forms.Label();
            this.Select_DataTreeView1 = new System.Windows.Forms.TreeView();
            this.DataOpenButton1 = new System.Windows.Forms.Button();
            this.Custom_SelectMonsterButton1 = new System.Windows.Forms.Button();
            this.Delete_SelectMonsterButton1 = new System.Windows.Forms.Button();
            this.Save_CustomDataButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CustomMonsterLabel1
            // 
            this.CustomMonsterLabel1.AutoSize = true;
            this.CustomMonsterLabel1.Location = new System.Drawing.Point(13, 13);
            this.CustomMonsterLabel1.Name = "CustomMonsterLabel1";
            this.CustomMonsterLabel1.Size = new System.Drawing.Size(76, 12);
            this.CustomMonsterLabel1.TabIndex = 0;
            this.CustomMonsterLabel1.Text = "編集するデータ";
            // 
            // Select_DataButton1
            // 
            this.Select_DataButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Select_DataButton1.Location = new System.Drawing.Point(156, 53);
            this.Select_DataButton1.Name = "Select_DataButton1";
            this.Select_DataButton1.Size = new System.Drawing.Size(75, 23);
            this.Select_DataButton1.TabIndex = 17;
            this.Select_DataButton1.Text = "参照";
            this.Select_DataButton1.UseVisualStyleBackColor = true;
            this.Select_DataButton1.Click += new System.EventHandler(this.button5_Click);
            // 
            // Select_DataTextBox1
            // 
            this.Select_DataTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Select_DataTextBox1.Location = new System.Drawing.Point(12, 28);
            this.Select_DataTextBox1.Name = "Select_DataTextBox1";
            this.Select_DataTextBox1.Size = new System.Drawing.Size(300, 19);
            this.Select_DataTextBox1.TabIndex = 16;
            this.Select_DataTextBox1.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // CustomMonsterLabel2
            // 
            this.CustomMonsterLabel2.AutoSize = true;
            this.CustomMonsterLabel2.Location = new System.Drawing.Point(13, 76);
            this.CustomMonsterLabel2.Name = "CustomMonsterLabel2";
            this.CustomMonsterLabel2.Size = new System.Drawing.Size(102, 12);
            this.CustomMonsterLabel2.TabIndex = 18;
            this.CustomMonsterLabel2.Text = "編集したいモンスター";
            // 
            // Select_DataTreeView1
            // 
            this.Select_DataTreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Select_DataTreeView1.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Select_DataTreeView1.Location = new System.Drawing.Point(12, 91);
            this.Select_DataTreeView1.Name = "Select_DataTreeView1";
            this.Select_DataTreeView1.Size = new System.Drawing.Size(300, 278);
            this.Select_DataTreeView1.TabIndex = 19;
            this.Select_DataTreeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            // 
            // DataOpenButton1
            // 
            this.DataOpenButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataOpenButton1.Location = new System.Drawing.Point(237, 53);
            this.DataOpenButton1.Name = "DataOpenButton1";
            this.DataOpenButton1.Size = new System.Drawing.Size(75, 23);
            this.DataOpenButton1.TabIndex = 20;
            this.DataOpenButton1.Text = "開く";
            this.DataOpenButton1.UseVisualStyleBackColor = true;
            this.DataOpenButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Custom_SelectMonsterButton1
            // 
            this.Custom_SelectMonsterButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Custom_SelectMonsterButton1.Location = new System.Drawing.Point(12, 375);
            this.Custom_SelectMonsterButton1.Name = "Custom_SelectMonsterButton1";
            this.Custom_SelectMonsterButton1.Size = new System.Drawing.Size(300, 23);
            this.Custom_SelectMonsterButton1.TabIndex = 21;
            this.Custom_SelectMonsterButton1.Text = "選択したモンスターを編集";
            this.Custom_SelectMonsterButton1.UseVisualStyleBackColor = true;
            this.Custom_SelectMonsterButton1.Click += new System.EventHandler(this.button2_Click);
            // 
            // Delete_SelectMonsterButton1
            // 
            this.Delete_SelectMonsterButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Delete_SelectMonsterButton1.Location = new System.Drawing.Point(12, 404);
            this.Delete_SelectMonsterButton1.Name = "Delete_SelectMonsterButton1";
            this.Delete_SelectMonsterButton1.Size = new System.Drawing.Size(300, 23);
            this.Delete_SelectMonsterButton1.TabIndex = 22;
            this.Delete_SelectMonsterButton1.Text = "選択したモンスターを削除";
            this.Delete_SelectMonsterButton1.UseVisualStyleBackColor = true;
            this.Delete_SelectMonsterButton1.Click += new System.EventHandler(this.button3_Click);
            // 
            // Save_CustomDataButton1
            // 
            this.Save_CustomDataButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Save_CustomDataButton1.Location = new System.Drawing.Point(12, 433);
            this.Save_CustomDataButton1.Name = "Save_CustomDataButton1";
            this.Save_CustomDataButton1.Size = new System.Drawing.Size(300, 23);
            this.Save_CustomDataButton1.TabIndex = 23;
            this.Save_CustomDataButton1.Text = "編集したデータを保存";
            this.Save_CustomDataButton1.UseVisualStyleBackColor = true;
            this.Save_CustomDataButton1.Click += new System.EventHandler(this.button4_Click);
            // 
            // DataCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 468);
            this.ControlBox = false;
            this.Controls.Add(this.Save_CustomDataButton1);
            this.Controls.Add(this.Delete_SelectMonsterButton1);
            this.Controls.Add(this.Custom_SelectMonsterButton1);
            this.Controls.Add(this.DataOpenButton1);
            this.Controls.Add(this.Select_DataTreeView1);
            this.Controls.Add(this.CustomMonsterLabel2);
            this.Controls.Add(this.Select_DataButton1);
            this.Controls.Add(this.Select_DataTextBox1);
            this.Controls.Add(this.CustomMonsterLabel1);
            this.Name = "DataCustom";
            this.Text = "データ編集";
            this.Activated += new System.EventHandler(this.DataCustom_Activated);
            this.Load += new System.EventHandler(this.DataCustom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CustomMonsterLabel1;
        private System.Windows.Forms.Button Select_DataButton1;
        private System.Windows.Forms.TextBox Select_DataTextBox1;
        private System.Windows.Forms.Label CustomMonsterLabel2;
        public System.Windows.Forms.TreeView Select_DataTreeView1;
        private System.Windows.Forms.Button DataOpenButton1;
        private System.Windows.Forms.Button Custom_SelectMonsterButton1;
        private System.Windows.Forms.Button Delete_SelectMonsterButton1;
        private System.Windows.Forms.Button Save_CustomDataButton1;
    }
}