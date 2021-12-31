using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SW_MonsterTool
{
    public partial class FilterForm : Form
    {

        int m_FlowHeight = 0;
        int m_Flow2Height = 0;
        int m_Flow3Height = 0;

        MainForm m_Parent;

        bool m_Init = false;

        List<string> m_IntelligenceCheck;
        List<string> m_PerceptionCheck;
        List<string> m_ReactionCheck;
        List<string> m_MoveCheck;
        List<string> m_CategoryCheck;
        List<string> m_Weak_PointCheck;
        List<string> m_DeclarationCheck;
        List<string> m_LangageCheck;
        List<string> m_ReferenceCheck;

        List<string> m_MonsterNameCheck;
        List<string> m_HabitatCheck;
        List<string> m_PartCheck;
        List<string> m_SpeNameCheck;
        List<string> m_DescriptionCheck;
        List<string> m_BootyCheck;
        List<string> m_MaterialCheck;

        List<string> m_MonsterNumCheck;
        List<string> m_StatusCheck;
        string m_GamelCheck;


        //あんま意味なかったが戻す必要もないので
        Panel m_Top;
        Panel m_ComponentPanel;
        Panel m_NamePanel;
        Label m_Name;


        public FilterForm(MainForm form)
        {
            m_IntelligenceCheck = new List<string>();
            m_PerceptionCheck = new List<string>();
            m_ReactionCheck = new List<string>();
            m_MoveCheck = new List<string>();
            m_CategoryCheck = new List<string>();
            m_Weak_PointCheck = new List<string>();
            m_DeclarationCheck = new List<string>();
            m_LangageCheck = new List<string>();
            m_ReferenceCheck = new List<string>();

            m_MonsterNameCheck = new List<string>();
            m_HabitatCheck = new List<string>();
            m_PartCheck = new List<string>();
            m_SpeNameCheck = new List<string>();
            m_DescriptionCheck = new List<string>();
            m_BootyCheck = new List<string>();
            m_MaterialCheck = new List<string>();

            m_MonsterNumCheck = new List<string>();
            m_StatusCheck = new List<string>();
            m_GamelCheck = "-22";


            m_Parent = form;
            InitializeComponent();

            timer1.Enabled = false;
            this.MaximizeBox = false;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SetPanel("分類", MainForm.m_MonsterUI.m_CategoryList,flowLayoutPanel1,ref m_FlowHeight);
            SetPanel("知能", MainForm.m_MonsterUI.m_IntelligenceList, flowLayoutPanel1, ref m_FlowHeight);
            SetPanel("知覚", MainForm.m_MonsterUI.m_PerceptionList, flowLayoutPanel1, ref m_FlowHeight);
            SetPanel("反応", MainForm.m_MonsterUI.m_ReactionList, flowLayoutPanel1, ref m_FlowHeight);
            SetPanel("言語", MainForm.m_MonsterUI.m_LanguageList, flowLayoutPanel1, ref m_FlowHeight);

            flowLayoutPanel1.Height = m_FlowHeight;

            if (flowLayoutPanel1.Height >= this.Height)
            {
                this.Height = flowLayoutPanel1.Height + 64 + tabPage1.Bounds.Y;
                tabControl1.Height = flowLayoutPanel1.Height + 64 + tabPage1.Bounds.Y;
            }

            SetPanel("弱点", MainForm.m_MonsterUI.m_Weak_PointList, flowLayoutPanel2, ref m_Flow2Height);
            SetPanel("移動方法", MainForm.m_MonsterUI.m_Move_SpeedList, flowLayoutPanel2, ref m_Flow2Height);
            SetPanel("特殊能力の型", MainForm.m_MonsterUI.m_DeclarationList, flowLayoutPanel2, ref m_Flow2Height);
            SetPanel("出典", MainForm.m_MonsterUI.m_ReferenceList, flowLayoutPanel2, ref m_Flow2Height);

            flowLayoutPanel2.Height = m_Flow2Height;

            if (flowLayoutPanel2.Height >= this.Height)
            {
                this.Height = flowLayoutPanel2.Height + 64 + tabPage2.Bounds.Y;
                tabControl1.Height = flowLayoutPanel2.Height + 64 + tabPage2.Bounds.Y;
            }

            SetFindBar("魔物名", "半角空白で複数、細かく指定できます", flowLayoutPanel3);
            SetFindBaseNums();
            SetFindStatus();
            SetFindSpecial();
            SetFindDropItem();

            flowLayoutPanel3.Height = m_Flow3Height;

            if (flowLayoutPanel3.Height >= this.Height)
            {
                this.Height = flowLayoutPanel3.Height + 64 + tabPage3.Bounds.Y;
                tabPage3.Height = flowLayoutPanel3.Height + 64 + tabPage3.Bounds.Y;
            }

            this.Height = flowLayoutPanel1.Height + 64 + tabPage1.Bounds.Y;
            tabControl1.Height = flowLayoutPanel1.Height + 64 + tabPage1.Bounds.Y;

            m_Init = true;

        }


        private int SetChoice(string name, Panel panel, List<string> list,FlowLayoutPanel flow)
        {
            int l_totalWidth = 0;
            int l_totalHeiht = 0;
            int l_Num = 0;
            foreach (var b in list.Select((v, i) => new { v, i }))
            {
                CheckBox l_Check = new CheckBox();
                l_Check.Text = b.v;
                l_Check.CheckedChanged += L_Check_CheckedChanged;
                l_Check.Name = name + ":" + b.v;

                if ((l_totalWidth + l_Check.PreferredSize.Width + 5) >= flow.Width)
                {
                    l_totalHeiht += 20;
                    l_totalWidth = 0;
                    l_Num += 1;
                }

                l_Check.SetBounds(5 + (l_totalWidth), 5 + (l_totalHeiht), l_Check.PreferredSize.Width, l_Check.PreferredSize.Height);

                SetIsChecked(name, ref l_Check);

                l_totalWidth += l_Check.PreferredSize.Width + 5;
                panel.Controls.Add(l_Check);
            }

            return (16 * (l_Num + 1)) + (5 * (l_Num + 1)) + panel.Margin.Top;
        }

        private void SetIsChecked(string name, ref CheckBox box)
        {
            if (name == "分類")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_CategoryCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }

                m_CategoryCheck = MainForm.m_ViewData.m_Filter.m_CategoryCheck;
            }
            else if (name == "知能")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_IntelligenceCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }

                m_IntelligenceCheck = MainForm.m_ViewData.m_Filter.m_IntelligenceCheck;
            }
            else if (name == "知覚")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_PerceptionCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }
                m_PerceptionCheck = MainForm.m_ViewData.m_Filter.m_PerceptionCheck;
            }
            else if (name == "反応")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_ReactionCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }
                m_ReactionCheck = MainForm.m_ViewData.m_Filter.m_ReactionCheck;
            }
            else if (name == "言語")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_LanguageCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }
                m_LangageCheck = MainForm.m_ViewData.m_Filter.m_LanguageCheck;

            }
            else if (name == "弱点")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_Week_PointCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }
                m_Weak_PointCheck = MainForm.m_ViewData.m_Filter.m_Week_PointCheck;
            }
            else if (name == "移動方法")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_Move_SpeedCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }
                m_MoveCheck = MainForm.m_ViewData.m_Filter.m_Move_SpeedCheck;
            }
            else if (name == "特殊能力の型")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_DeclarationCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }
                m_DeclarationCheck = MainForm.m_ViewData.m_Filter.m_DeclarationCheck;
            }
            else if (name == "出典")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_ReferenceCheck)
                {
                    if (v == box.Text)
                        box.Checked = true;
                }
                m_ReferenceCheck = MainForm.m_ViewData.m_Filter.m_ReferenceCheck;
            }
        }

        private void L_Check_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox l_Box = (CheckBox)sender;

            if (l_Box.Checked)
                l_Box.BackColor = Color.MediumVioletRed;
            else
                l_Box.BackColor = Color.Transparent;


            //初期化のときはなし
            if (m_Init == false) return;

            string l_ParentName = l_Box.Name.Replace(l_Box.Text, "");
            l_ParentName = l_ParentName.Replace(":", "");

            if (l_ParentName == "分類")
                FindListChange(ref m_CategoryCheck, l_Box.Text, l_Box.Checked);
            else if (l_ParentName == "知能")
                FindListChange(ref m_IntelligenceCheck, l_Box.Text, l_Box.Checked);
            else if (l_ParentName == "知覚")
                FindListChange(ref m_PerceptionCheck, l_Box.Text, l_Box.Checked);
            else if (l_ParentName == "反応")
                FindListChange(ref m_ReactionCheck, l_Box.Text, l_Box.Checked);
            else if (l_ParentName == "言語")
                FindListChange(ref m_LangageCheck, l_Box.Text, l_Box.Checked);
            else if (l_ParentName == "弱点")
                FindListChange(ref m_Weak_PointCheck, l_Box.Text, l_Box.Checked);
            else if (l_ParentName == "移動方法")
                FindListChange(ref m_MoveCheck, l_Box.Text, l_Box.Checked);
            else if (l_ParentName == "特殊能力の型")
                FindListChange(ref m_DeclarationCheck, l_Box.Text, l_Box.Checked);
            else if (l_ParentName == "出典")
                FindListChange(ref m_ReferenceCheck, l_Box.Text, l_Box.Checked);

        }

        private void FindListChange(ref List<string> list, string text, bool check)
        {
            if (check)
                list.Add(text);
            else
                list.Remove(text);
        }

        private Panel[] SetBasePanel(string name, FlowLayoutPanel panel)
        {
            Panel l_Top = new Panel();
            l_Top.Margin = new Padding(0);
            l_Top.SetBounds(0, 0, panel.Width, 140);
            l_Top.BackColor = Color.White;

            Label l_Name = new Label();
            l_Name.Text = name;
            l_Name.Font = new Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            l_Name.ForeColor = System.Drawing.SystemColors.Window;

            int l_Name_X = (l_Top.Width - l_Name.PreferredWidth) / 2;
            l_Name.SetBounds(l_Name_X, 5, l_Name.PreferredWidth, l_Name.PreferredHeight);

            Panel l_NamePanel = new Panel();
            l_NamePanel.SetBounds(l_Top.Bounds.X, l_Top.Bounds.Y, l_Top.Width, l_Name.PreferredHeight + 10);
            l_NamePanel.BackColor = Color.Black;

            Panel l_ComponentPanel = new Panel();
            l_ComponentPanel.SetBounds(0, l_NamePanel.Height, l_Top.Width, 20);

            l_NamePanel.Controls.Add(l_Name);
            l_Top.Controls.Add(l_NamePanel);
            l_Top.Controls.Add(l_ComponentPanel);

            return new Panel[] { l_Top, l_ComponentPanel, l_NamePanel };
        }

        private void SetPanel(string name, List<string> list,FlowLayoutPanel flow,ref int height)
        {
            m_Top = new Panel();
            m_Top.Margin = new Padding(0);
            m_Top.SetBounds(0, 0, flow.Width, 140);
            m_Top.BackColor = Color.White;

            m_Name = new Label();
            m_Name.Text = name;
            m_Name.Font = new Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            m_Name.ForeColor = System.Drawing.SystemColors.Window;

            int l_Name_X = (m_Top.Width - m_Name.PreferredWidth) / 2;
            m_Name.SetBounds(l_Name_X, 5, m_Name.PreferredWidth, m_Name.PreferredHeight);

            m_NamePanel = new Panel();
            m_NamePanel.SetBounds(m_Top.Bounds.X, m_Top.Bounds.Y, m_Top.Width, m_Name.PreferredHeight + 10);
            m_NamePanel.BackColor = Color.Black;

            m_ComponentPanel = new Panel();
            int l_H = SetChoice(name, m_ComponentPanel, list,flow);
            m_ComponentPanel.SetBounds(0, m_NamePanel.Height, m_Top.Width, l_H);

            m_Top.SetBounds(m_Top.Bounds.X, m_Top.Bounds.Y, m_Top.Width, l_H + m_NamePanel.Height);

            m_NamePanel.Controls.Add(m_Name);
            m_Top.Controls.Add(m_NamePanel);
            m_Top.Controls.Add(m_ComponentPanel);
            flow.Controls.Add(m_Top);

            height += m_Top.Height;
        }

        private void SetFindBar(string name, string ex, FlowLayoutPanel panel)
        {
            Panel[] l_Base = SetBasePanel(name, panel);
            int l_Padding = 5;
            int l_H = 0;

            Label l_Ex = new Label();
            l_Ex.Text = ex;
            l_Ex.SetBounds(l_Padding, l_Padding, l_Base[1].Width - l_Padding * 2, l_Ex.PreferredHeight);

            l_H += l_Padding * 2 + l_Ex.PreferredHeight;
            TextBox l_FindBox = new TextBox();
            l_FindBox.SetBounds(l_Padding, l_H, l_Base[1].Width - l_Padding * 2, l_FindBox.Height);
            l_FindBox.Name = "魔物名";
            SetIsTextBoxChange(ref l_FindBox, "魔物名");
            l_FindBox.TextChanged += L_FindBox_TextChanged;
            l_FindBox.Validated += L_FindBox_Validated1;

            l_H += l_Padding * 2 + l_FindBox.Height;

            l_Base[1].Controls.Add(l_Ex);
            l_Base[1].Controls.Add(l_FindBox);
            l_Base[1].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H);
            l_Base[0].SetBounds(l_Base[0].Bounds.X, l_Base[0].Bounds.Y, l_Base[0].Width, l_Base[2].Height + l_H);

            panel.Controls.Add(l_Base[0]);

            m_Flow3Height += l_Base[0].Height;
        }

        //穢れなど
        private void SetFindBaseNums()
        {
            Panel[] l_Base = SetBasePanel("魔物詳細値", flowLayoutPanel3);

            int l_Padding = 10;
            int l_X = l_Padding;
            int l_H = l_Padding;

            SetFindValueBar("魔物詳細値", "レベル", l_Padding, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("魔物詳細値", "知名度", l_Padding, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("魔物詳細値", "弱点値", l_Padding, 50, ref l_X, l_H, l_Base[1]);

            l_X = l_Padding;
            l_H += l_Padding * 2 + 6;

            SetFindValueBar("魔物詳細値", "穢れ", l_Padding, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("魔物詳細値", "先制値", l_Padding, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("魔物詳細値", "移動速度", l_Padding, 50, ref l_X, l_H, l_Base[1]);

            l_X = l_Padding;
            l_H += l_Padding * 2 + 6;

            SetFindValueBar("魔物詳細値", "生命抵抗力", l_Padding, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("魔物詳細値", "精神抵抗力", l_Padding, 50, ref l_X, l_H, l_Base[1]);

            l_H += l_Padding * 2 + 6;

            SetFindStringBar("生息地(半角空白で複数、細かく指定できます)", "生息地", 5, ref l_H, l_Base[1]);

            l_Base[1].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H);
            l_Base[0].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H + l_Base[2].Height);

            flowLayoutPanel3.Controls.Add(l_Base[0]);

            m_Flow3Height += l_Base[0].Height;
        }

        //ステータス
        private void SetFindStatus()
        {
            Panel[] l_Base = SetBasePanel("基礎ステータス", flowLayoutPanel3);

            int l_l_Padding = 8;
            int l_X = l_l_Padding;
            int l_H = l_l_Padding;

            SetFindValueBar("基礎ステータス", "命中力", l_l_Padding, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("基礎ステータス", "打撃点 2d+", l_l_Padding, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("基礎ステータス", "回避力", l_l_Padding, 50, ref l_X, l_H, l_Base[1]);

            l_X = l_l_Padding;
            l_H += l_l_Padding * 2 + 8;

            SetFindValueBar("基礎ステータス", "防護点", 5, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("基礎ステータス", "HP", 5, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("基礎ステータス", "MP", 5, 50, ref l_X, l_H, l_Base[1]);
            SetFindValueBar("基礎ステータス", "部位数", 5, 50, ref l_X, l_H, l_Base[1]);

            l_H += l_l_Padding * 2 + 8;

            SetFindStringBar("攻撃方法/部位(半角空白で複数、細かく指定できます)", "攻撃方法", 5, ref l_H, l_Base[1]);

            l_Base[1].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H);
            l_Base[0].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H + l_Base[2].Height);

            flowLayoutPanel3.Controls.Add(l_Base[0]);

            m_Flow3Height += l_Base[0].Height;
        }

        //特殊能力
        private void SetFindSpecial()
        {
            Panel[] l_Base = SetBasePanel("特殊能力", flowLayoutPanel3);

            int l_l_Padding = 5;
            int l_H = l_l_Padding;

            SetFindStringBar("特殊能力名(半角空白で複数、細かく指定できます)", "特殊能力名", 5, ref l_H, l_Base[1]);
            SetFindStringBar("能力解説(半角空白で複数、細かく指定できます)", "能力解説", 5, ref l_H, l_Base[1]);

            l_Base[1].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H);
            l_Base[0].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H + l_Base[2].Height);

            flowLayoutPanel3.Controls.Add(l_Base[0]);

            m_Flow3Height += l_Base[0].Height;
        }

        //戦利品
        private void SetFindDropItem()
        {
            Panel[] l_Base = SetBasePanel("戦利品", flowLayoutPanel3);

            int l_l_Padding = 8;
            int l_X = 5;
            int l_H = l_l_Padding;

            SetFindValueBar("戦利品", "ガメル", l_l_Padding, 100, ref l_X, l_H, l_Base[1]);

            l_H += l_l_Padding * 2 + 8;

            SetFindStringBar("戦利品名(半角空白で複数、細かく指定できます)", "戦利品名", 5, ref l_H, l_Base[1]);

            SetFindStringBar("マテリアルカード(半角空白で複数、細かく指定できます)", "マテリアルカード", 5, ref l_H, l_Base[1]);


            l_Base[1].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H);
            l_Base[0].SetBounds(0, l_Base[2].Height, l_Base[0].Width, l_H + l_Base[2].Height);

            flowLayoutPanel3.Controls.Add(l_Base[0]);

            m_Flow3Height += l_Base[0].Height;
        }

        private void SetFindStringBar(string name, string findname, int l_Padding, ref int h, Panel panel)
        {
            Label l_Ex = new Label();
            l_Ex.Text = name;
            l_Ex.SetBounds(l_Padding, h, panel.Width - l_Padding * 2, l_Ex.PreferredHeight);

            h += l_Padding * 2 + l_Ex.PreferredHeight;
            TextBox l_FindBox = new TextBox();
            l_FindBox.Name = findname;
            SetIsTextBoxChange(ref l_FindBox, findname);
            l_FindBox.TextChanged += L_FindBox_TextChanged;
            l_FindBox.Validated += L_FindBox_Validated1;
            l_FindBox.SetBounds(l_Padding, h, panel.Width - l_Padding * 2, l_FindBox.Height);

            h += l_Padding * 2 + l_FindBox.Height;

            panel.Controls.Add(l_Ex);
            panel.Controls.Add(l_FindBox);

        }

        private void SetIsTextBoxChange(ref TextBox box, string findname)
        {
            if (findname == "戦利品名")
            {
                box.Text = "";
                foreach (string v in MainForm.m_ViewData.m_Filter.m_BootyCheck)
                    box.Text += v + " ";

                m_BootyCheck = MainForm.m_ViewData.m_Filter.m_BootyCheck;
            }
            else if (findname == "マテリアルカード")
            {
                box.Text = "";
                foreach (string v in MainForm.m_ViewData.m_Filter.m_MaterialCheck)
                    box.Text += v + " ";

                m_MaterialCheck = MainForm.m_ViewData.m_Filter.m_MaterialCheck;
            }
            else if (findname == "特殊能力名")
            {
                box.Text = "";
                foreach (string v in MainForm.m_ViewData.m_Filter.m_SpeNameCheck)
                    box.Text += v + " ";

                m_SpeNameCheck = MainForm.m_ViewData.m_Filter.m_SpeNameCheck;
            }
            else if (findname == "能力解説")
            {
                box.Text = "";
                foreach (string v in MainForm.m_ViewData.m_Filter.m_DescriptionCheck)
                    box.Text += v + " ";

                m_DescriptionCheck = MainForm.m_ViewData.m_Filter.m_DescriptionCheck;
            }
            else if (findname == "攻撃方法")
            {
                box.Text = "";
                foreach (string v in MainForm.m_ViewData.m_Filter.m_PartCheck)
                    box.Text += v + " ";

                m_PartCheck = MainForm.m_ViewData.m_Filter.m_PartCheck;
            }
            else if (findname == "生息地")
            {
                box.Text = "";
                foreach (string v in MainForm.m_ViewData.m_Filter.m_HabitatCheck)
                    box.Text += v + " ";

                m_HabitatCheck = MainForm.m_ViewData.m_Filter.m_HabitatCheck;
            }
            else if (findname == "魔物名")
            {
                box.Text = "";
                foreach (string v in MainForm.m_ViewData.m_Filter.m_MonsterNameCheck)
                    box.Text += v + " ";

                m_MonsterNameCheck = MainForm.m_ViewData.m_Filter.m_MonsterNameCheck;
            }
        }

        private void L_FindBox_Validated1(object sender, EventArgs e)
        {

        }

        private void L_FindBox_TextChanged(object sender, EventArgs e)
        {
            TextBox l_Child = ((TextBox)sender);
            string l_Text = l_Child.Text;

            string l_ParentName = l_Child.Name;


            if (l_ParentName == "魔物名")
                TextFindListChange(l_Text, ref m_MonsterNameCheck);
            else if (l_ParentName == "生息地")
                TextFindListChange(l_Text, ref m_HabitatCheck);
            else if (l_ParentName == "攻撃方法")
                TextFindListChange(l_Text, ref m_PartCheck);
            else if (l_ParentName == "特殊能力名")
                TextFindListChange(l_Text, ref m_SpeNameCheck);
            else if (l_ParentName == "能力解説")
                TextFindListChange(l_Text, ref m_DescriptionCheck);
            else if (l_ParentName == "戦利品名")
                TextFindListChange(l_Text, ref m_BootyCheck);
            else if (l_ParentName == "マテリアルカード")
                TextFindListChange(l_Text, ref m_MaterialCheck);


        }

        private void TextFindListChange(string text, ref List<string> list)
        {
            list.Clear();

            char[] l_Sep = new char[] { ' ', '　' };
            string[] l_Find = text.Split(l_Sep);

            foreach (string v in l_Find)
            {
                if (v == "") continue;
                list.Add(v);
            }
        }

        private void SetFindValueBar(string parent, string name, int l_Padding, int width, ref int x, int h, Panel panel)
        {
            x += l_Padding;

            Label l_Label = new Label();
            l_Label.Text = name;
            l_Label.SetBounds(x, h + 3, l_Label.PreferredWidth, l_Label.PreferredHeight);

            x += l_Padding + l_Label.PreferredWidth;

            NumericUpDown l_FindBox = new NumericUpDown();
            l_FindBox.Name = parent + ":" + name;
            l_FindBox.Minimum = -22;
            l_FindBox.Maximum = 99999;
            l_FindBox.Value = -22;
            SetIsValue(parent, name, ref l_FindBox);
            l_FindBox.ValueChanged += L_FindBox_ValueChanged;
            l_FindBox.Validated += L_FindBox_Validated;
            l_FindBox.SetBounds(x, h, width, l_FindBox.Height);

            x += l_FindBox.Width;


            panel.Controls.Add(l_Label);
            panel.Controls.Add(l_FindBox);

        }

        private void SetIsValue(string parent, string name, ref NumericUpDown box)
        {
            if (parent == "基礎ステータス")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_StatusCheck)
                {
                    if (v.Contains(name))
                    {
                        string[] l_v = v.Split(':');

                        box.Value = int.Parse(l_v[1]);
                        break;
                    }
                }

                m_StatusCheck = MainForm.m_ViewData.m_Filter.m_StatusCheck;
            }
            else if (parent == "魔物詳細値")
            {
                foreach (string v in MainForm.m_ViewData.m_Filter.m_MonsterNumCheck)
                {
                    if (v.Contains(name))
                    {
                        string[] l_v = v.Split(':');

                        box.Value = int.Parse(l_v[1]);
                        break;
                    }
                }

                m_MonsterNumCheck = MainForm.m_ViewData.m_Filter.m_MonsterNumCheck;
            }
            if (parent == "戦利品")
            {
                if (MainForm.m_ViewData.m_Filter.m_GamelCheck.Contains(name))
                {
                    string[] l_v = MainForm.m_ViewData.m_Filter.m_GamelCheck.Split(':');

                    box.Value = int.Parse(l_v[1]);
                }

                m_GamelCheck = MainForm.m_ViewData.m_Filter.m_GamelCheck;
            }
        }

        private void L_FindBox_Validated(object sender, EventArgs e)
        {
            NumericUpDown l_FindBox = (NumericUpDown)sender;

            if (l_FindBox.Text == "")
                l_FindBox.Text = "-22";

            TextBoxIsChanged(l_FindBox);
        }

        private void L_FindBox_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown l_FindBox = (NumericUpDown)sender;

            TextBoxIsChanged(l_FindBox);

        }

        private void TextBoxIsChanged(NumericUpDown l_FindBox)
        {
            int l_Find = l_FindBox.Name.IndexOf(":");
            string l_Parent = l_FindBox.Name.Remove(l_Find, l_FindBox.Name.Count() - l_Find);
            string l_Child = l_FindBox.Name.Replace(l_Parent + ":", "");

            if (l_Parent == "魔物詳細値")
            {
                foreach (string v in m_MonsterNumCheck)
                {
                    if (v.Contains(l_Child))
                    {
                        m_MonsterNumCheck.Remove(v);
                        break;
                    }
                }

                if (l_FindBox.Text == "-22") return;

                m_MonsterNumCheck.Add(l_Child + ":" + l_FindBox.Value.ToString());
            }
            else if (l_Parent == "基礎ステータス")
            {
                foreach (string v in m_StatusCheck)
                {
                    if (v.Contains(l_Child))
                    {
                        m_StatusCheck.Remove(v);
                        break;
                    }
                }

                if (l_FindBox.Text == "-22") return;

                m_StatusCheck.Add(l_Child + ":" + l_FindBox.Value.ToString());

            }
            else if (l_Parent == "戦利品")
            {
                m_GamelCheck = "";

                if (l_FindBox.Text == "-22") return;

                m_GamelCheck = l_Child + ":" + l_FindBox.Value.ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        //キャンセル
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //適用
        private void button2_Click(object sender, EventArgs e)
        {
            MainForm.m_ViewData.m_Filter.m_IntelligenceCheck = m_IntelligenceCheck;
            MainForm.m_ViewData.m_Filter.m_PerceptionCheck = m_PerceptionCheck;
            MainForm.m_ViewData.m_Filter.m_ReactionCheck = m_ReactionCheck;
            MainForm.m_ViewData.m_Filter.m_Move_SpeedCheck = m_MoveCheck;
            MainForm.m_ViewData.m_Filter.m_CategoryCheck = m_CategoryCheck;
            MainForm.m_ViewData.m_Filter.m_Week_PointCheck = m_Weak_PointCheck;
            MainForm.m_ViewData.m_Filter.m_DeclarationCheck = m_DeclarationCheck;
            MainForm.m_ViewData.m_Filter.m_LanguageCheck = m_LangageCheck;
            MainForm.m_ViewData.m_Filter.m_ReferenceCheck = m_ReferenceCheck;

            MainForm.m_ViewData.m_Filter.m_MonsterNameCheck = m_MonsterNameCheck;
            MainForm.m_ViewData.m_Filter.m_HabitatCheck = m_HabitatCheck;
            MainForm.m_ViewData.m_Filter.m_DescriptionCheck = m_DescriptionCheck;
            MainForm.m_ViewData.m_Filter.m_SpeNameCheck = m_SpeNameCheck;
            MainForm.m_ViewData.m_Filter.m_HabitatCheck = m_HabitatCheck;
            MainForm.m_ViewData.m_Filter.m_BootyCheck = m_BootyCheck;
            MainForm.m_ViewData.m_Filter.m_MaterialCheck = m_MaterialCheck;
            MainForm.m_ViewData.m_Filter.m_PartCheck = m_PartCheck;


            MainForm.m_ViewData.m_Filter.m_MonsterNumCheck = m_MonsterNumCheck;
            MainForm.m_ViewData.m_Filter.m_StatusCheck = m_StatusCheck;
            MainForm.m_ViewData.m_Filter.m_GamelCheck = m_GamelCheck;

            m_Parent.m_OrderApply = true;

            Close();

        }

        //クリア
        private void button3_Click(object sender, EventArgs e)
        {
            m_Init = false;
            Control[] l_All = GetAllControls(this);
            foreach (Control c in l_All)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = "";
                }
                else if (c.GetType() == typeof(NumericUpDown))
                {
                    ((NumericUpDown)c).Value = -22;
                }
                else if (c.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)c).Checked = false;
                }
            }
            m_Init = true;

        }

        public Control[] GetAllControls(Control top)
        {
            ArrayList l_Buf = new ArrayList();
            foreach (Control c in top.Controls)
            {
                l_Buf.Add(c);
                l_Buf.AddRange(GetAllControls(c));
            }
            return (Control[])l_Buf.ToArray(typeof(Control));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((TabControl)sender).SelectedTab.Text == "規定値1")
            {
                this.Height = flowLayoutPanel1.Height + 64 + tabPage1.Bounds.Y;
                tabControl1.Height = flowLayoutPanel1.Height + 64 + tabPage1.Bounds.Y;
            }
            else if (((TabControl)sender).SelectedTab.Text == "規定値2")
            {
                this.Height = flowLayoutPanel2.Height + 64 + tabPage2.Bounds.Y;
                tabControl1.Height = flowLayoutPanel2.Height + 64 + tabPage2.Bounds.Y;
            }
            else if (((TabControl)sender).SelectedTab.Text == "自由値")
            {
                this.Height = flowLayoutPanel3.Height + 64 + tabPage3.Bounds.Y;
                tabControl1.Height = flowLayoutPanel3.Height + 64 + tabPage3.Bounds.Y;
            }
            else if (((TabControl)sender).SelectedTab.Text == "設定適用")
            {
                this.Height = 223;
            }

            tabControl1.Update();
        }

        private void CheckClear()
        {
            m_IntelligenceCheck.Clear();
            m_PerceptionCheck.Clear();
            m_ReactionCheck.Clear();
            m_MoveCheck.Clear();
            m_CategoryCheck.Clear();
            m_Weak_PointCheck.Clear();
            m_DeclarationCheck.Clear();
            m_LangageCheck.Clear();
            m_ReferenceCheck.Clear();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
