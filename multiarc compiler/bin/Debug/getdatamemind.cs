public static void getAddrData_memind(Int16 operand, Memory memory, ref UInt16 address, ref Int16 data)
{
	address |= memory.ReadByte((uint)operand);
	address |= (memory.ReadByte((uint)(operand + 1)) << 8);
	data |= memory.ReadByte((uint)address);
	data |= (memory.ReadByte((uint)(address + 1)) << 8);
}