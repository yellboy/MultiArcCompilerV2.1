/* 
 *This is auto-generated text. 
 *Please, edit only method bodies. 
 */

public static void execute_pop(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int[] operands, ref int[] result)
{	
	result = new int[1];
	byte[] readValue = new byte[1];
	uint sp = (uint)(constants.GetRegister("SP").Val);
	sp++;
	readValue = memory[sp]; 
	result[0] |= (((int)readValue[0]) << 8);
	sp++;
	readValue = memory[sp];
	result[0] |= (int)(readValue[0]) & 0xff;
	if (result[0] == 0)
	{
		constants.GetRegister("PSW").SetBits(0, 0, 1);
	}
	else
	{
		constants.GetRegister("PSW").SetBits(0, 0, 0);
	}
	constants.GetRegister("SP").Val = (int)sp;
}