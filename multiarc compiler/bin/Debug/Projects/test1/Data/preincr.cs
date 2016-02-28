/* 
 *This is auto-generated text. 
 *Please, edit only method bodies. 
 */

public static void getAddrData_preincr(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, ref int result)
{
	
	constants.GetRegister("R" + ir.GetBits(startBit, endBit)).Val--;
	uint address = (uint)constants.GetRegister("R" + ir.GetBits(startBit, endBit)).Val;
	byte[] toRead = memory[address];
	result |= (((int)(toRead[0])) << 8);
	address++;
	toRead = memory[address];
	result |= (int)(toRead[0]);
}

public static void setAddrData_preincr(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, int data)
{
	constants.GetRegister("R" + ir.GetBits(startBit, endBit)).Val--;
	uint address = (uint)constants.GetRegister("R" + ir.GetBits(startBit, endBit)).Val;
	byte[] toWrite = new byte[1];
	toWrite[0] = (byte)((data & 0xff00) >> 8);
	memory[address] = toWrite;
	address++;
	toWrite[0] = (byte)(data & 0xff);
	memory[address] = toWrite;
}

public static void getOperand_preincr(string image, int currentLocation, int relativeValue, int absoluteValue, ref int operand)
{
	// TODO Write how this addressing mode gets operand value from instruction assembly code here.
	// If this method is not necessary, just leave it like this.
}