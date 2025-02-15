namespace Animy
{
    partial class Canvas
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveProjectToolStripMenuItem = new ToolStripMenuItem();
            saveProjectAsToolStripMenuItem = new ToolStripMenuItem();
            projectInfoToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            currentFrameToolStripMenuItem = new ToolStripMenuItem();
            commandToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            imageToolStripMenuItem = new ToolStripMenuItem();
            findFlawToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, exportToolStripMenuItem, commandToolStripMenuItem, imageToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(683, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveProjectToolStripMenuItem, saveProjectAsToolStripMenuItem, projectInfoToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(56, 20);
            fileToolStripMenuItem.Text = "Project";
            // 
            // saveProjectToolStripMenuItem
            // 
            saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            saveProjectToolStripMenuItem.Size = new Size(157, 22);
            saveProjectToolStripMenuItem.Text = "Save Project";
            saveProjectToolStripMenuItem.Click += saveProjectToolStripMenuItem_Click;
            // 
            // saveProjectAsToolStripMenuItem
            // 
            saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            saveProjectAsToolStripMenuItem.Size = new Size(157, 22);
            saveProjectAsToolStripMenuItem.Text = "Save Project As";
            saveProjectAsToolStripMenuItem.Click += saveProjectAsToolStripMenuItem_Click;
            // 
            // projectInfoToolStripMenuItem
            // 
            projectInfoToolStripMenuItem.Name = "projectInfoToolStripMenuItem";
            projectInfoToolStripMenuItem.Size = new Size(157, 22);
            projectInfoToolStripMenuItem.Text = "Project Info";
            projectInfoToolStripMenuItem.Click += projectInfoToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { currentFrameToolStripMenuItem });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(53, 20);
            exportToolStripMenuItem.Text = "Export";
            // 
            // currentFrameToolStripMenuItem
            // 
            currentFrameToolStripMenuItem.Name = "currentFrameToolStripMenuItem";
            currentFrameToolStripMenuItem.Size = new Size(151, 22);
            currentFrameToolStripMenuItem.Text = "Current Frame";
            currentFrameToolStripMenuItem.Click += currentFrameToolStripMenuItem_Click;
            // 
            // commandToolStripMenuItem
            // 
            commandToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            commandToolStripMenuItem.Size = new Size(76, 20);
            commandToolStripMenuItem.Text = "Command";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(144, 22);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new Size(144, 22);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // imageToolStripMenuItem
            // 
            imageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { findFlawToolStripMenuItem });
            imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            imageToolStripMenuItem.Size = new Size(52, 20);
            imageToolStripMenuItem.Text = "Image";
            // 
            // findFlawToolStripMenuItem
            // 
            findFlawToolStripMenuItem.Name = "findFlawToolStripMenuItem";
            findFlawToolStripMenuItem.Size = new Size(180, 22);
            findFlawToolStripMenuItem.Text = "FindFlaw";
            findFlawToolStripMenuItem.Click += findFlawToolStripMenuItem_Click;
            // 
            // Canvas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 390);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Canvas";
            Text = "Canvas";
            Activated += Canvas_Activated;
            FormClosing += Canvas_FormClosing;
            Load += Canvas_Load;
            Enter += Canvas_Enter;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem currentFrameToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private ToolStripMenuItem saveProjectToolStripMenuItem;
        private ToolStripMenuItem projectInfoToolStripMenuItem;
        private ToolStripMenuItem commandToolStripMenuItem;
        public ToolStripMenuItem undoToolStripMenuItem;
        public ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem imageToolStripMenuItem;
        private ToolStripMenuItem findFlawToolStripMenuItem;
    }
}