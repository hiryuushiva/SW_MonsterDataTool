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

namespace SW_MonsterTool
{
    public partial class ChatPaletteEdit : Form
    {
        //ウィンドウなどをフォームの真ん中に出すためのクラス
        private WindowCenter m_WCenter;

        Monster m_EditMonster;

        FileUtil m_File;

        MonsterReadView m_View;

        public ChatPaletteEdit(Monster monster, MonsterReadView view)
        {
            InitializeComponent();

            m_View = view;
            m_EditMonster = monster;

            m_File = new FileUtil();
            m_WCenter = new WindowCenter();

            //どれ編集してるか
            this.Text = m_EditMonster.m_Name + this.Text;

            Init();
        }

        private void Init()
        {
            //ダイス振る
            foreach(string palette in m_EditMonster.m_DicePallet)
            {
                DicePaletteTextBox.Text += palette + "\n";
            }
            //固定値
            foreach (string palette in m_EditMonster.m_FixedPallet)
            {
                FixedPaletteTextBox.Text += palette + "\n";
            }
        }

        private void ChatPalette_SaveButton1_Click(object sender, EventArgs e)
        {
            m_WCenter.SetHook(this);
            DialogResult l_Delete = MessageBox.Show("編集した結果を保存しますか？",
                "質問",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (l_Delete == DialogResult.Yes)
            {
                string l_SeveURL = m_EditMonster.m_File_Source_URL;

                //ファイルが使われているかチェックして使われてたら削除を中止
                if (m_File.IsFileLocked(l_SeveURL))
                {
                    m_WCenter.SetHook(this); ;
                    MessageBox.Show(this, "出力ファイル先がどこかで利用されています", "問題", MessageBoxButtons.OK);
                    return;
                }

                //変更適用処理

                string[] l_Dice = DicePaletteTextBox.Text.Split('\n');
                string[] l_Fixed = FixedPaletteTextBox.Text.Split('\n');

                //一度リストの中空にしてから追加
                m_EditMonster.m_DicePallet.Clear();
                m_EditMonster.m_FixedPallet.Clear();

                foreach (string dice in l_Dice)
                {
                    if (dice == "")
                        continue;

                    m_EditMonster.m_DicePallet.Add(dice);
                }
                foreach (string fix in l_Fixed)
                {
                    if (fix == "")
                        continue;

                    m_EditMonster.m_FixedPallet.Add(fix);
                }

                //変更保存

                List<string> l_Result = new List<string>();

                foreach (Monster m in m_View.m_Monsters)
                {
                    l_Result.AddRange(Monster.MonsterToCSV(m));
                }

                m_File.CSVWrite(l_SeveURL, l_Result, false, "shift_jis");

                m_WCenter.SetHook(this); ;
                MessageBox.Show(this, "保存が完了しました", "送信", MessageBoxButtons.OK);

            }
            else if (l_Delete == DialogResult.No)
            {

            }
        }
    }
}
