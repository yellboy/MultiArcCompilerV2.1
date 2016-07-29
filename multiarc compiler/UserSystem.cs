/*
 * File: UserSystem.cs
 * Author: Bojan Jelaca
 * Date: November 2014
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using MoreLinq;
using System.Text.RegularExpressions;

namespace MultiArc_Compiler
{
    /// <summary>
    /// Represents current computer system.
    /// </summary>
    public class UserSystem
    {
        private static long lastId = 0;

        private long id = ++lastId;

        private double frequency = 100;

        public double Frequency 
        { 
            get 
            { 
                return frequency; 
            } 
            set 
            { 
                frequency = value; 
            }
        }

        private Thread thread;

        private volatile bool running = false;

        public bool Running 
        { 
            get { return running; } 
            set { running = value; } 
        }

        private long ticks = 0;

        private bool tickByTickMode = false;

        private Clipboard clipboard;

        /// <summary>
        /// Gets or sets cliboard on which systen is drawn.
        /// </summary>
        public Clipboard MyClipboard
        {
            get
            {
                return clipboard;
            }
            set
            {
                clipboard = value;
                components.ForEach(c => c.GetAllPins().ForEach(p => p.Clipboard = value));
            }
        }

        private delegate void SignalTicksChanged();

        private void signalTicksChanged()
        {
            if (clipboard.InvokeRequired)
            {
                SignalTicksChanged d = new SignalTicksChanged(signalTicksChanged);
                clipboard.Invoke(d);
            }
            else
            {
                clipboard.TicksChanged();
            }
        }

        private object _ticksLock = new object();

        /// <summary>
        /// Number of ticks of the system clock.
        /// </summary>
        public long Ticks
        {
            get
            {
                return ticks;
            }
            set
            {
                //lock (_ticksLock)
                //{
                    ticks = value;
                    var output = "Ticking ticks = " + ticks + "; wakeUpTimes = ";
                    foreach (var w in wakeUpTimes)
                    {
                        output += w + " ";
                    }
                    Console.WriteLine(output);
                    if (clipboard != null)
                    {
                        signalTicksChanged();
                    }
                    if (wakeUpTimes.Contains(ticks))
                    {
                        wakeUpTimes.Remove(ticks);
                        Monitor.PulseAll(Form1.LockObject);
                    }
                //}
            }
        }

        private volatile LinkedList<long> wakeUpTimes = new LinkedList<long>();

        private LinkedList<SystemComponent> components = new LinkedList<SystemComponent>();

        /// <summary>
        /// Gets or sets components of the system.
        /// </summary>
        public LinkedList<SystemComponent> Components
        {
            get
            {
                return components;
            }
            set
            {
                components = value;
            }
        }

        private LinkedList<Signal> signals = new LinkedList<Signal>();

        private Executor ex;

        public LinkedList<Signal> Signals
        {
            get
            {
                return signals;
            }
        }

        public LinkedList<Bus> Buses
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates one object of UserSystem class.
        /// </summary>
        public UserSystem()
        {
            Buses = new LinkedList<Bus>();

            components.Clear();
            wakeUpTimes.Clear();
        }

        public void AddToWakeUpTimes(long time)
        {
            if (!wakeUpTimes.Contains(time))
            {
                wakeUpTimes.AddLast(time);
            }
        }

        /// <summary>
        /// Checks if system contains given type of the component.
        /// </summary>
        /// <param name="selectedComponent">
        /// Component that should be checked.
        /// </param>
        /// <returns>
        /// True if system contains component and false otherwise.
        /// </returns>
        public bool ContainsComponentOfGivenType(SystemComponent selectedComponent)
        {
            foreach(SystemComponent component in components)
            {
                if (component.Name.ToLower().Equals(selectedComponent.Name.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        private void run()
        {
            while (true)
            {
                lock (Form1.LockObject)
                {
                    if (running)
                    {
                        if (!tickByTickMode)
                        {
                            //Console.WriteLine("Ticking ticks = " + ticks);
                            int milisecondsTimeout = (int)((1 / frequency) * 1000);
                            Thread.Sleep(milisecondsTimeout);
                            //Thread.Sleep(0);
                            Ticks++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts program execution.
        /// </summary>
        public void StartWorking(LinkedList<int> separators, LinkedList<int> breakPoints, TextBoxBase outputBox, int entryPoint, byte[] binary, bool stepByStepMode = false)
        {
            if (ex == null || ex.Executing == false)
            {
                ResetToDefault();
                thread = new Thread(new ThreadStart(run));
                Console.WriteLine("System thread id = " + thread.ManagedThreadId);
                running = true;
                thread.Start();
                Form1.Instance.ExecutionStarting();
                CPU cpu = (CPU)components.First(c => c is CPU);
                ex = new Executor(cpu, this, separators, breakPoints, outputBox, binary, entryPoint);
                if (stepByStepMode)
                {
                    ex.EnterStepByStep();
                }
                else
                {
                    ex.Debug();
                }
                foreach (var c in components.Where(c => c is NonCPUComponent))
                {
                    ((NonCPUComponent)c).StartWorking();
                }
                if (ex.Executing == true)
                {
                    Form1.Instance.MarkInstruction(ex.Next, Color.Yellow);
                }
            }
            else
            {
                running = true;
                ex.Continue();
                tickByTickMode = false;
                //ex.WaitUntilBreakpointOrEnd();
                //if (ex.Executing == true)
                //{
                //    markInstruction(ex.Next, Color.Yellow);
                //}
                //else
                //{
                //    deselectAllLines();
                //    CodeBox.ReadOnly = false;
                //    CodeBox.BackColor = Color.White;
                //    assembleToolStripMenuItem.Enabled = true;
                //    stopDebuggingToolStripMenuItem.Enabled = false;
                //}
            }

            clipboard.DisableFrequencyChanges();
        }

        public void StartWorkingTickByTick(LinkedList<int> separators, LinkedList<int> breakPoints, TextBoxBase outputBox, int entryPoint, byte[] binary)
        {
            tickByTickMode = true;
            StartWorking(separators, breakPoints, outputBox, entryPoint, binary);
        }

        public void ExecuteNextStep(LinkedList<int> separators, LinkedList<int> breakPoints, TextBoxBase outputBox, int entryPoint, byte[] binary)
        {
            running = true;
            if (ex == null || !ex.Executing)
            {
                StartWorking(separators, breakPoints, outputBox, entryPoint, binary, true);
            }
            else
            {
                ex.ExecuteNextStep();
            }
        }

        public void EnterTickByTickMode()
        {
            tickByTickMode = true;
        }

        /// <summary>
        /// Ends program execution.
        /// </summary>
        public void EndWorking()
        {
            running = false;
            thread.Abort();
            foreach (NonCPUComponent component in components.Where(c => c is NonCPUComponent))
            {
                component.StopWorking();
            }

            clipboard.EnableFrequencyChanges();
        }

        /// <summary>
        /// Wait certain amount of system ticks.
        /// </summary>
        /// <param name="numberOfTicks">
        /// Number of ticks to wait.
        /// </param>
        public void Wait(long numberOfTicks)
        {
            //lock (_ticksLock)
            //{
                var wakeUpTime = ticks + numberOfTicks;
                AddToWakeUpTimes(wakeUpTime);
                while (ticks < wakeUpTime)
                {
                    Console.WriteLine("Thread {0} waiting for {1} ticks at {2}. Wake up time {3}.", Thread.CurrentThread.ManagedThreadId, numberOfTicks, ticks, wakeUpTime);
                    Monitor.Wait(Form1.LockObject);
                    Console.WriteLine("Thread {0} waking at {1} ticks", Thread.CurrentThread.ManagedThreadId, ticks);
                }
            //}
        }

        /// <summary>
        /// Remove component from system.
        /// </summary>
        /// <param name="component">
        /// Component to be removed.
        /// </param>
        public void RemoveComponent(SystemComponent component)
        {
            components.Remove(component);

            if (!components.Any())
            {
                clipboard.EnableCloseButton();
            }
        }

        public void SaveSystemToFile(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode systemNode = doc.CreateElement("system");
            doc.AppendChild(systemNode);
            XmlNode componentsNode = doc.CreateElement("components");
            systemNode.AppendChild(componentsNode);
            foreach (SystemComponent c in components)
            {
                XmlNode componentNode = doc.CreateElement("component");
                XmlAttribute nameAttribute = doc.CreateAttribute("name");
                nameAttribute.Value = c.Name;
                componentNode.Attributes.Append(nameAttribute);
                XmlAttribute typeAttribute = doc.CreateAttribute("type");
                typeAttribute.Value = c.GetType().FullName;
                componentNode.Attributes.Append(typeAttribute);
                XmlAttribute xAttribute = doc.CreateAttribute("x");
                xAttribute.Value = c.Location.X.ToString();
                componentNode.Attributes.Append(xAttribute);
                XmlAttribute yAttribute = doc.CreateAttribute("y");
                yAttribute.Value = c.Location.Y.ToString();
                componentNode.Attributes.Append(yAttribute);
                componentsNode.AppendChild(componentNode);
            }
            XmlNode signalsNode = doc.CreateElement("signals");
            systemNode.AppendChild(signalsNode);
            foreach (Signal s in signals)
            {
                XmlNode signalNode = doc.CreateElement("signal");
                foreach (var name in s.Names)
                {
                    XmlElement nameElement = doc.CreateElement("name");
                    nameElement.InnerText = name;
                    signalNode.AppendChild(nameElement);
                }

                XmlNode linesNode = doc.CreateElement("lines");
                signalNode.AppendChild(linesNode);
                foreach (Line l in s.Lines.Where(l => l.ContainedByBus == null))
                {
                    XmlNode lineNode = doc.CreateElement("line");
                    linesNode.AppendChild(lineNode);
                    XmlAttribute x1Attribute = doc.CreateAttribute("x1");
                    x1Attribute.Value = l.X1.ToString();
                    lineNode.Attributes.Append(x1Attribute);
                    XmlAttribute x2Attribute = doc.CreateAttribute("x2");
                    x2Attribute.Value = l.X2.ToString();
                    lineNode.Attributes.Append(x2Attribute);
                    XmlAttribute y1Attribute = doc.CreateAttribute("y1");
                    y1Attribute.Value = l.Y1.ToString();
                    lineNode.Attributes.Append(y1Attribute);
                    XmlAttribute y2Attribute = doc.CreateAttribute("y2");
                    y2Attribute.Value = l.Y2.ToString();
                    lineNode.Attributes.Append(y2Attribute);
                }
                XmlNode pinsNode = doc.CreateElement("pins");
                signalNode.AppendChild(pinsNode);
                foreach (Pin pin in s.Pins)
                {
                    XmlNode pinNode = doc.CreateElement("pin");
                    XmlAttribute pinNameAttribute = doc.CreateAttribute("pin_name");
                    pinNameAttribute.Value = pin.Name;
                    pinNode.Attributes.Append(pinNameAttribute);
                    XmlAttribute componentNameAttribute = doc.CreateAttribute("component_name");
                    componentNameAttribute.Value = pin.ParentPort.Component.Name;
                    pinNode.Attributes.Append(componentNameAttribute);
                    pinsNode.AppendChild(pinNode);
                }
                signalsNode.AppendChild(signalNode);
            }
            XmlNode busesNode = doc.CreateElement("buses");
            systemNode.AppendChild(busesNode);
            foreach (var b in Buses)
            {
                XmlNode busNode = doc.CreateElement("bus");
                busesNode.AppendChild(busNode);
                foreach (var n in b.Names)
                {
                    XmlNode nameNode = doc.CreateElement("name");
                    nameNode.InnerText = n;
                    busNode.AppendChild(nameNode);
                }
                XmlNode linesNode = doc.CreateElement("lines");
                busNode.AppendChild(linesNode);
                foreach (var s in b.Signals)
                {
                    XmlNode signalNode = doc.CreateElement("signal");
                    signalNode.InnerText = s.Names.First();
                    busNode.AppendChild(signalNode);
                }
                foreach (var l in b.Lines)
                {
                    XmlNode lineNode = doc.CreateElement("line");
                    linesNode.AppendChild(lineNode);
                    XmlAttribute x1Attribute = doc.CreateAttribute("x1");
                    x1Attribute.Value = l.X1.ToString();
                    lineNode.Attributes.Append(x1Attribute);
                    XmlAttribute x2Attribute = doc.CreateAttribute("x2");
                    x2Attribute.Value = l.X2.ToString();
                    lineNode.Attributes.Append(x2Attribute);
                    XmlAttribute y1Attribute = doc.CreateAttribute("y1");
                    y1Attribute.Value = l.Y1.ToString();
                    lineNode.Attributes.Append(y1Attribute);
                    XmlAttribute y2Attribute = doc.CreateAttribute("y2");
                    y2Attribute.Value = l.Y2.ToString();
                    lineNode.Attributes.Append(y2Attribute);
                }
            }
            doc.Save(fileName);
        }

        public void LoadSystemFromFile(string fileName)
        {
            signals = new LinkedList<Signal>();
            Buses = new LinkedList<Bus>();
            components = new LinkedList<SystemComponent>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode systemNode = doc.FirstChild;
            foreach (XmlNode childNode in systemNode.ChildNodes)
            {
                if (childNode.Name.Equals("components"))
                {
                    foreach (XmlNode componentNode in childNode.ChildNodes)
                    {
                        SystemComponent component = clipboard.Component.First(c => c.Name == componentNode.Attributes["name"].Value);
                        component.Name = componentNode.Attributes["name"].Value;
                        component.System = this;
                        component.Location = new Point(Convert.ToInt32(componentNode.Attributes["x"].Value), Convert.ToInt32(componentNode.Attributes["y"].Value));
                        components.AddLast(component);
                    }
                }
                else if (childNode.Name.Equals("signals"))
                {
                    foreach (XmlNode signalNode in childNode.ChildNodes)
                    {
                        Signal signal = new Signal(this);
                        foreach (XmlNode innerNode in signalNode.ChildNodes)
                        {
                            switch (innerNode.Name)
                            {
                                case "lines":
                                    foreach (XmlNode lineNode in innerNode.ChildNodes)
                                    {
                                        int x1 = Convert.ToInt32(lineNode.Attributes["x1"].Value);
                                        int x2 = Convert.ToInt32(lineNode.Attributes["x2"].Value);
                                        int y1 = Convert.ToInt32(lineNode.Attributes["y1"].Value);
                                        int y2 = Convert.ToInt32(lineNode.Attributes["y2"].Value);
                                        Line line = new Line(1, x1, y1, x2, y2, signal);
                                        signal.Lines.AddLast(line);
                                    }
                                    break;
                                case "pins":
                                    foreach (XmlNode pinNode in innerNode.ChildNodes)
                                    {
                                        Pin pin = components.First(c => c.Name == pinNode.Attributes["component_name"].Value).GetPin(pinNode.Attributes["pin_name"].Value);
                                        pin.Signal = signal;
                                        pin.Clipboard = clipboard;
                                        signal.Pins.AddLast(pin);
                                    }
                                    break;
                                case "name":
                                    signal.Names.AddLast(innerNode.InnerText.Trim());
                                    break;
                                default:
                                    break;
                            }
                        }
                        signals.AddLast(signal);
                    }
                }
                else if (childNode.Name.Equals("buses"))
                {
                    foreach (XmlNode busNode in childNode.ChildNodes)
                    {
                        Bus bus = new Bus(this);
                        foreach (XmlNode innerNode in busNode.ChildNodes)
                        {
                            switch (innerNode.Name)
                            {
                                case "lines":
                                    foreach (XmlNode lineNode in innerNode.ChildNodes)
                                    {
                                        int x1 = Convert.ToInt32(lineNode.Attributes["x1"].Value);
                                        int x2 = Convert.ToInt32(lineNode.Attributes["x2"].Value);
                                        int y1 = Convert.ToInt32(lineNode.Attributes["y1"].Value);
                                        int y2 = Convert.ToInt32(lineNode.Attributes["y2"].Value);
                                        Line line = new Line(5, x1, y1, x2, y2);
                                        line.ContainedByBus = bus;
                                        bus.Lines.AddLast(line);
                                    }
                                    break;
                                case "signal":
                                    string signalName = innerNode.InnerText.Trim();
                                    Signal signal = signals.First(s => s.Names.Any(n => n == signalName));
                                    bus.Signals.AddLast(signal);
                                    signal.Bus = bus;
                                    break;
                                case "name":
                                    string name = innerNode.InnerText.Trim();
                                    bus.Names.AddLast(name);
                                    break;
                                default:
                                    break;
                            }
                        }
                        foreach (Signal s in bus.Signals)
                        {
                            foreach (Line l in bus.Lines)
                            {
                                s.Lines.AddLast(l);
                            }
                        }
                        Buses.AddLast(bus);
                    }
                }
            }
        }

        public void ResetToDefault()
        {
            foreach (var c in components)
            {
                c.ResetToDefault();
            }
            wakeUpTimes.Clear();
            ticks = 0;
        }
    }
}
