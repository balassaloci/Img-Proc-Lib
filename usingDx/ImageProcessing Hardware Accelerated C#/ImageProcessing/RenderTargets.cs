using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Collections;

namespace ImageProcessing
{
    public class RenderTarget
    {

        public readonly Texture Texture;

        public readonly Surface Surface;
        public RenderTarget(Device device, int width, int height)
        {
            Texture = new Texture(device, width, height, 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default);
            Surface = Texture.GetSurfaceLevel(0);
        }

        public void Dispose()
        {
            if (Texture.Disposed == false)
                Texture.Dispose();
            if (Surface.Disposed == false)
                Surface.Dispose();

        }
        public bool disposed
        {
            get { return Surface.Disposed; }
        }
        public static explicit operator Texture(RenderTarget rt)
        {
            return rt.Texture;
        }
        public static explicit operator Surface(RenderTarget rt)
        {
            return rt.Surface;
        }

        public int Width
        {
            get { return Surface.Description.Width; }
        }
        public int Height
        {
            get { return Surface.Description.Height; }
        }

    }
    public class DataTexture
    {

        public readonly Texture Texture;

        public readonly Surface Surface;
        public DataTexture(Device device, int width, int height)
        {
            Texture = new Texture(device, width, height, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);
            Surface = Texture.GetSurfaceLevel(0);
        }

        public void Dispose()
        {
            if (Texture.Disposed == false)
                Texture.Dispose();
            if (Surface.Disposed == false)
                Surface.Dispose();
        }
        public bool disposed
        {
            get { return Surface.Disposed; }
        }

        public unsafe void SetData(Frame data)
        {
            byte[] b =(byte[]) Surface.LockRectangle(typeof(byte), new Rectangle(0, 0, this.Width, this.Height), LockFlags.DoNotWait, new int[] { this.Width * this.Height * 4 });
        
            System.Runtime.InteropServices.Marshal.Copy((IntPtr)data.Data, b, 0, b.Length); 

            Surface.UnlockRectangle();
        }
        public void SetData(IntPtr scan)
        {
            byte[] b = (byte[])Surface.LockRectangle(typeof(byte), new Rectangle(0, 0, this.Width, this.Height), LockFlags.DoNotWait, new int[] { this.Width * this.Height * 4 });
            System.Runtime.InteropServices.Marshal.Copy(scan, b, 0, b.Length);
         
            Surface.UnlockRectangle();
        }
        public static explicit operator Texture(DataTexture rt)
        {
            return rt.Texture;
        }
        public static explicit operator Surface(DataTexture rt)
        {
            return rt.Surface;
        }

        public int Width
        {
            get { return Surface.Description.Width; }
        }
        public int Height
        {
            get { return Surface.Description.Height; }
        }

    }
    public class DataSurface
    {


        public readonly Surface Surface;
        public DataSurface(Device device, int width, int height)
        {
            Surface = device.CreateOffscreenPlainSurface(width, height, Format.A8R8G8B8, Pool.SystemMemory);
        }

        public void Dispose()
        {
            if (Surface.Disposed == false)
                Surface.Dispose();
        }
        public bool disposed
        {
            get { return Surface.Disposed; }
        }

        public unsafe Frame GetData(Frame inFrame)
        {
            byte[] b = (byte[])Surface.LockRectangle(typeof(byte), new Rectangle(0, 0, this.Width, this.Height), LockFlags.None, new int[] { this.Width * this.Height * 4 });
            Surface.UnlockRectangle();

            System.Runtime.InteropServices.Marshal.Copy(b, 0, (IntPtr)inFrame.Data, b.Length);
            return inFrame;
        }


        public static explicit operator Surface(DataSurface rt)
        {
            return rt.Surface;
        }

        public int Width
        {
            get { return Surface.Description.Width; }
        }
        public int Height
        {
            get { return Surface.Description.Height; }
        }


    }
    public class RenderTexture
    {

        public readonly Surface Surface;
        public readonly Texture Texture;

        public RenderTexture(Device device, int width, int height)
        {
            Texture = new Texture(device,width,height,1, Usage.RenderTarget, Format.A8R8G8B8 ,Pool.SystemMemory);
            Surface = this.Texture.GetSurfaceLevel(0);

        }

        public unsafe Frame GetData(Frame inFrame)
        {
            byte[] b = (byte[])Surface.LockRectangle(typeof(byte), new Rectangle(0, 0, this.Width, this.Height), LockFlags.None, new int[] { this.Width * this.Height * 4 });
            System.Runtime.InteropServices.Marshal.Copy(b,0,(IntPtr)inFrame.Data,b.Length);
            Surface.UnlockRectangle();
            return inFrame;
        }


        public void Dispose()
        {
            if (Texture.Disposed == false)
                Texture.Dispose();
            if (Surface.Disposed == false)
                Surface.Dispose();
        }
        public bool disposed
        {
            get { return Surface.Disposed; }
        }

       
        public static explicit operator Texture(RenderTexture rt)
        {
            return rt.Texture;
        }
        public static explicit operator Surface(RenderTexture rt)
        {
            return rt.Surface;
        }

        public int Width
        {
            get { return Surface.Description.Width; }
        }
        public int Height
        {
            get { return Surface.Description.Height; }
        }


    }

}
