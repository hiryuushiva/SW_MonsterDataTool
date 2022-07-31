using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_MonsterTool.Source.Utility.MathUtility
{
    public class MathUtil
    {

        //文字同士の計算
        public static string AddStringNum(string value1, string value2)
        {
            int v1, v2;
            try
            {
                v1 = int.Parse(value1);
                v2 = int.Parse(value2);
            }
            catch
            {
                return "0";
            }

            return (v1 + v2).ToString();
        }

        public static string SubStringNum(string value1, string value2)
        {
            int v1, v2;

            try
            {
                v1 = int.Parse(value1);
                v2 = int.Parse(value2);
            }
            catch
            {
                return "0";
            }

            return (v1 - v2).ToString();
        }

        public static int Clamp(int value,int min ,int max)
        {
            return Math.Max(min, Math.Min(value, max));
        }
    }
}
