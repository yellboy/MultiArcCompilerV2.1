public static void getAddrData_regdir(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, ref int result)
{
	int regNumber = ir.GetBits(startBit, endBit);
	result = constants.GetRegister("r" + regNumber).Val;
}

public static void setAddrData_regdir(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, int data)
{
	int regNumber = ir.GetBits(startBit, endBit);
	constants.GetRegister("r" + regNumber).Val = data;
}
	