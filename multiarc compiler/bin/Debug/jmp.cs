public static void execute_jmp(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int[] operands, ref int[] result)
{
	constants.GetRegister("pc").Val += ir.GetBits(7, 0);
}