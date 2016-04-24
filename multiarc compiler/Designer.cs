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
        private readonly ICollection<SystemComponent> _componentsList;

        private SystemComponent _selectedComponent;

        private readonly ICollection<ControlWithImage> _addedImages = new List<ControlWithImage>();

        private readonly ICollection<Pin> _addedPins = new List<Pin>();
        
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
            SaveButton.Enabled = false;
            _addedImages.Clear();
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
            DesignPanel.Refresh();
            _addedImages.Add(control);
            //SaveButton.Enabled = true;
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
                foreach (var c in _addedImages)
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

                foreach (var p in _addedPins)
                {
                    if (p.Location.X < leftBorder)
                    {
                        leftBorder = p.Location.X;
                    }
                    if (p.Location.X + p.Width > rightBorder)
                    {
                        rightBorder = p.Location.X + p.Width;
                    }
                    if (p.Location.Y < upperBorder)
                    {
                        upperBorder = p.Location.Y;
                    }
                    if (p.Location.Y + p.Height > lowerBorder)
                    {
                        lowerBorder = p.Location.Y + p.Height;
                    }
                }

                _selectedComponent.Size = new Size(rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1);

                foreach (var c in _addedImages)
                {
                    c.SetBounds(c.Location.X - leftBorder, c.Location.Y - upperBorder, c.Width, c.Height);
                    var clonedControl = (ControlWithImage)c.Clone();
                    clonedControl.AddToSystemComponent(_selectedComponent);
                    if (_selectedComponent.Region == null)
                    {
                        _selectedComponent.Region = c.Region.Clone();
                    }
                    else
                    {
                        _selectedComponent.Region.Union(c.Region);
                    }
                }

                foreach (var p in _addedPins)
                {
                    p.Location = new Point(p.Location.X - leftBorder, p.Location.Y - upperBorder);
                    var actualPin = _selectedComponent.GetPin(p.Name);
                    actualPin.Location = p.Location;
                    _selectedComponent.Region.Union(new Rectangle(p.Location.X, p.Location.Y, p.Size.Width, p.Size.Height));
                }
            }
        }

        private void AddPinButton_Click(object sender, EventArgs e)
        {
            var pin = (Pin)_selectedComponent.GetPin(PinsList.SelectedItem.ToString()).Clone();
            _addedPins.Add(pin);
            DesignPanel.Controls.Add(pin);
            pin.Designer = this;
            PinsList.Items.RemoveAt(PinsList.SelectedIndex);
            
            if (PinsList.Items.Count == 0)
            {
                SaveButton.Enabled = true;
            }
        }

        public void RemovePin(Pin pin)
        {
            DesignPanel.Controls.Remove(pin);
            PinsList.Items.Add(pin.Name);
            SaveButton.Enabled = false;
        }
    }
}
