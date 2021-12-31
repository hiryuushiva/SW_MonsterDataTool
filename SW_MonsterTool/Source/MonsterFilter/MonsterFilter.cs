using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW_MonsterTool.Source.Utility.MathUtility;
using SW_MonsterTool.Source.MData;

namespace SW_MonsterTool.Source.MFilter
{
    public class MonsterFilter
    {
        public List<string> m_IntelligenceCheck;
        public List<string> m_PerceptionCheck;
        public List<string> m_ReactionCheck;
        public List<string> m_Move_SpeedCheck;
        public List<string> m_CategoryCheck;
        public List<string> m_Week_PointCheck;
        public List<string> m_DeclarationCheck;
        public List<string> m_LanguageCheck;
        public List<string> m_ReferenceCheck;

        public List<string> m_MonsterNameCheck;
        public List<string> m_HabitatCheck;
        public List<string> m_PartCheck;
        public List<string> m_SpeNameCheck;
        public List<string> m_DescriptionCheck;
        public List<string> m_BootyCheck;
        public List<string> m_MaterialCheck;

        public List<string> m_MonsterNumCheck;
        public List<string> m_StatusCheck;
        public string m_GamelCheck;

        public MonsterFilter()
        {
            Initialize();
        }

        public void Initialize()
        {
            //検索用
            m_IntelligenceCheck = new List<string>();
            m_PerceptionCheck = new List<string>();
            m_ReactionCheck = new List<string>();
            m_Move_SpeedCheck = new List<string>();
            m_CategoryCheck = new List<string>();
            m_Week_PointCheck = new List<string>();
            m_DeclarationCheck = new List<string>();
            m_LanguageCheck = new List<string>();
            m_ReferenceCheck = new List<string>();

            m_MonsterNameCheck = new List<string>();
            m_HabitatCheck = new List<string>();
            m_PartCheck = new List<string>();
            m_SpeNameCheck = new List<string>();
            m_DescriptionCheck = new List<string>();
            m_BootyCheck = new List<string>();
            m_MaterialCheck = new List<string>();

            m_MonsterNumCheck = new List<string>();
            m_StatusCheck = new List<string>();
            m_GamelCheck = "-22";

        }

        //条件に合うものを探す処理
        public List<Monster> MatchFindOrder(List<Monster> monsters)
        {
            List<Monster> l_Monster = new List<Monster>();

            foreach (Monster m in monsters)
            {
                //条件カウント
                int l_Match_num = 0;

                foreach (string v in m_IntelligenceCheck)
                {
                    if (v == m.m_Intelligence)
                        l_Match_num += 1;
                }

                foreach (string v in m_PerceptionCheck)
                {
                    if (v == m.m_Perception)
                        l_Match_num += 1;
                }

                foreach (string v in m_ReactionCheck)
                {
                    if (v == m.m_Reaction)
                        l_Match_num += 1;
                }

                //移動の仕方絞り込み
                foreach (string v in m_Move_SpeedCheck)
                {
                    if (m.m_Move_Speed[0].Move_ORG.Contains(v) || m.m_Move_Speed[1].Move_ORG.Contains(v))
                        l_Match_num += 1;
                }

                foreach (string v in m_CategoryCheck)
                {
                    if (v == m.m_Category)
                        l_Match_num += 1;
                }

                foreach (string v in m_Week_PointCheck)
                {
                    if (m.m_Weak_Point.Contains(v))
                        l_Match_num += 1;
                }

                foreach (string v in m_LanguageCheck)
                {
                    foreach (string s in m.m_Language)
                    {
                        if (v == s)
                            l_Match_num += 1;
                    }

                }

                foreach (string v in m_ReferenceCheck)
                {
                    if (v == m.m_Reference)
                        l_Match_num += 1;
                }

                foreach (string v in m_MonsterNameCheck)
                {
                    if (m.m_Name.Contains(v))
                        l_Match_num += 1;
                }

                foreach (string v in m_HabitatCheck)
                {
                    foreach (string s in m.m_Habitat)
                    {
                        if (s.Contains(v))
                            l_Match_num += 1;
                    }
                }

                foreach (Monster.Status s in m.m_Statuses)
                {
                    foreach (string v in m_PartCheck)
                    {
                        if (s.Part.Contains(v))
                            l_Match_num += 1;
                    }

                    foreach (string v in m_StatusCheck)
                    {
                        string[] l_v = v.Split(':');

                        if (l_v[0] == "命中力")
                        {
                            //修正待ち

                            if (l_v[1] == s.HitPower)
                                l_Match_num += 1;
                            else if (MatchEXception(l_v[1]) && MatchEXception(s.HitPower))
                                l_Match_num += 1;
                        }
                        else if (l_v[0] == "打撃点 2d+")
                        {
                            string dame = s.Damage.Replace("2d", "");
                            dame = dame.Replace("+", "");
                            if (dame == "")
                                dame = "0";

                            if (l_v[1] == dame)
                                l_Match_num += 1;
                            else if (MatchEXception(l_v[1]) && MatchEXception(dame))
                                l_Match_num += 1;

                        }
                        else if (l_v[0] == "回避力")
                        {
                            //修正待ち

                            if (l_v[1] == s.Dodge)
                                l_Match_num += 1;
                            else if (MatchEXception(l_v[1]) && MatchEXception(s.Dodge))
                                l_Match_num += 1;

                        }
                        else if (l_v[0] == "防護点")
                        {
                            if (l_v[1] == s.Protection)
                                l_Match_num += 1;
                            else if (MatchEXception(l_v[1]) && MatchEXception(s.Protection))
                                l_Match_num += 1;

                        }
                        else if (l_v[0] == "HP")
                        {
                            if (l_v[1] == s.HP)
                                l_Match_num += 1;
                            else if (MatchEXception(l_v[1]) && MatchEXception(s.HP))
                                l_Match_num += 1;

                        }
                        else if (l_v[0] == "MP")
                        {
                            if (l_v[1] == s.MP)
                                l_Match_num += 1;
                            else if (MatchEXception(l_v[1]) && MatchEXception(s.MP))
                                l_Match_num += 1;

                        }
                        else if (l_v[0] == "部位数")
                        {
                            if (l_v[1] == m.m_Statuses.Count().ToString())
                                l_Match_num += 1;
                        }
                    }
                }

                foreach (Monster.Specal s in m.m_Specals)
                {
                    foreach (string v in m_DeclarationCheck)
                    {
                        if (v == s.Declaration1)
                            l_Match_num += 1;
                        if (v == s.Declaration2)
                            l_Match_num += 1;
                        if (v == s.Declaration3)
                            l_Match_num += 1;
                    }

                    foreach (string v in m_SpeNameCheck)
                    {
                        if (s.Name.Contains(v))
                            l_Match_num += 1;
                    }

                    foreach (string v in m_DescriptionCheck)
                    {

                        if (s.Effect.Contains(v))
                            l_Match_num += 1;
                    }
                }

                foreach (Monster.Booty s in m.m_Booty)
                {
                    foreach (string v in m_BootyCheck)
                    {
                        if (s.Name.Contains(v))
                            l_Match_num += 1;
                    }

                    foreach (string v in m_MaterialCheck)
                    {
                        if (s.Material.Contains(v))
                            l_Match_num += 1;
                    }

                    if (m_GamelCheck == s.Gamel)
                        l_Match_num += 1;

                }

                foreach (string v in m_MonsterNumCheck)
                {
                    string[] l_v = v.Split(':');

                    if (l_v[0] == "レベル")
                    {
                        if (l_v[1] == m.m_Level)
                            l_Match_num += 1;
                        else if (MatchEXception(l_v[1]) && MatchEXception(m.m_Level))
                            l_Match_num += 1;
                    }
                    else if (l_v[0] == "知名度")
                    {
                        if (l_v[1] == m.m_Recognition)
                            l_Match_num += 1;
                        else if (MatchEXception(l_v[1]) && MatchEXception(m.m_Recognition))
                            l_Match_num += 1;

                    }
                    else if (l_v[0] == "弱点値")
                    {
                        if (l_v[1] == m.m_Weak_Point_Value)
                            l_Match_num += 1;
                        else if (MatchEXception(l_v[1]) && MatchEXception(m.m_Weak_Point_Value))
                            l_Match_num += 1;

                    }
                    else if (l_v[0] == "穢れ")
                    {
                        if (l_v[1] == m.m_Kegare)
                            l_Match_num += 1;
                        else if (MatchEXception(l_v[1]) && MatchEXception(m.m_Kegare))
                            l_Match_num += 1;

                    }
                    else if (l_v[0] == "先制値")
                    {
                        if (l_v[1] == m.m_Preemptive_Value)
                            l_Match_num += 1;
                        else if (MatchEXception(l_v[1]) && MatchEXception(m.m_Preemptive_Value))
                            l_Match_num += 1;

                    }
                    else if (l_v[0] == "移動速度")
                    {
                        if (m.m_Move_Speed[0].Speed_ORG == l_v[1] || m.m_Move_Speed[1].Speed_ORG == l_v[1])
                            l_Match_num += 1;
                        else if (MatchEXception(l_v[1]) && (MatchEXception(m.m_Move_Speed[0].Speed_ORG) || MatchEXception(m.m_Move_Speed[1].Speed_ORG)))
                            l_Match_num += 1;
                    }
                    else if (l_v[0] == "生命抵抗力")
                    {
                        string l_Life_K = MathUtil.AddStringNum(m.m_Life_Resistance, "7");

                        if (m.m_Life_Resistance == l_v[1] || l_Life_K == l_v[1])
                            l_Match_num += 1;
                        else if (MatchEXception(l_v[1]) && (MatchEXception(m.m_Life_Resistance) || MatchEXception(l_Life_K)))
                            l_Match_num += 1;

                    }
                    else if (l_v[0] == "精神抵抗力")
                    {
                        string l_Spirit_K = MathUtil.AddStringNum(m.m_Spirit_Resistance, "7");

                        if (m.m_Spirit_Resistance == l_v[1] || l_Spirit_K == l_v[1])
                            l_Match_num += 1;
                        else if (MatchEXception(l_v[1]) && (MatchEXception(m.m_Spirit_Resistance) || MatchEXception(l_Spirit_K)))
                            l_Match_num += 1;
                    }
                }


                if (l_Match_num != 0)
                    l_Monster.Add(m);
            }

            return l_Monster;
        }

        //例外系検索
        private bool MatchEXception(string value)
        {
            string[] l_Ex = { "＊", "-21", "-", "なし", "元参照" };

            foreach (string v in l_Ex)
                if (value == v)
                    return true;

            return false;
        }

        //チェック系初期化
        public void CheckClear()
        {
            m_IntelligenceCheck.Clear();
            m_PerceptionCheck.Clear();
            m_ReactionCheck.Clear();
            m_Move_SpeedCheck.Clear();
            m_CategoryCheck.Clear();
            m_Week_PointCheck.Clear();
            m_DeclarationCheck.Clear();
            m_LanguageCheck.Clear();
            m_ReferenceCheck.Clear();
        }
    }
}
