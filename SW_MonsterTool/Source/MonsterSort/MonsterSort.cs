using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using SW_MonsterTool.Source.MData;

namespace SW_MonsterTool.Source.MSort
{
    public enum SortType
    {
        レベル順,
        知名度順,
        弱点値順,
        穢れ順,
        先制値順,
        移動速度順,
        生命抵抗力順,
        精神抵抗力順,
        命中力順,
        打撃点順,
        回避力順,
        防護点順,
        HP順,
        MP順,
        部位数順,
        戦利品ガメル順,
        分類順,
        なし,
    }

    public enum SortPriority
    {
        低い,
        高い,
        なし
    }

    //モンスターソートクラス
    //並び替え用
    // Create a node sorter that implements the IComparer interface.
    public class MonsterSort : IComparer
    {
        SortType m_Sort1Type;
        SortType m_Sort2Type;
        SortPriority m_Sort1Priority;
        SortPriority m_Sort2Priority;

        List<Monster> m_Monsters;

        public MonsterSort(SortType sort1, SortType sort2, SortPriority priority1, SortPriority priority2, List<Monster> monsters)
        {
            m_Sort1Type = sort1;
            m_Sort2Type = sort2;
            m_Sort1Priority = priority1;
            m_Sort2Priority = priority2;
            m_Monsters = monsters;
        }

        // Compare the length of the strings, or the strings
        // themselves, if they are the same length.
        public int Compare(object x, object y)
        {
            int l_Result1 = 1;
            int l_Result2 = 0;

            TreeNode l_TreeNodeX = x as TreeNode;
            TreeNode l_TreeNodeY = y as TreeNode;

            Monster l_Sort1 = null, l_Sort2 = null;

            foreach (Monster m in m_Monsters)
            {
                if (l_TreeNodeX.Text.Contains(m.m_Name))
                    l_Sort1 = m;
                else if (l_TreeNodeY.Text.Contains(m.m_Name))
                    l_Sort2 = m;

                if (l_Sort1 != null && l_Sort2 != null)
                    break;
            }

            SortEnumAction(ref l_Result1, m_Sort1Type, l_Sort1, l_Sort2, l_TreeNodeX.Text, l_TreeNodeY.Text);

            if (m_Sort2Type != SortType.なし)
            {
                SortEnumAction(ref l_Result2, m_Sort2Type,l_Sort1,l_Sort2,l_TreeNodeX.Text,l_TreeNodeY.Text);
            }

            if (m_Sort1Priority == SortPriority.低い)
                return l_Result1 - l_Result2;
            else if (m_Sort1Priority == SortPriority.高い)
                return -(l_Result1 - l_Result2);
            else
                return l_Result1 - l_Result2;
        }

        private void SortEnumAction(ref int result,SortType sort, Monster sort1, Monster sort2,string tx,string ty)
        {
            switch (sort)
            {
                case SortType.穢れ順: result += KegareSort(sort1, sort2); break;
                case SortType.知名度順: result += RecognitionSort(sort1, sort2); break;
                case SortType.レベル順: result += LevelSort(sort1, sort2); break;
                case SortType.弱点値順: result += Weak_Point_ValueSort(sort1, sort2); break;
                case SortType.先制値順: result += Preemptive_ValueSort(sort1, sort2); break;
                case SortType.移動速度順: result += SpeedSort(sort1, sort2); break;
                case SortType.生命抵抗力順: result += Life_ResistanceSort(sort1, sort2); break;
                case SortType.精神抵抗力順: result += Spirit_ResistanceSort(sort1, sort2); break;
                case SortType.命中力順: result += HitPowerSort(sort1, sort2); break;
                case SortType.打撃点順: result += DamageSort(sort1, sort2); break;
                case SortType.回避力順: result += DodgeSort(sort1, sort2); break;
                case SortType.防護点順: result += ProtectionSort(sort1, sort2); break;
                case SortType.HP順: result += HPSort(sort1, sort2); break;
                case SortType.MP順: result += MPSort(sort1, sort2); break;
                case SortType.部位数順: result += PartNumSort(sort1, sort2); break;
                case SortType.戦利品ガメル順: result += GamelSort(sort1, sort2); break;
                case SortType.分類順: result += CategorySort(tx, ty); break;
            }

        }

        private int CategorySort(string x, string y)
        {
            int l_Sort1 = 0;
            int l_Sort2 = 0;
            foreach (var b in MainForm.m_MonsterUI.m_CategoryList.Select((v, i) => new { v, i }))
            {
                if (x == b.v)
                    l_Sort1 = b.i;
                else if (y == b.v)
                    l_Sort2 = b.i;

            }

            return l_Sort1 - l_Sort2;
        }

        private int LevelSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            return SortStringParse(m1.m_Level, m2.m_Level);
        }

        private int KegareSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            return SortStringParse(m1.m_Kegare, m2.m_Kegare);
        }

        private int RecognitionSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            return SortStringParse(m1.m_Recognition, m2.m_Recognition);
        }

        private int Weak_Point_ValueSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            return SortStringParse(m1.m_Weak_Point_Value, m2.m_Weak_Point_Value);
        }

        private int Preemptive_ValueSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            return SortStringParse(m1.m_Preemptive_Value, m2.m_Preemptive_Value);
        }

        private int SpeedSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            //スピードのみ比較
            string l_M1_Speed1 = m1.m_Move_Speed[0].Speed_ORG, l_M1_Speed2 = m1.m_Move_Speed[1].Speed_ORG;
            //例外系があったら-1にする
            if (m1.m_Move_Speed[0].Move_ORG == "-" || m1.m_Move_Speed[0].Move_ORG == "＊" || m1.m_Move_Speed[0].Move_ORG == "なし")
                l_M1_Speed1 = "-1";
            if (m1.m_Move_Speed[1].Move_ORG == "-" || m1.m_Move_Speed[1].Move_ORG == "＊" || m1.m_Move_Speed[1].Move_ORG == "なし")
                l_M1_Speed2 = "-1";

            //大きい数値の方を代表にする
            int l_M1max = Math.Max(int.Parse(l_M1_Speed1), int.Parse(l_M1_Speed2));

            string l_M2_Speed1 = m2.m_Move_Speed[0].Speed_ORG, l_M2_Speed2 = m2.m_Move_Speed[1].Speed_ORG;
            //例外系があったら-1にする
            if (m2.m_Move_Speed[0].Move_ORG == "-" || m2.m_Move_Speed[0].Move_ORG == "＊" || m2.m_Move_Speed[0].Move_ORG == "なし")
                l_M2_Speed1 = "-1";
            if (m2.m_Move_Speed[1].Move_ORG == "-" || m2.m_Move_Speed[1].Move_ORG == "＊" || m2.m_Move_Speed[1].Move_ORG == "なし")
                l_M2_Speed2 = "-1";

            //大きい数値の方を代表にする
            int l_M2max = Math.Max(int.Parse(l_M2_Speed1), int.Parse(l_M2_Speed2));

            return l_M1max - l_M2max;
        }

        private int Life_ResistanceSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            string l_Monster1 = m1.m_Life_Resistance;
            string l_Monster2 = m2.m_Life_Resistance;

            return SortStringParse(l_Monster1, l_Monster2);
        }

        private int Spirit_ResistanceSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            string l_Monster1 = m1.m_Spirit_Resistance;
            string l_Monster2 = m2.m_Spirit_Resistance;

            return SortStringParse(l_Monster1, l_Monster2);
        }

        private int HitPowerSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            int l_Monster1_n = 0;
            foreach (Monster.Status s in m1.m_Statuses)
            {
                string[] l_Sp = s.HitPower.Split('(');

                l_Monster1_n = Math.Max(l_Monster1_n, ExString(l_Sp[0]));
            }

            int l_Monster2_n = 0;
            foreach (Monster.Status s in m2.m_Statuses)
            {
                string[] l_Sp = s.HitPower.Split('(');

                l_Monster2_n = Math.Max(l_Monster2_n, ExString(l_Sp[0]));
            }

            return l_Monster1_n - l_Monster2_n;
        }

        private int DamageSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            int l_Monster1_n = 0;
            foreach (Monster.Status s in m1.m_Statuses)
            {
                string[] l_Sp = s.Damage.Split('(');

                l_Monster1_n = Math.Max(l_Monster1_n, ExString(l_Sp[0]));
            }

            int l_Monster2_n = 0;
            foreach (Monster.Status s in m2.m_Statuses)
            {
                string[] l_Sp = s.Damage.Split('(');

                l_Monster2_n = Math.Max(l_Monster2_n, ExString(l_Sp[0]));
            }

            return l_Monster1_n - l_Monster2_n;
        }

        private int DodgeSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            int l_Monster1_n = 0;
            foreach (Monster.Status s in m1.m_Statuses)
            {
                string[] l_Sp = s.Dodge.Split('(');

                l_Monster1_n = Math.Max(l_Monster1_n, ExString(l_Sp[0]));
            }

            int l_Monster2_n = 0;
            foreach (Monster.Status s in m2.m_Statuses)
            {
                string[] l_Sp = s.Dodge.Split('(');

                l_Monster2_n = Math.Max(l_Monster2_n, ExString(l_Sp[0]));
            }

            return l_Monster1_n - l_Monster2_n;
        }

        private int ProtectionSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            int l_Monster1_n = 0;
            foreach (Monster.Status s in m1.m_Statuses)
            {
                string[] l_Sp = s.Protection.Split('(');

                l_Monster1_n = Math.Max(l_Monster1_n, ExString(l_Sp[0]));
            }

            int l_Monster2_n = 0;
            foreach (Monster.Status s in m2.m_Statuses)
            {
                string[] l_Sp = s.Protection.Split('(');

                l_Monster2_n = Math.Max(l_Monster2_n, ExString(l_Sp[0]));
            }

            return l_Monster1_n - l_Monster2_n;
        }

        private int HPSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            int l_Monster1_n = 0;
            foreach (Monster.Status s in m1.m_Statuses)
            {
                string[] l_Sp = s.HP.Split('(');

                l_Monster1_n = Math.Max(l_Monster1_n, ExString(l_Sp[0]));
            }

            int l_Monster2_n = 0;
            foreach (Monster.Status s in m2.m_Statuses)
            {
                string[] l_Sp = s.HP.Split('(');

                l_Monster2_n = Math.Max(l_Monster2_n, ExString(l_Sp[0]));
            }

            return l_Monster1_n - l_Monster2_n;
        }

        private int MPSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            int l_Monster1_n = 0;
            foreach (Monster.Status s in m1.m_Statuses)
            {
                string[] l_Sp = s.MP.Split('(');

                l_Monster1_n = Math.Max(l_Monster1_n, ExString(l_Sp[0]));
            }

            int l_Monster2_n = 0;
            foreach (Monster.Status s in m2.m_Statuses)
            {
                string[] l_Sp = s.MP.Split('(');

                l_Monster2_n = Math.Max(l_Monster2_n, ExString(l_Sp[0]));
            }

            return l_Monster1_n - l_Monster2_n;
        }

        private int PartNumSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            return m1.m_Statuses.Count() - m2.m_Statuses.Count();
        }

        private int GamelSort(Monster m1, Monster m2)
        {
            if (m1 == null || m2 == null)
                return 0;

            int l_Monster1_n = 0;
            foreach (Monster.Booty s in m1.m_Booty)
            {
                l_Monster1_n = Math.Max(l_Monster1_n, ExString(s.Gamel));
            }

            int l_Monster2_n = 0;
            foreach (Monster.Booty s in m2.m_Booty)
            {
                l_Monster2_n = Math.Max(l_Monster2_n, ExString(s.Gamel));
            }

            return l_Monster1_n - l_Monster2_n;
        }

        private int SortStringParse(string t1, string t2)
        {
            return int.Parse(t1) - int.Parse(t2);

        }

        private int ExString(string text)
        {
            int l_Parse = 0;
            try
            {
                l_Parse = int.Parse(text);
            }
            catch
            {
                return -1;
            }

            return l_Parse;
        }

    }
}
