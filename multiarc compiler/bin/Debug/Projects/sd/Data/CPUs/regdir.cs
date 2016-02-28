public static void getAddrData_regdir(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int startBit, int endBit, ref int result)
{
	int regNumber = ir.GetBits(startBit, endBit);
	result = cpu.Constants.GetRegister("r" + regNumber).Val;
}

public static void setAddrData_regdir(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int startBit, int endBit, int data)
{
	int regNumber = ir.GetBits(startBit, endBit);
	cpu.Constants.GetRegister("r" + regNumber).Val = data;
}
	