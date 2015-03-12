using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    public unsafe struct Frame
    {

        public int Width;
        public int Height;

        public byte* Data;
        public Frame(byte* data, int width, int height)
        {
            this.Data = data;
            this.Width = width;
            this.Height = height;

        }

        public static Frame OpenBitmap(Bitmap bitmap,out BitmapData bd)
        {
            bd = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
            return new Frame((byte*)bd.Scan0.ToPointer(), bitmap.Width, bd.Height);
        }
        public static void CloseBitmap(Bitmap bitmap ,BitmapData bitmapData)
        {
            bitmap.UnlockBits(bitmapData);
        }


    }
}
