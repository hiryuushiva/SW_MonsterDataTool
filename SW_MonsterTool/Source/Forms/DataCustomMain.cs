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


namespace SW_MonsterTool
{
    public partial class DataCustomMain : Form
    {
        private WindowCenter m_WCenter;

        private DataCustom m_DataCustom;

        public MonsterUIAdd m_Monster;
        public MonsterReadView m_ViewTab;

        public List<string> m_MonsterCSV;

        public bool m_FirstStart = false;

        public DataCustomMain()
        {
            m_FirstStart = false;

            m_MonsterCSV = new List<string>();

            m_WCenter = new WindowCenter();

            InitializeComponent();
            //フォームが閉じられた時の処理追加
            this.FormClosing += Form_Closing;

            m_Monster = new MonsterUIAdd(listViewEx1, m_INTcombo, m_ChikakuCombo, m_ReactionCombo, m_Bunrui, m_Shutten, m_Idou1, m_Idou2, m_Zyakuten);
        }

        //-----UIイベント処理

        // フォームが閉じられたときに行う処理書く
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            //選択フォームも一緒に閉じる
            m_DataCustom.Close();
        }

        private void DataCustomMain_Activated(object sender, EventArgs e)
        {
            if (m_DataCustom.TopMost)
            {
                m_DataCustom.TopMost = false;
            }
        }

        public void DataCustomSelectGet(DataCustom data)
        {
            m_DataCustom = data;
        }

        private void listViewEx1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listViewEx1.EditColumn();
        }

        private void listViewEx1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listViewEx1.Columns[e.ColumnIndex].Width;
        }

        //基礎ステータス
        private void button4_Click(object sender, EventArgs e)
        {
            m_Monster.AddStatus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_Monster.DeleteStatus();
        }

        //特殊能力
        private void button1_Click(object sender, EventArgs e)
        {
            m_Monster.AddSpecal(flowLayoutPanel3);
        }

        //特殊能力
        private void button2_Click(object sender, EventArgs e)
        {
            m_Monster.DeletePanel(flowLayoutPanel3);
        }

        private void LangePlus_Click(object sender, EventArgs e)
        {
            m_Monster.AddLanguage(flowLayoutPanel1);
        }

        private void LangeMinus_Click(object sender, EventArgs e)
        {
            m_Monster.DeletePanel(flowLayoutPanel1);
        }

        private void SeisokuPlus_Click(object sender, EventArgs e)
        {
            m_Monster.AddHabitat(flowLayoutPanel2);
        }

        private void SeisokuMinus_Click(object sender, EventArgs e)
        {
            m_Monster.DeletePanel(flowLayoutPanel2);
        }

        //戦利品
        private void button6_Click(object sender, EventArgs e)
        {
            m_Monster.AddBooty(flowLayoutPanel4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            m_Monster.DeletePanel(flowLayoutPanel4);
        }

        //連動する物
        private void m_Bunrui_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox l_Combo = (ComboBox)sender;

            //アンデットは例外なく穢れ固定
            if (l_Combo.Text == "アンデット")
            {
                numericUpDown2.Value = 5;
            }//これらは穢れなし
            else if (l_Combo.Text == "魔動機" || l_Combo.Text == "魔法生物" || l_Combo.Text == "魔神")
            {
                numericUpDown2.Value = 0;
            }
            else if (l_Combo.Text == "オリジナル")
            {
                m_Shutten.Text = "オリジナル";
                numericUpDown14.Value = -1;
            }

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (m_Bunrui.Text == "アンデット")
            {
                numericUpDown2.Value = 5;
            }
            //これらは穢れなし
            else if (m_Bunrui.Text == "魔動機" || m_Bunrui.Text == "魔法生物" ||
                m_Bunrui.Text == "魔神")
            {
                numericUpDown2.Value = 0;
            }
        }

        private void numericUpDown14_ValueChanged(object sender, EventArgs e)
        {
            if (m_Shutten.Text == "オリジナル")
            {
                numericUpDown14.Value = -1;
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void m_CoreCombo_Click(object sender, EventArgs e)
        {
            m_CoreCombo.Items.Clear();
            m_CoreCombo.Items.Add("なし");
            foreach (ListViewItem item in listViewEx1.Items)
            {
                m_CoreCombo.Items.Add(item.SubItems[1].Text);
            }
        }

        private void m_Zyakuten_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Zyakuten.Text == "なし")
            {
                numericUpDown3.Value = -1;
                numericUpDown7.Value = -1;
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (m_Zyakuten.Text == "なし")
                numericUpDown7.Value = -1;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (m_Zyakuten.Text == "なし")
                numericUpDown3.Value = -1;
        }

        private void m_Idou1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Idou1.Text == "-" || m_Idou1.Text == "＊")
                numericUpDown4.Value = -1;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (m_Idou1.Text == "-" || m_Idou1.Text == "＊")
                numericUpDown4.Value = -1;
        }

        private void m_Idou2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Idou2.Text == "-" || m_Idou2.Text == "＊")
                numericUpDown5.Value = -1;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (m_Idou2.Text == "-" || m_Idou2.Text == "＊")
                numericUpDown5.Value = -1;

        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown10.Value = numericUpDown9.Value + 7;
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown12.Value = numericUpDown11.Value + 7;
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            if (m_Bunrui.Text != "オリジナル" && m_Shutten.Text != "オリジナル")
            {
                numericUpDown10.Value = numericUpDown9.Value + 7;
            }
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            if (m_Bunrui.Text != "オリジナル" || m_Shutten.Text != "オリジナル")
            {
                numericUpDown12.Value = numericUpDown11.Value + 7;
            }
        }

        //画像参照ボタン
        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog l_Dialog = new OpenFileDialog();
            l_Dialog.Filter = "pngファイル|*.png|jpegファイル|*.jpeg|jpgファイル|*.jpg|すべてのファイル|*.*";
            l_Dialog.Title = "参照";
            if (l_Dialog.ShowDialog() == DialogResult.OK)
            {
                TextureURL.Text = l_Dialog.FileName;
            }
        }

        //-----UIイベント処理終わり

        //モンスターを編集画面にセット
        public void SelectMonsterSet()
        {
            if (m_ViewTab.m_SelectMonster == null)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "処理にエラーが起きてうまく選択できませんでした。", "警告", MessageBoxButtons.OK);
                return;
            }
            Monster m = m_ViewTab.m_SelectMonster;

            m_MonsterNameBox.Text = m.m_Name;

            m_Bunrui.Text = m.m_Category;

            LevelNumUpDown.Value = int.Parse(m.m_Level);

            m_Shutten.Text = m.m_Reference;

            m_INTcombo.Text = m.m_Intelligence;

            m_ChikakuCombo.Text = m.m_Perception;

            m_ReactionCombo.Text = m.m_Reaction;

            numericUpDown2.Value = int.Parse(m.m_Kegare);

            //ページ記載ある場合
            if (m.m_Page != "")
            {
                numericUpDown14.Value = int.Parse(m.m_Page);
            }
            else
                numericUpDown14.Value = -1;

            numericUpDown6.Value = int.Parse(m.m_Recognition);

            if (m.m_Weak_Point_Value != "なし")
                numericUpDown7.Value = int.Parse(m.m_Weak_Point_Value);
            else
                numericUpDown7.Value = -1;

            numericUpDown8.Value = int.Parse(m.m_Preemptive_Value);

            //生命、精神
            string l_Life = m.m_Life_Resistance;
            numericUpDown9.Value = int.Parse(l_Life);

            string l_Spirit = m.m_Spirit_Resistance;
            numericUpDown11.Value = int.Parse(l_Spirit);

            //弱点値
            if (m.m_Weak_Point.Contains("+"))
            {
                string[] l_Weak = m.m_Weak_Point.Split('+');
                m_Zyakuten.Text = l_Weak[0];
                numericUpDown3.Value = int.Parse(l_Weak[1]);
            }
            else
            {
                m_Zyakuten.Text = "なし";
                numericUpDown3.Value = -1;
            }

            //移動速度
            //方法1側が移動速度記入されてるか
            if (m.m_Move_Speed[0].Move_ORG == "-")
            {
                m_Idou1.Text = "-";
                numericUpDown4.Value = -1;
            }
            else
            {
                //元参照の奴
                if (m.m_Move_Speed[0].Move_ORG == "＊")
                {
                    m_Idou1.Text = "＊";
                    numericUpDown4.Value = -1;
                }
                else
                {
                    m_Idou1.Text = m.m_Move_Speed[0].Move_ORG;
                    numericUpDown4.Value = int.Parse(m.m_Move_Speed[0].Speed_ORG);
                }

            }

            //方法2側が移動速度記入されてるか
            if (m.m_Move_Speed[1].Move_ORG == "-")
            {
                m_Idou2.Text = "-";
                numericUpDown5.Value = -1;
            }
            else
            {
                if (m.m_Move_Speed[1].Move_ORG == "＊")
                {
                    m_Idou2.Text = "＊";
                    numericUpDown5.Value = -1;
                }
                else
                {

                    m_Idou2.Text = m.m_Move_Speed[1].Move_ORG;
                    numericUpDown5.Value = int.Parse(m.m_Move_Speed[1].Speed_ORG);
                }
            }

            //言語
            //パネルリセット
            flowLayoutPanel1.Controls.Clear();
            //言語持ちかどうか
            if (m.m_Language.Count() != 0)
            {
                int l_Combonum = 0;

                //言語分回す
                foreach (string lang in m.m_Language)
                {
                    m_Monster.AddLanguage(flowLayoutPanel1);
                    ComboBox l_combo = (ComboBox)flowLayoutPanel1.Controls[l_Combonum].Controls[0];

                    l_combo.Text = lang;
                    l_Combonum++;
                }
            }

            //生息地
            //パネルリセット
            flowLayoutPanel2.Controls.Clear();
            //生息地あるかどうか
            if (m.m_Habitat.Count() != 0)
            {
                int l_HabitatNum = 0;

                //生息地分回す
                foreach (string habitat in m.m_Habitat)
                {
                    m_Monster.AddHabitat(flowLayoutPanel2);
                    TextBox l_Text = (TextBox)flowLayoutPanel2.Controls[l_HabitatNum].Controls[0];

                    l_Text.Text = habitat;
                    l_HabitatNum++;
                }
            }

            //基本ステータス
            //パネルリセット
            listViewEx1.Items.Clear();

            int l_statusNum = 0;
            foreach (Monster.Status status in m.m_Statuses)
            {
                m_Monster.AddStatus();
                listViewEx1.Items[l_statusNum].SubItems[1].Text = status.Part;
                listViewEx1.Items[l_statusNum].SubItems[2].Text = status.HitPower;
                listViewEx1.Items[l_statusNum].SubItems[3].Text = status.Damage;
                listViewEx1.Items[l_statusNum].SubItems[4].Text = status.Dodge;
                listViewEx1.Items[l_statusNum].SubItems[5].Text = status.Protection;
                listViewEx1.Items[l_statusNum].SubItems[6].Text = status.HP;
                listViewEx1.Items[l_statusNum].SubItems[7].Text = status.MP;

                l_statusNum++;
            }
            //コア部位
            m_CoreCombo.Text = m.m_CorePart;

            //特殊能力
            //パネルリセット
            flowLayoutPanel3.Controls.Clear();
            if (m.m_Specals.Count != 0)
            {
                int l_SpeNum = 0;

                //能力分回す
                foreach (Monster.Specal spe in m.m_Specals)
                {
                    if (spe.Name == "なし") break;

                    m_Monster.AddSpecal(flowLayoutPanel3);
                    ComboBox l_Declaration1 = (ComboBox)flowLayoutPanel3.Controls[l_SpeNum].Controls[0];
                    ComboBox l_Declaration2 = (ComboBox)flowLayoutPanel3.Controls[l_SpeNum].Controls[1];
                    ComboBox l_Declaration3 = (ComboBox)flowLayoutPanel3.Controls[l_SpeNum].Controls[2];
                    TextBox l_text = (TextBox)flowLayoutPanel3.Controls[l_SpeNum].Controls[3];
                    RichTextBox richText = (RichTextBox)flowLayoutPanel3.Controls[l_SpeNum].Controls[4];

                    l_Declaration1.Text = spe.Declaration1;
                    l_Declaration2.Text = spe.Declaration2;
                    l_Declaration3.Text = spe.Declaration3;
                    l_text.Text = spe.Name;
                    richText.Text = spe.Effect.Replace(ReplacementList.m_LineFeed_R, "\r\n");

                    l_SpeNum++;
                }
            }

            //戦利品
            //パネルリセット
            flowLayoutPanel4.Controls.Clear();
            if (m.m_Booty.Count != 0)
            {
                int l_BootyNum = 0;

                //能力分回す
                foreach (Monster.Booty booty in m.m_Booty)
                {
                    m_Monster.AddBooty(flowLayoutPanel4);
                    ComboBox l_dice = (ComboBox)flowLayoutPanel4.Controls[l_BootyNum].Controls[0];
                    TextBox l_sName = (TextBox)flowLayoutPanel4.Controls[l_BootyNum].Controls[1];
                    TextBox l_sNum = (TextBox)flowLayoutPanel4.Controls[l_BootyNum].Controls[3];
                    TextBox l_gamel = (TextBox)flowLayoutPanel4.Controls[l_BootyNum].Controls[4];
                    ComboBox l_mate = (ComboBox)flowLayoutPanel4.Controls[l_BootyNum].Controls[6];

                    l_dice.Text = booty.Dice;
                    l_sName.Text = booty.Name;
                    l_sNum.Text = booty.Num;
                    l_gamel.Text = booty.Gamel;
                    l_mate.Text = booty.Material;

                    l_BootyNum++;
                }
            }

            //解説
            richTextBox1.Text = m.m_Description.Replace(ReplacementList.m_LineFeed_R, "\r\n");

            TextureURL.Text = m.m_Texture;

        }

        //フォームの情報をモンスター型に変換
        //主に編集の保存の際に使う
        public int FormToMonster(ref Monster m)
        {
            Monster l_Monster = new Monster();

            l_Monster.m_Name = m_MonsterNameBox.Text;
            if (l_Monster.m_Name == "" && !m_DataCustom.m_OutPutMode)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "名前を入力してください", "問題", MessageBoxButtons.OK);
                return -1;
            }

            l_Monster.m_Reaction = m_ReactionCombo.Text;
            l_Monster.m_Category = m_Bunrui.Text;
            l_Monster.m_Kegare = numericUpDown2.Value.ToString();
            l_Monster.m_Reference = m_Shutten.Text;

            if (numericUpDown14.Value.ToString() == "-1")
                l_Monster.m_Page = "";
            else
                l_Monster.m_Page = numericUpDown14.Value.ToString();

            l_Monster.m_Recognition = numericUpDown6.Value.ToString();
            l_Monster.m_Intelligence = m_INTcombo.Text;
            l_Monster.m_Weak_Point_Value = numericUpDown7.Value.ToString();
            if (l_Monster.m_Weak_Point_Value == "-1")
                l_Monster.m_Weak_Point_Value = "なし";
            l_Monster.m_Perception = m_ChikakuCombo.Text;
            l_Monster.m_Preemptive_Value = numericUpDown8.Value.ToString();

            string l_Element = m_Zyakuten.Text;
            string l_WeakDamage = numericUpDown3.Value.ToString();
            if (l_WeakDamage == "-1")
            {
                l_Monster.m_Weak_Point = l_Element;
                l_Monster.m_Weak_Point_O.Element = l_Element;
                l_Monster.m_Weak_Point_O.Damage = "なし";
            }
            else
            {
                l_Monster.m_Weak_Point = l_Element + "+" + l_WeakDamage;
                l_Monster.m_Weak_Point_O.Element = l_Element;
                l_Monster.m_Weak_Point_O.Damage = l_WeakDamage;
            }

            string l_Seimei = numericUpDown9.Value.ToString();
            string l_Seimei_K = numericUpDown10.Value.ToString();

            l_Monster.m_Life_Resistance = l_Seimei;

            string l_Seisin = numericUpDown11.Value.ToString();
            string l_Seisin_K = numericUpDown12.Value.ToString();

            l_Monster.m_Spirit_Resistance = l_Seisin;

            //移動方法が記載されてない場合二足として扱う
            if (m_Idou1.Text == "")
            {
                l_Monster.m_Move_Speed[0].Move_View = "";
                l_Monster.m_Move_Speed[0].Move_ORG = "二足";
            }
            //なし
            else if (m_Idou1.Text == "-")
            {
                l_Monster.m_Move_Speed[0].Move_View = "-";
                l_Monster.m_Move_Speed[0].Move_ORG = "-";
            }
            //二足以外の方法が存在
            else
            {
                l_Monster.m_Move_Speed[0].Move_View = m_Idou1.Text;
                l_Monster.m_Move_Speed[0].Move_ORG = m_Idou1.Text;
            }

            string l_Idou1_N = numericUpDown4.Value.ToString();
            l_Monster.m_Move_Speed[0].Speed_View = l_Idou1_N;
            l_Monster.m_Move_Speed[0].Speed_ORG = l_Idou1_N;
            //参照元モンスター
            if (l_Idou1_N == "-1" && m_Idou1.Text == "＊")
            {
                l_Monster.m_Move_Speed[0].Speed_View = "元参照";
                l_Monster.m_Move_Speed[0].Speed_ORG = "元参照";
            }
            //入力なし
            else if (l_Idou1_N == "-1")
            {
                l_Monster.m_Move_Speed[0].Speed_View = "";
                l_Monster.m_Move_Speed[0].Speed_ORG = "なし";
            }

            if (m_Idou2.Text == "")
            {
                l_Monster.m_Move_Speed[1].Move_View = "";
                l_Monster.m_Move_Speed[1].Move_ORG = "二足";
            }
            else if (m_Idou2.Text == "-")
            {
                l_Monster.m_Move_Speed[1].Move_View = "-";
                l_Monster.m_Move_Speed[1].Move_ORG = "-";
            }
            else
            {
                l_Monster.m_Move_Speed[1].Move_View = m_Idou2.Text;
                l_Monster.m_Move_Speed[1].Move_ORG = m_Idou2.Text;
            }

            string l_Idou2_N = numericUpDown5.Value.ToString();
            l_Monster.m_Move_Speed[1].Speed_View = l_Idou1_N;
            l_Monster.m_Move_Speed[1].Speed_ORG = l_Idou1_N;
            if (l_Idou2_N == "-1" && m_Idou2.Text == "＊")
            {
                l_Monster.m_Move_Speed[1].Speed_View = "元参照";
                l_Monster.m_Move_Speed[1].Speed_ORG = "元参照";
            }
            else if (l_Idou2_N == "-1")
            {
                l_Monster.m_Move_Speed[1].Speed_View = "";
                l_Monster.m_Move_Speed[1].Speed_ORG = "なし";
            }

            if ((l_Monster.m_Reaction == "" || l_Monster.m_Category == "" || l_Monster.m_Reference == "" || l_Monster.m_Intelligence == "" ||
             l_Monster.m_Perception == "" || l_Element == "") && !m_DataCustom.m_OutPutMode)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "未選択項目があります\n移動の未選択は二足になります", "問題", MessageBoxButtons.OK);
                return -1;
            }

            if ((l_Monster.m_Recognition == "0" || l_Monster.m_Weak_Point_Value == "0" || l_Monster.m_Reference == "0" || l_WeakDamage == "0" ||
                l_Seimei_K == "0" || l_Seisin_K == "0") && !m_DataCustom.m_OutPutMode)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "0ではいけない値があります", "問題", MessageBoxButtons.OK);
                return -1;
            }

            if ((l_Idou2_N == "0" || l_Idou1_N == "0") && !m_DataCustom.m_OutPutMode)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "移動力0はありません\n移動しなければ-を選択してください", "問題", MessageBoxButtons.OK);
                return -1;
            }

            //言語
            string l_Lang = "なし";
            if (flowLayoutPanel1.Controls.Count != 0)
            {
                int i = 0;
                foreach (Panel panel in flowLayoutPanel1.Controls)
                {
                    ComboBox l_combo = (ComboBox)panel.Controls[0];
                    if (l_combo.Text != "" && l_combo.Text != "なし")
                    {
                        l_Monster.m_Language.Add(l_combo.Text);
                        i++;
                    }
                }
            }
            else
            {
                l_Monster.m_Language.Add(l_Lang);
            }

            //生息地
            string l_Seisokuti = "不明";
            if (flowLayoutPanel2.Controls.Count != 0)
            {
                int i = 0;
                foreach (Panel panel in flowLayoutPanel2.Controls)
                {
                    TextBox l_text = (TextBox)panel.Controls[0];
                    if (l_text.Text != "")
                    {
                        l_Monster.m_Habitat.Add(l_text.Text);
                        i++;
                    }
                }
            }
            else
            {
                l_Monster.m_Habitat.Add(l_Seisokuti);
            }

            //基礎ステータス
            l_Monster.m_CorePart = m_CoreCombo.Text;
            if (l_Monster.m_CorePart == "")
                l_Monster.m_CorePart = "なし";

            bool l_StatusCheck = false;

            foreach (ListViewItem item in listViewEx1.Items)
            {
                Monster.Status status = new Monster.Status();

                if (item.SubItems.Count == 0)
                {
                    l_StatusCheck = true;
                    break;
                }

                status.Part = item.SubItems[1].Text;
                status.HitPower = item.SubItems[2].Text;
                status.Damage = item.SubItems[3].Text;
                status.Dodge = item.SubItems[4].Text;
                status.Protection = item.SubItems[5].Text;
                status.HP = item.SubItems[6].Text;
                status.MP = item.SubItems[7].Text;

                l_Monster.m_Statuses.Add(status);
            }


            if (l_StatusCheck && !m_DataCustom.m_OutPutMode)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "ステータスを設定してください\nダブルクリックで編集できます\n", "問題", MessageBoxButtons.OK);
                return -1;
            }

            //特殊能力
            if (flowLayoutPanel3.Controls.Count != 0)
            {
                foreach (Panel panel in flowLayoutPanel3.Controls)
                {
                    ComboBox l_Declaration1 = (ComboBox)panel.Controls[0];
                    ComboBox l_Declaration2 = (ComboBox)panel.Controls[1];
                    ComboBox l_Declaration3 = (ComboBox)panel.Controls[2];
                    TextBox l_text = (TextBox)panel.Controls[3];
                    RichTextBox richText = (RichTextBox)panel.Controls[4];

                    Monster.Specal specal = new Monster.Specal();

                    specal.Name = l_text.Text;
                    specal.Declaration1 = l_Declaration1.Text;
                    specal.Declaration2 = l_Declaration2.Text;
                    specal.Declaration3 = l_Declaration3.Text;
                    specal.Effect = richText.Text;

                    l_Monster.m_Specals.Add(specal);
                }
            }
            else
            {
                Monster.Specal specal = new Monster.Specal();

                specal.Name = "なし";
                specal.Declaration1 = "なし";
                specal.Declaration2 = "なし";
                specal.Declaration3 = "なし";
                specal.Effect = "なし";

                l_Monster.m_Specals.Add(specal);
            }

            //戦利品
            if (flowLayoutPanel4.Controls.Count != 0)
            {
                foreach (Panel panel in flowLayoutPanel4.Controls)
                {
                    ComboBox dice = (ComboBox)panel.Controls[0];
                    TextBox s_Name = (TextBox)panel.Controls[1];
                    TextBox s_Num = (TextBox)panel.Controls[3];
                    TextBox gamel = (TextBox)panel.Controls[4];
                    ComboBox mate = (ComboBox)panel.Controls[6];

                    Monster.Booty booty = new Monster.Booty();

                    string stex = s_Name.Text;
                    string numtex = s_Num.Text;
                    string game = gamel.Text;
                    string matex = mate.Text;

                    booty.Dice = dice.Text;

                    if (stex == "")
                        stex = "なし";
                    if (numtex == "")
                        numtex = "0";
                    if (game == "")
                        game = "0";
                    if (matex == "")
                        matex = "なし";

                    booty.Name = stex;
                    booty.Num = numtex;
                    booty.Gamel = game;
                    booty.Material = matex;

                    l_Monster.m_Booty.Add(booty);
                }
            }
            else
            {
                Monster.Booty booty = new Monster.Booty();

                booty.Dice = "なし";
                booty.Name = "なし";
                booty.Num = "0";
                booty.Gamel = "0";
                booty.Material = "なし";

                l_Monster.m_Booty.Add(booty);
            }



            l_Monster.m_Description = richTextBox1.Text;

            l_Monster.m_Level = LevelNumUpDown.Value.ToString();

            l_Monster.m_Texture = TextureURL.Text;

            //ダイスパレット消さない為
            l_Monster.m_DicePallet = m.m_DicePallet;
            l_Monster.m_FixedPallet = m.m_FixedPallet;

            m = l_Monster;

            return 1;

        }

        public void EditClear()
        {
            m_MonsterNameBox.Clear();
            m_ReactionCombo.Text = "";
            m_Bunrui.Text = "";
            numericUpDown2.Value = 0;
            m_Shutten.Text = "";
            numericUpDown14.Value = 0;
            numericUpDown6.Value = 0;
            m_INTcombo.Text = "";
            numericUpDown7.Value = 0;
            m_Zyakuten.Text = "";
            m_ChikakuCombo.Text = "";
            numericUpDown8.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown9.Value = 0;
            numericUpDown10.Value = 0;
            numericUpDown11.Value = 0;
            numericUpDown12.Value = 0;
            m_Idou1.Text = "";
            m_Idou2.Text = "";
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();
            m_CoreCombo.Text = "";
            listViewEx1.Items.Clear();
            richTextBox1.Text = "";
            LevelNumUpDown.Value = 1;
            TextureURL.Text = "";

        }

    }

}
