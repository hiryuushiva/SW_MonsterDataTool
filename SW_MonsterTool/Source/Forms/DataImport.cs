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
    public partial class DataImport : Form
    {
        private WindowCenter m_WCenter;

        public MainForm m_Main;
        public MonsterReadView m_ViewTab;

        public List<string> m_ImportMonsterCSV;

        //選択しているモンスターの情報
        public string m_SelectedMonsterName;
        public int m_SelectMonsterNumber;
        public TreeNode m_SelectNode;


        public DataImport()
        {
            InitializeComponent();

            m_ImportMonsterCSV = new List<string>();

            m_WCenter = new WindowCenter();

        }

        //ファイルをひらく
        private void Import_ReferenceButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog l_Dialog = new OpenFileDialog();
            l_Dialog.Filter = "CSVファイル|*.csv|jsonファイル|*.json|テキストファイル(*.txt)|*.txt|すべてのファイル|*.*";
            l_Dialog.Title = "開く";
            if (l_Dialog.ShowDialog() == DialogResult.OK)
            {
                Import_DataURLTextBox1.Text = l_Dialog.FileName;
            }

        }

        //listView反映
        private void Import_DataOpenButton1_Click(object sender, EventArgs e)
        {
            if (Import_DataURLTextBox1.Text == "") return;

            //リスト内削除しておかないとどんどんリスト増える
            m_ImportMonsterCSV.Clear();
            m_ImportMonsterCSV.Add(Import_DataURLTextBox1.Text);

            m_ViewTab = new MonsterReadView();
            int l_Read = m_ViewTab.ReadMonsterData(m_ImportMonsterCSV);
            if (l_Read > 0)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "読み込めないファイルがありました\nファイルの名前や場所を変えていませんか?", "警告", MessageBoxButtons.OK);
                return;
            }
            m_ViewTab.AddTreeView(ref Import_MonsterTreeView1);


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        //選択したデータを開く
        private void SelectMonsterImportButton1_Click(object sender, EventArgs e)
        {
            if (m_ViewTab == null) return;
            if (Import_MonsterTreeView1.SelectedNode == null) return;
            if (Import_MonsterTreeView1.SelectedNode.Parent == null) return;
            DialogResult l_DResult = MessageBox.Show("このモンスターをImportしますか？",
                "質問",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (l_DResult == DialogResult.Yes)
            {
                if (m_ViewTab.m_Heading)
                {
                    if (Import_MonsterTreeView1.SelectedNode.Text == "") return;

                    SelectMonsterProcess();
                    //メイン画面に反映
                    m_Main.SelectMonsterSet(m_ViewTab.m_SelectMonster);
                }
                else
                {
                    if (Import_MonsterTreeView1.SelectedNode.Text == "") return;

                    SelectMonsterProcess();
                    //メイン画面に反映
                    m_Main.SelectMonsterSet(m_ViewTab.m_SelectMonster);
                }
            }
            else
            {
                Import_MonsterTreeView1.SelectedNode = null;
            }

        }

        private void SelectMonsterProcess()
        {
            //選択時の項目名を保存
            m_SelectNode = Import_MonsterTreeView1.SelectedNode;
            m_SelectedMonsterName = m_SelectNode.Text;

            //選択したモンスター検索して反映
            m_ViewTab.SelectMonster(m_SelectNode.Text, ref m_SelectMonsterNumber);

        }

        private void DataImport_Deactivate(object sender, EventArgs e)
        {

        }

        
        private void DataImport_Load(object sender, EventArgs e)
        {
            //オーナー変えないと表示位置持ってかれる
            if (this.Owner != null)
            {
                this.Owner = null;
            }
        }
    }
}
