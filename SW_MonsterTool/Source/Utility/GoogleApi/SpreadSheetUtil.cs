using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

//スプレッドシートにデータを上げるクラス
//同時にデータが上がった時の処理など問題が多そうだったので実装を見送り
namespace SW_MonsterTool.Source.Utility.GoogleApi
{
    public static class SpreadSheetUtil
    {
        //参考記事:https://qiita.com/kik4/items/0723ce68d6bf994a5815

        // もしスコープを変えた時は事前にあるcredential（~/.credentials/sheets.googleapis.com-dotnet-quickstart.json）を消すこと
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "MonsterTool";
        // スプレッドシートID
        static String spreadsheetId = "";

        static UserCredential credential;

        static SpreadSheetUtil()
        {

            // credentialを取得
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = AppDomain.CurrentDomain.BaseDirectory;
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credentialの保存先: " + credPath);
            }

        }

        static public void WriteSheet(List<string> texts)
        {
            // Google Sheets API サービスを作る
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            //縦に記入する
            //今回は,で区切らずそのまま
            List<IList<object>> tes = new List<IList<object>>();
            foreach(string text in texts)
            {
                tes.Add(new List<object> { text });
            }

            var body = new ValueRange() { Values = tes };
            var req = service.Spreadsheets.Values.Append(body, spreadsheetId, "Sheet1!A1");
            req.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var result = req.Execute();

            // 終わり
            Console.WriteLine("書き込み完了");

        }

        static public void ReadSheet()
        {
            // Google Sheets API サービスを作る
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            //Rangeの所でどのシートで、どれぐらいの範囲かを決められる
            SpreadsheetsResource.ValuesResource.GetRequest request =
        service.Spreadsheets.Values.Get(spreadsheetId, "Sheet1");

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine(row[0]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.Read();
        }

        //スプレッドシートなのかどうか
        static public bool IsSpreadSheet(string url)
        {
            string front = "https://docs.google.com/spreadsheets/d/";
            string back = "/edit#gid=0";

            if (url.Contains(front) && url.Contains(back))
                return true;
            else
                return false;
        }

        static public void SetSpreadSheet(string url)
        {
            string front = "https://docs.google.com/spreadsheets/d/";
            string back = "/edit#gid=0";

            string id = url.Replace(front,"");
            id = id.Replace(back, "");

            spreadsheetId = id;
        }
    }
}
