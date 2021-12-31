using SW_MonsterTool.Source.MyListView;
using SW_MonsterTool.Source.Utility.FileUtility;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SW_MonsterTool.Source.MData;
using SW_MonsterTool.Source.MFilter;
using System;
using System.Text;
using System.Threading.Tasks;
using SW_MonsterTool.Source.Utility.MathUtility;

namespace SW_MonsterTool.Source.MDataView
{
    //モンスターデータを読み取り、描画など
    public class MonsterReadView
    {
        public bool m_Heading = false;

        //絞り込みなど用クラス
        public MonsterFilter m_Filter;

        //選択されたモンスターを入れる変数
        public Monster m_SelectMonster;

        //モンスターデータ読み取り用
        FileUtil m_File;

        //モンスター型に変換する前のCSV状態のデータ
        List<string[]> m_MonsterCSV;

        //モンスター型に変換する前のJsonデータ
        List<JsonMonsterData> m_MonsterJsons;

        //モンスター型に変換したデータを入れるリスト
        public List<Monster> m_Monsters;

        //メニュー用の
        public MainForm m_MenuOnly;

        public MonsterReadView()
        {
            Initialize();
            m_File = new FileUtil();
            m_Filter = new MonsterFilter();
            m_Monsters = new List<Monster>();
        }

        private void Initialize()
        {

        }

        //モンスターデータ読み取り
        public int ReadMonsterData(List<string> file)
        {
            if (file.Count() == 0) return -1;

            int l_Result = 0;
            m_Monsters.Clear();
            foreach (string url in file)
            {
                //そのurlのモンスターしか入ってないリスト
                List<Monster> l_URLList = new List<Monster>();

                //csvかjsonか判別
                string l_Where = url.Split('.').Last();
                if (l_Where.Equals("CSV", StringComparison.OrdinalIgnoreCase))
                {
                    m_File.m_NowFile = m_File.GetReadFile(url, "shift_jis");
                    if (m_File.m_NowFile == null)
                    {
                        l_Result += 1;
                        continue;
                    }
                    m_MonsterCSV = m_File.GetCSV(m_File.m_NowFile);

                    SetCSVMonsterData(url, l_URLList);
                }
                else if (l_Where.Equals("json", StringComparison.OrdinalIgnoreCase))
                {
                    m_File.m_NowFile = m_File.GetReadFile(url, "utf-8");
                    if (m_File.m_NowFile == null)
                    {
                        l_Result += 1;
                        continue;
                    }
                    m_MonsterJsons = m_File.GetJson(m_File.m_NowFile);

                    SetJsonMonsterData(url, l_URLList);

                    m_File.m_NowFile.Close();
                }
            }

            if (m_File.m_NowFile != null)
                m_File.m_NowFile.Close();


            return l_Result;
        }
        private void SetJsonMonsterData(string url, List<Monster> urlmonsters)
        {
            Monster l_Monster = new Monster();

            foreach (JsonMonsterData jmon in m_MonsterJsons)
            {
                l_Monster = Monster.ConvertFromJsonData(jmon);

                l_Monster.m_File_Source_URL = url;

                //名前変わる前にID記憶
                l_Monster.m_Sum_ID = url + "_" + SameMonsterCount(l_Monster.m_Name, l_Monster.m_Reference, urlmonsters).ToString();

                //同名処理
                SameMonsterProcess(ref l_Monster.m_Name, l_Monster.m_Reference, m_Monsters);

                m_Monsters.Add(l_Monster);
                urlmonsters.Add(l_Monster);

                l_Monster = new Monster();
            }
        }
        

        //CSV型のデータをMonster型に直して入れる処理
        private void SetCSVMonsterData(string url,List<Monster> urlmonsters)
        {
            Monster l_Monster = new Monster();

            for (int y = 0; y < m_MonsterCSV.Count; y++)
            {
                if (m_MonsterCSV[y][0] == "名前")
                {
                    l_Monster.m_Name = m_MonsterCSV[y][1];
                    l_Monster.m_Reaction = m_MonsterCSV[y][4];
                }
                else if (m_MonsterCSV[y][0] == "分類")
                {
                    l_Monster.m_Category = m_MonsterCSV[y][1];
                    l_Monster.m_Kegare = m_MonsterCSV[y][4];
                }
                else if (m_MonsterCSV[y][0] == "出典")
                {
                    l_Monster.m_Reference = m_MonsterCSV[y][1];

                    if (m_MonsterCSV[y][2] == "なし")
                        l_Monster.m_Page = "";
                    else
                        l_Monster.m_Page = m_MonsterCSV[y][2];

                    l_Monster.m_Recognition = m_MonsterCSV[y][4];
                }
                else if (m_MonsterCSV[y][0] == "知能")
                {
                    l_Monster.m_Intelligence = m_MonsterCSV[y][1];
                    l_Monster.m_Weak_Point_Value = m_MonsterCSV[y][4];
                }
                else if (m_MonsterCSV[y][0] == "知覚")
                {
                    l_Monster.m_Perception = m_MonsterCSV[y][1];
                    l_Monster.m_Preemptive_Value = m_MonsterCSV[y][4];
                }
                else if (m_MonsterCSV[y][0] == "弱点")
                {
                    if (m_MonsterCSV[y][1] != "なし")
                        l_Monster.m_Weak_Point = m_MonsterCSV[y][1] + "+" + m_MonsterCSV[y][2];
                    else
                        l_Monster.m_Weak_Point = m_MonsterCSV[y][1];

                    l_Monster.m_Weak_Point_O.Element = m_MonsterCSV[y][1];
                    l_Monster.m_Weak_Point_O.Damage = m_MonsterCSV[y][2];
                }
                else if (m_MonsterCSV[y][0] == "生命抵抗力")
                {
                    l_Monster.m_Life_Resistance = m_MonsterCSV[y][1];
                }
                else if (m_MonsterCSV[y][0] == "精神抵抗力")
                {
                    l_Monster.m_Spirit_Resistance = m_MonsterCSV[y][1];
                }
                else if (m_MonsterCSV[y][0] == "移動速度")
                {
                    if (m_MonsterCSV[y][1] == "二足")
                        l_Monster.m_Move_Speed[0].Move_View = "";
                    else if (m_MonsterCSV[y][1] == "なし")
                        l_Monster.m_Move_Speed[0].Move_View = "-";
                    else
                        l_Monster.m_Move_Speed[0].Move_View = "(" + m_MonsterCSV[y][1] + ")";


                    if (m_MonsterCSV[y][3] == "二足")
                        l_Monster.m_Move_Speed[1].Move_View = "";
                    else if (m_MonsterCSV[y][3] == "-")
                        l_Monster.m_Move_Speed[1].Move_View = "-";
                    else
                        l_Monster.m_Move_Speed[1].Move_View = "(" + m_MonsterCSV[y][3] + ")";

                    if (m_MonsterCSV[y][2] == "なし")
                        l_Monster.m_Move_Speed[0].Speed_View = "";
                    else
                        l_Monster.m_Move_Speed[0].Speed_View = m_MonsterCSV[y][2];

                    if (m_MonsterCSV[y][4] == "なし")
                        l_Monster.m_Move_Speed[1].Speed_View = "";
                    else
                        l_Monster.m_Move_Speed[1].Speed_View = m_MonsterCSV[y][4];

                    l_Monster.m_Move_Speed[0].Move_ORG = m_MonsterCSV[y][1];
                    l_Monster.m_Move_Speed[0].Speed_ORG = m_MonsterCSV[y][2];
                    l_Monster.m_Move_Speed[1].Move_ORG = m_MonsterCSV[y][3];
                    l_Monster.m_Move_Speed[1].Speed_ORG = m_MonsterCSV[y][4];

                }
                else if (m_MonsterCSV[y][0] == "言語")
                {
                    foreach (string lang in m_MonsterCSV[y])
                    {
                        if (lang == "言語" || lang == "") continue;

                        l_Monster.m_Language.Add(lang);
                    }
                }
                else if (m_MonsterCSV[y][0] == "生息地")
                {
                    foreach (string habitat in m_MonsterCSV[y])
                    {
                        if (habitat == "生息地" || habitat == "") continue;

                        l_Monster.m_Habitat.Add(habitat);
                    }
                }
                else if (m_MonsterCSV[y][0] == "基礎ステータス")
                {
                    l_Monster.m_CorePart = m_MonsterCSV[y][2];

                    for (int x = 0; x < m_MonsterCSV[y + 1].Count(); x++)
                    {
                        if (x == 0) continue;
                        if (m_MonsterCSV[y + 1][x] == "") break;

                        Monster.Status l_Status = new Monster.Status();

                        l_Status.Part = m_MonsterCSV[y + 1][x];
                        l_Status.HitPower = m_MonsterCSV[y + 2][x];
                        l_Status.Damage = m_MonsterCSV[y + 3][x];
                        l_Status.Dodge = m_MonsterCSV[y + 4][x];
                        l_Status.Protection = m_MonsterCSV[y + 5][x];
                        l_Status.HP = m_MonsterCSV[y + 6][x];
                        l_Status.MP = m_MonsterCSV[y + 7][x];

                        l_Monster.m_Statuses.Add(l_Status);
                    }
                }
                else if (m_MonsterCSV[y][0] == "特殊能力")
                {
                    for (int x = 0; x < m_MonsterCSV[y + 1].Count(); x++)
                    {
                        if (x == 0) continue;
                        if (m_MonsterCSV[y + 1][x] == "") break;

                        Monster.Specal l_Specal = new Monster.Specal();

                        l_Specal.Name = m_MonsterCSV[y + 1][x];
                        l_Specal.Declaration1 = m_MonsterCSV[y + 2][x];
                        l_Specal.Declaration2 = m_MonsterCSV[y + 3][x];
                        l_Specal.Declaration3 = m_MonsterCSV[y + 4][x];
                        l_Specal.Effect = m_MonsterCSV[y + 5][x];
                        l_Specal.Effect = l_Specal.Effect.Replace(ReplacementList.m_LineFeed_R, "\r\n");

                        l_Monster.m_Specals.Add(l_Specal);
                    }
                }
                else if (m_MonsterCSV[y][0] == "戦利品")
                {
                    for (int x = 0; x < m_MonsterCSV[y + 1].Count(); x++)
                    {
                        if (x == 0) continue;
                        if (m_MonsterCSV[y + 1][x] == "") break;

                        Monster.Booty l_Booty = new Monster.Booty();

                        l_Booty.Dice = m_MonsterCSV[y + 1][x];
                        l_Booty.Name = m_MonsterCSV[y + 2][x];
                        l_Booty.Num = m_MonsterCSV[y + 3][x];
                        l_Booty.Gamel = m_MonsterCSV[y + 4][x];
                        l_Booty.Material = m_MonsterCSV[y + 5][x];

                        l_Monster.m_Booty.Add(l_Booty);
                    }
                }
                else if (m_MonsterCSV[y][0] == "解説")
                {
                    l_Monster.m_Description = m_MonsterCSV[y][1];
                    l_Monster.m_Description = l_Monster.m_Description.Replace(ReplacementList.m_LineFeed_R, "\r\n");
                }
                else if (m_MonsterCSV[y][0] == "レベル")
                {
                    l_Monster.m_Level = m_MonsterCSV[y][1];
                    if (m_MonsterCSV[y].Count() >= 4)
                        l_Monster.m_Texture = m_MonsterCSV[y][3];
                }
                else if (m_MonsterCSV[y][0] == "チャットパレット")
                {
                    string[] l_Dice = m_MonsterCSV[y][1].Split(ReplacementList.m_LineFeed_R.ToCharArray());
                    string[] l_Fixed = m_MonsterCSV[y][2].Split(ReplacementList.m_LineFeed_R.ToCharArray());

                    foreach (string dice in l_Dice)
                    {
                        if (dice == "")
                            continue;
                        l_Monster.m_DicePallet.Add(dice);
                    }
                    foreach (string fix in l_Fixed)
                    {
                        if (fix == "")
                            continue;

                        l_Monster.m_FixedPallet.Add(fix);
                    }
                }
                else if (m_MonsterCSV[y][0] == "END")
                {
                    l_Monster.m_File_Source_URL = url;

                    //名前変わる前にID記憶
                    l_Monster.m_Sum_ID = url +"_"+ SameMonsterCount(l_Monster.m_Name, l_Monster.m_Reference, urlmonsters).ToString();

                    //同名処理
                    SameMonsterProcess(ref l_Monster.m_Name, l_Monster.m_Reference,m_Monsters);

                    m_Monsters.Add(l_Monster);
                    urlmonsters.Add(l_Monster);

                    l_Monster = new Monster();
                }
            }
        }

        //一部文字列を正しく戻す処理
        //現在は,と"のみ
        private void ReplaceMonsterDate()
        {
            for (int y = 0; y < m_MonsterCSV.Count; y++)
            {
                for (int x = 0; x < m_MonsterCSV[y].Count(); x++)
                {
                    string l_Rep = m_MonsterCSV[y][x];

                    if (!l_Rep.Contains(ReplacementList.m_Comma_R) && !l_Rep.Contains(ReplacementList.m_DoubleC_R)) continue;

                    l_Rep = l_Rep.Replace(ReplacementList.m_Comma_R, ",");
                    l_Rep = l_Rep.Replace(ReplacementList.m_DoubleC_R, "\"");

                    m_MonsterCSV[y][x] = l_Rep;
                }
            }
        }

        //モンスター一覧ツリービュー追加処理
        public void AddTreeView(ref TreeView treeview)
        {
            m_Heading = true;

            treeview.Nodes.Clear();

            foreach (string category in MainForm.m_MonsterUI.m_CategoryList)
            {
                TreeNode l_Node = new TreeNode(category);
                l_Node.NodeFont = new Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));

                foreach (Monster m in m_Monsters)
                {
                    if (m.m_Category != category)
                        continue;

                    TreeNode l_Chil = new TreeNode(m.m_Name + " (" + m.m_Reference + ")");
                    l_Chil.NodeFont = new Font("MS UI Gothic", 9F);

                    //メインフォームのみに追加する
                    if(treeview.Name == "MonsterTreeView1")
                    {
                        ContextMenuStrip l_Menu = new ContextMenuStrip();
                        ToolStripMenuItem l_EditLabel = new ToolStripMenuItem();
                        l_EditLabel.Text = "編集";
                        l_EditLabel.Click += new EventHandler(EditMenu_Click);
                        ToolStripMenuItem l_DeleteLabel = new ToolStripMenuItem();
                        l_DeleteLabel.Text = "削除";
                        l_DeleteLabel.Click += new EventHandler(DeleteMenu_Click);
                        ToolStripMenuItem l_ChatPaletteLabel = new ToolStripMenuItem();
                        l_ChatPaletteLabel.Text = "チャットパレット編集";
                        l_ChatPaletteLabel.Click += new EventHandler(ChatPalette_Click);

                        //Add the menu items to the menu.
                        l_Menu.Items.AddRange(new ToolStripMenuItem[]{ l_EditLabel, l_DeleteLabel, l_ChatPaletteLabel });

                        l_Chil.ContextMenuStrip = l_Menu;
                    }

                    l_Node.Nodes.Add(l_Chil);
                }

                treeview.Nodes.Add(l_Node);
            }
        }

        //ツリービューの編集メニュー関数
        private void EditMenu_Click(object sender, EventArgs e)
        {
            m_MenuOnly.EditMenuProcess(m_SelectMonster);
        }

        //ツリービューの削除メニュー関数
        private void DeleteMenu_Click(object sender, EventArgs e)
        {
            m_MenuOnly.DeleteMenuProcess();
        }

        private void ChatPalette_Click(object sender, EventArgs e)
        {
            m_MenuOnly.ChatPaletteProcess(m_SelectMonster);
        }

        //選択されているモンスターを取得
        public void SelectMonster(string selectnode_text)
        {
            int l_Index = selectnode_text.LastIndexOf(" (");

            string l_Name = selectnode_text.Remove(l_Index, selectnode_text.Count() - l_Index);
            string l_Reference = selectnode_text.Replace(l_Name + " (", "");
            l_Reference = l_Reference.Replace(")", "");

            foreach (Monster m in m_Monsters)
            {
                if (m.m_Name == l_Name && m.m_Reference == l_Reference)
                {
                    m_SelectMonster = m;
                    break;
                }
            }
        }

        //選択されているモンスターと何番目かを取得
        public void SelectMonster(string selectnode_text ,ref int number)
        {
            int l_Index = selectnode_text.LastIndexOf(" (");

            string l_Name = selectnode_text.Remove(l_Index, selectnode_text.Count() - l_Index);
            string l_Reference = selectnode_text.Replace(l_Name + " (", "");
            l_Reference = l_Reference.Replace(")", "");

            int l_Num = 0;
            foreach (Monster m in m_Monsters)
            {
                if (m.m_Name == l_Name && m.m_Reference == l_Reference)
                {
                    m_SelectMonster = m;
                    break;
                }
                l_Num++;
            }
            number = l_Num;
        }

        //同名で出典も同じモンスターへの処理
        public void SameMonsterProcess(ref string name,string reference,List<Monster> monsters)
        {
            //全く同じモンスターの数
            int l_SameNumber = 0;

            //名前も出典も完全一致してるときの対応策
            //最大数をカウントしてから

            foreach (Monster m in monsters)
            {
                //同名はname [num]で入っているのでそれを取り除く
                //なかったらそのまま入れる
                string l_SpritNum = m.m_Name.Split(' ').Last();
                string l_OrigName = "";
                if (l_SpritNum.Contains("["))
                    l_OrigName = m.m_Name.Replace(" "+l_SpritNum, "");
                else 
                    l_OrigName = m.m_Name;

                if (l_OrigName == name && m.m_Reference == reference)
                {
                    l_SameNumber++;
                }
            }

            if (l_SameNumber != 0)
                name = name + " [" + (l_SameNumber + 1).ToString() + "]";
        }

        public int SameMonsterCount(string name, string reference, List<Monster> monsters)
        {
            //全く同じモンスターの数
            int l_SameNumber = 0;

            //名前も出典も完全一致してるときの対応策
            //最大数をカウントしてから

            foreach (Monster m in monsters)
            {
                //同名はname [num]で入っているのでそれを取り除く
                //なかったらそのまま入れる
                string l_SpritNum = m.m_Name.Split(' ').Last();
                string l_OrigName = "";
                if (l_SpritNum.Contains("["))
                    l_OrigName = m.m_Name.Replace(" " + l_SpritNum, "");
                else
                    l_OrigName = m.m_Name;

                if (l_OrigName == name && m.m_Reference == reference)
                {
                    l_SameNumber++;
                }
            }

            if (l_SameNumber != 0)
                return l_SameNumber + 1;

            return 1;
        }


        //ツリービュー並び替え
        public void ChangeOrder(ref TreeView treeView)
        {
            treeView.Nodes.Clear();
            m_Heading = false;

            List<Monster> l_Monster = m_Filter.MatchFindOrder(m_Monsters);

            if (l_Monster.Count() != 0)
                foreach (Monster m in l_Monster)
                {
                    TreeNode l_Node = new TreeNode(m.m_Name + " (" + m.m_Reference + ")");
                    l_Node.NodeFont = new Font("MS UI Gothic", 9F);

                    treeView.Nodes.Add(l_Node);
                }
            else
                AddTreeView(ref treeView);

        }
    }
}
