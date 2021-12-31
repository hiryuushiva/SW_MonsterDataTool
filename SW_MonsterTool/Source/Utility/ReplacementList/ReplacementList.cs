using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_MonsterTool
{
    //置換文字リスト
    public static class ReplacementList
    {
        //改行用置換文字
        public static string m_LineFeed_R = "/k";

        //以下現在未使用
        //記入された,用置換文字
        public static string m_Comma_R = "/?)";
        //CSV区切りに戻す,用置換文字
        public static string m_CSVComma_R = "/!)";
        //文字列に含まれた"用置換文字
        public static string m_DoubleC_R = "/$)";
        
    }
}
