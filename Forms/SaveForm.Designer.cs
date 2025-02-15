namespace Animy.Forms
{
    partial class SaveForm
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
            saveBtn = new Button();
            saveAsBtn = new Button();
            dontSaveBtn = new Button();
            cancleBtn = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(99, 108);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(81, 36);
            saveBtn.TabIndex = 0;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // saveAsBtn
            // 
            saveAsBtn.Location = new Point(12, 108);
            saveAsBtn.Name = "saveAsBtn";
            saveAsBtn.Size = new Size(81, 36);
            saveAsBtn.TabIndex = 1;
            saveAsBtn.Text = "Save_As";
            saveAsBtn.UseVisualStyleBackColor = true;
            saveAsBtn.Click += saveAsBtn_Click;
            // 
            // dontSaveBtn
            // 
            dontSaveBtn.Location = new Point(186, 108);
            dontSaveBtn.Name = "dontSaveBtn";
            dontSaveBtn.Size = new Size(81, 36);
            dontSaveBtn.TabIndex = 2;
            dontSaveBtn.Text = "Don't_Save";
            dontSaveBtn.UseVisualStyleBackColor = true;
            dontSaveBtn.Click += dontSaveBtn_Click;
            // 
            // cancleBtn
            // 
            cancleBtn.Location = new Point(273, 108);
            cancleBtn.Name = "cancleBtn";
            cancleBtn.Size = new Size(81, 36);
            cancleBtn.TabIndex = 3;
            cancleBtn.Text = "Cancle";
            cancleBtn.UseVisualStyleBackColor = true;
            cancleBtn.Click += cancleBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 53);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // SaveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(371, 156);
            Controls.Add(label1);
            Controls.Add(cancleBtn);
            Controls.Add(dontSaveBtn);
            Controls.Add(saveAsBtn);
            Controls.Add(saveBtn);
            Name = "SaveForm";
            Text = "SaveForm";
            Load += SaveForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button saveBtn;
        private Button saveAsBtn;
        private Button dontSaveBtn;
        private Button cancleBtn;
        private Label label1;
    }
}