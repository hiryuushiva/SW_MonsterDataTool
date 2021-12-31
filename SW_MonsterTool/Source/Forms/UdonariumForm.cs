using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SW_MonsterTool.Source.MData;
using SW_MonsterTool.Source.MyNumeric;
using SW_MonsterTool.Source.Utility.Udonarium;
using SW_MonsterTool.Source.Utility.MathUtility;

namespace SW_MonsterTool
{
    public partial class UdonariumForm : Form
    {
        //欠片部位
        public class PiecePart
        {
            public MyNumericUpDown m_PieceNumUpDown;
            public TextBox m_HPTextBox;
            public TextBox m_MPTextBox;

            public PiecePart()
            {
                m_PieceNumUpDown = new MyNumericUpDown();
                m_HPTextBox = new TextBox();
                m_MPTextBox = new TextBox();
            }
        }


        private Monster m_SelectMonster;
        private bool m_Head;
        private List<PiecePart> m_PiecePartList;
        private string m_Life_R = "0";
        private string m_Spirit_R = "0";

        private WindowCenter m_WCenter;

        public UdonariumForm()
        {
            InitializeComponent();

            m_WCenter = new WindowCenter();
            m_PiecePartList = new List<PiecePart>();

            RuleBookCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
            Export_PartComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            //ツリービューにセット
            ChangeOrder(ref SelectMonsterView);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (SelectRuleCheack1.Checked && RuleBookCombo1.Text == "ソードワールド2.0")
            {
                SeimeiTextBox.Enabled = true;
                SeisinTextBox.Enabled = true;

                SeimeiTextBox.Text = m_Life_R;
                SeisinTextBox.Text = m_Spirit_R;
            }
            else
            {
                SeimeiTextBox.Enabled = false;
                SeisinTextBox.Enabled = false;

                SeimeiTextBox.Text = "";
                SeisinTextBox.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RuleBookCombo1.Text == "ソードワールド2.5")
                SelectRuleCheack1.Enabled = false;
            else
                SelectRuleCheack1.Enabled = true;

            if (SelectRuleCheack1.Checked || RuleBookCombo1.Text == "ソードワールド2.5")
            {
                SeimeiTextBox.Enabled = true;
                SeisinTextBox.Enabled = true;

                SeimeiTextBox.Text = m_Life_R;
                SeisinTextBox.Text = m_Spirit_R;
            }
            else
            {
                SeimeiTextBox.Enabled = false;
                SeisinTextBox.Enabled = false;

                SeimeiTextBox.Text = "";
                SeisinTextBox.Text = "";
            }

        }

        private void SetMonsterData()
        {
            m_PiecePartList.Clear();
            Export_MonsterDataPanel1.Controls.Clear();
            U_ExportMonsterNameLabel1.Text = m_SelectMonster.m_Name;

            if (m_SelectMonster == null) return;

            string l_Life = m_SelectMonster.m_Life_Resistance;
            string l_Spirit = m_SelectMonster.m_Spirit_Resistance;

            m_Life_R = l_Life;
            m_Spirit_R = l_Spirit;

            if (SelectRuleCheack1.Checked || RuleBookCombo1.Text == "ソードワールド2.5")
            {

                SeimeiTextBox.Text = l_Life;
                SeisinTextBox.Text = l_Spirit;
            }

            Export_PartComboBox1.Items.Clear();

            Export_PartComboBox1.Items.Add("全ての部位");
            Export_PartComboBox1.Text = "全ての部位";

            foreach (var ms in m_SelectMonster.m_Statuses.Select((v, i) => new { v, i }))
            {
                PiecePart l_Part = new PiecePart();
                SetPanel(Export_MonsterDataPanel1, ms.v, ms.i, ref l_Part);

                Export_PartComboBox1.Items.Add(ms.v.Part);

                m_PiecePartList.Add(l_Part);
            }
        }

        private void SetPanel(FlowLayoutPanel panel, Monster.Status ms, int num, ref PiecePart bui)
        {
            Panel l_Top = new Panel();
            l_Top.Margin = new Padding(0);
            l_Top.SetBounds(0, 0, panel.Width, 140);
            l_Top.BackColor = Color.White;

            int l_X = 0;
            int l_Height = 0;

            Label l_Label = new Label();
            l_Label.Text = "部位名:" + ms.Part;
            l_Label.Font = new Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            l_Label.SetBounds(l_X, l_Height, l_Label.PreferredWidth, l_Label.PreferredHeight);

            l_Height += 3 + 16 + 3;

            SetHPMPBar(num.ToString(), "HP", ms.HP, 0, 70, ref l_X, l_Height, l_Top, ref bui.m_HPTextBox);
            SetHPMPBar(num.ToString(), "MP", ms.MP, 3, 70, ref l_X, l_Height, l_Top, ref bui.m_MPTextBox);

            l_Height += 3 + 20 + 3;

            l_X = 0;

            SetFindValueBar(num.ToString(), "剣のかけら", 0, 0, 70, ref l_X, l_Height, l_Top, ref bui);

            l_Height += 3 + 20 + 3;

            l_Top.Controls.Add(l_Label);

            l_Top.Height = l_Height;

            panel.Controls.Add(l_Top);
        }

        private void SetHPMPBar(string find, string name, string value, int padding, int width, ref int x, int h, Panel panel, ref TextBox box)
        {
            x += padding;

            Label l_Label = new Label();
            l_Label.Text = name;
            l_Label.SetBounds(x, h + 3, width, l_Label.PreferredHeight);

            x += padding + width;

            TextBox l_NumBox = new TextBox();
            l_NumBox.Text = value;
            l_NumBox.Name = find + name;
            l_NumBox.ReadOnly = true;

            l_NumBox.SetBounds(x, h, width, l_NumBox.Height);

            x += l_NumBox.Width;


            panel.Controls.Add(l_Label);
            panel.Controls.Add(l_NumBox);

            box = l_NumBox;
        }

        private void SetFindValueBar(string find, string name, int value, int padding, int width, ref int x, int h, Panel panel, ref PiecePart bui)
        {
            x += padding;

            Label l_Label = new Label();
            l_Label.Text = name;
            l_Label.SetBounds(x, h + 3, width, l_Label.PreferredHeight);

            x += padding + width;

            MyNumericUpDown l_NumBox = new MyNumericUpDown();
            l_NumBox.Minimum = 0;
            l_NumBox.Value = value;
            l_NumBox.Name = find;
            l_NumBox.ValueChanged += L_NumBox_ValueChanged;


            l_NumBox.SetBounds(x, h, width, l_NumBox.Height);

            x += l_NumBox.Width;


            panel.Controls.Add(l_Label);
            panel.Controls.Add(l_NumBox);

            bui.m_PieceNumUpDown = l_NumBox;

        }

        private void L_NumBox_ValueChanged(object sender, EventArgs e)
        {
            MyNumericUpDown l_NumUpDown = ((MyNumericUpDown)sender);

            int l_Num = int.Parse(l_NumUpDown.Name);

            Monster.Status l_MS = m_SelectMonster.m_Statuses[l_Num];

            int l_Abs = l_NumUpDown.Abs();
            if (l_NumUpDown.IsUpDown() == 1)
            {
                for (int i = 0; i != l_Abs; i++)
                {
                    m_SelectMonster.m_TotalPiece = MathUtil.AddStringNum(m_SelectMonster.m_TotalPiece, "1");

                    l_MS.HP = MathUtil.AddStringNum(l_MS.HP, "5");
                    m_PiecePartList[l_Num].m_HPTextBox.Text = l_MS.HP;
                    l_MS.MP = MathUtil.AddStringNum(l_MS.MP, "1");
                    m_PiecePartList[l_Num].m_MPTextBox.Text = l_MS.MP;

                    m_Life_R = MathUtil.AddStringNum(m_Life_R, SelectRuleValue().ToString());
                    m_Spirit_R = MathUtil.AddStringNum(m_Spirit_R, SelectRuleValue().ToString());
                }
            }
            else
            {
                for (int i = 0; i != l_Abs; i++)
                {
                    l_MS.HP = MathUtil.SubStringNum(l_MS.HP, "5");
                    m_PiecePartList[l_Num].m_HPTextBox.Text = l_MS.HP;
                    l_MS.MP = MathUtil.SubStringNum(l_MS.MP, "1");
                    m_PiecePartList[l_Num].m_MPTextBox.Text = l_MS.MP;

                    m_Life_R = MathUtil.SubStringNum(m_Life_R, SelectRuleValue().ToString());
                    m_Spirit_R = MathUtil.SubStringNum(m_Spirit_R, SelectRuleValue().ToString());


                    m_SelectMonster.m_TotalPiece = MathUtil.SubStringNum(m_SelectMonster.m_TotalPiece, "1");
                }
            }


            if (SelectRuleCheack1.Checked || RuleBookCombo1.Text == "ソードワールド2.5")
            {

                SeimeiTextBox.Text = m_Life_R;
                SeisinTextBox.Text = m_Spirit_R;
            }

            l_MS.Sword_Piece = l_NumUpDown.ValueAsInt.ToString();

            m_SelectMonster.m_Statuses[l_Num] = l_MS;
        }

        private int SelectRuleValue()
        {
            int l_Total = int.Parse(m_SelectMonster.m_TotalPiece);

            int l_Result = 0;

            if (l_Total <= 5)
                l_Result = 1;
            else if (l_Total >= 6 && l_Total <= 10)
                l_Result = 2;
            else if (l_Total >= 11 && l_Total <= 15)
                l_Result = 3;
            else if (l_Total >= 16)
                l_Result = 4;

            return l_Result;
        }

        public void SelectMonster(string name, string shuntten)
        {
            foreach (Monster m in MainForm.m_ViewData.m_Monsters)
            {
                if (m.m_Name == name && m.m_Reference == shuntten)
                {

                    m_SelectMonster = m.DeepClone();
                    break;
                }
            }
        }

        public void AddTreeView(ref TreeView treeview)
        {
            m_Head = true;

            treeview.Nodes.Clear();

            foreach (string category in MainForm.m_MonsterUI.m_CategoryList)
            {
                TreeNode l_Node = new TreeNode(category);
                l_Node.NodeFont = new Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));

                foreach (Monster m in MainForm.m_ViewData.m_Monsters)
                {
                    if (m.m_Category != category)
                        continue;

                    TreeNode l_Name = new TreeNode(m.m_Name + "(" + m.m_Reference + ")");
                    l_Name.NodeFont = new Font("MS UI Gothic", 9F);
                    l_Node.Nodes.Add(l_Name);
                }

                treeview.Nodes.Add(l_Node);
            }

        }

        public void ChangeOrder(ref TreeView treeView)
        {
            treeView.Nodes.Clear();
            m_Head = false;

            List<Monster> l_Monster = MainForm.m_ViewData.m_Filter.MatchFindOrder(MainForm.m_ViewData.m_Monsters);

            if (l_Monster.Count() != 0)
                foreach (Monster m in l_Monster)
                {
                    TreeNode l_Node = new TreeNode(m.m_Name + "(" + m.m_Reference + ")");
                    l_Node.NodeFont = new Font("MS UI Gothic", 9F);

                    treeView.Nodes.Add(l_Node);
                }
            else
                AddTreeView(ref treeView);

        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (m_Head)
            {
                if (e.Node.Parent == null) return;
                int l_Start = e.Node.Text.IndexOf("(");

                string l_Name = e.Node.Text.Remove(l_Start, e.Node.Text.Count() - l_Start);
                string l_Refe = e.Node.Text.Replace(l_Name + "(", "");
                l_Refe = l_Refe.Replace(")", "");

                SelectMonster(l_Name, l_Refe);
            }
            else
            {
                int l_Start = e.Node.Text.IndexOf("(");

                string l_Name = e.Node.Text.Remove(l_Start, e.Node.Text.Count() - l_Start);
                string l_Refe = e.Node.Text.Replace(l_Name + "(", "");
                l_Refe = l_Refe.Replace(")", "");

                SelectMonster(l_Name, l_Refe);
            }

            SetMonsterData();
        }

        //出力
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_SelectMonster == null)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "出力対象が存在しません", "問題", MessageBoxButtons.OK);
                return;
            }

            Monster l_SendMonster = m_SelectMonster.DeepClone();
            if (SelectRuleCheack1.Checked || RuleBookCombo1.Text == "ソードワールド2.5")
            {
                l_SendMonster.m_Life_Resistance = m_Life_R;
                l_SendMonster.m_Spirit_Resistance = m_Spirit_R;
            }

            int l_Result = UdonariumUtil.Export(l_SendMonster, RuleBookCombo1.Text,Export_PartComboBox1.Text);

            if (l_Result == -1)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "ファイル名に使えない文字が含まれています\n部位名、名前を修正してください", "問題", MessageBoxButtons.OK);
            }
            else
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "出力が完了しました", "完了", MessageBoxButtons.OK);
            }
        }
    }
}
