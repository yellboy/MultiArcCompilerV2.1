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
    public partial class Clipboard : Form
    {

        private bool drawingSignal = false;

        private Signal currentlyDrawing = null;

        private int signalX = 0, signalY = 0;

        private LinkedList<SystemComponent> componentsList;

        public LinkedList<SystemComponent> Component { get { return componentsList; } }

        private UserSystem system;

        public Clipboard(LinkedList<SystemComponent> componentsList, UserSystem system)
        {
            InitializeComponent();
            this.Visible = true;
            this.componentsList = componentsList;
            foreach (Control component in componentsList)
            {
                componentsListBox.Items.Add(component);
            }
            this.system = system;
            system.MyClipboard = this;
            TicksChanged();
        }

        private void addComponentButton_Click(object sender, EventArgs e)
        {
            Graphics graphics = systemPanel1.CreateGraphics();
            SystemComponent selectedComponent = (SystemComponent)(componentsList.ElementAt(componentsListBox.SelectedIndex));
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
                systemPanel1.Controls.Add(componentToAdd as Control);
                if (!system.Components.Contains(componentToAdd))
                {
                    system.Components.AddLast(componentToAdd);
                }
                componentToAdd.System = system;
            }
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
            if (drawingSignal == true)
            {
                if (x != signalX || y != signalY)
                {
                    Line line1 = new Line(signalX, signalY, x, signalY, currentlyDrawing);
                    Line line2 = new Line(x, signalY, x, y, currentlyDrawing);
                    systemPanel1.Controls.Add(line1);
                    systemPanel1.Controls.Add(line2);
                    currentlyDrawing.Lines.AddLast(line1);
                    currentlyDrawing.Lines.AddLast(line2);
                }
                else
                {
                    Line line = new Line(signalX, signalY, x, y, currentlyDrawing);
                    systemPanel1.Controls.Add(line);
                    currentlyDrawing.Lines.AddLast(line);
                }
                drawingSignal = false;
                currentlyDrawing.Pins.AddLast(pin);
                pin.Signal = currentlyDrawing;
                this.Cursor = Cursors.Arrow;
                system.Signals.AddLast(currentlyDrawing);
                currentlyDrawing.SetColor(Color.Violet);
            }
            else
            {
                currentlyDrawing = new Signal();
                currentlyDrawing.Pins.AddLast(pin);
                pin.Signal = currentlyDrawing;
                drawingSignal = true;
                signalX = x;
                signalY = y;
                this.Cursor = Cursors.Cross;
            }
        }

        private void systemPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawingSignal == true)
            {
                Graphics graphics = systemPanel1.CreateGraphics();
                graphics.Clear(systemPanel1.BackColor);
                graphics.DrawLine(Pens.Black, signalX, signalY, e.X, signalY);
                graphics.DrawLine(Pens.Black, e.X, signalY, e.X, e.Y);
            }
        }

        private void systemPanel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            drawingSignal = false;
            this.Cursor = Cursors.Arrow;
            system.Signals.AddLast(currentlyDrawing);
            currentlyDrawing.SetColor(Color.Violet);
        }

        private void systemPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (drawingSignal == true)
            {
                if (e.X != signalX || e.Y != signalY)
                {
                    Line line1 = new Line(signalX, signalY, e.X, signalY, currentlyDrawing);
                    Line line2 = new Line(e.X, signalY, e.X, e.Y, currentlyDrawing);
                    systemPanel1.Controls.Add(line1);
                    systemPanel1.Controls.Add(line2);
                    currentlyDrawing.Lines.AddLast(line1);
                    currentlyDrawing.Lines.AddLast(line2);
                }
                else
                {
                    Line line = new Line(signalX, signalY, e.X, e.Y, currentlyDrawing);
                    systemPanel1.Controls.Add(line);
                    currentlyDrawing.Lines.AddLast(line);
                }
                signalX = e.X;
                signalY = e.Y;
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
            system.Ticks++;
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
        }

        private void DrawSystem()
        {
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
    }
}
