namespace MultiArc_Compiler
{
    partial class Designer
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
            this.ComponentsComboBox = new System.Windows.Forms.ComboBox();
            this.DesignPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BrowseComponentImageButton = new System.Windows.Forms.Button();
            this.BrowseComponentImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.PinsList = new System.Windows.Forms.ListBox();
            this.AddPinButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComponentsComboBox
            // 
            this.ComponentsComboBox.FormattingEnabled = true;
            this.ComponentsComboBox.Location = new System.Drawing.Point(13, 29);
            this.ComponentsComboBox.Name = "ComponentsComboBox";
            this.ComponentsComboBox.Size = new System.Drawing.Size(130, 21);
            this.ComponentsComboBox.TabIndex = 0;
            this.ComponentsComboBox.SelectedIndexChanged += new System.EventHandler(this.ComponentsComboBox_SelectedIndexChanged);
            // 
            // DesignPanel
            // 
            this.DesignPanel.BackColor = System.Drawing.Color.GhostWhite;
            this.DesignPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DesignPanel.Location = new System.Drawing.Point(150, 13);
            this.DesignPanel.Name = "DesignPanel";
            this.DesignPanel.Size = new System.Drawing.Size(443, 343);
            this.DesignPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select component";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Add image";
            // 
            // BrowseComponentImageButton
            // 
            this.BrowseComponentImageButton.Location = new System.Drawing.Point(76, 56);
            this.BrowseComponentImageButton.Name = "BrowseComponentImageButton";
            this.BrowseComponentImageButton.Size = new System.Drawing.Size(67, 23);
            this.BrowseComponentImageButton.TabIndex = 4;
            this.BrowseComponentImageButton.Text = "Browse";
            this.BrowseComponentImageButton.UseVisualStyleBackColor = true;
            // 
            // PinsList
            // 
            this.PinsList.Enabled = false;
            this.PinsList.FormattingEnabled = true;
            this.PinsList.Location = new System.Drawing.Point(13, 144);
            this.PinsList.Name = "PinsList";
            this.PinsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.PinsList.Size = new System.Drawing.Size(130, 212);
            this.PinsList.Sorted = true;
            this.PinsList.TabIndex = 5;
            // 
            // AddPinButton
            // 
            this.AddPinButton.Enabled = false;
            this.AddPinButton.Location = new System.Drawing.Point(68, 362);
            this.AddPinButton.Name = "AddPinButton";
            this.AddPinButton.Size = new System.Drawing.Size(75, 23);
            this.AddPinButton.TabIndex = 6;
            this.AddPinButton.Text = "Add";
            this.AddPinButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pins";
            // 
            // Designer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 397);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AddPinButton);
            this.Controls.Add(this.PinsList);
            this.Controls.Add(this.BrowseComponentImageButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DesignPanel);
            this.Controls.Add(this.ComponentsComboBox);
            this.Name = "Designer";
            this.Text = "Designer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComponentsComboBox;
        private System.Windows.Forms.Panel DesignPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BrowseComponentImageButton;
        private System.Windows.Forms.OpenFileDialog BrowseComponentImageDialog;
        private System.Windows.Forms.ListBox PinsList;
        private System.Windows.Forms.Button AddPinButton;
        private System.Windows.Forms.Label label3;
    }
}