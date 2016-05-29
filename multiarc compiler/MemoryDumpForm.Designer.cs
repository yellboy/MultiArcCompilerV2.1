namespace MultiArc_Compiler
{
    partial class MemoryDumpForm
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
            this.Visible = false;
            //base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoryDumpForm));
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.GotoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // prevButton
            // 
            this.prevButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.prevButton.Location = new System.Drawing.Point(13, 276);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(75, 23);
            this.prevButton.TabIndex = 0;
            this.prevButton.Text = "Prev 100";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.nextButton.Location = new System.Drawing.Point(332, 276);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next 100";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.updateButton.Location = new System.Drawing.Point(522, 276);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // AddressBox
            // 
            this.AddressBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressBox.Location = new System.Drawing.Point(145, 278);
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.Size = new System.Drawing.Size(100, 20);
            this.AddressBox.TabIndex = 3;
            this.AddressBox.TextChanged += new System.EventHandler(this.AddressBox_TextChanged);
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(94, 281);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(45, 13);
            this.AddressLabel.TabIndex = 4;
            this.AddressLabel.Text = "Address";
            // 
            // GotoButton
            // 
            this.GotoButton.Enabled = false;
            this.GotoButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GotoButton.Location = new System.Drawing.Point(251, 276);
            this.GotoButton.Name = "GotoButton";
            this.GotoButton.Size = new System.Drawing.Size(75, 23);
            this.GotoButton.TabIndex = 5;
            this.GotoButton.Text = "Goto";
            this.GotoButton.UseVisualStyleBackColor = true;
            this.GotoButton.Click += new System.EventHandler(this.GotoButton_Click);
            // 
            // MemoryDumpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 311);
            this.Controls.Add(this.GotoButton);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.AddressBox);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.prevButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MemoryDumpForm";
            this.Text = "MemoryDumpForm";
            this.VisibleChanged += new System.EventHandler(this.MemoryDumpForm_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.TextBox AddressBox;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.Button GotoButton;

    }
}