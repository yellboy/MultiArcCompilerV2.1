namespace MultiArc_Compiler
{
    partial class ManageSignalNamesDialog
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
            this.NamesList = new System.Windows.Forms.ListBox();
            this.NamesLabel = new System.Windows.Forms.Label();
            this.NewNameInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddNameButton = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NamesList
            // 
            this.NamesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NamesList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NamesList.FormattingEnabled = true;
            this.NamesList.Location = new System.Drawing.Point(12, 25);
            this.NamesList.Name = "NamesList";
            this.NamesList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.NamesList.Size = new System.Drawing.Size(120, 223);
            this.NamesList.TabIndex = 0;
            this.NamesList.SelectedIndexChanged += new System.EventHandler(this.NamesList_SelectedIndexChanged);
            // 
            // NamesLabel
            // 
            this.NamesLabel.AutoSize = true;
            this.NamesLabel.Location = new System.Drawing.Point(9, 6);
            this.NamesLabel.Name = "NamesLabel";
            this.NamesLabel.Size = new System.Drawing.Size(75, 13);
            this.NamesLabel.TabIndex = 1;
            this.NamesLabel.Text = "Current names";
            // 
            // NewNameInput
            // 
            this.NewNameInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewNameInput.Location = new System.Drawing.Point(211, 25);
            this.NewNameInput.Name = "NewNameInput";
            this.NewNameInput.Size = new System.Drawing.Size(100, 20);
            this.NewNameInput.TabIndex = 2;
            this.NewNameInput.TextChanged += new System.EventHandler(this.NewNameInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "New name";
            // 
            // AddNameButton
            // 
            this.AddNameButton.Enabled = false;
            this.AddNameButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddNameButton.Location = new System.Drawing.Point(236, 51);
            this.AddNameButton.Name = "AddNameButton";
            this.AddNameButton.Size = new System.Drawing.Size(75, 23);
            this.AddNameButton.TabIndex = 4;
            this.AddNameButton.Text = "Add";
            this.AddNameButton.UseVisualStyleBackColor = true;
            this.AddNameButton.Click += new System.EventHandler(this.AddNameButton_Click);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Enabled = false;
            this.ApplyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ApplyButton.Location = new System.Drawing.Point(150, 225);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyButton.TabIndex = 5;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CancelButton.Location = new System.Drawing.Point(236, 225);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Enabled = false;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveButton.Location = new System.Drawing.Point(236, 80);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 7;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // ManageSignalNamesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 262);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.AddNameButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NewNameInput);
            this.Controls.Add(this.NamesLabel);
            this.Controls.Add(this.NamesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ManageSignalNamesDialog";
            this.Text = "Manage Signal Names";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox NamesList;
        private System.Windows.Forms.Label NamesLabel;
        private System.Windows.Forms.TextBox NewNameInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddNameButton;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button RemoveButton;
    }
}