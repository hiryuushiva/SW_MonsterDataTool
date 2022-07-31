using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW_MonsterTool.Source.MyListView;
using SW_MonsterTool.Source.Utility.FileUtility;
using System.Drawing;
using System.Windows.Forms;
using SW_MonsterTool.Source.Utility.MathUtility;

namespace SW_MonsterTool.Source.MUIAdd
{
    //ToolのUI追加系処理クラス
    public class MonsterUIAdd
    {
        //編集用ステータス
        private ColumnHeaderEx m_Edit_NoHeader;
        private ColumnHeaderEx m_Edit_PartHeader;
        private ColumnHeaderEx m_Edit_HitHeader;
        private ColumnHeaderEx m_Edit_DamageHeader;
        private ColumnHeaderEx m_Edit_DodgeHeader;
        private ColumnHeaderEx m_Edit_ProtectionHeader;
        private ColumnHeaderEx m_Edit_HPHeader;
        private ColumnHeaderEx m_Edit_MPHeader;

        //表示用ステータス
        private ColumnHeaderEx m_View_NoHeader;
        private ColumnHeaderEx m_View_PartHeader;
        private ColumnHeaderEx m_View_HitHeader;
        private ColumnHeaderEx m_View_DamageHeader;
        private ColumnHeaderEx m_View_DodgeHeader;
        private ColumnHeaderEx m_View_ProtectionHeader;
        private ColumnHeaderEx m_View_HPHeader;
        private ColumnHeaderEx m_View_MPHeader;

        //画像用ステータス
        private ColumnHeaderEx m_Image_NoHeader;
        private ColumnHeaderEx m_Image_PartHeader;
        private ColumnHeaderEx m_Image_HitHeader;
        private ColumnHeaderEx m_Image_DamageHeader;
        private ColumnHeaderEx m_Image_DodgeHeader;
        private ColumnHeaderEx m_Image_ProtectionHeader;
        private ColumnHeaderEx m_Image_HPHeader;
        private ColumnHeaderEx m_Image_MPHeader;

        //モンスターデータ細かい奴
        private ComboBox m_IntelligenceCombo;
        private ComboBox m_PerceptionCombo;
        private ComboBox m_ReactionCombo;
        private ComboBox m_Move_Speed1Combo;
        private ComboBox m_Move_Speed2Combo;
        private ComboBox m_CategoryCombo;
        private ComboBox m_ReferenceCombo;
        private ComboBox m_Weak_PointCombo;

        public List<string> m_IntelligenceList;
        public List<string> m_PerceptionList;
        public List<string> m_ReactionList;
        public List<string> m_Move_SpeedList;
        public List<string> m_CategoryList;
        public List<string> m_Weak_PointList;
        public List<string> m_DeclarationList;
        public List<string> m_LanguageList;
        public List<string> m_ReferenceList;

        private ListViewEx m_ListView;
        private int[] m_ListViewSelectItem;

        public FileUtil m_ConfigCSV;

        //引数でもらってくる
        public MonsterUIAdd(ListViewEx listView, ComboBox INT, ComboBox Chikaku,
            ComboBox Reaction, ComboBox Bunrui, ComboBox Shutten, ComboBox idou1, ComboBox idou2,
            ComboBox zyakuten)
        {
            m_IntelligenceCombo = INT;
            m_PerceptionCombo = Chikaku;
            m_ReactionCombo = Reaction;
            m_CategoryCombo = Bunrui;
            m_ReferenceCombo = Shutten;
            m_ListView = listView;
            m_Move_Speed1Combo = idou1;
            m_Move_Speed2Combo = idou2;
            m_Weak_PointCombo = zyakuten;

            Initialize();
        }

        private void Initialize()
        {
            //ステータスUI周り
            this.m_Edit_NoHeader = ((new ColumnHeaderEx()));
            this.m_Edit_PartHeader = ((new ColumnHeaderEx()));
            this.m_Edit_HitHeader = ((new ColumnHeaderEx()));
            this.m_Edit_DamageHeader = ((new ColumnHeaderEx()));
            this.m_Edit_DodgeHeader = ((new ColumnHeaderEx()));
            this.m_Edit_ProtectionHeader = ((new ColumnHeaderEx()));
            this.m_Edit_HPHeader = ((new ColumnHeaderEx()));
            this.m_Edit_MPHeader = ((new ColumnHeaderEx()));

            this.m_Edit_NoHeader.Text = "No";
            this.m_Edit_NoHeader.Width = 30;

            this.m_Edit_PartHeader.Text = "攻撃方法(部位)";
            this.m_Edit_PartHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_Edit_PartHeader.Width = 160;
            m_Edit_PartHeader.Editable = true;

            this.m_Edit_HitHeader.Text = "命中力";
            this.m_Edit_HitHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Edit_HitHeader.Editable = true;

            this.m_Edit_DamageHeader.Text = "打撃点";
            this.m_Edit_DamageHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Edit_DamageHeader.Editable = true;

            this.m_Edit_DodgeHeader.Text = "回避力";
            this.m_Edit_DodgeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Edit_DodgeHeader.Editable = true;

            this.m_Edit_ProtectionHeader.Text = "防護点";
            this.m_Edit_ProtectionHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Edit_ProtectionHeader.Editable = true;

            this.m_Edit_HPHeader.Text = "HP";
            this.m_Edit_HPHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Edit_HPHeader.Editable = true;

            this.m_Edit_MPHeader.Text = "MP";
            this.m_Edit_MPHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Edit_MPHeader.Editable = true;

            ColumnHeaderEx[] colHeaderRegValue =
                {m_Edit_NoHeader,m_Edit_PartHeader,m_Edit_HitHeader,m_Edit_DamageHeader,m_Edit_DodgeHeader,m_Edit_ProtectionHeader,m_Edit_HPHeader,m_Edit_MPHeader };
            m_ListView.Columns.AddRange(colHeaderRegValue);

            m_ListViewSelectItem = new int[2];

            m_ListView.DoubleClick += new EventHandler(SelectListViewItem);
            m_ListView.LeaveEditBoxEvent(AutoEntry);

            ConfigLoad();

            //表示ステータスUI周り
            this.m_View_NoHeader = ((new ColumnHeaderEx()));
            this.m_View_PartHeader = ((new ColumnHeaderEx()));
            this.m_View_HitHeader = ((new ColumnHeaderEx()));
            this.m_View_DamageHeader = ((new ColumnHeaderEx()));
            this.m_View_DodgeHeader = ((new ColumnHeaderEx()));
            this.m_View_ProtectionHeader = ((new ColumnHeaderEx()));
            this.m_View_HPHeader = ((new ColumnHeaderEx()));
            this.m_View_MPHeader = ((new ColumnHeaderEx()));

            this.m_View_NoHeader.Text = "No";
            this.m_View_NoHeader.Width = 0;

            this.m_View_PartHeader.Text = "攻撃方法(部位)";
            this.m_View_PartHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_View_PartHeader.Width = 104;

            this.m_View_HitHeader.Text = "命中力";
            this.m_View_HitHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_View_HitHeader.Width = 50;

            this.m_View_DamageHeader.Text = "打撃点";
            this.m_View_DamageHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_View_DamageHeader.Width = 50;

            this.m_View_DodgeHeader.Text = "回避力";
            this.m_View_DodgeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_View_DodgeHeader.Width = 50;

            this.m_View_ProtectionHeader.Text = "防護点";
            this.m_View_ProtectionHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_View_ProtectionHeader.Width = 50;

            this.m_View_HPHeader.Text = "HP";
            this.m_View_HPHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_View_HPHeader.Width = 50;

            this.m_View_MPHeader.Text = "MP";
            this.m_View_MPHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_View_MPHeader.Width = 50;

            //画像ステータス周り
            this.m_Image_NoHeader = ((new ColumnHeaderEx()));
            this.m_Image_PartHeader = ((new ColumnHeaderEx()));
            this.m_Image_HitHeader = ((new ColumnHeaderEx()));
            this.m_Image_DamageHeader = ((new ColumnHeaderEx()));
            this.m_Image_DodgeHeader = ((new ColumnHeaderEx()));
            this.m_Image_ProtectionHeader = ((new ColumnHeaderEx()));
            this.m_Image_HPHeader = ((new ColumnHeaderEx()));
            this.m_Image_MPHeader = ((new ColumnHeaderEx()));

            this.m_Image_NoHeader.Text = "No";
            this.m_Image_NoHeader.Width = 0;

            this.m_Image_PartHeader.Text = "攻撃方法(部位)";
            this.m_Image_PartHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_Image_PartHeader.Width = 160;

            this.m_Image_HitHeader.Text = "命中力";
            this.m_Image_HitHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_Image_HitHeader.Width = 65;

            this.m_Image_DamageHeader.Text = "打撃点";
            this.m_Image_DamageHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Image_DamageHeader.Width = 65;

            this.m_Image_DodgeHeader.Text = "回避力";
            this.m_Image_DodgeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Image_DodgeHeader.Width = 65;

            this.m_Image_ProtectionHeader.Text = "防護点";
            this.m_Image_ProtectionHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Image_ProtectionHeader.Width = 65;

            this.m_Image_HPHeader.Text = "HP";
            this.m_Image_HPHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Image_HPHeader.Width = 55;

            this.m_Image_MPHeader.Text = "MP";
            this.m_Image_MPHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_Image_MPHeader.Width = 55;

        }

        //コンフィグに関係するもの読み込み
        public void ConfigLoad()
        {
            //コンフィグ読み込み
            m_ConfigCSV = new FileUtil();
            m_ConfigCSV.m_NowFile = m_ConfigCSV.GetReadFile(MainForm.m_ConfigCSV, "shift_jis");

            //初期化
            m_IntelligenceList = new List<string>();
            m_PerceptionList = new List<string>();
            m_ReactionList = new List<string>();
            m_Move_SpeedList = new List<string>();
            m_CategoryList = new List<string>();
            m_Weak_PointList = new List<string>();
            m_DeclarationList = new List<string>();
            m_LanguageList = new List<string>();
            m_ReferenceList = new List<string>();

            m_IntelligenceCombo.Items.Clear();
            m_PerceptionCombo.Items.Clear();
            m_ReactionCombo.Items.Clear();
            m_Weak_PointCombo.Items.Clear();
            m_Move_Speed1Combo.Items.Clear();
            m_Move_Speed2Combo.Items.Clear();
            m_CategoryCombo.Items.Clear();
            m_ReferenceCombo.Items.Clear();

            //設定ファイルなかったらリターン
            if (m_ConfigCSV.m_NowFile == null)
                return;


            //読み込みを適応
            m_ConfigCSV.m_CSVList = m_ConfigCSV.GetCSV(m_ConfigCSV.m_NowFile);
            for (int x = 0; x < m_ConfigCSV.m_CSVList[x].Count(); x++)
            {
                for (int y = 1; y < m_ConfigCSV.m_CSVList.Count; y++)
                {
                    if (m_ConfigCSV.m_CSVList[y][x] == "")
                        continue;

                    if (m_ConfigCSV.m_CSVList[0][x] == "知識")
                    {
                        m_IntelligenceList.Add(m_ConfigCSV.m_CSVList[y][x]);
                        m_IntelligenceCombo.Items.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                    else if (m_ConfigCSV.m_CSVList[0][x] == "知覚")
                    {
                        m_PerceptionList.Add(m_ConfigCSV.m_CSVList[y][x]);
                        m_PerceptionCombo.Items.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                    else if (m_ConfigCSV.m_CSVList[0][x] == "反応")
                    {
                        m_ReactionList.Add(m_ConfigCSV.m_CSVList[y][x]);
                        m_ReactionCombo.Items.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                    else if (m_ConfigCSV.m_CSVList[0][x] == "言語")
                    {
                        m_LanguageList.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                    else if (m_ConfigCSV.m_CSVList[0][x] == "弱点")
                    {
                        m_Weak_PointList.Add(m_ConfigCSV.m_CSVList[y][x]);
                        m_Weak_PointCombo.Items.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                    else if (m_ConfigCSV.m_CSVList[0][x] == "特殊能力")
                    {
                        m_DeclarationList.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                    else if (m_ConfigCSV.m_CSVList[0][x] == "移動")
                    {
                        m_Move_SpeedList.Add(m_ConfigCSV.m_CSVList[y][x]);
                        m_Move_Speed1Combo.Items.Add(m_ConfigCSV.m_CSVList[y][x]);
                        m_Move_Speed2Combo.Items.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                    else if (m_ConfigCSV.m_CSVList[0][x] == "分類")
                    {
                        m_CategoryList.Add(m_ConfigCSV.m_CSVList[y][x]);
                        m_CategoryCombo.Items.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                    else if (m_ConfigCSV.m_CSVList[0][x] == "出典")
                    {
                        m_ReferenceList.Add(m_ConfigCSV.m_CSVList[y][x]);
                        m_ReferenceCombo.Items.Add(m_ConfigCSV.m_CSVList[y][x]);
                    }
                }
            }

            m_ConfigCSV.m_NowFile.Close();
        }

        public void SelectListViewItem(object sender, EventArgs e)
        {
            m_ListViewSelectItem[0] = m_ListView.CurrentRowIndex;
            m_ListViewSelectItem[1] = m_ListView.CurrentColumnIndex;
        }

        //固定値自動記入
        public void AutoEntry(object sender, EventArgs e)
        {
            if (m_ListViewSelectItem[1] != 2 && m_ListViewSelectItem[1] != 4 && m_ListViewSelectItem[1] != 3)
                return;

            int l_Dodge = -1, l_Hit = -1, l_Damage = -1;
            string l_DodgeText, l_HitText,l_DamageText;
            try
            {
                l_Hit = int.Parse(m_ListView.Items[m_ListViewSelectItem[0]].SubItems[2].Text);
            }
            catch
            {
                l_Hit = -1;
            }

            if (l_Hit != -1)
            {
                l_HitText = m_ListView.Items[m_ListViewSelectItem[0]].SubItems[2].Text;
                m_ListView.Items[m_ListViewSelectItem[0]].SubItems[2].Text = l_HitText + "(" + MathUtil.AddStringNum(l_HitText, "7") + ")";
            }

            try
            {
                l_Dodge = int.Parse(m_ListView.Items[m_ListViewSelectItem[0]].SubItems[4].Text);
            }
            catch
            {
                l_Dodge = -1;
            }

            if (l_Dodge != -1)
            {
                l_DodgeText = m_ListView.Items[m_ListViewSelectItem[0]].SubItems[4].Text;
                m_ListView.Items[m_ListViewSelectItem[0]].SubItems[4].Text = l_DodgeText + "(" + MathUtil.AddStringNum(l_DodgeText, "7") + ")";
            }

            try
            {
                l_Damage = int.Parse(m_ListView.Items[m_ListViewSelectItem[0]].SubItems[3].Text);
            }
            catch
            {
                l_Damage = -1;
            }

            if (l_Damage != -1)
            {
                l_DamageText = m_ListView.Items[m_ListViewSelectItem[0]].SubItems[3].Text;
                m_ListView.Items[m_ListViewSelectItem[0]].SubItems[3].Text = "2d+" + l_DamageText;
            }

        }

        //表示用ステータスUI追加
        public void AddCloumnHeader(ListViewEx list)
        {
            ColumnHeaderEx[] l_ColHeaderRegValue =
                {m_View_NoHeader,m_View_PartHeader,m_View_HitHeader,m_View_DamageHeader,m_View_DodgeHeader,m_View_ProtectionHeader,m_View_HPHeader,m_View_MPHeader };
            list.Columns.AddRange(l_ColHeaderRegValue);
        }

        //画像用ステータスUI追加
        public void AddImageCloumnHeader(ListViewEx list)
        {
            ColumnHeaderEx[] l_ColHeaderRegValue =
                {m_Image_NoHeader,m_Image_PartHeader,m_Image_HitHeader,m_Image_DamageHeader,m_Image_DodgeHeader,m_Image_ProtectionHeader,m_Image_HPHeader,m_Image_MPHeader };
            list.Columns.AddRange(l_ColHeaderRegValue);
        }

        //言語追加
        public void AddLanguage(FlowLayoutPanel flow)
        {
            Panel l_Panel = new Panel();
            l_Panel.Width = 175;
            l_Panel.Height = 20;
            ComboBox l_Combo = new ComboBox();
            l_Combo.Width = 172;
            foreach (string lang in m_LanguageList)
            {
                l_Combo.Items.Add(lang);
            }

            l_Panel.Controls.Add(l_Combo);
            flow.Controls.Add(l_Panel);
        }

        //パネル削除
        public void DeletePanel(FlowLayoutPanel flow)
        {
            if (flow.Controls.Count != 0)
                flow.Controls.RemoveAt(flow.Controls.Count - 1);
        }

        //生息地追加
        public void AddHabitat(FlowLayoutPanel flow)
        {
            Panel l_Panel = new Panel();
            l_Panel.Width = 175;
            l_Panel.Height = 20;
            TextBox l_Text = new TextBox();
            l_Text.Width = 172;

            l_Panel.Controls.Add(l_Text);
            flow.Controls.Add(l_Panel);
        }

        //特技追加
        public void AddSpecal(FlowLayoutPanel flow)
        {
            Panel l_Panel = new Panel();
            l_Panel.Width = 472;
            l_Panel.Height = 85;
            l_Panel.BackColor = Color.White;

            ComboBox l_Declaration1 = new ComboBox();
            l_Declaration1.SetBounds(0, 0, 80, l_Declaration1.Height);

            ComboBox l_Declaration2 = new ComboBox();
            l_Declaration2.SetBounds(85, 0, 80, l_Declaration2.Height);

            ComboBox l_Declaration3 = new ComboBox();
            l_Declaration3.SetBounds(170, 0, 80, l_Declaration3.Height);
            foreach (string spe in m_DeclarationList)
            {
                l_Declaration1.Items.Add(spe);
                l_Declaration2.Items.Add(spe);
                l_Declaration3.Items.Add(spe);
            }

            l_Panel.Controls.Add(l_Declaration1);
            l_Panel.Controls.Add(l_Declaration2);
            l_Panel.Controls.Add(l_Declaration3);

            TextBox l_Name = new TextBox();
            l_Name.SetBounds(255, 0, 215, l_Name.Height);
            l_Panel.Controls.Add(l_Name);

            RichTextBox l_Effect = new RichTextBox();
            l_Effect.SetBounds(0, 25, 470, 60);
            l_Panel.Controls.Add(l_Effect);

            flow.Controls.Add(l_Panel);
        }

        //戦利品追加
        public void AddBooty(FlowLayoutPanel flow)
        {
            Panel l_Panel = new Panel();
            l_Panel.Width = 474;
            l_Panel.Height = 20;
            l_Panel.BackColor = Color.White;

            ComboBox l_DiceCombo = new ComboBox();
            l_DiceCombo.SetBounds(0, 0, 70, l_DiceCombo.Height);
            l_DiceCombo.Items.Add("");
            l_DiceCombo.Items.Add("自動");
            l_Panel.Controls.Add(l_DiceCombo);

            TextBox l_NameText = new TextBox();
            l_NameText.SetBounds(75, 0, l_Panel.Width / 3 + 36, l_NameText.Height);
            l_Panel.Controls.Add(l_NameText);

            int labelx = (l_NameText.Width + 75);
            Label l_MultipLabel = new Label();
            l_MultipLabel.SetBounds(labelx, 4, 15, l_MultipLabel.Height);
            l_MultipLabel.Text = "×";
            l_Panel.Controls.Add(l_MultipLabel);

            TextBox l_NumText1 = new TextBox();
            l_NumText1.SetBounds(labelx + 15, 0, 30, l_NameText.Height);
            l_Panel.Controls.Add(l_NumText1);

            TextBox l_GamelText1 = new TextBox();
            l_GamelText1.SetBounds(labelx + 50, 0, 60, l_NameText.Height);
            l_Panel.Controls.Add(l_GamelText1);

            Label l_GamelLabel1 = new Label();
            l_GamelLabel1.SetBounds(labelx + 110, 4, 15, l_GamelLabel1.Height);
            l_GamelLabel1.Text = "G";
            l_Panel.Controls.Add(l_GamelLabel1);

            ComboBox l_MateCombo2 = new ComboBox();
            l_MateCombo2.SetBounds(labelx + 125, 0, 80, l_MateCombo2.Height);
            l_MateCombo2.Items.Add("");
            l_MateCombo2.Items.Add("赤B");
            l_Panel.Controls.Add(l_MateCombo2);


            flow.Controls.Add(l_Panel);
        }

        //ステータス追加
        public void AddStatus()
        {
            ListViewItem listViewItem1 = new ListViewItem(new string[] {
            (m_ListView.Items.Count+1).ToString(),
            "ダブルクリックで編集",
            "n(n+7)",
            "2d+n",
            "n(n+7)",
            "n",
            "n",
            "n"}, -1, Color.Empty, SystemColors.InactiveBorder, null);

            m_ListView.Items.Add(listViewItem1);

        }

        //ステータス削除
        public void DeleteStatus()
        {
            if (m_ListView.Items.Count != 0)
            {
                //選択されてるかそうじゃないか
                if (m_ListView.FocusedItem != null)
                {
                    m_ListView.Items.Remove(m_ListView.FocusedItem);
                    //フォーカスされてるのを最後のやつにしておく
                    if (m_ListView.Items.Count != 0)
                        m_ListView.FocusedItem = m_ListView.Items[m_ListView.Items.Count - 1];

                    //消されたら番号整列
                    int i = 1;
                    foreach (ListViewItem item in m_ListView.Items)
                    {
                        item.Text = i.ToString();
                        i++;
                    }
                }
                else
                    m_ListView.Items.RemoveAt(m_ListView.Items.Count - 1);

            }

        }
    }

}

