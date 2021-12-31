using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace SW_MonsterTool.Source.MyListView
{
    public class ColumnHeaderEx : ColumnHeader
    {
        public ColumnHeaderEx() : base()
        {
            this.Editable = false;
        }

        [Category("Behavior")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        public bool Editable { get; set; }
    }
}
