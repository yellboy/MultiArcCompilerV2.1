/*
 * File: Instruction.cs
 * Author: Bojan Jelaca
 * Date: March 2014
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;
using System.Windows.Forms;

namespace MultiArc_Compiler
{

    /// <summary>
    /// Class that represents one type of instruction.
    /// </summary>
    public class Instruction
    {

        /// <summary>
        /// String that contains code to be executed when this instruction is executed.
        /// </summary>
        private string executionCode;

        /// <summary>
        /// Name of the instruction.
        /// </summary>
        private string name;

        public string Name
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

        /// <summary>
        /// Instruction size in bytes.
        /// </summary>
        private int size;

        public int Size
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

        /// <summary>
        /// Mnemonic of the instruction.
        /// </summary>
        private string mnemonic;

        public string Mnemonic
        {
            get
            {
                return mnemonic;
            }
            set
            {
                mnemonic = value;
            }
        }

        

        /// <summary>
        /// Name of the file with the execution code.
        /// </summary>
        private string fileName;

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        /// <summary>
        /// Start bit of the opcode.
        /// </summary>
        private int startBit;

        public int StartBit
        {
            get
            {
                return startBit;
            }
            set
            {
                startBit = value;
            }
        }

        /// <summary>
        /// Bit mask for the instruction.
        /// </summary>
        private byte[] mask;

        public byte[] Mask
        {
            get
            {
                return mask;
            }
            set
            {
                mask = value;
            }
        }

        /// <summary>
        /// End bit of the opcode.
        /// </summary>
        private int endBit;

        public int EndBit
        {
            get
            {
                return endBit;
            }
            set
            {
                endBit = value;
            }
        }

        /// <summary>
        ///  List of the arguments.
        /// </summary>
        private LinkedList<Argument> arguments;

        public LinkedList<Argument> Arguments
        {
            get
            {
                return arguments;
            }
        }

        /// <summary>
        /// Results of the execution code compile.
        /// </summary>
        private CompilerResults results;

        /// <summary>
        /// Adds one argument to the list of arguments.
        /// </summary>
        /// <param name="a">
        /// Argument to be added.
        /// </param>
        public void AddArgument(Argument a)
        {
            arguments.AddLast(a);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Instruction()
        {
            arguments = new LinkedList<Argument>();
        }

        /// <summary>
        /// Returns expression describing this instruction.
        /// </summary>
        /// <returns></returns>
        public string GetExpression()
        {
            string retVal = name + " = " + mnemonic + " ";
            for (int i = 0; i < arguments.Count; i++)
            {
                retVal += arguments.ElementAt(i).GetExpression();
                if (i == arguments.Count - 1)
                {
                    retVal += " ;";
                }
                else
                {
                    retVal += " COMMA ";
                }
            }
            if (arguments.Count == 0)
            {
                retVal += " ;";
            }
            return retVal;
        }

        /// <summary>
        /// Execution of instruction. 
        /// </summary>
        /// <param name="ir">
        /// Binary code of the instruction represented as InstructionRegister object.
        /// </param>
        /// <param name="constants">
        /// Constants for current architecture.
        /// </param>
        /// <param name="variables">
        /// User variables.
        /// </param>
        /// <param name="operands">
        /// Operands needed for operation.
        /// </param>
        /// <returns>
        /// Result of instruction execution.
        /// </returns>
        public int[] Execute(InstructionRegister ir, CPU cpu, Variables variables, int[] operands)
        {
            int[] result = null;
            var t = results.CompiledAssembly.GetType("DynamicClass" + name);
            object[] parameters = new object[] { ir, Program.Mem, cpu, variables, operands, result };
            t.GetMethod("execute_" + this.mnemonic.ToLower()).Invoke(null, parameters);
            result = (int[])(parameters[5]);
            return result;
        }

        /// <summary>
        /// Reads addressing modes from binary code.
        /// </summary>
        /// <param name="binary">
        /// Binary code.
        /// </param>
        public void ReadAddressingModes(byte[] binary)
        {
            foreach (Argument arg in arguments)
            {
                if (arg.AddressingModes.Count > 1)
                {
                    foreach (AddressingMode addrMode in arg.AddressingModes)
                    {
                        int codeStarts = arg.CodeStarts[addrMode.Name];
                        int codeEnds = arg.CodeEnds[addrMode.Name];
                        int codeValue = 0;
                        int codeSize = 1;
                        for (int k = codeStarts; k >= codeEnds; k--)
                        {
                            if (k % 8 == 0 && k != codeEnds)
                            {
                                codeSize++;
                            }
                        }
                        int codeCount = codeStarts - codeEnds;
                        int byteCount = codeSize - 1;
                        for (int k = codeStarts; k >= codeEnds; k--)
                        {
                            int semiValue = (binary[binary.Length - 1 - codeEnds / 8 - byteCount] & (1 << ((codeCount + codeEnds) % 8)));
                            codeValue |= semiValue << byteCount * 8;
                            //codeValue |= (byte)((semiValue & (1 << (codeEnds % 8 + codeCount))) >> byteCount * 8); // This might be a problem.
                            if ((codeEnds + codeCount) % 8 == 0)
                                byteCount--;
                            codeCount--;
                        }
                        codeValue >>= codeEnds % 8;
                        if (arg.CodeValues[addrMode.Name] == codeValue)
                        {
                            arg.SelectedAddressingMode = addrMode;
                            break;
                        }
                    }
                }
                else
                {
                    arg.SelectedAddressingMode = arg.AddressingModes.ElementAt(0);
                }
            }

            if (arguments.Any(a => a.SelectedAddressingMode == null))
            {
                throw new Exception("Invalid addressing mode");
            }
        }

        /// <summary>
        /// Fetches operands for all arguments that are declared as src.
        /// </summary>
        /// <param name="ir">
        /// Binary code of the instruction represented with InstructionRegister object.
        /// </param>
        /// <param name="constants">
        /// Architecture constants of the curent architecture.
        /// </param>
        /// <param name="variables">
        /// User variables.
        /// </param>
        /// <returns>
        /// Array containing fetched operands.
        /// </returns>
        public int[] FetchOperands(InstructionRegister ir, CPU cpu, Variables variables)
        {
            LinkedList<int> operands = new LinkedList<int>();
            foreach (Argument arg in arguments)
            {
                if (arg.Type.ToLower().Equals("src"))
                {
                    int result = arg.SelectedAddressingMode.GetData(ir, cpu, variables, arg.OperandStarts[arg.SelectedAddressingMode.Name], arg.OperandEnds[arg.SelectedAddressingMode.Name]);
                    operands.AddLast(result);
                }
            }
            return operands.ToArray();
        }

        /// <summary>
        /// Stores result of the execution.
        /// </summary>
        /// <param name="ir">
        /// Binary code of the instruction represented with InstructionRegister object.
        /// </param>
        /// <param name="constants">
        /// Architecture constants.
        /// </param>
        /// <param name="variables">
        /// User variables.
        /// </param>
        /// <param name="dataToStore">
        /// Result of the instruction execution.
        /// </param>
        public void StoreResult(InstructionRegister ir, CPU cpu, Variables variables, int[] dataToStore)
        {
            int argCount = 0;
            foreach (Argument arg in arguments)
            {
                if (arg.Type.ToLower().Equals("dst"))
                {
                    arg.SelectedAddressingMode.SetData(ir, cpu, variables, arg.OperandStarts[arg.SelectedAddressingMode.Name], arg.OperandEnds[arg.SelectedAddressingMode.Name], dataToStore[argCount++]);
                }
            }
        }

        /// <summary>
        /// Compiles execution code of this instruction.
        /// </summary>
        /// <param name="output">
        /// Output where compile errors will be written.
        /// </param>
        /// <returns>
        /// Bool value indicating whether compile was successful or not.
        /// </returns>
        public bool CompileCode(TextBoxBase output)
        {
            executionCode = File.ReadAllText(fileName);
            var provider = CSharpCodeProvider.CreateProvider("c#");
            var options = new CompilerParameters();
            var assemblyContainingNotDynamicClass = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            options.ReferencedAssemblies.Add(assemblyContainingNotDynamicClass);
            var assemblyContaningForms = Assembly.GetAssembly(typeof(System.Windows.Forms.Control)).Location;
            options.ReferencedAssemblies.Add(assemblyContaningForms);
            var assemblyContainingComponent = Assembly.GetAssembly(typeof(System.ComponentModel.Component)).Location;
            options.ReferencedAssemblies.Add(assemblyContainingComponent);
            string code = @"

using System;
using System.IO;
using MultiArc_Compiler;

public class DynamicClass" + name + @"
{
";
            code += executionCode;
            code += "}";
            results = provider.CompileAssemblyFromSource(options, new[] { code });
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    output.AppendText(DateTime.Now.ToString() + "Error in " + fileName + ": " + error.ErrorText + " in line " + (error.Line - 8) + ".\n");
                }
                return false;
            }
            return true;
        }
    }
}
