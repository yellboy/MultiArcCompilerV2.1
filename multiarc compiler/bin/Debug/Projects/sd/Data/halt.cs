public static void execute_halt(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int[] operands, ref int[] result)
{
	variables.SetVariable("working", false);
}