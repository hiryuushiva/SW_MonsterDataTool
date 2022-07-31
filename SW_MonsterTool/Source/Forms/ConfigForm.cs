using SW_MonsterTool.Source.MyListView;
using SW_MonsterTool.Source.Utility.FileUtility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SW_MonsterTool
{
    public partial class ConfigForm : Form
    {
        //変更されたかどうか
        bool m_MS,m_OC, m_MC, m_CS, m_CC;

        private ColumnHeaderEx m_NoHeader;
        private ColumnHeaderEx m_URLHeader;

        private WindowCenter m_WCenter;

        public ConfigForm()
        {
            InitializeComponent();

            m_WCenter = new WindowCenter();

            this.m_NoHeader = new ColumnHeaderEx();
            this.m_URLHeader = new ColumnHeaderEx();

            this.m_NoHeader.Text = "No";
            this.m_NoHeader.Width = 0;

            this.m_URLHeader.Text = "URL";
            this.m_URLHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            m_URLHeader.Width = MonsterCSVListView1.Width;

            ColumnHeaderEx[] l_ColHeaderRegValue = { m_NoHeader, m_URLHeader };
            MonsterCSVListView1.Columns.AddRange(l_ColHeaderRegValue);

            LoadURL();
            this.MaximizeBox = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void LoadURL()
        {
            //実行ファイルの出力先パスを取得
            string l_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            l_Path = Path.Combine(l_Path, "URLs.txt");

            FileUtil l_FileUtil = new FileUtil();

            //なければ作成
            if (l_FileUtil.IsFileLocked(l_Path))
            {
                File.WriteAllText(l_Path,"");
            }
            else
            {
                string l_Source = File.ReadAllText(l_Path);

                //読み込めたが何もない場合
                if (l_Source == "")
                    return;

                char[] l_Separator = new char[] { '\r', '\n' };
                string[] l_Splitted = l_Source.Split(l_Separator, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    for (int i = 0; i < l_Splitted.Count(); i++)
                    {
                        if (l_Splitted[i] == "魔物スプレッドシート")
                            MonsterCSVSpreadSheetTextBox1.Text = l_Splitted[i + 1].Replace("||||", "");
                        else if (l_Splitted[i] == "出力先CSV")
                            OutPutCSVTextBox1.Text = l_Splitted[i + 1].Replace("||||", "");
                        else if (l_Splitted[i] == "魔物CSV")
                        {
                            int l_Num = 1;
                            while (l_Splitted[i + l_Num] != "Configスプレッドシート")
                            {
                                if (l_Splitted[i + l_Num] == "||||") break;
                                AddURL(l_Splitted[i + l_Num].Replace("||||", ""));
                                l_Num++;
                            }
                        }
                        else if (l_Splitted[i] == "Configスプレッドシート")
                            ConfigCSVSpreadSheetTextBox1.Text = l_Splitted[i + 1].Replace("||||", "");
                        else if (l_Splitted[i] == "CofigCSV")
                            ConfigCSVTextBox1.Text = l_Splitted[i + 1].Replace("||||", "");
                    }
                }
                catch
                {

                    var l_File = File.Create(l_Path);

                    m_MS = false;
                    m_OC = false;
                    m_MC = false;
                    m_CS = false;
                    m_CC = false;

                    m_WCenter.SetHook(this);;
                    MessageBox.Show(this, "読み込めないファイルがあり、URL設定を初期化しました", "警告", MessageBoxButtons.OK);

                    l_File.Close();

                    Close();

                    return;
                }

                List<string> l_URLs = new List<string>();
                if (MonsterCSVListView1.Items.Count != 0)
                    foreach (ListViewItem item in MonsterCSVListView1.Items)
                    {
                        l_URLs.Add(item.SubItems[1].Text);
                    }

                MainForm.m_MonsterSpread = MonsterCSVSpreadSheetTextBox1.Text;
                MainForm.m_OutMonsterFile = OutPutCSVTextBox1.Text;
                MainForm.m_ReadMonsterFiles = l_URLs;
                MainForm.m_ConfigSpread = ConfigCSVSpreadSheetTextBox1.Text;
                MainForm.m_ConfigCSV = ConfigCSVTextBox1.Text;

                m_MS = false;
                m_OC = false;
                m_MC = false;
                m_CS = false;
                m_CC = false;
            }
        }

        //保存
        private void Config_SaveButton1_Click(object sender, EventArgs e)
        {
            SaveURL();
            m_MS = false;
            m_OC = false;
            m_MC = false;
            m_CS = false;
            m_CC = false;

            Close();
        }

        public bool IsChangeValue()
        {
            if (m_MS || m_MC || m_CS || m_CC|| m_OC)
                return true;

            return false;
        }

        private void SaveURL()
        {
            string l_Result = "";
            List<string> l_URLs = new List<string>();

            l_Result += "魔物スプレッドシート\r\n";
            if (MonsterCSVSpreadSheetTextBox1.Text != "")
                l_Result += MonsterCSVSpreadSheetTextBox1.Text + "\r\n";
            else
                l_Result += "||||" + "\r\n";
            l_Result += "出力先CSV\r\n";
            if (OutPutCSVTextBox1.Text != "")
                l_Result += OutPutCSVTextBox1.Text + "\r\n";
            else
                l_Result += "||||" + "\r\n";
            l_Result += "魔物CSV\r\n";
            if (MonsterCSVListView1.Items.Count != 0)
                foreach (ListViewItem item in MonsterCSVListView1.Items)
                {
                    l_Result += item.SubItems[1].Text + "\r\n";
                    l_URLs.Add(item.SubItems[1].Text);
                }
            else
                l_Result += "||||" + "\r\n";
            l_Result += "Configスプレッドシート\r\n";
            if (ConfigCSVSpreadSheetTextBox1.Text != "")
                l_Result += ConfigCSVSpreadSheetTextBox1.Text + "\r\n";
            else
                l_Result += "||||" + "\r\n";
            l_Result += "CofigCSV\r\n";
            if (ConfigCSVTextBox1.Text != "")
                l_Result += ConfigCSVTextBox1.Text;
            else
                l_Result += "||||";


            MainForm.m_MonsterSpread = MonsterCSVSpreadSheetTextBox1.Text;
            MainForm.m_ReadMonsterFiles = l_URLs;
            MainForm.m_OutMonsterFile = OutPutCSVTextBox1.Text;
            MainForm.m_ConfigSpread = ConfigCSVSpreadSheetTextBox1.Text;
            MainForm.m_ConfigCSV = ConfigCSVTextBox1.Text;

            string l_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            l_Path = Path.Combine(l_Path, "URLs.txt");

            File.WriteAllText(l_Path, l_Result);
            MainForm.m_MonsterUI.ConfigLoad();
            int l_Read = MainForm.m_ViewData.ReadMonsterData(MainForm.m_ReadMonsterFiles);
            if (l_Read > 0)
            {
                m_WCenter.SetHook(this);;
                MessageBox.Show(this, "読み込めないファイルがありました\nファイルの名前や場所を変えていませんか?", "警告", MessageBoxButtons.OK);
            }
        }

        private void OutPutCSV_ReferenceButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog l_Dialog = new OpenFileDialog();
            l_Dialog.Filter = "編集可能ファイル(*.json;*.csv;*.txt)|*.json;*.csv;*.txt|CSVファイル(*.csv)|*.csv|jsonファイル(*.json)|*.json|テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            l_Dialog.Title = "開く";
            if (l_Dialog.ShowDialog() == DialogResult.OK)
            {
                OutPutCSVTextBox1.Text = l_Dialog.FileName;
            }
        }

        //追加
        private void MCSVList_AddButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog l_Dialog = new OpenFileDialog();
            l_Dialog.Filter = "編集可能ファイル(*.json;*.csv;*.txt)|*.json;*.csv;*.txt|CSVファイル(*.csv)|*.csv|jsonファイル(*.json)|*.json|テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            l_Dialog.Title = "開く";
            if (l_Dialog.ShowDialog() == DialogResult.OK)
            {
                if (AsURL(l_Dialog.FileName)) return;
                AddURL(l_Dialog.FileName);
                m_MC = true;
            }
        }

        private bool AsURL(string url)
        {
            foreach (ListViewItem item in MonsterCSVListView1.Items)
            {
                if (url == item.SubItems[1].Text)
                {
                    m_WCenter.SetHook(this);;
                    MessageBox.Show(this, "すでに同じURLが存在します", "重複", MessageBoxButtons.OK);
                    return true;
                }
            }
            return false;
        }

        public void AddURL(string file_name)
        {
            ListViewItem l_ListViewItem1 = new ListViewItem(new string[] {
            (MonsterCSVListView1.Items.Count+1).ToString(),
            file_name}, -1, Color.Empty, SystemColors.InactiveBorder, null);

            MonsterCSVListView1.Items.Add(l_ListViewItem1);
        }

        private void ConfigCSV_ReferenceButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog l_Dialog = new OpenFileDialog();
            l_Dialog.Filter = "編集可能ファイル(*.json;*.csv;*.txt)|*.json;*.csv;*.txt|CSVファイル(*.csv)|*.csv|jsonファイル(*.json)|*.json|テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            if (l_Dialog.ShowDialog() == DialogResult.OK)
            {
                ConfigCSVTextBox1.Text = l_Dialog.FileName;
            }
        }

        //閉じる前
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClose(e);
        }

        public void IsClose(FormClosingEventArgs e)
        {
            if (IsChangeValue())
            {
                m_WCenter.SetHook(this);;
                DialogResult l_Result = MessageBox.Show(this, "変更を保存しますか？", "保存", MessageBoxButtons.YesNoCancel);
                //何が選択されたか調べる
                if (l_Result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    SaveURL();
                }
                else if (l_Result == DialogResult.No)
                {
                    //「いいえ」が選択された時
                    return;
                }
                else if (l_Result == DialogResult.Cancel)
                {
                    //「キャンセル」が選択された時
                    e.Cancel = true;
                }

                return;
            }
        }

        private void MonsterCSVListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = MonsterCSVListView1.Columns[e.ColumnIndex].Width;
        }

        private void MCSVList_DeleteButton1_Click(object sender, EventArgs e)
        {
            DeleteURL();
            m_MC = true;
        }

        public void DeleteURL()
        {
            if (MonsterCSVListView1.Items.Count != 0)
            {
                if (MonsterCSVListView1.Items[0].Text == "")
                    MonsterCSVListView1.Items.Clear();

                //選択されてるかそうじゃないか
                if (MonsterCSVListView1.FocusedItem != null)
                {
                    MonsterCSVListView1.Items.Remove(MonsterCSVListView1.FocusedItem);
                    //フォーカスされてるのを最後のやつにしておく
                    if (MonsterCSVListView1.Items.Count != 0)
                        MonsterCSVListView1.FocusedItem = MonsterCSVListView1.Items[MonsterCSVListView1.Items.Count - 1];

                    //消されたら番号整列
                    int i = 1;
                    foreach (ListViewItem item in MonsterCSVListView1.Items)
                    {
                        item.Text = i.ToString();
                        i++;
                    }
                }
                else
                    MonsterCSVListView1.Items.RemoveAt(MonsterCSVListView1.Items.Count - 1);

            }

        }

        private void MonsterCSVSpreadSheetTextBox1_TextChanged(object sender, EventArgs e)
        {
            m_MS = true;
        }

        private void OutPutCSVTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            m_OC = true;
        }

        private void ConfigCSVTextBox1_TextChanged(object sender, EventArgs e)
        {
            m_CS = true;
        }

        private void ConfigCSVSpreadSheetTextBox1_TextChanged(object sender, EventArgs e)
        {
            m_CC = true;
        }
    }
}
