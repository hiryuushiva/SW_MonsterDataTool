using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SW_MonsterTool.Source.MyListView
{
    public partial class ListViewEx : ListView
    {
        public ListViewEx() : base()
        {
            this.View = View.Details;
            this.FullRowSelect = true;
            this.columns = new ColumnHeaderExCollection(this);

            this.EditBox = new TextBox();
            this.EditBox.Parent = this;
            this.EditBox.Visible = false;
            this.EditBox.BorderStyle = BorderStyle.FixedSingle;
            this.EditBox.Leave += EditBox_Leave;
            this.EditBox.KeyPress += EditBox_KeyPress;

            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.EditBox.KeyPress -= EditBox_KeyPress;
                this.EditBox.Leave -= EditBox_Leave;
                this.EditBox.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EditBox_Leave(object sender, EventArgs e)
        {
            CurrentColumn.Text = EditBox.Text;
            EditBox.Visible = false;
        }

        private void EditBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                //決定
                case (char)Keys.Enter:
                    this.Focus();
                    e.Handled = true;
                    break;
                //キャンセル
                case (char)Keys.Escape:
                    EditBox.Text = CurrentColumn.Text;
                    this.Focus();
                    e.Handled = true;
                    break;
            }
        }



        ColumnHeaderExCollection columns;
        TextBox EditBox;



        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new View View
        {
            get { return base.View; }
            set { base.View = View.Details; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool FullRowSelect
        {
            get { return base.FullRowSelect; }
            set { base.FullRowSelect = true; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal ColumnHeaderCollection ColumnsOrg { get { return base.Columns; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ColumnHeaderExCollection Columns { get { return this.columns; } }

        [Browsable(false)]
        public ListViewItem CurrentRow { get; set; }

        [Browsable(false)]
        public ListViewItem.ListViewSubItem CurrentColumn { get; set; }

        [Browsable(false)]
        public int CurrentRowIndex { get { return (CurrentRow == null) ? -1 : CurrentRow.Index; } }

        [Browsable(false)]
        public int CurrentColumnIndex { get { return (CurrentColumn == null) ? -1 : CurrentRow.SubItems.IndexOf(CurrentColumn); } }

        protected override void OnItemCheck(ItemCheckEventArgs ice)
        {
            if (CurrentColumn != null && CurrentColumnIndex != 0)
                ice.NewValue = ice.CurrentValue;

            base.OnItemCheck(ice);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point loc = this.PointToClient(Cursor.Position);

            CurrentRow = null;
            CurrentColumn = null;

            CurrentRow = this.HitTest(loc).Item;
            if (CurrentRow == null || !CurrentRow.Bounds.Contains(loc))
                CurrentRow = null;
            else
            {
                CurrentColumn = CurrentRow.GetSubItemAt(loc.X, loc.Y);
                if (CurrentColumn == null || !CurrentColumn.Bounds.Contains(loc))
                    CurrentColumn = null;
            }

            base.OnMouseDown(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.Items.Count > 0)
                if (this.Items[0] == null)
                    return;

            this.Focus();
            base.OnResize(e);
        }

        protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
        {
            this.Focus();
            base.OnColumnWidthChanging(e);
        }

        protected override void OnNotifyMessage(Message m)
        {
            switch (m.Msg)
            {
                case WM_HSCROLL:
                case WM_VSCROLL:
                    this.Focus();
                    break;
            }
            base.OnNotifyMessage(m);
        }

        const int WM_HSCROLL = 0x114;
        const int WM_VSCROLL = 0x115;

        public void EditColumn()
        {
            if (CurrentColumn == null || CurrentColumnIndex == 0) return;
            if (!this.columns[CurrentColumnIndex].Editable) return;

            Rectangle rect = CurrentColumn.Bounds;
            rect.Intersect(this.ClientRectangle);
            rect.Y -= 1;

            EditBox.Bounds = rect;
            EditBox.Text = CurrentColumn.Text;
            EditBox.Visible = true;
            EditBox.BringToFront();
            EditBox.Focus();
        }

        public void LeaveEditBoxEvent(EventHandler e)
        {
            this.EditBox.Leave += e;
        }
    }
}