using ColorHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Animy
{
    public partial class ColorPicker : DockContent
    {

        private HSV hsv;
        private RGB rgb;
        bool rgbMode = false;

        bool isGradientBoxClicked;


        public ColorPicker()
        {
            InitializeComponent();
        }
        //===================//
        // form functions    //
        //===================//

        private void ColorPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;

        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {
            this.AutoScroll = true;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CloseButtonVisible = false;
            

            hsv = new HSV(0, 0, 0);
            rgb = new RGB(0, 0, 0);

            trackBar1.Maximum = 360;
            trackBar1.Minimum = 0;
            trackBar1.Value = 0;

            trackBar2.Maximum = 100;
            trackBar2.Minimum = 0;
            trackBar2.Value = 0;


            trackBar3.Maximum = 100;
            trackBar3.Minimum = 0;
            trackBar3.Value = 0;


            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;

            label1.Text = "H";
            label2.Text = "S";
            label3.Text = "V";

            label4.Text = "R";
            label5.Text = "G";
            label6.Text = "B";
            checkBox1.Text = "RGB Mode";

            UpdateColor();
        }

        //===================//
        // general functions //
        //===================//
        public void UpdateColor()
        {
            if (!rgbMode) //hsv mode
            {

                hsv = new HSV(trackBar1.Value, (byte)trackBar2.Value, (byte)trackBar3.Value);
                rgb = ColorHelper.ColorConverter.HsvToRgb(hsv);
                pictureBox1.BackColor = Color.FromArgb(rgb.R, rgb.G, rgb.B);
                Console.WriteLine(rgb.ToString());

                textBox1.Text = hsv.H.ToString();
                textBox2.Text = hsv.S.ToString();
                textBox3.Text = hsv.V.ToString();


                textBox4.Text = rgb.R.ToString();
                textBox5.Text = rgb.G.ToString();
                textBox6.Text = rgb.B.ToString();
            }


            else //rgb mode
            {
                rgb = new RGB((byte)trackBar1.Value, (byte)trackBar2.Value, (byte)trackBar3.Value);
                hsv = ColorHelper.ColorConverter.RgbToHsv(rgb);
                pictureBox1.BackColor = Color.FromArgb(rgb.R, rgb.G, rgb.B);

                textBox1.Text = rgb.R.ToString();
                textBox2.Text = rgb.G.ToString();
                textBox3.Text = rgb.B.ToString();

                textBox4.Text = hsv.H.ToString();
                textBox5.Text = hsv.S.ToString();
                textBox6.Text = hsv.V.ToString();

            }

            pictureBox2.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
        }
        public void SetColor(Color col)
        {

            if (col.A == 0)
            {
                col = Color.White;
            }


            rgb.R = col.R;
            rgb.G = col.G;
            rgb.B = col.B;
            Console.WriteLine(rgb.ToString());

            HSV hsv = ColorHelper.ColorConverter.RgbToHsv(rgb);
            this.hsv = hsv;

            if(rgbMode)
            {
                trackBar1.Value = rgb.R;
                trackBar2.Value = rgb.G;
                trackBar3.Value = rgb.B;
            }
            else
            {
                trackBar1.Value = this.hsv.H;
                trackBar2.Value = this.hsv.S;
                trackBar3.Value = this.hsv.V;

            }

        }
        public Color GetColor()
        {
            return Color.FromArgb(255,rgb.R, rgb.G, rgb.B);
        }

        //===================//
        // controls functions//
        //===================//
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            UpdateColor();

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            UpdateColor();

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            UpdateColor();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (!checkBox1.Checked)//hsv mode
            {
                button1max.Visible = false;
                button1min.Visible = false;

                rgbMode = false;
                label1.Text = "H";
                label2.Text = "S";
                label3.Text = "V";

                label4.Text = "R";
                label5.Text = "G";
                label6.Text = "B";

                trackBar1.Maximum = 360;
                trackBar2.Maximum = 100;
                trackBar3.Maximum = 100;

                trackBar1.Value = hsv.H;
                trackBar2.Value = hsv.S;
                trackBar3.Value = hsv.V;
                UpdateColor();



            }
            else // rgb mode
            {
                button1max.Visible = true;
                button1min.Visible = true;

                rgbMode = true;
                label1.Text = "R";
                label2.Text = "G";
                label3.Text = "B";

                label4.Text = "H";
                label5.Text = "S";
                label6.Text = "V";

                trackBar1.Maximum = 255;
                trackBar2.Maximum = 255;
                trackBar3.Maximum = 255;

                trackBar1.Value = rgb.R;
                trackBar2.Value = rgb.G;
                trackBar3.Value = rgb.B;
                UpdateColor();

            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!rgbMode) //hsv mode
            {
                int result;
                if (int.TryParse(textBox1.Text, out result))
                {
                    if (result > 360)
                        result = 360;
                    if (result < 0)
                        result = 0;

                    if (result <= 360 && result >= 0)
                    {
                        trackBar1.Value = result;
                        textBox1.Text = trackBar1.Value.ToString();
                        UpdateColor();
                    }
                }
            }
            else //rgb mode
            {
                int result;
                if (int.TryParse(textBox1.Text, out result))
                {
                    if (result > 255)
                        result = 255;
                    if (result < 0)
                        result = 0;

                    if (result <= 255 && result >= 0)
                    {
                        trackBar1.Value = result;
                        textBox1.Text = trackBar1.Value.ToString();
                        UpdateColor();
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!rgbMode) //hsv mode
            {
                int result;
                if (int.TryParse(textBox2.Text, out result))
                {
                    if (result > 100)
                        result = 100;
                    if (result < 0)
                        result = 0;

                    if (result <= 100 && result >= 0)
                    {
                        trackBar2.Value = result;
                        textBox2.Text = trackBar2.Value.ToString();
                        UpdateColor();
                    }
                }
            }
            else //rgb mode
            {
                int result;
                if (int.TryParse(textBox2.Text, out result))
                {
                    if (result > 255)
                        result = 255;
                    if (result < 0)
                        result = 0;

                    if (result <= 255 && result >= 0)
                    {
                        trackBar2.Value = result;
                        textBox2.Text = trackBar2.Value.ToString();
                        UpdateColor();
                    }
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!rgbMode) //hsv mode
            {
                int result;
                if (int.TryParse(textBox3.Text, out result))
                {
                    if (result > 100)
                        result = 100;
                    if (result < 0)
                        result = 0;

                    if (result <= 100 && result >= 0)
                    {
                        trackBar3.Value = result;
                        textBox3.Text = trackBar3.Value.ToString();
                        UpdateColor();
                    }
                }
            }
            else //rgb mode
            {
                int result;
                if (int.TryParse(textBox3.Text, out result))
                {
                    if (result > 255)
                        result = 255;
                    if (result < 0)
                        result = 0;

                    if (result <= 255 && result >= 0)
                    {
                        trackBar3.Value = result;
                        textBox3.Text = trackBar3.Value.ToString();
                        UpdateColor();
                    }
                }
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e) //hue or rgb r
        {
            Graphics graphics = e.Graphics;
            Rectangle area = new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height);


            if (!rgbMode)//hsv mode
            {
                for (int i = 0; i < 360; i++)
                {
                    HSV minHSV = new HSV(i, hsv.S, hsv.V);
                    RGB min = ColorHelper.ColorConverter.HsvToRgb(minHSV);


                    using (Brush brush = new SolidBrush(Color.FromArgb(min.R, min.G, min.B)))
                    {
                        graphics.FillRectangle(brush, i * 0.588f, 0, pictureBox2.Width, pictureBox2.Height);
                    }

                }
            }
            else //rgb mode
            {
                RGB min = new RGB(0,rgb.G,rgb.B);
                RGB max = new RGB(255, rgb.G, rgb.B);

                LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(min.R, min.G, min.B), Color.FromArgb(max.R, max.G, max.B), LinearGradientMode.Horizontal);
                graphics.FillRectangle(lgb, area);
            }




        }


        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle area = new Rectangle(0, 0, pictureBox3.Width, pictureBox3.Height);

            if (!rgbMode) //hsv mode
            {

                HSV minHSV = new HSV(hsv.H, 0, hsv.V);
                RGB min = ColorHelper.ColorConverter.HsvToRgb(minHSV);

                HSV maxHSV = new HSV(hsv.H, 100, hsv.V);
                RGB max = ColorHelper.ColorConverter.HsvToRgb(maxHSV);

                LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(min.R, min.G, min.B), Color.FromArgb(max.R, max.G, max.B), LinearGradientMode.Horizontal);
                graphics.FillRectangle(lgb, area);
            }
            else //rgb mode
            {
                RGB min = new RGB(rgb.R, 0, rgb.B);
                RGB max = new RGB(rgb.R, 255, rgb.B);

                LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(min.R, min.G, min.B), Color.FromArgb(max.R, max.G, max.B), LinearGradientMode.Horizontal);
                graphics.FillRectangle(lgb, area);
            }

        }


        private void pictureBox4_Paint(object sender, PaintEventArgs e) // value , rgb b
        {
            Graphics graphics = e.Graphics;
            Rectangle area = new Rectangle(0, 0, pictureBox4.Width, pictureBox4.Height);

            if (!rgbMode) //hsv mode
            {


                HSV minHSV = new HSV(hsv.H, hsv.S, 0);
                RGB min = ColorHelper.ColorConverter.HsvToRgb(minHSV);

                HSV maxHSV = new HSV(hsv.H, hsv.S, 100);
                RGB max = ColorHelper.ColorConverter.HsvToRgb(maxHSV);

                LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(min.R, min.G, min.B), Color.FromArgb(max.R, max.G, max.B), LinearGradientMode.Horizontal);
                graphics.FillRectangle(lgb, area);
            }
            else //rgb mode
            {
                RGB min = new RGB(rgb.R, rgb.G, 0);
                RGB max = new RGB(rgb.R, rgb.G, 255);

                LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(min.R, min.G, min.B), Color.FromArgb(max.R, max.G, max.B), LinearGradientMode.Horizontal);
                graphics.FillRectangle(lgb, area);
            }

        }


        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            isGradientBoxClicked = true;
            trackBar1.Focus();
            int trackBarWidth = trackBar1.Width;
            int pictureBoxWidth = pictureBox2.Width;
            int newValue = (e.X * trackBar1.Maximum) / pictureBoxWidth;
            newValue = Math.Max(trackBar1.Minimum, Math.Min(trackBar1.Maximum, newValue));
            trackBar1.Value = newValue;
            UpdateColor();

        }
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isGradientBoxClicked = false;
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isGradientBoxClicked)
            {
                trackBar1.Focus();
                int trackBarWidth = trackBar1.Width;
                int pictureBoxWidth = pictureBox2.Width;
                int newValue = (e.X * trackBar1.Maximum) / pictureBoxWidth;
                newValue = Math.Max(trackBar1.Minimum, Math.Min(trackBar1.Maximum, newValue));
                trackBar1.Value = newValue;
                UpdateColor();
            }
        }


        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            isGradientBoxClicked = true;
            trackBar2.Focus();
            int trackBarWidth = trackBar2.Width;
            int pictureBoxWidth = pictureBox3.Width;
            int newValue = (e.X * trackBar2.Maximum) / pictureBoxWidth;
            newValue = Math.Max(trackBar2.Minimum, Math.Min(trackBar2.Maximum, newValue));
            trackBar2.Value = newValue;
            UpdateColor();

        }
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            isGradientBoxClicked = false;

        }
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (isGradientBoxClicked)
            {
                trackBar2.Focus();
                int trackBarWidth = trackBar2.Width;
                int pictureBoxWidth = pictureBox3.Width;
                int newValue = (e.X * trackBar2.Maximum) / pictureBoxWidth;
                newValue = Math.Max(trackBar2.Minimum, Math.Min(trackBar2.Maximum, newValue));
                trackBar2.Value = newValue;
                UpdateColor();
            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            isGradientBoxClicked = true;
            trackBar3.Focus();
            int trackBarWidth = trackBar3.Width;
            int pictureBoxWidth = pictureBox4.Width;
            int newValue = (e.X * trackBar3.Maximum) / pictureBoxWidth;
            newValue = Math.Max(trackBar3.Minimum, Math.Min(trackBar3.Maximum, newValue));
            trackBar3.Value = newValue;
            UpdateColor();
        }
        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            isGradientBoxClicked = false;

        }
        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            if (isGradientBoxClicked)
            {
                trackBar3.Focus();
                int trackBarWidth = trackBar3.Width;
                int pictureBoxWidth = pictureBox4.Width;
                int newValue = (e.X * trackBar3.Maximum) / pictureBoxWidth;
                newValue = Math.Max(trackBar3.Minimum, Math.Min(trackBar3.Maximum, newValue));
                trackBar3.Value = newValue;
                UpdateColor();
            }
        }

        private void button1dec_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value > 1)
                trackBar1.Value -= 1;
            if (trackBar1.Value == 1)
                trackBar1.Value = 0;

            UpdateColor();

        }

        private void button1inc_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value < trackBar1.Maximum - 1)
                trackBar1.Value += 1;

            if (trackBar1.Value == trackBar1.Maximum - 1)
                trackBar1.Value = trackBar1.Maximum;

            UpdateColor();
        }

        private void button1min_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            UpdateColor();
        }

        private void button1max_Click(object sender, EventArgs e)
        {
            trackBar1.Value = trackBar1.Maximum;
            UpdateColor();

        }

        private void button2dec_Click(object sender, EventArgs e)
        {
            if (trackBar2.Value > 1)
                trackBar2.Value -= 1;
            if (trackBar2.Value == 1)
                trackBar2.Value = 0;

            UpdateColor();
        }

        private void button2inc_Click(object sender, EventArgs e)
        {
            if (trackBar2.Value < trackBar2.Maximum - 1)
                trackBar2.Value += 1;

            if (trackBar2.Value == trackBar2.Maximum - 1)
                trackBar2.Value = trackBar2.Maximum;

            UpdateColor();
        }

        private void button2min_Click(object sender, EventArgs e)
        {
            trackBar2.Value = 0;
            UpdateColor();
        }

        private void button2max_Click(object sender, EventArgs e)
        {
            trackBar2.Value = trackBar2.Maximum;
            UpdateColor();
        }

        private void button3dec_Click(object sender, EventArgs e)
        {
            if (trackBar3.Value > 1)
                trackBar3.Value -= 1;
            if (trackBar3.Value == 1)
                trackBar3.Value = 0;

            UpdateColor();
        }

        private void button3inc_Click(object sender, EventArgs e)
        {
            if (trackBar3.Value < trackBar3.Maximum - 1)
                trackBar3.Value += 1;

            if (trackBar3.Value == trackBar3.Maximum - 1)
                trackBar3.Value = trackBar3.Maximum;

            UpdateColor();
        }

        private void button3min_Click(object sender, EventArgs e)
        {
            trackBar3.Value = 0;
            UpdateColor();

        }

        private void button3max_Click(object sender, EventArgs e)
        {
            trackBar3.Value = trackBar3.Maximum;
            UpdateColor();

        }
    }
}
