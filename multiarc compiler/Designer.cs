using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoreLinq;

namespace MultiArc_Compiler
{
    public partial class Designer : Form
    {
        private ICollection<SystemComponent> _componentsList;

        public Designer(ICollection<SystemComponent> components)
        {
            InitializeComponent();
            components.ForEach(c => ComponentsComboBox.Items.Add(c.Name));
            _componentsList = components;
            Visible = true;
        }

        private void ComponentsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrowseComponentImageButton.Enabled = true;
        }
    }
}
