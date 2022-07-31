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

namespace SW_MonsterTool.Source.Forms
{
    public partial class ImageExportForm : Form
    {
        private WindowCenter m_WCenter;

        public MainForm m_Main;
        public MonsterReadView m_ViewTab;

        public List<string> m_ImageMonsterCSV;

        //選択しているモンスターの情報
        public string m_SelectedMonsterName;
        public int m_SelectMonsterNumber;
        public TreeNode m_SelectNode;


        public ImageExportForm()
        {
            InitializeComponent();

            m_ImageMonsterCSV = new List<string>();

            m_WCenter = new WindowCenter();

            //ヘッダーを追加
            MainForm.m_MonsterUI.AddImageCloumnHeader(StatusListView2);

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        //参照
        private void Image_ReferenceButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog l_Dialog = new OpenFileDialog();
            l_Dialog.Filter = "編集可能ファイル(*.json;*.csv;*.txt)|*.json;*.csv;*.txt|CSVファイル(*.csv)|*.csv|jsonファイル(*.json)|*.json|テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            l_Dialog.Title = "開く";
            if (l_Dialog.ShowDialog() == DialogResult.OK)
            {
                Image_DataURLTextBox1.Text = l_Dialog.FileName;
            }
        }

        //開く
        private void Image_DataOpenButton1_Click(object sender, EventArgs e)
        {
            if (Image_DataURLTextBox1.Text == "") return;

            //リスト内削除しておかないとどんどんリスト増える
            m_ImageMonsterCSV.Clear();
            m_ImageMonsterCSV.Add(Image_DataURLTextBox1.Text);

            m_ViewTab = new MonsterReadView();
            int l_Read = m_ViewTab.ReadMonsterData(m_ImageMonsterCSV);
            if (l_Read > 0)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "読み込めないファイルがありました\nファイルの名前や場所を変えていませんか?", "警告", MessageBoxButtons.OK);
                return;
            }
            m_ViewTab.AddTreeView(ref Image_MonsterTreeView1);

        }

        private void SelectMonsterImagePreviewButton1_Click(object sender, EventArgs e)
        {
            if (m_ViewTab == null) return;
            if (Image_MonsterTreeView1.SelectedNode == null) return;
            if (Image_MonsterTreeView1.SelectedNode.Parent == null) return;
            if (Image_MonsterTreeView1.SelectedNode.Text == "") return;

            SelectMonsterProcess();
            //プレビュー反映
            SelectMonsterView();

        }

        //画像出力
        private void SelectMonsterImageButton1_Click(object sender, EventArgs e)
        {
            if (m_ViewTab == null) return;
            if (Image_MonsterTreeView1.SelectedNode == null) return;
            if (Image_MonsterTreeView1.SelectedNode.Parent == null) return;
            DialogResult l_DResult = MessageBox.Show("このデータから画像を作成しますか？\n画像化する場合は一度プレビューしてください",
                "質問",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (l_DResult == DialogResult.Yes)
            {
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.FileName = View_MonsterNameLabel1.Text + ".png";
                dialog.Filter = "pngファイル(*.png)|*.png|jpegファイル(*.jpeg;*.jpg)|*.jpeg;*.jpg|すべてのファイル(*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Title = "保存先のファイルを選択してください";
                dialog.RestoreDirectory = true;

                string l_FileURL = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    l_FileURL = dialog.FileName;
                }

                //出力
                string l_Where = l_FileURL.Split('.').Last();
                System.Drawing.Imaging.ImageFormat l_Format;

                //フォーマットごとにかえる
                if (l_Where.Equals("png", StringComparison.OrdinalIgnoreCase))
                {
                    l_Format = System.Drawing.Imaging.ImageFormat.Png;
                }
                else if (l_Where.Equals("jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    l_Format = System.Drawing.Imaging.ImageFormat.Jpeg;
                }
                else if (l_Where.Equals("jpg", StringComparison.OrdinalIgnoreCase))
                {
                    l_Format = System.Drawing.Imaging.ImageFormat.Jpeg;
                }
                else
                {
                    m_WCenter.SetHook(this);
                    MessageBox.Show(this, "サポート外の形式です", "完了", MessageBoxButtons.OK);
                    return;
                }

                int w = Image_BackPanel.Size.Width; // コントロールの幅
                int h = Image_BackPanel.Size.Height; // コントロールの高さ

                using (Bitmap bmp = new Bitmap(w, h))
                {
                    Image_BackPanel.DrawToBitmap(bmp, new Rectangle(0, 0, w, h));
                    bmp.Save(l_FileURL, l_Format);
                }

                m_WCenter.SetHook(this);
                MessageBox.Show(this, "作成しました", "完了", MessageBoxButtons.OK);
            }
        }

        private void SelectMonsterProcess()
        {
            //選択時の項目名を保存
            m_SelectNode = Image_MonsterTreeView1.SelectedNode;
            m_SelectedMonsterName = m_SelectNode.Text;

            //選択したモンスター検索して反映
            m_ViewTab.SelectMonster(m_SelectNode.Text, ref m_SelectMonsterNumber);

            //画像関連反映 falseの場合は初期状態にする
            if (Image_UseBox.Checked == true)
            {
                if (m_ViewTab.m_SelectMonster.m_Texture != "" && m_ViewTab.m_SelectMonster.m_Texture != "None")
                {
                    MonsterSummaryPanel1.Width = 384;
                    pictureBox1.Visible = true;
                }
                else
                {
                    MonsterSummaryPanel1.Width = 535;
                    pictureBox1.Visible = false;
                    pictureBox1.SetBounds(pictureBox1.Bounds.X, 40, pictureBox1.Width, pictureBox1.Height);
                    pictureBox1.ImageLocation = "";
                    Image_UseBox.Checked = false;
                }
            }
            else
            {
                MonsterSummaryPanel1.Width = 535;
                pictureBox1.Visible = false;
                pictureBox1.SetBounds(pictureBox1.Bounds.X, 40, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.ImageLocation = "";
                Image_UseBox.Checked = false;
            }
        }

        private void SelectMonsterView()
        {
            if (m_ViewTab.m_SelectMonster == null)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "処理にエラーが起きてうまく選択できませんでした。", "警告", MessageBoxButtons.OK);
                return;
            }

            Monster l_SelectMonster = m_ViewTab.m_SelectMonster;
            const int l_FontY = 12;
            int l_PanelSetY = 0;

            int l_RowNum = 0;
            int l_RowSpaceNum = 0;

            View_MonsterNameLabel1.Text = l_SelectMonster.m_Name;

            View_LevelLabel1.Text = l_SelectMonster.m_Level;
            int l_Total = LevelBackPanel1.Width;
            int l_X = (l_Total - View_LevelLabel1.Width) / 2;
            View_LevelLabel1.SetBounds(l_X, View_LevelLabel1.Bounds.Y, View_LevelLabel1.Width, View_LevelLabel1.Height - 1);

            View_IntelligenceLabel1.Width = View_IntelligenceLabel1.PreferredWidth;
            View_IntelligenceText1.Text = l_SelectMonster.m_Intelligence;
            View_IntelligenceText1.SetBounds(View_IntelligenceLabel1.Width + 2, View_IntelligenceText1.Bounds.Y, View_IntelligenceText1.PreferredWidth, View_IntelligenceText1.Height);

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
            View_LanguageText1.SetBounds(View_LanguageLabel1.Width + 2, View_LanguageText1.Bounds.Y, View_LanguageText1.PreferredWidth, View_LanguageText1.PreferredHeight);
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
            View_HabitatText1.SetBounds(View_HabitatLabel1.Width + 2, View_HabitatText1.Bounds.Y, View_HabitatText1.PreferredWidth, View_HabitatText1.PreferredHeight);
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

            //画像反映
            if (Image_UseBox.Checked == true)
            {
                if (m_ViewTab.m_SelectMonster.m_Texture != "")
                {
                    pictureBox1.SetBounds(pictureBox1.Bounds.X, pictureBox1.Bounds.Y + (MonsterSummaryPanel1.Height - pictureBox1.Height) / 2, pictureBox1.Width, pictureBox1.Height);
                    pictureBox1.ImageLocation = m_ViewTab.m_SelectMonster.m_Texture;
                }
            }

            //ステータス
            l_PanelSetY += MonsterSummaryPanel1.Height + 47;
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
            //StatusListView2.Width = MonsterSummaryPanel1.Width;
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

            //特殊
            panel11.SetBounds(panel11.Bounds.X, l_PanelSetY, panel11.Width, panel11.Height);
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
                    l_Panel_h += l_Panel.Height + 7;
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

            l_PanelSetY += panel11.Height + 7;
            //

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
            l_PanelSetY += panel10.Height + 7;
            //


            //出典
            View_DescriptionTextBox1.SetBounds(View_DescriptionTextBox1.Bounds.X, l_PanelSetY, View_DescriptionTextBox1.Width, View_DescriptionTextBox1.Height);
            View_DescriptionTextBox1.Text = l_SelectMonster.m_Description + "\r\n(出典：" + l_SelectMonster.m_Reference + " / P." + l_SelectMonster.m_Page + ")";

            int l_Line = View_DescriptionTextBox1.GetLineFromCharIndex(View_DescriptionTextBox1.TextLength) + 1;

            View_DescriptionTextBox1.Height = View_DescriptionTextBox1.Font.Height * l_Line + View_DescriptionTextBox1.Margin.Vertical;

            l_PanelSetY += View_DescriptionTextBox1.Height + 7;

            //バックパネルをサイズ合わせる
            Image_BackPanel.SetBounds(Image_BackPanel.Bounds.X, Image_BackPanel.Bounds.Y, Image_BackPanel.Width, l_PanelSetY);

        }

        private void StatusListView2_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = StatusListView2.Columns[e.ColumnIndex].Width;
        }

        //アイコン画像を付けるか付けないか
        private void Image_UseBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
