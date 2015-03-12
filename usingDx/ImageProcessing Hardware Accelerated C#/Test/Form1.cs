using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageProcessing;
using ImageProcessing.Effects;
using System.Drawing.Imaging;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Fields
        OpenFileDialog of = new OpenFileDialog();
        Bitmap CurrentBitmap = null;
        EffectHelper EH;
#endregion

        #region Methods & functions
        private void choseImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (of.ShowDialog() != DialogResult.OK) return;
            Image s = (Image)System.Drawing.Image.FromFile(of.FileName);

            Bitmap clone = new Bitmap(s.Width, s.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(s, new Rectangle(0, 0, clone.Width, clone.Height));

                s.Dispose();

                CurrentBitmap = clone;
                pictureBox1.Image = (Image)clone.Clone();
        }
        

        }


        private void applyMultipleEffectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Fit()) return;
            BitmapData bd;
            Bitmap b = (Bitmap)CurrentBitmap.Clone();

            Frame frm = Frame.OpenBitmap(b, out bd);

            EffectData[] dataseries = new EffectData[3];

            dataseries[0] = SwirlEffect.GetData(20f, 1f, new PointF(.5f, .5f));
            dataseries[1] = ZoomBlurEffect.GetData(0.5f, new PointF(0.5f, 0.5f));
            dataseries[2] = GrayScaleEffect.GetData();
            frm = EH.ApplyMultipleEffects(frm, dataseries);

            Frame.CloseBitmap(b, bd);
            pictureBox2.Image = (Image)b;
        }



        private void setAsOriginaleImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                CurrentBitmap = (Bitmap)pictureBox2.Image;

                pictureBox1.Image = (Bitmap)CurrentBitmap.Clone();
                pictureBox2.Image = null;

            }
        }

        internal bool Fit()
        {
            if (CurrentBitmap == null)
            {
                return false;
            }
            if (EH == null)
            {
                EH = new EffectHelper();
                Size s = EffectHelper.GetMaxSize();
                EH.Load(s.Width, s.Height);
            }

            if (pictureBox2.Image != null)
            {
                Image b = pictureBox2.Image;
                pictureBox2.Image = null;
                b.Dispose();
            }
            return true;
        }
      

        private void F_Closing(object sender, FormClosingEventArgs e)
        {
           if(EH != null)  EH.DisposeAll();
        }

        public void ApplyEffect(EffectData data)
        {
            if (!Fit()) return;

            BitmapData bd;
            Bitmap b = (Bitmap)CurrentBitmap.Clone();
            Frame frm = Frame.OpenBitmap(b, out bd);

            frm = EH.ApplyEffect(frm, data);

            Frame.CloseBitmap(b, bd);
            
            pictureBox2.Image =(Image) b;
            pictureBox2.Refresh();
        }

        #endregion

        #region effect parameters handlers
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
          
            ApplyEffect(BlurEffect.GetData((float)(trackBar1.Value/1000f)));

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            ApplyEffect(DirectionalBlurEffect.GetData((float)trackBar3.Value,(float)trackBar2.Value / 150));
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            trackBar2_Scroll(sender, e);

        }

        private void button1_Click (object sender, EventArgs e)
        {
            ApplyEffect(GrayScaleEffect.GetData());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ApplyEffect(InvertEffect.GetData());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ApplyEffect(MonoChromeEffect.GetData(((Button)sender).BackColor));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ApplyEffect(MonoChromeEffect.GetData(((Button)sender).BackColor));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ApplyEffect(MonoChromeEffect.GetData(((Button)sender).BackColor));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplyEffect(MonoChromeEffect.GetData(((Button)sender).BackColor));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ApplyEffect(MonoChromeEffect.GetData(((Button)sender).BackColor));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ApplyEffect(MonoChromeEffect.GetData(((Button)sender).BackColor));
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ApplyEffect(OldMovieEffect.GetData());
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            ApplyEffect(PixelateEffect.GetData(new PointF((float)trackBar4.Value, (float)trackBar6.Value), (float)trackBar5.Value/10f));
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            trackBar5_Scroll(sender,e);
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            trackBar5_Scroll(sender, e);

        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            ApplyEffect(RippleEffect.GetData(new PointF((float)trackBar8.Value / 100f,
               (float)trackBar7.Value / 100f),
                 (float)trackBar9.Value/100f,
                   (float)trackBar10.Value,
                   (float)trackBar11.Value / 20f,
                    (float)trackBar12.Value / 50f));
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            trackBar8_Scroll(sender, e);
        }

        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            trackBar8_Scroll(sender, e);
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            trackBar8_Scroll(sender, e);
        }

        private void trackBar11_Scroll(object sender, EventArgs e)
        {
            trackBar8_Scroll(sender, e);
        }

        private void trackBar12_Scroll(object sender, EventArgs e)
        {
            trackBar8_Scroll(sender, e);
        }

        private void trackBar17_Scroll(object sender, EventArgs e)
        {
            ApplyEffect(SwirlEffect.GetData(
                (float)trackBar18.Value,
            (float)trackBar15.Value/50f,
                new PointF((float)trackBar17.Value / 100f,
            (float)trackBar16.Value / 100f)            ));
        }

        private void trackBar16_Scroll(object sender, EventArgs e)
        {
            trackBar17_Scroll(sender, e);
        }

        private void trackBar18_Scroll(object sender, EventArgs e)
        {
            trackBar17_Scroll(sender, e);
        }

        private void trackBar15_Scroll(object sender, EventArgs e)
        {
            trackBar17_Scroll(sender, e);
        }

        private void trackBar13_Scroll(object sender, EventArgs e)
        {
            ApplyEffect(ToneLevelsEffect.GetData((float)trackBar13.Value));
        }

        private void trackBar19_Scroll(object sender, EventArgs e)
        {
            ApplyEffect(WaveWarperEffect.GetData((float)trackBar19.Value, (float)trackBar14.Value));
        }

        private void trackBar14_Scroll(object sender, EventArgs e)
        {
            trackBar19_Scroll(sender, e);
        }

        private void trackBar22_Scroll(object sender, EventArgs e)
        {
            ApplyEffect(ZoomBlurEffect.GetData(
                (float)trackBar23.Value / 50f,
                new PointF((float)trackBar22.Value / 100f,
             (float)  (float)trackBar21.Value / 100f)             ));
        }

        private void trackBar21_Scroll(object sender, EventArgs e)
        {
            trackBar22_Scroll(sender, e);
        }

        private void trackBar23_Scroll(object sender, EventArgs e)
        {
            trackBar22_Scroll(sender, e);
        }

        #endregion
        ScaleExtended scaleEff;

        private void scaleEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Fit()) return;
            if (scaleEff == null)
            {
                scaleEff = new ScaleExtended(EH.Device);
                EH.Manager.Register("ScaleExtended", scaleEff);

            }

            EffectData d = ScaleExtended.GetData(.5f, .5f, .5f, .5f);
            ApplyEffect(d);

        }

       

      

       
       
     

       

     


    }

}