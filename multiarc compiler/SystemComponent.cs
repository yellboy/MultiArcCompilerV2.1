﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MoreLinq;
using System.Xml;

namespace MultiArc_Compiler
{
    public abstract class SystemComponent : DropableControl
    {
        protected string name;

        /// <summary>
        /// Gets or sets name of the component.
        /// </summary>
        public new abstract string Name
        {
            get;
            set;
        }

        public string ArcFile { get; protected set; }

        public string FileName 
        { 
            get; 
            protected set; 
        }

        public abstract string ArcDirectoryName { get; }


        protected LinkedList<Port> ports = new LinkedList<Port>();
        
        /// <summary>
        /// List of all ports.
        /// </summary>
        public LinkedList<Port> Ports
        {
            get
            {
                return ports;
            }
            set
            {
                ports = value;
            }
        }

        /// <summary>
        /// Gets one port of the component.
        /// </summary>
        /// <param name="name">
        /// Name of the wanted port.
        /// </param>
        /// <returns>
        /// Wanted port or null if there is no port with the given name.
        /// </returns>
        public Port GetPort(string name)
        {
            return ports.FirstOrDefault(port => port.Name.ToLower().Equals(name.ToLower()));
        }

        public Pin GetPin(string name)
        {
            foreach (Port port in ports)
            {
                for (int i = 0; i < port.Size; i++)
                {
                    if (port[i].Name.ToLower().Equals(name.ToLower()))
                    {
                        return port[i];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all ports of the component.
        /// </summary>
        /// <returns>
        /// All ports of the component in linked list.
        /// </returns>
        public LinkedList<Port> GetAllPorts()
        {
            return ports;
        }

        protected UserSystem system;

        /// <summary>
        /// Gets or sets user system this component is part of.
        /// </summary>
        public UserSystem System
        {
            get
            {
                return system;
            }
            set
            {
                system = value;
            }
        }

        /// <summary>
        /// Indicating whether there is signal attached to any port of component.
        /// </summary>
        public bool SignalAttached
        {
            get
            {
                foreach (Port port in ports)
                {
                    for (int i = 0; i < port.Size; i++)
                    {
                        if (port[i].Signal != null)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        protected ContextMenuStrip menu = new ContextMenuStrip();

        protected virtual string[] MenuItems 
        {
            get
            {
                return new[] 
                {
                    "Remove"
                };
            }
        }

        protected string _projectPath;

        /// <summary>
        /// Creates one object of SystemComponent class.
        /// </summary>
        public SystemComponent(string projectPath)
        {
            _projectPath = projectPath;
            base.Paint += this.redraw;
            base.AllowDrop = true;
            MenuItems.ForEach(mi => menu.Items.Add(mi));
            menu.ItemClicked += MenuItemClicked;
        }

        protected virtual void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            system.RemoveComponent(this);
            Parent.Controls.Remove(this);
        }

        protected void redraw(object sender, PaintEventArgs e)
        {
            Graphics graphics = this.CreateGraphics();

            var haveNonPortControls = false;
            foreach (var c in Controls)
            {
                if (c is ControlWithImage)
                {
                    haveNonPortControls = true;
                    break;
                }
            }

            if (haveNonPortControls)
            {
            }
            else
            {
                Rectangle rectangle = new Rectangle(5, 5, this.Width - 10, this.Height - 10);
                graphics.FillRectangle(new SolidBrush(Color.White), rectangle);
                graphics.DrawRectangle(Pens.Black, rectangle);
                LinkedList<Port> rightPorts = new LinkedList<Port>();
                int rightCount = 0;
                rightPorts.Clear();
                foreach (Port port in ports)
                {
                    if (port.PortPosition == Position.RIGHT)
                    {
                        rightPorts.AddLast(port);
                        rightCount += port.Size;
                    }
                }
                int rightStep = rightCount != 0 ? (this.Height - 10) / rightCount : 0;
                int y = 5 + rightStep / 2;
                foreach (Port port in rightPorts)
                {
                    for (int i = 0; i < port.Size; i++)
                    {
                        string pinName = port.Name + "" + i;
                        graphics.DrawString(pinName, new Font(new FontFamily("Arial"), 6), Brushes.Black, this.Width - 5 - pinName.Length * 6, y - 3);
                        //graphics.DrawLine(Pens.Black, this.Width - 5, y, this.Width, y);
                        port[i].Location = new Point(this.Width - 5, y);
                        y += rightStep;
                    }
                }
                LinkedList<Port> leftPorts = new LinkedList<Port>();
                int leftCount = 0;
                leftPorts.Clear();
                foreach (Port port in ports)
                {
                    if (port.PortPosition == Position.LEFT)
                    {
                        leftPorts.AddLast(port);
                        leftCount += port.Size;
                    }
                }
                int leftStep = leftCount != 0 ? (this.Height - 10) / leftCount : 0;
                y = 5 + leftStep / 2;
                foreach (Port port in leftPorts)
                {
                    for (int i = 0; i < port.Size; i++)
                    {
                        string pinName = port.Name + "" + i;
                        graphics.DrawString(pinName, new Font(new FontFamily("Arial"), 6), Brushes.Black, 5, y - 3);
                        port[i].Location = new Point(0, y);
                        y += leftStep;
                    }
                }
                LinkedList<Port> upPorts = new LinkedList<Port>();
                int upCount = 0;
                upPorts.Clear();
                foreach (Port port in ports)
                {
                    if (port.PortPosition == Position.UP)
                    {
                        upPorts.AddLast(port);
                        upCount += port.Size;
                    }
                }
                int upStep = upCount != 0 ? (this.Width - 10) / upCount : 0;
                int x = 5 + upStep / 2;
                foreach (Port port in upPorts)
                {
                    for (int i = 0; i < port.Size; i++)
                    {
                        string pinName = port.Name + "" + i;
                        graphics.DrawString(pinName, new Font(new FontFamily("Arial"), 6), Brushes.Black, x - pinName.Length * 3, 8);
                        port[i].Location = new Point(x, 0);
                        x += upStep;
                    }
                }
                LinkedList<Port> downPorts = new LinkedList<Port>();
                int downCount = 0;
                downPorts.Clear();
                foreach (Port port in ports)
                {
                    if (port.PortPosition == Position.DOWN)
                    {
                        downPorts.AddLast(port);
                        downCount += port.Size;
                    }
                }
                int downStep = downCount != 0 ? (this.Width - 10) / downCount : 0;
                x = 5 + downStep / 2;
                foreach (Port port in downPorts)
                {
                    for (int i = 0; i < port.Size; i++)
                    {
                        string pinName = port.Name + "" + i;
                        graphics.DrawString(pinName, new Font(new FontFamily("Arial"), 6), Brushes.Black, x - pinName.Length * 3, this.Height - 14);
                        port[i].Location = new Point(x, this.Height - 5);
                        x += downStep;
                    }
                }
            }
        }

        /// <summary>
        /// Loads component from file.
        /// </summary>
        /// <param name="arcFile">
        /// Path to the file with specification.
        /// </param>
        /// <param name="dataFolder">
        /// Data folder of the project.
        /// </param>
        /// <returns></returns>
        public abstract int Load(string arcFile, string dataFolder);

        /// <summary>
        /// Draws component on the clipboard.
        /// </summary>
        public virtual void Draw()
        {
            Visible = true;
            foreach (Port port in ports)
            {
                for (int i = 0; i < port.Size; i++)
                {
                    base.Controls.Add(port[i]);
                }
            }
        }

        /// <summary>
        /// Makes copy of the component.
        /// </summary>
        /// <returns>
        /// New component as copy of current component.
        /// </returns>
        public abstract object Clone();

        public override void MouseDownAction(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && SignalAttached == false)
            {
                DoDragDrop(this, DragDropEffects.Move);
            }
            else if (e.Button == MouseButtons.Right)
            {
                menu.Show(this, new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y));
            }
        }

        public void Wait(string portName, int value)
        {
            Port port = GetPort(portName);
            //lock (port)
            //{
                while (port.Val != value)
                {
                    //Monitor.Wait(port);
                    Monitor.Wait(Form1.LockObject);
                }
            //}
        }

        public void Wait(string pinName, PinValue value)
        {
            Pin pin = GetPin(pinName);
            //lock (pin.ParentPort)
            //{
                while (pin.Val != value)
                {
                    //Monitor.Wait(pin.ParentPort);
                    Monitor.Wait(Form1.LockObject);
                }
            //}
        }

        public void WaitForRisingEdge(string pinName)
        {
            Pin pin = GetPin(pinName);
            //lock (pin.ParentPort)
            //{
                while (!((pin.OldVal == PinValue.FALSE || pin.OldVal == PinValue.HIGHZ) && pin.Val == PinValue.TRUE))
                {
                    Console.WriteLine("Thread {0} waiting for rising edge of {1}", Thread.CurrentThread.ManagedThreadId, pinName);
                    //Monitor.Wait(pin.ParentPort);
                    Monitor.Wait(Form1.LockObject);
                    Console.WriteLine("Thread {0} waiting for rising edge of {1} waking up", Thread.CurrentThread.ManagedThreadId, pinName);
                }
            //}
        }

        public void WaitForFallingEdge(string pinName)
        {
            Pin pin = GetPin(pinName);
            //lock (pin.ParentPort)
            //{
                while (!(pin.OldVal == PinValue.TRUE && pin.Val == PinValue.FALSE))
                {
                    //Monitor.Wait(pin.ParentPort);
                    Monitor.Wait(Form1.LockObject);
                }
            //}
        }

        public void Wait(long systemTicks)
        {
            system.Wait(systemTicks);
        }

        public abstract int CompileCode(string dataFolder);

        public virtual void ResetToDefault()
        {
            foreach (var p in ports)
            {
                p.ResetToDefault();
            }
        }

        public IEnumerable<Pin> GetAllPins()
        {
            var pinsList = new List<Pin>();
            ports.ForEach(port => pinsList.AddRange(port.GetAllPins()));

            return pinsList;
        }

        public void ProcessDesignNode(XmlNode designNode)
        {
            foreach (Control c in this.Controls)
            {
                if (c is ControlWithImage)
                {
                    ((ControlWithImage)c).DisposeImage();
                    Controls.Remove(c);
                }
            }

            var imageControls = new List<ControlWithImage>();

            foreach (XmlNode node in designNode.ChildNodes)
            {
                if (node.Name == "image")
                {
                    string fileName = null;
                    int x = 0;
                    int y = 0;
                    int level = 0;
                    bool transparent = false;

                    foreach (XmlNode property in node.ChildNodes)
                    {
                        switch (property.Name)
                        {
                            case "file":
                                fileName = property.InnerText;
                                break;
                            case "level":
                                level = Convert.ToInt32(property.InnerText);
                                break;
                            case "x":
                                x = Convert.ToInt32(property.InnerText);
                                break;
                            case "y":
                                y = Convert.ToInt32(property.InnerText);
                                break;
                            case "transparent":
                                transparent = Convert.ToBoolean(property.InnerText);
                                break;
                            default:
                                break;
                        }
                    }

                    CopyImageToTempFolder(fileName);
                    var image = new Bitmap(string.Format("{0}/temp/{1}", _projectPath, fileName));
                    var control = new ControlWithImage(image, null)
                    {
                        Transparent = transparent,
                        Location = new Point(x, y),
                        Level = level
                    };

                    imageControls.Add(control);
                    control.Draw();
                }
                else if (node.Name == "pin")
                {
                    int x = 0;
                    int y = 0;
                    string pinName = null;

                    foreach (XmlNode property in node.ChildNodes)
                    {
                        switch (property.Name)
                        {
                            case "name":
                                pinName = property.InnerText;
                                break;
                            case "x":
                                x = Convert.ToInt32(property.InnerText);
                                break;
                            case "y":
                                y = Convert.ToInt32(property.InnerText);
                                break;
                            default:
                                break;
                        }
                    }

                    var pin = GetPin(pinName);
                    pin.Location = new Point(x, y);
                }
            }

            int lowerBorder = imageControls.Select(c => c.Location.Y + c.Height).Max();
            int upperBorder = 0;
            int leftBorder = 0;
            int rightBorder = imageControls.Select(c => c.Location.X + c.Width).Max();

            foreach (var p in GetAllPins())
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

            Size = new Size(rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1);

            foreach (var c in imageControls)
            {
                c.SetBounds(c.Location.X - leftBorder, c.Location.Y - upperBorder, c.Width, c.Height);
                c.AddToSystemComponent(this);
                if (Region == null)
                {
                    Region = c.Region.Clone();
                }
                else
                {
                    Region.Union(c.Region);
                }
            }

            foreach (var p in GetAllPins())
            {
                Region.Union(new Rectangle(p.Location.X, p.Location.Y, p.Size.Width, p.Size.Height));
            }

            imageControls.OrderBy(c => c.Level).ForEach(c => c.BringToFront());
            GetAllPins().ForEach(p => p.BringToFront());
        }

        private void CopyImageToTempFolder(string fileName)
        {
            var tempDirectoryPath = string.Format("{0}/temp", _projectPath);
            if (!Directory.Exists(tempDirectoryPath))
            {
                Directory.CreateDirectory(tempDirectoryPath);
            }

            var tempFile = string.Format("{0}/{1}", tempDirectoryPath, fileName);
            var file = File.Open(tempFile, FileMode.OpenOrCreate, FileAccess.Write);
            file.Close();
            File.Copy(string.Format("{0}/Images/{1}", _projectPath, fileName), tempFile, true);
        }

        public void DisposeAllImages()
        {
            foreach (var c in Controls)
            {
                if (c is ControlWithImage)
                {
                    ((ControlWithImage) c).DisposeImage();
                }
            }
        }
    }
}
