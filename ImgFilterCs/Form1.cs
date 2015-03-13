using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace ImgFilterCs
{


    public partial class Form1 : Form
    {
        Bitmap img;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            img = openImg();
            pictureBox1.Image = img;

        }

        Bitmap openImg()
        {

            OpenFileDialog of = new OpenFileDialog();

            if (of.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("No img open");
                Application.Exit();

                return null;
            }

            Bitmap bmp = new Bitmap(of.FileName);

            return bmp;
        }

        public void InvertImage(ref Bitmap img)
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pxCol = img.GetPixel(i, j);
                    Color inverted = Color.FromArgb(255 - pxCol.R, 255 - pxCol.G, 255 - pxCol.B);
                    img.SetPixel(i, j, inverted);
                }
            }

        }
        
        private void invertButton_Click(object sender, EventArgs e)
        {

            InvertImage(ref img);
            pictureBox1.Image = img;

        }

        private void BlurButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            float[,] identity = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };

            float v = (float)(1.0 / 9.0);
            float[,] boxBlur = { { v, v, v }, { v, v, v }, { v, v, v } };
            
            float[,] edge = { { -1, -1, -1 },
                              { -1, 8, -1 },
                              { -1, -1, -1 } };

            float[,] sharpen = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };

            float[,] gaussian = { { 1/16, 1/8, 1/16 }, { 1/8, 1/4, 1/8 }, { 1/16, 1/8, 1/16 } };

            float[,] random = { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };

            float[,] corner = {{-1, 0, 1},
                               {-2, 0, 2},
                               {-1, 0, 1},};
            

            FilterLib filter = new FilterLib(img);
            
            //filter.ToBW();

            pictureBox1.Image = filter.applyMask(gaussian);

            Cursor = Cursors.Default;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            Cursor = Cursors.WaitCursor;

            cursorPos.Text = string.Format("Cursor X:{0}, Y:{1} [CLICK]", e.X, e.Y);
            int tresh = Convert.ToInt32(treshTB.Text);
            int maxdist = Convert.ToInt32(maxDistTextBox.Text);

            Color colr = img.GetPixel(e.X, e.Y);
            
            FilterLib filter = new FilterLib(img);

            //pictureBox1.Image = filter.FindWColor(colr, tresh);

            pictureBox1.Image = filter.blobDetect(colr, tresh, maxdist);

            Cursor = Cursors.Default;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //cursorPos.Text = string.Format("Cursor X:{0}, Y:{1}", e.X, e.Y);

        }

    }
}