namespace MultiArc_Compiler
{
    partial class BusPinStatesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusPinStatesForm));
            this.PinStatesGrid = new System.Windows.Forms.DataGridView();
            this.indexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pinNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pinValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.busPinsDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PinStatesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.busPinsDtoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PinStatesGrid
            // 
            this.PinStatesGrid.AllowUserToAddRows = false;
            this.PinStatesGrid.AllowUserToDeleteRows = false;
            this.PinStatesGrid.AllowUserToResizeColumns = false;
            this.PinStatesGrid.AllowUserToResizeRows = false;
            this.PinStatesGrid.AutoGenerateColumns = false;
            this.PinStatesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PinStatesGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.PinStatesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.PinStatesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PinStatesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexDataGridViewTextBoxColumn,
            this.componentNameDataGridViewTextBoxColumn,
            this.pinNameDataGridViewTextBoxColumn,
            this.pinValueDataGridViewTextBoxColumn});
            this.PinStatesGrid.DataSource = this.busPinsDtoBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PinStatesGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.PinStatesGrid.Enabled = false;
            this.PinStatesGrid.Location = new System.Drawing.Point(0, 0);
            this.PinStatesGrid.MultiSelect = false;
            this.PinStatesGrid.Name = "PinStatesGrid";
            this.PinStatesGrid.ReadOnly = true;
            this.PinStatesGrid.Size = new System.Drawing.Size(524, 45);
            this.PinStatesGrid.TabIndex = 0;
            // 
            // indexDataGridViewTextBoxColumn
            // 
            this.indexDataGridViewTextBoxColumn.DataPropertyName = "Index";
            this.indexDataGridViewTextBoxColumn.HeaderText = "Index";
            this.indexDataGridViewTextBoxColumn.Name = "indexDataGridViewTextBoxColumn";
            this.indexDataGridViewTextBoxColumn.ReadOnly = true;
            this.indexDataGridViewTextBoxColumn.Width = 120;
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
            // busPinsDtoBindingSource
            // 
            this.busPinsDtoBindingSource.DataSource = typeof(MultiArc_Compiler.BusPinsDto);
            // 
            // BusPinStatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 262);
            this.Controls.Add(this.PinStatesGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BusPinStatesForm";
            this.Text = "Pin States";
            ((System.ComponentModel.ISupportInitialize)(this.PinStatesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.busPinsDtoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PinStatesGrid;
        private System.Windows.Forms.BindingSource busPinsDtoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pinNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pinValueDataGridViewTextBoxColumn;

    }
}