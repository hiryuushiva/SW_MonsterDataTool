using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW_MonsterTool.Source.Utility.MathUtility;
using System.Windows.Forms;

namespace SW_MonsterTool.Source.MData
{

    //モンスターデータクラス
    public class Monster
    {

        public struct MoveSpeed
        {
            //記入されてる数値
            public string Move_ORG;
            public string Speed_ORG;
            //描画する場合の数値
            public string Move_View;
            public string Speed_View;
        }

        public struct WeakPoint
        {
            public string Element;
            public string Damage;
        }

        public struct Status
        {
            public string Part;
            public string HitPower;
            public string Damage;
            public string Dodge;
            public string Protection;
            public string HP;
            public string MP;
            public string Sword_Piece;
        }

        public struct Specal
        {
            public string Name;
            public string Declaration1;
            public string Declaration2;
            public string Declaration3;
            public string Effect;
        }

        public struct Booty
        {
            public string Dice;
            public string Name;
            public string Num;
            public string Gamel;
            public string Material;
        }

        //モンスター名
        public string m_Name;

        //分類
        public string m_Category;

        //出典
        public string m_Reference;

        //ページ数
        public string m_Page;

        //知能
        public string m_Intelligence;

        //知覚
        public string m_Perception;

        //反応
        public string m_Reaction;

        //穢れ
        public string m_Kegare;

        //知名度
        public string m_Recognition;

        //弱点値（数値）
        public string m_Weak_Point_Value;

        //先制値(名前イニシアチブにするべきだったかも)
        public string m_Preemptive_Value;

        //弱点（炎、氷など）
        public string m_Weak_Point;
        public WeakPoint m_Weak_Point_O;

        //生命抵抗力
        public string m_Life_Resistance;

        //精神抵抗力
        public string m_Spirit_Resistance;

        //移動速度
        public MoveSpeed[] m_Move_Speed;

        //言語リスト
        public List<string> m_Language;

        //生息地リスト
        public List<string> m_Habitat;

        //コア部位
        public string m_CorePart;

        //ステータスリスト
        public List<Status> m_Statuses;

        //特殊能力リスト
        public List<Specal> m_Specals;

        //戦利品リスト
        public List<Booty> m_Booty;

        //解説
        public string m_Description;

        //レベル
        public string m_Level;

        //剣の欠片
        public string m_TotalPiece;

        //画像
        public string m_Texture;

        //チャットパレット用

        //ダイス振るリスト
        public List<string> m_DicePallet;
        //固定値リスト
        public List<string> m_FixedPallet;


        ////以下出力しない値

        //ファイル元のURL
        //個々に保存したり編集する項目用
        public string m_File_Source_URL;

        //全く同じデータ用に自分の専用IDを記録しておく
        public string m_Sum_ID;

        public Monster()
        {
            m_Name = "";
            m_Category = "";
            m_Reference = "";
            m_Intelligence = "";
            m_Perception = "";
            m_Reaction = "";
            m_Kegare = "";
            m_Recognition = "";
            m_Weak_Point_Value = "";
            m_Preemptive_Value = "";
            m_Weak_Point = "";
            m_Weak_Point_O = new WeakPoint();
            m_Language = new List<string>();
            m_Habitat = new List<string>();
            m_CorePart = "";
            m_Statuses = new List<Status>();
            m_Specals = new List<Specal>();
            m_Booty = new List<Booty>();
            m_Description = "";
            m_Level = "";
            m_Life_Resistance = "";
            m_Spirit_Resistance = "";
            m_Move_Speed = new MoveSpeed[2];
            m_Page = "";
            m_TotalPiece = "0";
            m_Texture = "";
            m_Sum_ID = "";
            m_DicePallet = new List<string>();
            m_FixedPallet = new List<string>();

        }

        public Monster DeepClone()
        {
            // 簡易コピー
            Monster cloned = (Monster)MemberwiseClone();

            // 参照型フィールドの複製を作成する(簡易コピーを行う)
            if (this.m_Language != null)
            {
                cloned.m_Language = new List<string>(this.m_Language);
            }
            if (this.m_Habitat != null)
            {
                cloned.m_Habitat = new List<string>(this.m_Habitat);
            }
            if (this.m_Statuses != null)
            {
                cloned.m_Statuses = new List<Status>(this.m_Statuses);
            }
            if (this.m_Specals != null)
            {
                cloned.m_Specals = new List<Specal>(this.m_Specals);
            }
            if (this.m_Booty != null)
            {
                cloned.m_Booty = new List<Booty>(this.m_Booty);
            }


            return cloned;
        }

        //Json用モンスターデータ→モンスター型
        public static Monster ConvertFromJsonData(JsonMonsterData json)
        {
            Monster l_Result = new Monster();

            l_Result.m_Name = json.Name;

            l_Result.m_Category = json.Category;

            l_Result.m_Reference = json.Reference.Book;

            if (json.Reference.Page == "なし")
                l_Result.m_Page = "";
            else
                l_Result.m_Page = json.Reference.Page;

            l_Result.m_Intelligence = json.Inteligence;

            l_Result.m_Reaction = json.Reaction;

            l_Result.m_Perception = json.Perception;

            l_Result.m_Kegare = json.Kegare;

            l_Result.m_Recognition = json.Recognition;

            l_Result.m_Weak_Point_Value = json.WeakPointValue;

            l_Result.m_Preemptive_Value = json.PreemptiveValue;

            if (json.WeekPoint.Element != "なし")
                l_Result.m_Weak_Point = json.WeekPoint.Element + "+" + json.WeekPoint.Damage;
            else
                l_Result.m_Weak_Point = json.WeekPoint.Element;
            l_Result.m_Weak_Point_O.Element = json.WeekPoint.Element;
            l_Result.m_Weak_Point_O.Damage = json.WeekPoint.Damage;

            l_Result.m_Life_Resistance = json.LifeResistance;

            l_Result.m_Spirit_Resistance = json.SpiritResistance;

            {
                //1
                if (json.MoveSpeed[0].Move == "二足")
                    l_Result.m_Move_Speed[0].Move_View = "";
                else if (json.MoveSpeed[0].Move == "なし")
                    l_Result.m_Move_Speed[0].Move_View = "-";
                else
                    l_Result.m_Move_Speed[0].Move_View = "(" + json.MoveSpeed[0].Move + ")";

                if (json.MoveSpeed[1].Move == "二足")
                    l_Result.m_Move_Speed[1].Move_View = "";
                else if (json.MoveSpeed[1].Move == "-")
                    l_Result.m_Move_Speed[1].Move_View = "-";
                else
                    l_Result.m_Move_Speed[1].Move_View = "(" + json.MoveSpeed[1].Move + ")";

                //1
                if (json.MoveSpeed[0].Speed == "なし")
                    l_Result.m_Move_Speed[0].Speed_View = "";
                else
                    l_Result.m_Move_Speed[0].Speed_View = json.MoveSpeed[0].Speed;

                if (json.MoveSpeed[1].Speed == "なし")
                    l_Result.m_Move_Speed[1].Speed_View = "";
                else
                    l_Result.m_Move_Speed[1].Speed_View = json.MoveSpeed[1].Speed;

                l_Result.m_Move_Speed[0].Move_ORG = json.MoveSpeed[0].Move;
                l_Result.m_Move_Speed[0].Speed_ORG = json.MoveSpeed[0].Speed;

                l_Result.m_Move_Speed[1].Move_ORG = json.MoveSpeed[1].Move;
                l_Result.m_Move_Speed[1].Speed_ORG = json.MoveSpeed[1].Speed;
            }

            foreach (string lang in json.Language)
            {
                if (lang == "") continue;

                l_Result.m_Language.Add(lang);
            }

            foreach (string habitat in json.Habitat)
            {
                if (habitat == "") continue;

                l_Result.m_Habitat.Add(habitat);
            }

            l_Result.m_CorePart = json.CorePart;

            foreach (JsonStatus status in json.Statuses)
            {
                Status l_Status = new Status();

                l_Status.Part = status.Part;
                l_Status.HitPower = status.HitPower;
                l_Status.Damage = status.Damage;
                l_Status.Dodge = status.Dodge;
                l_Status.Protection = status.Protection;
                l_Status.HP = status.HP;
                l_Status.MP = status.MP;
                l_Status.Sword_Piece = status.SwordPiece;

                l_Result.m_Statuses.Add(l_Status);
            }

            foreach (JsonSpecal spe in json.Specal)
            {
                Specal l_Specal = new Specal();

                l_Specal.Name = spe.Name;
                l_Specal.Declaration1 = spe.Declaration1;
                l_Specal.Declaration2 = spe.Declaration2;
                l_Specal.Declaration3 = spe.Declaration3;
                l_Specal.Effect = spe.Effect;

                l_Result.m_Specals.Add(l_Specal);
            }

            foreach (JsonBooty booty in json.Booty)
            {
                Booty l_Booty = new Booty();

                l_Booty.Dice = booty.Dice;
                l_Booty.Name = booty.Name;
                l_Booty.Num = booty.Num;
                l_Booty.Gamel = booty.Gamel;
                l_Booty.Material = booty.Material;

                l_Result.m_Booty.Add(l_Booty);
            }

            l_Result.m_Description = json.Description;

            l_Result.m_Level = json.Level;

            l_Result.m_Texture = json.Texture;

            l_Result.m_DicePallet = json.ChatPalette.DicePalette;

            l_Result.m_FixedPallet = json.ChatPalette.FixedPalette;

            return l_Result;
        }

        //モンスター型→Json用のMonster
        public static JsonMonsterData ConvertToJson(Monster data)
        {
            JsonMonsterData l_Result = new JsonMonsterData();

            l_Result.Name = data.m_Name;

            //同名モンスターだった場合、いらないのを消す
            string l_SpritNum = l_Result.Name.Split(' ').Last();
            if (l_SpritNum.Contains("["))
                l_Result.Name = l_Result.Name.Replace(" " + l_SpritNum, "");

            l_Result.Category = data.m_Category;

            l_Result.Reference = new JsonReference();

            l_Result.Reference.Book = data.m_Reference;

            l_Result.Reference.Page = data.m_Page;
            if (l_Result.Reference.Page == "")
                l_Result.Reference.Page = "なし";

            l_Result.Inteligence = data.m_Intelligence;

            l_Result.Reaction = data.m_Reaction;

            l_Result.Kegare = data.m_Kegare;

            l_Result.Recognition = data.m_Recognition;

            l_Result.Perception = data.m_Perception;

            l_Result.WeakPointValue = data.m_Weak_Point_Value;

            l_Result.PreemptiveValue = data.m_Preemptive_Value;

            l_Result.WeekPoint = new JsonWeekPoint();
            l_Result.WeekPoint.Element = data.m_Weak_Point_O.Element;
            l_Result.WeekPoint.Damage = data.m_Weak_Point_O.Damage;

            l_Result.LifeResistance = data.m_Life_Resistance;

            l_Result.SpiritResistance = data.m_Spirit_Resistance;

            l_Result.MoveSpeed = new List<JsonMoveSpeed>();
            l_Result.MoveSpeed.Add(new JsonMoveSpeed());
            l_Result.MoveSpeed.Add(new JsonMoveSpeed());
            l_Result.MoveSpeed[0].Move = data.m_Move_Speed[0].Move_ORG;
            l_Result.MoveSpeed[0].Speed = data.m_Move_Speed[0].Speed_ORG;
            l_Result.MoveSpeed[1].Move = data.m_Move_Speed[1].Move_ORG;
            l_Result.MoveSpeed[1].Speed = data.m_Move_Speed[1].Speed_ORG;

            l_Result.Language = new List<string>();
            if (data.m_Language.Count != 0)
                foreach (string lang in data.m_Language)
                {
                    l_Result.Language.Add(lang);
                }
            else
                l_Result.Language.Add("なし");

            l_Result.Habitat = new List<string>();
            if (data.m_Habitat.Count != 0)
                foreach (string habitat in data.m_Habitat)
                {
                    l_Result.Habitat.Add(habitat);
                }
            else
                l_Result.Habitat.Add("不明");

            l_Result.CorePart = data.m_CorePart;
            if (l_Result.CorePart == "")
                l_Result.CorePart = "なし";

            l_Result.Statuses = new List<JsonStatus>();
            foreach (Status stetus in data.m_Statuses)
            {
                JsonStatus l_Ste = new JsonStatus();

                l_Ste.Part = stetus.Part;
                l_Ste.HitPower = stetus.HitPower;
                l_Ste.Damage = stetus.Damage;
                l_Ste.Dodge = stetus.Dodge;
                l_Ste.Protection = stetus.Protection;
                l_Ste.HP = stetus.HP;
                l_Ste.MP = stetus.MP;
                l_Ste.SwordPiece = stetus.Sword_Piece;

                l_Result.Statuses.Add(l_Ste);
            }

            l_Result.Specal = new List<JsonSpecal>();
            if (data.m_Specals.Count != 0)
                foreach (Specal specal in data.m_Specals)
                {
                    JsonSpecal l_Spe = new JsonSpecal();

                    l_Spe.Name = specal.Name;
                    l_Spe.Declaration1 = specal.Declaration1;
                    l_Spe.Declaration2 = specal.Declaration2;
                    l_Spe.Declaration3 = specal.Declaration3;
                    l_Spe.Effect = specal.Effect;

                    l_Result.Specal.Add(l_Spe);
                }
            else
            {
                JsonSpecal l_Spe = new JsonSpecal();

                l_Spe.Name = "なし";
                l_Spe.Declaration1 = "なし";
                l_Spe.Declaration2 = "なし";
                l_Spe.Declaration3 = "なし";
                l_Spe.Effect = "なし";

                l_Result.Specal.Add(l_Spe);
            }

            l_Result.Booty = new List<JsonBooty>();
            if (data.m_Booty.Count != 0)
                foreach (Booty booty in data.m_Booty)
                {
                    JsonBooty l_Booty = new JsonBooty();

                    l_Booty.Dice = booty.Dice;
                    if (l_Booty.Dice == "")
                        l_Booty.Dice = "なし";
                    l_Booty.Name = booty.Name;
                    if (l_Booty.Name == "")
                        l_Booty.Name = "なし";
                    l_Booty.Num = booty.Num;
                    if (l_Booty.Num == "")
                        l_Booty.Num = "0";
                    l_Booty.Gamel = booty.Gamel;
                    if (l_Booty.Gamel == "")
                        l_Booty.Gamel = "0";
                    l_Booty.Material = booty.Material;
                    if (l_Booty.Material == "")
                        l_Booty.Material = "なし";

                    l_Result.Booty.Add(l_Booty);
                }
            else
            {
                JsonBooty l_Booty = new JsonBooty();

                l_Booty.Dice = "なし";
                l_Booty.Name = "なし";
                l_Booty.Num = "0";
                l_Booty.Gamel = "0";
                l_Booty.Material = "なし";
                l_Result.Booty.Add(l_Booty);
            }

            l_Result.Description = data.m_Description;
            if (l_Result.Description == "")
                l_Result.Description = "詳細不明";

            l_Result.Level = data.m_Level;

            l_Result.TotalSwordPiece = data.m_TotalPiece;

            l_Result.Texture = data.m_Texture;

            l_Result.ChatPalette = new JsonChatPalette();
            l_Result.ChatPalette.DicePalette = data.m_DicePallet;

            l_Result.ChatPalette.FixedPalette = data.m_FixedPallet;

            return l_Result;
        }

        //Monster型→CSV用形式に変換
        public static List<string> MonsterToCSV(Monster m)
        {
            string l_Name = m.m_Name;
            if (l_Name.Contains("\""))
                l_Name = l_Name.Replace("\"", "\"\"");

            //同名モンスターだった場合、いらないのを消す
            string l_SpritNum = l_Name.Split(' ').Last();
            if (l_SpritNum.Contains("["))
                l_Name = l_Name.Replace(" " + l_SpritNum, "");

            string l_Reaction = m.m_Reaction;
            if (l_Reaction.Contains("\""))
                l_Reaction = l_Reaction.Replace("\"", "\"\"");

            string l_Category = m.m_Category;
            if (l_Category.Contains("\""))
                l_Category = l_Category.Replace("\"", "\"\"");

            string l_Kegare = m.m_Kegare;

            string l_Reference = m.m_Reference;
            if (l_Reference.Contains("\""))
                l_Reference = l_Reference.Replace("\"", "\"\"");

            string l_Page = m.m_Page;
            if (l_Page == "")
                l_Page = "なし";

            string l_Chimei = m.m_Recognition;

            string l_Intelligence = m.m_Intelligence;
            if (l_Intelligence.Contains("\""))
                l_Intelligence = l_Intelligence.Replace("\"", "\"\"");

            string l_Weak_Point_Value = m.m_Weak_Point_Value;

            string l_Perception = m.m_Perception;
            if (l_Perception.Contains("\""))
                l_Perception = l_Perception.Replace("\"", "\"\"");

            string l_Sensei = m.m_Preemptive_Value;

            string l_Element = m.m_Weak_Point_O.Element;
            if (l_Element.Contains("\""))
                l_Element = l_Element.Replace("\"", "\"\"");

            string l_WeakDamage = m.m_Weak_Point_O.Damage;
            //生命
            string l_Seimei = m.m_Life_Resistance;

            //精神
            string l_Seisin = m.m_Spirit_Resistance;

            //移動
            string l_Idou1 = m.m_Move_Speed[0].Move_ORG;
            if (l_Idou1.Contains("\""))
                l_Idou1 = l_Idou1.Replace("\"", "\"\"");

            string l_Idou1_N = m.m_Move_Speed[0].Speed_ORG;

            string l_Idou2 = m.m_Move_Speed[1].Move_ORG;
            if (l_Idou2.Contains("\""))
                l_Idou2 = l_Idou2.Replace("\"", "\"\"");

            string l_Idou2_N = m.m_Move_Speed[1].Speed_ORG;

            //言語
            string l_Lang = "なし";
            if (m.m_Language.Count != 0)
            {
                int i = 0;
                foreach (string lang in m.m_Language)
                {
                    if (i == 0)
                        l_Lang = "";

                    string l_LangText = lang;
                    if (l_LangText.Contains("\""))
                        l_LangText = l_LangText.Replace("\"", "\"\"");

                    l_Lang += "\"" + l_LangText + "\"" + ",";
                    i++;
                }
            }

            //生息地
            string l_Seisokuti = "不明";
            if (m.m_Habitat.Count != 0)
            {
                int i = 0;
                foreach (string seisoku in m.m_Habitat)
                {
                    if (i == 0)
                        l_Seisokuti = "";

                    string l_HabiText = seisoku;
                    if (l_HabiText.Contains("\""))
                        l_HabiText = l_HabiText.Replace("\"", "\"\"");

                    l_Seisokuti += "\"" + l_HabiText + "\"" + ",";
                    i++;
                }
            }

            //基礎ステータス
            string l_Core = m.m_CorePart;
            if (l_Core == "")
                l_Core = "なし";
            if (l_Core.Contains("\""))
                l_Core = l_Core.Replace("\"", "\"\"");

            string l_BuiName = "";
            string l_Hit = "";
            string l_Damage = "";
            string l_Kaihi = "";
            string l_Bougo = "";
            string l_HP = "";
            string l_MP = "";
            foreach (Monster.Status status in m.m_Statuses)
            {
                string l_LBui = status.Part;
                if (l_LBui.Contains("\""))
                    l_LBui = l_LBui.Replace("\"", "\"\"");

                string l_LHit = status.HitPower;
                if (l_LHit.Contains("\""))
                    l_LHit = l_LHit.Replace("\"", "\"\"");

                string l_LDama = status.Damage;
                if (l_LDama.Contains("\""))
                    l_LDama = l_LDama.Replace("\"", "\"\"");

                string l_LDodge = status.Dodge;
                if (l_LDodge.Contains("\""))
                    l_LDodge = l_LDodge.Replace("\"", "\"\"");

                string l_LPro = status.Protection;
                if (l_LPro.Contains("\""))
                    l_LPro = l_LPro.Replace("\"", "\"\"");

                string l_LHP = status.HP;
                if (l_LHP.Contains("\""))
                    l_LHP = l_LHP.Replace("\"", "\"\"");

                string l_LMP = status.MP;
                if (l_LMP.Contains("\""))
                    l_LMP = l_LMP.Replace("\"", "\"\"");

                l_BuiName += "\"" + l_LBui + "\"" + ",";

                l_Hit += "\"" + l_LHit + "\"" + ",";

                l_Damage += "\"" + l_LDama + "\"" + ",";

                l_Kaihi += "\"" + l_LDodge + "\"" + ",";

                l_Bougo += "\"" + l_LPro + "\"" + ",";

                l_HP += "\"" + l_LHP + "\"" + ",";

                l_MP += "\"" + l_LMP + "\"" + ",";
            }

            string l_SpeName = "";
            string l_SpeDeclaration_1 = "";
            string l_SpeDeclaration_2 = "";
            string l_SpeDeclaration_3 = "";
            string l_Spe = "";
            if (m.m_Specals.Count != 0)
            {
                foreach (Monster.Specal specal in m.m_Specals)
                {
                    string l_LSpName = specal.Name;
                    if (l_LSpName.Contains("\""))
                        l_LSpName = l_LSpName.Replace("\"", "\"\"");

                    string l_LSpDec1 = specal.Declaration1;
                    if (l_LSpDec1.Contains("\""))
                        l_LSpDec1 = l_LSpDec1.Replace("\"", "\"\"");

                    string l_LSpDec2 = specal.Declaration2;
                    if (l_LSpDec2.Contains("\""))
                        l_LSpDec2 = l_LSpDec2.Replace("\"", "\"\"");

                    string l_LSpDec3 = specal.Declaration3;
                    if (l_LSpDec3.Contains("\""))
                        l_LSpDec3 = l_LSpDec3.Replace("\"", "\"\"");

                    string l_LSpEffe = specal.Effect;
                    if (l_LSpEffe.Contains("\""))
                        l_LSpEffe = l_LSpEffe.Replace("\"", "\"\"");

                    l_SpeName += "\"" + l_LSpName + "\"" + ",";
                    l_SpeDeclaration_1 += "\"" + l_LSpDec1 + "\"" + ",";
                    l_SpeDeclaration_2 += "\"" + l_LSpDec2 + "\"" + ",";
                    l_SpeDeclaration_3 += "\"" + l_LSpDec3 + "\"" + ",";
                    l_Spe += "\"" + l_LSpEffe + "\"" + ",";
                }
            }
            else
            {
                l_SpeName = "なし";
                l_SpeDeclaration_1 = "なし";
                l_SpeDeclaration_2 = "なし";
                l_SpeDeclaration_3 = "なし";
                l_Spe = "なし";
            }

            string l_Dice = "";
            string l_SenName = "";
            string l_SenNum = "";
            string l_Gamel = "";
            string l_Mate = "";
            if (m.m_Booty.Count != 0)
            {
                foreach (Monster.Booty booty in m.m_Booty)
                {
                    string l_DiceText = booty.Dice;
                    if (l_DiceText.Contains("\""))
                        l_DiceText = l_DiceText.Replace("\"", "\"\"");

                    string l_BootyNameText = booty.Name;
                    if (l_BootyNameText.Contains("\""))
                        l_BootyNameText = l_BootyNameText.Replace("\"", "\"\"");

                    string l_BootyNumText = booty.Num;
                    if (l_BootyNumText.Contains("\""))
                        l_BootyNumText = l_BootyNumText.Replace("\"", "\"\"");

                    string l_GamelText = booty.Gamel;
                    if (l_GamelText.Contains("\""))
                        l_GamelText = l_GamelText.Replace("\"", "\"\"");

                    string l_MateText = booty.Material;
                    if (l_MateText.Contains("\""))
                        l_MateText = l_MateText.Replace("\"", "\"\"");

                    if (l_BootyNameText == "")
                        l_BootyNameText = "なし";
                    if (l_BootyNumText == "")
                        l_BootyNumText = "0";
                    if (l_GamelText == "")
                        l_GamelText = "0";
                    if (l_MateText == "")
                        l_MateText = "なし";

                    l_Dice += "\"" + l_DiceText + "\"" + ",";
                    l_SenName += "\"" + l_BootyNameText + "\"" + ",";
                    l_SenNum += "\"" + l_BootyNumText + "\"" + ",";
                    l_Gamel += "\"" + l_GamelText + "\"" + ",";
                    l_Mate += "\"" + l_MateText + "\"" + ",";

                }
            }
            else
            {
                l_Dice = "なし";
                l_SenName = "なし";
                l_SenNum = "0";
                l_Gamel = "0";
                l_Mate = "なし";
            }

            string l_Kaisetu = m.m_Description;
            if (l_Kaisetu == "")
                l_Kaisetu = "詳細不明";
            if (l_Kaisetu.Contains("\""))
                l_Kaisetu = l_Kaisetu.Replace("\"", "\"\"");

            string l_Level = m.m_Level;

            string l_Texture = m.m_Texture;

            //チャパレ保存
            string l_DiceP = "";
            string l_FixedP = "";

            //ダイス振る
            foreach (string palette in m.m_DicePallet)
            {
                l_DiceP += palette + ReplacementList.m_LineFeed_R;
            }
            //固定値
            foreach (string palette in m.m_FixedPallet)
            {
                l_FixedP += palette + ReplacementList.m_LineFeed_R;
            }

            //\n\rで吐き出すと勝手に改行されるので変換しておく
            //読み込むときは逆処理
            l_Kaisetu = l_Kaisetu.Replace("\r", "");
            l_Spe = l_Spe.Replace("\r", "");

            l_Kaisetu = l_Kaisetu.Replace("\n", ReplacementList.m_LineFeed_R);
            l_Spe = l_Spe.Replace("\n", ReplacementList.m_LineFeed_R);

            //後で見やすいように書いておく
            List<string> l_Result = new List<string>();
            l_Result.Add("名前," + "\"" + l_Name + "\"" + ",,反応," + "\"" + l_Reaction + "\"");
            l_Result.Add("分類," + "\"" + l_Category + "\"" + ",,穢れ," + l_Kegare);
            l_Result.Add("出典," + "\"" + l_Reference + "\"" + "," + l_Page + ",知名度," + l_Chimei);
            l_Result.Add("知能," + "\"" + l_Intelligence + "\"" + ",,弱点値," + l_Weak_Point_Value);
            l_Result.Add("知覚," + "\"" + l_Perception + "\"" + ",,先制値," + l_Sensei);
            l_Result.Add("弱点," + "\"" + l_Element + "\"" + "," + l_WeakDamage);
            l_Result.Add("生命抵抗力," + l_Seimei + "," + MathUtil.AddStringNum(l_Seimei, "7"));
            l_Result.Add("精神抵抗力," + l_Seisin + "," + MathUtil.AddStringNum(l_Seisin, "7"));
            l_Result.Add("移動速度," + "\"" + l_Idou1 + "\"" + "," + l_Idou1_N + "," + "\"" + l_Idou2 + "\"" + "," + l_Idou2_N);
            l_Result.Add("言語," + l_Lang);
            l_Result.Add("生息地," + l_Seisokuti);
            l_Result.Add("基礎ステータス,コア部位," + l_Core);
            l_Result.Add("部位," + l_BuiName);
            l_Result.Add("命中力," + l_Hit);
            l_Result.Add("打撃点," + l_Damage);
            l_Result.Add("回避力," + l_Kaihi);
            l_Result.Add("防護点," + l_Bougo);
            l_Result.Add("HP," + l_HP);
            l_Result.Add("MP," + l_MP);
            l_Result.Add("特殊能力,");
            l_Result.Add("名称," + l_SpeName);
            l_Result.Add("宣言1," + l_SpeDeclaration_1);
            l_Result.Add("宣言2," + l_SpeDeclaration_2);
            l_Result.Add("宣言3," + l_SpeDeclaration_3);
            l_Result.Add("効果," + l_Spe);
            l_Result.Add("戦利品,");
            l_Result.Add("ダイス目," + l_Dice);
            l_Result.Add("名称," + l_SenName);
            l_Result.Add("個数," + l_SenNum);
            l_Result.Add("値段," + l_Gamel);
            l_Result.Add("マテリアルカード," + l_Mate);
            l_Result.Add("解説," + "\"" + l_Kaisetu + "\"");
            l_Result.Add("レベル," + l_Level + ",画像," + l_Texture);
            l_Result.Add("チャットパレット,"+ "\"" + l_DiceP + "\"" + "," + "\"" + l_FixedP+ "\"");
            l_Result.Add("END,");


            return l_Result;
        }

        //一応用意
        //ただ引数めんどい
        public static int FormToMonster(MonsterFormTools form,WindowCenter center,IWin32Window parent,bool output, ref Monster m)
        {
            Monster l_Monster = new Monster();

            l_Monster.m_Name = form.MonsterNameBox.Text;

            if (l_Monster.m_Name == "" && !output)
            {
                center.SetHook(parent); ;
                MessageBox.Show(parent, "名前を入力してください", "問題", MessageBoxButtons.OK);
                return -1;
            }

            l_Monster.m_Reaction = form.ReactionBox.Text;
            l_Monster.m_Category = form.CategoryBox.Text;
            l_Monster.m_Kegare = form.KegareNumUpDown.Value.ToString();
            l_Monster.m_Reference = form.ReferenceBox.Text;

            if (form.PageNumUpDown.Value.ToString() == "-1")
                l_Monster.m_Page = "";
            else
                l_Monster.m_Page = form.PageNumUpDown.Value.ToString();

            l_Monster.m_Recognition = form.Name_RecognitionNumUpDown.Value.ToString();
            l_Monster.m_Intelligence = form.IntelligenceBox.Text;
            l_Monster.m_Weak_Point_Value = form.Weak_Point_ValueNumUpDown.Value.ToString();
            if (l_Monster.m_Weak_Point_Value == "-1")
                l_Monster.m_Weak_Point_Value = "なし";
            l_Monster.m_Perception = form.PerceptionBox.Text;
            l_Monster.m_Preemptive_Value = form.Preemptive_ValueNumUpDown.Value.ToString();

            string l_Element = form.Weak_PointBox.Text;
            string l_WeakDamage = form.Weak_PointNumUpDown.Value.ToString();
            if (l_WeakDamage == "-1")
            {
                l_Monster.m_Weak_Point = l_Element;
                l_Monster.m_Weak_Point_O.Element = l_Element;
                l_Monster.m_Weak_Point_O.Damage = "なし";
            }
            else
            {
                l_Monster.m_Weak_Point = l_Element + "+" + l_WeakDamage;
                l_Monster.m_Weak_Point_O.Element = l_Element;
                l_Monster.m_Weak_Point_O.Damage = l_WeakDamage;
            }

            string l_Seimei = form.Life_ResistanceNumUpDown.Value.ToString();
            string l_Seimei_K = MathUtil.AddStringNum(l_Seimei, "7");

            l_Monster.m_Life_Resistance = l_Seimei;

            string l_Seisin = form.Spirit_ResistanceNumUpDown.Value.ToString();
            string l_Seisin_K = MathUtil.AddStringNum(l_Seisin, "7");

            l_Monster.m_Spirit_Resistance = l_Seisin;

            //移動方法が記載されてない場合二足として扱う
            if (form.Move_SpeedBox1.Text == "")
            {
                l_Monster.m_Move_Speed[0].Move_View = "";
                l_Monster.m_Move_Speed[0].Move_ORG = "二足";
            }
            //なし
            else if (form.Move_SpeedBox1.Text == "-")
            {
                l_Monster.m_Move_Speed[0].Move_View = "-";
                l_Monster.m_Move_Speed[0].Move_ORG = "-";
            }
            //二足以外の方法が存在
            else
            {
                l_Monster.m_Move_Speed[0].Move_View = form.Move_SpeedBox1.Text;
                l_Monster.m_Move_Speed[0].Move_ORG = form.Move_SpeedBox1.Text;
            }

            string l_Idou1_N = form.Move_SpeedNumUpDown1.Value.ToString();
            l_Monster.m_Move_Speed[0].Speed_View = l_Idou1_N;
            l_Monster.m_Move_Speed[0].Speed_ORG = l_Idou1_N;
            //参照元モンスター
            if (l_Idou1_N == "-1" && form.Move_SpeedBox1.Text == "＊")
            {
                l_Monster.m_Move_Speed[0].Speed_View = "元参照";
                l_Monster.m_Move_Speed[0].Speed_ORG = "元参照";
            }
            //入力なし
            else if (l_Idou1_N == "-1")
            {
                l_Monster.m_Move_Speed[0].Speed_View = "";
                l_Monster.m_Move_Speed[0].Speed_ORG = "なし";
            }


            if (form.Move_SpeedBox2.Text == "")
            {
                l_Monster.m_Move_Speed[1].Move_View = "";
                l_Monster.m_Move_Speed[1].Move_ORG = "二足";
            }
            else if (form.Move_SpeedBox2.Text == "-")
            {
                l_Monster.m_Move_Speed[1].Move_View = "-";
                l_Monster.m_Move_Speed[1].Move_ORG = "-";
            }
            else
            {
                l_Monster.m_Move_Speed[1].Move_View = form.Move_SpeedBox2.Text;
                l_Monster.m_Move_Speed[1].Move_ORG = form.Move_SpeedBox2.Text;
            }

            string l_Idou2_N = form.Move_SpeedNumUpDown2.Value.ToString();
            l_Monster.m_Move_Speed[1].Speed_View = l_Idou1_N;
            l_Monster.m_Move_Speed[1].Speed_ORG = l_Idou1_N;
            if (l_Idou2_N == "-1" && form.Move_SpeedBox2.Text == "＊")
            {
                l_Monster.m_Move_Speed[1].Speed_View = "元参照";
                l_Monster.m_Move_Speed[1].Speed_ORG = "元参照";
            }
            else if (l_Idou2_N == "-1")
            {
                l_Monster.m_Move_Speed[1].Speed_View = "";
                l_Monster.m_Move_Speed[1].Speed_ORG = "なし";
            }

            if ((l_Monster.m_Reaction == "" || l_Monster.m_Category == "" || l_Monster.m_Reference == "" || l_Monster.m_Intelligence == "" ||
             l_Monster.m_Perception == "" || l_Element == "") && !output)
            {
                center.SetHook(parent); ;
                MessageBox.Show(parent, "未選択項目があります\n移動の未選択は二足になります", "問題", MessageBoxButtons.OK);
                return -1;
            }

            if ((l_Monster.m_Recognition == "0" || l_Monster.m_Weak_Point_Value == "0" || l_Monster.m_Reference == "0" || l_WeakDamage == "0" ||
                l_Seimei_K == "0" || l_Seisin_K == "0")&&!output)
            {
                center.SetHook(parent); ;
                MessageBox.Show(parent, "0ではいけない値があります", "問題", MessageBoxButtons.OK);
                return -1;
            }

            if ((l_Idou2_N == "0" || l_Idou1_N == "0")&&!output)
            {
                center.SetHook(parent); ;
                MessageBox.Show(parent, "移動力0はありません\n移動しなければ-を選択してください", "問題", MessageBoxButtons.OK);
                return -1;
            }

            //言語
            string l_Lang = "なし";
            if (form.LanguagePanel.Controls.Count != 0)
            {
                int i = 0;
                foreach (Panel panel in form.LanguagePanel.Controls)
                {
                    ComboBox l_combo = (ComboBox)panel.Controls[0];
                    if (l_combo.Text != "" && l_combo.Text != "なし")
                    {
                        l_Monster.m_Language.Add(l_combo.Text);
                        i++;
                    }
                }
            }
            else
            {
                l_Monster.m_Language.Add(l_Lang);
            }

            //生息地
            string l_Seisokuti = "不明";
            if (form.HabitatPanel.Controls.Count != 0)
            {
                int i = 0;
                foreach (Panel panel in form.HabitatPanel.Controls)
                {
                    TextBox l_text = (TextBox)panel.Controls[0];
                    if (l_text.Text != "")
                    {
                        l_Monster.m_Habitat.Add(l_text.Text);
                        i++;
                    }
                }
            }
            else
            {
                l_Monster.m_Habitat.Add(l_Seisokuti);
            }

            //基礎ステータス
            l_Monster.m_CorePart = form.CorePartBox.Text;
            if (l_Monster.m_CorePart == "")
                l_Monster.m_CorePart = "なし";

            bool l_StatusCheck = false;

            foreach (ListViewItem item in form.StatusListView.Items)
            {
                Monster.Status status = new Monster.Status();

                if (item.SubItems.Count == 0)
                {
                    l_StatusCheck = true;
                    break;
                }

                status.Part = item.SubItems[1].Text;
                status.HitPower = item.SubItems[2].Text;
                status.Damage = item.SubItems[3].Text;
                status.Dodge = item.SubItems[4].Text;
                status.Protection = item.SubItems[5].Text;
                status.HP = item.SubItems[6].Text;
                status.MP = item.SubItems[7].Text;
                status.Sword_Piece = "0";

                l_Monster.m_Statuses.Add(status);
            }


            if (l_StatusCheck && !output)
            {
                center.SetHook(parent); ;
                MessageBox.Show(parent, "ステータスを設定してください\nダブルクリックで編集できます\n", "問題", MessageBoxButtons.OK);
                return -1;
            }

            //特殊能力
            if (form.SPAbilityPanel.Controls.Count != 0)
            {
                foreach (Panel panel in form.SPAbilityPanel.Controls)
                {
                    ComboBox l_Declaration1 = (ComboBox)panel.Controls[0];
                    ComboBox l_Declaration2 = (ComboBox)panel.Controls[1];
                    ComboBox l_Declaration3 = (ComboBox)panel.Controls[2];
                    TextBox l_text = (TextBox)panel.Controls[3];
                    RichTextBox richText = (RichTextBox)panel.Controls[4];

                    Monster.Specal specal = new Monster.Specal();

                    specal.Name = l_text.Text;
                    specal.Declaration1 = l_Declaration1.Text;
                    specal.Declaration2 = l_Declaration2.Text;
                    specal.Declaration3 = l_Declaration3.Text;
                    specal.Effect = richText.Text;

                    l_Monster.m_Specals.Add(specal);
                }
            }
            else
            {
                Monster.Specal specal = new Monster.Specal();

                specal.Name = "なし";
                specal.Declaration1 = "なし";
                specal.Declaration2 = "なし";
                specal.Declaration3 = "なし";
                specal.Effect = "なし";

                l_Monster.m_Specals.Add(specal);
            }

            //戦利品
            if (form.BootyPanel.Controls.Count != 0)
            {
                foreach (Panel panel in form.BootyPanel.Controls)
                {
                    ComboBox dice = (ComboBox)panel.Controls[0];
                    TextBox s_Name = (TextBox)panel.Controls[1];
                    TextBox s_Num = (TextBox)panel.Controls[3];
                    TextBox gamel = (TextBox)panel.Controls[4];
                    ComboBox mate = (ComboBox)panel.Controls[6];

                    Monster.Booty booty = new Monster.Booty();

                    string stex = s_Name.Text;
                    string numtex = s_Num.Text;
                    string game = gamel.Text;
                    string matex = mate.Text;

                    booty.Dice = dice.Text;

                    if (stex == "")
                        stex = "なし";
                    if (numtex == "")
                        numtex = "0";
                    if (game == "")
                        game = "0";
                    if (matex == "")
                        matex = "なし";

                    booty.Name = stex;
                    booty.Num = numtex;
                    booty.Gamel = game;
                    booty.Material = matex;

                    l_Monster.m_Booty.Add(booty);
                }
            }
            else
            {
                Monster.Booty booty = new Monster.Booty();

                booty.Dice = "なし";
                booty.Name = "なし";
                booty.Num = "0";
                booty.Gamel = "0";
                booty.Material = "なし";

                l_Monster.m_Booty.Add(booty);
            }

            l_Monster.m_Description = form.DescriptionTextBox.Text;

            l_Monster.m_Level = form.LevelNumUpDown.Value.ToString();

            l_Monster.m_Texture = form.TextureURL.Text;

            //ダイスパレット消さない為
            l_Monster.m_DicePallet = m.m_DicePallet;
            l_Monster.m_FixedPallet = m.m_FixedPallet;

            m = l_Monster;

            return 1;

        }

    }
}
