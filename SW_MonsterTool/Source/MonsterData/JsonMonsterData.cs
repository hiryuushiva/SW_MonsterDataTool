using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_MonsterTool.Source.MData
{
    public class JsonReference
    {
        public string Book { get; set; }
        public string Page { get; set; }
    }

    public class JsonWeekPoint
    {
        public string Element { get; set; }
        public string Damage { get; set; }
    }

    public class JsonMoveSpeed
    {
        public string Move { get; set; }
        
        public string Speed { get; set; }
    }

    public class JsonStatus
    {
        public string Part { get; set; }
        public string HitPower { get; set; }
        public string Damage { get; set; }
        public string Dodge { get; set; }
        public string Protection { get; set; }
        public string HP { get; set; }
        public string MP { get; set; }
        public string SwordPiece { get; set; }
    }

    public class JsonSpecal
    {
        public string Name { get; set; }
        public string Declaration1 { get; set; }
        public string Declaration2 { get; set; }
        public string Declaration3 { get; set; }
        public string Effect { get; set; }

    }

    public class JsonBooty
    {
        public string Dice { get; set; }
        public string Name { get; set; }
        public string Num { get; set; }
        public string Gamel { get; set; }
        public string Material { get; set; }
    }

    public class JsonChatPalette
    {
        public List<string> DicePalette { get; set; }
        public List<string> FixedPalette { get; set; }
    }

    public class JsonMonsterData
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public JsonReference Reference { get; set; }

        public string Inteligence { get; set; }
        
        public string Reaction { get; set; }
        
        public string Kegare { get; set; }
        
        public string Recognition { get; set; }

        public string Perception { get; set; }
        
        public string WeakPointValue { get; set; }
        
        public string PreemptiveValue { get; set; }

        public JsonWeekPoint WeekPoint { get; set; }

        public string LifeResistance { get; set; }

        public string SpiritResistance { get; set; }

        public List<JsonMoveSpeed> MoveSpeed { get; set; }

        public List<string> Language { get; set; }

        public List<string> Habitat { get; set; }

        public string CorePart { get; set; }

        public List<JsonStatus> Statuses { get; set; }

        public List<JsonSpecal> Specal { get; set; }

        public List<JsonBooty> Booty { get; set; }

        public string Description { get; set; }

        public string Level { get; set; }

        public string TotalSwordPiece { get; set; }

        public string Texture { get; set; }

        public JsonChatPalette ChatPalette { get; set; }
    }
}
