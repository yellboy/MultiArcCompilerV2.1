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
        public BusPinStatesForm(Bus bus)
        {
            InitializeComponent();
            
            Visible = true;

            for (var i = 0; i < bus.Signals.Count; i++)
            {
                var pins = bus.Signals.ElementAt(i).Pins;

                for (var j = 0; j < pins.Count; j++)
                {
                    var pin = pins.ElementAt(j);

                    if (!pin.ValueSetExternaly && !(pin.Val == PinValue.HIGHZ && pin.SetValue == PinValue.UNDEFINED || pin.Val == PinValue.UNDEFINED))
                    {
                        busPinsDtoBindingSource.Add(new BusPinsDto
                        {
                            Index = i,
                            ComponentName = pin.ParentPort.Component.Name,
                            PinName = pin.Name,
                            PinValue = pin.Val == PinValue.HIGHZ ? pin.SetValue.ToString() : pin.Val.ToString()
                        });
                    }
                }
            }

            PinStatesGrid.ClearSelection();
        }
    }
}
