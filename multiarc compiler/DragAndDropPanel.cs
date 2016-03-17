using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public class DragAndDropPanel : Panel
    {
        public DragAndDropPanel()
        {
            DragDrop += new System.Windows.Forms.DragEventHandler(this.DragAndDrop);
            DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
        }

        private void DragAndDrop(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            Control item = (Control)(e.Data.GetData(formats[0]));
            //item.Location = new Point(e.X, e.Y);
            //item.Location = new Point(e.X - this.Location.X - 8, e.Y);
            item.Location = PointToClient(new Point(e.X - 8, e.Y - 8));
            item.Refresh();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}
