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
                               {-1, 0, 1}};

            float v5 = (float)(1.0 / 5.0);

            float[,] blur_corner = {{0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}, {v5, v5, v5, v5, v5}, {0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}};


            float[,] corner3 = {{-1, -2, -1},
                               {0, 0, 0},
                               {1, 2, 1}};

            float[,] blur_corner3 = {{0, 0, v5, 0, 0}, {0, 0, v5, 0, 0}, {0, 0, v5, 0, 0}, {0, 0, v5, 0, 0}, {0, 0, v5, 0, 0}};


            float vLine = (float)(1.0 / 5.0);

            

            float[,] lineBlurFilter = { { vLine, vLine, vLine, vLine, vLine}};

            float[,] corner2 = {{-3, 0, 3},
                               {-10, 0, 10},
                               {-3, 0, 3}};


            FilterLib filter = new FilterLib(img);
            filter.ToBW();
            
            FilterLib filter2 = new FilterLib(img);
            filter2.ToBW();
            
            filter.orig = filter.applyMask(corner);
            filter.orig = filter.applyMask(blur_corner);            
            filter.orig = filter.treshold(filter.orig, 50);
            Bitmap imga = filter.orig;
            
            filter2.orig = filter2.applyMask(corner3);
            filter2.orig = filter2.applyMask(blur_corner3);
            filter2.orig = filter2.treshold(filter2.orig, 50);
            Bitmap imgb = filter2.orig;

            img = sumImg(imga, imgb);           

            
            //filtered = filter.treshold(filtered, 100);
            pictureBox1.Image = img;
            Cursor = Cursors.Default;

        }

        Bitmap sumImg(Bitmap a, Bitmap b)
        {
            Bitmap output = new Bitmap(a.Width, a.Height);

            for (int i = 0; i < a.Width; i++)
            {
                for (int j = 0; j < a.Height; j++)
                {
                    int newVal = a.GetPixel(i, j).R + b.GetPixel(i, j).R;
                    if (newVal > 255)
                    {
                        newVal = 255;
                    }

                    output.SetPixel(i, j, Color.FromArgb(newVal, newVal, newVal));
                }
            }
            
            return output;
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


        TrackLib tracker = new TrackLib();

        private void LoadSequenceButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Please open first element in sequence";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                
                string formattedName = "", dir = "";

                tracker.getFormattedName(dialog.FileName, ref dir, ref formattedName);
                tracker.LoadSequence(formattedName, dir);

                SequenceViewPB.Image = tracker.sequence.First();
            }

        }

        private void SequenceViewPB_MouseClick(object sender, MouseEventArgs e)
        {
            Point mouseLoc = new Point(e.X,e.Y);
            Bitmap markerImage = (Bitmap)tracker.sequence[tracker.currentIndex()].Clone();

            SequenceViewPB.Image = tracker.drawMarker(markerImage, mouseLoc, 10);
        }

        private void NextImgButton_Click(object sender, EventArgs e)
        {
            
            //tracker.getNext(ref newImage);

            //pictureBox1.Image = newImage;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            Int32 tresh = 0;
            if (Int32.TryParse(textBox1.Text, out tresh))
            {
                Cursor = Cursors.WaitCursor;

                Bitmap tresholded = new Bitmap(img.Width, img.Height);

                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        if (img.GetPixel(i, j).R < tresh)
                        {
                            tresholded.SetPixel(i, j, Color.Black);
                        }
                        else
                        {
                            tresholded.SetPixel(i, j, Color.White);
                        }
                    }
                }

                float v = (float)(1.0 / 9.0);
                float[,] boxBlur = { { v, v, v }, { v, v, v }, { v, v, v } };
                
                float w = (float)(1.0 / 5.0);
                float[,] lineBlur = { { 0, 0, w, 0, 0 }, { 0, 0, w, 0, 0 }, { 0, 0, w, 0, 0 }, { 0, 0, w, 0, 0 }, { 0, 0, w, 0, 0 } };

                FilterLib filter = new FilterLib(tresholded);
                tresholded = filter.applyMask(boxBlur);

                pictureBox1.Image = tresholded;

                Cursor = Cursors.Default;
            }
        }

        

    }
}