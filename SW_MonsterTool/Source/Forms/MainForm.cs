using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SW_MonsterTool.Source.MUIAdd;
using SW_MonsterTool.Source.MDataView;
using SW_MonsterTool.Source.Utility.FileUtility;
using System.IO;
using SW_MonsterTool.Source.Utility.GoogleApi;
using SW_MonsterTool.Source.MData;
using SW_MonsterTool.Source.Utility.MathUtility;
using SW_MonsterTool.Source.Forms;


namespace SW_MonsterTool
{
    public partial class MainForm : Form
    {
        //モンスターUI追加処理用
        public static MonsterUIAdd m_MonsterUI;
        //モンスターデータ表示処理用
        public static MonsterReadView m_ViewData;
        //出力先File
        public FileUtil m_OutFile;

        //ウィンドウなどをフォームの真ん中に出すためのクラス
        private WindowCenter m_WCenter;

        //ConfigForm
        public ConfigForm m_URLForm;

        public static string m_MonsterSpread;
        //出力先
        public static string m_OutMonsterFile;
        //読み込み複数
        public static List<string> m_ReadMonsterFiles;
        public static string m_ConfigSpread;
        public static string m_ConfigCSV;

        //

        //ソートフィルター用
        public bool m_OrderApply = false;

        //一部ウィンドウの場合はアクティブ無効化しない為の処理
        private List<Form> m_ExActiveFroms;

        // カウンタ
        private int m_ClosedSubForms = 0;

        // 1回の生成で一度に生成する子フォーム数
        private const int m_OpenedSubForms = 1;

        public MainForm()
        {
            m_ReadMonsterFiles = new List<string>();
            m_ExActiveFroms = new List<Form>();

            //設定読み込みなども含めて一度起動して閉じる
            m_URLForm = new ConfigForm();
            m_URLForm.Close();

            m_WCenter = new WindowCenter();

            m_OutFile = new FileUtil();

            InitializeComponent();

            m_MonsterUI = new MonsterUIAdd(StatusListView1, IntelligenceBox1, PerceptionBox1, ReactionBox1, CategoryBox1, ReferenceBox1, Move_SpeedBox1, Move_SpeedBox2, Weak_PointBox1);

            m_ViewData = new MonsterReadView();
            m_ViewData.m_MenuOnly = this;
            //ヘッダーを追加
            m_MonsterUI.AddCloumnHeader(StatusListView2);

            //モンスターデータ読み取り
            int l_Read = m_ViewData.ReadMonsterData(m_ReadMonsterFiles);
            if (l_Read > 0)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "読み込めないファイルがありました\nファイルの名前や場所を変えていませんか?", "警告", MessageBoxButtons.OK);
            }
            //ビュータブのツリービューにデータ追加
            m_ViewData.AddTreeView(ref MonsterTreeView1);

            this.MaximizeBox = false;
        }

        //ステータスの文字編集
        private void StatusListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            StatusListView1.EditColumn();
        }

        //ステータスビューの幅固定化
        private void StatusListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = StatusListView1.Columns[e.ColumnIndex].Width;
        }

        private void StatusListView2_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = StatusListView2.Columns[e.ColumnIndex].Width;
        }

        //基礎ステータス
        private void StatusPlus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.AddStatus();
        }

        private void StatusMinus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.DeleteStatus();
        }

        //特殊能力
        private void SPAbilityPlus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.AddSpecal(SPAbilityPanel1);
        }

        private void SPAbilityMinus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.DeletePanel(SPAbilityPanel1);
        }

        //言語
        private void LangePlus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.AddLanguage(LanguagePanel1);
        }

        private void LangeMinus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.DeletePanel(LanguagePanel1);
        }

        //生息地
        private void HabitatPlus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.AddHabitat(HabitatPanel1);
        }

        private void HabitatMinus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.DeletePanel(HabitatPanel1);
        }

        //戦利品
        private void BootyPlus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.AddBooty(BootyPanel1);
        }

        private void BootyMinus_Click(object sender, EventArgs e)
        {
            m_MonsterUI.DeletePanel(BootyPanel1);
        }

        //連動する奴
        private void CategoryBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox l_Combo = (ComboBox)sender;

            //アンデットは例外なく穢れ固定
            if (l_Combo.Text == "アンデット")
            {
                KegareNumUpDown1.Value = 5;
            }//これらは穢れなし
            else if (l_Combo.Text == "魔動機" || l_Combo.Text == "魔法生物" || l_Combo.Text == "魔神")
            {
                KegareNumUpDown1.Value = 0;
            }
            else if (l_Combo.Text == "オリジナル")
            {
                ReferenceBox1.Text = "オリジナル";
                PageNumUpDown1.Value = -1;
            }

        }

        private void KegareNumUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (CategoryBox1.Text == "アンデット")
            {
                KegareNumUpDown1.Value = 5;
            }
            //これらは穢れなし
            else if (CategoryBox1.Text == "魔動機" || CategoryBox1.Text == "魔法生物" ||
                CategoryBox1.Text == "魔神")
            {
                KegareNumUpDown1.Value = 0;
            }
        }

        private void PageNumUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (ReferenceBox1.Text == "オリジナル")
            {
                PageNumUpDown1.Value = -1;
            }


        }

        private void ReferenceComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryBox1.Text == "オリジナル")
                ReferenceBox1.Text = "オリジナル";

            if (ReferenceBox1.Text == "オリジナル")
            {
                PageNumUpDown1.Value = -1;
            }

        }

        private void CorePartBox1_Click(object sender, EventArgs e)
        {
            CorePartBox1.Items.Clear();
            CorePartBox1.Items.Add("なし");
            foreach (ListViewItem item in StatusListView1.Items)
            {
                CorePartBox1.Items.Add(item.SubItems[1].Text);
            }
        }

        private void Weak_PointBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Weak_PointBox1.Text == "なし")
            {
                Weak_PointNumUpDown1.Value = -1;
                Weak_Point_ValueNumUpDown1.Value = -1;
            }
        }

        //弱点値
        private void Weak_Point_ValueNumUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Weak_PointBox1.Text == "なし")
                Weak_Point_ValueNumUpDown1.Value = -1;
        }

        //弱点（属性の方）
        private void Weak_PointNumUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Weak_PointBox1.Text == "なし")
                Weak_PointNumUpDown1.Value = -1;
        }

        private void Move_SpeedBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Move_SpeedBox1.Text == "-" || Move_SpeedBox1.Text == "＊")
                Move_SpeedNumUpDown1.Value = -1;
        }

        private void Move_SpeedNumUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Move_SpeedBox1.Text == "-" || Move_SpeedBox1.Text == "＊")
                Move_SpeedNumUpDown1.Value = -1;
        }

        private void Move_SpeedBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Move_SpeedBox2.Text == "-" || Move_SpeedBox2.Text == "＊")
                Move_SpeedNumUpDown2.Value = -1;
        }

        private void Move_SpeedNumUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (Move_SpeedBox2.Text == "-" || Move_SpeedBox2.Text == "＊")
                Move_SpeedNumUpDown2.Value = -1;

        }

        private void Life_ResistanceNumUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Life_ResistanceNumUpDown2.Value = Life_ResistanceNumUpDown1.Value + 7;
        }

        private void Spirit_ResistanceNumUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Spirit_ResistanceNumUpDown2.Value = Spirit_ResistanceNumUpDown1.Value + 7;
        }

        private void Life_ResistanceNumUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (CategoryBox1.Text != "オリジナル" && ReferenceBox1.Text != "オリジナル")
            {
                Life_ResistanceNumUpDown2.Value = Life_ResistanceNumUpDown1.Value + 7;
            }
        }

        private void Spirit_ResistanceNumUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (CategoryBox1.Text != "オリジナル" || ReferenceBox1.Text != "オリジナル")
            {
                Spirit_ResistanceNumUpDown2.Value = Spirit_ResistanceNumUpDown1.Value + 7;
            }
        }

        //追加ボタン
        private void MonsterDataAddButon1_Click(object sender, EventArgs e)
        {
            string l_Where = m_OutMonsterFile.Split('.').Last();

            if (m_OutFile.IsFileLocked(m_OutMonsterFile))
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "出力ファイル先が設定されていません\nまたはどこかが利用しています", "問題", MessageBoxButtons.OK);
                return;
            }

            if (l_Where.Equals("CSV", StringComparison.OrdinalIgnoreCase))
            {
                List<string> l_Result = PushCSV();

                m_OutFile.CSVWrite(m_OutMonsterFile, l_Result, true, "shift_jis");

                if (SpreadSheetAddCheck1.Checked)
                {
                    if (!SpreadSheetUtil.IsSpreadSheet(m_MonsterSpread))
                    {
                        m_WCenter.SetHook(this);
                        MessageBox.Show(this, "出力スプレッドシートが設定されていません\nまたはURLに問題があります", "問題", MessageBoxButtons.OK);
                        return;
                    }

                    SpreadSheetUtil.SetSpreadSheet(m_MonsterSpread);
                    SpreadSheetUtil.WriteSheet(l_Result);
                }
            }
            else if (l_Where.Equals("json", StringComparison.OrdinalIgnoreCase))
            {
                //Json追加するには一回読み込んで追加して出力の手順

                m_OutFile.m_NowFile = m_OutFile.GetReadFile(m_OutMonsterFile, "utf-8");

                List<JsonMonsterData> l_MonsterJsons = m_OutFile.GetJson(m_OutFile.m_NowFile);

                Monster l_SetMonster = new Monster();

                //追加前のモンスター全部入ってるやつ
                List<Monster> l_Result = new List<Monster>();

                foreach (JsonMonsterData jmon in l_MonsterJsons)
                {
                    l_SetMonster = Monster.ConvertFromJsonData(jmon);

                    l_Result.Add(l_SetMonster);

                    l_SetMonster = new Monster();
                }

                Monster l_AddMonster = new Monster();

                Monster.FormToMonster(new MonsterFormTools(MonsterNameBox1, CategoryBox1, LevelNumUpDown, ReferenceBox1, IntelligenceBox1,
                    PerceptionBox1, ReactionBox1, KegareNumUpDown1, PageNumUpDown1, Name_RecognitionNumUpDown1, Weak_Point_ValueNumUpDown1,
                    Weak_PointBox1, Weak_PointNumUpDown1, Preemptive_ValueNumUpDown1, Life_ResistanceNumUpDown1, Spirit_ResistanceNumUpDown1,
                    Move_SpeedBox1, Move_SpeedBox2, Move_SpeedNumUpDown1, Move_SpeedNumUpDown2, LanguagePanel1, HabitatPanel1, CorePartBox1,
                    StatusListView1, SPAbilityPanel1, BootyPanel1, DescriptionTextBox1, new TextBox()), m_WCenter, this, true, ref l_AddMonster);

                l_Result.Add(l_AddMonster);

                List<JsonMonsterData> l_OutJson = new List<JsonMonsterData>();
                foreach (Monster monster in l_Result)
                {
                    l_OutJson.Add(Monster.ConvertToJson(monster));
                }

                m_OutFile.WriteJson(l_OutJson, m_OutMonsterFile);

            }

            if (m_OutFile.m_NowFile != null)
                m_OutFile.m_NowFile.Close();

            m_WCenter.SetHook(this);
            MessageBox.Show(this, "出力が終わりました", "送信", MessageBoxButtons.OK);

            m_ViewData.ChangeOrder(ref MonsterTreeView1);
        }

        private List<string> PushCSV()
        {

            string l_Name = MonsterNameBox1.Text;
            if (l_Name == "")
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "名前を入力してください", "問題", MessageBoxButtons.OK);
                return null;
            }
            if (l_Name.Contains("\""))
                l_Name = l_Name.Replace("\"", "\"\"");

            string l_Reaction = ReactionBox1.Text;
            if (l_Reaction.Contains("\""))
                l_Reaction = l_Reaction.Replace("\"", "\"\"");

            string l_Category = CategoryBox1.Text;
            if (l_Category.Contains("\""))
                l_Category = l_Category.Replace("\"", "\"\"");

            string l_Kegare = KegareNumUpDown1.Value.ToString();

            string l_Reference = ReferenceBox1.Text;
            if (l_Reference.Contains("\""))
                l_Reference = l_Reference.Replace("\"", "\"\"");

            string l_Page = PageNumUpDown1.Value.ToString();
            if (l_Page == "-1")
                l_Page = "なし";

            string l_Chimei = Name_RecognitionNumUpDown1.Value.ToString();

            string l_Intelligence = IntelligenceBox1.Text;
            if (l_Intelligence.Contains("\""))
                l_Intelligence = l_Intelligence.Replace("\"", "\"\"");

            string l_Weak_Point_Value = Weak_Point_ValueNumUpDown1.Value.ToString();
            if (l_Weak_Point_Value == "-1")
                l_Weak_Point_Value = "なし";

            string l_Perception = PerceptionBox1.Text;
            if (l_Perception.Contains("\""))
                l_Perception = l_Perception.Replace("\"", "\"\"");

            string l_Sensei = Preemptive_ValueNumUpDown1.Value.ToString();

            string l_Element = Weak_PointBox1.Text;
            if (l_Element.Contains("\""))
                l_Element = l_Element.Replace("\"", "\"\"");

            string l_WeakDamage = Weak_PointNumUpDown1.Value.ToString();
            if (l_WeakDamage == "-1")
                l_WeakDamage = "なし";

            string l_Seimei = Life_ResistanceNumUpDown1.Value.ToString();

            string l_Seimei_K = Life_ResistanceNumUpDown2.Value.ToString();

            string l_Seisin = Spirit_ResistanceNumUpDown1.Value.ToString();

            string l_Seisin_K = Spirit_ResistanceNumUpDown2.Value.ToString();

            string l_Idou1 = Move_SpeedBox1.Text;
            if (l_Idou1 == "")
                l_Idou1 = "二足";
            if (l_Idou1.Contains("\""))
                l_Idou1 = l_Idou1.Replace("\"", "\"\"");

            string l_Idou1_N = Move_SpeedNumUpDown1.Value.ToString();
            if (l_Idou1_N == "-1" && l_Idou1 == "＊")
                l_Idou1_N = "元参照";
            else if (l_Idou1_N == "-1")
                l_Idou1_N = "なし";

            string l_Idou2 = Move_SpeedBox2.Text;
            if (l_Idou2 == "")
                l_Idou2 = "二足";
            if (l_Idou2.Contains("\""))
                l_Idou2 = l_Idou2.Replace("\"", "\"\"");

            string l_Idou2_N = Move_SpeedNumUpDown2.Value.ToString();
            if (l_Idou2_N == "-1" && l_Idou2 == "＊")
                l_Idou2_N = "元参照";
            else if (l_Idou2_N == "-1")
                l_Idou2_N = "なし";

            if (l_Reaction == "" || l_Category == "" || l_Reference == "" || l_Intelligence == "" ||
             l_Perception == "" || l_Element == "")
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "未選択項目があります\n移動の未選択は二足になります", "問題", MessageBoxButtons.OK);
                return null;
            }

            if (l_Chimei == "0" || l_Weak_Point_Value == "0" || l_Sensei == "0" || l_WeakDamage == "0" ||
                l_Seimei_K == "0" || l_Seisin_K == "0")
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "0ではいけない値があります", "問題", MessageBoxButtons.OK);
                return null;
            }

            if (l_Idou2_N == "0" || l_Idou1_N == "0")
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "移動力0はありません\n移動しなければ-を選択してください", "問題", MessageBoxButtons.OK);
                return null;
            }

            string l_Lang = "なし";
            if (LanguagePanel1.Controls.Count != 0)
            {
                int i = 0;
                foreach (Panel panel in LanguagePanel1.Controls)
                {
                    ComboBox l_combo = (ComboBox)panel.Controls[0];
                    if (l_combo.Text != "" && l_combo.Text != "なし")
                    {
                        if (i == 0)
                            l_Lang = "";

                        string l_LangText = l_combo.Text;
                        if (l_LangText.Contains("\""))
                            l_LangText = l_LangText.Replace("\"", "\"\"");

                        l_Lang += "\"" + l_LangText + "\"" + ",";
                        i++;
                    }
                }
            }

            string l_Seisokuti = "不明";
            if (HabitatPanel1.Controls.Count != 0)
            {
                int i = 0;
                foreach (Panel panel in HabitatPanel1.Controls)
                {
                    TextBox l_text = (TextBox)panel.Controls[0];
                    if (l_text.Text != "")
                    {
                        if (i == 0)
                            l_Seisokuti = "";

                        string l_HabiText = l_text.Text;
                        if (l_HabiText.Contains("\""))
                            l_HabiText = l_HabiText.Replace("\"", "\"\"");

                        l_Seisokuti += "\"" + l_HabiText + "\"" + ",";
                        i++;
                    }
                }
            }
            string l_Core = CorePartBox1.Text;
            if (l_Core == "")
                l_Core = "なし";
            if (l_Core.Contains("\""))
                l_Core = l_Core.Replace("\"", "\"\"");

            string l_BuiName = "";
            string l_Hit = "";
            string l_Damage = "";
            string l_Kaihi = "";
            string l_Bougo = "";
            string l_HP = "";
            string l_MP = "";
            foreach (ListViewItem item in StatusListView1.Items)
            {
                string l_LBui = item.SubItems[1].Text;
                if (l_LBui.Contains("\""))
                    l_LBui = l_LBui.Replace("\"", "\"\"");

                string l_LHit = item.SubItems[2].Text;
                if (l_LHit.Contains("\""))
                    l_LHit = l_LHit.Replace("\"", "\"\"");

                string l_LDama = item.SubItems[3].Text;
                if (l_LDama.Contains("\""))
                    l_LDama = l_LDama.Replace("\"", "\"\"");

                string l_LDodge = item.SubItems[4].Text;
                if (l_LDodge.Contains("\""))
                    l_LDodge = l_LDodge.Replace("\"", "\"\"");

                string l_LPro = item.SubItems[5].Text;
                if (l_LPro.Contains("\""))
                    l_LPro = l_LPro.Replace("\"", "\"\"");

                string l_LHP = item.SubItems[6].Text;
                if (l_LHP.Contains("\""))
                    l_LHP = l_LHP.Replace("\"", "\"\"");

                string l_LMP = item.SubItems[7].Text;
                if (l_LMP.Contains("\""))
                    l_LMP = l_LMP.Replace("\"", "\"\"");


                l_BuiName = l_BuiName + "\"" + l_LBui + "\"" + ",";

                l_Hit = l_Hit + "\"" + l_LHit + "\"" + ",";

                l_Damage = l_Damage + "\"" + l_LDama + "\"" + ",";

                l_Kaihi = l_Kaihi + "\"" + l_LDodge + "\"" + ",";

                l_Bougo = l_Bougo + "\"" + l_LPro + "\"" + ",";

                l_HP = l_HP + "\"" + l_LPro + "\"" + ",";

                l_MP = l_MP + "\"" + l_LMP + "\"" + ",";
            }

            //ステータスが追加されてない場合
            if (l_BuiName == "")
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "ステータスを設定してください\nダブルクリックで編集できます\n", "問題", MessageBoxButtons.OK);
                return null;
            }

            string l_SpeName = "";
            string l_SpeDeclaration_1 = "";
            string l_SpeDeclaration_2 = "";
            string l_SpeDeclaration_3 = "";
            string l_Spe = "";
            if (SPAbilityPanel1.Controls.Count != 0)
            {
                foreach (Panel panel in SPAbilityPanel1.Controls)
                {
                    ComboBox l_Declaration1 = (ComboBox)panel.Controls[0];
                    ComboBox l_Declaration2 = (ComboBox)panel.Controls[1];
                    ComboBox l_Declaration3 = (ComboBox)panel.Controls[2];
                    TextBox l_text = (TextBox)panel.Controls[3];
                    RichTextBox richText = (RichTextBox)panel.Controls[4];

                    string l_LSpName = l_text.Text;
                    if (l_LSpName.Contains("\""))
                        l_LSpName = l_LSpName.Replace("\"", "\"\"");

                    string l_LSpDec1 = l_Declaration1.Text;
                    if (l_LSpDec1.Contains("\""))
                        l_LSpDec1 = l_LSpDec1.Replace("\"", "\"\"");

                    string l_LSpDec2 = l_Declaration2.Text;
                    if (l_LSpDec2.Contains("\""))
                        l_LSpDec2 = l_LSpDec2.Replace("\"", "\"\"");

                    string l_LSpDec3 = l_Declaration3.Text;
                    if (l_LSpDec3.Contains("\""))
                        l_LSpDec3 = l_LSpDec3.Replace("\"", "\"\"");

                    string l_LSpEffe = richText.Text;
                    if (l_LSpEffe.Contains("\""))
                        l_LSpEffe = l_LSpEffe.Replace("\"", "\"\"");

                    l_SpeName += "\"" + l_LSpName + "\"" + ",";
                    l_SpeDeclaration_1 += "\"" + l_LSpDec1 + "\"" + ",";
                    l_SpeDeclaration_2 += "\"" + l_LSpDec2 + "\"" + ",";
                    l_SpeDeclaration_3 += "\"" + l_LSpDec3 + "\"" + ",";
                    l_Spe += "\"" + l_LSpEffe + "\"" + ",";

                }

            }
            else
            {
                l_SpeName = "なし";
                l_SpeDeclaration_1 = "なし";
                l_SpeDeclaration_2 = "なし";
                l_SpeDeclaration_3 = "なし";
                l_Spe = "なし";
            }

            string l_Dice = "";
            string l_SenName = "";
            string l_SenNum = "";
            string l_Gamel = "";
            string l_Mate = "";
            if (BootyPanel1.Controls.Count != 0)
            {
                foreach (Panel panel in BootyPanel1.Controls)
                {
                    ComboBox l_CBDice = (ComboBox)panel.Controls[0];
                    TextBox l_TBName = (TextBox)panel.Controls[1];
                    TextBox l_TBNum = (TextBox)panel.Controls[3];
                    TextBox l_TBGamel = (TextBox)panel.Controls[4];
                    ComboBox l_CBMate = (ComboBox)panel.Controls[6];

                    string l_DiceText = l_CBDice.Text;
                    if (l_DiceText.Contains("\""))
                        l_DiceText = l_DiceText.Replace("\"", "\"\"");

                    string l_BootyNameText = l_TBName.Text;
                    if (l_BootyNameText.Contains("\""))
                        l_BootyNameText = l_BootyNameText.Replace("\"", "\"\"");

                    string l_BootyNumText = l_TBNum.Text;
                    if (l_BootyNumText.Contains("\""))
                        l_BootyNumText = l_BootyNumText.Replace("\"", "\"\"");

                    string l_GamelText = l_TBGamel.Text;
                    if (l_GamelText.Contains("\""))
                        l_GamelText = l_GamelText.Replace("\"", "\"\"");

                    string l_MateText = l_CBMate.Text;
                    if (l_MateText.Contains("\""))
                        l_MateText = l_MateText.Replace("\"", "\"\"");

                    if (l_BootyNameText == "")
                        l_BootyNameText = "なし";
                    if (l_BootyNumText == "")
                        l_BootyNumText = "0";
                    if (l_GamelText == "")
                        l_GamelText = "0";
                    if (l_MateText == "")
                        l_MateText = "なし";

                    l_Dice += "\"" + l_DiceText + "\"" + ",";
                    l_SenName += "\"" + l_BootyNameText + "\"" + ",";
                    l_SenNum += "\"" + l_BootyNumText + "\"" + ",";
                    l_Gamel += "\"" + l_GamelText + "\"" + ",";
                    l_Mate += "\"" + l_MateText + "\"" + ",";

                }
            }
            else
            {
                l_Dice = "なし";
                l_SenName = "なし";
                l_SenNum = "0";
                l_Gamel = "0";
                l_Mate = "なし";
            }

            //解説なんもない場合は詳細不明と追加
            string l_Kaisetu = DescriptionTextBox1.Text;
            if (l_Kaisetu == "")
                l_Kaisetu = "詳細不明";
            if (l_Kaisetu.Contains("\""))
                l_Kaisetu = l_Kaisetu.Replace("\"", "\"\"");

            //レベルは1からなので警告はしない
            string l_Level = LevelNumUpDown.Value.ToString();

            //チャットパレットはここでは追加しないので空欄だけ用意する

            //\nで吐き出すと勝手に改行されると思うので置換する
            //読み込むときは逆のことをしよう
            l_Kaisetu = l_Kaisetu.Replace("\n", ReplacementList.m_LineFeed_R);
            l_Spe = l_Spe.Replace("\n", ReplacementList.m_LineFeed_R);


            List<string> l_Result = new List<string>();
            l_Result.Add("名前" + "," + "\"" + l_Name + "\"" + "," + "," + "反応" + "," + "\"" + l_Reaction + "\"");
            l_Result.Add("分類" + "," + "\"" + l_Category + "\"" + "," + "," + "穢れ" + "," + l_Kegare);
            l_Result.Add("出典" + "," + "\"" + l_Reference + "\"" + "," + l_Page + "," + "知名度" + "," + l_Chimei);
            l_Result.Add("知能" + "," + "\"" + l_Intelligence + "\"" + "," + "," + "弱点値" + "," + l_Weak_Point_Value);
            l_Result.Add("知覚" + "," + "\"" + l_Perception + "\"" + "," + "," + "先制値" + "," + l_Sensei);
            l_Result.Add("弱点" + "," + "\"" + l_Element + "\"" + "," + l_WeakDamage);
            l_Result.Add("生命抵抗力" + "," + l_Seimei + "," + l_Seimei_K);
            l_Result.Add("精神抵抗力" + "," + l_Seisin + "," + l_Seisin_K);
            l_Result.Add("移動速度" + "," + "\"" + l_Idou1 + "\"" + "," + l_Idou1_N + "," + "\"" + l_Idou2 + "\"" + "," + l_Idou2_N);
            l_Result.Add("言語" + "," + l_Lang);
            l_Result.Add("生息地" + "," + l_Seisokuti);
            l_Result.Add("基礎ステータス" + "," + "コア部位" + "," + l_Core);
            l_Result.Add("部位" + "," + l_BuiName);
            l_Result.Add("命中力" + "," + l_Hit);
            l_Result.Add("打撃点" + "," + l_Damage);
            l_Result.Add("回避力" + "," + l_Kaihi);
            l_Result.Add("防護点" + "," + l_Bougo);
            l_Result.Add("HP" + "," + l_HP);
            l_Result.Add("MP" + "," + l_MP);
            l_Result.Add("特殊能力" + ",");
            l_Result.Add("名称" + "," + l_SpeName);
            l_Result.Add("宣言1" + "," + l_SpeDeclaration_1);
            l_Result.Add("宣言2" + "," + l_SpeDeclaration_2);
            l_Result.Add("宣言3" + "," + l_SpeDeclaration_3);
            l_Result.Add("効果" + "," + l_Spe);
            l_Result.Add("戦利品" + ",");
            l_Result.Add("ダイス目" + "," + l_Dice);
            l_Result.Add("名称" + "," + l_SenName);
            l_Result.Add("個数" + "," + l_SenNum);
            l_Result.Add("値段" + "," + l_Gamel);
            l_Result.Add("マテリアルカード" + "," + l_Mate);
            l_Result.Add("解説" + "," + "\"" + l_Kaisetu + "\"");
            l_Result.Add("レベル" + "," + l_Level + "," + "画像" + ",");
            l_Result.Add("チャットパレット" + "," + ",");
            l_Result.Add("END" + ",");

            return l_Result;
        }

        //初期化ボタン
        private void ClearButton1_Click(object sender, EventArgs e)
        {
            MonsterNameBox1.Clear();
            ReactionBox1.Text = "";
            CategoryBox1.Text = "";
            KegareNumUpDown1.Value = 0;
            ReferenceBox1.Text = "";
            PageNumUpDown1.Value = 0;
            Name_RecognitionNumUpDown1.Value = 0;
            IntelligenceBox1.Text = "";
            Weak_Point_ValueNumUpDown1.Value = 0;
            Weak_PointBox1.Text = "";
            PerceptionBox1.Text = "";
            Preemptive_ValueNumUpDown1.Value = 0;
            Weak_PointNumUpDown1.Value = 0;
            Life_ResistanceNumUpDown1.Value = 0;
            Life_ResistanceNumUpDown2.Value = 0;
            Spirit_ResistanceNumUpDown1.Value = 0;
            Spirit_ResistanceNumUpDown2.Value = 0;
            Move_SpeedBox1.Text = "";
            Move_SpeedBox2.Text = "";
            Move_SpeedNumUpDown1.Value = 0;
            Move_SpeedNumUpDown2.Value = 0;
            LanguagePanel1.Controls.Clear();
            HabitatPanel1.Controls.Clear();
            SPAbilityPanel1.Controls.Clear();
            BootyPanel1.Controls.Clear();
            CorePartBox1.Text = "";
            StatusListView1.Items.Clear();
            DescriptionTextBox1.Text = "";
            LevelNumUpDown.Value = 1;

        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                // 子フォームのインスタンスを作成する
                m_URLForm = new ConfigForm();

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                m_URLForm.Closed += new EventHandler(this.SubFormClosed);
                m_URLForm.Deactivate += new EventHandler(this.SubFormDeactivated);
                m_WCenter.SetHook(this);
                m_URLForm.Show(this);
            }

            // 親フォーム自身を無効化しておく
            this.Enabled = false;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = false;
            }
        }

        //フォームが閉じられるときの処理
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_URLForm.IsDisposed) return;
            if (!m_URLForm.IsChangeValue()) return;

            m_WCenter.SetHook(this);
            m_URLForm.BringToFront();
            m_URLForm.IsClose(e);

        }

        private void config再読み込みToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //設定読み込みなども含めて一度起動して閉じる
            m_URLForm = new ConfigForm();
            m_URLForm.Close();
            m_MonsterUI.ConfigLoad();
            int l_Read = m_ViewData.ReadMonsterData(m_ReadMonsterFiles);
            if (l_Read > 0)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "読み込めないファイルがありました\nファイルの名前や場所を変えていませんか?", "警告", MessageBoxButtons.OK);
            }
            m_ViewData.ChangeOrder(ref MonsterTreeView1);
        }

        private void LevelNumUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (m_ViewData.m_Heading)
            {
                if (e.Node.Parent == null) return;

                m_ViewData.SelectMonster(e.Node.Text);

                SelectMonsterView();
            }
            else
            {
                m_ViewData.SelectMonster(e.Node.Text);

                SelectMonsterView();
            }
        }

        //モンスターを編集画面にセット(Import用)
        public void SelectMonsterSet(Monster viewMonster)
        {
            Monster l_ViewMonster = viewMonster;


            MonsterNameBox1.Text = l_ViewMonster.m_Name;

            CategoryBox1.Text = l_ViewMonster.m_Category;

            LevelNumUpDown.Value = int.Parse(l_ViewMonster.m_Level);

            ReferenceBox1.Text = l_ViewMonster.m_Reference;

            IntelligenceBox1.Text = l_ViewMonster.m_Intelligence;

            PerceptionBox1.Text = l_ViewMonster.m_Perception;

            ReactionBox1.Text = l_ViewMonster.m_Reaction;

            KegareNumUpDown1.Value = int.Parse(l_ViewMonster.m_Kegare);

            //ページ記載ある場合
            if (l_ViewMonster.m_Page != "")
            {
                PageNumUpDown1.Value = int.Parse(l_ViewMonster.m_Page);
            }
            else
                PageNumUpDown1.Value = -1;

            Name_RecognitionNumUpDown1.Value = int.Parse(l_ViewMonster.m_Recognition);

            Preemptive_ValueNumUpDown1.Value = int.Parse(l_ViewMonster.m_Preemptive_Value);

            //生命、精神
            string l_Seimei = l_ViewMonster.m_Life_Resistance;
            Life_ResistanceNumUpDown1.Value = int.Parse(l_Seimei);

            string l_Seisin = l_ViewMonster.m_Spirit_Resistance;
            Spirit_ResistanceNumUpDown1.Value = int.Parse(l_Seisin);

            //弱点
            if (l_ViewMonster.m_Weak_Point.Contains("+"))
            {
                string[] l_Weak_Point = l_ViewMonster.m_Weak_Point.Split('+');
                Weak_PointBox1.Text = l_Weak_Point[0];
                Weak_PointNumUpDown1.Value = int.Parse(l_Weak_Point[1]);
            }
            else
            {
                Weak_PointBox1.Text = "なし";
                Weak_PointNumUpDown1.Value = -1;
            }

            //弱点値(弱点あるかないかで判別してるので後にやる)
            if (l_ViewMonster.m_Weak_Point_Value != "なし")
                Weak_Point_ValueNumUpDown1.Value = int.Parse(l_ViewMonster.m_Weak_Point_Value);
            else
                Weak_Point_ValueNumUpDown1.Value = -1;


            //移動速度
            //方法1側が移動速度記入されてるか
            if (l_ViewMonster.m_Move_Speed[0].Move_ORG == "-")
            {
                Move_SpeedBox1.Text = "-";
                Move_SpeedNumUpDown1.Value = -1;
            }
            else
            {
                if (l_ViewMonster.m_Move_Speed[0].Move_ORG == "＊")
                {
                    Move_SpeedBox1.Text = "＊";
                    Move_SpeedNumUpDown1.Value = -1;
                }
                else
                {
                    Move_SpeedBox1.Text = l_ViewMonster.m_Move_Speed[0].Move_ORG;
                    Move_SpeedNumUpDown1.Value = int.Parse(l_ViewMonster.m_Move_Speed[0].Speed_ORG);
                }
            }

            //方法2側が移動速度記入されてるか
            if (l_ViewMonster.m_Move_Speed[1].Move_ORG == "-")
            {
                Move_SpeedBox2.Text = "-";
                Move_SpeedNumUpDown2.Value = -1;
            }
            else
            {
                if (l_ViewMonster.m_Move_Speed[1].Move_ORG == "＊")
                {
                    Move_SpeedBox2.Text = "＊";
                    Move_SpeedNumUpDown2.Value = -1;
                }
                else
                {

                    Move_SpeedBox2.Text = l_ViewMonster.m_Move_Speed[1].Move_ORG;
                    Move_SpeedNumUpDown2.Value = int.Parse(l_ViewMonster.m_Move_Speed[1].Speed_ORG);
                }
            }

            //言語
            //パネルリセット
            LanguagePanel1.Controls.Clear();
            //言語持ちかどうか
            if (l_ViewMonster.m_Language.Count() != 0)
            {
                int l_Combonum = 0;

                //言語分回す
                foreach (string lang in l_ViewMonster.m_Language)
                {
                    m_MonsterUI.AddLanguage(LanguagePanel1);
                    ComboBox l_Combo = (ComboBox)LanguagePanel1.Controls[l_Combonum].Controls[0];

                    l_Combo.Text = lang;
                    l_Combonum++;
                }
            }

            //生息地
            //パネルリセット
            HabitatPanel1.Controls.Clear();
            //生息地あるかどうか
            if (l_ViewMonster.m_Habitat.Count() != 0)
            {
                int l_SeisokuNum = 0;

                //生息地分回す
                foreach (string habitat in l_ViewMonster.m_Habitat)
                {
                    m_MonsterUI.AddHabitat(HabitatPanel1);
                    TextBox l_Text = (TextBox)HabitatPanel1.Controls[l_SeisokuNum].Controls[0];

                    l_Text.Text = habitat;
                    l_SeisokuNum++;
                }
            }

            //基本ステータス
            //パネルリセット
            StatusListView1.Items.Clear();

            int l_StatusNum = 0;
            foreach (Monster.Status status in l_ViewMonster.m_Statuses)
            {
                m_MonsterUI.AddStatus();
                StatusListView1.Items[l_StatusNum].SubItems[1].Text = status.Part;
                StatusListView1.Items[l_StatusNum].SubItems[2].Text = status.HitPower;
                StatusListView1.Items[l_StatusNum].SubItems[3].Text = status.Damage;
                StatusListView1.Items[l_StatusNum].SubItems[4].Text = status.Dodge;
                StatusListView1.Items[l_StatusNum].SubItems[5].Text = status.Protection;
                StatusListView1.Items[l_StatusNum].SubItems[6].Text = status.HP;
                StatusListView1.Items[l_StatusNum].SubItems[7].Text = status.MP;

                l_StatusNum++;
            }
            //コア部位
            CorePartBox1.Text = l_ViewMonster.m_CorePart;

            //特殊能力
            //パネルリセット
            SPAbilityPanel1.Controls.Clear();
            if (l_ViewMonster.m_Specals.Count != 0)
            {
                int l_SpeNum = 0;

                //能力分回す
                foreach (Monster.Specal spe in l_ViewMonster.m_Specals)
                {
                    if (spe.Name == "なし") break;

                    m_MonsterUI.AddSpecal(SPAbilityPanel1);
                    ComboBox l_Declaration1 = (ComboBox)SPAbilityPanel1.Controls[l_SpeNum].Controls[0];
                    ComboBox l_Declaration2 = (ComboBox)SPAbilityPanel1.Controls[l_SpeNum].Controls[1];
                    ComboBox l_Declaration3 = (ComboBox)SPAbilityPanel1.Controls[l_SpeNum].Controls[2];
                    TextBox l_Text = (TextBox)SPAbilityPanel1.Controls[l_SpeNum].Controls[3];
                    RichTextBox l_RichText = (RichTextBox)SPAbilityPanel1.Controls[l_SpeNum].Controls[4];

                    l_Declaration1.Text = spe.Declaration1;
                    l_Declaration2.Text = spe.Declaration2;
                    l_Declaration3.Text = spe.Declaration3;
                    l_Text.Text = spe.Name;
                    l_RichText.Text = spe.Effect.Replace(ReplacementList.m_LineFeed_R, "\r\n");

                    l_SpeNum++;
                }
            }

            //戦利品
            //パネルリセット
            BootyPanel1.Controls.Clear();
            if (l_ViewMonster.m_Booty.Count != 0)
            {
                int l_SenriNum = 0;

                //能力分回す
                foreach (Monster.Booty booty in l_ViewMonster.m_Booty)
                {
                    m_MonsterUI.AddBooty(BootyPanel1);
                    ComboBox l_Dice = (ComboBox)BootyPanel1.Controls[l_SenriNum].Controls[0];
                    TextBox l_sName = (TextBox)BootyPanel1.Controls[l_SenriNum].Controls[1];
                    TextBox l_sNum = (TextBox)BootyPanel1.Controls[l_SenriNum].Controls[3];
                    TextBox l_Gamel = (TextBox)BootyPanel1.Controls[l_SenriNum].Controls[4];
                    ComboBox l_Mate = (ComboBox)BootyPanel1.Controls[l_SenriNum].Controls[6];

                    l_Dice.Text = booty.Dice;
                    l_sName.Text = booty.Name;
                    l_sNum.Text = booty.Num;
                    l_Gamel.Text = booty.Gamel;
                    l_Mate.Text = booty.Material;

                    l_SenriNum++;
                }
            }

            //解説
            DescriptionTextBox1.Text = l_ViewMonster.m_Description.Replace(ReplacementList.m_LineFeed_R, "\r\n");

        }

        //モンスタービューの表示処理
        void SelectMonsterView()
        {
            if (m_ViewData.m_SelectMonster == null)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "処理にエラーが起きてうまく選択できませんでした。", "警告", MessageBoxButtons.OK);
                return;
            }

            Monster l_SelectMonster = m_ViewData.m_SelectMonster;
            const int l_FontY = 12;
            int l_PanelSetY = 0;

            int l_RowNum = 0;
            int l_RowSpaceNum = 0;

            View_MonsterNameLabel1.Text = l_SelectMonster.m_Name;

            View_LevelLabel1.Text = l_SelectMonster.m_Level;
            int l_Total = LevelBackPanel1.Width;
            int l_X = (l_Total - View_LevelLabel1.Width) / 2;
            View_LevelLabel1.SetBounds(l_X, View_LevelLabel1.Bounds.Y, View_LevelLabel1.Width, View_LevelLabel1.Height);

            View_IntelligenceLabel1.Width = View_IntelligenceLabel1.PreferredWidth;
            View_IntelligenceText1.Text = l_SelectMonster.m_Intelligence;
            View_IntelligenceText1.SetBounds(View_IntelligenceLabel1.Width, View_IntelligenceText1.Bounds.Y, View_IntelligenceText1.PreferredWidth, View_IntelligenceText1.Height);

            View_PerceptionLabel1.SetBounds(View_IntelligenceText1.Bounds.X + View_IntelligenceText1.Width + 5, View_PerceptionLabel1.Bounds.Y, View_PerceptionLabel1.PreferredWidth, View_PerceptionLabel1.Height);
            View_PerceptionText1.Text = l_SelectMonster.m_Perception;
            View_PerceptionText1.SetBounds(View_PerceptionLabel1.Bounds.X + View_PerceptionLabel1.Width, View_PerceptionText1.Bounds.Y, View_PerceptionText1.PreferredWidth, View_PerceptionText1.Height);

            View_ReactionLabel1.SetBounds(View_PerceptionText1.Bounds.X + View_PerceptionText1.Width + 5, View_ReactionLabel1.Bounds.Y, View_ReactionLabel1.PreferredWidth, View_ReactionLabel1.Height);
            View_ReactionText1.Text = l_SelectMonster.m_Reaction;
            View_ReactionText1.SetBounds(View_ReactionLabel1.Bounds.X + View_ReactionLabel1.Width, View_ReactionText1.Bounds.Y, View_ReactionText1.PreferredWidth, View_ReactionText1.Height);

            View_KegareLabel1.SetBounds(View_ReactionText1.Bounds.X + View_ReactionText1.Width + 5, View_KegareLabel1.Bounds.Y, View_KegareLabel1.PreferredWidth, View_KegareLabel1.Height);
            View_KegareText1.Text = l_SelectMonster.m_Kegare;
            View_KegareText1.SetBounds(View_KegareLabel1.Bounds.X + View_KegareLabel1.Width + 5, View_KegareText1.Bounds.Y, View_KegareText1.PreferredWidth, View_KegareText1.Height);
            l_RowNum += 1;
            l_RowSpaceNum = l_RowNum + 1;

            //言語
            View_LanguageLabel1.Width = View_LanguageLabel1.PreferredWidth;
            View_LanguageText1.Text = "";
            int l_NowL_w = 0;
            int l_LColumn = 0;
            foreach (var lang in l_SelectMonster.m_Language.Select((v, i) => new { v, i }))
            {
                Label l_Width = new Label();
                l_Width.Text = lang.v + "、";

                //最後かどうか
                if (lang.i + 1 == l_SelectMonster.m_Language.Count)
                {
                    //言語足していってパネルの横超えそうになったら
                    if (MonsterSummaryPanel1.Width <= (l_Width.PreferredWidth + l_NowL_w))
                    {
                        View_LanguageText1.Text = View_LanguageText1.Text.Remove(View_LanguageText1.Text.Length - 1, 1);
                        View_LanguageText1.Text += "\n" + lang.v;
                    }
                    else
                        View_LanguageText1.Text += lang.v;
                }
                else
                {
                    //言語足していってパネルの横超えそうになったら
                    if (MonsterSummaryPanel1.Width <= (l_Width.PreferredWidth + l_NowL_w))
                    {
                        View_LanguageText1.Text = View_LanguageText1.Text.Remove(View_LanguageText1.Text.Length - 1, 1);
                        View_LanguageText1.Text += "\n" + lang.v + "、";
                        l_LColumn += 1;
                        l_NowL_w = 0;
                    }
                    else
                        View_LanguageText1.Text += lang.v + "、";
                }
                l_NowL_w += l_Width.PreferredWidth;
            }
            View_LanguageText1.SetBounds(View_LanguageLabel1.Width, View_LanguageText1.Bounds.Y, View_LanguageText1.PreferredWidth, View_LanguageText1.PreferredHeight);
            l_RowNum += 1;
            l_RowSpaceNum = l_RowNum + 1;

            //生息地
            View_HabitatLabel1.SetBounds(View_HabitatLabel1.Bounds.X, View_LanguageText1.PreferredHeight + (l_FontY * l_RowNum) + (5 * l_RowSpaceNum), View_HabitatLabel1.PreferredWidth, View_HabitatLabel1.Height);
            View_HabitatText1.Text = "";
            View_HabitatText1.SetBounds(View_HabitatText1.Bounds.X, View_LanguageText1.PreferredHeight + (l_FontY * l_RowNum) + (5 * l_RowSpaceNum), View_HabitatText1.PreferredWidth, View_HabitatText1.Height);
            int l_SNow_w = 0;
            int l_SColumn = 0;
            foreach (var seisoku in l_SelectMonster.m_Habitat.Select((v, i) => new { v, i }))
            {
                Label l_Width = new Label();
                l_Width.Text = seisoku.v + "、";

                //最後かどうか
                if (seisoku.i + 1 == l_SelectMonster.m_Habitat.Count)
                {
                    //言語足していってパネルの横超えそうになったら
                    if (MonsterSummaryPanel1.Width <= (l_Width.PreferredWidth + l_SNow_w))
                    {
                        View_HabitatText1.Text = View_HabitatText1.Text.Remove(View_HabitatText1.Text.Length - 1, 1);
                        View_HabitatText1.Text += "\n" + seisoku.v;
                    }
                    else
                        View_HabitatText1.Text += seisoku.v;
                }
                else
                {
                    //言語足していってパネルの横超えそうになったら
                    if (MonsterSummaryPanel1.Width <= (l_Width.PreferredWidth + l_SNow_w))
                    {
                        View_HabitatText1.Text = View_HabitatText1.Text.Remove(View_HabitatText1.Text.Length - 1, 1);
                        View_HabitatText1.Text += "\n" + seisoku.v + "、";
                        l_SColumn += 1;
                        l_SNow_w = 0;
                    }
                    else
                        View_HabitatText1.Text += seisoku.v + "、";
                }
                l_SNow_w += l_Width.PreferredWidth;
            }
            View_HabitatText1.SetBounds(View_HabitatLabel1.Width, View_HabitatText1.Bounds.Y, View_HabitatText1.PreferredWidth, View_HabitatText1.PreferredHeight);
            l_RowNum += 1;
            l_RowSpaceNum = l_RowNum + 1;

            //
            View_NRandWPVLabel1.SetBounds(View_NRandWPVLabel1.Bounds.X, View_LanguageText1.PreferredHeight + View_HabitatText1.PreferredHeight + (l_FontY * l_RowNum) + (4 * l_RowSpaceNum), View_NRandWPVLabel1.PreferredWidth, View_NRandWPVLabel1.Height);
            View_NRandWPVText1.Text = l_SelectMonster.m_Recognition + " / " + l_SelectMonster.m_Weak_Point_Value;
            View_NRandWPVText1.SetBounds(View_NRandWPVText1.Bounds.X, View_LanguageText1.PreferredHeight + View_HabitatText1.PreferredHeight + (l_FontY * l_RowNum) + (4 * l_RowSpaceNum), View_NRandWPVText1.PreferredWidth, View_NRandWPVText1.Height);

            View_WeakPointLabel1.SetBounds(View_WeakPointLabel1.Bounds.X, View_LanguageText1.PreferredHeight + View_HabitatText1.PreferredHeight + (l_FontY * l_RowNum) + (4 * l_RowSpaceNum), View_WeakPointLabel1.PreferredWidth, View_WeakPointLabel1.Height);
            View_WeakPointText1.Text = l_SelectMonster.m_Weak_Point;
            View_WeakPointText1.SetBounds(View_WeakPointText1.Bounds.X, View_LanguageText1.PreferredHeight + View_HabitatText1.PreferredHeight + (l_FontY * l_RowNum) + (4 * l_RowSpaceNum), View_WeakPointText1.PreferredWidth, View_WeakPointText1.Height);

            //
            View_Preemptive_ValueLabel1.SetBounds(View_Preemptive_ValueLabel1.Bounds.X, View_NRandWPVLabel1.Bounds.Y + l_FontY * 2, View_Preemptive_ValueLabel1.PreferredWidth, View_Preemptive_ValueLabel1.Height);
            View_Preemptive_ValueText1.Text = l_SelectMonster.m_Preemptive_Value;
            View_Preemptive_ValueText1.SetBounds(View_Preemptive_ValueText1.Bounds.X, View_NRandWPVLabel1.Bounds.Y + l_FontY * 2, View_Preemptive_ValueText1.PreferredWidth, View_Preemptive_ValueText1.Height);

            View_Move_SpeedLabel1.SetBounds(View_Move_SpeedLabel1.Bounds.X, View_NRandWPVLabel1.Bounds.Y + l_FontY * 2, View_Move_SpeedLabel1.PreferredWidth, View_Move_SpeedLabel1.Height);
            View_Move_SpeedText1.Text = l_SelectMonster.m_Move_Speed[0].Speed_View + l_SelectMonster.m_Move_Speed[0].Move_View + " / " + l_SelectMonster.m_Move_Speed[1].Speed_View + l_SelectMonster.m_Move_Speed[1].Move_View;
            View_Move_SpeedText1.SetBounds(View_Move_SpeedText1.Bounds.X, View_NRandWPVLabel1.Bounds.Y + l_FontY * 2, View_Move_SpeedText1.PreferredWidth, View_Move_SpeedText1.Height);

            //
            View_Life_ResistanceLabel1.SetBounds(View_Life_ResistanceLabel1.Bounds.X, View_Preemptive_ValueLabel1.Bounds.Y + l_FontY * 2, View_Life_ResistanceLabel1.PreferredWidth, View_Life_ResistanceLabel1.Height);
            View_Life_ResistanceText1.Text = l_SelectMonster.m_Life_Resistance + " / (" + MathUtil.AddStringNum(l_SelectMonster.m_Life_Resistance, "7") + ")";
            View_Life_ResistanceText1.SetBounds(View_Life_ResistanceText1.Bounds.X, View_Preemptive_ValueLabel1.Bounds.Y + l_FontY * 2, View_Life_ResistanceText1.PreferredWidth, View_Life_ResistanceText1.Height);

            View_Spirit_ResistanceLabel1.SetBounds(View_Spirit_ResistanceLabel1.Bounds.X, View_Preemptive_ValueLabel1.Bounds.Y + l_FontY * 2, View_Spirit_ResistanceLabel1.PreferredWidth, View_Spirit_ResistanceLabel1.Height);
            View_Spirit_ResistanceText1.Text = l_SelectMonster.m_Spirit_Resistance + " / (" + MathUtil.AddStringNum(l_SelectMonster.m_Spirit_Resistance, "7") + ")";
            View_Spirit_ResistanceText1.SetBounds(View_Spirit_ResistanceText1.Bounds.X, View_Preemptive_ValueLabel1.Bounds.Y + l_FontY * 2, View_Spirit_ResistanceText1.PreferredWidth, View_Spirit_ResistanceText1.Height);

            //panelサイズ
            MonsterSummaryPanel1.Height = MonsterSummaryPanel1.PreferredSize.Height + 6;

            //ステータス
            l_PanelSetY += MonsterSummaryPanel1.Height + 40;
            StatusListView2.SetBounds(StatusListView2.Bounds.X, l_PanelSetY, StatusListView2.Width, StatusListView2.Height);
            StatusListView2.Items.Clear();
            foreach (var status in l_SelectMonster.m_Statuses)
            {
                ListViewItem listViewItem1 = new ListViewItem(new string[] {
            (StatusListView2.Items.Count+1).ToString(),
            status.Part,
            status.HitPower,
            status.Damage,
            status.Dodge,
            status.Protection,
            status.HP,
            status.MP}, -1, Color.Empty, SystemColors.InactiveBorder, null);

                StatusListView2.Items.Add(listViewItem1);
            }

            var l_Rect = StatusListView2.GetItemRect(StatusListView2.Items.Count - 1);
            StatusListView2.ClientSize = new Size(StatusListView2.ClientSize.Width, l_Rect.Top + l_Rect.Height);
            StatusListView2.Width = MonsterSummaryPanel1.Width;
            //
            l_PanelSetY += StatusListView2.Height + 7;

            if (l_SelectMonster.m_Statuses.Count == 1)
            {
                PartPanel1.Hide();
            }
            else
            {
                PartPanel1.Show();
                PartPanel1.SetBounds(PartPanel1.Bounds.X, l_PanelSetY, PartPanel1.Width, PartPanel1.Height);
                View_PartNumText1.Text = l_SelectMonster.m_Statuses.Count.ToString();
                View_CorePartNumText1.Text = l_SelectMonster.m_CorePart;
                l_PanelSetY += PartPanel1.Height + 7;
            }

            //戦利品
            panel10.SetBounds(panel10.Bounds.X, l_PanelSetY, panel10.Width, panel10.Height);
            View_BootyPanel1.Controls.Clear();
            foreach (var booty in l_SelectMonster.m_Booty)
            {
                Panel l_Panel = new Panel();
                l_Panel.Width = View_BootyPanel1.Width - 9;
                l_Panel.Height = 12;
                l_Panel.BackColor = Color.White;

                int l_Dice_end = 50;
                Label l_Dice = new Label();
                l_Dice.SetBounds(0, 0, l_Dice_end, l_Dice.Height);
                l_Dice.Text = booty.Dice;
                l_Panel.Controls.Add(l_Dice);

                Label l_Text = new Label();
                l_Text.Text = booty.Name;
                l_Text.Width = l_Text.PreferredWidth;
                l_Text.SetBounds(l_Dice_end, 0, l_Text.Width, l_Text.Height);
                l_Panel.Controls.Add(l_Text);

                if (booty.Name != "なし")
                {
                    int l_Labelx = (l_Text.Width + l_Dice_end);
                    Label l_Label = new Label();
                    l_Label.SetBounds(l_Labelx, 0, 15, l_Label.Height);
                    l_Label.Text = "×";
                    l_Panel.Controls.Add(l_Label);

                    Label l_Text2 = new Label();
                    l_Text2.Text = booty.Num;
                    l_Text2.Width = l_Text2.PreferredWidth;
                    l_Text2.SetBounds(l_Labelx + 15, 0, l_Text2.Width, l_Text.Height);
                    l_Panel.Controls.Add(l_Text2);

                    Label l_Text3 = new Label();
                    l_Text3.Text = booty.Gamel;
                    l_Text3.Width = l_Text3.PreferredWidth;
                    l_Text3.SetBounds(l_Labelx + 45, 0, l_Text3.Width, l_Text.Height);
                    l_Panel.Controls.Add(l_Text3);

                    Label l_Label2 = new Label();
                    l_Label2.Text = "G";
                    l_Label2.Width = l_Label2.PreferredWidth;
                    l_Label2.SetBounds(l_Text3.Bounds.X + l_Text3.Width, 0, l_Label2.Width, l_Label2.Height);
                    l_Panel.Controls.Add(l_Label2);

                    Label l_Mate = new Label();
                    l_Mate.Text = booty.Material;
                    l_Mate.Width = l_Mate.PreferredWidth;
                    l_Mate.SetBounds(l_Labelx + 115, 0, l_Mate.Width, l_Mate.Height);
                    l_Panel.Controls.Add(l_Mate);
                }

                View_BootyPanel1.Controls.Add(l_Panel);

            }
            panel10.Height = BootyBackPanel1.Height + 5 + (17 * l_SelectMonster.m_Booty.Count);
            //

            //特殊
            View_Special_AbilityPanel1.Controls.Clear();
            int l_Panel_h = 0;
            if (l_SelectMonster.m_Specals[0].Name != "なし")
                foreach (var specal in l_SelectMonster.m_Specals)
                {
                    Panel l_Panel = new Panel();
                    l_Panel.Width = View_Special_AbilityPanel1.Width - 6;
                    l_Panel.BackColor = Color.White;

                    Label l_Name = new Label();
                    l_Name.Text = "名称 ： " + specal.Name;
                    l_Name.Size = l_Name.PreferredSize;
                    l_Name.SetBounds(0, 4, l_Name.Width, l_Name.Height + 4);
                    l_Panel.Controls.Add(l_Name);

                    string l_Spe;
                    if (specal.Declaration2 == "")
                        l_Spe = "";
                    else
                        l_Spe = " / " + specal.Declaration2;

                    if (specal.Declaration3 == "")
                        l_Spe += "";
                    else
                        l_Spe += " / " + specal.Declaration3;

                    Label l_Declaration = new Label();
                    l_Declaration.Text = "能力の型 ： " + specal.Declaration1 + l_Spe;
                    l_Declaration.Size = l_Declaration.PreferredSize;
                    l_Declaration.SetBounds(0, l_Name.Height + 4, l_Declaration.Width, l_Declaration.Height + 4);

                    l_Panel.Controls.Add(l_Declaration);

                    int l_Rich_y = l_Name.Height + 4 + l_Declaration.Height;
                    TextBox l_Rich = new TextBox();
                    l_Rich.Multiline = true;
                    l_Rich.ReadOnly = true;
                    l_Rich.Text = specal.Effect;

                    l_Rich.SetBounds(0, l_Rich_y, l_Panel.Width, l_Rich.Height);

                    int l_RichLine = l_Rich.GetLineFromCharIndex(l_Rich.TextLength) + 1;

                    l_Rich.Height = l_Rich.Font.Height * l_RichLine + l_Rich.Margin.Vertical;

                    l_Panel.Height = l_Rich_y + l_Rich.Height;
                    l_Panel_h += l_Panel.Height + 8;
                    l_Panel.Controls.Add(l_Rich);

                    View_Special_AbilityPanel1.Controls.Add(l_Panel);
                }
            else
            {
                Label l_None = new Label();

                l_None.Text = "なし";

                View_Special_AbilityPanel1.Controls.Add(l_None);
                l_Panel_h = 18;
            }

            View_Special_AbilityPanel1.Height = l_Panel_h;
            panel11.Height = Special_AbilityBackPanel1.Height + View_Special_AbilityPanel1.Height;
            //

            l_PanelSetY += panel10.Height + 7;
            View_DescriptionTextBox1.SetBounds(View_DescriptionTextBox1.Bounds.X, l_PanelSetY, View_DescriptionTextBox1.Width, View_DescriptionTextBox1.Height);
            View_DescriptionTextBox1.Text = l_SelectMonster.m_Description + "\r\n(出典：" + l_SelectMonster.m_Reference + " / P." + l_SelectMonster.m_Page + ")";

            int l_Line = View_DescriptionTextBox1.GetLineFromCharIndex(View_DescriptionTextBox1.TextLength) + 1;

            View_DescriptionTextBox1.Height = View_DescriptionTextBox1.Font.Height * l_Line + View_DescriptionTextBox1.Margin.Vertical;

            l_PanelSetY += View_DescriptionTextBox1.Height + 7;

            tabContol.Update();
        }

        //絞り込み
        private void button10_Click(object sender, EventArgs e)
        {

            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                // 子フォームのインスタンスを作成する
                FilterForm l_SubForm = new FilterForm(this);

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_SubForm.Closed += new EventHandler(this.SubFormClosed);
                l_SubForm.Deactivate += new EventHandler(this.SubFormDeactivated);
                m_WCenter.SetHook(this);
                l_SubForm.Show(this);
            }

            // 親フォーム自身を無効化しておく
            this.Enabled = false;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = false;
            }
        }

        //並び替え
        private void button11_Click(object sender, EventArgs e)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                // 子フォームのインスタンスを作成する
                SortForm l_SubForm = new SortForm(this);

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_SubForm.Closed += new EventHandler(this.SubFormClosed);
                l_SubForm.Deactivate += new EventHandler(this.SubFormDeactivated);
                m_WCenter.SetHook(this);
                l_SubForm.Show(this);
            }

            // 親フォーム自身を無効化しておく
            this.Enabled = false;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = false;
            }
        }

        // 子フォームが閉じられた時
        private void SubFormClosed(object sender, EventArgs e)
        {
            // 閉じられた子フォームの数+1
            m_ClosedSubForms++;

            // すべてのフォームが閉じられたら
            if (m_ClosedSubForms >= m_OpenedSubForms)
            {
                // 親フォーム自身の無効化を解除してカウンタをリセット
                this.Enabled = true;
                foreach (Form form in m_ExActiveFroms)
                {
                    form.Enabled = true;
                }
                m_ClosedSubForms = 0;
            }

            //設定読み込みなども含めて一度起動して閉じる
            m_URLForm = new ConfigForm();
            m_URLForm.Close();
            m_MonsterUI.ConfigLoad();
            int l_Read = m_ViewData.ReadMonsterData(m_ReadMonsterFiles);
            if (l_Read > 0)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "読み込めないファイルがありました\nファイルの名前や場所を変えていませんか?", "警告", MessageBoxButtons.OK);
            }
            m_ViewData.ChangeOrder(ref MonsterTreeView1);
        }

        // 例外子フォームが閉じられた時
        private void ExSubFormClosed(object sender, EventArgs e)
        {
            // 閉じられた子フォームの数+1
            m_ClosedSubForms++;

            // すべてのフォームが閉じられたら
            if (m_ClosedSubForms >= m_OpenedSubForms)
            {
                // 親フォーム自身の無効化を解除してカウンタをリセット
                this.Enabled = true;
                m_ExActiveFroms.Remove(((Form)sender));
                m_ClosedSubForms = 0;
            }
        }

        //サブフォームから外れた時
        private void SubFormDeactivated(object sender, EventArgs e)
        {
            this.Enabled = true;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = true;
            }
        }

        //メインフォームがアクティブの場合
        private void Form1_Activated(object sender, EventArgs e)
        {
            //サブフォーム開いている間
            if ((this.OwnedForms.Count()) != 0)
            {
                bool l_Ex = false;
                foreach (Form form in m_ExActiveFroms)
                {
                    if (form.Name == OwnedForms.Last().Name)
                    {
                        l_Ex = true;
                        break;
                    }
                }

                //例外フォームはカウントしない
                if (l_Ex)
                    return;

                //例外じゃないやつは最後に開いてる
                OwnedForms.Last().BringToFront();
                this.Enabled = false;
                foreach (Form form in m_ExActiveFroms)
                {
                    form.Enabled = false;
                }
            }
            else
            {
                //それ以外

            }

        }


        //ユドナリウム出力
        private void udonariumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                // 子フォームのインスタンスを作成する
                UdonariumForm l_SubForm = new UdonariumForm();

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_SubForm.Closed += new EventHandler(this.SubFormClosed);
                l_SubForm.Deactivate += new EventHandler(this.SubFormDeactivated);
                m_WCenter.SetHook(this);

                l_SubForm.MaximizeBox = false;

                l_SubForm.Show(this);
            }

            // 親フォーム自身を無効化しておく
            this.Enabled = false;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = false;
            }
        }

        //ココフォリア出力
        private void cCFOLIAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_WCenter.SetHook(this);
            DialogResult result = MessageBox.Show("ココフォリアの外部データ対応はベータ版です\nココフォリア側の更新によってデータが入らなくなる可能性があります",
                "質問",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {

                // 指定した数分子フォームを開く
                for (int i = 0; i < m_OpenedSubForms; i++)
                {
                    // 子フォームのインスタンスを作成する
                    CcfoliaForm l_SubForm = new CcfoliaForm();

                    // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                    l_SubForm.Closed += new EventHandler(this.SubFormClosed);
                    l_SubForm.Deactivate += new EventHandler(this.SubFormDeactivated);
                    m_WCenter.SetHook(this);

                    l_SubForm.MaximizeBox = false;

                    l_SubForm.Show(this);
                }

                // 親フォーム自身を無効化しておく
                this.Enabled = false;
                foreach (Form form in m_ExActiveFroms)
                {
                    form.Enabled = false;
                }
            }
        }

        private void データ編集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                // 子フォームのインスタンスを作成する
                DataCustom l_DataCustomSelect = new DataCustom();
                DataCustomMain l_DataCustomMain = new DataCustomMain();

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_DataCustomSelect.Closed += new EventHandler(this.SubFormClosed);
                l_DataCustomMain.Closed += new EventHandler(this.SubFormClosed);

                l_DataCustomSelect.Deactivate += new EventHandler(this.SubFormDeactivated);
                l_DataCustomMain.Deactivate += new EventHandler(this.SubFormDeactivated);
                m_WCenter.SetHook(this);
                //データセレクトフォームをセット
                l_DataCustomMain.DataCustomSelectGet(l_DataCustomSelect);
                l_DataCustomSelect.DataCustomSelectGet(l_DataCustomMain);

                l_DataCustomMain.MaximizeBox = false;
                l_DataCustomSelect.MaximizeBox = false;


                l_DataCustomSelect.Show(this);
                l_DataCustomMain.Show(this);
            }

            // 親フォーム自身を無効化しておく
            this.Enabled = false;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = false;
            }
        }

        private void configCSV編集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                // 子フォームのインスタンスを作成する
                ConfigCSVEdit l_ConfigCSVEdit = new ConfigCSVEdit();

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_ConfigCSVEdit.Closed += new EventHandler(this.SubFormClosed);

                l_ConfigCSVEdit.Deactivate += new EventHandler(this.SubFormDeactivated);

                l_ConfigCSVEdit.MaximizeBox = false;

                m_WCenter.SetHook(this);

                l_ConfigCSVEdit.Show(this);
            }

            // 親フォーム自身を無効化しておく
            this.Enabled = false;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = false;
            }

        }

        private void 詳細ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                // 子フォームのインスタンスを作成する
                DetailsForm l_Details = new DetailsForm();

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_Details.Closed += new EventHandler(this.ExSubFormClosed);
                l_Details.MaximizeBox = false;


                m_WCenter.SetHook(this);

                l_Details.Show(this);

                //例外フォームに追加
                m_ExActiveFroms.Add(l_Details);

            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                //子フォームのインスタンスを作成する
                DataImport l_DataImport = new DataImport();

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_DataImport.Closed += new EventHandler(this.ExSubFormClosed);
                l_DataImport.m_Main = this;
                l_DataImport.MaximizeBox = false;

                m_WCenter.SetHook(this);

                l_DataImport.Show(this);

                //例外フォームに追加
                m_ExActiveFroms.Add(l_DataImport);

            }
        }

        //ツリービューの編集メニュー関数
        public void EditMenuProcess(Monster m)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                // 子フォームのインスタンスを作成する
                DataCustom l_DataCustomSelect = new DataCustom();
                DataCustomMain l_DataCustomMain = new DataCustomMain();

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_DataCustomSelect.Closed += new EventHandler(this.SubFormClosed);
                l_DataCustomMain.Closed += new EventHandler(this.SubFormClosed);

                l_DataCustomSelect.Deactivate += new EventHandler(this.SubFormDeactivated);
                l_DataCustomMain.Deactivate += new EventHandler(this.SubFormDeactivated);
                m_WCenter.SetHook(this);
                //データセレクトフォームをセット
                l_DataCustomMain.DataCustomSelectGet(l_DataCustomSelect);
                l_DataCustomSelect.DataCustomSelectGet(l_DataCustomMain);

                l_DataCustomMain.MaximizeBox = false;
                l_DataCustomSelect.MaximizeBox = false;

                //編集したいモンスターをセット
                l_DataCustomSelect.SetCustomMonster(m);

                l_DataCustomSelect.Show(this);
                l_DataCustomMain.Show(this);
            }

            // 親フォーム自身を無効化しておく
            this.Enabled = false;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = false;
            }
        }

        //ツリービューの削除メニュー関数
        public void DeleteMenuProcess()
        {
            if (MonsterTreeView1.SelectedNode == null) return;
            if (MonsterTreeView1.SelectedNode.Parent == null) return;
            DialogResult l_Delete = MessageBox.Show("このモンスターを削除しますか？\n操作は戻せません",
                "質問",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (l_Delete == DialogResult.Yes)
            {
                //消すデータセット
                //選択時の項目名を保存
                TreeNode l_SelectNode = MonsterTreeView1.SelectedNode;

                //選択したモンスターの位置記録
                int l_SelectNumber = -1;
                //選択したモンスターのファイル場所、保存するため用
                string l_SeveURL = "";

                //選択したモンスター検索して反映
                m_ViewData.SelectMonster(l_SelectNode.Text, ref l_SelectNumber);
                l_SeveURL = m_ViewData.m_SelectMonster.m_File_Source_URL;

                //ファイルが使われているかチェックして使われてたら削除を中止
                if (m_OutFile.IsFileLocked(l_SeveURL))
                {
                    m_WCenter.SetHook(this); ;
                    MessageBox.Show(this, "出力ファイル先がどこかで利用されています\n削除を中止します", "問題", MessageBoxButtons.OK);
                    return;
                }

                m_ViewData.m_Monsters.RemoveAt(l_SelectNumber);
                MonsterTreeView1.SelectedNode.Remove();

                //消したデータが最後尾の場合セレクトナンバーも変わる処理
                if (l_SelectNumber > m_ViewData.m_Monsters.Count - 1)
                    l_SelectNumber = m_ViewData.m_Monsters.Count - 1;

                //変更保存

                List<string> l_Result = new List<string>();

                foreach (Monster m in m_ViewData.m_Monsters)
                {
                    l_Result.AddRange(Monster.MonsterToCSV(m));
                }

                m_OutFile.CSVWrite(l_SeveURL, l_Result, false, "shift_jis");

                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "削除が完了しました", "送信", MessageBoxButtons.OK);

                m_ViewData.ChangeOrder(ref MonsterTreeView1);
            }
            else if (l_Delete == DialogResult.No)
            {

            }
        }

        public void ChatPaletteProcess(Monster m)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                //子フォームのインスタンスを作成する
                ChatPaletteEdit l_ChatPalette = new ChatPaletteEdit(m, m_ViewData);

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_ChatPalette.Closed += new EventHandler(this.ExSubFormClosed);
                l_ChatPalette.MaximizeBox = false;

                m_WCenter.SetHook(this);

                l_ChatPalette.Show(this);

                //例外フォームに追加
                m_ExActiveFroms.Add(l_ChatPalette);

            }
        }


        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void MonsterTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (m_ViewData.m_Heading)
                {
                    if (e.Node.Parent == null) return;

                    m_ViewData.SelectMonster(e.Node.Text);
                    MonsterTreeView1.SelectedNode = e.Node;

                    SelectMonsterView();
                }
                else
                {
                    m_ViewData.SelectMonster(e.Node.Text);
                    MonsterTreeView1.SelectedNode = e.Node;

                    SelectMonsterView();
                }
            }
        }

        private void 画像出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 指定した数分子フォームを開く
            for (int i = 0; i < m_OpenedSubForms; i++)
            {
                //子フォームのインスタンスを作成する
                ImageExportForm l_ImageForm = new ImageExportForm();

                // 子フォームのClosedイベントをSubFormClosed()にハンドルして開く
                l_ImageForm.Closed += new EventHandler(this.ExSubFormClosed);
                l_ImageForm.m_Main = this;
                l_ImageForm.MaximizeBox = false;

                m_WCenter.SetHook(this);

                l_ImageForm.Show(this);

            }

            // 親フォーム自身を無効化しておく
            this.Enabled = false;
            foreach (Form form in m_ExActiveFroms)
            {
                form.Enabled = false;
            }
        }
    }
}
