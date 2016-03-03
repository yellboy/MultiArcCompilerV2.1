/*
 * File: Executor.cs
 * Author: Bojan Jelaca
 * Date: April 2014
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace MultiArc_Compiler
{
    /// <summary>
    /// Class that provides execution of binary code.
    /// </summary>
    class Executor
    {
        private byte[] binary;

        /// <summary>
        /// Architecture constants.
        /// </summary>
        private CPU cpu;

        private UserSystem system;

        /// <summary>
        /// Array that contains points in code that separates instructions.
        /// </summary>
        private LinkedList<int> separators;

        /// <summary>
        /// Entry point of the program.
        /// </summary>
        private int entryPoint;

        /// <summary>
        /// Text box representing output.
        /// </summary>
        private TextBoxBase output;

        /// <summary>
        /// True if program is executed in step by step mode.
        /// </summary>
        private volatile bool stepByStepMode = false;
        
        /// <summary>
        /// Semaphores for threads synchronization.
        /// </summary>
        private Semaphore sem = new Semaphore(0, 1), instSem = new Semaphore(0, 1), breakSem = new Semaphore(0, 1);

        /// <summary>
        /// Thread used when in step by step mode.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// True if some program is executing.
        /// </summary>
        private volatile bool executing = false;

        private LinkedList<int> breakPoints;

        /// <summary>
        /// True if some program is currently being executed.
        /// </summary>
        public bool Executing
        {
            get
            {
                bool temp;
                Thread.BeginCriticalRegion();
                temp = executing;
                Thread.EndCriticalRegion();
                return temp;
            }
            set
            {
                Thread.BeginCriticalRegion();
                executing = true;
                Thread.EndCriticalRegion();
            }
        }

        private volatile int next;

        private Variables vars;

        /// <summary>
        /// Next instruction to be executed.
        /// </summary>
        public int Next
        {
            get
            {
                int temp;
                Thread.BeginCriticalRegion();
                temp = next;
                Thread.EndCriticalRegion();
                return temp;
            }
            set
            {
                Thread.BeginCriticalRegion();
                next = value;
                Thread.EndCriticalRegion();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cpu">
        /// CPU for which code is specified.
        /// </param>
        /// <param name="breakPoints">
        /// Locations in code where there are breakpoints.
        /// </param>
        /// <param name="system">
        /// System on which program is executed.
        /// </param>
        /// <param name="separators">
        /// Array that has informations about points where instruction separators are.
        /// </param>
        /// <param name="entryPoint">
        /// Entry point of the program.
        /// </param>
        /// <param name="psw">
        /// Program status word before exection.
        /// </param>
        /// <param name="output">
        /// Text box representing output.
        /// </param>
        public Executor(CPU cpu, UserSystem system, LinkedList<int> separators, LinkedList<int> breakPoints, TextBoxBase output, byte[] binary, int entryPoint = 0) 
        {
            this.cpu = cpu;
            this.system = system;
            this.entryPoint = entryPoint;
            this.separators = separators;
            this.breakPoints = breakPoints;
            this.output = output;
            this.binary = binary;
        }

        /// <summary>
        /// Method that executes binary code.
        /// </summary>
        public void Execute()
        {
            try
            {
                cpu.Constants.GetRegister("pc").Val = entryPoint;
                LinkedList<Instruction> instructions = new LinkedList<Instruction>();
                LinkedList<byte> binary = new LinkedList<byte>();
                Thread.BeginCriticalRegion();
                executing = true;
                Thread.EndCriticalRegion();
                Variables vars = new Variables();
                vars.SetVariable("working", true);
                while (true)
                {
                    int pc = cpu.Constants.GetRegister("pc").Val;
                    if (separators.Contains(pc) && pc != entryPoint)
                    {
                        Instruction inst = cpu.Constants.MatchInstruction(binary.ToArray());
                        InstructionRegister ir = new InstructionRegister(binary.ToArray());
                        inst.ReadAddressingModes(binary.ToArray());
                        int[] operands = inst.FetchOperands(ir, cpu, vars);
                        int[] result = inst.Execute(ir, cpu, vars, operands);
                        inst.StoreResult(ir, cpu, vars, result);
                        binary.Clear();
                        pc = cpu.Constants.GetRegister("pc").Val;
                    }
                    if (pc - entryPoint >= binary.Count() || (bool)vars.GetVariable("working") == false)
                    {
                        Thread.BeginCriticalRegion();
                        executing = false;
                        Thread.EndCriticalRegion();
                        break;
                    }
                    //binary.AddLast(binaryCode[pc++]);
                    //byte[] nextWord = Program.Mem[(uint)pc];
                    byte[] nextWord = cpu.ReadFromMemory((uint)pc);
                    for (int i = 0; i < nextWord.Length; i++ )
                    {
                        binary.AddLast(nextWord[i]);
                    }
                    pc++;
                    cpu.Constants.GetRegister("pc").Val = pc;
                }
                system.EndWorking();
                output.Text += DateTime.Now.ToString() + " Code executed successfully.\n";
                output.ScrollToCaret();
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                writeToOutput(DateTime.Now + " Execution error: " + ex.InnerException.Message + " in " + ex.InnerException.TargetSite + "\n");
                File.AppendAllText("error.txt", ex.ToString());
                StopDebugging();
                system.EndWorking();
                endExecution();
            }
            catch (Exception ex)
            {
                writeToOutput(DateTime.Now + " Execution error: " + ex.Message + "\n");
                File.AppendAllText("error.txt", ex.ToString());
                StopDebugging();
                system.EndWorking();
                endExecution();
            }
        }
        
        /// <summary>
        /// Executes one instruction.
        /// </summary>
        /// <param name="next">
        /// Line of the instruction that shall be executed after this method is executed. Note: This is out parameter.
        /// </param>
        /// <returns>
        /// Bool value signaling whether executed instruction is last in program.
        /// </returns>
        public bool ExecuteNextStep()
        {
            Thread.BeginCriticalRegion();
            stepByStepMode = true;
            Thread.EndCriticalRegion();
            //breakSem.Release(1);
            return !thread.IsAlive; 
        }

        /// <summary>
        /// Executes binary code step by step.
        /// </summary>
        private void executeStepByStep()
        {
            LinkedList<byte> instructionCode = new LinkedList<byte>();
            Thread.BeginCriticalRegion();
            executing = true;
            next = 0;
            Thread.EndCriticalRegion();
            try
            {
                vars = new Variables();
                vars.SetVariable("working", true);
                while (true)
                {
                    lock (Form1.LockObject)
                    {
                        int pc = cpu.Constants.GetRegister("pc").Val;
                        Console.WriteLine("Executing pc = " + pc);
                        if (separators.Contains(pc - entryPoint) && pc != entryPoint)
                        {

                            Thread.BeginCriticalRegion();
                            bool temp = stepByStepMode;
                            Thread.EndCriticalRegion();
                            // TODO Check for step by step mode
                            if (separators.Contains(pc - entryPoint))
                            {
                                for (int i = 0; i < separators.Count - 1; i++)
                                {
                                    if (separators.ElementAt(i) == pc - entryPoint)
                                    {
                                        Thread.BeginCriticalRegion();
                                        next = i;
                                        Thread.EndCriticalRegion();
                                    }
                                }
                            }
                            Instruction inst = cpu.Constants.MatchInstruction(instructionCode.ToArray());
                            InstructionRegister ir = new InstructionRegister(instructionCode.ToArray());
                            inst.ReadAddressingModes(binary.ToArray());
                            int[] operands = inst.FetchOperands(ir, cpu, vars);
                            int[] result = inst.Execute(ir, cpu, vars, operands);
                            inst.StoreResult(ir, cpu, vars, result);
                            instructionCode.Clear();
                            pc = cpu.Constants.GetRegister("pc").Val;
                            if (pc - entryPoint >= binary.Count() || (bool)vars.GetVariable("working") == false) // First condition might not work for addressing word larger than 1
                            {

                                Thread.BeginCriticalRegion();
                                executing = false;
                                Thread.EndCriticalRegion();
                                //instSem.Release(1);
                                Thread.Yield();
                                break;
                            }
                            //Thread.BeginCriticalRegion();
                            //temp = stepByStepMode;
                            //Thread.EndCriticalRegion();
                            //if (temp == true)
                            //{
                            //    //instSem.Release(1);
                            //    Thread.Yield();
                            //}
                        }
                        //byte[] readFromMemory = Program.Mem[(uint)pc];
                        Form1.Instance.InstructionReached(next);
                        if ((stepByStepMode || breakPoints.Contains(next)) && separators.Contains(pc - entryPoint))
                        {
                            //breakSem.WaitOne();
                            system.Running = false;
                        }
                        byte[] readFromMemory = cpu.ReadFromMemory((uint)pc);
                        Console.WriteLine("Read from memory {0}", ConversionHelper.ConvertFromByteArrayToInt(readFromMemory));
                        pc++;
                        for (int k = 0; k < readFromMemory.Length; k++)
                        {
                            instructionCode.AddLast(readFromMemory[k]);
                        }
                        //binary.AddLast(binaryCode[pc++]);
                        cpu.Constants.GetRegister("pc").Val = pc;
                        Thread.EndCriticalRegion();
                    }
                }
                //breakSem.Release();
                system.EndWorking();
                writeToOutput(" Code executed successfully.\n");
                Form1.Instance.ExecutionStoped();
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                writeToOutput(DateTime.Now + " Execution error: " + ex.InnerException.Message + " in " + ex.InnerException.TargetSite + "\n");
                File.AppendAllText("error.txt", ex.ToString());
                //instSem.Release(1);
                //breakSem.Release(1);
                StopDebugging();
                system.EndWorking();
                endExecution();
            }
            catch (Exception ex)
            {
                writeToOutput(DateTime.Now + " Execution error: " + ex.Message + "\n");
                File.AppendAllText("error.txt", ex.ToString());
                //instSem.Release(1);
                //breakSem.Release(1);
                StopDebugging();
                system.EndWorking();
                endExecution();
            }
        }

        /// <summary>
        /// Delegate used to write to output.
        /// </summary>
        /// <param name="text">
        /// Text to be written.
        /// </param>
        private delegate void SetTextCallback(string text);

        /// <summary>
        /// Method used to write to output from different thread.
        /// </summary>
        /// <param name="text">
        /// Text to be written.
        /// </param>
        private void writeToOutput(string text)
        {

            if (output.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(writeToOutput);
                Form1.Instance.BeginInvoke(d, new object[] { text });
            }
            else
            {
                output.Text += DateTime.Now.ToString() + text;
                output.ScrollToCaret();
            }
        }

        /// <summary>
        /// Enter step by step mode.
        /// </summary>
        public void EnterStepByStep()
        {
            cpu.Constants.GetRegister("pc").Val = entryPoint;
            Thread.BeginCriticalRegion();
            next = 0;
            Thread.EndCriticalRegion();
            Thread.BeginCriticalRegion();
            stepByStepMode = true;
            Thread.EndCriticalRegion();
            thread = new Thread(new ThreadStart(executeStepByStep));
            Console.WriteLine("Executor thread id = " + thread.ManagedThreadId);
            thread.Start();
        }

        /// <summary>
        /// Abort execution, if there is any.
        /// </summary>
        public void StopDebugging()
        {
            Thread.BeginCriticalRegion();
            executing = false;
            if (vars != null)
            {
                vars.SetVariable("working", false);
            }
            Thread.EndCriticalRegion();
        }

        /// <summary>
        /// Continue execution and exit step by step mode.
        /// </summary>
        public void Continue()
        {
            Thread.BeginCriticalRegion();
            stepByStepMode = false;
            Thread.EndCriticalRegion();
            //breakSem.Release(1);
            //system.Running = true;
        }

        /// <summary>
        /// Wait until one instruction is executed.
        /// </summary>
        public void WaitForOneInstruction()
        {
            instSem.WaitOne();
        }

        /// <summary>
        /// Debug program.
        /// </summary>
        public void Debug()
        {
            cpu.Constants.GetRegister("pc").Val = entryPoint;
            Thread.BeginCriticalRegion();
            next = 0;
            Thread.EndCriticalRegion();
            Thread.BeginCriticalRegion();
            stepByStepMode = false;
            Thread.EndCriticalRegion();
            thread = new Thread(new ThreadStart(executeStepByStep));
            Console.WriteLine("Executor thread id = " + thread.ManagedThreadId);
            thread.Start();
        }

        /// <summary>
        /// Wait untill breakpoint is reached or execution is over.
        /// </summary>
        public void WaitUntilBreakpointOrEnd()
        {
            breakSem.WaitOne();
        }

        /// <summary>
        /// Aborts execution of the thread, if there is any.
        /// </summary>
        public void Abort()
        {
            Thread.BeginCriticalRegion();
            executing = false;
            Thread.EndCriticalRegion();
            endExecution();
            if (thread != null)
            {
                thread.Abort();
            }
        }

        /// <summary>
        /// Delegate used for informing main form that execution is over, if it is necessarry to inform it.
        /// </summary>
        private delegate void EndExecution();

        /// <summary>
        /// Informing the main form that execution is over, if it is necessarry to inform it.
        /// </summary>
        private void endExecution()
        {
            if (Form1.Instance.InvokeRequired == true)
            {
                EndExecution d = new EndExecution(endExecution);
                Form1.Instance.BeginInvoke(d);
            }
            else
            {
                Form1.Instance.ExecutionOver();
            }
        }
    }
}
