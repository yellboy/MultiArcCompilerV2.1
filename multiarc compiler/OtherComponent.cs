using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.CSharp;
using MoreLinq;

namespace MultiArc_Compiler
{
    public class OtherComponent : NonCPUComponent
    {
        private string dataFolder;
        private RegistersForm observer;
        private ArchConstants constants = new ArchConstants();

        public override string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ArcDirectoryName
        {
            get { return "Other/"; }
        }

        public OtherComponent(string projectPath) : base(projectPath) { }

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
                    case "registers":
                        var list = node.ChildNodes;
                        int registerErrorCount = 0;
                        foreach (XmlNode n in list)
                        {
                            if (n.Name != "#whitespace")   
                            {
                                Register r = new Register(0, null) {Size = 0, Val = 0, Group = null};
                                XmlNodeList children = n.ChildNodes;
                                foreach (XmlNode child in children)
                                {
                                    switch (child.Name)
                                    {
                                        case "size":
                                            r.Size = Convert.ToInt32(child.InnerText);
                                            break;
                                        case "name":
                                            r.AddName(child.InnerText);
                                            break;
                                        case "group":
                                            r.Group = child.InnerText;
                                            break;
                                        case "value":
                                            r.Val = Convert.ToInt32(child.InnerText);
                                            break;
                                        case "part":
                                            XmlNodeList part = child.ChildNodes;
                                            Register partReg = new Register(0, observer);
                                            partReg.Size = 0;
                                            partReg.BaseReg = r;
                                            partReg.Group = null;
                                            partReg.Start = -1;
                                            partReg.End = -1;
                                            foreach (XmlNode partParameter in part)
                                            {
                                                switch (partParameter.Name)
                                                {
                                                    case "start":
                                                        partReg.Start = Convert.ToInt32(partParameter.InnerText);
                                                        break;
                                                    case "end":
                                                        partReg.End = Convert.ToInt32(partParameter.InnerText);
                                                        break;
                                                    case "group":
                                                        partReg.Group = partParameter.InnerText;
                                                        break;
                                                    case "name":
                                                        partReg.AddName(partParameter.InnerText);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                            if (partReg.Start == -1)
                                            {
                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + n.Name + "'s part start must be specified.\n");
                                                registerErrorCount++;
                                            }
                                            if (partReg.End == -1)
                                            {
                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + n.Name + "'s part end must be specified.\n");
                                                registerErrorCount++;
                                            }
                                            if (partReg.Names.Count == 0)
                                            {
                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + n.Name + "'s part name(s) must be specified.\n");
                                                registerErrorCount++;
                                            }
                                            if (registerErrorCount == 0)
                                            {
                                                partReg.Size = partReg.End - partReg.Start + 1;
                                                r.AddPart(partReg);
                                                constants.AddRegister(partReg);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (r.Size == 0)
                                {
                                    Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + n.Name + "'s size must be specified.\n");
                                    registerErrorCount++;
                                }
                                if (r.Names.Count == 0)
                                {
                                    Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + n.Name + "'s name(s) must be specified.\n");
                                    registerErrorCount++;
                                }
                                if (registerErrorCount == 0)
                                {
                                    constants.AddRegister(r);
                                }
                            }
                        }
                        observer = new RegistersForm(constants);
                        errorCount += registerErrorCount;
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
            if (FileName == null)
            {
                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: File name must be specified. \n");
                errorCount++;
            }
            if (errorCount == 0)
            {
                if (!File.Exists(dataFolder + "Other/" + FileName))
                {
                    var file = File.Create(dataFolder + "Other/" + FileName);
                    file.Close();
                    const string methodBody = @"
// This is auto-generated code.
// Please, edit only method body.

public static void Cycle(OtherComponent component)
{
    // Define how this component behaves during one cycle.
}
";
                    File.WriteAllText(dataFolder + "Other/" + FileName, methodBody);
                }
                errorCount += CompileCode(dataFolder);
                observer = new RegistersForm(constants);
            }
            return errorCount;
        }

        public Register GetRegister(string name)
        {
            return constants.GetRegister(name);
        }

        public override object Clone()
        {
            var newComponent = new OtherComponent(ProjectPath);
            newComponent.results = results;
            newComponent.observer = this.observer;
            newComponent.Height = this.Height;
            newComponent.Width = this.Width;
            newComponent.name = this.name;
            newComponent.DrawnFromCode = this.DrawnFromCode;
            newComponent.dataFolder = this.dataFolder;
            newComponent.ArcFile = this.ArcFile;
            newComponent.ports = new LinkedList<Port>();
            newComponent.ports.Clear();
            
            if (HasNonPortControls)
            {
                foreach (var c in Controls)
                {
                    var controlWithImage = c as ControlWithImage;

                    if (controlWithImage != null)
                    {
                        newComponent.Controls.Add((ControlWithImage)controlWithImage.Clone());
                    }
                }
            }
            
            foreach (Port port in ports)
            {
                var newPort = (Port)port.Clone();
                newPort.Component = newComponent;
                newComponent.ports.AddLast(newPort);
                newComponent.GetAllPins().ForEach(p => newComponent.Controls.Add(p));
            }

            return newComponent;
        }
    }
}
