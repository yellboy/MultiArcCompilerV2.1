using PerCederberg.Grammatica.Runtime;
using System.Collections.Generic;

namespace MultiArc_Compiler {


	public class MyAnalyzer : Arch4Analyzer
	{
	
		private Node[] instructions;
		private int count = 0;
		
		public MyAnalyzer(Node[] instructions)
		{
			super();
			this.instructions = instructions;
		}
		
		public Node ExitInstruction(Token node)
		{	
			if (count == instructions.Length)
			{
				Node[] temp = new Node[count + 50];
				for (int i = 0; i < count; i++)
				{
					temp[i] = instructions[i];
				}
				instructions = temp;
			}
			instructions[count++] = node;
			return node;
		}
	}
}