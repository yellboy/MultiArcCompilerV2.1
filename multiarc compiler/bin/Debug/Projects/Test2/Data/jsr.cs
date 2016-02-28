/* 
 *This is auto-generated text. 
 *Please, edit only method bodies. 
 */

public static void execute_jsr(InstructionRegister ir, Memory memory, ArchConstants constants, Variables variables, int[] operands, ref int[] result)
{	
	uint sp = (uint)constants.GetRegister("SP").Val;
	byte[] toWrite = new byte[1];
	toWrite[0] = (byte)(constants.GetRegister("PC").Val & 0xff);
	memory[sp] = toWrite;
	sp--;
	toWrite[0] = (byte)((constants.GetRegister("PC").Val & 0xff00) >> 8);
	memory[sp] = toWrite;
	sp--;
	constants.GetRegister("PC").Val = operands[0];
	constants.GetRegister("SP").Val = (int)sp;
}