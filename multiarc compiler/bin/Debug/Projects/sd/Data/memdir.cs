public static void getAddrData_memdir(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int startBit, int endBit, ref int result)
{
	uint address = (uint)(ir.GetBits(startBit, endBit));
	byte[] val = memory[address];
	result = 0;
	for (int j = 0; j < memory.AuSize; j++)
	{
		result |= val[j] << (8 * j);
	}
}

public static void setAddrData_memdir(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int startBit, int endBit, int data)
{
	byte[] val = new byte[memory.AuSize];
	for (int j = 0; j < memory.AuSize; j++)
	{
		val[j] = (byte)((data >> (8 * j)) & 0xff);
	}
	uint address = (uint)(ir.GetBits(startBit, endBit));
	memory[address] = val;
}