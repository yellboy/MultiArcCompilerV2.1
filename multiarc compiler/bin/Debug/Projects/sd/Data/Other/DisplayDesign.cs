public static void DrawComponent(MultiArc_Compiler.OtherComponent component, Graphics graphics) 
{
	var value = component.GetPort("DSP").Val;
	component.Height = 48;
	component.Width = 50;
	Rectangle rectangle = new Rectangle(5, 0, 45, 44);
	graphics.FillRectangle(new SolidBrush(Color.White), rectangle);
	graphics.DrawRectangle(component.DefaultPen, rectangle);
	component.GetPin("DSP0").Location = new Point(0, 1);
	component.GetPin("DSP1").Location = new Point(0, 7);
	component.GetPin("DSP2").Location = new Point(0, 13);
	component.GetPin("DSP3").Location = new Point(0, 19);
	component.GetPin("DSP4").Location = new Point(0, 25);
	component.GetPin("DSP5").Location = new Point(0, 31);
	component.GetPin("DSP6").Location = new Point(0, 37);
	component.GetPin("DSP7").Location = new Point(0, 43);
	graphics.DrawString(value.ToString(), new Font(new FontFamily("Arial"), 16), Brushes.Black, new Point(5, 20));
}