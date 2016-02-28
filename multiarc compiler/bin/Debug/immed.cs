public static void getAddrData_immed(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, ref int result)
{
	result = ir.GetBits(startBit, endBit);
}