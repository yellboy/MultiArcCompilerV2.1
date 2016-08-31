public static void DrawComponent(MultiArc_Compiler.OtherComponent component, Graphics graphics) 
{
	var value = component.GetPort("DSP").Val;
	component.Height = 52;
	component.Width = 50;
	Rectangle rectangle = new Rectangle(5, 0, 45, 48);
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
	
	string[] chars = new string[11];
	
	chars[0] = " ";
	chars[1] = ((value & 0x01) > 0) ? "_" : "  ";
	chars[2] = " ";
	chars[3] = "\n";
	chars[4] = ((value & 0x20) > 0) ? "|" : "  ";
	chars[5] = ((value & 0x40) > 0) ? "_" : "  ";
	chars[6] = ((value & 0x02) > 0) ? "|" : "  ";
	chars[7] = "\n";
	chars[8] = ((value & 0x10) > 0) ? "|" : "  ";
	chars[9] = ((value & 0x08) > 0) ? "_" : "  ";
    chars[10] = ((value & 0x04) > 0) ? "|" : "  ";	
	string s = string.Join(string.Empty, chars);
	graphics.DrawString(s, new Font(new FontFamily("Arial"), 8), Brushes.Black, new Point(5, 5));
}