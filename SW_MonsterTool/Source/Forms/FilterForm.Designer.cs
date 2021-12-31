namespace SW_MonsterTool
{
    partial class FilterForm
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.Select_ClearButton1 = new System.Windows.Forms.Button();
            this.OK_Button1 = new System.Windows.Forms.Button();
            this.Cancel_Button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(380, 457);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(411, 483);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(403, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "規定値1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.flowLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(403, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "規定値2";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(380, 457);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage4.Controls.Add(this.flowLayoutPanel3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(403, 457);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "自由値";
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(380, 457);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.Select_ClearButton1);
            this.tabPage3.Controls.Add(this.OK_Button1);
            this.tabPage3.Controls.Add(this.Cancel_Button1);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(403, 457);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "設定適用";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "該当する物がない場合デフォルト設定になります";
            // 
            // Select_ClearButton1
            // 
            this.Select_ClearButton1.BackColor = System.Drawing.Color.IndianRed;
            this.Select_ClearButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Select_ClearButton1.Location = new System.Drawing.Point(11, 127);
            this.Select_ClearButton1.Name = "Select_ClearButton1";
            this.Select_ClearButton1.Size = new System.Drawing.Size(185, 23);
            this.Select_ClearButton1.TabIndex = 4;
            this.Select_ClearButton1.Text = "選択クリア";
            this.Select_ClearButton1.UseVisualStyleBackColor = false;
            this.Select_ClearButton1.Click += new System.EventHandler(this.button3_Click);
            // 
            // OK_Button1
            // 
            this.OK_Button1.Location = new System.Drawing.Point(207, 98);
            this.OK_Button1.Name = "OK_Button1";
            this.OK_Button1.Size = new System.Drawing.Size(185, 23);
            this.OK_Button1.TabIndex = 3;
            this.OK_Button1.Text = "選択完了";
            this.OK_Button1.UseVisualStyleBackColor = true;
            this.OK_Button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // Cancel_Button1
            // 
            this.Cancel_Button1.Location = new System.Drawing.Point(11, 98);
            this.Cancel_Button1.Name = "Cancel_Button1";
            this.Cancel_Button1.Size = new System.Drawing.Size(185, 23);
            this.Cancel_Button1.TabIndex = 2;
            this.Cancel_Button1.Text = "キャンセル";
            this.Cancel_Button1.UseVisualStyleBackColor = true;
            this.Cancel_Button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(337, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "-21にすると数値がなし、例外設定になっているモンスターを取得します。";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "自由値の数値は-22がデフォルト値です。";
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 481);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FilterForm";
            this.Text = "絞り込み";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Select_ClearButton1;
        private System.Windows.Forms.Button OK_Button1;
        private System.Windows.Forms.Button Cancel_Button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}