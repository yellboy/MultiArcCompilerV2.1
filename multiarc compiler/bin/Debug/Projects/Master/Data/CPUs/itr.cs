/* 
*This is auto-generated text. 
*Please, edit only method bodies. 
*/

public static void execute_itr(InstructionRegister ir, Memory memory, CPU cpu, Variables variables, int[] operands, ref int[] result)
{	
	variables.SetVariable("INTADDR", ir.GetBits(7, 0));
	cpu.GetPort("INT").Val = 0;
}