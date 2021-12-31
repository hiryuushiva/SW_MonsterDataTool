using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SW_MonsterTool.Source.Utility.FileUtility;
using Microsoft.VisualBasic.FileIO;

namespace SW_MonsterTool
{
    public partial class ConfigCSVEdit : Form
    {
        private WindowCenter m_WCenter;

        //CSV関係
        FileUtil m_File;
        TextFieldParser m_ReadFile;
        List<string[]> m_ConfigCSV;
        string m_ExportURL;


        public ConfigCSVEdit()
        {
            m_WCenter = new WindowCenter();
            m_File = new FileUtil();
            m_ConfigCSV = new List<string[]>();
            m_ConfigCSV.Clear();

            m_ExportURL = "";

            InitializeComponent();

            ConfigCSVSet();
        }

        private void ConfigCSVSet()
        {
            //CSVファイルロード
            m_ReadFile = m_File.GetReadFile(MainForm.m_ConfigCSV, "shift_jis");
            if(m_ReadFile==null)
            {
                m_WCenter.SetHook(this);
                MessageBox.Show(this, "Configが設定されていません\n既存のConfigCSVを編集する場合、ファイル設定で設定しておいてください", "警告", MessageBoxButtons.OK);
                return;
            }

            m_ConfigCSV = m_File.GetCSV(m_ReadFile);

            for (int y = 0; y < m_ConfigCSV.Count; y++)
            {
                if (y == 0) continue;

                if (m_ConfigCSV[y][0] != "")
                {
                    IntelligenceBox1.Text += m_ConfigCSV[y][0] + "\n";
                }
                if (m_ConfigCSV[y][1] != "")
                {
                    PerceptionBox1.Text += m_ConfigCSV[y][1] + "\n";
                }
                if (m_ConfigCSV[y][2] != "")
                {
                    ReactionBox1.Text += m_ConfigCSV[y][2] + "\n";
                }
                if (m_ConfigCSV[y][3] != "")
                {
                    LanguageBox1.Text += m_ConfigCSV[y][3] + "\n";
                }
                if (m_ConfigCSV[y][4] != "")
                {
                    Weak_PointBox1.Text += m_ConfigCSV[y][4] + "\n";
                }
                if (m_ConfigCSV[y][5] != "")
                {
                    Special_AbilityBox1.Text += m_ConfigCSV[y][5] + "\n";
                }
                if (m_ConfigCSV[y][6] != "")
                {
                    Moving_MethodBox1.Text += m_ConfigCSV[y][6] + "\n";
                }
                if (m_ConfigCSV[y][7] != "")
                {
                    CategoryBox1.Text += m_ConfigCSV[y][7] + "\n";
                }
                if (m_ConfigCSV[y][8] != "")
                {
                    ReferenceBox1.Text += m_ConfigCSV[y][8] + "\n";
                }
            }

            m_ReadFile.Close();
        }

        private void ConfigCSVSave()
        {
            DialogResult l_DResult = MessageBox.Show("編集したデータをすべて保存しますか？",
                "質問",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (l_DResult == DialogResult.Yes)
            {
                OpenFileDialog l_Dialog = new OpenFileDialog();
                l_Dialog.Filter = "CSVファイル|*.csv|テキストファイル(*.txt)|*.txt|すべてのファイル|*.*";
                l_Dialog.Title = "保存";
                if (l_Dialog.ShowDialog() == DialogResult.OK)
                {
                    m_ExportURL = l_Dialog.FileName;
                }

                List<string> l_Result = new List<string>();
                l_Result = CreateCSV();

                if (m_File.IsFileLocked(m_ExportURL))
                {
                    if (m_ExportURL == MainForm.m_ConfigCSV)
                    {
                        m_ReadFile.Close();
                    }
                    else
                    {
                        m_WCenter.SetHook(this);;
                        MessageBox.Show(this, "出力ファイル先がどこかで利用されています", "問題", MessageBoxButtons.OK);
                        return;
                    }
                }

                m_File.CSVWrite(m_ExportURL, l_Result, false, "shift_jis");

                m_WCenter.SetHook(this);;
                MessageBox.Show(this, "出力が終わりました", "送信", MessageBoxButtons.OK);

            }
        }

        private List<string> CreateCSV()
        {
            List<string> l_Result = new List<string>();
            l_Result.Clear();

            //念のため変換しておく
            IntelligenceBox1.Text.Replace(",","、");
            PerceptionBox1.Text.Replace(",", "、");
            ReactionBox1.Text.Replace(",", "、");
            LanguageBox1.Text.Replace(",", "、");
            Weak_PointBox1.Text.Replace(",", "、");
            Special_AbilityBox1.Text.Replace(",", "、");
            Moving_MethodBox1.Text.Replace(",", "、");
            CategoryBox1.Text.Replace(",", "、");
            ReferenceBox1.Text.Replace(",", "、");
            //切り分け
            string[] l_Intelligence = IntelligenceBox1.Text.Split('\n');
            string[] l_Perception = PerceptionBox1.Text.Split('\n');
            string[] l_Reaction = ReactionBox1.Text.Split('\n');
            string[] l_Language = LanguageBox1.Text.Split('\n');
            string[] l_Weak_Point = Weak_PointBox1.Text.Split('\n');
            string[] l_Special = Special_AbilityBox1.Text.Split('\n');
            string[] l_Moving_Method = Moving_MethodBox1.Text.Split('\n');
            string[] l_Category = CategoryBox1.Text.Split('\n');
            string[] l_Reference = ReferenceBox1.Text.Split('\n');

            //どれが一番サイズがでかいか比べ、それに合わせる
            int l_MaxY = Max(l_Perception.Count(), l_Perception.Count(), l_Reaction.Count(), l_Language.Count(),
                l_Weak_Point.Count(), l_Special.Count(), l_Moving_Method.Count(), l_Category.Count(), l_Reference.Count());

            for (int y = 0; y < l_MaxY; y++)
            {
                //x最初に追加
                l_Result.Add("");

                if (y == 0)
                {
                    l_Result[0] += "知識,知覚,反応,言語,弱点,特殊能力,移動,分類,出典,";
                }
                else
                {
                    CreateRow(ref l_Result, y, l_Intelligence, 0);
                    CreateRow(ref l_Result, y, l_Perception, 1);
                    CreateRow(ref l_Result, y, l_Reaction, 2);
                    CreateRow(ref l_Result, y, l_Language, 3);
                    CreateRow(ref l_Result, y, l_Weak_Point, 4);
                    CreateRow(ref l_Result, y, l_Special, 5);
                    CreateRow(ref l_Result, y, l_Moving_Method, 6);
                    CreateRow(ref l_Result, y, l_Category, 7);
                    CreateRow(ref l_Result, y, l_Reference, 8);
                }
            }

            return l_Result;

        }

        private void CreateRow(ref List<string> list, int y, string[] Cloumn, int num)
        {
            //出力Split用
            int l_Y = y - 1;

            if (l_Y >= Cloumn.Count())
            {
                list[y] += ",";
            }
            else
            {
                list[y] += Cloumn[l_Y] + ",";
            }
        }

        private void Config_SaveButton1_Click(object sender, EventArgs e)
        {
            ConfigCSVSave();
        }

        public int Max(params int[] nums)
        {
            // 引数が渡されない場合
            if (nums.Length == 0) return 0;

            int max = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                max = max > nums[i] ? max : nums[i];
                // Minの場合は不等号を逆にすればOK
            }
            return max;
        }
    }
}
