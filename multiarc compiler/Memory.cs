/*
* File: Memory.cs
* Author: Bojan Jelaca
* Date: September 2013
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using MoreLinq;
using System.Linq;

namespace MultiArc_Compiler
{

    /// <sumary>
    /// Simulates memory.
    /// </sumary>
    public class Memory: NonCPUComponent
    {
        private const string MemoryDump = "Memory dump";

        private string name;

        /// <summary>
        /// Gets or sets name of the memory.
        /// </summary>
        public override string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private static int nextId = 0;

        private int id = nextId++;

        private int size = -1;

        /// <summary>
        /// Size of memory in bytes.
        /// </summary>
        public new int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        private uint romStart;

        /// <summary>
        /// Begining address of rom memory.
        /// </summary>
        public uint RomStart
        {
            get
            {
                return romStart;
            }
            set
            {
                romStart = value;
            }
        }

        private uint romEnd;

        /// <summary>
        /// Ending address of rom memory.
        /// </summary>
        public uint RomEnd
        {
            get
            {
                return romEnd;
            }
            set
            {
                romEnd = value;
            }
        }

        private uint ramStart;

        /// <summary>
        /// Starting address of ram memory.
        /// </summary>
        public uint RamStart
        {
            get
            {
                return ramStart;
            }
            set
            {
                ramStart = value;
            }
        }

        private uint ramEnd;

        /// <summary>
        /// Ending address of ram memory.
        /// </summary>
        public uint RamEnd
        {
            get
            {
                return ramEnd;
            }
            set
            {
                ramEnd = value;
            }
        }

        /// <summary>
        /// Array that represents memory.
        /// </summary>
        private byte[][] memory;

        /// <summary>
        /// Array that represents the information about taken memory.
        /// </summary>
        private bool[] free;

        /// <summary>
        /// Size of addressible unit in bytes.
        /// </summary>
        private int auSize = -1;

        public int AuSize
        {
            get
            {
                return auSize;
            }
            set
            {
                auSize = value;
            }
        }

        private string initFile;

        /// <summary>
        /// Path to the initializatoin file.
        /// </summary>
        public string InitFile
        {
            get
            {
                return initFile;
            }
            set
            {
                initFile = value;
            }
        }

        private string storageFile;
        
        /// <summary>
        /// Path to the file storing values for memory.
        /// </summary>
        public string StorageFile
        {
            get
            {
                return storageFile;
            }
        }

        private IMemoryObserver observer;

        /// <summary>
        /// Observer following all changes.
        /// </summary>
        public IMemoryObserver Observer
        {
            get
            {
                return observer;
            }
            set
            {
                observer = value;
            }
        }

        private bool observe;

        /// <summary>
        /// Indicating whether every change of memory should be notified to observer.
        /// </summary>
        public bool Observe
        {
            get
            {
                return observe;
            }
            set
            {
                observe = value;
            }
        }

        /// <summary>
        /// Indexers for write to memory and read from memory.
        /// </summary>
        /// <param name="address">
        /// Address to read or to write from.
        /// </param>
        /// <returns>
        /// Value from wanted address.
        /// </returns>
        public byte[] this[uint address]
        {
            get
            {
                FileStream fs = new FileStream(storageFile, FileMode.Open);
                try
                {
                    fs.Seek(address - fs.Position, SeekOrigin.Current);
                    byte[] ret = new byte[auSize];
                    for (int i = 0; i < auSize; i++)
                    {
                        ret[i] = (byte)(fs.ReadByte());
                    }
                    return ret;
                }
                finally 
                {
                    fs.Close();
                }
                
            }
            set
            {
                FileStream fs = new FileStream(storageFile, FileMode.Open);
                try
                {
                    fs.Seek(address - fs.Position, SeekOrigin.Current);
                    byte[] ret = new byte[auSize];
                    for (int i = 0; i < auSize; i++)
                    {
                        fs.WriteByte(value[i]);
                    }
                    free[address] = false;
                    if (observer != null && observe == true)
                    {
                        SignalLocationChange(address, value);
                    }
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        public override string ArcDirectoryName
        {
            get { return "Memories/"; }
        }

        protected override string[] MenuItems
        {
            get
            {
                return new[]
                {
                    MemoryDump, Copy, Remove
                };
            }
        }

        public Memory(string projectPath) : base(projectPath) { }

        /// <summary>
        /// Delegate used for notifying observer that some location was changed.
        /// </summary>
        /// <param name="address">
        /// Address of the changed location.
        /// </param>
        /// <param name="newValue">
        /// New value of the changed value.
        /// </param>
        private delegate void LocationChangeNotifier(uint address, byte[] newValue);

        /// <summary>
        /// Notifies observer that some location was changed.
        /// </summary>
        /// <param name="address">
        /// Address of the changed location.
        /// </param>
        /// <param name="newValue">
        /// New value of the changed location.
        /// </param>
        private void SignalLocationChange(uint address, byte[] newValue)
        {
            try
            {
                if (((MemoryDumpForm)observer).InvokeRequired == true)
                {
                    LocationChangeNotifier d = new LocationChangeNotifier(SignalLocationChange);
                    ((MemoryDumpForm)observer).BeginInvoke(d, new object[] { address, newValue });
                }
                else
                {
                    observer.LocationChanged(address, newValue);
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.txt", ex.ToString());
            }
        }

        /// <summary>
        /// Gets the address of the first free block in memory.
        /// </summary>
        /// <param name="length">
        /// Length of the required block.
        /// </param>
        /// <returns>
        /// Address of the required block or -1 if there is not enough free space. 
        /// </returns>
        public int FirstFree(int length)
        {
            for (int i = 0; i < size; i++)
            {
                bool ok = true;
                for (int j = i; j < i + length; j++)
                {
                    if (free[j] == false)
                        ok = false;
                }
                if (ok == true)
                    return i;
            }
            return -1;
        }

        public int FirstFreeInRAM(int length)
        {
            for (uint i = ramStart; i < size; i++)
            {
                bool ok = true;
                for (uint j = i; j < i + length; j++)
                {
                    if (free[j] == false)
                        ok = false;
                }
                if (ok == true)
                    return (int)i;
            }
            return -1;
        }

        /// <summary>
        /// Allocates memory.
        /// </summary>
        /// <param name="address">
        /// Starting address.
        /// </param>
        /// <param name="length">
        /// Length in bytes to allocate.
        /// </param>
        /// <returns>
        /// Bool value that indicates wheter allocation was successfull. 
        /// </returns>
        public bool Allocate(Int64 address, int length)
        {
            if (length + address > size)
                return false;
            for (Int64 i = address; i < address + length; i++)
            {
                if (free[i] == false)
                    return false;
            }
            for (Int64 i = address; i < address + length; i++)
            {
                free[i] = false;
            }
            return true;
        }

        /// <summary>
        /// Memory dump.
        /// </summary>
        public void dump()
        {
            
        }

        /// <summary>
        /// Initializes memory from init file.
        /// </summary>
        public void Initialize()
        {
            free = new bool[size];
            for (int i = 0; i < size; i++)
            {
                free[i] = true;
            }
            string[] lines = null;
            if (initFile != null && File.Exists(initFile))
            {
                lines = File.ReadAllLines(initFile);
            }
            storageFile = dataFolder + name + id + ".mem";
            Dictionary<uint, byte[]> map = new Dictionary<uint, byte[]>();
            for (int i = 0; lines != null && i < lines.Length; i++)
            {
                string[] words = lines[i].Split(' ', '\t');
                uint address = 0;
                if (words[0].StartsWith("0x") || words[0].StartsWith("0X"))
                {
                    address = Convert.ToUInt32(words[0], 16);
                }
                else if (words[0].StartsWith("0b") || words[0].StartsWith("0B"))
                {
                    address = Convert.ToUInt32(words[0], 2);
                }
                else if (words[0].StartsWith("0o") || words[0].StartsWith("0O"))
                {
                    address = Convert.ToUInt32(words[0], 8);
                }
                else
                {
                    address = Convert.ToUInt32(words[0], 10);
                }
                byte[] val = new byte[auSize];
                for (int j = 1; j <= auSize; j++)
                {
                    if (words[j].StartsWith("0x") || words[j].StartsWith("0X"))
                    {
                        val[j - 1] = Convert.ToByte(words[j].Substring(2, words[j].Length - 2), 16);
                    }
                    else if (words[j].StartsWith("0b") || words[j].StartsWith("0B"))
                    {
                        val[j - 1] = Convert.ToByte(words[j].Substring(2, words[j].Length - 2), 2);
                    }
                    else if (words[j].StartsWith("0o") || words[j].StartsWith("0O"))
                    {
                        val[j - 1] = Convert.ToByte(words[j].Substring(2, words[j].Length - 2), 8);
                    }
                    else
                    {
                        val[j - 1] = Convert.ToByte(words[j], 10);
                    }
                }
                map.Add(address, val);
            }
            File.Delete(storageFile);
            for (uint i = 0; i < size; i++)
            {
                if (map.ContainsKey(i))
                {
                    for (int k = 0; k < auSize; k++)
                    {
                        File.AppendAllText(storageFile, "" + (char)(map[i][k]));
                        free[k + i] = false;
                    }
                }
                else
                {
                    for (int k = 0; k < auSize; k++)
                    {
                        File.AppendAllText(storageFile, "\0");
                    }
                }
            }
        }

        private string dataFolder;

        /// <summary>
        /// Loads memory from file.
        /// </summary>
        /// <param name="arcFile">
        /// Path to the file with the description.
        /// </param>
        /// <param name="dataFolder">
        /// Path to the data folder of the application.
        /// </param>
        /// <returns>
        /// Number of errors during loading.
        /// </returns>
        public override int Load(string arcFile, string dataFolder)
        {
            this.ArcFile = arcFile;
            this.dataFolder = dataFolder;
            this.observer = null;
            int errorCount = 0;
            string content = File.ReadAllText(arcFile);
            XmlReader xmlReader = XmlReader.Create(new StringReader(content));
            XmlDocument doc = new XmlDocument();
            XmlNode head = doc.ReadNode(xmlReader);
            foreach (XmlNode node in head.ChildNodes)
            {
                switch (node.Name)
                {
                    case "name":
                        name = node.InnerText;
                        break;
                    case "filename":
                        FileName = node.InnerText;
                        break;
                    case "size":
                        size = Convert.ToInt32(node.InnerText);
                        break;
                    case "au":
                        auSize = Convert.ToInt32(node.InnerText);
                        break;
                    case "ram_start":
                        ramStart = Convert.ToUInt32(node.InnerText);
                        break;
                    case "ram_end":
                        ramEnd = Convert.ToUInt32(node.InnerText); ;
                        break;
                    case "rom_start":
                        romStart = Convert.ToUInt32(node.InnerText);
                        break;
                    case "rom_end":
                        romEnd = Convert.ToUInt32(node.InnerText);
                        break;
                    case "init_file":
                        initFile = dataFolder + node.InnerText;
                        break;
                    case "dimensions":
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            if (!child.Name.Equals("#whitespace"))
                            {
                                switch (child.Name.ToLower())
                                {
                                    case "height":
                                        this.Height = Convert.ToInt32(child.InnerText);
                                        break;
                                    case "width":
                                        this.Width = Convert.ToInt32(child.InnerText);
                                        break;
                                }
                            }
                        }
                        break;
                    case "ports":
                        int portErrorCount = 0;
                        foreach (XmlNode port in node.ChildNodes)
                        {
                            if (!port.Name.Equals("#whitespace"))
                            {
                                Port newPort = new Port(port.Name, this);
                                foreach (XmlNode portChild in port.ChildNodes)
                                {
                                    switch (portChild.Name)
                                    {
                                        case "name":
                                            newPort.Name = portChild.InnerText.Trim();
                                            break;
                                        case "number":
                                            newPort.Size = Convert.ToInt32(portChild.InnerText);
                                            break;
                                        case "side":
                                            string innerText = portChild.InnerText.ToLower().Trim();
                                            if (!(innerText.Equals("left") || innerText.Equals("right") ||
                                                innerText.Equals("up") || innerText.Equals("down")))
                                            {
                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Port side can only take values left, right, up or down.\n");
                                                portErrorCount++;
                                            }
                                            else
                                            {
                                                switch (innerText)
                                                {
                                                    case "left":
                                                        newPort.PortPosition = Position.LEFT;
                                                        break;
                                                    case "right":
                                                    default:
                                                        newPort.PortPosition = Position.RIGHT;
                                                        break;
                                                    case "up":
                                                        newPort.PortPosition = Position.UP;
                                                        break;
                                                    case "down":
                                                        newPort.PortPosition = Position.DOWN;
                                                        break;
                                                }
                                            }
                                            break;
                                        case "type":
                                            innerText = portChild.InnerText.ToLower().Trim();
                                            if (!(innerText.Equals("in") || innerText.Equals("out") || innerText.Equals("inout")))
                                            {

                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Port type can only take values in, out or inout.\n");
                                                portErrorCount++;
                                            }
                                            else
                                            {
                                                switch (innerText)
                                                {
                                                    case "in":
                                                        newPort.PortType = Kind.IN;
                                                        break;
                                                    case "out":
                                                        newPort.PortType = Kind.OUT;
                                                        break;
                                                    case "inout":
                                                    default:
                                                        newPort.PortType = Kind.INOUT;
                                                        break;
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (portErrorCount == 0)
                                {
                                    newPort.InitializePins();
                                    ports.AddLast(newPort);
                                }
                            }
                        }
                        break;
                    case "design":
                        errorCount += ProcessDesignNode(node);
                        break;
                    default:
                        break;
                }
            } 
            if (size < 0)
            {
                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Memory size must be specified.\n");
                errorCount++;
            }
            if (FileName == null)
            {
                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: File name must be specified. \n");
                errorCount++;
            }
            else if (auSize <= 0)
            {
                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Addressible unit size in memory must be specified.\n");
                errorCount++;
            }
            if (errorCount == 0)
            {
                if (!File.Exists(dataFolder + "Memories/" + FileName))
                {
                    var file = File.Create(dataFolder + "Memories/" + FileName);
                    file.Close();
                    const string methodBody = @"
// This is auto-generated code.
// Please, edit only method body.

public static void Cycle(Memory memory)
{
    // Define how memory behaves during one cycle.
}
";
                    File.WriteAllText(dataFolder + "Memories/" + FileName, methodBody);
                }
                errorCount += CompileCode(dataFolder);
                Initialize();
                observer = new MemoryDumpForm(this);
            }
            return errorCount;
        }

        protected override void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case MemoryDump:
                    ((MemoryDumpForm)observer).Show();
                    break;
                default: 
                    base.MenuItemClicked(sender, e);
                    break;
            }
            
        }

        public override object Clone()
        {
            Memory newMemory = new Memory(ProjectPath);
            newMemory.DesignCompileResults = DesignCompileResults;
            newMemory.results = results;
            newMemory.auSize = this.auSize;
            newMemory.size = this.size;
            newMemory.ramEnd = this.ramEnd;
            newMemory.ramStart = this.ramStart;
            newMemory.romEnd = this.romEnd;
            newMemory.romStart = this.romStart;
            newMemory.observer = this.observer;
            newMemory.Height = this.Height;
            newMemory.Width = this.Width;
            newMemory.name = this.name;
            newMemory.initFile = this.initFile;
            newMemory.dataFolder = this.dataFolder;
            newMemory.ArcFile = this.ArcFile;
            newMemory.ports = new LinkedList<Port>();
            newMemory.ports.Clear();
            foreach (Port port in ports)
            {
                var newPort = (Port)port.Clone();
                newMemory.ports.AddLast(newPort);
                newPort.Component = newMemory;
                newMemory.GetAllPins().ForEach(p => newMemory.Controls.Add(p));
            }

            newMemory.Initialize();
            return newMemory;
        }
    }
}
