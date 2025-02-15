using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animy.Forms
{
    public partial class StartUp : Form
    {
        public StartUp()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void StartUp_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("images/version_logo.png");
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
