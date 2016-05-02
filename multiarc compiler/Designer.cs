using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MoreLinq;
using System.IO;
using System.Xml;

namespace MultiArc_Compiler
{
    public partial class Designer : Form
    {
        private readonly ICollection<SystemComponent> _componentsList;

        private SystemComponent _selectedComponent;

        private readonly ICollection<ControlWithImage> _addedImages = new List<ControlWithImage>();

        private readonly ICollection<Pin> _addedPins = new List<Pin>();

        private int _lastLevel = 0;

        private string _projectFolder;
        
        public Designer(ICollection<SystemComponent> components, string projectFolder)
        {
            InitializeComponent();
            components.ForEach(c => ComponentsComboBox.Items.Add(c.Name));
            _componentsList = components;
            _projectFolder = projectFolder;
            Visible = true;
        }

        private void ComponentsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedComponent = _componentsList.ElementAt(ComponentsComboBox.SelectedIndex);

            if (_selectedComponent.System != null)
            {
                MessageBox.Show("This component is already added to system. Please remove it in order to change its design");
            }
            else
            {
                BrowseComponentImageButton.Enabled = true;
                PinsList.Items.Clear();
                _selectedComponent.Ports.ForEach(port => port.GetAllPins().ForEach(pin => PinsList.Items.Add(pin.Name)));
                PinsList.Enabled = true;
                AddPinButton.Enabled = true;
                SaveButton.Enabled = false;
                _addedImages.Clear();
                _lastLevel = 0;
            }
        }

        private void BrowseComponentImageDialog_FileOk(object sender, CancelEventArgs e)
        {
            var image = new Bitmap(BrowseComponentImageDialog.FileName);
            var control = new ControlWithImage(image, this);
            if (MessageBox.Show("Do you want to make this image transparent?", "Transparent", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                control.MakeTransparent();
            }

            DesignPanel.Controls.Add(control);
            control.Level = _lastLevel++;
            DesignPanel.Refresh();
            _addedImages.Add(control);
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

                SaveComponentPermanently();
            }
        }

        private void SaveComponentPermanently()
        {
            var document = new XmlDocument();
            document.Load(_selectedComponent.ArcFile);
            var rootNode = document.FirstChild;
            XmlNode designNode = null;
            foreach (XmlNode n in rootNode.ChildNodes)
            {
                if (n.Name == "design")
                {
                    designNode = n;
                }
            }

            if (designNode != null)
            {
                rootNode.RemoveChild(designNode);
            }

            designNode = document.CreateElement("design");
            rootNode.AppendChild(designNode);

            foreach (var c in _addedImages)
            {
                var fileName = string.Format("{0}{1}.png", _selectedComponent.Name, c.Level);
                File.WriteAllBytes(string.Format("{0}/Images/{1}", _projectFolder, fileName), c.ImageStream);
                
                var imageNode = document.CreateElement("image");
                designNode.AppendChild(imageNode);

                var fileNameNode = document.CreateElement("file");
                fileNameNode.InnerText = fileName;
                imageNode.AppendChild(fileNameNode);

                var xNode = document.CreateElement("x");
                xNode.InnerText = c.Location.X.ToString();
                imageNode.AppendChild(xNode);

                var yNode = document.CreateElement("y");
                yNode.InnerText = c.Location.Y.ToString();
                imageNode.AppendChild(yNode);
            }

            foreach (var p in _addedPins)
            {
                var pinNode = document.CreateElement("pin");
                designNode.AppendChild(pinNode);

                var nameNode = document.CreateElement("name");
                nameNode.InnerText = p.Name;
                pinNode.AppendChild(nameNode);

                var xNode = document.CreateElement("x");
                xNode.InnerText = p.Location.X.ToString();
                pinNode.AppendChild(xNode);

                var yNode = document.CreateElement("y");
                yNode.InnerText = p.Location.Y.ToString();
                pinNode.AppendChild(yNode);
            }

            document.Save(_selectedComponent.ArcFile);
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

        public void BringToFront(ControlWithImage controlWithImage)
        {
            controlWithImage.BringToFront();
            _addedImages.Where(i => i != controlWithImage && i.Level > controlWithImage.Level).ForEach(i => i.Level--);
            controlWithImage.Level = _lastLevel;
        }

        public void SendToBack(ControlWithImage controlWithImage)
        {
            controlWithImage.SendToBack();
            _addedImages.Where(i => i != controlWithImage && i.Level < controlWithImage.Level).ForEach(i => i.Level++);
            controlWithImage.Level = 0;
        }

        public void Remove(ControlWithImage controlWithImage)
        {
            DesignPanel.Controls.Remove(controlWithImage);
            _addedImages.Where(i => i != controlWithImage && i.Level > controlWithImage.Level).ForEach(i => i.Level--);
            _addedImages.Remove(controlWithImage);
            _lastLevel--;
        }
    }
}
