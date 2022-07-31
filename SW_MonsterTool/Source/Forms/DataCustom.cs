using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SW_MonsterTool.Source.MDataView;
using SW_MonsterTool.Source.MData;
using SW_MonsterTool.Source.Utility.FileUtility;
using SW_MonsterTool.Source.Utility.MathUtility;

namespace SW_MonsterTool
{
    public partial class DataCustom : Form
    {
        DataCustomMain m_DataCustomMain;

        WindowCenter m_WCenter;

        //選択しているモンスターの情報
        public string m_SelectedMonsterName;
        public int m_SelectMonsterNumber;
        public TreeNode m_SelectNode;

        //編集回数
        private int m_EditNum = 0;
        private int m_DeleteNum = 0;

        //出力関連
        private FileUtil m_OutFile;
        private string m_EditFileURL;
        public bool m_OutPutMode;

        public DataCustom()
        {
            m_OutPutMode = false;
            m_EditFileURL = "";
            m_SelectedMonsterName = "";
            m_SelectMonsterNumber = 0;

            m_OutFile = new FileUtil();
            m_WCenter = new WindowCenter();

            InitializeComponent();
        }

        private void DataCustom_Load(object sender, EventArgs e)
        {
        }

        private void DataCustom_Activated(object sender, EventArgs e)
        {
            if (!m_DataCustomMain.m_FirstStart && !this.TopMost)
            {
                m_DataCustomMain.m_FirstStart = true;
                this.TopMost = true;
            }
        }

        //ファイルをひらく
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "編集可能ファイル(*.json;*.csv;*.txt)|*.json;*.csv;*.txt|CSVファイル(*.csv)|*.csv|jsonファイル(*.json)|*.json|テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            dialog.Title = "開く";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Select_DataTextBox1.Text = dialog.FileName;
            }

        }

        //モンスターを削除
        private void button3_Click(object sender, EventArgs e)
        {
            if (Select_DataTreeView1.SelectedNode == null) return;
            if (Select_DataTreeView1.SelectedNode.Parent == null) return;
            DialogResult result = MessageBox.Show("このモンスターを削除しますか？",
                "質問",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                //消すデータセット
                SelectMonsterProcess();

                m_DataCustomMain.m_ViewTab.m_Monsters.RemoveAt(m_SelectMonsterNumber);
                Select_DataTreeView1.SelectedNode.Remove();
                m_DeleteNum += 1;

                //消したデータが最後尾の場合セレクトナンバーも変わる処理
                if (m_SelectMonsterNumber > m_DataCustomMain.m_ViewTab.m_Monsters.Count - 1)
                    m_SelectMonsterNumber = m_DataCustomMain.m_ViewTab.m_Monsters.Count - 1;
            }
            else if (result == DialogResult.No)
            {

            }
        }

        //編集したデータを保存
        private void button4_Click(object sender, EventArgs e)
        {
            //何も編集してない場合は通さない
            if (m_EditNum == 0 && m_DeleteNum == 0) return;

            DialogResult result = MessageBox.Show("編集したデータをすべて保存しますか？",
                "質問",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = Select_DataTextBox1.Text;
                dialog.Filter = "編集可能ファイル(*.json;*.csv;*.txt)|*.json;*.csv;*.txt|CSVファイル(*.csv)|*.csv|jsonファイル(*.json)|*.json|テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Title = "保存先のファイルを選択してください";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    m_EditFileURL = dialog.FileName;
                }
                else
                {
                    MessageBox.Show(this, "キャンセルされました", "結果", MessageBoxButtons.OK);
                    m_OutPutMode = false;
                    return;
                }

                m_OutPutMode = true;

                List<string> l_Result = new List<string>();

                if (m_OutFile.IsFileLocked(m_EditFileURL))
                {
                    m_WCenter.SetHook(this); ;
                    MessageBox.Show(this, "出力ファイル先がどこかで利用されています", "問題", MessageBoxButtons.OK);
                    m_OutPutMode = false;
                    return;
                }

                //編集されたデータを保存する
                if (m_EditNum > 0)
                {
                    //範囲外になる場合があるとエラー落ちする対策
                    m_SelectMonsterNumber = MathUtil.Clamp(m_SelectMonsterNumber, 0, m_DataCustomMain.m_ViewTab.m_Monsters.Count - 1);

                    Monster l_EditM = m_DataCustomMain.m_ViewTab.m_Monsters[m_SelectMonsterNumber];

                    int read = m_DataCustomMain.FormToMonster(ref l_EditM);
                    if (read < 0)
                    {
                        m_WCenter.SetHook(this); ;
                        MessageBox.Show(this, "エラーが起きました\nお手数ですが編集をやり直してください\nそれでも解決しない場合は開発者へお問い合わせください", "警告", MessageBoxButtons.OK);
                        m_OutPutMode = false;
                        return;
                    }

                    m_DataCustomMain.m_ViewTab.m_Monsters[m_SelectMonsterNumber] = l_EditM;
                }

                //出力
                string l_Where = m_EditFileURL.Split('.').Last();

                if (l_Where.Equals("CSV", StringComparison.OrdinalIgnoreCase))
                {
                    //モンスター型→csvへ変換
                    foreach (Monster monster in m_DataCustomMain.m_ViewTab.m_Monsters)
                    {
                        l_Result.AddRange(Monster.MonsterToCSV(monster));
                    }

                    m_OutFile.CSVWrite(m_EditFileURL, l_Result, false, "shift_jis");

                }
                else if (l_Where.Equals("json", StringComparison.OrdinalIgnoreCase))
                {
                    //モンスター型→jsonへ変換
                    List<JsonMonsterData> l_OutJson = new List<JsonMonsterData>();
                    foreach (Monster monster in m_DataCustomMain.m_ViewTab.m_Monsters)
                    {
                        l_OutJson.Add(Monster.ConvertToJson(monster));
                    }

                    m_OutFile.WriteJson(l_OutJson, m_EditFileURL);

                }

                if(m_OutFile.m_NowFile != null)
                m_OutFile.m_NowFile.Close();

                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "出力が終わりました", "送信", MessageBoxButtons.OK);

                EditClear();
            }
            else if (result == DialogResult.No)
            {
                m_OutPutMode = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void DataCustomSelectGet(DataCustomMain data)
        {
            m_DataCustomMain = data;
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //m_SelectNode = e.Node;
        }

        //下に反映
        private void button1_Click(object sender, EventArgs e)
        {
            if (Select_DataTextBox1.Text == "") return;

            //色々初期化
            EditClear();

            //リスト内削除しておかないとどんどんリスト増える
            m_DataCustomMain.m_MonsterCSV.Clear();
            m_DataCustomMain.m_MonsterCSV.Add(Select_DataTextBox1.Text);

            m_DataCustomMain.m_ViewTab = new MonsterReadView();
            int read = m_DataCustomMain.m_ViewTab.ReadMonsterData(m_DataCustomMain.m_MonsterCSV);
            if (read > 0)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "読み込めないファイルがありました\nファイルの名前や場所を変えていませんか?", "警告", MessageBoxButtons.OK);
                return;
            }
            m_DataCustomMain.m_ViewTab.AddTreeView(ref Select_DataTreeView1);

        }

        //選択したデータを開く
        private void button2_Click(object sender, EventArgs e)
        {
            if (m_DataCustomMain.m_ViewTab == null) return;

            if (m_DataCustomMain.m_ViewTab.m_Heading)
            {
                if (Select_DataTreeView1.SelectedNode == null) return;
                if (Select_DataTreeView1.SelectedNode.Parent == null) return;
                if (Select_DataTreeView1.SelectedNode.Text == "") return;

                //編集初回時だけはスルーする
                if (m_EditNum > 0)
                {
                    //範囲外になる場合があるとエラー落ちする対策
                    m_SelectMonsterNumber = MathUtil.Clamp(m_SelectMonsterNumber, 0, m_DataCustomMain.m_ViewTab.m_Monsters.Count - 1);

                    //編集されたデータをリストに反映させる作業
                    Monster m = m_DataCustomMain.m_ViewTab.m_Monsters[m_SelectMonsterNumber];

                    int read = m_DataCustomMain.FormToMonster(ref m);

                    if (read < 0)
                    {
                        m_WCenter.SetHook(this); ;
                        MessageBox.Show(this, "エラーが起きました\nお手数ですが編集をやり直してください\nそれでも解決しない場合は開発者へお問い合わせください", "警告", MessageBoxButtons.OK);
                    }

                    m_DataCustomMain.m_ViewTab.m_Monsters[m_SelectMonsterNumber] = m;
                    m_SelectNode.Text = m.m_Name + " (" + m.m_Reference + ")";
                }

                SelectMonsterProcess();
                //メイン画面に反映
                m_DataCustomMain.SelectMonsterSet();
                m_EditNum += 1;
            }
            else
            {
                if (Select_DataTreeView1.SelectedNode.Text == "") return;

                //編集初回時だけはスルーする
                if (m_EditNum > 0)
                {
                    //範囲外になる場合があるとエラー落ちする対策
                    m_SelectMonsterNumber = MathUtil.Clamp(m_SelectMonsterNumber, 0, m_DataCustomMain.m_ViewTab.m_Monsters.Count - 1);

                    //編集されたデータをリストに反映させる作業
                    Monster m = m_DataCustomMain.m_ViewTab.m_Monsters[m_SelectMonsterNumber];

                    int read = m_DataCustomMain.FormToMonster(ref m);
                    if (read < 0)
                    {
                        m_WCenter.SetHook(this); ;
                        MessageBox.Show(this, "エラーが起きました\nお手数ですが編集をやり直してください\nそれでも解決しない場合は開発者へお問い合わせください", "警告", MessageBoxButtons.OK);
                        return;
                    }

                    m_DataCustomMain.m_ViewTab.m_Monsters[m_SelectMonsterNumber] = m;
                    m_SelectNode.Text = m.m_Name + " (" + m.m_Reference + ")";
                }

                SelectMonsterProcess();
                //メイン画面に反映
                m_DataCustomMain.SelectMonsterSet();
                m_EditNum += 1;

            }

        }

        private void SelectMonsterProcess()
        {
            //選択時の項目名を保存
            m_SelectNode = Select_DataTreeView1.SelectedNode;
            m_SelectedMonsterName = m_SelectNode.Text;

            //選択したモンスター検索して反映
            m_DataCustomMain.m_ViewTab.SelectMonster(m_SelectNode.Text, ref m_SelectMonsterNumber);

        }

        private void EditClear()
        {
            Select_DataTreeView1.Nodes.Clear();
            m_SelectedMonsterName = "";
            m_SelectMonsterNumber = 0;
            m_SelectNode = null;
            m_DataCustomMain.EditClear();
            m_EditNum = 0;
            m_DeleteNum = 0;
            m_OutPutMode = false;
        }

        public void SetCustomMonster(Monster m)
        {
            Select_DataTextBox1.Text = m.m_File_Source_URL;

            //色々初期化
            EditClear();

            //リスト内削除しておかないとどんどんリスト増える
            m_DataCustomMain.m_MonsterCSV.Clear();
            m_DataCustomMain.m_MonsterCSV.Add(Select_DataTextBox1.Text);

            m_DataCustomMain.m_ViewTab = new MonsterReadView();
            int read1 = m_DataCustomMain.m_ViewTab.ReadMonsterData(m_DataCustomMain.m_MonsterCSV);
            if (read1 > 0)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "読み込めないファイルがありました\nファイルの名前や場所を変えていませんか?", "警告", MessageBoxButtons.OK);
                return;
            }
            m_DataCustomMain.m_ViewTab.AddTreeView(ref Select_DataTreeView1);

            //ビューのままの番号になっているでこのリストにあった名前にする
            string l_SpritNum = m.m_Name.Split(' ').Last();
            string l_MonsterName = "";
            if (l_SpritNum.Contains("["))
                l_MonsterName = m.m_Name.Replace(" " + l_SpritNum, "");
            else
                l_MonsterName = m.m_Name;

            //全く同じモンスターの数
            int l_SameNumber = 0;

            //名前も出典も完全一致してるときの対応策
            //最大数をカウントしてから

            foreach (Monster same_m in m_DataCustomMain.m_ViewTab.m_Monsters)
            {
                //同名はname [num]で入っているのでそれを取り除く
                //なかったらそのまま入れる
                string l_SameSpritNum = same_m.m_Name.Split(' ').Last();
                string l_OrigName = "";
                if (l_SameSpritNum.Contains("["))
                    l_OrigName = same_m.m_Name.Replace(" " + l_SameSpritNum, "");
                else
                    l_OrigName = same_m.m_Name;

                if (l_OrigName == l_MonsterName && same_m.m_Reference == m.m_Reference && same_m.m_Sum_ID == m.m_Sum_ID)
                {
                    l_SameNumber = int.Parse(same_m.m_Sum_ID.Split('_').Last());
                }
            }

            //2個以上の場合のみ名前に足す
            if (l_SameNumber > 1)
                l_MonsterName = l_MonsterName + " [" + (l_SameNumber).ToString() + "]";


            //選択時の項目名を保存
            foreach (TreeNode parent in Select_DataTreeView1.Nodes)
            {
                foreach (TreeNode chil in parent.Nodes)
                {
                    if (chil.Text == (l_MonsterName + " (" + m.m_Reference + ")"))
                    {
                        m_SelectNode = chil;
                        m_SelectedMonsterName = m_SelectNode.Text;
                        break;
                    }
                }
            }

            if (m_SelectNode == null)
            {
                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "モンスターを見つけられませんでした。\n編集メニューからモンスターを選択してください", "問題", MessageBoxButtons.OK);
                return;
            }

            //選択したモンスター検索して反映
            m_DataCustomMain.m_ViewTab.SelectMonster(m_SelectNode.Text, ref m_SelectMonsterNumber);


            //編集初回時だけはスルーする
            if (m_EditNum > 0)
            {
                //範囲外になる場合があるとエラー落ちする対策
                m_SelectMonsterNumber = MathUtil.Clamp(m_SelectMonsterNumber, 0, m_DataCustomMain.m_ViewTab.m_Monsters.Count - 1);

                //編集されたデータをリストに反映させる作業

                Monster l_EditM = m_DataCustomMain.m_ViewTab.m_Monsters[m_SelectMonsterNumber];

                int read2 = m_DataCustomMain.FormToMonster(ref l_EditM);
                if (read2 < 0)
                {
                    m_WCenter.SetHook(this); ;
                    MessageBox.Show(this, "エラーが起きました\nお手数ですが編集をやり直してください\nそれでも解決しない場合は開発者へお問い合わせください", "警告", MessageBoxButtons.OK);
                }

                m_DataCustomMain.m_ViewTab.m_Monsters[m_SelectMonsterNumber] = l_EditM;
                m_SelectNode.Text = l_EditM.m_Name + " (" + l_EditM.m_Reference + ")";
            }

            //メイン画面に反映
            m_DataCustomMain.SelectMonsterSet();
            m_EditNum += 1;

        }



    }
}
