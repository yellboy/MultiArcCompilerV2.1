using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public partial class BusPinStatesForm : Form
    {
        private Bus _bus;

        public BusPinStatesForm(Bus bus)
        {
            InitializeComponent();
            
            _bus = bus;
            Visible = true;

            for (var i = 0; i < _bus.Signals.Count; i++)
            {
                var pins = _bus.Signals.ElementAt(i).Pins;

                for (var j = 0; j < pins.Count; j++)
                {
                    var pin = pins.ElementAt(j);

                    // TODO Add checking if the value is set by component.
                    busPinsDtoBindingSource.Add(new BusPinsDto
                    {
                        Index = i,
                        ComponentName = pin.ParentPort.Component.Name,
                        PinName = pin.Name,
                        PinValue = pin.Val.ToString()
                    });
                }
            }

            PinStatesGrid.ClearSelection();
        }
    }
}
