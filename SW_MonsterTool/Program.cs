using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW_MonsterTool
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                OutputErrLog(ex);
            }
        }

        /**
        *  エラー調査用ログ出力
        *  @param ex catchしたエラー内容
        */
        static public void OutputErrLog(Exception ex)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path2 = "ErrorLog_"+ DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            path = Path.Combine(path, path2);


            // 書き込み設定
            System.IO.StreamWriter sw = new StreamWriter(
             path,   // 出力先ファイル名
             true,                                                           // 追加書き込み
             System.Text.Encoding.GetEncoding("Shift_JIS"));                 // 文字コード

            // ログ出力
            Console.SetOut(sw); // 出力先（Outプロパティ）を設定
            Console.WriteLine("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "] message: " + ex.Message);
            Console.WriteLine("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "] stack trace: " + ex.StackTrace);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine();

            // ファイルを閉じる
            sw.Dispose();
        }
    }
}
