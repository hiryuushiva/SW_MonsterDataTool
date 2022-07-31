namespace SW_MonsterTool
{
    partial class ConfigForm
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
            this.MonsterCSVSpreadSheetLabel1 = new System.Windows.Forms.Label();
            this.MonsterCSVSpreadSheetTextBox1 = new System.Windows.Forms.TextBox();
            this.MonsterCSVLabel1 = new System.Windows.Forms.Label();
            this.MCSVList_AddButton1 = new System.Windows.Forms.Button();
            this.ConfigCSV_ReferenceButton1 = new System.Windows.Forms.Button();
            this.ConfigCSVSpreadSheetTextBox1 = new System.Windows.Forms.TextBox();
            this.ConfigCSVLabel1 = new System.Windows.Forms.Label();
            this.ConfigCSVTextBox1 = new System.Windows.Forms.TextBox();
            this.ConfigCSVSpreadSheetLabel1 = new System.Windows.Forms.Label();
            this.Config_SaveButton1 = new System.Windows.Forms.Button();
            this.MCSVList_DeleteButton1 = new System.Windows.Forms.Button();
            this.OutPutCSVTextBox1 = new System.Windows.Forms.TextBox();
            this.OutputCSVLabel1 = new System.Windows.Forms.Label();
            this.OutPutCSV_ReferenceButton1 = new System.Windows.Forms.Button();
            this.MonsterCSVListView1 = new SW_MonsterTool.Source.MyListView.ListViewEx();
            this.SuspendLayout();
            // 
            // MonsterCSVSpreadSheetLabel1
            // 
            this.MonsterCSVSpreadSheetLabel1.AutoSize = true;
            this.MonsterCSVSpreadSheetLabel1.Location = new System.Drawing.Point(13, 13);
            this.MonsterCSVSpreadSheetLabel1.Name = "MonsterCSVSpreadSheetLabel1";
            this.MonsterCSVSpreadSheetLabel1.Size = new System.Drawing.Size(123, 12);
            this.MonsterCSVSpreadSheetLabel1.TabIndex = 0;
            this.MonsterCSVSpreadSheetLabel1.Text = "魔物CSVスプレッドシート";
            // 
            // MonsterCSVSpreadSheetTextBox1
            // 
            this.MonsterCSVSpreadSheetTextBox1.Enabled = false;
            this.MonsterCSVSpreadSheetTextBox1.Location = new System.Drawing.Point(13, 28);
            this.MonsterCSVSpreadSheetTextBox1.Name = "MonsterCSVSpreadSheetTextBox1";
            this.MonsterCSVSpreadSheetTextBox1.Size = new System.Drawing.Size(408, 19);
            this.MonsterCSVSpreadSheetTextBox1.TabIndex = 1;
            this.MonsterCSVSpreadSheetTextBox1.TextChanged += new System.EventHandler(this.MonsterCSVSpreadSheetTextBox1_TextChanged);
            // 
            // MonsterCSVLabel1
            // 
            this.MonsterCSVLabel1.AutoSize = true;
            this.MonsterCSVLabel1.Location = new System.Drawing.Point(10, 111);
            this.MonsterCSVLabel1.Name = "MonsterCSVLabel1";
            this.MonsterCSVLabel1.Size = new System.Drawing.Size(108, 12);
            this.MonsterCSVLabel1.TabIndex = 2;
            this.MonsterCSVLabel1.Text = "読み込む魔物ファイル";
            // 
            // MCSVList_AddButton1
            // 
            this.MCSVList_AddButton1.Location = new System.Drawing.Point(344, 261);
            this.MCSVList_AddButton1.Name = "MCSVList_AddButton1";
            this.MCSVList_AddButton1.Size = new System.Drawing.Size(75, 23);
            this.MCSVList_AddButton1.TabIndex = 4;
            this.MCSVList_AddButton1.Text = "追加";
            this.MCSVList_AddButton1.UseVisualStyleBackColor = true;
            this.MCSVList_AddButton1.Click += new System.EventHandler(this.MCSVList_AddButton1_Click);
            // 
            // ConfigCSV_ReferenceButton1
            // 
            this.ConfigCSV_ReferenceButton1.Location = new System.Drawing.Point(344, 376);
            this.ConfigCSV_ReferenceButton1.Name = "ConfigCSV_ReferenceButton1";
            this.ConfigCSV_ReferenceButton1.Size = new System.Drawing.Size(75, 23);
            this.ConfigCSV_ReferenceButton1.TabIndex = 9;
            this.ConfigCSV_ReferenceButton1.Text = "参照";
            this.ConfigCSV_ReferenceButton1.UseVisualStyleBackColor = true;
            this.ConfigCSV_ReferenceButton1.Click += new System.EventHandler(this.ConfigCSV_ReferenceButton1_Click);
            // 
            // ConfigCSVSpreadSheetTextBox1
            // 
            this.ConfigCSVSpreadSheetTextBox1.Enabled = false;
            this.ConfigCSVSpreadSheetTextBox1.Location = new System.Drawing.Point(12, 309);
            this.ConfigCSVSpreadSheetTextBox1.Name = "ConfigCSVSpreadSheetTextBox1";
            this.ConfigCSVSpreadSheetTextBox1.Size = new System.Drawing.Size(408, 19);
            this.ConfigCSVSpreadSheetTextBox1.TabIndex = 8;
            this.ConfigCSVSpreadSheetTextBox1.TextChanged += new System.EventHandler(this.ConfigCSVSpreadSheetTextBox1_TextChanged);
            // 
            // ConfigCSVLabel1
            // 
            this.ConfigCSVLabel1.AutoSize = true;
            this.ConfigCSVLabel1.Location = new System.Drawing.Point(12, 336);
            this.ConfigCSVLabel1.Name = "ConfigCSVLabel1";
            this.ConfigCSVLabel1.Size = new System.Drawing.Size(128, 12);
            this.ConfigCSVLabel1.TabIndex = 7;
            this.ConfigCSVLabel1.Text = "読み込むConfigCSVURL";
            // 
            // ConfigCSVTextBox1
            // 
            this.ConfigCSVTextBox1.Location = new System.Drawing.Point(12, 351);
            this.ConfigCSVTextBox1.Name = "ConfigCSVTextBox1";
            this.ConfigCSVTextBox1.Size = new System.Drawing.Size(408, 19);
            this.ConfigCSVTextBox1.TabIndex = 6;
            this.ConfigCSVTextBox1.TextChanged += new System.EventHandler(this.ConfigCSVTextBox1_TextChanged);
            // 
            // ConfigCSVSpreadSheetLabel1
            // 
            this.ConfigCSVSpreadSheetLabel1.AutoSize = true;
            this.ConfigCSVSpreadSheetLabel1.Location = new System.Drawing.Point(12, 294);
            this.ConfigCSVSpreadSheetLabel1.Name = "ConfigCSVSpreadSheetLabel1";
            this.ConfigCSVSpreadSheetLabel1.Size = new System.Drawing.Size(132, 12);
            this.ConfigCSVSpreadSheetLabel1.TabIndex = 5;
            this.ConfigCSVSpreadSheetLabel1.Text = "ConfigCSVスプレッドシート";
            // 
            // Config_SaveButton1
            // 
            this.Config_SaveButton1.Location = new System.Drawing.Point(12, 402);
            this.Config_SaveButton1.Name = "Config_SaveButton1";
            this.Config_SaveButton1.Size = new System.Drawing.Size(407, 23);
            this.Config_SaveButton1.TabIndex = 10;
            this.Config_SaveButton1.Text = "変更を保存";
            this.Config_SaveButton1.UseVisualStyleBackColor = true;
            this.Config_SaveButton1.Click += new System.EventHandler(this.Config_SaveButton1_Click);
            // 
            // MCSVList_DeleteButton1
            // 
            this.MCSVList_DeleteButton1.Location = new System.Drawing.Point(263, 261);
            this.MCSVList_DeleteButton1.Name = "MCSVList_DeleteButton1";
            this.MCSVList_DeleteButton1.Size = new System.Drawing.Size(75, 23);
            this.MCSVList_DeleteButton1.TabIndex = 12;
            this.MCSVList_DeleteButton1.Text = "削除";
            this.MCSVList_DeleteButton1.UseVisualStyleBackColor = true;
            this.MCSVList_DeleteButton1.Click += new System.EventHandler(this.MCSVList_DeleteButton1_Click);
            // 
            // OutPutCSVTextBox1
            // 
            this.OutPutCSVTextBox1.Location = new System.Drawing.Point(13, 67);
            this.OutPutCSVTextBox1.Name = "OutPutCSVTextBox1";
            this.OutPutCSVTextBox1.Size = new System.Drawing.Size(408, 19);
            this.OutPutCSVTextBox1.TabIndex = 14;
            this.OutPutCSVTextBox1.TextChanged += new System.EventHandler(this.OutPutCSVTextBox1_TextChanged_1);
            // 
            // OutputCSVLabel1
            // 
            this.OutputCSVLabel1.AutoSize = true;
            this.OutputCSVLabel1.Location = new System.Drawing.Point(13, 51);
            this.OutputCSVLabel1.Name = "OutputCSVLabel1";
            this.OutputCSVLabel1.Size = new System.Drawing.Size(75, 12);
            this.OutputCSVLabel1.TabIndex = 13;
            this.OutputCSVLabel1.Text = "ファイル出力先";
            // 
            // OutPutCSV_ReferenceButton1
            // 
            this.OutPutCSV_ReferenceButton1.Location = new System.Drawing.Point(346, 92);
            this.OutPutCSV_ReferenceButton1.Name = "OutPutCSV_ReferenceButton1";
            this.OutPutCSV_ReferenceButton1.Size = new System.Drawing.Size(75, 23);
            this.OutPutCSV_ReferenceButton1.TabIndex = 15;
            this.OutPutCSV_ReferenceButton1.Text = "参照";
            this.OutPutCSV_ReferenceButton1.UseVisualStyleBackColor = true;
            this.OutPutCSV_ReferenceButton1.Click += new System.EventHandler(this.OutPutCSV_ReferenceButton1_Click);
            // 
            // MonsterCSVListView1
            // 
            this.MonsterCSVListView1.CurrentColumn = null;
            this.MonsterCSVListView1.CurrentRow = null;
            this.MonsterCSVListView1.FullRowSelect = true;
            this.MonsterCSVListView1.HideSelection = false;
            this.MonsterCSVListView1.Location = new System.Drawing.Point(12, 126);
            this.MonsterCSVListView1.Name = "MonsterCSVListView1";
            this.MonsterCSVListView1.Size = new System.Drawing.Size(408, 129);
            this.MonsterCSVListView1.TabIndex = 11;
            this.MonsterCSVListView1.UseCompatibleStateImageBehavior = false;
            this.MonsterCSVListView1.View = System.Windows.Forms.View.Details;
            this.MonsterCSVListView1.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.MonsterCSVListView1_ColumnWidthChanging);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 441);
            this.Controls.Add(this.OutPutCSV_ReferenceButton1);
            this.Controls.Add(this.OutPutCSVTextBox1);
            this.Controls.Add(this.OutputCSVLabel1);
            this.Controls.Add(this.MCSVList_DeleteButton1);
            this.Controls.Add(this.MonsterCSVListView1);
            this.Controls.Add(this.Config_SaveButton1);
            this.Controls.Add(this.ConfigCSV_ReferenceButton1);
            this.Controls.Add(this.ConfigCSVSpreadSheetTextBox1);
            this.Controls.Add(this.ConfigCSVLabel1);
            this.Controls.Add(this.ConfigCSVTextBox1);
            this.Controls.Add(this.ConfigCSVSpreadSheetLabel1);
            this.Controls.Add(this.MCSVList_AddButton1);
            this.Controls.Add(this.MonsterCSVLabel1);
            this.Controls.Add(this.MonsterCSVSpreadSheetTextBox1);
            this.Controls.Add(this.MonsterCSVSpreadSheetLabel1);
            this.Name = "ConfigForm";
            this.Text = "設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MonsterCSVSpreadSheetLabel1;
        private System.Windows.Forms.TextBox MonsterCSVSpreadSheetTextBox1;
        private System.Windows.Forms.Label MonsterCSVLabel1;
        private System.Windows.Forms.Button MCSVList_AddButton1;
        private System.Windows.Forms.Button ConfigCSV_ReferenceButton1;
        private System.Windows.Forms.TextBox ConfigCSVSpreadSheetTextBox1;
        private System.Windows.Forms.Label ConfigCSVLabel1;
        private System.Windows.Forms.TextBox ConfigCSVTextBox1;
        private System.Windows.Forms.Label ConfigCSVSpreadSheetLabel1;
        private System.Windows.Forms.Button Config_SaveButton1;
        private Source.MyListView.ListViewEx MonsterCSVListView1;
        private System.Windows.Forms.Button MCSVList_DeleteButton1;
        private System.Windows.Forms.TextBox OutPutCSVTextBox1;
        private System.Windows.Forms.Label OutputCSVLabel1;
        private System.Windows.Forms.Button OutPutCSV_ReferenceButton1;
    }
}