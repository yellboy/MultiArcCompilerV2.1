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
    public partial class SignalPinStatesForm : Form
    {
        public SignalPinStatesForm(Signal signal)
        {
            InitializeComponent();
            Visible = true;

            foreach (var pin in signal.Pins)
            {
                if (!pin.ValueSetExternaly && !(pin.Val == PinValue.HIGHZ && pin.SetValue == PinValue.UNDEFINED || pin.Val == PinValue.UNDEFINED))
                {
                    signalPinsDtoBindingSource.Add(new SignalPinsDto
                    {
                        ComponentName = pin.ParentPort.Component.Name,
                        PinName = pin.Name,
                        PinValue = pin.Val == PinValue.HIGHZ ? pin.SetValue.ToString() : pin.Val.ToString()
                    });
                }
            }

            PinStatesGrid.ClearSelection();
        }
    }
}
