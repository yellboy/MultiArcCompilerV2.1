using System.Windows.Forms;
namespace MultiArc_Compiler
{
    partial class Clipboard
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
            this.componentsListBox = new System.Windows.Forms.ListBox();
            this.systemPanel1 = new DragAndDropPanel();
            this.addComponentButton = new System.Windows.Forms.Button();
            this.nextClockButton = new System.Windows.Forms.Button();
            this.executeButton = new System.Windows.Forms.Button();
            this.ticksLabel = new System.Windows.Forms.Label();
            this.ticksCountLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveSystemDialog = new System.Windows.Forms.SaveFileDialog();
            this.LoadSystemButton = new System.Windows.Forms.Button();
            this.LoadSystemDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // componentsListBox
            // 
            this.componentsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.componentsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.componentsListBox.FormattingEnabled = true;
            this.componentsListBox.Location = new System.Drawing.Point(13, 39);
            this.componentsListBox.Name = "componentsListBox";
            this.componentsListBox.Size = new System.Drawing.Size(107, 264);
            this.componentsListBox.TabIndex = 0;
            this.componentsListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.componentsListBox_DrawItem);
            // 
            // systemPanel1
            // 
            this.systemPanel1.AllowDrop = true;
            this.systemPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.systemPanel1.BackColor = System.Drawing.Color.GhostWhite;
            this.systemPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.systemPanel1.Location = new System.Drawing.Point(127, 39);
            this.systemPanel1.Name = "systemPanel1";
            this.systemPanel1.Size = new System.Drawing.Size(458, 309);
            this.systemPanel1.TabIndex = 1;
            this.systemPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.systemPanel1_MouseClick);
            this.systemPanel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.systemPanel1_MouseDoubleClick);
            this.systemPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.systemPanel1_MouseMove);
            // 
            // addComponentButton
            // 
            this.addComponentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addComponentButton.Location = new System.Drawing.Point(12, 325);
            this.addComponentButton.Name = "addComponentButton";
            this.addComponentButton.Size = new System.Drawing.Size(75, 23);
            this.addComponentButton.TabIndex = 2;
            this.addComponentButton.Text = "Add";
            this.addComponentButton.UseVisualStyleBackColor = true;
            this.addComponentButton.Click += new System.EventHandler(this.addComponentButton_Click);
            // 
            // nextClockButton
            // 
            this.nextClockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextClockButton.Location = new System.Drawing.Point(555, 354);
            this.nextClockButton.Name = "nextClockButton";
            this.nextClockButton.Size = new System.Drawing.Size(30, 23);
            this.nextClockButton.TabIndex = 3;
            this.nextClockButton.Text = ">";
            this.nextClockButton.UseVisualStyleBackColor = true;
            this.nextClockButton.Click += new System.EventHandler(this.nextClockButton_Click);
            // 
            // executeButton
            // 
            this.executeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.executeButton.Location = new System.Drawing.Point(482, 354);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(31, 23);
            this.executeButton.TabIndex = 4;
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // ticksLabel
            // 
            this.ticksLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ticksLabel.AutoSize = true;
            this.ticksLabel.Location = new System.Drawing.Point(12, 359);
            this.ticksLabel.Name = "ticksLabel";
            this.ticksLabel.Size = new System.Drawing.Size(39, 13);
            this.ticksLabel.TabIndex = 5;
            this.ticksLabel.Text = "Ticks: ";
            // 
            // ticksCountLabel
            // 
            this.ticksCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ticksCountLabel.AutoSize = true;
            this.ticksCountLabel.Location = new System.Drawing.Point(57, 359);
            this.ticksCountLabel.Name = "ticksCountLabel";
            this.ticksCountLabel.Size = new System.Drawing.Size(13, 13);
            this.ticksCountLabel.TabIndex = 6;
            this.ticksCountLabel.Text = "0";
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Location = new System.Drawing.Point(519, 354);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(30, 23);
            this.stopButton.TabIndex = 7;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 10);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveSystemDialog
            // 
            this.SaveSystemDialog.Title = "Save system";
            this.SaveSystemDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveSystemDialog_FileOk);
            // 
            // LoadSystemButton
            // 
            this.LoadSystemButton.Location = new System.Drawing.Point(94, 10);
            this.LoadSystemButton.Name = "LoadSystemButton";
            this.LoadSystemButton.Size = new System.Drawing.Size(75, 23);
            this.LoadSystemButton.TabIndex = 9;
            this.LoadSystemButton.Text = "Load";
            this.LoadSystemButton.UseVisualStyleBackColor = true;
            this.LoadSystemButton.Click += new System.EventHandler(this.LoadSystemButton_Click);
            // 
            // LoadSystemDialog
            // 
            this.LoadSystemDialog.FileName = "openFileDialog1";
            this.LoadSystemDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadSystemDialog_FileOk);
            // 
            // Clipboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 386);
            this.Controls.Add(this.LoadSystemButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.ticksCountLabel);
            this.Controls.Add(this.ticksLabel);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.nextClockButton);
            this.Controls.Add(this.addComponentButton);
            this.Controls.Add(this.systemPanel1);
            this.Controls.Add(this.componentsListBox);
            this.Name = "Clipboard";
            this.Text = "Clipboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox componentsListBox;
        private DragAndDropPanel systemPanel1;
        private System.Windows.Forms.Button addComponentButton;
        private System.Windows.Forms.Button nextClockButton;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.Label ticksLabel;
        private System.Windows.Forms.Label ticksCountLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.SaveFileDialog SaveSystemDialog;
        private System.Windows.Forms.Button LoadSystemButton;
        private System.Windows.Forms.OpenFileDialog LoadSystemDialog;


    }
}