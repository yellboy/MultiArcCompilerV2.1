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

            foreach (var p in signal.Pins)
            {
                if (!p.ValueSetExternaly)
                {
                    signalPinsDtoBindingSource.Add(new SignalPinsDto
                    {
                        ComponentName = p.ParentPort.Component.Name,
                        PinName = p.Name,
                        PinValue = p.Val.ToString()
                    });
                }
            }

            PinStatesGrid.ClearSelection();
        }
    }
}
