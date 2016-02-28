public static void getAddrData_regind(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, ref int result)
{
	int regNumber = ir.GetBits(startBit, endBit);
	byte[] val = memory[(uint)(constants.GetRegister("r" + regNumber).Val)];
	for (int i = 0; i < memory.AuSize; i++)
	{
		result |= (int)(val[i] << (8 * i));
	}
}
public static void setAddrData_regind(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, int data)
{
	int regNumber = ir.GetBits(startBit, endBit);
	byte[] val = new byte[memory.AuSize];
	for (int i = 0; i < memory.AuSize; i++)
	{
		val[i] = (byte)((data >> (i * 8)) & 0xff);
	}
	memory[(uint)(constants.GetRegister("r" + regNumber).Val)] = val;
}