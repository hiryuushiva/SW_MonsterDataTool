namespace SW_MonsterTool
{
    partial class SortForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.FirstConditionPage = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SecondConditionPage = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.Sort_CancelButton1 = new System.Windows.Forms.Button();
            this.Sort_OKButton1 = new System.Windows.Forms.Button();
            this.Sort_ResetButton1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.FirstConditionPage.SuspendLayout();
            this.SecondConditionPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.FirstConditionPage);
            this.tabControl1.Controls.Add(this.SecondConditionPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(372, 218);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // FirstConditionPage
            // 
            this.FirstConditionPage.Controls.Add(this.flowLayoutPanel1);
            this.FirstConditionPage.Location = new System.Drawing.Point(4, 22);
            this.FirstConditionPage.Name = "FirstConditionPage";
            this.FirstConditionPage.Padding = new System.Windows.Forms.Padding(3);
            this.FirstConditionPage.Size = new System.Drawing.Size(364, 192);
            this.FirstConditionPage.TabIndex = 0;
            this.FirstConditionPage.Text = "第1条件";
            this.FirstConditionPage.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(352, 180);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // SecondConditionPage
            // 
            this.SecondConditionPage.Controls.Add(this.flowLayoutPanel2);
            this.SecondConditionPage.Location = new System.Drawing.Point(4, 22);
            this.SecondConditionPage.Name = "SecondConditionPage";
            this.SecondConditionPage.Padding = new System.Windows.Forms.Padding(3);
            this.SecondConditionPage.Size = new System.Drawing.Size(364, 192);
            this.SecondConditionPage.TabIndex = 1;
            this.SecondConditionPage.Text = "第2条件";
            this.SecondConditionPage.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 6);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(352, 180);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // Sort_CancelButton1
            // 
            this.Sort_CancelButton1.Location = new System.Drawing.Point(10, 253);
            this.Sort_CancelButton1.Name = "Sort_CancelButton1";
            this.Sort_CancelButton1.Size = new System.Drawing.Size(174, 23);
            this.Sort_CancelButton1.TabIndex = 1;
            this.Sort_CancelButton1.Text = "キャンセル";
            this.Sort_CancelButton1.UseVisualStyleBackColor = true;
            this.Sort_CancelButton1.Click += new System.EventHandler(this.CanselButton1_Click);
            // 
            // Sort_OKButton1
            // 
            this.Sort_OKButton1.Location = new System.Drawing.Point(190, 253);
            this.Sort_OKButton1.Name = "Sort_OKButton1";
            this.Sort_OKButton1.Size = new System.Drawing.Size(174, 23);
            this.Sort_OKButton1.TabIndex = 2;
            this.Sort_OKButton1.Text = "選択完了";
            this.Sort_OKButton1.UseVisualStyleBackColor = true;
            this.Sort_OKButton1.Click += new System.EventHandler(this.Sort_OKButton1_Click);
            // 
            // Sort_ResetButton1
            // 
            this.Sort_ResetButton1.Location = new System.Drawing.Point(10, 224);
            this.Sort_ResetButton1.Name = "Sort_ResetButton1";
            this.Sort_ResetButton1.Size = new System.Drawing.Size(174, 23);
            this.Sort_ResetButton1.TabIndex = 3;
            this.Sort_ResetButton1.Text = "全てリセットする";
            this.Sort_ResetButton1.UseVisualStyleBackColor = true;
            this.Sort_ResetButton1.Click += new System.EventHandler(this.AllResetButton1_Click);
            // 
            // SortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 287);
            this.Controls.Add(this.Sort_ResetButton1);
            this.Controls.Add(this.Sort_OKButton1);
            this.Controls.Add(this.Sort_CancelButton1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SortForm";
            this.Text = "並び替え";
            this.tabControl1.ResumeLayout(false);
            this.FirstConditionPage.ResumeLayout(false);
            this.SecondConditionPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage FirstConditionPage;
        private System.Windows.Forms.TabPage SecondConditionPage;
        private System.Windows.Forms.Button Sort_CancelButton1;
        private System.Windows.Forms.Button Sort_OKButton1;
        private System.Windows.Forms.Button Sort_ResetButton1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}