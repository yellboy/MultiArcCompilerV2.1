/* 
 *This is auto-generated text. 
 *Please, edit only method bodies. 
 */

public static void execute_bz(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int[] operands, ref int[] result)
{	
	if (constants.GetRegister("PSW").GetBits(0, 0) == 1)
	{
		constants.GetRegister("PC").Val += operands[0];
	}
}