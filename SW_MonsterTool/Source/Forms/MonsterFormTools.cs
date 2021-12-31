using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SW_MonsterTool.Source.MyListView;

namespace SW_MonsterTool
{
    //フォームからの出力に必要なツールアイテムをまとめておく構造体
    public struct MonsterFormTools
    {
        public TextBox MonsterNameBox;

        public ComboBox CategoryBox;

        public NumericUpDown LevelNumUpDown;

        public ComboBox ReferenceBox;

        public ComboBox IntelligenceBox;

        public ComboBox PerceptionBox;

        public ComboBox ReactionBox;

        public NumericUpDown KegareNumUpDown;

        public NumericUpDown PageNumUpDown;

        public NumericUpDown Name_RecognitionNumUpDown;

        public NumericUpDown Weak_Point_ValueNumUpDown;

        public ComboBox Weak_PointBox;

        public NumericUpDown Weak_PointNumUpDown;

        public NumericUpDown Preemptive_ValueNumUpDown;

        public NumericUpDown Life_ResistanceNumUpDown;

        public NumericUpDown Spirit_ResistanceNumUpDown;

        public ComboBox Move_SpeedBox1;
        public NumericUpDown Move_SpeedNumUpDown1;
        public ComboBox Move_SpeedBox2;
        public NumericUpDown Move_SpeedNumUpDown2;

        public FlowLayoutPanel LanguagePanel;

        public FlowLayoutPanel HabitatPanel;

        public ComboBox CorePartBox;

        public ListViewEx StatusListView;

        public FlowLayoutPanel SPAbilityPanel;

        public FlowLayoutPanel BootyPanel;

        public RichTextBox DescriptionTextBox;

        public TextBox TextureURL;

        public MonsterFormTools(TextBox name, ComboBox category, NumericUpDown level, ComboBox refe, ComboBox inteli,
            ComboBox per, ComboBox reac, NumericUpDown kegare, NumericUpDown page, NumericUpDown reco, NumericUpDown weakvalue,
            ComboBox wpb, NumericUpDown wpn, NumericUpDown preem, NumericUpDown life, NumericUpDown spirit,
            ComboBox move1, ComboBox move2, NumericUpDown ms1, NumericUpDown ms2, FlowLayoutPanel lang, FlowLayoutPanel habi,
            ComboBox core, ListViewEx status, FlowLayoutPanel abi, FlowLayoutPanel booty,RichTextBox descript, TextBox texture)
        {
            MonsterNameBox = name;
            CategoryBox = category;
            LevelNumUpDown = level;
            ReferenceBox = refe;
            IntelligenceBox = inteli;
            PerceptionBox = per;
            ReactionBox = reac;
            KegareNumUpDown = kegare;
            PageNumUpDown = page;
            Name_RecognitionNumUpDown = reco;
            Weak_Point_ValueNumUpDown = weakvalue;
            Weak_PointBox = wpb;
            Weak_PointNumUpDown = wpn;
            Preemptive_ValueNumUpDown = preem;
            Life_ResistanceNumUpDown = life;
            Spirit_ResistanceNumUpDown = spirit;
            Move_SpeedBox1 = move1;
            Move_SpeedBox2 = move2;
            Move_SpeedNumUpDown1 = ms1;
            Move_SpeedNumUpDown2 = ms2;
            LanguagePanel = lang;
            HabitatPanel = habi;
            CorePartBox = core;
            StatusListView = status;
            SPAbilityPanel = abi;
            BootyPanel = booty;
            DescriptionTextBox = descript;
            TextureURL = texture;
        }
    }
}
