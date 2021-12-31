using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW_MonsterTool.Source.MyNumeric
{
    public class MyNumericUpDown : NumericUpDown
    {
        private int _abs_increment, _increment;
        private int _abs_scroll_increment, _scroll_increment;
        private int _old_value;

        /// <summary>
        /// Gets or sets the value to increment or decrement the spin box (also known as an up-down control) when the up or down buttons are clicked.
        /// </summary>
        new public int Increment
        {
            set
            {
                this._increment = value;
                this._abs_increment = Math.Abs(value);
            }
            get => this._increment;
        }

        /// <summary>
        /// Gets or sets the value to increment or decrement the spin box (also known as an up-down control) when th mousee wheel spined.
        /// </summary>
        public int ScrollIncrement
        {
            set
            {
                this._scroll_increment = value;
                this._abs_scroll_increment = Math.Abs(value);
            }
            get => this._scroll_increment;
        }

        public int IsUpDown()
        {
            if (this._old_value < this.ValueAsInt)
                return 1;
            else
                return -1;
        }

        public int Abs()
        {
            int abs = this.ValueAsInt - this._old_value;
            return Math.Abs(abs);
        }

        public int ValueAsInt
        {
            set => this.Value = value;
            get => (int)this.Value;
        }

        public MyNumericUpDown() : base()
        {
            this.Increment = 1;
            this.ScrollIncrement = 1;
            this.ImeMode = ImeMode.Disable;
            this._old_value = this.ValueAsInt;
        } // ctor ()

        override public void UpButton() => UpDown(this._increment > 0);

        override public void DownButton() => UpDown(this._increment < 0);

        private void UpDown(bool up)
        {
            this._old_value = this.ValueAsInt;

            this.Value = up
                ? Math.Min(this.Value + this._abs_increment, this.Maximum) // increment
                : Math.Max(this.Value - this._abs_increment, this.Minimum) // decrement
                ;

        }

        protected override void OnTextBoxTextChanged(object source, EventArgs e)
        {

            TextBox textBox = ((TextBox)source);
            int l_Value = 0;
            try
            {
                l_Value = int.Parse(textBox.Text);
            }
            catch
            {
                return;
            }

            this._old_value = this.ValueAsInt;

            if (this._old_value < l_Value)
                this.Value = Math.Min(l_Value, this.Maximum);
            else
                this.Value = Math.Max(l_Value, this.Minimum);

            textBox.Text = this.Value.ToString();

        }

        override protected void OnMouseWheel(MouseEventArgs e)
        {
            this._old_value = this.ValueAsInt;

            if (e is HandledMouseEventArgs hme) hme.Handled = true;

            this.Value = e.Delta > 0 ^ this._scroll_increment > 0
                ? Math.Max(this.Value - this._abs_increment, this.Minimum) // decrement
                : Math.Min(this.Value + this._abs_increment, this.Maximum) // increment
                ;


        } // override protected void OnMouseWheel (MouseEventArgs)
    } // public class MyNumericUpDown : NumericUpDown
}
