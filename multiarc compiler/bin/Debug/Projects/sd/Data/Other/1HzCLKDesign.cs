public static void DrawComponent(MultiArc_Compiler.OtherComponent component, System.Drawing.Graphics graphics) 
{
    // This is auto generated code. Please, edit only method body.
    // Define how component is drawn here.
	Console.WriteLine("Drawn CLK");
	
	component.Height = 20;
	component.Width = 25;
	Rectangle rectangle = new Rectangle(0, 0, 20, 20);
	graphics.FillRectangle(new SolidBrush(Color.White), rectangle);
	graphics.DrawRectangle(Pens.Black, rectangle);
	component.GetPin("CLK0").Location = new Point(20, 10);
}