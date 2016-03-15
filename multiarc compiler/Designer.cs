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
            var graphics = DesignPanel.CreateGraphics();
            if (MessageBox.Show("Do you want to make this image transparent?", "Transparent", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int upperBorder = image.Size.Height;
                int leftBorder = image.Size.Width;
                int lowerBorder = 0;
                int rightBorder = 0;
                for (int x = 0; x < image.Size.Width; x++)
                {
                    for (int y = 0; y < image.Size.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y);
                        var color = pixel.ToArgb();
                        var white = Color.White.ToArgb();
                        if (color != white)
                        {
                            if (y < upperBorder)
                            {
                                upperBorder = y;
                            }
                            if (x < leftBorder)
                            {
                                leftBorder = x;
                            }
                            if (y > lowerBorder)
                            {
                                lowerBorder = y;
                            }
                            if (x > rightBorder)
                            {
                                rightBorder = x;
                            }
                        }
                    }
                }
                image.MakeTransparent();
                graphics.DrawImage(image, new Rectangle(0, 0, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), new Rectangle(leftBorder, upperBorder, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), GraphicsUnit.Pixel);
            }
            else
            {
                graphics.DrawImage(image, new Point(0, 0));
            }
        }

        private void BrowseComponentImageButton_Click(object sender, EventArgs e)
        {
            BrowseComponentImageDialog.ShowDialog();
        }
    }
}
