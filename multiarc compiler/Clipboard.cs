using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MoreLinq;
using System.Runtime.InteropServices;

namespace MultiArc_Compiler
{
    public partial class Clipboard : Form
    {
        private const int NoCloseButton = 0x200;

        private bool drawingConnector = false;

        private Connector currentlyDrawnConnector = null;

        private bool drawingBus = false;

        private int connectorX = 0, connectorY = 0;

        private LinkedList<SystemComponent> componentsList;

        public LinkedList<SystemComponent> Component { get { return componentsList; } }
        
        private UserSystem system;

        public Clipboard(LinkedList<SystemComponent> componentsList, UserSystem system)
        {
            InitializeComponent();
            this.Visible = true;
            this.componentsList = componentsList;
            foreach (var component in componentsList)
            {
                componentsListBox.Items.Add(component);
                component.DetachAllSignals();
            }
            this.system = system;
            system.MyClipboard = this;
            TicksChanged();
            addComponentButton.Enabled = false;
            frequencyInput.Value = (decimal)system.Frequency;
        }

        private void addComponentButton_Click(object sender, EventArgs e)
        {
            Graphics graphics = systemPanel1.CreateGraphics();
            SystemComponent selectedComponent = componentsList.ElementAt(componentsListBox.SelectedIndex);
            SystemComponent componentToAdd;
            if (!system.ContainsComponentOfGivenType(selectedComponent))
            {
                componentToAdd = selectedComponent;
            }
            else
            {
                componentToAdd = (SystemComponent)selectedComponent.Clone();
            }
            if (componentToAdd.SignalAttached == false)
            {
                componentToAdd.Draw();
                systemPanel1.Controls.Add(componentToAdd);
                if (!system.Components.Contains(componentToAdd))
                {
                    system.Components.AddLast(componentToAdd);
                }
                componentToAdd.System = system;
                componentToAdd.GetAllPins().ForEach(p => p.Clipboard = this);
            }

            DisableCloseButton();
        }

        private void componentsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0 && e.Index < componentsListBox.Items.Count)
            {
                e.Graphics.DrawString(((SystemComponent)componentsListBox.Items[e.Index]).Name, e.Font, Brushes.Black, e.Bounds);
            }
        }

        public void PinClicked(Pin pin)
        {
            int x = pin.Location.X + pin.Parent.Location.X + (pin.ParentPort.PortPosition == Position.RIGHT ? 5 : 0);
            int y = pin.Location.Y + pin.Parent.Location.Y + (pin.ParentPort.PortPosition == Position.DOWN ? 5 : 0);
            if (drawingConnector && !drawingBus)
            {
                var signal = currentlyDrawnConnector as Signal;
                
                if (x != connectorX || y != connectorY)
                {
                    Line line1 = new Line(Thicknes, connectorX, connectorY, x, connectorY, signal);
                    Line line2 = new Line(Thicknes, x, connectorY, x, y, signal);
                    systemPanel1.Controls.Add(line1);
                    systemPanel1.Controls.Add(line2);
                    currentlyDrawnConnector.Lines.AddLast(line1);
                    currentlyDrawnConnector.Lines.AddLast(line2);
                }
                else
                {
                    Line line = new Line(Thicknes, connectorX, connectorY, x, y, signal);
                    systemPanel1.Controls.Add(line);
                    currentlyDrawnConnector.Lines.AddLast(line);
                }
                drawingConnector = false;
                signal.Pins.AddLast(pin);
                pin.Signal = signal;
                Cursor = Cursors.Arrow;
                system.Signals.AddLast(signal);
                currentlyDrawnConnector.SetColor(Color.Violet);
            }
            else
            {
                currentlyDrawnConnector = new Signal(system);
                var signal = currentlyDrawnConnector as Signal;
                signal.Pins.AddLast(pin);
                pin.Signal = signal;
                StartDrawing(x, y);
            }
        }

        private void StartDrawing(int x, int y)
        {
            currentlyDrawnConnector.AddGenericName();
            drawingConnector = true;
            connectorX = x;
            connectorY = y;
            this.Cursor = Cursors.Cross;
        }

        private void systemPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawingConnector == true)
            {
                Graphics graphics = systemPanel1.CreateGraphics();
                graphics.PageUnit = GraphicsUnit.Pixel;
                graphics.Clear(systemPanel1.BackColor);
                var pen = new Pen(Color.Black, Thicknes);
                graphics.DrawLine(pen, connectorX, connectorY, e.X, connectorY);
                graphics.DrawLine(pen, e.X, connectorY, e.X, e.Y);
            }
        }

        private void systemPanel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private int GetBusWidth()
        {
            using (var dialog = new SetBusWidthForm(this))
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return dialog.BusWidth;
                }

                return 0;
            }
        }

        private void systemPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (drawingConnector == true)
                {
                    if (e.X != connectorX || e.Y != connectorY)
                    {
                        Line line1 = new Line(Thicknes, connectorX, connectorY, e.X, connectorY);
                        Line line2 = new Line(Thicknes, e.X, connectorY, e.X, e.Y);
                        systemPanel1.Controls.Add(line1);
                        systemPanel1.Controls.Add(line2);
                        currentlyDrawnConnector.Lines.AddLast(line1);
                        currentlyDrawnConnector.Lines.AddLast(line2);
                    }
                    else
                    {
                        Line line = new Line(Thicknes, connectorX, connectorY, e.X, e.Y);
                        systemPanel1.Controls.Add(line);
                        currentlyDrawnConnector.Lines.AddLast(line);
                    }
                    connectorX = e.X;
                    connectorY = e.Y;
                }
                else if (drawingBus)
                {
                    drawingBus = true;
                    currentlyDrawnConnector = new Bus(system);
                    StartDrawing(e.X, e.Y);
                }
            }
            else
            {
                if (drawingConnector)
                {
                    if (e.X != connectorX || e.Y != connectorY)
                    {
                        Line line1 = new Line(Thicknes, connectorX, connectorY, e.X, connectorY);
                        Line line2 = new Line(Thicknes, e.X, connectorY, e.X, e.Y);
                        systemPanel1.Controls.Add(line1);
                        systemPanel1.Controls.Add(line2);
                        currentlyDrawnConnector.Lines.AddLast(line1);
                        currentlyDrawnConnector.Lines.AddLast(line2);
                    }
                    else
                    {
                        Line line = new Line(Thicknes, connectorX, connectorY, e.X, e.Y);
                        systemPanel1.Controls.Add(line);
                        currentlyDrawnConnector.Lines.AddLast(line);
                    }

                    drawingConnector = false;
                    this.Cursor = Cursors.Arrow;
                    if (drawingBus)
                    {
                        var bus = currentlyDrawnConnector as Bus;
                        system.Buses.AddLast(bus);
                        var width = GetBusWidth();
                        for (var i = 0; i < width; i++)
                        {
                            var signal = new Signal(system);
                            signal.Bus = bus;
                            signal.Names.AddLast(string.Format("{0}[{1}]", bus.Names.First(), i));
                            bus.Lines.ForEach(l =>
                            {
                                signal.Lines.AddLast(l);
                                l.ContainedBySignal = signal;
                            });
                            bus.Signals.AddLast(signal);
                            system.Signals.AddLast(signal);
                        }
                    }
                    else
                    {
                        var signal = currentlyDrawnConnector as Signal;
                        system.Signals.AddLast(signal);
                        signal.Lines.ForEach(l => l.ContainedBySignal = signal);
                    }

                    drawingBus = false;
                    currentlyDrawnConnector.SetColor(Color.Violet);
                }
            }
        }

        private int Thicknes
        {
            get
            {
                return drawingBus ? 5 : 1;
            }
        }

        public void TicksChanged()
        {
            ticksCountLabel.Text = "" + system.Ticks;
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("GUI thread id = " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            Form1.Instance.Execute();
        }

        private void nextClockButton_Click(object sender, EventArgs e)
        {
            if (!system.Running)
            {
                Form1.Instance.ExecuteTickByTick();
            }

            lock (Form1.LockObject)
            {
                system.Ticks++;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            system.EndWorking();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveSystemDialog.ShowDialog();
        }

        private void SaveSystemDialog_FileOk(object sender, CancelEventArgs e)
        {
            system.SaveSystemToFile(SaveSystemDialog.FileName);
        }

        private void LoadSystemButton_Click(object sender, EventArgs e)
        {
            LoadSystemDialog.ShowDialog();
        }

        private void LoadSystemDialog_FileOk(object sender, CancelEventArgs e)
        {
            system.LoadSystemFromFile(LoadSystemDialog.FileName);
            DrawSystem();
            DisableCloseButton();
        }

        private void DrawSystem()
        {
            systemPanel1.Controls.Clear();

            foreach (var c in system.Components)
            {
                c.Draw();
                systemPanel1.Controls.Add(c);
            }
            foreach (var s in system.Signals)
            {
                foreach (var l in s.Lines)
                {
                    systemPanel1.Controls.Add(l);
                }
                s.SetColor(Color.Violet);
            }
            systemPanel1.Refresh();
        }

        [DllImport("user32")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint itemId, uint uEnable);

        public void DisableCloseButton()
        {
            // The 1 parameter means to gray out. 0xF060 is SC_CLOSE.
            EnableMenuItem(GetSystemMenu(Handle, false), 0xF060, 1);
        }

        public void EnableCloseButton()
        {
            // The zero parameter means to enable. 0xF060 is SC_CLOSE.
            EnableMenuItem(GetSystemMenu(Handle, false), 0xF060, 0);
        }

        private void componentsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (componentsListBox.SelectedItems.Count > 0)
            {
                addComponentButton.Enabled = true;
                return;
            }

            addComponentButton.Enabled = false;
        }

        private delegate void SwitchFrequencyInputEnabledDelegate(bool enabled);

        private void SwitchFrequencyInputEnabled(bool enabled)
        {
            frequencyInput.Enabled = enabled;
        }

        public void DisableFrequencyChanges()
        {
            SwitchFrequencyInputEnabledThreadSafe(false);
        }

        public void EnableFrequencyChanges()
        {
            SwitchFrequencyInputEnabledThreadSafe(true);
        }

        private void SwitchFrequencyInputEnabledThreadSafe(bool enable)
        {
            if (frequencyInput.InvokeRequired)
            {
                SwitchFrequencyInputEnabledDelegate d = new SwitchFrequencyInputEnabledDelegate(SwitchFrequencyInputEnabled);
                this.BeginInvoke(d, enable);
            }
            else
            {
                SwitchFrequencyInputEnabled(enable);
            }
        }

        private void frequencyInput_ValueChanged(object sender, EventArgs e)
        {
            system.Frequency = (double)frequencyInput.Value;
        }

        private void DrawBusButton_Click(object sender, EventArgs e)
        {
            drawingBus = true;
        }

        private void systemPanel1_Click(object sender, EventArgs e)
        {
            foreach (var c in systemPanel1.Controls)
            {
                var dropableControl = c as DropableControl;
                if (dropableControl != null)
                {
                    dropableControl.DeselectControl();
                }
            }
        }
    }
}
