public static void execute_halt(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int[] operands, ref int[] result)
{
	variables.SetVariable("working", false);
}