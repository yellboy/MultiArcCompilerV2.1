using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImageMapTestClient
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private DotNetOpenSource.Controls.ImageMap imageMap1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			this.imageMap1.AddRectangle("Rectangle", 140, 20, 280, 60);
			this.imageMap1.AddPolygon("Polygon", new Point[] {new Point(100, 100), new Point(180, 80), new Point(200, 140)});
			this.imageMap1.AddElipse("Ellipse", 80, 100, 60);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.imageMap1 = new DotNetOpenSource.Controls.ImageMap();
			this.SuspendLayout();
			// 
			// imageMap1
			// 
			this.imageMap1.Image = ((System.Drawing.Bitmap)(resources.GetObject("imageMap1.Image")));
			this.imageMap1.Name = "imageMap1";
			this.imageMap1.Size = new System.Drawing.Size(304, 200);
			this.imageMap1.TabIndex = 0;
			this.imageMap1.RegionClick += new DotNetOpenSource.Controls.ImageMap.RegionClickDelegate(this.imageMap1_RegionClick);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(296, 198);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.imageMap1});
			this.Name = "Form1";
			this.Text = "ImageMap Test Form";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void imageMap1_RegionClick(int index, string key)
		{
			MessageBox.Show("Region #" + index + ", " + key + ", clicked!", "Region Click");
		}
	}
}
