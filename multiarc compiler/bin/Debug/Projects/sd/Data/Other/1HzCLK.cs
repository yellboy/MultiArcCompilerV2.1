
// This is auto-generated code.
// Please, edit only method body.

public static void Cycle(OtherComponent component)
{
	Console.WriteLine("CLK working");
	component.GetPort("CLK").Val = 0;
	component.Wait(1);
	component.GetPort("CLK").Val = 1;
	component.Wait(1);
}
