namespace Animy
{
    partial class ProjectSetup
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            nameBox = new TextBox();
            pathBox = new TextBox();
            browseBtn = new Button();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            widthBox = new TextBox();
            heightBox = new TextBox();
            fpsCombo = new ComboBox();
            CreateBtn = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Location = new Point(4, 9);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 0;
            label1.Text = "Project Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Location = new Point(4, 55);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 1;
            label2.Text = "Project Path";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Location = new Point(4, 106);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 2;
            label3.Text = "Project Setting";
            // 
            // nameBox
            // 
            nameBox.Location = new Point(4, 27);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(242, 23);
            nameBox.TabIndex = 3;
            nameBox.TextChanged += nameBox_TextChanged;
            // 
            // pathBox
            // 
            pathBox.Location = new Point(4, 73);
            pathBox.Name = "pathBox";
            pathBox.Size = new Size(179, 23);
            pathBox.TabIndex = 4;
            pathBox.TextChanged += pathBox_TextChanged;
            // 
            // browseBtn
            // 
            browseBtn.Location = new Point(189, 73);
            browseBtn.Name = "browseBtn";
            browseBtn.Size = new Size(57, 23);
            browseBtn.TabIndex = 5;
            browseBtn.Text = "browse";
            browseBtn.UseVisualStyleBackColor = true;
            browseBtn.Click += browseBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Location = new Point(4, 132);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 6;
            label4.Text = "Width";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Control;
            label5.Location = new Point(4, 162);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 7;
            label5.Text = "Height";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.Control;
            label6.Location = new Point(152, 162);
            label6.Name = "label6";
            label6.Size = new Size(23, 15);
            label6.TabIndex = 8;
            label6.Text = "fps";
            // 
            // widthBox
            // 
            widthBox.Location = new Point(49, 129);
            widthBox.Name = "widthBox";
            widthBox.Size = new Size(66, 23);
            widthBox.TabIndex = 9;
            widthBox.TextChanged += widthBox_TextChanged;
            // 
            // heightBox
            // 
            heightBox.Location = new Point(49, 159);
            heightBox.Name = "heightBox";
            heightBox.Size = new Size(66, 23);
            heightBox.TabIndex = 10;
            heightBox.TextChanged += heightBox_TextChanged;
            // 
            // fpsCombo
            // 
            fpsCombo.FormattingEnabled = true;
            fpsCombo.Items.AddRange(new object[] { "24", "30" });
            fpsCombo.Location = new Point(181, 159);
            fpsCombo.Name = "fpsCombo";
            fpsCombo.Size = new Size(66, 23);
            fpsCombo.TabIndex = 11;
            fpsCombo.SelectedIndexChanged += fpsCombo_SelectedIndexChanged;
            // 
            // CreateBtn
            // 
            CreateBtn.Location = new Point(97, 198);
            CreateBtn.Name = "CreateBtn";
            CreateBtn.Size = new Size(78, 23);
            CreateBtn.TabIndex = 12;
            CreateBtn.Text = "Create";
            CreateBtn.UseVisualStyleBackColor = true;
            CreateBtn.Click += CreateBtn_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // ProjectSetup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.MenuBar;
            ClientSize = new Size(259, 233);
            Controls.Add(CreateBtn);
            Controls.Add(fpsCombo);
            Controls.Add(heightBox);
            Controls.Add(widthBox);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(browseBtn);
            Controls.Add(pathBox);
            Controls.Add(nameBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ProjectSetup";
            Text = "ProjectSetup";
            Load += ProjectSetup_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox nameBox;
        private TextBox pathBox;
        private Button browseBtn;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox widthBox;
        private TextBox heightBox;
        private ComboBox fpsCombo;
        private Button CreateBtn;
        private System.Windows.Forms.Timer timer1;
    }
}