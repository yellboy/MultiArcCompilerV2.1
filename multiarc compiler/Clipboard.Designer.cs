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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clipboard));
            this.componentsListBox = new System.Windows.Forms.ListBox();
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
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.frequencyInput = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.DrawBusButton = new System.Windows.Forms.Button();
            this.systemPanel1 = new MultiArc_Compiler.DragAndDropPanel();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyInput)).BeginInit();
            this.SuspendLayout();
            // 
            // componentsListBox
            // 
            this.componentsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.componentsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.componentsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.componentsListBox.FormattingEnabled = true;
            this.componentsListBox.Location = new System.Drawing.Point(13, 39);
            this.componentsListBox.Name = "componentsListBox";
            this.componentsListBox.Size = new System.Drawing.Size(107, 249);
            this.componentsListBox.TabIndex = 0;
            this.componentsListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.componentsListBox_DrawItem);
            this.componentsListBox.SelectedIndexChanged += new System.EventHandler(this.componentsListBox_SelectedIndexChanged);
            // 
            // addComponentButton
            // 
            this.addComponentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addComponentButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addComponentButton.Location = new System.Drawing.Point(12, 294);
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
            this.nextClockButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextClockButton.BackgroundImage")));
            this.nextClockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.nextClockButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.nextClockButton.Location = new System.Drawing.Point(535, 356);
            this.nextClockButton.Name = "nextClockButton";
            this.nextClockButton.Size = new System.Drawing.Size(22, 22);
            this.nextClockButton.TabIndex = 3;
            this.tooltip.SetToolTip(this.nextClockButton, "Next step");
            this.nextClockButton.UseVisualStyleBackColor = true;
            this.nextClockButton.Click += new System.EventHandler(this.nextClockButton_Click);
            // 
            // executeButton
            // 
            this.executeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.executeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("executeButton.BackgroundImage")));
            this.executeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.executeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.executeButton.Location = new System.Drawing.Point(507, 356);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(22, 22);
            this.executeButton.TabIndex = 4;
            this.executeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tooltip.SetToolTip(this.executeButton, "Execute");
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // ticksLabel
            // 
            this.ticksLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ticksLabel.AutoSize = true;
            this.ticksLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ticksLabel.Location = new System.Drawing.Point(12, 361);
            this.ticksLabel.Name = "ticksLabel";
            this.ticksLabel.Size = new System.Drawing.Size(69, 13);
            this.ticksLabel.TabIndex = 5;
            this.ticksLabel.Text = "System ticks:";
            // 
            // ticksCountLabel
            // 
            this.ticksCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ticksCountLabel.AutoSize = true;
            this.ticksCountLabel.Location = new System.Drawing.Point(91, 361);
            this.ticksCountLabel.Name = "ticksCountLabel";
            this.ticksCountLabel.Size = new System.Drawing.Size(13, 13);
            this.ticksCountLabel.TabIndex = 6;
            this.ticksCountLabel.Text = "0";
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stopButton.BackgroundImage")));
            this.stopButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stopButton.Location = new System.Drawing.Point(563, 356);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(22, 22);
            this.stopButton.TabIndex = 7;
            this.tooltip.SetToolTip(this.stopButton, "Stop execution");
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.SaveSystemDialog.DefaultExt = "sys";
            this.SaveSystemDialog.Filter = "sys files|*.sys";
            this.SaveSystemDialog.Title = "Save system";
            this.SaveSystemDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveSystemDialog_FileOk);
            // 
            // LoadSystemButton
            // 
            this.LoadSystemButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.LoadSystemDialog.DefaultExt = "sys";
            this.LoadSystemDialog.Filter = "sys files|*.sys|all files|*.*";
            this.LoadSystemDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadSystemDialog_FileOk);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ticks frequency";
            // 
            // frequencyInput
            // 
            this.frequencyInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.frequencyInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frequencyInput.Location = new System.Drawing.Point(213, 356);
            this.frequencyInput.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.frequencyInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frequencyInput.Name = "frequencyInput";
            this.frequencyInput.Size = new System.Drawing.Size(120, 20);
            this.frequencyInput.TabIndex = 11;
            this.frequencyInput.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.frequencyInput.ValueChanged += new System.EventHandler(this.frequencyInput_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Hz";
            // 
            // DrawBusButton
            // 
            this.DrawBusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DrawBusButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DrawBusButton.Location = new System.Drawing.Point(13, 324);
            this.DrawBusButton.Name = "DrawBusButton";
            this.DrawBusButton.Size = new System.Drawing.Size(75, 23);
            this.DrawBusButton.TabIndex = 13;
            this.DrawBusButton.Text = "Draw bus";
            this.DrawBusButton.UseVisualStyleBackColor = true;
            this.DrawBusButton.Click += new System.EventHandler(this.DrawBusButton_Click);
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
            this.systemPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.systemPanel1_MouseMove);
            // 
            // Clipboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 386);
            this.Controls.Add(this.DrawBusButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.frequencyInput);
            this.Controls.Add(this.label1);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(613, 424);
            this.Name = "Clipboard";
            this.Text = "System";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Clipboard_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.frequencyInput)).EndInit();
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
        private ToolTip tooltip;
        private Label label1;
        private NumericUpDown frequencyInput;
        private Label label2;
        private Button DrawBusButton;


    }
}