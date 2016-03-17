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

        private SystemComponent _selectedComponent;

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
            _selectedComponent = _componentsList.ElementAt(ComponentsComboBox.SelectedIndex);
            PinsList.Items.Clear();
            _selectedComponent.Ports.ForEach(port => port.GetAllPins().ForEach(pin => PinsList.Items.Add(pin.Name)));
            PinsList.Enabled = true;
            AddPinButton.Enabled = true;
        }

        private void BrowseComponentImageDialog_FileOk(object sender, CancelEventArgs e)
        {
            var image = new Bitmap(BrowseComponentImageDialog.FileName);
            var control = new ControlWithImage(image);
            if (MessageBox.Show("Do you want to make this image transparent?", "Transparent", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                control.MakeTransparent();
            }
            DesignPanel.Controls.Add(control);
        }

        private void BrowseComponentImageButton_Click(object sender, EventArgs e)
        {
            BrowseComponentImageDialog.ShowDialog();
        }
    }
}
