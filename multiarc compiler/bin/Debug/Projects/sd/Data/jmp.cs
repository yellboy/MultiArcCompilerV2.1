public static void execute_jmp(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int[] operands, ref int[] result)
{
	cpu.Constants.GetRegister("pc").Val += ir.GetBits(7, 0);
}