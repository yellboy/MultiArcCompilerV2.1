/* 
 *This is auto-generated text. 
 *Please, edit only method bodies. 
 */

public static void execute_ld(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int[] operands, ref int[] result)
{	
	constants.GetRegister("A").Val = operands[0];
	if (operands[0] == 0)
	{
		constants.GetRegister("PSW").SetBits(0, 0, 1);
	}
	else
	{	
		constants.GetRegister("PSW").SetBits(0, 0, 0);
	}
}