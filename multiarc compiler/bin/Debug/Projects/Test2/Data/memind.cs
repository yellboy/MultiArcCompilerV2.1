/* 
 *This is auto-generated text. 
 *Please, edit only method bodies. 
 */

public static void getAddrData_memind(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, ref int result)
{
	uint address1 = (uint)ir.GetBits(startBit, endBit);
	byte[] toRead = memory[address1];
	int address = 0;
	address |= (((int)(toRead[0])) << 8);
	address1++;
	toRead = memory[address1];
	address |= (int)(toRead[0]);
	toRead = memory[(uint)address];
	result |= (((int)(toRead[0])) << 8);
	address++;
	toRead = memory[(uint)address];
	result |= (int)(toRead[0]);
}

public static void setAddrData_memind(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, int data)
{
	uint address1 = (uint)ir.GetBits(startBit, endBit);
	byte[] toRead = memory[address1];
	int address = 0;
	address |= (((int)(toRead[0])) << 8);
	address1++;
	toRead = memory[address1];
	address |= (int)(toRead[0]);
	byte[] toWrite = new byte[1];
	toWrite[0] = (byte)((data & 0xff00) >> 8);
	memory[(uint)address] = toWrite;
	address++;
	toWrite[0] = (byte)(data & 0xff);
	memory[(uint)address] = toWrite;
}

public static void getOperand_memind(string image, int currentLocation, int relativeValue, int absoluteValue, ref int operand)
{
	// TODO Write how this addressing mode gets operand value from instruction assembly code here.
	// If this method is not necessary, just leave it like this.
}