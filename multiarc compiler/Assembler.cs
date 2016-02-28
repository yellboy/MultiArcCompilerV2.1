/*
 * File: Assembler.cs
 * Author: Bojan Jelaca
 * Date: October 2013
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PerCederberg.Grammatica.Runtime;
using PerCederberg.Grammatica.Runtime.RE;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.Reflection;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    /// <summary>
    /// Two pass assembler.
    /// </summary>
    public class Assembler
    {
        /// <summary>
        /// Contains binary constants that will be used to make binary code.
        /// </summary>
        private CPU cpu;

        public CPU Cpu
        {
            get
            {
                return cpu;
            }
            set
            {
                cpu = value;
            }
        }

        /// <summary>
        /// Path to the file with code.
        /// </summary>
        private string codePath;

        public string CodePath
        {
            get
            {
                return codePath;
            }
            set
            {
                codePath = value;
            }
        }

        /// <summary>
        /// String that contains assembly code.
        /// </summary>
        private string code;

        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        /// <summary>
        /// Data memory.
        /// </summary>
        private Memory memory;

        /// <summary>
        /// List that represents symbol table.
        /// </summary>
        private LinkedList<Symbol> symbolTable;

        private LinkedList<Node> instructions = new LinkedList<Node>();

        private Dictionary<Node, string> labels = new Dictionary<Node, string>();

        /// <summary>
        /// Binary code that assemler generates.
        /// </summary>
        private byte[] binaryCode;

        public byte[] BinaryCode
        {
            get
            {
                return binaryCode;
            }
        }

        /// <summary>
        /// Number of bytes in binary code.
        /// </summary>
        private int count;

        public int Count
        {
            get
            {
                return count;
            }
        }



        /// <summary>
        /// List that contains informations about positions in binary code where are instruction separators.
        /// </summary>
        private LinkedList<int> separators;
        
        /// <summary>
        /// Output represented with TextBoxBase.
        /// </summary>
        private TextBoxBase output;

        public LinkedList<int> Separators
        {
            get
            {
                return separators;
            }
        }

        private int origin;

        /// <summary>
        /// Entry point of the program.
        /// </summary>
        public int Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="codePath">
        /// String with full path of the file that contains assembly code.
        /// </param>
        /// <param name="cpu">
        /// CPU for which code is written.
        /// </param>
        /// <param name="output">
        /// Output represented as TextBox.
        /// </param>
        public Assembler(string codePath, CPU cpu, TextBoxBase output)
        {
            this.cpu = cpu;
            this.codePath = codePath;
            this.code = File.ReadAllText(codePath);
            this.output = output;
            symbolTable = new LinkedList<Symbol>(); // Creates empty symbol table.
            separators = new LinkedList<int>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">
        /// String array that contains code.
        /// </param>
        /// <param name="cpu">
        /// CPU for which code is written.
        /// </param>
        /// <param name="output">
        /// Output represented as TextBox.
        /// </param>
        public Assembler(string code, CPU cpu, Memory memory, TextBoxBase output)
        {
            this.memory = memory;
            this.cpu = cpu;
            this.code = code;
            this.codePath = "NO PATH";
            this.output = output;
            symbolTable = new LinkedList<Symbol>(); // Creates emtpy symbol table.
            separators = new LinkedList<int>();
        }

        /// <summary>
        /// Assembles the given code.
        /// </summary>
        /// <param name="outputPath">
        /// Path to output file.
        /// </param>
        /// <returns>
        /// Binary code of assembled program.
        /// </returns>
        public byte[] Assemble(string outputPath = "out.o")
        {
            try
            {
                if (!code.EndsWith("\n"))
                {
                    code += "\r\n";
                }
                var provider = CSharpCodeProvider.CreateProvider("c#");
                var options = new CompilerParameters();
                var assemblyContainingNotDynamicClass = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
                options.ReferencedAssemblies.Add(assemblyContainingNotDynamicClass);
                string parserCode = File.ReadAllText("Grammar/cs/" + ((SystemComponent)cpu).Name + "Parser.cs");
                string constantsCode = File.ReadAllText("Grammar/cs/" + ((SystemComponent)cpu).Name + "Constants.cs");
                string tokenizerCode = File.ReadAllText("Grammar/cs/" + ((SystemComponent)cpu).Name + "Tokenizer.cs");
                string analyzerCode = File.ReadAllText("Grammar/cs/" + ((SystemComponent)cpu).Name + "Analyzer.cs");
                var results = provider.CompileAssemblyFromSource(options, new[] { parserCode, constantsCode, tokenizerCode, analyzerCode });
                if (results.Errors.Count > 0)
                {
                    foreach (var error in results.Errors)
                    {
                        File.AppendAllText("error.txt", error + "\n");
                    }
                    return null;
                }
                else
                {
                    Parser parser = null;
                    var t = results.CompiledAssembly.GetType("MultiArc_Compiler." + cpu.Name + "Parser", true);
                    object instance = Activator.CreateInstance(t, new[] { new StringReader(code) });
                    parser = (Parser)instance;
                    try
                    {
                        Node n = parser.Parse();
                        instructions.Clear();
                        labels.Clear();
                        symbolTable.Clear();
                        getInstructionsFromTree(n);
                        getOriginFromTree(n);
                        binaryCode = new byte[instructions.Count * cpu.Constants.MAX_BYTES]; // THIS MUST BE TESTED.
                        count = 0;
                        separators.Clear();
                        separators = new LinkedList<int>();
                        // First passing:
                        for (int i = 0; i < instructions.Count; i++)
                        {
                            Instruction inst = cpu.Constants.GetInstruction(instructions.ElementAt(i).GetName());
                            if (labels.ContainsKey(instructions.ElementAt(i)))
                            {
                                Symbol symbol = new Symbol(labels[instructions.ElementAt(i)], 0, count, true);
                                if (symbolTable.Contains(symbol))
                                {
                                    throw new Exception("Label already defined");
                                }
                                else
                                {
                                    symbolTable.AddLast(symbol);
                                }
                            }
                            for (int j = inst.Mask.Length - 1; j >= 0; j--)
                            {
                                binaryCode[count + j] = inst.Mask[j];
                            }
                            LinkedList<AddressingMode> addrModes = new LinkedList<AddressingMode>();
                            LinkedList<int> argumentsIndexes = new LinkedList<int>();
                            for (int j = 0; j < instructions.ElementAt(i).GetChildCount(); j++)
                            {
                                AddressingMode am = null;
                                am = cpu.Constants.GetAddressingMode(instructions.ElementAt(i).GetChildAt(j).Name);
                                if (!object.ReferenceEquals(am, null))
                                {
                                    addrModes.AddLast(am);
                                    argumentsIndexes.AddLast(j);
                                }
                            }
                            for (int j = 0; j < inst.Arguments.Count; j++)
                            {
                                int argcodeValue = inst.Arguments.ElementAt(j).CodeValues[addrModes.ElementAt(j).Name];
                                int argcodeStart = inst.Arguments.ElementAt(j).CodeStarts[addrModes.ElementAt(j).Name];
                                int argcodeEnd = inst.Arguments.ElementAt(j).CodeEnds[addrModes.ElementAt(j).Name];
                                int argcodeSize = 1;
                                for (int k = argcodeStart; k >= argcodeEnd; k--)
                                {
                                    if (k % 8 == 0 && k != argcodeEnd)
                                    {
                                        argcodeSize++;
                                    }
                                }
                                int argcodeCount = argcodeStart - argcodeEnd;
                                int byteCount = argcodeSize - 1;               
                                for (int k = argcodeStart; k >= argcodeEnd; k--)
                                {
                                    int semiValue = (argcodeValue & (1 << argcodeCount)) << argcodeEnd % 8;
                                    binaryCode[count + inst.Size - 1 - argcodeEnd / 8 - byteCount] |= (byte)((semiValue & (1 << (argcodeEnd % 8 + argcodeCount))) >> byteCount * 8); // This might be a problem.
                                    if ((argcodeEnd + argcodeCount) % 8 == 0)
                                        byteCount--;
                                    argcodeCount--;
                                }
                                AddressingMode am = addrModes.ElementAt(j);
                                int argumentIndex = argumentsIndexes.ElementAt(j);
                                int operandValue = 0;
                                bool shouldBeWritten = false;
                                if (am.OperandInValues)
                                {
                                    shouldBeWritten = true;
                                    string expr = "";
                                    if (instructions.ElementAt(i).GetChildAt(argumentIndex).GetChildAt(0) is Production)
                                    {
                                        expr = ((Production)instructions.ElementAt(i).GetChildAt(argumentIndex).GetChildAt(0)).Name;
                                    }
                                    else
                                    {
                                        for (int l = 0; l < instructions.ElementAt(i).GetChildAt(argumentIndex).GetChildCount(); l++)
                                            expr += ((Token)instructions.ElementAt(i).GetChildAt(argumentIndex).GetChildAt(l)).Image;
                                    }
                                    if (am.Values.ContainsKey(expr.ToLower()))
                                    {
                                        operandValue = am.Values[expr.ToLower()];
                                    }
                                }
                                else if (am.OperandReadFromExpression)
                                {
                                    Node node = (instructions.ElementAt(i).GetChildAt(argumentIndex));
                                    int childCount = node.GetChildCount();
                                    Node child = node;
                                    int sign = 1;
                                    while (!(child.GetChildAt(0) is Token))
                                    {
                                        child = child.GetChildAt(0);
                                    }
                                    for (int l = 0; l < node.GetChildCount() && !child.Name.Equals("DEC_NUMBER") && !child.Name.Equals("HEX_NUMBER") && !child.Name.Equals("OCT_NUMBER") && !child.Name.Equals("BIN_NUMBER") && !child.Name.Equals("IDENTIFIER"); l++)
                                    {
                                        child = node.GetChildAt(l);
                                        if (l != 0 && node.GetChildAt(l - 1).Name.Equals("SIGN"))
                                        {
                                            if (((Token)node.GetChildAt(l - 1)).Image.Equals("+"))
                                            {
                                                sign = 1;
                                            }
                                            else
                                            {
                                                sign = -1;
                                            }
                                        }
                                    }
                                    if (child.Name.Equals("DEC_NUMBER"))
                                    {
                                        operandValue = Convert.ToInt32(((Token)child).Image.ToLower()) * sign;
                                        shouldBeWritten = true;
                                    }
                                    else if (child.Name.Equals("HEX_NUMBER"))
                                    {
                                        operandValue = Convert.ToInt32(((Token)child).Image.ToLower().Substring(0, ((Token)child).Image.Length - 1), 16) * sign;
                                        shouldBeWritten = true;
                                    }
                                    else if (child.Name.Equals("OCT_NUMBER"))
                                    {
                                        operandValue = Convert.ToInt32(((Token)child).Image.ToLower().Substring(0, ((Token)child).Image.Length - 1), 8) * sign;
                                        shouldBeWritten = true;
                                    }
                                    else if (child.Name.Equals("BIN_NUMBER"))
                                    {
                                        operandValue = Convert.ToInt32(((Token)child).Image.ToLower().Substring(0, ((Token)child).Image.Length - 1), 2) * sign;
                                        shouldBeWritten = true;
                                    }
                                    else if (child.Name.Equals("IDENTIFIER"))
                                    {
                                        string label = ((Token)child).Image;
                                        operandValue = 0;
                                        foreach (Symbol s in symbolTable)
                                        {
                                            if (s.Label.ToLower().Equals(label))
                                            {
                                                operandValue = s.Offset;
                                                break;
                                            }
                                        }
                                        
                                    }
                                    if (am.OperandType.ToLower().Equals("relative"))
                                    {
                                        operandValue = operandValue - inst.Size - count;
                                    }
                                }
                                else if (am.OperandValueDefinedByUser)
                                {
                                    Node node = (instructions.ElementAt(i).GetChildAt(argumentIndex));
                                    int childCount = node.GetChildCount();
                                    Node child = node;
                                    bool labelNotYet = false;
                                    int sign = 1;
                                    while (!(child.GetChildAt(0) is Token))
                                    {
                                        child = child.GetChildAt(0);
                                    }
                                    for (int l = 0; l < node.GetChildCount() && !child.Name.Equals("DEC_NUMBER") && !child.Name.Equals("HEX_NUMBER") && !child.Name.Equals("OCT_NUMBER") && !child.Name.Equals("BIN_NUMBER") && !child.Name.Equals("IDENTIFIER"); l++)
                                    {
                                        child = node.GetChildAt(l); 
                                        if (l != 0 && node.GetChildAt(l - 1).Name.Equals("SIGN"))
                                        {
                                            if (((Token)node.GetChildAt(l - 1)).Image.Equals("+"))
                                            {
                                                sign = 1;
                                            }
                                            else
                                            {
                                                sign = -1;
                                            }
                                        }
                                    }
                                    if (child.Name.Equals("DEC_NUMBER"))
                                    {
                                        operandValue = Convert.ToInt32(((Token)child).Image.ToLower()) * sign;
                                    }
                                    else if (child.Name.Equals("HEX_NUMBER"))
                                    {
                                        operandValue = Convert.ToInt32(((Token)child).Image.ToLower().Substring(0, ((Token)child).Image.Length - 1), 16) * sign;
                                    }
                                    else if (child.Name.Equals("OCT_NUMBER"))
                                    {
                                        operandValue = Convert.ToInt32(((Token)child).Image.ToLower().Substring(0, ((Token)child).Image.Length - 1), 8) * sign;
                                    }
                                    else if (child.Name.Equals("BIN_NUMBER"))
                                    {
                                        operandValue = Convert.ToInt32(((Token)child).Image.ToLower().Substring(0, ((Token)child).Image.Length - 1), 2) * sign;
                                    }
                                    else if (child.Name.Equals("IDENTIFIER"))
                                    {
                                        string label = ((Token)child).Image;
                                        operandValue = 0;
                                        bool found = false;
                                        foreach (Symbol s in symbolTable)
                                        {
                                            if (s.Label.ToLower().Equals(label))
                                            {
                                                operandValue = s.Offset;
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (found == false)
                                        {
                                            labelNotYet = true;
                                        }
                                    }
                                    int relativeOperandValue = operandValue - inst.Size - count;
                                    string image = getExpression(instructions.ElementAt(i), "");
                                    if (labelNotYet == false)
                                    {
                                        operandValue = am.GetOperandValue(image, count + inst.Size, relativeOperandValue, operandValue);
                                        shouldBeWritten = true;
                                    }
                                }
                                if (shouldBeWritten == true)
                                {
                                    int operandStart = inst.Arguments.ElementAt(j).OperandStarts[am.Name];
                                    int operandEnd = inst.Arguments.ElementAt(j).OperandEnds[am.Name];
                                    int operandSize = 1;
                                    for (int k = operandStart; k >= operandEnd; k--)
                                    {
                                        if (k % 8 == 0 && k != operandEnd)
                                        {
                                            operandSize++;
                                        }
                                    }
                                    int operandCount = operandStart - operandEnd;
                                    byteCount = operandSize - 1;
                                    for (int k = operandStart; k >= operandEnd; k--)
                                    {
                                        int semiValue = (operandValue & (1 << operandCount)) << operandEnd % 8;
                                        binaryCode[count + inst.Size - 1 - operandEnd / 8 - byteCount] |= (byte)((semiValue & (1 << (operandEnd % 8 + operandCount))) >> byteCount * 8); // This might be a problem.
                                        if ((operandEnd + operandCount) % 8 == 0)
                                            byteCount--;
                                        operandCount--;
                                    }
                                }
                            }
                            separators.AddLast(count);
                            count += inst.Size;
                        }
                        separators.AddLast(count);
                        count = 0;
                        // Second passing:
                        for (int i = 0; i < instructions.Count; i++)
                        {
                            Instruction inst = cpu.Constants.GetInstruction(instructions.ElementAt(i).GetName());
                            LinkedList<AddressingMode> addrModes = new LinkedList<AddressingMode>();
                            LinkedList<int> argumentsIndexes = new LinkedList<int>();
                            for (int j = 0; j < instructions.ElementAt(i).GetChildCount(); j++)
                            {
                                AddressingMode am = null;
                                am = cpu.Constants.GetAddressingMode(instructions.ElementAt(i).GetChildAt(j).Name);
                                if (!object.ReferenceEquals(am, null))
                                {
                                    addrModes.AddLast(am);
                                    argumentsIndexes.AddLast(j);
                                }
                            }
                            for (int j = 0; j < inst.Arguments.Count; j++)
                            {
                                AddressingMode am = addrModes.ElementAt(j);
                                int argumentIndex = argumentsIndexes.ElementAt(j);
                                int operandValue = 0;
                                bool shouldBeOverwriten = false;
                                if (am.OperandReadFromExpression)
                                {
                                    Node node = (instructions.ElementAt(i).GetChildAt(argumentIndex));
                                    int childCount = node.GetChildCount();
                                    Node child = node;
                                    while (!(child.GetChildAt(0) is Token))
                                    {
                                        child = child.GetChildAt(0);
                                    }
                                    for (int l = 0; l < node.GetChildCount() && !child.Name.Equals("DEC_NUMBER") && !child.Name.Equals("HEX_NUMBER") && !child.Name.Equals("OCT_NUMBER") && !child.Name.Equals("BIN_NUMBER") && !child.Name.Equals("IDENTIFIER"); l++)
                                    {
                                        child = node.GetChildAt(l);
                                    }
                                    if (child.Name.Equals("IDENTIFIER"))
                                    {
                                        string label = ((Token)child).Image;
                                        operandValue = 0;
                                        shouldBeOverwriten = true;
                                        bool found = false;
                                        foreach (Symbol s in symbolTable)
                                        {
                                            if (s.Label.ToLower().Equals(label))
                                            {
                                                operandValue = s.Offset;
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (found == false)
                                        {
                                            operandValue = Program.Mem.FirstFreeInRAM(am.Result.Size / 8);
                                            if (operandValue == -1)
                                            {
                                                throw new Exception("Memory full");
                                            }
                                            Symbol newSymbol = new Symbol(label, 0, operandValue, true);
                                            symbolTable.AddLast(newSymbol);
                                            Program.Mem.Allocate(operandValue, am.Result.Size / 8);
                                        }
                                        
                                    }
                                    if (am.OperandType.ToLower().Equals("relative"))
                                    {
                                        operandValue = operandValue - inst.Size - count;
                                    }
                                }
                                else if (am.OperandValueDefinedByUser)
                                {
                                    Node node = (instructions.ElementAt(i).GetChildAt(argumentIndex));
                                    int childCount = node.GetChildCount();
                                    Node child = node;
                                    while (!(child.GetChildAt(0) is Token))
                                    {
                                        child = child.GetChildAt(0);
                                    }
                                    for (int l = 0; l < node.GetChildCount() && !child.Name.Equals("DEC_NUMBER") && !child.Name.Equals("HEX_NUMBER") && !child.Name.Equals("OCT_NUMBER") && !child.Name.Equals("BIN_NUMBER") && !child.Name.Equals("IDENTIFIER"); l++)
                                    {
                                        child = node.GetChildAt(l);
                                    }
                                    if (child.Name.Equals("IDENTIFIER"))
                                    {
                                        string label = ((Token)child).Image;
                                        bool found = false;
                                        shouldBeOverwriten = true;
                                        foreach (Symbol s in symbolTable)
                                        {
                                            if (s.Label.ToLower().Equals(label))
                                            {
                                                operandValue = s.Offset;
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (found == false)
                                        {
                                            operandValue = Program.Mem.FirstFree(Program.Mem.AuSize);
                                            Program.Mem.Allocate(operandValue, Program.Mem.AuSize); // This must be improved.
                                        }
                                    }
                                    int relativeOperandValue = operandValue - inst.Size - count;
                                    string image = getExpression(instructions.ElementAt(i), "");
                                    operandValue = am.GetOperandValue(image, count + inst.Size, relativeOperandValue, operandValue);
                                }
                                if (shouldBeOverwriten == true)
                                {
                                    int operandStart = inst.Arguments.ElementAt(j).OperandStarts[am.Name];
                                    int operandEnd = inst.Arguments.ElementAt(j).OperandEnds[am.Name];
                                    int operandSize = 1;
                                    for (int k = operandStart; k >= operandEnd; k--)
                                    {
                                        if (k % 8 == 0 && k != operandEnd)
                                        {
                                            operandSize++;
                                        }
                                    }
                                    int operandCount = operandStart - operandEnd;
                                    int byteCount = operandSize - 1;
                                    for (int k = operandStart; k >= operandEnd; k--)
                                    {
                                        int semiValue = (operandValue & (1 << operandCount)) << operandEnd % 8;
                                        binaryCode[count + inst.Size - 1 - operandEnd / 8 - byteCount] |= (byte)((semiValue & (1 << (operandEnd % 8 + operandCount))) >> byteCount * 8); // This might be a problem.
                                        if ((operandEnd + operandCount) % 8 == 0)
                                            byteCount--;
                                        operandCount--;
                                    }
                                }
                            }
                            count += inst.Size;
                        }
                        output.Text += DateTime.Now.ToString() + " Compile successfull.\n";
                        output.ScrollToCaret();
                        byte[] ret = new byte[count];
                        for (int i = 0; i < count; i++)
                        {
                            ret[i] = binaryCode[i];
                        }
                        for (int i = 0; i < count; i += Program.Mem.AuSize)
                        {
                            byte[] toWrite = new byte[Program.Mem.AuSize];
                            for (int j = 0; j < Program.Mem.AuSize && i + j < count; j++)
                            {
                                toWrite[j] = ret[j + i];
                            }
                            Program.Mem[(uint)(i + origin)] = toWrite;
                        }
                        return ret;
                    }
                    catch (ParserLogException ex)
                    {
                        string o = "Error(s) in code existed. Compile unsuccessfull.\r\nList of errors:\r\n";
                        for (int j = 0; j < ex.Count; j++)
                        {
                            ParseException pe = ex[j];
                            o += (j + 1) + ": Syntax error " + '\'' + pe.ErrorMessage + '\'' + " in line " + pe.Line + " and column " + pe.Column + "\n";
                        }
                        output.Text += DateTime.Now.ToString() + " " + o;
                        output.ScrollToCaret();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("error.txt", ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets origin (entry point) from parse tree, if it exists.
        /// </summary>
        /// <param name="n">
        /// Root node of the tree.
        /// </param>
        private void getOriginFromTree(Node n)
        {
            if (n.Name.ToLower().Equals("origin"))
            {
                origin = Convert.ToInt32(((Token)n.GetChildAt(1)).Image);
            }
            else
            {
                for (int i = 0; i < n.GetChildCount(); i++)
                {
                    getOriginFromTree(n.GetChildAt(i));
                }
            }
        }


        /// <summary>
        /// Recursive method that creates expression from node.
        /// </summary>
        /// <param name="node">
        /// Node for which expression needs to be calculated.
        /// </param>
        /// <param name="expression">
        /// Expression created by now.
        /// </param>
        /// <returns>
        /// Wanted expression.
        /// </returns>
        private string getExpression(Node node, string expression)
        {
            if (node is Token)
            {
                return expression + " " + ((Token)node).Image;
            }
            else
            {
                for (int i = 0; i < node.GetChildCount(); i++)
                {
                    expression = getExpression(node.GetChildAt(i), expression);
                }
                return expression;
            }
        }

        /// <summary>
        /// Tests if input is any kind of jump or branch operation code. OBSOLETE
        /// </summary>
        /// <param name="input">
        /// String with operation code to be tested.
        /// </param>
        /// <returns>
        /// True if it is jump or branch and false otherwise.
        /// </returns>
        private bool IsJump(string input)
        {
            return input.ToLower().Equals("jmp") | input.ToLower().Equals("jgr") | input.ToLower().Equals("jeq") | 
                input.ToLower().Equals("jls") | input.ToLower().Equals("jge") | input.ToLower().Equals("jle");
        }

        /// <summary>
        /// Adds new symbol to symbol table, if it is not already there.
        /// </summary>
        /// <param name="label">
        /// Label of symbol.
        /// </param>
        /// <param name="section">
        /// Section of symbol.
        /// </param>
        /// <param name="offset">
        /// Offset of symbol in its section.
        /// </param>
        /// <param name="local">
        /// Is symbol local?
        /// </param>
        private void AddToSymbolTable(string label, int section, short offset, bool local)
        {
            bool contains = false;
            foreach (Symbol s in symbolTable)
            {
                if (s.Label.Equals(label))
                {
                    contains = true;
                }
            }
            if (!contains)
            {
                symbolTable.AddLast(new Symbol(label, section, offset, local));
            }
        }

        /// <summary>
        /// Checks if input string can reference any register. OBSOLETE
        /// </summary>
        /// <param name="input">
        /// String to be checked.
        /// </param>
        /// <returns>
        /// Returns true if input string can reference register or false if it can not.
        /// </returns>
        private bool IsRegister(string input) 
        {
            if (!(input.StartsWith("R") || input.StartsWith("r")))
            {
                return false;
            }
            for (int i = 1; i < input.Length; i++)
            {
                if (!((input.ToCharArray())[i] >= '0' && (input.ToCharArray())[i] <= '9'))
                {
                    return false;
                }
            }
            if (Convert.ToInt64(input.Substring(1)) >= cpu.Constants.NUM_OF_REGISTERS)
                return false;
            return true;
        }

        /// <summary>
        /// Checks if input string can be literal. OBSOLETE
        /// </summary>
        /// <param name="input">
        /// Input string.
        /// </param>
        /// <returns>
        /// Returns true if input string can be literal or false if it can not.
        /// </returns>
        private bool IsLiteral(string input)
        {
            if (!(((input.ToCharArray())[0] >= 'a' && (input.ToCharArray())[0] <= 'z') ||
                ((input.ToCharArray())[0] >= 'A' && (input.ToCharArray())[0] <= 'Z')))
            {
                return false;
            }
            if (input.EndsWith(":"))
            {
                return false;
            }
            char[] arr = input.ToCharArray();
            for (int i = 1; i < arr.Length; i++)
            {
                if (!((arr[i] >= 'a' && arr[i] <= 'z')   ||
                        (arr[i] >= 'A' && arr[i] <= 'Z') ||
                        (arr[i] >= '0' && arr[i] <= '9') ||
                        arr[i] == '_'))
                    return false;
            }
            if (IsRegister(input))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets symbol with given label.
        /// </summary>
        /// <param name="label">
        /// Label of symbol.
        /// </param>
        /// <returns>
        /// Required symbol or null if it is not in symbol table.
        /// </returns>
        private Symbol GetSymbol(string label)
        {
            foreach (Symbol s in symbolTable)
            {
                if (s.Label.Equals(label))
                    return s;
            }
            return null;
        }


        /// <summary>
        /// Detects all the instruction nodes in parse tree and puts them into the instructions list.
        /// </summary>
        /// <param name="n">
        /// Root node of the parse tree.
        /// </param>
        private void getInstructionsFromTree(Node n)
        {
            for (int i = 0; i < n.GetChildCount(); i++)
            {
                if (n.Name.Equals("Instruction"))
                {
                    this.instructions.AddLast(n.GetChildAt(i));
                    if (n.Parent.GetChildAt(0).Name.Equals("IDENTIFIER") && n.Parent.GetChildAt(1).Name.Equals("COLON"))
                    {
                        labels.Add(n.GetChildAt(i), ((Token)n.Parent.GetChildAt(0)).Image);
                    }
                }
            }
            for (int i = 0; i < n.GetChildCount(); i++)
            {
                getInstructionsFromTree(n.GetChildAt(i));
            }
        }
    }
}
