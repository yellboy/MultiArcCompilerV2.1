
// This is auto-generated code. Please, edit only method bodies.
    
public static byte[] ReadFromMemory(CPU cpu, uint address)
{
    // Define how CPU reads from memory here. 
    // If this method is not needed, just leave it empty.
	cpu.GetPort("ADDRDATA").Val = (int)address;
	cpu.GetPort("WR_RD").Val = 1;
	cpu.Wait(2);
	cpu.GetPort("WR_RD").Val = 0;
	cpu.Wait(13);
	//return Program.Mem[address];
	int readValue = cpu.GetPort("ADDRDATA").Val;
	byte[] binaryValue = ConversionHelper.ConvertFromIntToByteArray(cpu.GetPort("ADDRDATA").Val, cpu.GetPort("ADDRDATA").Size);
	cpu.Wait(3);
	return binaryValue;
}

public static void WriteToMemory(CPU cpu, uint address, byte[] value)
{
    // Define how CPU writtes to memory here.
    // If this method is not needed, just leave it empty.
}

public static void CheckForInterrupts(CPU cpu, Variables variables)
{
	var address = variables.GetVariable("INTADDR");
	
	var intr = cpu.GetPort("INT").Val;
	
	if (address != null && intr == 1)
	{
		cpu.GetRegister("pc").Val = (int)address;
		cpu.GetPort("INT").Val = 0;
	}
}
