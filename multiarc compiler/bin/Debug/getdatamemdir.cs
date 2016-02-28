public static void getAddrData_memdir(Int16 operand, Memory memory, ref UInt16 address, ref Int16 data)
{
	address = (UInt16)operand;
//	data |= (Int16)(memory.ReadByte((uint)operand));
//	data |= (Int16)(memory.ReadByte((uint)(operand + 1)) << 8);
}