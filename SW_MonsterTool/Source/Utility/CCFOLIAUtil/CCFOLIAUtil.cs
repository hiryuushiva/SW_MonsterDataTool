using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SW_MonsterTool.Source.MData;
using System.IO.Compression;
using System.Collections.Generic;
using SW_MonsterTool.Source.Utility.MathUtility;
using SW_MonsterTool.Source.Utility.FileUtility;
using System.Text.Json;
using System.Windows.Forms;

namespace SW_MonsterTool.Source.Utility.CCFOLIAUtil
{
    public class CCFOLIAUtil
    {

        //ココフォリアまだキャラzip対応してないのでクリップボードにコピーでとどめておく
        public static int Export(Monster monster, string dicepot, string export_part, bool hide, bool invi, bool secre)
        {
            //FileUtil l_File = new FileUtil();

            ////パス作成
            //string l_File_Path = "";
            //l_File_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "CCFOLIAMonsterData" + "\\" + dicepot + "\\" + monster.m_Name + "_" + export_part;

            ////フォルダ作成
            //Directory.CreateDirectory(l_File_Path);
            ////jsonパス作成
            //string l_Path = Path.Combine(l_File_Path, "data.json");

            ////ファイル名に使用できない文字を取得
            //char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();

            //if (!(export_part.IndexOfAny(invalidChars) < 0))
            //    return -1;
            //if (!(monster.m_Name.IndexOfAny(invalidChars) < 0))
            //    return -1;

            //キャラデータ作成
            CCFOLIACharacterJson l_Chara = new CCFOLIACharacterJson();

            l_Chara.kind = "character";

            l_Chara.data = CreateData(monster, export_part, hide, invi, secre);

            string l_Result = JsonSerializer.Serialize(l_Chara);

            //クリップボード出力
            Clipboard.SetText(l_Result);

            ////ファイル出力
            //l_File.FileWrite(l_Path,l_Result,false,"utf-8");

            ////既にファイルが存在するか確かめるための作業
            //ZipArchive l_Zip;
            //try
            //{
            //    l_Zip = ZipFile.Open(l_File_Path + ".zip", ZipArchiveMode.Read);
            //}
            //catch
            //{
            //    l_Zip = null;
            //}

            ////ヌルだったらそのまま
            ////そうじゃなかったらファイル消して作成
            //if (l_Zip == null)
            //    ZipFile.CreateFromDirectory(
            //        l_File_Path,
            //        l_File_Path + ".zip");
            //else
            //{
            //    //ファイル開けたままなので閉じてから
            //    l_Zip.Dispose();
            //    //同じ名前のやつは削除してから新たに追加
            //    File.Delete(l_File_Path + ".zip");
            //    ZipFile.CreateFromDirectory(l_File_Path, l_File_Path + ".zip");
            //}

            ////zipにする前のフォルダも消しておく
            //Delete(l_File_Path);

            return 1;
        }

        private static CCFOLIACharacterData CreateData(Monster monster, string export_part, bool hide, bool invi, bool secre)
        {
            CCFOLIACharacterData l_Data = new CCFOLIACharacterData();

            //チャットパレット用
            List<string> l_DicePallet = new List<string>();
            l_DicePallet.Add("ダイスを振るリスト");
            List<string> l_FixedPallet = new List<string>();
            l_FixedPallet.Add("固定値のリスト");

            l_Data.name = monster.m_Name;

            l_Data.initiative = int.Parse(monster.m_Preemptive_Value);

            l_Data.memo += "知能 : " + monster.m_Intelligence + " / 知覚 : " + monster.m_Perception + "\n";
            l_Data.memo += "反応 : " + monster.m_Reaction + " / 穢れ : " + monster.m_Kegare + "\n";

            l_Data.memo += "使用可能言語\n";
            foreach (var lang in monster.m_Language.Select((text, i) => new { text, i }))
            {
                if (((lang.i + 1) % 3) == 0)
                    l_Data.memo += lang.text + "\n";
                else if ((lang.i + 1) == monster.m_Language.Count)
                {
                    l_Data.memo += lang.text + "\n";
                }
                else
                    l_Data.memo += lang.text + " / ";
            }

            l_Data.memo += "主な生息地域\n";
            foreach (var habi in monster.m_Habitat.Select((text, i) => new { text, i }))
            {
                if (((habi.i + 1) % 3) == 0)
                    l_Data.memo += habi.text + "\n";
                else if ((habi.i + 1) == monster.m_Habitat.Count)
                {
                    l_Data.memo += habi.text + "\n";
                }
                else
                    l_Data.memo += habi.text + " / ";
            }

            l_Data.memo += "出典 : " + monster.m_Reference + " / P." + monster.m_Page + "\n";

            l_Data.memo += "戦利品\n";

            foreach (Monster.Booty booty in monster.m_Booty)
            {
                l_Data.memo += booty.Dice + " " + booty.Name + "×" + booty.Num + "個 " + booty.Gamel + "G " + booty.Material + "\n";
            }

            l_Data.externalUrl = "SW_MonsterDataToolで作成";

            l_DicePallet.Add("2d6+" + monster.m_Life_Resistance + " 生命抵抗力判定");
            l_DicePallet.Add("2d6+" + monster.m_Spirit_Resistance + " 精神抵抗力判定");
            l_FixedPallet.Add(MathUtil.AddStringNum(monster.m_Life_Resistance, "7") + " 生命抵抗力判定(固定値)");
            l_FixedPallet.Add(MathUtil.AddStringNum(monster.m_Spirit_Resistance, "7") + " 精神抵抗力判定(固定値)");

            int l_StartIndex;
            l_Data.status = new List<CCFOLIACharacter_Status>();
            l_Data.parameter = new List<CCFOLIACharacter_Params>();
            foreach (Monster.Status sta in monster.m_Statuses)
            {

                if (export_part == sta.Part)
                {
                    try
                    {
                        l_Data.status.Add(CreateStatus("HP", int.Parse(sta.HP)));
                    }
                    catch
                    {
                        l_Data.status.Add(CreateStatus("HP", 0));
                    }

                    try
                    {
                        l_Data.status.Add(CreateStatus("MP", int.Parse(sta.MP)));
                    }
                    catch
                    {
                        l_Data.status.Add(CreateStatus("MP", 0));
                    }
                    try
                    {
                        l_Data.status.Add(CreateStatus("防護点", int.Parse(sta.Protection), 100));
                    }
                    catch
                    {
                    }

                    l_Data.status.Add(CreateStatus("ダメbd", 0, 100));
                    l_Data.status.Add(CreateStatus("回避bd", 0, 100));
                    l_Data.status.Add(CreateStatus("命中bd", 0, 100));

                    //パレット追加
                    l_StartIndex = sta.HitPower.IndexOf('(');
                    string l_HitPower = sta.HitPower.Remove(l_StartIndex, sta.HitPower.Count() - l_StartIndex);
                    l_DicePallet.Add("2d6+" + l_HitPower + "+{命中bd}" + " " + sta.Part + " の" + " 命中判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_HitPower, "7") + "+{命中bd}" + " " + sta.Part + " の" + " 命中判定(固定値)");

                    string l_Damage = sta.Damage.Replace("2d+", "");
                    l_DicePallet.Add("2d6+" + l_Damage + "+{ダメbd}" + " " + sta.Part + " の" + " ダメージ判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_Damage, "7") + "+{ダメbd}" + " " + sta.Part + " の" + " ダメージ判定(固定値)");

                    l_StartIndex = sta.Dodge.IndexOf('(');
                    string l_Dodge = sta.Dodge.Remove(l_StartIndex, sta.Dodge.Count() - l_StartIndex);
                    l_DicePallet.Add("2d6+" + l_Dodge + "+{回避bd}" + " " + sta.Part + " の" + " 回避判定");
                    l_FixedPallet.Add(MathUtil.AddStringNum(l_Dodge, "7") + "+{回避bd}" + " " + sta.Part + " の" + " 回避判定(固定値)");

                    l_Data.parameter.Add(CreateParam("命中率", sta.HitPower));
                    l_Data.parameter.Add(CreateParam("打撃点", sta.Damage));
                    l_Data.parameter.Add(CreateParam("回避力", sta.Dodge));
                    l_Data.parameter.Add(CreateParam("防護点", sta.Protection));
                    l_Data.parameter.Add(CreateParam("剣の欠片", sta.Sword_Piece));

                    break;
                }
            }
            l_Data.parameter.Add(CreateParam("知名度", monster.m_Recognition));
            l_Data.parameter.Add(CreateParam("弱点値", monster.m_Weak_Point_Value));
            l_Data.parameter.Add(CreateParam("弱点", monster.m_Weak_Point));
            l_Data.parameter.Add(CreateParam("移動速度1", monster.m_Move_Speed[0].Speed_View + monster.m_Move_Speed[0].Move_View));
            l_Data.parameter.Add(CreateParam("移動速度2", monster.m_Move_Speed[1].Speed_View + monster.m_Move_Speed[1].Move_View));
            l_Data.parameter.Add(CreateParam("生命抵抗力", monster.m_Life_Resistance + "(" + MathUtil.AddStringNum(monster.m_Spirit_Resistance, "7") + ")"));
            l_Data.parameter.Add(CreateParam("精神抵抗力", monster.m_Spirit_Resistance + "(" + MathUtil.AddStringNum(monster.m_Spirit_Resistance, "7") + ")"));



            List<string> l_TotalPallet = new List<string>();
            l_TotalPallet.AddRange(l_DicePallet);
            l_TotalPallet.AddRange(monster.m_DicePallet);
            l_TotalPallet.AddRange(l_FixedPallet);
            l_TotalPallet.AddRange(monster.m_FixedPallet);

            foreach (string pallet in l_TotalPallet)
            {
                l_Data.commands += pallet + "\n";
            }

            l_Data.hideStatus = hide;

            l_Data.invisible = invi;

            l_Data.secret = secre;

            return l_Data;
        }

        private static CCFOLIACharacter_Faces CreateFaces(string url, string name)
        {
            CCFOLIACharacter_Faces l_Face = new CCFOLIACharacter_Faces();
            l_Face.iconUrl = url;
            l_Face.label = name;

            return l_Face;
        }

        private static CCFOLIACharacter_Params CreateParam(string name, string value)
        {
            CCFOLIACharacter_Params l_Param = new CCFOLIACharacter_Params();

            l_Param.label = name;
            l_Param.value = value;

            return l_Param;

        }

        private static CCFOLIACharacter_Status CreateStatus(string name, int value, int max)
        {
            CCFOLIACharacter_Status l_Status = new CCFOLIACharacter_Status();

            l_Status.label = name;
            l_Status.value = value;
            l_Status.max = max;

            return l_Status;
        }

        private static CCFOLIACharacter_Status CreateStatus(string name, int value)
        {
            CCFOLIACharacter_Status l_Status = new CCFOLIACharacter_Status();

            l_Status.label = name;
            l_Status.value = value;
            l_Status.max = value;

            return l_Status;
        }

        private static void Delete(string targetDirectoryPath)
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
