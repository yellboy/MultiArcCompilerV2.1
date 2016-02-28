public static void executest(Int16 data, UInt16 address, Memory memory, ref Int16 acc, ref Int16 pc, ref Int16 psw)
{	
	byte[] toWrite = new byte[2];
	toWrite[0] = (byte)((acc & 0xff00) >> 8);
	toWrite[1] = (byte)(acc & 0x00ff);
	memory[address] = toWrite;
}