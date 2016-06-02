namespace MultiArc_Compiler
{
    partial class SetBusWidthForm
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
            this.OkButton = new System.Windows.Forms.Button();
            this.BusWidthInput = new System.Windows.Forms.NumericUpDown();
            this.SetWidthLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BusWidthInput)).BeginInit();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OkButton.Location = new System.Drawing.Point(75, 113);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // BusWidthInput
            // 
            this.BusWidthInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BusWidthInput.Location = new System.Drawing.Point(52, 52);
            this.BusWidthInput.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.BusWidthInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BusWidthInput.Name = "BusWidthInput";
            this.BusWidthInput.Size = new System.Drawing.Size(120, 20);
            this.BusWidthInput.TabIndex = 1;
            this.BusWidthInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SetWidthLabel
            // 
            this.SetWidthLabel.AutoSize = true;
            this.SetWidthLabel.Location = new System.Drawing.Point(6, 9);
            this.SetWidthLabel.Name = "SetWidthLabel";
            this.SetWidthLabel.Size = new System.Drawing.Size(212, 13);
            this.SetWidthLabel.TabIndex = 2;
            this.SetWidthLabel.Text = "Please, set width of the bus (number of bits)";
            // 
            // SetBusWidthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 148);
            this.Controls.Add(this.SetWidthLabel);
            this.Controls.Add(this.BusWidthInput);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SetBusWidthForm";
            this.Text = "Bus width";
            ((System.ComponentModel.ISupportInitialize)(this.BusWidthInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.NumericUpDown BusWidthInput;
        private System.Windows.Forms.Label SetWidthLabel;
    }
}