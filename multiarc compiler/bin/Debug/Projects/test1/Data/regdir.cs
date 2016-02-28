/* 
 *This is auto-generated text. 
 *Please, edit only method bodies. 
 */

public static void getAddrData_regdir(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, ref int result)
{
	result = constants.GetRegister("R" + ir.GetBits(startBit, endBit)).Val;
}

public static void setAddrData_regdir(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int startBit, int endBit, int data)
{
	constants.GetRegister("R" + ir.GetBits(startBit, endBit)).Val = data;
}

public static void getOperand_regdir(string image, int currentLocation, int relativeValue, int absoluteValue, ref int operand)
{
	// TODO Write how this addressing mode gets operand value from instruction assembly code here.
	// If this method is not necessary, just leave it like this.
}