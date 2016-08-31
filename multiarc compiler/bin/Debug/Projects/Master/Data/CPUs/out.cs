/* 
*This is auto-generated text. 
*Please, edit only method bodies. 
*/

public static void execute_out(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int[] operands, ref int[] result)
{	
	var operand = operands[0];
	cpu.GetPort("DATA").Val = operand;
}