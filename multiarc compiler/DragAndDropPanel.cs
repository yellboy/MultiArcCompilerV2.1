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
            var item = (DropableControl)e.Data.GetData(formats[0]);
            //item.Location = new Point(e.X, e.Y);
            //item.Location = new Point(e.X - this.Location.X - 8, e.Y);
            var point = PointToClient(new Point(e.X - item.ClickedX, e.Y - item.ClickedY));

            var dx = item.Location.X - point.X;
            var dy = item.Location.Y - point.Y;
            
            item.Location = point;
            item.Refresh();

            foreach (var c in Controls)
            {
                if (c != item)
                {
                    var dropableControl = c as DropableControl;

                    if (dropableControl != null && dropableControl.Selected)
                    {
                        var newLocation = new Point(dropableControl.Location.X - dx, dropableControl.Location.Y - dy);
                        dropableControl.Location = newLocation;
                        dropableControl.Refresh();
                    }
                }
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}
