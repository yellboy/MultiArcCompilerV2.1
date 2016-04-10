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
            var item = (DropableControl)(e.Data.GetData(formats[0]));
            //item.Location = new Point(e.X, e.Y);
            //item.Location = new Point(e.X - this.Location.X - 8, e.Y);
            var point = new Point(e.X - item.ClickedX, e.Y - item.ClickedY);
            item.Location = PointToClient(point);
            item.Refresh();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}
