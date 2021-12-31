namespace SW_MonsterTool
{
    partial class CcfoliaForm
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
            this.SelectMonsterView = new System.Windows.Forms.TreeView();
            this.CC_ExportMonsterNameLabel1 = new System.Windows.Forms.Label();
            this.U_ExportMonsterNamePanel1 = new System.Windows.Forms.Panel();
            this.RuleBookCombo1 = new System.Windows.Forms.ComboBox();
            this.RuleBookLabel1 = new System.Windows.Forms.Label();
            this.SelectRuleCheack1 = new System.Windows.Forms.CheckBox();
            this.Export_Life_ResistanceLabel1 = new System.Windows.Forms.Label();
            this.Export_Spirit_ResistanceLabel1 = new System.Windows.Forms.Label();
            this.Export_MonsterDataPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Export_Button1 = new System.Windows.Forms.Button();
            this.SeimeiTextBox = new System.Windows.Forms.TextBox();
            this.SeisinTextBox = new System.Windows.Forms.TextBox();
            this.Export_PartComboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SecretCheckBox = new System.Windows.Forms.CheckBox();
            this.InvisibleCheckBox = new System.Windows.Forms.CheckBox();
            this.HideStatusCheckBox = new System.Windows.Forms.CheckBox();
            this.U_ExportMonsterNamePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectMonsterView
            // 
            this.SelectMonsterView.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SelectMonsterView.Location = new System.Drawing.Point(12, 12);
            this.SelectMonsterView.Name = "SelectMonsterView";
            this.SelectMonsterView.Size = new System.Drawing.Size(214, 426);
            this.SelectMonsterView.TabIndex = 1;
            this.SelectMonsterView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            // 
            // CC_ExportMonsterNameLabel1
            // 
            this.CC_ExportMonsterNameLabel1.AutoSize = true;
            this.CC_ExportMonsterNameLabel1.BackColor = System.Drawing.Color.Black;
            this.CC_ExportMonsterNameLabel1.Font = new System.Drawing.Font("MS UI Gothic", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CC_ExportMonsterNameLabel1.ForeColor = System.Drawing.SystemColors.Window;
            this.CC_ExportMonsterNameLabel1.Location = new System.Drawing.Point(0, 0);
            this.CC_ExportMonsterNameLabel1.Name = "CC_ExportMonsterNameLabel1";
            this.CC_ExportMonsterNameLabel1.Size = new System.Drawing.Size(162, 23);
            this.CC_ExportMonsterNameLabel1.TabIndex = 7;
            this.CC_ExportMonsterNameLabel1.Text = "モンスターネーム";
            // 
            // U_ExportMonsterNamePanel1
            // 
            this.U_ExportMonsterNamePanel1.BackColor = System.Drawing.Color.Black;
            this.U_ExportMonsterNamePanel1.Controls.Add(this.CC_ExportMonsterNameLabel1);
            this.U_ExportMonsterNamePanel1.Location = new System.Drawing.Point(232, 12);
            this.U_ExportMonsterNamePanel1.Name = "U_ExportMonsterNamePanel1";
            this.U_ExportMonsterNamePanel1.Size = new System.Drawing.Size(343, 23);
            this.U_ExportMonsterNamePanel1.TabIndex = 11;
            // 
            // RuleBookCombo1
            // 
            this.RuleBookCombo1.FormattingEnabled = true;
            this.RuleBookCombo1.Items.AddRange(new object[] {
            "ソードワールド2.0",
            "ソードワールド2.5"});
            this.RuleBookCombo1.Location = new System.Drawing.Point(298, 47);
            this.RuleBookCombo1.Name = "RuleBookCombo1";
            this.RuleBookCombo1.Size = new System.Drawing.Size(142, 20);
            this.RuleBookCombo1.TabIndex = 13;
            this.RuleBookCombo1.Text = "ソードワールド2.0";
            this.RuleBookCombo1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // RuleBookLabel1
            // 
            this.RuleBookLabel1.AutoSize = true;
            this.RuleBookLabel1.Location = new System.Drawing.Point(233, 50);
            this.RuleBookLabel1.Name = "RuleBookLabel1";
            this.RuleBookLabel1.Size = new System.Drawing.Size(59, 12);
            this.RuleBookLabel1.TabIndex = 14;
            this.RuleBookLabel1.Text = "ルールブック";
            // 
            // SelectRuleCheack1
            // 
            this.SelectRuleCheack1.AutoSize = true;
            this.SelectRuleCheack1.Checked = true;
            this.SelectRuleCheack1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelectRuleCheack1.Location = new System.Drawing.Point(446, 49);
            this.SelectRuleCheack1.Name = "SelectRuleCheack1";
            this.SelectRuleCheack1.Size = new System.Drawing.Size(78, 16);
            this.SelectRuleCheack1.TabIndex = 15;
            this.SelectRuleCheack1.Text = "選択ルール";
            this.SelectRuleCheack1.UseVisualStyleBackColor = true;
            this.SelectRuleCheack1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Export_Life_ResistanceLabel1
            // 
            this.Export_Life_ResistanceLabel1.AutoSize = true;
            this.Export_Life_ResistanceLabel1.Location = new System.Drawing.Point(234, 73);
            this.Export_Life_ResistanceLabel1.Name = "Export_Life_ResistanceLabel1";
            this.Export_Life_ResistanceLabel1.Size = new System.Drawing.Size(65, 12);
            this.Export_Life_ResistanceLabel1.TabIndex = 22;
            this.Export_Life_ResistanceLabel1.Text = "生命抵抗力";
            // 
            // Export_Spirit_ResistanceLabel1
            // 
            this.Export_Spirit_ResistanceLabel1.AutoSize = true;
            this.Export_Spirit_ResistanceLabel1.Location = new System.Drawing.Point(400, 73);
            this.Export_Spirit_ResistanceLabel1.Name = "Export_Spirit_ResistanceLabel1";
            this.Export_Spirit_ResistanceLabel1.Size = new System.Drawing.Size(65, 12);
            this.Export_Spirit_ResistanceLabel1.TabIndex = 26;
            this.Export_Spirit_ResistanceLabel1.Text = "精神抵抗力";
            // 
            // Export_MonsterDataPanel1
            // 
            this.Export_MonsterDataPanel1.AutoScroll = true;
            this.Export_MonsterDataPanel1.Location = new System.Drawing.Point(232, 96);
            this.Export_MonsterDataPanel1.Name = "Export_MonsterDataPanel1";
            this.Export_MonsterDataPanel1.Size = new System.Drawing.Size(343, 266);
            this.Export_MonsterDataPanel1.TabIndex = 29;
            // 
            // Export_Button1
            // 
            this.Export_Button1.Location = new System.Drawing.Point(463, 415);
            this.Export_Button1.Name = "Export_Button1";
            this.Export_Button1.Size = new System.Drawing.Size(112, 23);
            this.Export_Button1.TabIndex = 30;
            this.Export_Button1.Text = "クリップボードにコピー";
            this.Export_Button1.UseVisualStyleBackColor = true;
            this.Export_Button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SeimeiTextBox
            // 
            this.SeimeiTextBox.Location = new System.Drawing.Point(298, 70);
            this.SeimeiTextBox.Name = "SeimeiTextBox";
            this.SeimeiTextBox.ReadOnly = true;
            this.SeimeiTextBox.Size = new System.Drawing.Size(96, 19);
            this.SeimeiTextBox.TabIndex = 31;
            // 
            // SeisinTextBox
            // 
            this.SeisinTextBox.Location = new System.Drawing.Point(471, 70);
            this.SeisinTextBox.Name = "SeisinTextBox";
            this.SeisinTextBox.ReadOnly = true;
            this.SeisinTextBox.Size = new System.Drawing.Size(96, 19);
            this.SeisinTextBox.TabIndex = 32;
            // 
            // Export_PartComboBox1
            // 
            this.Export_PartComboBox1.FormattingEnabled = true;
            this.Export_PartComboBox1.Location = new System.Drawing.Point(336, 417);
            this.Export_PartComboBox1.Name = "Export_PartComboBox1";
            this.Export_PartComboBox1.Size = new System.Drawing.Size(121, 20);
            this.Export_PartComboBox1.TabIndex = 33;
            this.Export_PartComboBox1.Text = "全ての部位";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "出力する部位";
            // 
            // SecretCheckBox
            // 
            this.SecretCheckBox.AutoSize = true;
            this.SecretCheckBox.Location = new System.Drawing.Point(236, 369);
            this.SecretCheckBox.Name = "SecretCheckBox";
            this.SecretCheckBox.Size = new System.Drawing.Size(142, 16);
            this.SecretCheckBox.TabIndex = 35;
            this.SecretCheckBox.Text = "ステータスを非公開にする";
            this.SecretCheckBox.UseVisualStyleBackColor = true;
            // 
            // InvisibleCheckBox
            // 
            this.InvisibleCheckBox.AutoSize = true;
            this.InvisibleCheckBox.Location = new System.Drawing.Point(384, 368);
            this.InvisibleCheckBox.Name = "InvisibleCheckBox";
            this.InvisibleCheckBox.Size = new System.Drawing.Size(174, 16);
            this.InvisibleCheckBox.TabIndex = 36;
            this.InvisibleCheckBox.Text = "発言時キャラクターを表示しない";
            this.InvisibleCheckBox.UseVisualStyleBackColor = true;
            // 
            // HideStatusCheckBox
            // 
            this.HideStatusCheckBox.AutoSize = true;
            this.HideStatusCheckBox.Location = new System.Drawing.Point(236, 391);
            this.HideStatusCheckBox.Name = "HideStatusCheckBox";
            this.HideStatusCheckBox.Size = new System.Drawing.Size(186, 16);
            this.HideStatusCheckBox.TabIndex = 37;
            this.HideStatusCheckBox.Text = "盤面キャラクター一覧に表示しない";
            this.HideStatusCheckBox.UseVisualStyleBackColor = true;
            // 
            // CcfoliaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 450);
            this.Controls.Add(this.HideStatusCheckBox);
            this.Controls.Add(this.InvisibleCheckBox);
            this.Controls.Add(this.SecretCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Export_PartComboBox1);
            this.Controls.Add(this.SeisinTextBox);
            this.Controls.Add(this.SeimeiTextBox);
            this.Controls.Add(this.Export_Button1);
            this.Controls.Add(this.Export_MonsterDataPanel1);
            this.Controls.Add(this.Export_Spirit_ResistanceLabel1);
            this.Controls.Add(this.Export_Life_ResistanceLabel1);
            this.Controls.Add(this.SelectRuleCheack1);
            this.Controls.Add(this.RuleBookLabel1);
            this.Controls.Add(this.RuleBookCombo1);
            this.Controls.Add(this.U_ExportMonsterNamePanel1);
            this.Controls.Add(this.SelectMonsterView);
            this.Name = "CcfoliaForm";
            this.Text = "出力";
            this.U_ExportMonsterNamePanel1.ResumeLayout(false);
            this.U_ExportMonsterNamePanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TreeView SelectMonsterView;
        private System.Windows.Forms.Label CC_ExportMonsterNameLabel1;
        private System.Windows.Forms.Panel U_ExportMonsterNamePanel1;
        private System.Windows.Forms.ComboBox RuleBookCombo1;
        private System.Windows.Forms.Label RuleBookLabel1;
        private System.Windows.Forms.CheckBox SelectRuleCheack1;
        private System.Windows.Forms.Label Export_Life_ResistanceLabel1;
        private System.Windows.Forms.Label Export_Spirit_ResistanceLabel1;
        private System.Windows.Forms.FlowLayoutPanel Export_MonsterDataPanel1;
        private System.Windows.Forms.Button Export_Button1;
        private System.Windows.Forms.TextBox SeimeiTextBox;
        private System.Windows.Forms.TextBox SeisinTextBox;
        private System.Windows.Forms.ComboBox Export_PartComboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox SecretCheckBox;
        private System.Windows.Forms.CheckBox InvisibleCheckBox;
        private System.Windows.Forms.CheckBox HideStatusCheckBox;
    }
}