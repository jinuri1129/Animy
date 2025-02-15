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
    public partial class SaveForm : Form
    {
        private Canvas canvas;
        public SaveForm(Canvas canvas)
        {
            InitializeComponent();
            this.canvas = canvas;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void SaveForm_Load(object sender, EventArgs e)
        {
            label1.Text = $"{canvas.Text} has been changed \n Do you want to Save?";
        }

        private void dontSaveBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancleBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            canvas.Save();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void saveAsBtn_Click(object sender, EventArgs e)
        {
            if (canvas.SaveAs())
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }

        }
    }
}
