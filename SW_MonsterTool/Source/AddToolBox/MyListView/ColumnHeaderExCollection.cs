using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace SW_MonsterTool.Source.MyListView
{
    public class ColumnHeaderExCollection : CollectionBase
    {
        private ListViewEx owner;

        public ColumnHeaderExCollection(ListViewEx owner)
        {
            this.owner = owner;
        }

        public void Add(ColumnHeaderEx header)
        {
            owner.ColumnsOrg.Add((ColumnHeaderEx)header.Clone());
            this.InnerList.Add(header);
        }

        public void AddRange(ColumnHeaderEx[] headers)
        {
            for (int i = 0; i < headers.Length; i++)
                Add((ColumnHeaderEx)headers[i]);
        }

        public ColumnHeaderEx this[int i]
        {
            get
            {
                if (i < 0 || i >= this.InnerList.Count)
                    return null;
                return (ColumnHeaderEx)this.InnerList[i];
            }
            set
            {
                this.InnerList[i] = value;
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            this.owner.ColumnsOrg.Insert(index, (ColumnHeader)((ICloneable)value).Clone());
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            this.owner.ColumnsOrg.RemoveAt(index);
        }

        protected override void OnClearComplete()
        {
            base.OnClearComplete();
            this.owner.ColumnsOrg.Clear();
        }
    }
}