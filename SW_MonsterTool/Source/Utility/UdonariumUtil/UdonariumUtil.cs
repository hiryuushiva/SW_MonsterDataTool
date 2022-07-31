using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using SW_MonsterTool.Source.MData;
using System.IO.Compression;
using System.Collections.Generic;
using SW_MonsterTool.Source.Utility.MathUtility;

//ユドナリウム出力クラス
namespace SW_MonsterTool.Source.Utility.Udonarium
{
    public enum ValueType
    {
        None,
        Note,
        NumberResource,
    }

    public class UdonariumUtil
    {
        public static int Export(Monster monster, string dicepot, string export_part)
        {
            //チャットパレット用
            List<string> l_DicePallet = new List<string>();
            l_DicePallet.Add("ダイスを振るリスト");
            List<string> l_FixedPallet = new List<string>();
            l_FixedPallet.Add("固定値のリスト");

            //パス作成
            string l_File_Path = "";
            if (export_part == "全ての部位")
                l_File_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "UdonariumMonsterData" + "\\" + dicepot + "\\" + monster.m_Name;
            else
                l_File_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "UdonariumMonsterData" + "\\" + dicepot + "\\" + monster.m_Name+"_"+export_part;

            //ファイル名に使用できない文字を取得
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();

            if (!(export_part.IndexOfAny(invalidChars) < 0))
                return -1;
            if (!(monster.m_Name.IndexOfAny(invalidChars) < 0))
                return -1;

            //フォルダ作成
            Directory.CreateDirectory(l_File_Path);
            //xmlパス作成
            string l_Path = Path.Combine(l_File_Path, "data.xml");

            // XmlWriterの設定
            var l_Settings = new XmlWriterSettings();
            //インデント
            l_Settings.Indent = true;
            //改行
            l_Settings.IndentChars = "\t";

            // 出力する場所を指定
            XmlWriter l_Character = XmlWriter.Create(l_Path, l_Settings);

            // 要素characterを指定
            l_Character.WriteStartElement("character");
            l_Character.WriteAttributeString("location.name", "table");
            l_Character.WriteAttributeString("location.x", "400");
            l_Character.WriteAttributeString("location.y", "400");
            l_Character.WriteAttributeString("posZ", "0");
            l_Character.WriteAttributeString("rotate", "0");
            l_Character.WriteAttributeString("roll", "0");

            //キャラ画像(一応設定してるが同梱するだけになる)
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "character");

            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "image");

            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("type", "image");
            l_Character.WriteAttributeString("name", "imageIdentifier");
            //要素書き込み
            if (monster.m_Texture != "")
            {
                string[] l_Sp = monster.m_Texture.Split('\\');
                string[] l_Texture = l_Sp.Last().Split('.');

                l_Character.WriteString(l_Texture[0]);


                File.Copy(monster.m_Texture, l_File_Path + "\\" + l_Sp.Last());
            }
            else
                l_Character.WriteString("testCharacter_1_image");

            // 要素characterの閉じタグを指定
            l_Character.WriteEndElement();
            l_Character.WriteEndElement();

            //モンスター名
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "common");
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "name");
            if (export_part == "全ての部位")
                l_Character.WriteString(monster.m_Name);
            else
                l_Character.WriteString(monster.m_Name+"("+ export_part+")");
            l_Character.WriteEndElement();
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "size");
            l_Character.WriteString("1");
            l_Character.WriteEndElement();
            l_Character.WriteEndElement();

            //詳細値
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "detail");

            //魔物詳細値1
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "魔物詳細値1");
            //
            AddNode(ref l_Character, "種族", monster.m_Category, ValueType.None);
            //
            AddNode(ref l_Character, "レベル", monster.m_Level, ValueType.None);
            //
            AddNode(ref l_Character, "出典", monster.m_Reference, ValueType.None);
            //
            l_Character.WriteEndElement();

            //魔物詳細値2
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "魔物詳細値2");
            //
            AddNode(ref l_Character, "知能", monster.m_Intelligence, ValueType.None);
            //
            AddNode(ref l_Character, "知覚", monster.m_Perception, ValueType.None);
            //
            AddNode(ref l_Character, "反応", monster.m_Reaction, ValueType.None);
            //
            AddNode(ref l_Character, "穢れ", monster.m_Kegare, ValueType.None);
            //
            string l_Lang = "";
            foreach (var l in monster.m_Language.Select((v, i) => new { v, i }))
            {
                if (l.i < monster.m_Language.Count())
                    l_Lang += l.v + "\n";
                else
                    l_Lang += l.v;
            }
            AddNode(ref l_Character, "言語", l_Lang, ValueType.Note);
            //
            string l_Habitat = "";
            foreach (var s in monster.m_Habitat.Select((v, i) => new { v, i }))
            {
                if (s.i < monster.m_Habitat.Count())
                    l_Habitat += s.v + "\n";
                else
                    l_Habitat += s.v;
            }
            AddNode(ref l_Character, "生息地", l_Habitat, ValueType.Note);
            //
            AddNode(ref l_Character, "知名度", monster.m_Recognition, ValueType.None);
            //
            AddNode(ref l_Character, "弱点値", monster.m_Weak_Point_Value, ValueType.None);
            //
            AddNode(ref l_Character, "弱点", monster.m_Weak_Point, ValueType.None);
            //
            AddNode(ref l_Character, "先制値", monster.m_Preemptive_Value, ValueType.None);
            //
            string l_Move = monster.m_Move_Speed[0].Speed_View + monster.m_Move_Speed[0].Move_View + "/" + monster.m_Move_Speed[1].Speed_View + monster.m_Move_Speed[1].Move_View;
            AddNode(ref l_Character, "移動速度", l_Move, ValueType.None);
            //
            AddNode(ref l_Character, "生命抵抗力", monster.m_Life_Resistance + " / ( " + MathUtil.AddStringNum(monster.m_Life_Resistance, "7") + ")", ValueType.None);
            //
            AddNode(ref l_Character, "精神抵抗力", monster.m_Spirit_Resistance + " / ( " + MathUtil.AddStringNum(monster.m_Spirit_Resistance, "7") + ")", ValueType.None);

            //パレット追加
            l_DicePallet.Add("2d6+" + monster.m_Life_Resistance + " " + monster.m_Name + " の" + "生命抵抗力判定");
            l_DicePallet.Add("2d6+" + monster.m_Spirit_Resistance + " " + monster.m_Name + " の" + "精神抵抗力判定");
            l_FixedPallet.Add(MathUtil.AddStringNum(monster.m_Life_Resistance, "7") + " " + monster.m_Name + " の" + "生命抵抗力判定(固定値)");
            l_FixedPallet.Add(MathUtil.AddStringNum(monster.m_Spirit_Resistance, "7") + " " + monster.m_Name + " の" + "精神抵抗力判定(固定値)");
            //

            l_Character.WriteEndElement();


            //基本ステータス
            int l_StartIndex;
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "基本ステータス");
            AddNode(ref l_Character, "部位", "部位数:" + monster.m_Statuses.Count().ToString() + " コア部位:" + monster.m_CorePart, ValueType.None);
            foreach (Monster.Status s in monster.m_Statuses)
            {
                if (export_part == "全ての部位")
                {
                    l_Character.WriteStartElement("data");
                    l_Character.WriteAttributeString("name", s.Part);
                    AddNode(ref l_Character, "剣の欠片", s.Sword_Piece, ValueType.None);
                    AddNode(ref l_Character, "命中力", s.HitPower, ValueType.None);
                    AddNode(ref l_Character, "打撃点", s.Damage, ValueType.None);
                    AddNode(ref l_Character, "回避力", s.Dodge, ValueType.None);
                    AddNode(ref l_Character, "防護点", s.Protection, ValueType.None);
                    AddNode(ref l_Character, "部位HP", s.HP, ValueType.NumberResource);
                    AddNode(ref l_Character, "部位MP", s.MP, ValueType.NumberResource);
                    l_Character.WriteEndElement();

                    //パレット追加
                    l_StartIndex = s.HitPower.IndexOf('(');
                    string l_HitPower = s.HitPower.Remove(l_StartIndex, s.HitPower.Count() - l_StartIndex);
                    l_DicePallet.Add("2d6+" + l_HitPower + " " + monster.m_Name + " の " + s.Part + " 命中判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_HitPower, "7") + " " + monster.m_Name + " の " + s.Part + " 命中判定(固定値)");

                    string l_Damage = s.Damage.Replace("2d+", "");
                    l_DicePallet.Add("2d6+" + l_Damage + " " + monster.m_Name + " の " + s.Part + " ダメージ判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_Damage, "7") + " " + monster.m_Name + " の " + s.Part + " ダメージ判定(固定値)");

                    l_StartIndex = s.Dodge.IndexOf('(');
                    string l_Dodge = s.Dodge.Remove(l_StartIndex, s.Dodge.Count() - l_StartIndex);
                    l_DicePallet.Add("2d6+" + l_Dodge + " " + monster.m_Name + " の " + s.Part + " 回避判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_Dodge, "7") + " " + monster.m_Name + " の " + s.Part + " 回避判定(固定値)");

                }
                else if (export_part == s.Part)
                {
                    l_Character.WriteStartElement("data");
                    l_Character.WriteAttributeString("name", s.Part);
                    AddNode(ref l_Character, "剣の欠片", s.Sword_Piece, ValueType.None);
                    AddNode(ref l_Character, "命中力", s.HitPower, ValueType.None);
                    AddNode(ref l_Character, "打撃点", s.Damage, ValueType.None);
                    AddNode(ref l_Character, "回避力", s.Dodge, ValueType.None);
                    AddNode(ref l_Character, "防護点", s.Protection, ValueType.None);
                    AddNode(ref l_Character, "HP", s.HP, ValueType.NumberResource);
                    AddNode(ref l_Character, "MP", s.MP, ValueType.NumberResource);
                    l_Character.WriteEndElement();

                    //パレット追加
                    l_StartIndex = s.HitPower.IndexOf('(');
                    string l_HitPower = s.HitPower.Remove(l_StartIndex, s.HitPower.Count() - l_StartIndex);
                    l_DicePallet.Add("2d6+" + l_HitPower + " " + monster.m_Name + " の " + s.Part + " 命中判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_HitPower, "7") + " " + monster.m_Name + " の " + s.Part + " 命中判定(固定値)");

                    string l_Damage = s.Damage.Replace("2d+", "");
                    l_DicePallet.Add("2d6+" + l_Damage + " " + monster.m_Name + " の " + s.Part + " ダメージ判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_Damage, "7") + " " + monster.m_Name + " の " + s.Part + " ダメージ判定(固定値)");

                    l_StartIndex = s.Dodge.IndexOf('(');
                    string l_Dodge = s.Dodge.Remove(l_StartIndex, s.Dodge.Count() - l_StartIndex);
                    l_DicePallet.Add("2d6+" + l_Dodge + " " + monster.m_Name + " の " + s.Part + " 回避判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_Dodge, "7") + " " + monster.m_Name + " の " + s.Part + " 回避判定(固定値)");

                }
            }

            l_Character.WriteEndElement();

            //特殊能力
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "特殊能力");
            foreach (Monster.Specal s in monster.m_Specals)
            {
                l_Character.WriteStartElement("data");
                l_Character.WriteAttributeString("name", s.Name);
                AddNode(ref l_Character, "宣言1", s.Declaration1, ValueType.None);
                if (s.Declaration2 != "")
                    AddNode(ref l_Character, "宣言2", s.Declaration2, ValueType.None);
                if (s.Declaration3 != "")
                    AddNode(ref l_Character, "宣言3", s.Declaration3, ValueType.None);
                AddNode(ref l_Character, "解説", s.Effect, ValueType.Note);
                l_Character.WriteEndElement();


                //パレット追加

            }
            l_Character.WriteEndElement();

            //戦利品
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "戦利品");
            foreach (Monster.Booty s in monster.m_Booty)
            {
                l_Character.WriteStartElement("data");
                l_Character.WriteAttributeString("name", s.Dice);
                AddNode(ref l_Character, "名称,個数", s.Name + " " + s.Num + "個", ValueType.None);
                AddNode(ref l_Character, "値段", s.Gamel + "G", ValueType.None);
                AddNode(ref l_Character, "マテリアルカード", s.Material, ValueType.None);
                l_Character.WriteEndElement();
            }
            l_Character.WriteEndElement();

            //解説
            l_Character.WriteStartElement("data");
            l_Character.WriteAttributeString("name", "解説");
            AddNode(ref l_Character, "解説", monster.m_Description, ValueType.Note);
            l_Character.WriteEndElement();

            l_Character.WriteEndElement();
            l_Character.WriteEndElement();

            string l_Dice = "";
            if (dicepot == "ソードワールド2.0")
                l_Dice = "SwordWorld2_0";
            else
                l_Dice = "SwordWorld2_5";

            //チャットパレット
            l_Character.WriteStartElement("chat-palette");
            l_Character.WriteAttributeString("dicebot", l_Dice);

            List<string> l_TotalPallet = new List<string>();
            l_TotalPallet.AddRange(l_DicePallet);
            l_TotalPallet.AddRange(monster.m_DicePallet);
            l_TotalPallet.AddRange(l_FixedPallet);
            l_TotalPallet.AddRange(monster.m_FixedPallet);

            l_Character.WriteString(CreateChatPallet(monster, l_TotalPallet));
            l_Character.WriteEndElement();

            // xmlに書き込む
            l_Character.Close();

            //Zipファイルに吐き出す作業

            //既にファイルが存在するか確かめるための作業
            ZipArchive l_Zip;
            try
            {
                l_Zip = ZipFile.Open(l_File_Path + ".zip", ZipArchiveMode.Read);
            }
            catch
            {
                l_Zip = null;
            }

            //ヌルだったらそのまま
            //そうじゃなかったらファイル消して作成
            if (l_Zip == null)
                ZipFile.CreateFromDirectory(
                    l_File_Path,
                    l_File_Path + ".zip");
            else
            {
                //ファイル開けたままなので閉じてから
                l_Zip.Dispose();
                //同じ名前のやつは削除してから新たに追加
                File.Delete(l_File_Path + ".zip");
                ZipFile.CreateFromDirectory(l_File_Path, l_File_Path + ".zip");
            }

            //zipにする前のフォルダも消しておく
            Delete(l_File_Path);

            //Console.WriteLine("xmlに書き込みが完了しました。");

            return 1;
        }

        //ユドナリウム上での＋追加に値する処理
        private static void AddNode(ref XmlWriter xml, string name, string value, ValueType type)
        {
            xml.WriteStartElement("data");

            //ノードの名前
            xml.WriteAttributeString("name", name);
            //ユドナリウムの見た目タイプ設定
            if (type == ValueType.Note)
                xml.WriteAttributeString("type", "note");
            else if (type == ValueType.NumberResource)
                xml.WriteAttributeString("type", "numberResource");
            //値
            xml.WriteString(value);

            xml.WriteEndElement();
        }

        //チャットパレット作成処理
        private static string CreateChatPallet(Monster m, List<string> pallet)
        {
            string l_Pallet = "";

            foreach (string chat in pallet)
            {
                l_Pallet += chat + "\n";
            }

            return l_Pallet;
        }

        //
        public static void Delete(string targetDirectoryPath)
        {
            if (!Directory.Exists(targetDirectoryPath))
            {
                return;
            }

            //ディレクトリ以外の全ファイルを削除
            string[] filePaths = Directory.GetFiles(targetDirectoryPath);
            foreach (string filePath in filePaths)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }

            //ディレクトリの中のディレクトリも再帰的に削除
            string[] directoryPaths = Directory.GetDirectories(targetDirectoryPath);
            foreach (string directoryPath in directoryPaths)
            {
                Delete(directoryPath);
            }

            //中が空になったらディレクトリ自身も削除
            Directory.Delete(targetDirectoryPath, false);
        }
    }
}
