/* 
 *This is auto-generated text. 
 *Please, edit only method bodies. 
 */

public static void getAddrData_postdecr(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, ref int result)
{
	uint address = (uint)constants.GetRegister("R" + ir.GetBits(startBit, endBit)).Val;
	byte[] toRead = memory[address];
	result |= (((int)(toRead[0])) << 8);
	address++;
	toRead = memory[address];
	result |= (int)(toRead[0]);
	constants.GetRegister("R" + ir.GetBits(startBit, endBit)).Val++;
}

public static void setAddrData_postdecr(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, int data)
{
	
}

public static void getOperand_postdecr(string image, int currentLocation, int relativeValue, int absoluteValue, ref int operand)
{
	// TODO Write how this addressing mode gets operand value from instruction assembly code here.
	// If this method is not necessary, just leave it like this.
}