using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace MultiArc_Compiler
{
    public static class Program
    {
        public static Memory Mem;

        private static Form1 form;

        /// <summary>
        /// The main entry point for the application.
        /// </summary> 
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(form = new Form1());
            }
            catch (Exception ex)
            {
                form.AddToOutput(ex.ToString());
                File.WriteAllText("error.txt", ex.ToString());
            }
        }
    }
}
