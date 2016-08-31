using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public partial class Form1 : Form
    {

        private string arcFileName = null;

        public string ArcFileName
        {
            get
            {
                return arcFileName;
            }
        }

        /// <summary>
        /// Contants that represents whole architecture.
        /// </summary>
        private static ArchConstants constants = new ArchConstants();

        public static ArchConstants Constants
        {
            get
            {
                return constants;
            }
        }

        public static string openedFileName = null;

        public static string openedBinFileName = null;

        private byte[] binary;

        private LinkedList<int> separators;

        public static Form1 Instance = null;

        private LinkedList<int> breakPoints = new LinkedList<int>();

        private bool compiled = false;

        private Form registersForm;

        private Clipboard clipboard;

        private Form memoryForm;

        private CPU cpu;

        private UserSystem system;

        private LinkedList<SystemComponent> componentsList = new LinkedList<SystemComponent>();

        private string memoryFileName;

        private string otherComponentFileName;

        public static object LockObject = new object();

        public Form1()
        {
            InitializeComponent();
            Instance = this;
            CodeBox.AppendText("  ");
            CodeBox.Clear();
            designerToolStripMenuItem.Enabled = false;
            systemToolStripMenuItem.Enabled = false;
            loadToolStripMenuItem.Enabled = false;
            recompileCodeToolStripMenuItem.Enabled = false;
            registersToolStripMenuItem.Enabled = false;
            memoryDumpToolStripMenuItem.Enabled = false;
            executeToolStripMenuItem.Enabled = false;
            executeWithoutDebugToolStripMenuItem.Enabled = false;
            nextStepToolStripMenuItem.Enabled = false;
            LoadArcButton.Enabled = false;
            assembleToolStripMenuItem.Enabled = false;
            stopDebuggingToolStripMenuItem.Enabled = false;
            systemToolStripMenuItem.Enabled = false;
            DebugButton.Enabled = false;
            fileChanged = false;
            compiled = false;
            system = new UserSystem();
        }

        private int entryPoint;

        public void assembleButton_Click(object sender, EventArgs e)
        {
            Assemble();
        }

        public void Assemble()
        {
            Assembler asm = new Assembler(CodeBox.Text, cpu, Program.Mem, OutputBox);
            bool prevObserve = Program.Mem.Observe;
            Program.Mem.Observe = false;
            binary = asm.Assemble();
            Program.Mem.Observe = prevObserve;
            if (memoryForm != null && memoryForm.Visible == true)
            {
                memoryForm.Refresh();
            }
            entryPoint = asm.Origin;
            if (binary != null)
            {
                BinaryCodeBox.Text = "";
                separators = asm.Separators;
                long maxB = binary.Length / Program.Mem.AuSize + entryPoint;
                int count = 0;
                while (maxB != 0)
                {
                    count++;
                    maxB /= 10;
                }
                BinaryCodeBox.Text = String.Format("{0:D" + count + "}", entryPoint) + ":\t";
                for (int i = 0; i < asm.Count; i++)
                {
                    if (separators.Contains(i) && i != 0)
                    {
                        BinaryCodeBox.Text += "\n" + String.Format("{0:D" + count + "}", (i / Program.Mem.AuSize + entryPoint)) + ":\t";
                    }
                    string toAdd = BitConverter.ToString(binary, i, 1);
                    BinaryCodeBox.Text += toAdd; // Convert byte to hex string and write it to binary code box.
                }
                BinaryCodeBox.Text += "\n" + String.Format("{0:D" + count + "}", (asm.Count / Program.Mem.AuSize + entryPoint)) + ":\t";
                compiled = true;
            }
        }

        private bool fileChanged;

        private void LoadFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            openedFileName = LoadFileDialog.FileName;
            fileChanged = false;
            compiled = false;
            string[] code;
            code = File.ReadAllLines(LoadFileDialog.FileName);
            CodeBox.Text = "";
            breakPoints.Clear();
            for (int i = 0; i < code.Length; i++)
            {
                CodeBox.Text += code[i] + '\n';
            }
            fileChanged = false;
        }

        private void LoadFileBrowseButton_Click(object sender, EventArgs e)
        {
            LoadFileDialog.ShowDialog();
        }

        private void LoadFromFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                string[] code = new string[3];
                breakPoints.Clear();
                CodeBox.Text = "";
                for (int i = 0; i < code.Length; i++)
                {
                    CodeBox.Text += code[i] + '\n';
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("File not found! ");
            }
        }

        private void SaveFileAsButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog.ShowDialog();
        }

        private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllText(SaveFileDialog.FileName, CodeBox.Text);
            openedFileName = SaveFileDialog.FileName;
            FileNameLabel.Text = "File: " + SaveFileDialog.FileName;
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            if (openedFileName == null)
            {
                SaveFileDialog.ShowDialog();
            }
            else
            {
                File.WriteAllText(openedFileName, CodeBox.Text);
            }
        }

        private ArchConstants savedConstants = null;

        /// <summary>
        /// Saves current architecture constants so that they can be restored later if it is neccessary.
        /// </summary>
        public void SaveArchitecture()
        {
            savedConstants = (ArchConstants)(constants.Clone());
        }

        /// <summary>ite
        /// Restores old architecture constants.
        /// </summary>
        public void RestoreArchitecture()
        {
            constants = savedConstants;
        }


        private void LoadArchitecture(object sender, CancelEventArgs e)
        {
            int errorCount = 0;
            recompileCodeToolStripMenuItem.Enabled = true;
            string fileName;
            if (arcFileName == null || projectOpenning == false)
            {
                fileName = LoadArchitectureDialog.FileName;
            }
            else
            {
                fileName = arcFileName;
            }
            string content = File.ReadAllText(fileName);
            try
            {
                CPU oldCPU = cpu;
                cpu = new CPU(projectPath);
                errorCount = cpu.Load(fileName, dataFolder);
                if (errorCount == 0)
                {
                    if (oldCPU != null)
                    {
                        componentsList.Remove(oldCPU);
                    }
                    componentsList.AddLast(cpu);
                }
                else
                {
                    cpu = oldCPU;
                }
                registersForm = null;
                memoryForm = null;
            }
            catch (Exception ex)
            {
                OutputBox.Text += DateTime.Now.ToString() + " Error in architecture file: " + ex.Message + "\n";
                OutputBox.ScrollToCaret();
                File.AppendAllText("error.txt", ex.ToString());
                projectOpenning = false;
                memoryDumpToolStripMenuItem.Enabled = false;
                registersToolStripMenuItem.Enabled = false;
                assembleToolStripMenuItem.Enabled = false;
                executeToolStripMenuItem.Enabled = false;
                nextStepToolStripMenuItem.Enabled = false;
                executeWithoutDebugToolStripMenuItem.Enabled = false;
                stopDebuggingToolStripMenuItem.Enabled = false;
                DebugButton.Enabled = false;
                recompileCodeToolStripMenuItem.Enabled = false;
                return;
            }
            if (errorCount == 0)
            {
                prepareGrammarFile();
                foreach (AddressingMode am in cpu.Constants.AllAddressingModes)
                {
                    appendAMToGrammarFile(am);
                }
                foreach (Instruction inst in cpu.Constants.InstructionSet)
                {
                    appendInstructionToGrammarFile(inst);
                }
                addAllInstructionsToGrammarFile();
                try
                {
                    string command = "/C java -jar Grammar//grammatica-1.5.jar " + cpu.Name + ".grammar --csoutput grammar//cs --csnamespace MultiArc_Compiler --cspublic";
                    Process proc = new Process();
                    proc.StartInfo.FileName = "CMD.exe";
                    proc.StartInfo.Arguments = command;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.Start();
                    string line = "";
                    int count = 30;
                    while (!proc.StandardError.EndOfStream)
                    {
                        string l = proc.StandardError.ReadLine();
                        line += l + "\n";
                        count--;
                        if (count == 0)
                            break;
                    }
                    if (!line.Equals(""))
                    {
                        projectOpenning = false;
                        memoryDumpToolStripMenuItem.Enabled = false;
                        registersToolStripMenuItem.Enabled = false;
                        assembleToolStripMenuItem.Enabled = false;
                        executeToolStripMenuItem.Enabled = false;
                        nextStepToolStripMenuItem.Enabled = false;
                        executeWithoutDebugToolStripMenuItem.Enabled = false;
                        stopDebuggingToolStripMenuItem.Enabled = false;
                        DebugButton.Enabled = false;
                        recompileCodeToolStripMenuItem.Enabled = false;
                        OutputBox.Text += DateTime.Now.ToString() + " Error in grammar: " + line + "\n";
                        OutputBox.ScrollToCaret();
                    }
                    else
                    {
                        string grammar = File.ReadAllText(cpu.Name + ".grammar");
                        File.Delete(cpu.Name + ".grammar");
                        File.WriteAllText(dataFolder + cpu.Name + ".grammar", grammar);
                        bool compileSuccessful = true;
                        foreach (AddressingMode am in cpu.Constants.AllAddressingModes)
                        {
                            if (am.CompileCode(OutputBox) == false)
                            {
                                compileSuccessful = false;
                            }
                        }
                        foreach (Instruction i in cpu.Constants.InstructionSet)
                        {
                            if (i.CompileCode(OutputBox) == false)
                            {
                                compileSuccessful = false;
                            }
                        }
                        if (compileSuccessful == true)
                        {
                            if (projectOpenning == false)
                            {
                                if (!File.Exists(dataFolder + "CPUs\\" + fileName))
                                {
                                    var file = File.Create(dataFolder + "CPUs\\" + fileName.Substring(fileName.LastIndexOf('\\')));
                                    file.Close();
                                }
                                File.WriteAllText(dataFolder + "CPUs\\" + fileName.Substring(fileName.LastIndexOf('\\')), content);
                            }
                            arcFileName = dataFolder + fileName;
                            projectOpenning = false;
                            OutputBox.AppendText(DateTime.Now.ToString() + " CPU architecture loaded successfully. \n");
                            OutputBox.ScrollToCaret();
                            EnableAllButtons();
                            compiled = false;
                        }
                        else
                        {
                            projectOpenning = false;
                            DisableAllButtons();
                        }
                    }
                }
                catch (Exception ex)
                {
                    projectOpenning = false;
                    DisableAllButtons();
                    File.AppendAllText("error.txt", ex.ToString());
                }
            }
            else
            {
                DisableAllButtons();
                projectOpenning = false;
            }
            
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            Execute();
        }

        public void Execute()
        {
            if (compiled == false)
            {
                this.Assemble();
            }
            if (compiled == true)
            {
                system.DisableSelectingToAllComponents();
                system.StartWorking(separators, breakPoints, OutputBox, entryPoint, binary);
            }
        }

        public void ExecuteTickByTick()
        {
            if (compiled == false)
            {
                this.Assemble();
            }
            if (compiled == true)
            {
                if (!system.Executing)
                {
                    system.ResetToDefault();
                }

                system.StartWorkingTickByTick(separators, breakPoints, OutputBox, entryPoint, binary);
            }
        }

        private delegate void ExecutionStopedDelegate();

        public void ExecutionStoped()
        {
            if (this.InvokeRequired)
            {
                ExecutionStopedDelegate d = new ExecutionStopedDelegate(executionStoped);
                this.BeginInvoke(d);
            }
            else
            {
                executionStoped();
            }
        }

        private void executionStoped()
        {
            deselectAllLines();
            CodeBox.ReadOnly = false;
            CodeBox.BackColor = Color.White;
            assembleToolStripMenuItem.Enabled = true;
            loadToolStripMenuItem.Enabled = true;
            recompileCodeToolStripMenuItem.Enabled = true;
            LoadArcButton.Enabled = true;
            stopDebuggingToolStripMenuItem.Enabled = false;
        }

        public void ExecutionStarting()
        {
            stopDebuggingToolStripMenuItem.Enabled = true;
            CodeBox.ReadOnly = true;
            CodeBox.BackColor = Color.White;
            assembleToolStripMenuItem.Enabled = false;
            loadToolStripMenuItem.Enabled = false;
            recompileCodeToolStripMenuItem.Enabled = false;
            LoadArcButton.Enabled = false;
        }

        public void MarkInstruction(int number, Color color)
        {
            string[] lines = CodeBoxLinesWithInvokeRequired();
            int start = 0;
            for (int i = 0; i <= number; i++)
            {
                string temp = "";
                if (lines[i].Contains(";") && (lines[i].StartsWith(" ") || lines[i].StartsWith("\t") || lines[i].StartsWith(";")))
                {
                    int count;
                    for (count = 0; lines[i][count] != ';'; count++);
                    for (int j = count; j < lines[i].Length; j++)
                    {
                        temp += lines[i][j];
                    }
                }
                bool originFound = false;
                string[] words = lines[i].Split(' ', '\t');
                for (int k = 0; k < words.Length; k++)
                {
                    if (words[k].ToLower().Equals("org"))
                    {
                        originFound = true;
                    }
                }
                if (String.IsNullOrWhiteSpace(lines[i]) || String.IsNullOrEmpty(lines[i]) || temp.StartsWith(";") || originFound)
                {
                    number++;
                }
                if (i != number)
                {
                    start += lines[i].Length + 1;
                }
            }
            int length = lines[number].Length;
            CodeBoxSelectWithInvokeRequired(start, length, color);
            bool compiledOld = compiled;
            bool fileChangedOld = fileChanged;
            compiled = compiledOld;
            fileChanged = fileChangedOld;
        }

        private delegate string[] CodeBoxLinesDelegate();

        private string[] CodeBoxLinesWithInvokeRequired()
        {
            if (CodeBox.InvokeRequired)
            {
                CodeBoxLinesDelegate d = new CodeBoxLinesDelegate(CodeBoxLines);
                return d.Invoke();
            }
            return CodeBoxLines();
        }

        public string[] CodeBoxLines()
        {
            return CodeBox.Lines;
        }

        private delegate void CodeBoxSelectDelegate(int start, int length, Color color);

        private void CodeBoxSelectWithInvokeRequired(int start, int length, Color color)
        {
            if (CodeBox.InvokeRequired)
            {
                CodeBoxSelectDelegate d = new CodeBoxSelectDelegate(CodeBoxSelect);
                d.Invoke(start, length, color);
            }
            else
            {
                CodeBoxSelect(start, length, color);
            }
        }

        private bool colorChanging = false;

        private void CodeBoxSelect(int start, int length, Color color)
        {
            colorChanging = true;
            CodeBox.Select(start, length);
            CodeBox.SelectionBackColor = color;
            colorChanging = false;

        }

        private void memoryDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (memoryForm != null && memoryForm.Visible == false)
            //{
            //    memoryForm.Visible = true;
            //}
            ((MemoryDumpForm)Program.Mem.Observer).Visible = true;
            ((MemoryDumpForm)Program.Mem.Observer).Focus();
            //memoryForm.Focus();
            Program.Mem.Observe = true;
        }

        private void registersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (registersForm != null && registersForm.Visible == false)
            //{
            //    registersForm.Visible = true;
            //}
            if (cpu.Observer != null)
            {
                ((RegistersForm)cpu.Observer).Visible = true;
            }
            ((RegistersForm)cpu.Observer).Focus();
            
        }

        private void prepareGrammarFile()
        {
            try
            {
                string fileName = cpu.Name + ".grammar";
                File.Delete(fileName);
                string content = @"/* This is auto-generated text. Do not edit! */
%header%

GRAMMARTYPE = ""LL""
CASESENSITIVE = ""false""

%tokens%

";

                for (int i = 0; i < cpu.Constants.Tokens.Count; i++)
                {
                    content += cpu.Constants.Tokens.ElementAt(i).Key + " = " + '"' + cpu.Constants.Tokens.ElementAt(i).Value + '"' + '\n'; 
                }
                for (int i = 0; i < cpu.Constants.Mnemonics.Count; i++)
                {
                    string token = cpu.Constants.GetMnemonic(i);
                    content += token.ToUpper() + " = " + '"' + token + '"' + '\n';
                }
                for (int i = 0; i < cpu.Constants.NUM_OF_REGISTERS; i++)
                {
                    for (int j = 0; j < cpu.Constants.GetRegister(i).Names.Count; j++)
                    {
                        string token = cpu.Constants.GetRegister(i).Names.ElementAt(j);
                        content += token.ToUpper() + " = " + '"' + token + '"' + '\n';
                    }
                }
                content += @"
EQUALS = ""=""
LEFT_PAREN = ""(""
RIGHT_PAREN = "")""
HASH = ""#""
COLON = "":""
COMMA = "",""
ORG = ""org""

SIGN = <<[+-]>>
DEC_NUMBER = <<[0-9]+>>
BIN_NUMBER = <<[01]+[bB]>>
OCT_NUMBER = <<[0-8]+[oO]>>
HEX_NUMBER = <<[0-9a-f]+[hH]>>
IDENTIFIER = <<[a-z][a-z0-9_]*>>

ENTER = <<[\n\r]+>>
SINGLE_LINE_COMMENT = <<;.*>> %ignore%
WHITESPACE = <<[ \t]+>> %ignore%

%productions%

Program = [Separator] [Origin] Lines ;

Separator = ENTER (ENTER)* ;

Origin = ORG DEC_NUMBER Separator ;

Lines = Line (Line)* ;

Line = [IDENTIFIER "":""] Instruction Separator ;

";
                File.AppendAllText(fileName, content);
            }
            catch (Exception ex) 
            {
                File.WriteAllText("error.txt", ex.ToString());
            }
        }

        private void appendAMToGrammarFile(AddressingMode am)
        {
            try
            {
                string toAppend = am.Name.ToUpper() + " = ";
                for (int i = 0; i < am.Expressions.Count; i++)
                {
                    toAppend += am.Expressions.ElementAt(i) + (i == am.Expressions.Count - 1 ? " ;\n\n" : " | ");
                }
                File.AppendAllText(cpu.Name + ".grammar", toAppend);
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.txt", ex.ToString());
            }
        }

        private void appendInstructionToGrammarFile(Instruction i)
        {
            try
            {
                File.AppendAllText(cpu.Name + ".grammar", i.GetExpression() + "\n\n");
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.txt", ex.ToString());
            }
        }

        private void addAllInstructionsToGrammarFile()
        {
            try
            {
                string toAppend = "Instruction = ( ";
                for (int i = 0; i < cpu.Constants.InstructionSet.Count; i++)
                {
                    toAppend += cpu.Constants.InstructionSet.ElementAt(i).Name;
                    if (i == cpu.Constants.InstructionSet.Count - 1)
                    {
                        toAppend += " ) ;";
                    }
                    else
                    {
                        toAppend += " ) | ( ";
                    }
                }
                File.AppendAllText(cpu.Name + ".grammar", toAppend);
            }
            catch (Exception ex)
            {
                File.AppendAllText("error.txt", ex.ToString());
            }
        }

        public void AddToOutput(string text)
        {
            OutputBox.Text += DateTime.Now.ToString() + " " + text + "\n";
            OutputBox.ScrollToCaret();
        }

        private void clearOutputButton_Click(object sender, EventArgs e)
        {
            OutputBox.Text = "";
        }

        Executor ex;

        private void nextStep(object sender, EventArgs e)
        {
            if (compiled == false)
            {
                Assemble();
            }
            if (compiled == true)
            {
                if (ex == null || ex.Executing == false)
                {
                    system.ExecuteNextStep(separators, breakPoints, OutputBox, entryPoint, binary);
                    CodeBox.ReadOnly = true;
                    CodeBox.BackColor = Color.White;
                    deselectAllLines();
                    //selectLine(ex.Next);
                    assembleToolStripMenuItem.Enabled = false;
                    loadToolStripMenuItem.Enabled = false;
                    recompileCodeToolStripMenuItem.Enabled = false;
                    LoadArcButton.Enabled = false;
                    stopDebuggingToolStripMenuItem.Enabled = true;
                }
                //else
                //{
                //    //ex.WaitForOneInstruction();
                //    //if (ex.Executing == true)
                //    //{
                //    //    deselectAllLines();
                //    //    selectLine(ex.Next);
                //    //}
                //    //else
                //    //{
                //    //    deselectAllLines();
                //    //    CodeBox.ReadOnly = false;
                //    //    CodeBox.BackColor = Color.White;
                //    //    assembleToolStripMenuItem.Enabled = true;
                //    //    loadToolStripMenuItem.Enabled = true;
                //    //    recompileCodeToolStripMenuItem.Enabled = true;
                //    //    LoadArcButton.Enabled = true;
                //    //    stopDebuggingToolStripMenuItem.Enabled = false;
                //    //}
                //}
            }
        }

        private int start, length;

        private void selectLine(int number)
        {
            string[] lines = CodeBox.Lines;
            start = 0;
            for (int i = 0; i <= number; i++)
            {
                string temp = "";
                if (lines[i].Contains(";") && (lines[i].StartsWith(" ") || lines[i].StartsWith("\t") || lines[i].StartsWith(";")))
                {
                    int count;
                    for (count = 0; lines[i][count] != ';'; count++) ;
                    for (int j = count; j < lines[i].Length; j++)
                    {
                        temp += lines[i][j];
                    }
                }
                bool originFound = false;
                string[] words = lines[i].Split(' ', '\t');
                for (int k = 0; k < words.Length; k++)
                {
                    if (words[k].ToLower().Equals("org"))
                    {
                        originFound = true;
                    }
                }
                if (String.IsNullOrWhiteSpace(lines[i]) || String.IsNullOrEmpty(lines[i]) || temp.StartsWith(";") || originFound)
                {
                    number++;
                }
                if (i != number)
                {
                    start += lines[i].Length + 1;
                }
            }
            length = lines[number].Length;
            CodeBox.Select(start, length);
            bool compiledOld = compiled;
            bool fileChangedOld = fileChanged;
            CodeBox.SelectionBackColor = Color.Yellow;
            compiled = compiledOld;
            fileChanged = fileChangedOld;
        }

        private void deselectAllLines()
        {
            bool compiledOld;
            bool fileChangedOld;
            string[] lines = CodeBox.Lines;
            int start = 0;
            int emptyCount = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string temp = "";
                if (lines[i].Contains(";") && (lines[i].StartsWith(" ") || lines[i].StartsWith("\t") || lines[i].StartsWith(";")))
                {
                    int count;
                    for (count = 0; lines[i][count] != ';'; count++);
                    for (int j = count; j < lines[i].Length; j++)
                    {
                        temp += lines[i][j];
                    }
                }
                bool originFound = false;
                string[] words = lines[i].Split(' ', '\t');
                for (int k = 0; k < words.Length; k++)
                {
                    if (words[k].ToLower().Equals("org"))
                    {
                        originFound = true;
                    }
                }
                if (String.IsNullOrWhiteSpace(lines[i]) || String.IsNullOrEmpty(lines[i]) || temp.StartsWith(";") || originFound)
                {
                    emptyCount++;
                }
                else
                {
                    if (breakPoints.Contains(i - emptyCount))
                    {
                        CodeBox.Select(start, lines[i].Length);
                        compiledOld = compiled;
                        fileChangedOld = fileChanged;
                        colorChanging = true;
                        CodeBox.SelectionBackColor = Color.Red;
                        colorChanging = false;
                        compiled = compiledOld;
                        fileChanged = fileChangedOld;
                    }
                    else
                    {
                        CodeBox.Select(start, lines[i].Length);
                        compiledOld = compiled;
                        fileChangedOld = fileChanged;
                        CodeBox.SelectionBackColor = Color.White;
                        compiled = compiledOld;
                        fileChanged = fileChangedOld;
                    }
                }
                start += lines[i].Length + 1;
            }
            CodeBox.Select(start, length);
            compiledOld = compiled;
            fileChangedOld = fileChanged;
            CodeBox.SelectionBackColor = Color.White;
            compiled = compiledOld;
            fileChanged = fileChangedOld;
            CodeBox.DeselectAll();
        }

        [DllImport("user32.dll")]
        private static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);


        private void lineNumbers_For_RichTextBox1_Click(object sender, EventArgs e)
        {
            int minScroll;
            int maxScroll;
            GetScrollRange(CodeBox.Handle, 1, out minScroll, out maxScroll);
            Point rtfPoint = Point.Empty;
            SendMessage(CodeBox.Handle, 0x400 + 221, 0, ref rtfPoint);
            Point screenPos = new Point(MousePosition.X, MousePosition.Y);
            Point myPos = lineNumbers_For_RichTextBox2.PointToClient(screenPos);
            double height = (double)CodeBox.Height / 19.0;
            int line = (int)(Math.Truncate((myPos.Y + rtfPoint.Y) / height));
            if (line < CodeBox.Lines.Length - 1)
            {
                toggleBreakpoint(line);
            }
        }

        private void toggleBreakpoint(int line)
        {
            string[] lines = CodeBox.Lines; 
            string temp = "";
            if (lines[line].Contains(";") && (lines[line].StartsWith(" ") || lines[line].StartsWith("\t") || lines[line].StartsWith(";")))
            {
                int count;
                for (count = 0; lines[line][count] != ';'; count++) ;
                for (int j = count; j < lines[line].Length; j++)
                {
                    temp += lines[line][j];
                }
            }
            bool originFound = false;
            string[] words = lines[line].Split(' ', '\t');
            for (int k = 0; k < words.Length; k++)
            {
                if (words[k].ToLower().Equals("org"))
                {
                    originFound = true;
                }
            }
            if (!(String.IsNullOrWhiteSpace(lines[line]) || String.IsNullOrEmpty(lines[line]) || temp.StartsWith(";") || originFound))
            {
                int start = 0;
                for (int i = 0; i < line; i++)
                {
                    start += lines[i].Length + 1;
                }
                int length = lines[line].Length;
                CodeBox.Select(start, length);
                for (int i = 0; i < line; i++)
                {
                    string t = "";
                    if (lines[i].Contains(";") && (lines[i].StartsWith(" ") || lines[i].StartsWith("\t") || lines[i].StartsWith(";")))
                    {
                        int count;
                        for (count = 0; lines[i][count] != ';'; count++) ;
                        for (int j = count; j < lines[i].Length; j++)
                        {
                            t += lines[i][j];
                        }
                    } 
                    bool orgFound = false;
                    string[] w = lines[i].Split(' ', '\t');
                    for (int k = 0; k < w.Length; k++)
                    {
                        if (w[k].ToLower().Equals("org"))
                        {
                            orgFound = true;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(lines[i]) || String.IsNullOrEmpty(lines[i]) || lines[i].StartsWith(";") || t.StartsWith(";") || orgFound)
                    {
                        line--;
                    }
                }
                if (breakPoints.Contains(line))
                {
                    bool compiledOld = compiled;
                    bool fileChangedOld = fileChanged;
                    breakPoints.Remove(line);
                    CodeBox.SelectionBackColor = Color.White;
                    CodeBox.DeselectAll();
                    compiled = compiledOld;
                    fileChanged = fileChangedOld;
                }
                else
                {
                    bool compiledOld = compiled;
                    bool fileChangedOld = fileChanged;
                    breakPoints.AddLast(line);
                    colorChanging = true;
                    CodeBox.SelectionBackColor = Color.Red;
                    colorChanging = false;
                    CodeBox.DeselectAll();
                    compiled = compiledOld;
                    fileChanged = fileChangedOld;
                }
            }
        }

        private void executeWithoutDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteWithoutDebugging();
        }

        private void ExecuteWithoutDebugging()
        {
            if (compiled == false)
            {
                Assemble();
            }
            if (compiled == true)
            {
                if (ex == null || ex.Executing == false)
                {
                    assembleToolStripMenuItem.Enabled = false;
                    stopDebuggingToolStripMenuItem.Enabled = true;
                    loadToolStripMenuItem.Enabled = false;
                    recompileCodeToolStripMenuItem.Enabled = false;
                    LoadArcButton.Enabled = false;
                    ex = new Executor(cpu, system, separators, breakPoints, OutputBox, binary, entryPoint);
                    ex.Execute();
                    stopDebuggingToolStripMenuItem.Enabled = false;
                    loadToolStripMenuItem.Enabled = true;
                    recompileCodeToolStripMenuItem.Enabled = true;
                    LoadArcButton.Enabled = true;
                    assembleToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectDialog.InitialDirectory = Application.StartupPath + "\\Projects";
            NewProjectDialog.ShowDialog();
        }

        private string projectPath = null;

        private string projectName = null;

        private string dataFolder = null;

        private void NewProjectDialog_FileOk(object sender, CancelEventArgs e)
        {
            int lastIndex = NewProjectDialog.FileName.LastIndexOf('\\');
            projectPath = NewProjectDialog.FileName.Substring(0, lastIndex + 1);
            projectName = NewProjectDialog.FileName.Substring(lastIndex + 1);
            dataFolder = projectPath + "Data\\";
            string content = @"Time: " + DateTime.Now.ToString() + @"
Name: " + projectName + @"
";
            File.WriteAllText(NewProjectDialog.FileName, content);
            if (Directory.Exists(dataFolder))
            {
                Directory.Delete(dataFolder, true);
            }
            Directory.CreateDirectory(dataFolder);
            Directory.CreateDirectory(projectPath + "Images\\");
            Directory.CreateDirectory(projectPath + "System\\");
            Directory.CreateDirectory(dataFolder + "CPUs\\");
            Directory.CreateDirectory(dataFolder + "Memories\\");
            Directory.CreateDirectory(dataFolder + "Other\\");
            LoadArchitectureDialog.InitialDirectory = dataFolder + "CPUs\\";
            LoadMemoryDialog.InitialDirectory = dataFolder + "Memories\\";
            LoadOtherComponentDialog.InitialDirectory = dataFolder + "Other\\";
            LoadFileDialog.InitialDirectory = dataFolder;
            loadToolStripMenuItem.Enabled = true;
            LoadArcButton.Enabled = true;
        }

        private void CodeBox_TextChanged(object sender, EventArgs e)
        {
            if (!colorChanging)
            {
                fileChanged = true;
                compiled = false;
            }
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fileChanged == true)
            {
                new SaveOldFileForm(openedFileName, CodeBox);
            }
            else
            {
                ClearCode();
            }
        }

        public void ClearCode()
        {
            CodeBox.Clear();
            breakPoints.Clear();
            compiled = false;
            fileChanged = false;
        }

        private void projectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenProjectDialog.InitialDirectory = Application.StartupPath + "\\Projects";
            OpenProjectDialog.ShowDialog();
        }

        private bool projectOpenning = false;

        private void OpenProjectDialog_FileOk(object sender, CancelEventArgs e)
        {
            int lastIndex = OpenProjectDialog.FileName.LastIndexOf('\\');
            projectPath = OpenProjectDialog.FileName.Substring(0, lastIndex + 1);
            projectName = OpenProjectDialog.FileName.Substring(lastIndex + 1);
            dataFolder = projectPath + "Data\\";
            LoadArchitectureDialog.InitialDirectory = dataFolder;
            LoadFileDialog.InitialDirectory = dataFolder;
            loadToolStripMenuItem.Enabled = true;
            LoadArcButton.Enabled = true;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (SystemComponent c in componentsList)
            {
                c.DisposeAllImages();
            }

            string[] cpuFiles = Directory.GetFiles(dataFolder + "CPUs\\");
            if (cpuFiles != null)
            {
                foreach (string f in cpuFiles)
                {
                    if (f.ToLower().EndsWith(".arc"))
                    {
                        arcFileName = f;
                        projectOpenning = true;
                        registersForm = null;
                        LoadArchitecture(sender, e);
                    }
                }
            }
            string[] memoryFiles = Directory.GetFiles(dataFolder + "Memories\\");
            if (memoryFiles != null)
            {
                foreach (string f in memoryFiles)
                {
                    if (f.ToLower().EndsWith(".arc"))
                    {
                        memoryFileName = f;
                        projectOpenning = true;
                        registersForm = null;
                        LoadMemoryDialog_FileOk(sender, e);
                    }
                }
            }
            string[] otherFiles = Directory.GetFiles(dataFolder + "Other\\");
            if (otherFiles != null)
            {
                foreach (string f in otherFiles)
                {
                    if (f.ToLower().EndsWith(".arc"))
                    {
                        otherComponentFileName = f;
                        projectOpenning = true;
                        registersForm = null;
                        LoadOtherComponentDialog_FileOk(sender, e);
                    }
                }
            }


            projectOpenning = false;
            sw.Stop();

            Console.WriteLine("Project openning time: {0} ms.", sw.ElapsedMilliseconds);
        }

        private void recompileCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string contents = File.ReadAllText(dataFolder + cpu.Name + ".grammar");
                File.WriteAllText(cpu.Name + ".grammar", contents);
                string command = "/C java -jar Grammar//grammatica-1.5.jar " + cpu.Name + ".grammar --csoutput grammar//cs --csnamespace MultiArc_Compiler --cspublic";
                Process proc = new Process();
                proc.StartInfo.FileName = "CMD.exe";
                proc.StartInfo.Arguments = command;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                string line = "";
                while (!proc.StandardOutput.EndOfStream)
                    line += proc.StandardOutput.ReadLine() + "\n";
                File.WriteAllText("cmdout.txt", line);
                line = "";
                while (!proc.StandardError.EndOfStream)
                {
                    string l = proc.StandardError.ReadLine();
                    line += l + "\n";
                }
                if (!line.Equals(""))
                {
                    memoryDumpToolStripMenuItem.Enabled = false;
                    registersToolStripMenuItem.Enabled = false;
                    assembleToolStripMenuItem.Enabled = false;
                    executeToolStripMenuItem.Enabled = false;
                    nextStepToolStripMenuItem.Enabled = false;
                    executeWithoutDebugToolStripMenuItem.Enabled = false;
                    stopDebuggingToolStripMenuItem.Enabled = false;
                    DebugButton.Enabled = false;
                    recompileCodeToolStripMenuItem.Enabled = false;
                    OutputBox.Text += DateTime.Now.ToString() + " Error in grammar: " + line + "\n";
                    OutputBox.ScrollToCaret();
                }
                else
                {
                    string grammar = File.ReadAllText(cpu.Name + ".grammar");
                    File.Delete(cpu.Name + ".grammar");
                    File.WriteAllText(dataFolder + cpu.Name + ".grammar", grammar);
                    bool compileSuccessful = true;
                    foreach (SystemComponent c in componentsList)
                    {
                        if (c.CompileCode(dataFolder) > 0)
                        {
                            compileSuccessful = false;
                        }
                    }
                    foreach (AddressingMode am in cpu.Constants.AllAddressingModes)
                    {
                        if (am.CompileCode(OutputBox) == false)
                        {
                            compileSuccessful = false;
                        }
                    }
                    foreach (Instruction i in cpu.Constants.InstructionSet)
                    {
                        if (i.CompileCode(OutputBox) == false)
                        {
                            compileSuccessful = false;
                        }
                    }
                    if (compileSuccessful == true)
                    {
                        OutputBox.AppendText(DateTime.Now.ToString() + " Architecture loaded successfully. \n");
                        OutputBox.ScrollToCaret();
                        memoryDumpToolStripMenuItem.Enabled = true;
                        registersToolStripMenuItem.Enabled = true;
                        assembleToolStripMenuItem.Enabled = true;
                        executeToolStripMenuItem.Enabled = true;
                        nextStepToolStripMenuItem.Enabled = true;
                        executeWithoutDebugToolStripMenuItem.Enabled = true;
                        DebugButton.Enabled = true;
                        stopDebuggingToolStripMenuItem.Enabled = false;
                        compiled = false;
                    }
                    else
                    {
                        memoryDumpToolStripMenuItem.Enabled = false;
                        registersToolStripMenuItem.Enabled = false;
                        assembleToolStripMenuItem.Enabled = false;
                        executeToolStripMenuItem.Enabled = false;
                        nextStepToolStripMenuItem.Enabled = false;
                        executeWithoutDebugToolStripMenuItem.Enabled = false;
                        stopDebuggingToolStripMenuItem.Enabled = false;
                        DebugButton.Enabled = false;
                        recompileCodeToolStripMenuItem.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("error.txt", ex.ToString());
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CodeBox.Size = new Size((int)(Math.Round((this.Size.Width - 16 - 2 * 29) / 1.9974)), (int)(Math.Round((this.Size.Height - 78 - 36 - 25) / 1.4581)));
            BinaryCodeBox.Size = new Size((int)(Math.Round((this.Size.Width - 16 - 2 * 29) / 2.0026)), (int)(Math.Round((this.Size.Height - 78 - 36 - 25) / 1.4581)));
            BinaryCodeBox.Location = new Point(CodeBox.Location.X + CodeBox.Size.Width + 16, CodeBox.Location.Y);
            BinaryCodeLabel.Location = new Point(BinaryCodeBox.Location.X - 3, BinaryCodeLabel.Location.Y);
            OutputBox.Size = new Size((int)(this.Size.Width - 29 - 9), (int)(Math.Round((this.Size.Height - 78 - 36 - 25) / 3.8305)));
            OutputBox.Location = new Point(9, CodeBox.Location.Y + CodeBox.Size.Height + 36);
            outputLabel.Location = new Point(9, OutputBox.Location.Y - 16);
            clearOutputButton.Location = new Point(9 + OutputBox.Size.Width - 45, OutputBox.Location.Y - 22);
        }

        private void stopDebuggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopDebugging();
            deselectAllLines();
            CodeBox.ReadOnly = false;
            stopDebuggingToolStripMenuItem.Enabled = false;
            OutputBox.AppendText(DateTime.Now + " Code stopped. \n");
        }

        public void StopDebugging()
        {
            if (ex != null)
            {
                ex.StopDebugging();
            }
        }

        private delegate void Deselect();

        public void ExecutionOver()
        {
            if (CodeBox.InvokeRequired == true)
            {
                Deselect d = new Deselect(ExecutionOver);
                CodeBox.BeginInvoke(d);
            }
            else
            {
                deselectAllLines();
            }
            assembleToolStripMenuItem.Enabled = true;
            stopDebuggingToolStripMenuItem.Enabled = false;
        }

        private void systemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clipboard = new Clipboard(componentsList, system, projectPath);
            clipboard.Visible = true;
            clipboard.Focus();
        }

        private void cPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadArchitectureDialog.ShowDialog();
        }

        private void memoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMemoryDialog.ShowDialog();
        }

        private void LoadMemoryDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                Memory memory = new Memory(projectPath);
                string fileName = memoryFileName == null ? LoadMemoryDialog.FileName : memoryFileName;
                string content = File.ReadAllText(fileName);
                int errorCount = memory.Load(fileName, dataFolder);
                if (errorCount == 0)
                {
                    Program.Mem = memory;
                    var sameMemory = componentsList.FirstOrDefault(c => c.Name == memory.Name);
                    if (sameMemory != null)
                    {
                        componentsList.Remove(sameMemory);
                    }

                    componentsList.AddLast(memory);
                    OutputBox.Text += DateTime.Now.ToString() + " Memory architecture loaded succesfully.\n";
                    if (projectOpenning == false)
                    {
                        if (!File.Exists(dataFolder + "Memories\\" + fileName))
                        {
                            var file = File.Create(dataFolder + "Memories\\" + fileName.Substring(fileName.LastIndexOf('\\')));
                            file.Close();
                        }
                        File.WriteAllText(dataFolder + "Memories\\" + fileName.Substring(fileName.LastIndexOf('\\')), content);
                    }

                    EnableAllButtons(); 
                    compiled = false;
                }
            }
            catch (Exception ex)
            {
                OutputBox.Text += DateTime.Now.ToString() + " Error in architecture file: " + ex.Message + "\n";
                OutputBox.ScrollToCaret();
                File.AppendAllText("error.txt", ex.ToString());
                projectOpenning = false;
                DisableAllButtons();
                return;
            }
        }

        private void DisableAllButtons()
        {
            designerToolStripMenuItem.Enabled = false;
            memoryDumpToolStripMenuItem.Enabled = false;
            registersToolStripMenuItem.Enabled = false;
            assembleToolStripMenuItem.Enabled = false;
            executeToolStripMenuItem.Enabled = false;
            nextStepToolStripMenuItem.Enabled = false;
            executeWithoutDebugToolStripMenuItem.Enabled = false;
            stopDebuggingToolStripMenuItem.Enabled = false;
            DebugButton.Enabled = false;
            recompileCodeToolStripMenuItem.Enabled = false;
        }

        private delegate void SwitchToInstructionDelegate(int number);

        public void InstructionReached(int instructionNumber)
        {
            if (CodeBox.InvokeRequired)
            {
                SwitchToInstructionDelegate d = new SwitchToInstructionDelegate(SwitchToInstruction);
                //d.Invoke(instructionNumber, Color.Yellow);
                this.BeginInvoke(d, instructionNumber);
            }
            else
            {
                SwitchToInstruction(instructionNumber);
            }
        }

        public void SwitchToInstruction(int instructionNumber)
        {
            deselectAllLines();
            MarkInstruction(instructionNumber, Color.Yellow);
        }

        private void otherComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOtherComponentDialog.ShowDialog();
        }

        private void LoadOtherComponentDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                OtherComponent otherComponent = new OtherComponent(projectPath);
                string fileName = otherComponentFileName ?? LoadOtherComponentDialog.FileName;
                string content = File.ReadAllText(fileName);
                int errorCount = otherComponent.Load(fileName, dataFolder);
                if (errorCount == 0)
                {
                    var sameComponent = componentsList.FirstOrDefault(c => c.Name == otherComponent.Name);
                    if (sameComponent != null)
                    {
                        componentsList.Remove(sameComponent);
                    }

                    componentsList.AddLast(otherComponent);
                    OutputBox.Text += DateTime.Now.ToString() + " Component architecture loaded succesfully.\n";
                    if (projectOpenning == false)
                    {
                        if (!File.Exists(dataFolder + "Other\\" + fileName))
                        {
                            var file = File.Create(dataFolder + "Other\\" + fileName.Substring(fileName.LastIndexOf('\\')));
                            file.Close();
                        }
                        File.WriteAllText(dataFolder + "Other\\" + fileName.Substring(fileName.LastIndexOf('\\')), content);
                    }

                    EnableAllButtons();
                }
            }
            catch (Exception ex)
            {
                OutputBox.Text += DateTime.Now.ToString() + " Error in architecture file: " + ex.Message + "\n";
                OutputBox.ScrollToCaret();
                File.AppendAllText("error.txt", ex.ToString());
                projectOpenning = false;
                DisableAllButtons();
                return;
            }
        }

        private void EnableAllButtons()
        {
            designerToolStripMenuItem.Enabled = true;
            memoryDumpToolStripMenuItem.Enabled = true;
            registersToolStripMenuItem.Enabled = true;
            assembleToolStripMenuItem.Enabled = true;
            executeToolStripMenuItem.Enabled = true;
            nextStepToolStripMenuItem.Enabled = true;
            executeWithoutDebugToolStripMenuItem.Enabled = true;
            systemToolStripMenuItem.Enabled = true;
            DebugButton.Enabled = true;
            recompileCodeToolStripMenuItem.Enabled = true;
            designerToolStripMenuItem.Enabled = true;
            compiled = false;
        }

        private void designerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Designer designer = new Designer(componentsList, projectPath);
        }
    }
}
