public static void getAddrData_immed(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int startBit, int endBit, ref int result)
{
	result = ir.GetBits(startBit, endBit);
}