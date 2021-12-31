
namespace SW_MonsterTool
{
    partial class ChatPaletteEdit
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
            this.DicePaletteLabel1 = new System.Windows.Forms.Label();
            this.FixedPaletteTextBox = new System.Windows.Forms.RichTextBox();
            this.ChatPalette_SaveButton1 = new System.Windows.Forms.Button();
            this.FixedLabel1 = new System.Windows.Forms.Label();
            this.DicePaletteTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DicePaletteLabel1
            // 
            this.DicePaletteLabel1.AutoSize = true;
            this.DicePaletteLabel1.Location = new System.Drawing.Point(61, 28);
            this.DicePaletteLabel1.Name = "DicePaletteLabel1";
            this.DicePaletteLabel1.Size = new System.Drawing.Size(96, 12);
            this.DicePaletteLabel1.TabIndex = 11;
            this.DicePaletteLabel1.Text = "ダイスを振るパレット";
            // 
            // FixedPaletteTextBox
            // 
            this.FixedPaletteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FixedPaletteTextBox.Location = new System.Drawing.Point(225, 43);
            this.FixedPaletteTextBox.Name = "FixedPaletteTextBox";
            this.FixedPaletteTextBox.Size = new System.Drawing.Size(200, 320);
            this.FixedPaletteTextBox.TabIndex = 10;
            this.FixedPaletteTextBox.Text = "";
            this.FixedPaletteTextBox.WordWrap = false;
            // 
            // ChatPalette_SaveButton1
            // 
            this.ChatPalette_SaveButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatPalette_SaveButton1.Location = new System.Drawing.Point(12, 372);
            this.ChatPalette_SaveButton1.Name = "ChatPalette_SaveButton1";
            this.ChatPalette_SaveButton1.Size = new System.Drawing.Size(413, 23);
            this.ChatPalette_SaveButton1.TabIndex = 12;
            this.ChatPalette_SaveButton1.Text = "保存";
            this.ChatPalette_SaveButton1.UseVisualStyleBackColor = true;
            this.ChatPalette_SaveButton1.Click += new System.EventHandler(this.ChatPalette_SaveButton1_Click);
            // 
            // FixedLabel1
            // 
            this.FixedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FixedLabel1.AutoSize = true;
            this.FixedLabel1.Location = new System.Drawing.Point(295, 28);
            this.FixedLabel1.Name = "FixedLabel1";
            this.FixedLabel1.Size = new System.Drawing.Size(75, 12);
            this.FixedLabel1.TabIndex = 14;
            this.FixedLabel1.Text = "固定値パレット";
            // 
            // DicePaletteTextBox
            // 
            this.DicePaletteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DicePaletteTextBox.Location = new System.Drawing.Point(12, 43);
            this.DicePaletteTextBox.Name = "DicePaletteTextBox";
            this.DicePaletteTextBox.Size = new System.Drawing.Size(200, 320);
            this.DicePaletteTextBox.TabIndex = 13;
            this.DicePaletteTextBox.Text = "";
            this.DicePaletteTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "ステータス、精神/生命抵抗は出力時に自動で記入されます";
            // 
            // ChatPaletteEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 407);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FixedLabel1);
            this.Controls.Add(this.DicePaletteTextBox);
            this.Controls.Add(this.ChatPalette_SaveButton1);
            this.Controls.Add(this.DicePaletteLabel1);
            this.Controls.Add(this.FixedPaletteTextBox);
            this.Name = "ChatPaletteEdit";
            this.Text = "ChatPaletteEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DicePaletteLabel1;
        private System.Windows.Forms.RichTextBox FixedPaletteTextBox;
        private System.Windows.Forms.Button ChatPalette_SaveButton1;
        private System.Windows.Forms.Label FixedLabel1;
        private System.Windows.Forms.RichTextBox DicePaletteTextBox;
        private System.Windows.Forms.Label label1;
    }
}