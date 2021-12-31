using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SW_MonsterTool.Source.Utility.CCFOLIAUtil
{
    public class CCFOLIACharacterJson
    {
        public string kind { get; set; }

        public CCFOLIACharacterData data { get; set; }
    }

    public class CCFOLIACharacter_Status
    {
        public string label { get; set; }

        public int value { get; set; }

        public int max { get; set; }
    }

    public class CCFOLIACharacter_Params
    {
        public string label { get; set; }

        public string value { get; set; }
    }

    public class CCFOLIACharacter_Faces
    {
        public string iconUrl { get; set; }

        public string label { get; set; }
    }

    //入力不要なのはコメントアウトしてある
    public class CCFOLIACharacterData
    {
        public string name { get; set; }

        public string memo { get; set; }

        public int initiative { get; set; }

        public string externalUrl { get; set; }

        public List<CCFOLIACharacter_Status> status { get; set; }

        [JsonPropertyName("params")]
        public List<CCFOLIACharacter_Params> parameter { get; set; }

        //public string iconUrl { get; set; }

        //public List<CCFOLIACharacter_Faces> faces { get; set; }

        //public int x { get; set; }

        //public int y { get; set; }

        //public int angle { get; set; }

        //public int width { get; set; }

        //public int height { get; set; }

        //public bool active { get; set; }

        //ステータスを非公開にする
        public bool secret { get; set; }

        //発言時キャラクターを表示しない
        public bool invisible { get; set; }

        //盤面キャラクター一覧に表示しない
        public bool hideStatus { get; set; }

        //public string color { get; set; }

        //チャパレ
        public string commands { get; set; }

        //public string owner { get; set; }

    }

}
