using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using SW_MonsterTool.Source.MSort;

namespace SW_MonsterTool
{
    public partial class SortForm : Form
    {
        int m_FlowHeight = 0;

        List<string> m_SortList;
        List<CheckBox> m_CheckSortList1;
        List<CheckBox> m_CheckSortList2;
        List<CheckBox> m_CheckSortPriority1;
        List<CheckBox> m_CheckSortPriority2;
        List<string> m_SortPriority;

        bool m_FindCheckNow = false;
        MainForm m_Parent;

        public SortForm(MainForm form)
        {
            m_Parent = form;
            m_SortList = new List<string>();
            m_CheckSortList1 = new List<CheckBox>();
            m_CheckSortList2 = new List<CheckBox>();
            m_CheckSortPriority1 = new List<CheckBox>();
            m_CheckSortPriority2 = new List<CheckBox>();
            m_SortPriority = new List<string>();
            m_SortList.Add("レベル順");
            m_SortList.Add("知名度順");
            m_SortList.Add("弱点値順");
            m_SortList.Add("穢れ順");
            m_SortList.Add("先制値順");
            m_SortList.Add("移動速度順");
            m_SortList.Add("生命抵抗力順");
            m_SortList.Add("精神抵抗力順");
            m_SortList.Add("命中力順");
            m_SortList.Add("打撃点順");
            m_SortList.Add("回避力順");
            m_SortList.Add("防護点順");
            m_SortList.Add("HP順");
            m_SortList.Add("MP順");
            m_SortList.Add("部位数順");
            m_SortList.Add("戦利品ガメル順");
            m_SortList.Add("分類順");

            m_SortPriority.Add("高い順");
            m_SortPriority.Add("低い順");

            InitializeComponent();

            SetPanel("並び替え条件", m_SortList, flowLayoutPanel1, m_CheckSortList1);
            SetPanel("並び順", m_SortPriority, flowLayoutPanel1, m_CheckSortPriority1);
            SetPanel("並び替え条件", m_SortList, flowLayoutPanel2, m_CheckSortList2);
            SetPanel("並び順", m_SortPriority, flowLayoutPanel2, m_CheckSortPriority2);

            this.MaximizeBox = false;
        }

        private void SetPanel(string name, List<string> list, FlowLayoutPanel flowpanel, List<CheckBox> checks)
        {
            Panel l_Top = new Panel();
            l_Top.Margin = new Padding(0);
            l_Top.SetBounds(0, 0, flowpanel.Width, 140);
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
            int l_Height = SetChoice(name, l_ComponentPanel, list, flowpanel, checks);
            l_ComponentPanel.SetBounds(0, l_NamePanel.Height, l_Top.Width, l_Height);

            l_Top.SetBounds(l_Top.Bounds.X, l_Top.Bounds.Y, l_Top.Width, l_Height + l_NamePanel.Height);

            l_NamePanel.Controls.Add(l_Name);
            l_Top.Controls.Add(l_NamePanel);
            l_Top.Controls.Add(l_ComponentPanel);
            flowpanel.Controls.Add(l_Top);

            m_FlowHeight += l_Top.Height;
        }

        private int SetChoice(string name, Panel panel, List<string> list, FlowLayoutPanel flowpanel, List<CheckBox> checks)
        {
            int l_TotalWidth = 0;
            int l_TotalHeiht = 0;
            int l_Num = 0;
            foreach (var b in list.Select((v, i) => new { v, i }))
            {
                CheckBox l_Check = new CheckBox();
                l_Check.Text = b.v;
                l_Check.CheckedChanged += L_Check_CheckedChanged;
                l_Check.Name = name + ":" + b.v + flowpanel.Name;

                if ((l_TotalWidth + l_Check.PreferredSize.Width + 5) >= flowpanel.Width)
                {
                    l_TotalHeiht += 20;
                    l_TotalWidth = 0;
                    l_Num += 1;
                }

                l_Check.SetBounds(5 + (l_TotalWidth), 5 + (l_TotalHeiht), l_Check.PreferredSize.Width, l_Check.PreferredSize.Height);

                SetIsChecked(ref l_Check);

                l_TotalWidth += l_Check.PreferredSize.Width + 5;
                checks.Add(l_Check);
                panel.Controls.Add(l_Check);
            }

            return (16 * (l_Num + 1)) + (5 * (l_Num + 1)) + panel.Margin.Top;
        }

        private void L_Check_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox l_Box = (CheckBox)sender;

            string l_ParentName = l_Box.Name.Replace(l_Box.Text, "");
            string[] l_Names = l_ParentName.Split(':');

            int l_Count = 0;

            if (l_Names[0] == "並び替え条件")
            {
                if (l_Names[1] == flowLayoutPanel1.Name)
                {
                    l_Count = CheckCount(m_CheckSortList1);

                    if (l_Count == 0)
                        l_Box.Checked = true;
                }
            }
            if (l_Names[0] == "並び順")
            {
                if (l_Names[1] == flowLayoutPanel1.Name)
                {
                    l_Count = CheckCount(m_CheckSortPriority1);

                    if (l_Count == 0)
                        l_Box.Checked = true;
                }
            }


            if (l_Box.Checked)
                l_Box.BackColor = Color.MediumVioletRed;
            else
                l_Box.BackColor = Color.Transparent;

            if (m_FindCheckNow) return;


            if (l_Names[0] == "並び替え条件")
            {
                if (l_Names[1] == flowLayoutPanel1.Name)
                    IsCheck(l_Box, m_CheckSortList1);
                else if (l_Names[1] == flowLayoutPanel2.Name)
                    IsCheck(l_Box, m_CheckSortList2);
            }
            if (l_Names[0] == "並び順")
            {
                if (l_Names[1] == flowLayoutPanel1.Name)
                    IsCheck(l_Box, m_CheckSortPriority1);
                else if (l_Names[1] == flowLayoutPanel2.Name)
                    IsCheck(l_Box, m_CheckSortPriority2);
            }

        }

        private int CheckCount(List<CheckBox> sort)
        {
            int l_Result = 0;
            foreach (CheckBox check in sort)
            {
                if (check.Checked)
                    l_Result += 1;
            }

            return l_Result;
        }

        private void IsCheck(CheckBox box, List<CheckBox> sort)
        {
            m_FindCheckNow = true;
            foreach (CheckBox check in sort)
            {
                if (check.Name != box.Name)
                    check.Checked = false;
            }
            m_FindCheckNow = false;
        }

        private void SetIsChecked(ref CheckBox box)
        {
            string l_ParentName = box.Name.Replace(box.Text, "");
            string[] l_Names = l_ParentName.Split(':');

            if (l_Names[0] == "並び替え条件")
            {
                if (l_Names[1] == flowLayoutPanel1.Name)
                    if (box.Text == "分類順")
                        box.Checked = true;
            }
            else if (l_Names[0] == "並び順")
            {
                if (l_Names[1] == flowLayoutPanel1.Name)
                    if (box.Text == "低い順")
                        box.Checked = true;
            }

        }

        //ソート
        private void Sort_OKButton1_Click(object sender, EventArgs e)
        {
            SortType l_Sort1 = SortType.分類順;
            SortType l_Sort2 = SortType.なし;
            SortPriority l_Priority1 = SortPriority.低い;
            SortPriority l_Priority2 = SortPriority.なし;

            foreach (CheckBox checkBox in m_CheckSortList1)
            {
                if (checkBox.Checked)
                {
                    ClassificationCheck(checkBox.Text, ref l_Sort1);
                    break;
                }

            }

            foreach (CheckBox checkBox in m_CheckSortList2)
            {
                if (checkBox.Checked)
                {
                    ClassificationCheck(checkBox.Text, ref l_Sort2);
                    break;
                }
                else
                    l_Sort2 = SortType.なし;

            }

            foreach (CheckBox checkBox in m_CheckSortPriority1)
            {
                if (checkBox.Checked)
                    if (checkBox.Text == "高い順")
                        l_Priority1 = SortPriority.高い;
                    else
                        l_Priority1 = SortPriority.低い;

            }

            foreach (CheckBox checkBox in m_CheckSortPriority2)
            {
                if (checkBox.Checked)
                    if (checkBox.Text == "高い順")
                        l_Priority2 = SortPriority.高い;
                    else
                        l_Priority2 = SortPriority.低い;
                else
                    l_Priority2 = SortPriority.なし;
            }

            m_Parent.MonsterTreeView1.BeginUpdate();
            m_Parent.MonsterTreeView1.TreeViewNodeSorter = new MonsterSort(l_Sort1, l_Sort2, l_Priority1, l_Priority2, MainForm.m_ViewData.m_Monsters);
            m_Parent.MonsterTreeView1.EndUpdate();

            m_Parent.m_OrderApply = true;
            Close();
        }

        private void ClassificationCheck(string text, ref SortType type)
        {
            if (text == "レベル順")
                type = SortType.レベル順;
            else if (text == "知名度順")
                type = SortType.知名度順;
            else if (text == "弱点値順")
                type = SortType.弱点値順;
            else if (text == "穢れ順")
                type = SortType.穢れ順;
            else if (text == "先制値順")
                type = SortType.先制値順;
            else if (text == "移動速度順")
                type = SortType.移動速度順;
            else if (text == "生命抵抗力順")
                type = SortType.生命抵抗力順;
            else if (text == "精神抵抗力順")
                type = SortType.精神抵抗力順;
            else if (text == "命中力順")
                type = SortType.命中力順;
            else if (text == "打撃点順")
                type = SortType.打撃点順;
            else if (text == "回避力順")
                type = SortType.回避力順;
            else if (text == "防護点順")
                type = SortType.防護点順;
            else if (text == "HP順")
                type = SortType.HP順;
            else if (text == "MP順")
                type = SortType.MP順;
            else if (text == "部位数順")
                type = SortType.部位数順;
            else if (text == "戦利品ガメル順")
                type = SortType.戦利品ガメル順;
            else if (text == "分類順")
                type = SortType.分類順;
        }

        //全てクリア
        private void AllResetButton1_Click(object sender, EventArgs e)
        {
            Control[] l_All = GetAllControls(this);
            foreach (Control c in l_All)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox box = ((CheckBox)c);
                    box.Checked = false;
                    
                    SetIsChecked(ref box);
                }
            }
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


        //キャンセル
        private void CanselButton1_Click(object sender, EventArgs e)
        {
            m_Parent.m_OrderApply = true;

            Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
