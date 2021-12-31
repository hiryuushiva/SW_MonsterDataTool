using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using SW_MonsterTool.Source.MData;

namespace SW_MonsterTool.Source.Utility.FileUtility
{
    public class FileUtil
    {
        public TextFieldParser m_NowFile;
        public List<string[]> m_CSVList;

        public FileUtil()
        {
            m_CSVList = new List<string[]>();
        }

        //ここに書くべきかどうか
        public List<JsonMonsterData> GetJson(TextFieldParser parser)
        {
            string l_Json = parser.ReadToEnd();

            if (l_Json == "")
                return new List<JsonMonsterData>();

            try
            {
                return JsonSerializer.Deserialize<List<JsonMonsterData>>(l_Json);
            }
            catch
            {
               return new List<JsonMonsterData>();
            }
        }
        public void WriteJson(List<JsonMonsterData> monster, string file_path)
        {
            var l_Options = new JsonSerializerOptions
            {
                // JavaScriptEncoder.Createでエンコードしない文字を指定するのも可
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                // 読みやすいようインデントを付ける
                WriteIndented = true
            };

            string l_Json = JsonSerializer.Serialize(monster, l_Options);

            FileWrite(file_path, l_Json, false, "utf-8");
        }

        //TextFieldParser形式データを取得する
        public TextFieldParser GetReadFile(string file_path, string encoding_name)
        {
            TextFieldParser l_Parser = null;
            try
            {
                l_Parser = new TextFieldParser(@file_path, Encoding.GetEncoding(encoding_name));
            }
            catch
            {
                return null;
            }

            return l_Parser;

        }

        //CSV形式にまとめられたものを書き込む
        //export側のデータがCSV形式にまとめられてなかった場合ただ改行して書き込むだけ
        public void CSVWrite(string file_path, List<string> export, bool append, string encoding_name)
        {
            using (var sw = new StreamWriter(file_path, append, Encoding.GetEncoding(encoding_name)))
            {
                foreach (string text in export)
                {
                    sw.WriteLine(text);
                }
                sw.Close();
            }
        }

        //ファイルを書き込む
        public void FileWrite(string file_path, string export, bool append, string encoding_name)
        {
            using (var sw = new StreamWriter(file_path, append, Encoding.GetEncoding(encoding_name)))
            {
                sw.Write(export);

                sw.Close();
            }
        }

        //ファイルをCSVの形で取得する
        public List<string[]> GetCSV(TextFieldParser parser)
        {
            List<string[]> l_Result = new List<string[]>();

            // カンマ区切りの指定
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            // フィールドが引用符で囲まれているか
            parser.HasFieldsEnclosedInQuotes = true;
            // フィールドの空白トリム設定
            parser.TrimWhiteSpace = false;

            // ファイルの終端までループ
            while (!parser.EndOfData)
            {
                // フィールドを読込
                string[] row = parser.ReadFields();
                l_Result.Add(row);
            }

            return l_Result;
        }

        //ファイルが使用されているかどうか
        //使用されていたら閉じる処理を行う
        public bool IsFileLocked(string path)
        {
            FileStream l_Stream = null;

            try
            {
                l_Stream = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.None);
            }
            catch
            {
                return true;
            }
            finally
            {
                if (l_Stream != null)
                    l_Stream.Close();
            }

            return false;
        }
    }
}
