/* 
 * File: CPU.cs
 * Author: Bojan Jelaca
 * Date: October 2014
 */

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Microsoft.CSharp;

namespace MultiArc_Compiler
{

    [Serializable]
    /// <summary>
    /// Represents central processing unit.
    /// </summary>
    public class CPU : SystemComponent
    {

        private new string name;

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

        public override string ArcDirectoryName
        {
            get { return "CPUs/"; }
        }

        private ArchConstants constants = new ArchConstants();

        /// <summary>
        /// Architecture constants.
        /// </summary>
        public ArchConstants Constants
        {
            get
            {
                return constants;
            }
            set
            {
                constants = value;
            }
        }

        private Executor executor;

        private IRegistersObserver registersObserver;

        public IRegistersObserver Observer
        {
            get
            {
                return registersObserver;
            }
            set
            {
                registersObserver = value;
            }
        }

        private bool wasDrawn = false;

        private CompilerResults results;

        public CPU(string projectPath) : base(projectPath) { }

        /// <summary>
        /// Loads architecture from file.
        /// </summary>
        /// <param name="arcFile">
        /// File with the description of architecture.
        /// </param>
        /// <param name=param name="dataFolder">
        /// Path to data folder of the application.
        /// </param>
        /// <returns>
        /// Number of errors occured while loading architecture from file.
        /// </returns>
        public override int Load(string arcFile, string dataFolder)
        {
            int errorCount = 0;
            this.ArcFile = arcFile;
            constants.RemoveAllInstructions();
            constants.RemoveAllAddressingModes();
            constants.RemoveAllDataTypes();
            constants.RemoveAllMnemonics();
            constants.RemoveAllRegisters();
            constants.ClearTokens();
            ports.Clear();
            string content = File.ReadAllText(arcFile);
            XmlReader xmlReader = XmlReader.Create(new StringReader(content));
            XmlDocument doc = new XmlDocument();
            XmlNode head = doc.ReadNode(xmlReader); 
            bool registersSpecified = false;
            registersObserver = null;
            foreach (XmlNode node in head.ChildNodes)
            {
                XmlNodeList list = null;
                switch (node.Name)
                {
                    case "name":
                        this.name = node.InnerText;
                        break;
                    case "filename":
                        this.FileName = node.InnerText;
                        break;
                    case "instruction_mnemonics":
                        foreach (XmlNode name in node.ChildNodes)
                            if (name.Name.Equals("name"))
                                constants.AddMnemonic(name.InnerText);
                        break;
                    case "user_tokens":
                        foreach (XmlNode token in node.ChildNodes)
                        {
                            if (!token.Name.Equals("#whitespace"))
                            {
                                constants.AddToken(token.Name, token.InnerText);
                            }
                        }
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
                                                    case "right": default:
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
                                                    case "inout": default:
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
                        errorCount += portErrorCount;
                        break;
                    case "data":
                        list = node.ChildNodes;
                        foreach (XmlNode n in list)
                        {
                            if (!n.Name.Equals("#whitespace"))
                            {
                                constants.AddDataType(n.Name, Convert.ToInt32(n.InnerText));
                            }
                        }
                        break;
                    case "memory":
                        int memoryErrorCount = 0;
                        Program.Mem = new Memory(_projectPath);
                        Program.Mem.Name = this.name + "_memory";
                        foreach (XmlNode mem in node.ChildNodes)
                        {
                            switch (mem.Name)
                            {
                                case "size":
                                    Program.Mem.Size = Convert.ToInt32(mem.InnerText);
                                    break;
                                case "au":
                                    Program.Mem.AuSize = Convert.ToInt32(mem.InnerText);
                                    break;
                                case "ram_start":
                                    Program.Mem.RamStart = Convert.ToUInt32(mem.InnerText);
                                    break;
                                case "ram_end":
                                    Program.Mem.RamEnd = Convert.ToUInt32(mem.InnerText);
                                    break;
                                case "rom_start":
                                    Program.Mem.RomStart = Convert.ToUInt32(mem.InnerText);
                                    break;
                                case "rom_end":
                                    Program.Mem.RomEnd = Convert.ToUInt32(mem.InnerText);
                                    break;
                                case "init_file":
                                    Program.Mem.InitFile = dataFolder + mem.InnerText;
                                    break;
                                default:
                                    break;
                            }
                        } 
                        Program.Mem.Observer = new MemoryDumpForm(Program.Mem);
                        if (Program.Mem.Size < 0)
                        {
                            Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Memory size must be specified.\n");
                            memoryErrorCount++;
                        }
                        else if (Program.Mem.AuSize <= 0)
                        {
                            Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Addressible unit size in memory must be specified.\n");
                            memoryErrorCount++;
                        }
                        if (memoryErrorCount == 0)
                        {
                            Program.Mem.Initialize();
                        }
                        errorCount += memoryErrorCount;
                        break;
                    case "registers":
                        list = node.ChildNodes;
                        registersSpecified = true;
                        int registerErrorCount = 0;
                        foreach (XmlNode n in list)
                        {
                            switch (n.Name)
                            {
                                case "general_purpose":
                                    {
                                        string prefix = "R";
                                        string group = "";
                                        int size = 0;
                                        int number = 0;
                                        int value = 0;
                                        XmlNodeList children = n.ChildNodes;
                                        foreach (XmlNode child in children)
                                        {
                                            switch (child.Name)
                                            {
                                                case "number":
                                                    number = Convert.ToInt32(child.InnerText);
                                                    break;
                                                case "size":
                                                    size = Convert.ToInt32(child.InnerText);
                                                    break;
                                                case "prefix":
                                                    prefix = child.InnerText;
                                                    break;
                                                case "value":
                                                    value = Convert.ToInt32(child.InnerText);
                                                    break;
                                                case "name":
                                                    group = child.InnerText;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        if (group == "")
                                        {
                                            Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: General purpose registers' group name must be specified.\n");

                                            registerErrorCount++;
                                        }
                                        if (number == 0)
                                        {
                                            Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: General purpose registers' number must be specified.\n");

                                            registerErrorCount++;
                                        }
                                        if (size == 0)
                                        {
                                            Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: General purpose registers' size must be specified.\n");

                                            registerErrorCount++;
                                        }
                                        if (registerErrorCount == 0)
                                        {
                                            for (int i = 0; i < number; i++)
                                            {
                                                Register r = new Register(size, registersObserver);
                                                r.Size = size;
                                                r.AddName(prefix + i);
                                                r.Val = value;
                                                r.Group = group;
                                                constants.AddRegister(r);
                                            }
                                        }
                                    }
                                    break;
                                case "#whitespace":
                                    break;
                                default:
                                    {
                                        Register r = new Register(0, null);
                                        r.Size = 0;
                                        r.Val = 0;
                                        r.Group = null;
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
                                                    Register partReg = new Register(0, registersObserver);
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
                                    break;
                            }
                        }
                        if (constants.GetRegister("pc") == null)
                        {
                            Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: PC register must be specified.\n");
                            registerErrorCount++;
                        }
                        registersObserver = new RegistersForm(constants);
                        errorCount += registerErrorCount;
                        break;
                    case "addressing_modes":
                        {
                            int amErrorCount = 0;
                            XmlNodeList modes = node.ChildNodes;
                            foreach (XmlNode mode in modes)
                            {
                                if (!mode.Name.Equals("#whitespace"))
                                {
                                    amErrorCount = 0;
                                    AddressingMode am = new AddressingMode();
                                    XmlNodeList children = mode.ChildNodes;
                                    foreach (XmlNode child in children)
                                    {
                                        switch (child.Name)
                                        {
                                            case "name":
                                                am.Name = child.InnerText;
                                                break;
                                            case "file":
                                                am.FileName = dataFolder + "CPUs\\" + child.InnerText;
                                                break;
                                            case "result":
                                                am.Result = constants.GetDataType(child.InnerText);
                                                break;
                                            case "expression_value":
                                                {
                                                    string exp = "";
                                                    int val = 0;
                                                    foreach (XmlNode valNode in child.ChildNodes)
                                                    {
                                                        switch (valNode.Name)
                                                        {
                                                            case "expression":
                                                                exp = valNode.InnerText;
                                                                break;
                                                            case "value":
                                                                val = Convert.ToInt32(valNode.InnerText);
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    am.AddValue(exp.ToLower(), val);
                                                }
                                                break;
                                            case "operand":
                                                if (child.HasChildNodes)
                                                {
                                                    foreach (XmlNode bin in child.ChildNodes)
                                                    {
                                                        switch (bin.InnerText)
                                                        {
                                                            case "user_defined":
                                                                am.OperandValueDefinedByUser = true;
                                                                break;
                                                            case "read_from_expression":
                                                                am.OperandReadFromExpression = true;
                                                                break;
                                                            case "read_from_values":
                                                                am.OperandInValues = true;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "operand_type":
                                                am.OperandType = child.InnerText;
                                                break;
                                            case "expression":
                                                if (child.HasChildNodes)
                                                {
                                                    string exp = "";
                                                    LinkedList<string> groups = new LinkedList<string>();
                                                    XmlNodeList expressions = child.ChildNodes;
                                                    foreach (XmlNode expression in expressions)
                                                    {
                                                        switch (expression.Name)
                                                        {
                                                            case "registers_group":
                                                                am.OperandReadFromExpression = false;
                                                                am.OperandInValues = true;
                                                                groups.AddLast(expression.InnerText);
                                                                exp += expression.InnerText;
                                                                break;
                                                            case "#whitespace":
                                                                break;
                                                            default:
                                                                {
                                                                    string semiExp = expression.InnerText;
                                                                    int index = 0;
                                                                    while (semiExp[index] == '\t' || semiExp[index] == '\n')
                                                                    {
                                                                        index++;
                                                                    }
                                                                    semiExp = semiExp.Substring(index, semiExp.Length - index);
                                                                    index = semiExp.Length - 1;
                                                                    while (semiExp[index] == '\t' || semiExp[index] == '\n')
                                                                    {
                                                                        index--;
                                                                    }
                                                                    semiExp = semiExp.Substring(0, index + 1);
                                                                    exp += semiExp;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    int lastVal = 0;
                                                    if (groups.Count != 0)
                                                    {
                                                        foreach (string group in groups)
                                                        {
                                                            for (int i = 0; i < constants.NUM_OF_REGISTERS; i++)
                                                            {
                                                                Register r = constants.GetRegister(i);
                                                                if (r.Group != null && r.Group.ToLower().Equals(group.ToLower()))
                                                                {

                                                                    while (am.Values.ContainsValue(lastVal))
                                                                        lastVal++;
                                                                    for (int j = 0; j < r.Names.Count; j++)
                                                                    {
                                                                        string expToAdd = "";
                                                                        int ind = exp.IndexOf(r.Group);
                                                                        expToAdd = exp.Replace(r.Group, r.Names.ElementAt(j));
                                                                        am.AddExpression(expToAdd);
                                                                        string[] expParts = expToAdd.Split('"', ' ');
                                                                        expToAdd = "";
                                                                        for (int c = 0; c < expParts.Length; c++)
                                                                        {
                                                                            expToAdd += expParts[c];
                                                                        }
                                                                        if (!am.Values.ContainsKey(expToAdd.ToLower()))
                                                                        {
                                                                            am.Values.Add(expToAdd.ToLower(), lastVal);
                                                                        }
                                                                    }
                                                                    lastVal++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        am.AddExpression(exp);
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    if (am.Name == null)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + mode.Name + "'s name must be specified.\n");

                                        amErrorCount++;
                                    }
                                    if (am.FileName == null)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + mode.Name + "'s file must be specified.\n");

                                        amErrorCount++;
                                    }
                                    if (am.OperandInValues == false && am.OperandReadFromExpression == false && am.OperandValueDefinedByUser == false)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + mode.Name + "'s operand must be specified.\n");

                                        amErrorCount++;
                                    }
                                    if (am.OperandType == null)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + mode.Name + "'s operand type must be specified.\n");

                                        amErrorCount++;
                                    }
                                    else if (!am.OperandType.Equals("relative") && !am.OperandType.Equals("absolute"))
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + mode.Name + "'s operand type can only be 'absolute' or 'relative'.\n");

                                        amErrorCount++;
                                    }
                                    if (am.Expressions.Count == 0 && am.Values.Count == 0)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + mode.Name + "'s expression must be specified.\n");

                                        amErrorCount++;
                                    }
                                    if (amErrorCount == 0)
                                    {
                                        constants.AddAddressingMode(am);
                                        string contents =
@"/* 
*This is auto-generated text. 
*Please, edit only method bodies. 
*/

public static void getAddrData_" + am.Name + @"(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int startBit, int endBit, ref int result)
{
// TODO Write how this addressing mode gets operand here.
// It this method is not necessary, just leave it like this.
}

public static void setAddrData_" + am.Name + @"(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int startBit, int endBit, int data)
{
// TODO Write how this addressing mode stores operand here.
// It this method is not necessary, just leave it like this.
}

public static void getOperand_" + am.Name + @"(string image, int currentLocation, int relativeValue, int absoluteValue, ref int operand)
{
// TODO Write how this addressing mode gets operand value from instruction assembly code here.
// If this method is not necessary, just leave it like this.
}";
                                        if (!File.Exists(am.FileName))
                                        {
                                            var file = File.Create(am.FileName);
                                            file.Close();
                                            File.WriteAllText(am.FileName, contents);
                                        }
                                    }
                                    errorCount += amErrorCount;
                                }
                            }
                        }
                        break;
                    case "instructions":
                        {
                            int instErrorCount = 0;
                            foreach (XmlNode instruction in node.ChildNodes)
                            {
                                if (!instruction.Name.Equals("#whitespace"))
                                {
                                    Instruction i = new Instruction();
                                    i.Name = instruction.Name;
                                    i.StartBit = -1;
                                    i.EndBit = -1;
                                    i.Size = -1;
                                    bool opcodeSpecified = false;
                                    foreach (XmlNode child in instruction.ChildNodes)
                                    {
                                        switch (child.Name)
                                        {
                                            case "size":
                                                i.Size = Convert.ToInt32(child.InnerText);
                                                break;
                                            case "opcode":
                                                XmlNodeList opCodeList = child.ChildNodes;
                                                int size = 0;
                                                int value = -1;
                                                opcodeSpecified = true;
                                                foreach (XmlNode opCodeNode in opCodeList)
                                                {
                                                    switch (opCodeNode.Name)
                                                    {
                                                        case "start_bit":
                                                            i.StartBit = Convert.ToInt32(opCodeNode.InnerText);
                                                            break;
                                                        case "end_bit":
                                                            i.EndBit = Convert.ToInt32(opCodeNode.InnerText);
                                                            break;
                                                        case "value":
                                                            value = Convert.ToInt32(opCodeNode.InnerText, 2);
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                }
                                                if (i.StartBit == -1)
                                                {
                                                    Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + instruction.Name + "'s start bit of opcode must be specified.\n");

                                                    instErrorCount++;
                                                }
                                                if (i.EndBit == -1)
                                                {
                                                    Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + instruction.Name + "'s end bit of opcode must be specified.\n");

                                                    instErrorCount++;
                                                }
                                                if (value == -1)
                                                {
                                                    Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + instruction.Name + "'s value of opcode must be specified.\n");

                                                    instErrorCount++;
                                                }
                                                if (instErrorCount == 0)
                                                {
                                                    size = 1;
                                                    for (int j = i.StartBit; j >= i.EndBit; j--)
                                                    {
                                                        if (j % 8 == 0 && j != i.EndBit)
                                                        {
                                                            size++;
                                                        }
                                                    }
                                                    i.Mask = new byte[size];
                                                    int count = i.StartBit - i.EndBit;
                                                    int byteCount = size - 1;
                                                    for (int j = i.StartBit; j >= i.EndBit; j--)
                                                    {
                                                        int semiValue = (value & (1 << count)) << i.EndBit % 8;
                                                        i.Mask[size - 1 - byteCount] |= (byte)((semiValue & (1 << (i.EndBit % 8 + count))) >> byteCount * 8); // This might be a problem.
                                                        if ((i.EndBit + count) % 8 == 0)
                                                            byteCount--;
                                                        count--;
                                                    }
                                                }
                                                break;
                                            case "mnemonic":
                                                i.Mnemonic = child.InnerText;
                                                break;
                                            case "file":
                                                i.FileName = dataFolder + "CPUs\\" + child.InnerText;
                                                break;
                                            case "arguments":
                                                {

                                                    XmlNodeList args = child.ChildNodes;
                                                    foreach (XmlNode arg in args)
                                                    {
                                                        if (arg.Name.Equals("arg"))
                                                        {
                                                            Argument a = new Argument();
                                                            bool addressingModeSpecified = false;
                                                            foreach (XmlNode argNode in arg.ChildNodes)
                                                            {
                                                                switch (argNode.Name)
                                                                {
                                                                    case "type":
                                                                        a.Type = argNode.InnerText;
                                                                        break;
                                                                    case "addressing_mode":
                                                                        {
                                                                            int codeStart = -1;
                                                                            int codeEnd = -1;
                                                                            int operandStart = -1;
                                                                            int operandEnd = -1;
                                                                            int codeValue = -1;
                                                                            addressingModeSpecified = true;
                                                                            string name = null;
                                                                            XmlNodeList amNodes = argNode.ChildNodes;
                                                                            foreach (XmlNode amNode in amNodes)
                                                                            {
                                                                                switch (amNode.Name)
                                                                                {
                                                                                    case "name":
                                                                                        name = amNode.InnerText;
                                                                                        break;
                                                                                    case "opcode":
                                                                                        {
                                                                                            codeStart = -1;
                                                                                            codeEnd = -1;
                                                                                            XmlNodeList opcodeList = amNode.ChildNodes;
                                                                                            foreach (XmlNode opcodeNode in opcodeList)
                                                                                            {
                                                                                                switch (opcodeNode.Name)
                                                                                                {
                                                                                                    case "start_bit":
                                                                                                        codeStart = Convert.ToInt32(opcodeNode.InnerText);
                                                                                                        break;
                                                                                                    case "end_bit":
                                                                                                        codeEnd = Convert.ToInt32(opcodeNode.InnerText);
                                                                                                        break;
                                                                                                    case "value":
                                                                                                        codeValue = Convert.ToInt32(opcodeNode.InnerText, 2);
                                                                                                        break;
                                                                                                    default:
                                                                                                        break;
                                                                                                }
                                                                                            }
                                                                                            if (codeEnd == -1)
                                                                                            {
                                                                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument code start bit must be specified, if opcode is specified.\n");

                                                                                                instErrorCount++;
                                                                                            }
                                                                                            if (codeStart == -1)
                                                                                            {
                                                                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument code end bit must be specified, if opcode is specified.\n");

                                                                                                instErrorCount++;
                                                                                            }
                                                                                            if (codeValue == -1)
                                                                                            {
                                                                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument code value must be specified, if opcode is specified.\n");

                                                                                                instErrorCount++;
                                                                                            }

                                                                                        }
                                                                                        break;
                                                                                    case "operand":
                                                                                        {
                                                                                            XmlNodeList operandList = amNode.ChildNodes;
                                                                                            operandEnd = -1;
                                                                                            operandStart = -1;
                                                                                            foreach (XmlNode operandNode in operandList)
                                                                                            {
                                                                                                switch (operandNode.Name)
                                                                                                {
                                                                                                    case "start_bit":
                                                                                                        operandStart = Convert.ToInt32(operandNode.InnerText);
                                                                                                        break;
                                                                                                    case "end_bit":
                                                                                                        operandEnd = Convert.ToInt32(operandNode.InnerText);
                                                                                                        break;
                                                                                                    default:
                                                                                                        break;
                                                                                                }
                                                                                            }

                                                                                        }
                                                                                        if (operandEnd == -1)
                                                                                        {
                                                                                            Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument operand start bit must be specified, if operand is specified.\n");

                                                                                            instErrorCount++;
                                                                                        }
                                                                                        if (operandStart == -1)
                                                                                        {
                                                                                            Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument operand end bit must be specified, if operand is specified.\n");

                                                                                            instErrorCount++;
                                                                                        }
                                                                                        break;
                                                                                    default:
                                                                                        break;
                                                                                }
                                                                            }
                                                                            if (name == null)
                                                                            {
                                                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument addressing mode name must be specified.\n");

                                                                                instErrorCount++;
                                                                            }
                                                                            if (constants.GetAddressingMode(name) == null)
                                                                            {
                                                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument addressing mode must be previously created.\n");

                                                                                instErrorCount++;
                                                                            }
                                                                            if (instErrorCount == 0)
                                                                            {
                                                                                a.AddAddressingMode(constants.GetAddressingMode(name), codeValue, codeStart, codeEnd, operandStart, operandEnd);
                                                                            }
                                                                        }
                                                                        break;
                                                                    default:
                                                                        break;
                                                                }
                                                            }
                                                            if (a.Type == null)
                                                            {
                                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument's type must be specified.\n");

                                                                instErrorCount++;
                                                            }
                                                            else if (!a.Type.Equals("src") && !a.Type.Equals("dst"))
                                                            {
                                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument's type must be either 'src' or 'dst'.\n");

                                                                instErrorCount++;
                                                            }
                                                            if (addressingModeSpecified == false)
                                                            {
                                                                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Every " + instruction.Name + "'s argument's addressing mode must be specified.\n");

                                                                instErrorCount++;
                                                            }
                                                            if (instErrorCount == 0)
                                                            {
                                                                i.AddArgument(a);
                                                            }
                                                        }

                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    if (i.Size == -1)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + instruction.Name + "'s size must be specified.\n");

                                        instErrorCount++;
                                    }
                                    if (opcodeSpecified == false)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + instruction.Name + "'s opcode must be specified.\n");

                                        instErrorCount++;
                                    }
                                    if (i.Mnemonic == null)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + instruction.Name + "'s mnemonic must be specified.\n");

                                        instErrorCount++;
                                    }
                                    if (!constants.Mnemonics.Contains(i.Mnemonic))
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + instruction.Name + "'s mnemonic must be listed in mnemonics list.\n");

                                        instErrorCount++;
                                    }
                                    if (i.FileName == null)
                                    {
                                        Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: " + instruction.Name + "'s file must be specified.\n");

                                        instErrorCount++;
                                    }
                                    constants.AddInstruction(i);
                                    if (instErrorCount == 0)
                                    {
                                        string contents =
@"/* 
*This is auto-generated text. 
*Please, edit only method bodies. 
*/

public static void execute_" + i.Mnemonic.ToLower() + @"(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int[] operands, ref int[] result)
{	
// TODO Write how this instruction executes here.
// If this method is not necessary just leave it like this.
}";
                                        if (!File.Exists(i.FileName))
                                        {
                                            var file = File.Create(i.FileName);
                                            file.Close();
                                            File.WriteAllText(i.FileName, contents);
                                        }
                                    }
                                }
                            }
                            errorCount += instErrorCount;
                        }
                        break;
                    case "design":
                        ProcessDesignNode(node);
                        break;
                    default:
                        break;
                }
            }
            if (this.name == null)
            {
                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Architecture must have a name.\n");

                errorCount++;
            }
            if (this.FileName == null)
            {
                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: File name must be specified");
            }
            if (constants.Mnemonics == null || constants.Mnemonics.Count == 0)
            {
                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Mnemonics must be specified.\n");

                errorCount++;
            }
            if (registersSpecified == false)
            {
                Form1.Instance.AddToOutput(DateTime.Now.ToString() + " ERROR: Registers must be specified.\n");

                errorCount++;
            }
            if (errorCount == 0)
            {
                if (!File.Exists(dataFolder + "CPUs/" + FileName))
                {
                    string methodBodies =
@"
// This is auto-generated code. Please, edit only method bodies.
    
public static byte[] ReadFromMemory(CPU cpu, uint address)
{
    // Define how CPU reads from memory here. 
    // If this method is not needed, just leave it empty.
    return null;
}

public static void WriteToMemory(CPU cpu, uint address, byte[] value)
{
    // Define how CPU writtes to memory here.
    // If this method is not needed, just leave it empty.
}";
                    var file = File.Create(dataFolder + "CPUs/" + FileName);
                    file.Close();
                    File.WriteAllText(dataFolder + "CPUs/" + FileName, methodBodies);
                }
                errorCount = CompileCode(dataFolder);
            }
            return errorCount;
        }

        public override int CompileCode(string dataFolder)
        {
            string methodBodies = File.ReadAllText(dataFolder + "CPUs/" + FileName);
            string executableCode =
@"
using System;
using System.IO;
using MultiArc_Compiler;

public class DynamicClass" + name + @"
{
" + methodBodies + @"
}";
            var provider = CSharpCodeProvider.CreateProvider("c#");
            var options = new CompilerParameters();
            var assemblyContainingNotDynamicClass = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            options.ReferencedAssemblies.Add(assemblyContainingNotDynamicClass);
            var assemblyContaningForms = Assembly.GetAssembly(typeof(Control)).Location;
            options.ReferencedAssemblies.Add(assemblyContaningForms);
            var assemblyContainingComponent = Assembly.GetAssembly(typeof(Component)).Location;
            options.ReferencedAssemblies.Add(assemblyContainingComponent);
            results = provider.CompileAssemblyFromSource(options, new[] { executableCode });
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    Form1.Instance.AddToOutput(DateTime.Now.ToString() + "Error in " + FileName + ": " + error.ErrorText + " in line " + (error.Line - 8) + ".\n");
                }
            }
            return results.Errors.Count;
        }

        /// <summary>
        /// Draws component.
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            if (wasDrawn == false)
            {
                wasDrawn = true;
            }
        }

        protected override string[] MenuItems 
        {
            get
            {
                return new[]
                {
                    "Registers",
                    "Memory dump",
                    "Remove"
                };
            }
        }

        protected override void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Registers":
                    (this.registersObserver as RegistersForm).Show();
                    break;
                case "Memory dump":
                    (Program.Mem.Observer as MemoryDumpForm).Show();
                    break;
                case "Remove":
                    base.MenuItemClicked(sender, e);
                    break;
            }
        }

        public override object Clone()
        {
            return this;
        }

        /// <summary>
        /// Reads one word from memory.
        /// </summary>
        /// <param name="address">
        /// Address of the word.
        /// </param>
        /// <returns>
        /// Wanted word.
        /// </returns>
        public byte[] ReadFromMemory(uint address)
        {
            var t = results.CompiledAssembly.GetType("DynamicClass" + name);
            object[] parameters = new object[] { this, address };
            return (byte[])t.GetMethod("ReadFromMemory").Invoke(null, parameters);
        }
    }
}