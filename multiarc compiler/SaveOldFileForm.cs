using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MultiArc_Compiler
{
    public partial class SaveOldFileForm : Form
    {
        private string fileName;

        private TextBoxBase textBox;

        public SaveOldFileForm(string fileName, TextBoxBase textBox)
        {
            InitializeComponent();
            label1.Text = "You will lose your work on " + fileName.Substring(fileName.LastIndexOf('\\') + 1) + @".
Do you want to save it before this action?";
            this.Visible = true;
            Form1.Instance.Enabled = false;
            this.textBox = textBox;
            this.fileName = fileName;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText(fileName, textBox.Text);
            Form1.Instance.Enabled = true;
            Form1.Instance.ClearCode();
            this.Dispose();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Form1.Instance.Enabled = true;
            this.Dispose();
        }
    }
}
