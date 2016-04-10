using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
            //DesignPanel.Controls.Add(_selectedComponent);
        }

        private void BrowseComponentImageDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {

                var image = new Bitmap(BrowseComponentImageDialog.FileName);
                var control = new ControlWithImage(image);
                if (
                    MessageBox.Show("Do you want to make this image transparent?", "Transparent",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    control.MakeTransparent();
                }
                DesignPanel.Controls.Add(control);
                DesignPanel.Refresh();
                //_selectedComponent.Controls.Add(control);
            }
            catch (Exception ex)
            {
                int x = 3;
            }
        }

        private void BrowseComponentImageButton_Click(object sender, EventArgs e)
        {
            BrowseComponentImageDialog.ShowDialog();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Sure", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int upperBorder = DesignPanel.Height;
                int lowerBorder = 0;
                int leftBorder = DesignPanel.Width;
                int rightBorder = 0;
                foreach (ControlWithImage c in DesignPanel.Controls)
                {
                    if (c.Location.X < leftBorder)
                    {
                        leftBorder = c.Location.X;
                    }
                    if (c.Location.X + c.Width > rightBorder)
                    {
                        rightBorder = c.Location.X + c.Width;
                    }
                    if (c.Location.Y < upperBorder)
                    {
                        upperBorder = c.Location.Y;
                    }
                    if (c.Location.Y + c.Height > lowerBorder)
                    {
                        lowerBorder = c.Location.Y + c.Height;
                    }
                }

                _selectedComponent.Size = new Size(rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1);

                foreach (ControlWithImage c in DesignPanel.Controls)
                {
                    c.SetBounds(c.Location.X - leftBorder, c.Location.Y - upperBorder, c.Width, c.Height);
                    _selectedComponent.Controls.Add(c);
                }

            }
        }
    }
}
