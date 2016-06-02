using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public partial class SetBusWidthForm : Form
    {
        public int BusWidth { get; private set; }

        public SetBusWidthForm(Clipboard clipboard)
        {
            InitializeComponent();
            Location = new Point(clipboard.Location.X + clipboard.Width / 2 - Width / 2, clipboard.Location.Y + clipboard.Height / 2 - Height / 2);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            BusWidth = (int)BusWidthInput.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
