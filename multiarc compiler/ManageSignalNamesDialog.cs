using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public partial class ManageSignalNamesDialog : Form
    {
        private Connector _connector;

        public ManageSignalNamesDialog(Connector connector)
        {
            InitializeComponent();
            Visible = true;
            connector.Names.ForEach(s => NamesList.Items.Add(s));

            _connector = connector;
        }

        private void AddNameButton_Click(object sender, EventArgs e)
        {
            NamesList.Items.Add(NewNameInput.Text.Trim());

            ApplyButton.Enabled = true;
        }

        private void NewNameInput_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NewNameInput.Text.Trim()) && !NamesList.Items.Contains(NewNameInput.Text))
            {
                AddNameButton.Enabled = true;
            }
            else
            {
                AddNameButton.Enabled = false;
            }
        }

        private void NamesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NamesList.SelectedItems.Count > 0)
            {
                RemoveButton.Enabled = true;
            }
            else
            {
                RemoveButton.Enabled = false;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            var selectedNames = new LinkedList<string>();

            foreach (var s in NamesList.SelectedItems)
            {
                selectedNames.AddLast(s as string);
            }

            foreach (var name in selectedNames)
            {
                NamesList.Items.Remove(name);
            }

            if (NamesList.Items.Count > 0)
            {
                ApplyButton.Enabled = true;
            }
            else
            {
                ApplyButton.Enabled = false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            foreach (string name in NamesList.Items)
            {
                if (!_connector.Names.Contains(name))
                {
                    _connector.AddName(name);
                }
            }

            Dispose(true);
        }
    }
}
 