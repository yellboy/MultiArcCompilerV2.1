namespace MultiArc_Compiler
{
    partial class SignalPinStatesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignalPinStatesForm));
            this.PinStatesGrid = new System.Windows.Forms.DataGridView();
            this.signalPinsDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.componentNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pinNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pinValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.PinStatesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalPinsDtoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PinStatesGrid
            // 
            this.PinStatesGrid.AllowUserToAddRows = false;
            this.PinStatesGrid.AllowUserToDeleteRows = false;
            this.PinStatesGrid.AutoGenerateColumns = false;
            this.PinStatesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PinStatesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.PinStatesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PinStatesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.componentNameDataGridViewTextBoxColumn,
            this.pinNameDataGridViewTextBoxColumn,
            this.pinValueDataGridViewTextBoxColumn});
            this.PinStatesGrid.DataSource = this.signalPinsDtoBindingSource;
            this.PinStatesGrid.Enabled = false;
            this.PinStatesGrid.Location = new System.Drawing.Point(0, 0);
            this.PinStatesGrid.Name = "PinStatesGrid";
            this.PinStatesGrid.ReadOnly = true;
            this.PinStatesGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.PinStatesGrid.RowHeadersVisible = false;
            this.PinStatesGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PinStatesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PinStatesGrid.Size = new System.Drawing.Size(364, 262);
            this.PinStatesGrid.TabIndex = 0;
            // 
            // signalPinsDtoBindingSource
            // 
            this.signalPinsDtoBindingSource.DataSource = typeof(MultiArc_Compiler.SignalPinsDto);
            // 
            // componentNameDataGridViewTextBoxColumn
            // 
            this.componentNameDataGridViewTextBoxColumn.DataPropertyName = "ComponentName";
            this.componentNameDataGridViewTextBoxColumn.HeaderText = "Component Name";
            this.componentNameDataGridViewTextBoxColumn.Name = "componentNameDataGridViewTextBoxColumn";
            this.componentNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.componentNameDataGridViewTextBoxColumn.Width = 120;
            // 
            // pinNameDataGridViewTextBoxColumn
            // 
            this.pinNameDataGridViewTextBoxColumn.DataPropertyName = "PinName";
            this.pinNameDataGridViewTextBoxColumn.HeaderText = "Pin Name";
            this.pinNameDataGridViewTextBoxColumn.Name = "pinNameDataGridViewTextBoxColumn";
            this.pinNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.pinNameDataGridViewTextBoxColumn.Width = 120;
            // 
            // pinValueDataGridViewTextBoxColumn
            // 
            this.pinValueDataGridViewTextBoxColumn.DataPropertyName = "PinValue";
            this.pinValueDataGridViewTextBoxColumn.HeaderText = "Pin Value";
            this.pinValueDataGridViewTextBoxColumn.Name = "pinValueDataGridViewTextBoxColumn";
            this.pinValueDataGridViewTextBoxColumn.ReadOnly = true;
            this.pinValueDataGridViewTextBoxColumn.Width = 120;
            // 
            // SignalPinStatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 262);
            this.Controls.Add(this.PinStatesGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SignalPinStatesForm";
            this.Text = "Signal Pin States";
            ((System.ComponentModel.ISupportInitialize)(this.PinStatesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalPinsDtoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PinStatesGrid;
        private System.Windows.Forms.BindingSource signalPinsDtoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pinNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pinValueDataGridViewTextBoxColumn;
    }
}