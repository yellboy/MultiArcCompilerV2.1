namespace MultiArc_Compiler
{
    partial class Form1
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
            if (ex != null)
            {
                ex.StopDebugging();
                ex.Abort();
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LoadFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.BinSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.BinLoadFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.BinFileNameLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.architectureToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recompileCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assembleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeWithoutDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopDebuggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadArchitectureDialog = new System.Windows.Forms.OpenFileDialog();
            this.OutputBox = new System.Windows.Forms.RichTextBox();
            this.outputLabel = new System.Windows.Forms.Label();
            this.clearOutputButton = new System.Windows.Forms.Button();
            this.NewProjectButton = new System.Windows.Forms.Button();
            this.NewProjectTip = new System.Windows.Forms.ToolTip(this.components);
            this.NewFileButton = new System.Windows.Forms.Button();
            this.NewFileTip = new System.Windows.Forms.ToolTip(this.components);
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.OpenFileTip = new System.Windows.Forms.ToolTip(this.components);
            this.SaveFileButton = new System.Windows.Forms.Button();
            this.SaveFileTip = new System.Windows.Forms.ToolTip(this.components);
            this.LoadArcButton = new System.Windows.Forms.Button();
            this.LoadArcTip = new System.Windows.Forms.ToolTip(this.components);
            this.DebugButton = new System.Windows.Forms.Button();
            this.DebugTip = new System.Windows.Forms.ToolTip(this.components);
            this.NewProjectDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenProjectDialog = new System.Windows.Forms.OpenFileDialog();
            this.ClearOutputTip = new System.Windows.Forms.ToolTip(this.components);
            this.ToggleBrekpointTip = new System.Windows.Forms.ToolTip(this.components);
            this.LoadMemoryDialog = new System.Windows.Forms.OpenFileDialog();
            this.CodeBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BinaryCodeBox = new System.Windows.Forms.RichTextBox();
            this.BinaryCodeLabel = new System.Windows.Forms.Label();
            this.lineNumbers_For_RichTextBox2 = new LineNumbers.LineNumbers_For_RichTextBox();
            this.LoadOtherComponentDialog = new System.Windows.Forms.OpenFileDialog();
            this.designerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadFileDialog
            // 
            this.LoadFileDialog.DefaultExt = "as";
            this.LoadFileDialog.Filter = "as files|*.as; *.asm|all files|*.*";
            this.LoadFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadFileDialog_FileOk);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "as";
            this.SaveFileDialog.Filter = "|as files|*.as|asm files|*.asm|";
            this.SaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog_FileOk);
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(54, 6);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.FileNameLabel.TabIndex = 14;
            // 
            // BinLoadFileDialog
            // 
            this.BinLoadFileDialog.FileName = "openFileDialog1";
            // 
            // BinFileNameLabel
            // 
            this.BinFileNameLabel.AutoSize = true;
            this.BinFileNameLabel.Location = new System.Drawing.Point(505, 6);
            this.BinFileNameLabel.Name = "BinFileNameLabel";
            this.BinFileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.BinFileNameLabel.TabIndex = 21;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.architectureToolStripMenuItem1,
            this.debugToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(829, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.fileToolStripMenuItem1});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.projectToolStripMenuItem.Text = "Project";
            this.projectToolStripMenuItem.Click += new System.EventHandler(this.projectToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.fileToolStripMenuItem1.Text = "File";
            this.fileToolStripMenuItem1.Click += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem1,
            this.fileToolStripMenuItem2});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // projectToolStripMenuItem1
            // 
            this.projectToolStripMenuItem1.Name = "projectToolStripMenuItem1";
            this.projectToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.projectToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.projectToolStripMenuItem1.Text = "Project";
            this.projectToolStripMenuItem1.Click += new System.EventHandler(this.projectToolStripMenuItem1_Click);
            // 
            // fileToolStripMenuItem2
            // 
            this.fileToolStripMenuItem2.Name = "fileToolStripMenuItem2";
            this.fileToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileToolStripMenuItem2.Size = new System.Drawing.Size(186, 22);
            this.fileToolStripMenuItem2.Text = "File";
            this.fileToolStripMenuItem2.Click += new System.EventHandler(this.LoadFileBrowseButton_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveFileAsButton_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memoryDumpToolStripMenuItem,
            this.registersToolStripMenuItem,
            this.systemToolStripMenuItem,
            this.designerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // memoryDumpToolStripMenuItem
            // 
            this.memoryDumpToolStripMenuItem.Name = "memoryDumpToolStripMenuItem";
            this.memoryDumpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.memoryDumpToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.memoryDumpToolStripMenuItem.Text = "Memory dump";
            this.memoryDumpToolStripMenuItem.Click += new System.EventHandler(this.memoryDumpToolStripMenuItem_Click);
            // 
            // registersToolStripMenuItem
            // 
            this.registersToolStripMenuItem.Name = "registersToolStripMenuItem";
            this.registersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.registersToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.registersToolStripMenuItem.Text = "Registers";
            this.registersToolStripMenuItem.Click += new System.EventHandler(this.registersToolStripMenuItem_Click);
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.systemToolStripMenuItem.Text = "System";
            this.systemToolStripMenuItem.Click += new System.EventHandler(this.systemToolStripMenuItem_Click);
            // 
            // architectureToolStripMenuItem1
            // 
            this.architectureToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.recompileCodeToolStripMenuItem});
            this.architectureToolStripMenuItem1.Name = "architectureToolStripMenuItem1";
            this.architectureToolStripMenuItem1.Size = new System.Drawing.Size(84, 20);
            this.architectureToolStripMenuItem1.Text = "Architecture";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPUToolStripMenuItem,
            this.memoryToolStripMenuItem,
            this.otherComponentToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // cPUToolStripMenuItem
            // 
            this.cPUToolStripMenuItem.Name = "cPUToolStripMenuItem";
            this.cPUToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.cPUToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.cPUToolStripMenuItem.Text = "CPU";
            this.cPUToolStripMenuItem.Click += new System.EventHandler(this.cPUToolStripMenuItem_Click);
            // 
            // memoryToolStripMenuItem
            // 
            this.memoryToolStripMenuItem.Name = "memoryToolStripMenuItem";
            this.memoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.memoryToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.memoryToolStripMenuItem.Text = "Memory";
            this.memoryToolStripMenuItem.Click += new System.EventHandler(this.memoryToolStripMenuItem_Click);
            // 
            // otherComponentToolStripMenuItem
            // 
            this.otherComponentToolStripMenuItem.Name = "otherComponentToolStripMenuItem";
            this.otherComponentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.otherComponentToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.otherComponentToolStripMenuItem.Text = "Other Component";
            this.otherComponentToolStripMenuItem.Click += new System.EventHandler(this.otherComponentToolStripMenuItem_Click);
            // 
            // recompileCodeToolStripMenuItem
            // 
            this.recompileCodeToolStripMenuItem.Name = "recompileCodeToolStripMenuItem";
            this.recompileCodeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.recompileCodeToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.recompileCodeToolStripMenuItem.Text = "Recompile code";
            this.recompileCodeToolStripMenuItem.Click += new System.EventHandler(this.recompileCodeToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assembleToolStripMenuItem,
            this.executeToolStripMenuItem,
            this.executeWithoutDebugToolStripMenuItem,
            this.nextStepToolStripMenuItem,
            this.stopDebuggingToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // assembleToolStripMenuItem
            // 
            this.assembleToolStripMenuItem.Name = "assembleToolStripMenuItem";
            this.assembleToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.assembleToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.assembleToolStripMenuItem.Text = "Assemble ";
            this.assembleToolStripMenuItem.Click += new System.EventHandler(this.assembleButton_Click);
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.executeToolStripMenuItem.Text = "Execute";
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // executeWithoutDebugToolStripMenuItem
            // 
            this.executeWithoutDebugToolStripMenuItem.Name = "executeWithoutDebugToolStripMenuItem";
            this.executeWithoutDebugToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.executeWithoutDebugToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.executeWithoutDebugToolStripMenuItem.Text = "Execute without debugging";
            this.executeWithoutDebugToolStripMenuItem.Click += new System.EventHandler(this.executeWithoutDebugToolStripMenuItem_Click);
            // 
            // nextStepToolStripMenuItem
            // 
            this.nextStepToolStripMenuItem.Name = "nextStepToolStripMenuItem";
            this.nextStepToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.nextStepToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.nextStepToolStripMenuItem.Text = "Next step";
            this.nextStepToolStripMenuItem.Click += new System.EventHandler(this.nextStep);
            // 
            // stopDebuggingToolStripMenuItem
            // 
            this.stopDebuggingToolStripMenuItem.Name = "stopDebuggingToolStripMenuItem";
            this.stopDebuggingToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.stopDebuggingToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.stopDebuggingToolStripMenuItem.Text = "Stop debugging";
            this.stopDebuggingToolStripMenuItem.Click += new System.EventHandler(this.stopDebuggingToolStripMenuItem_Click);
            // 
            // LoadArchitectureDialog
            // 
            this.LoadArchitectureDialog.DefaultExt = "arc";
            this.LoadArchitectureDialog.Filter = "arc files|*.arc|all files|*.*";
            this.LoadArchitectureDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadArchitecture);
            // 
            // OutputBox
            // 
            this.OutputBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OutputBox.Location = new System.Drawing.Point(12, 420);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(807, 118);
            this.OutputBox.TabIndex = 24;
            this.OutputBox.Text = "";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(9, 404);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(39, 13);
            this.outputLabel.TabIndex = 25;
            this.outputLabel.Text = "Output";
            // 
            // clearOutputButton
            // 
            this.clearOutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearOutputButton.Image = ((System.Drawing.Image)(resources.GetObject("clearOutputButton.Image")));
            this.clearOutputButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.clearOutputButton.Location = new System.Drawing.Point(774, 400);
            this.clearOutputButton.Name = "clearOutputButton";
            this.clearOutputButton.Size = new System.Drawing.Size(45, 22);
            this.clearOutputButton.TabIndex = 26;
            this.clearOutputButton.Text = "Clear";
            this.clearOutputButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ClearOutputTip.SetToolTip(this.clearOutputButton, "Clear output");
            this.clearOutputButton.UseVisualStyleBackColor = true;
            this.clearOutputButton.Click += new System.EventHandler(this.clearOutputButton_Click);
            // 
            // NewProjectButton
            // 
            this.NewProjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewProjectButton.Image = ((System.Drawing.Image)(resources.GetObject("NewProjectButton.Image")));
            this.NewProjectButton.Location = new System.Drawing.Point(12, 27);
            this.NewProjectButton.Name = "NewProjectButton";
            this.NewProjectButton.Size = new System.Drawing.Size(22, 22);
            this.NewProjectButton.TabIndex = 28;
            this.NewProjectTip.SetToolTip(this.NewProjectButton, "New project");
            this.NewProjectButton.UseVisualStyleBackColor = true;
            // 
            // NewFileButton
            // 
            this.NewFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewFileButton.Image = ((System.Drawing.Image)(resources.GetObject("NewFileButton.Image")));
            this.NewFileButton.Location = new System.Drawing.Point(40, 27);
            this.NewFileButton.Name = "NewFileButton";
            this.NewFileButton.Size = new System.Drawing.Size(22, 22);
            this.NewFileButton.TabIndex = 29;
            this.NewFileTip.SetToolTip(this.NewFileButton, "New file");
            this.NewFileButton.UseVisualStyleBackColor = true;
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OpenFileButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenFileButton.Image")));
            this.OpenFileButton.Location = new System.Drawing.Point(69, 27);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(22, 22);
            this.OpenFileButton.TabIndex = 30;
            this.OpenFileTip.SetToolTip(this.OpenFileButton, "Open file");
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.LoadFileBrowseButton_Click);
            // 
            // SaveFileButton
            // 
            this.SaveFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveFileButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveFileButton.Image")));
            this.SaveFileButton.Location = new System.Drawing.Point(98, 27);
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(22, 22);
            this.SaveFileButton.TabIndex = 31;
            this.SaveFileTip.SetToolTip(this.SaveFileButton, "Save file");
            this.SaveFileButton.UseVisualStyleBackColor = true;
            this.SaveFileButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // LoadArcButton
            // 
            this.LoadArcButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoadArcButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadArcButton.Image")));
            this.LoadArcButton.Location = new System.Drawing.Point(127, 27);
            this.LoadArcButton.Name = "LoadArcButton";
            this.LoadArcButton.Size = new System.Drawing.Size(22, 22);
            this.LoadArcButton.TabIndex = 32;
            this.LoadArcTip.SetToolTip(this.LoadArcButton, "Load architecture");
            this.LoadArcButton.UseVisualStyleBackColor = true;
            // 
            // DebugButton
            // 
            this.DebugButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DebugButton.Image = ((System.Drawing.Image)(resources.GetObject("DebugButton.Image")));
            this.DebugButton.Location = new System.Drawing.Point(155, 27);
            this.DebugButton.Name = "DebugButton";
            this.DebugButton.Size = new System.Drawing.Size(22, 22);
            this.DebugButton.TabIndex = 33;
            this.DebugTip.SetToolTip(this.DebugButton, "Execute");
            this.DebugButton.UseVisualStyleBackColor = true;
            this.DebugButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // NewProjectDialog
            // 
            this.NewProjectDialog.DefaultExt = "prj";
            this.NewProjectDialog.Filter = "prj files|*.prj";
            this.NewProjectDialog.InitialDirectory = "\\Projects";
            this.NewProjectDialog.RestoreDirectory = true;
            this.NewProjectDialog.Title = "Create new project";
            this.NewProjectDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.NewProjectDialog_FileOk);
            // 
            // OpenProjectDialog
            // 
            this.OpenProjectDialog.DefaultExt = "prj";
            this.OpenProjectDialog.Filter = "prj files|*.prj|all files|*.*";
            this.OpenProjectDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenProjectDialog_FileOk);
            // 
            // LoadMemoryDialog
            // 
            this.LoadMemoryDialog.Filter = "arc files|*.arc|all files|*.*";
            this.LoadMemoryDialog.Title = "Load memory";
            this.LoadMemoryDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadMemoryDialog_FileOk);
            // 
            // CodeBox
            // 
            this.CodeBox.AcceptsTab = true;
            this.CodeBox.BackColor = System.Drawing.SystemColors.Window;
            this.CodeBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CodeBox.Location = new System.Drawing.Point(30, 74);
            this.CodeBox.Name = "CodeBox";
            this.CodeBox.Size = new System.Drawing.Size(386, 322);
            this.CodeBox.TabIndex = 40;
            this.CodeBox.TabStop = false;
            this.CodeBox.Text = "   ";
            this.CodeBox.TextChanged += new System.EventHandler(this.CodeBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Code";
            // 
            // BinaryCodeBox
            // 
            this.BinaryCodeBox.AcceptsTab = true;
            this.BinaryCodeBox.BackColor = System.Drawing.SystemColors.Window;
            this.BinaryCodeBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BinaryCodeBox.Location = new System.Drawing.Point(432, 74);
            this.BinaryCodeBox.Name = "BinaryCodeBox";
            this.BinaryCodeBox.ReadOnly = true;
            this.BinaryCodeBox.Size = new System.Drawing.Size(385, 322);
            this.BinaryCodeBox.TabIndex = 41;
            this.BinaryCodeBox.TabStop = false;
            this.BinaryCodeBox.Text = "";
            // 
            // BinaryCodeLabel
            // 
            this.BinaryCodeLabel.AutoSize = true;
            this.BinaryCodeLabel.Location = new System.Drawing.Point(429, 61);
            this.BinaryCodeLabel.Name = "BinaryCodeLabel";
            this.BinaryCodeLabel.Size = new System.Drawing.Size(64, 13);
            this.BinaryCodeLabel.TabIndex = 39;
            this.BinaryCodeLabel.Text = "Binary Code";
            // 
            // lineNumbers_For_RichTextBox2
            // 
            this.lineNumbers_For_RichTextBox2._SeeThroughMode_ = false;
            this.lineNumbers_For_RichTextBox2.AutoSizing = true;
            this.lineNumbers_For_RichTextBox2.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lineNumbers_For_RichTextBox2.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbers_For_RichTextBox2.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbers_For_RichTextBox2.BorderLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox2.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox2.BorderLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox2.DockSide = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
            this.lineNumbers_For_RichTextBox2.GridLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox2.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox2.GridLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox2.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbers_For_RichTextBox2.LineNrs_AntiAlias = true;
            this.lineNumbers_For_RichTextBox2.LineNrs_AsHexadecimal = false;
            this.lineNumbers_For_RichTextBox2.LineNrs_ClippedByItemRectangle = true;
            this.lineNumbers_For_RichTextBox2.LineNrs_LeadingZeroes = true;
            this.lineNumbers_For_RichTextBox2.LineNrs_Offset = new System.Drawing.Size(0, 0);
            this.lineNumbers_For_RichTextBox2.Location = new System.Drawing.Point(11, 74);
            this.lineNumbers_For_RichTextBox2.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbers_For_RichTextBox2.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox2.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
            this.lineNumbers_For_RichTextBox2.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbers_For_RichTextBox2.MarginLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox2.Name = "lineNumbers_For_RichTextBox2";
            this.lineNumbers_For_RichTextBox2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbers_For_RichTextBox2.ParentRichTextBox = this.CodeBox;
            this.lineNumbers_For_RichTextBox2.Show_BackgroundGradient = true;
            this.lineNumbers_For_RichTextBox2.Show_BorderLines = true;
            this.lineNumbers_For_RichTextBox2.Show_GridLines = true;
            this.lineNumbers_For_RichTextBox2.Show_LineNrs = true;
            this.lineNumbers_For_RichTextBox2.Show_MarginLines = true;
            this.lineNumbers_For_RichTextBox2.Size = new System.Drawing.Size(18, 322);
            this.lineNumbers_For_RichTextBox2.TabIndex = 43;
            this.lineNumbers_For_RichTextBox2.Click += new System.EventHandler(this.lineNumbers_For_RichTextBox1_Click);
            // 
            // LoadOtherComponentDialog
            // 
            this.LoadOtherComponentDialog.DefaultExt = "arc files|*.arc|all files|*.*";
            this.LoadOtherComponentDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadOtherComponentDialog_FileOk);
            // 
            // designerToolStripMenuItem
            // 
            this.designerToolStripMenuItem.Name = "designerToolStripMenuItem";
            this.designerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.designerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.designerToolStripMenuItem.Text = "Designer";
            this.designerToolStripMenuItem.Click += new System.EventHandler(this.designerToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 543);
            this.Controls.Add(this.lineNumbers_For_RichTextBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BinaryCodeBox);
            this.Controls.Add(this.CodeBox);
            this.Controls.Add(this.BinaryCodeLabel);
            this.Controls.Add(this.DebugButton);
            this.Controls.Add(this.LoadArcButton);
            this.Controls.Add(this.SaveFileButton);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.NewFileButton);
            this.Controls.Add(this.NewProjectButton);
            this.Controls.Add(this.clearOutputButton);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.BinFileNameLabel);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "MultiArc Compiler";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog LoadFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.SaveFileDialog BinSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog BinLoadFileDialog;
        private System.Windows.Forms.Label BinFileNameLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog LoadArchitectureDialog;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryDumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registersToolStripMenuItem;
        private System.Windows.Forms.RichTextBox OutputBox;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Button clearOutputButton;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assembleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeWithoutDebugToolStripMenuItem;
        private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem architectureToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recompileCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Button NewProjectButton;
        private System.Windows.Forms.ToolTip NewProjectTip;
        private System.Windows.Forms.Button NewFileButton;
        private System.Windows.Forms.ToolTip NewFileTip;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.ToolTip OpenFileTip;
        private System.Windows.Forms.Button SaveFileButton;
        private System.Windows.Forms.ToolTip SaveFileTip;
        private System.Windows.Forms.Button LoadArcButton;
        private System.Windows.Forms.ToolTip LoadArcTip;
        private System.Windows.Forms.Button DebugButton;
        private System.Windows.Forms.ToolTip DebugTip;
        private System.Windows.Forms.SaveFileDialog NewProjectDialog;
        private System.Windows.Forms.OpenFileDialog OpenProjectDialog;
        private System.Windows.Forms.ToolStripMenuItem stopDebuggingToolStripMenuItem;
        private System.Windows.Forms.ToolTip ClearOutputTip;
        private System.Windows.Forms.ToolTip ToggleBrekpointTip;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog LoadMemoryDialog;
        private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox2;
        private System.Windows.Forms.RichTextBox CodeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox BinaryCodeBox;
        private System.Windows.Forms.Label BinaryCodeLabel;
        private System.Windows.Forms.ToolStripMenuItem otherComponentToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog LoadOtherComponentDialog;
        private System.Windows.Forms.ToolStripMenuItem designerToolStripMenuItem;

    }
}

