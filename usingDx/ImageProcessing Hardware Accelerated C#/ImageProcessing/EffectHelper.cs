using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Windows.Forms;
using System.Drawing;

namespace ImageProcessing
{
   
    public class EffectHelper 
    {

        public EffectHelper()
        {

        }

        public void Load(int width, int height)
        {
            this._Width = width;
            this._Height = height;

            Control = new Control();
            Control.Width = width;
            Control.Height = height;

            PP = new PresentParameters();
            PP.BackBufferCount = 1;
            PP.BackBufferFormat = Format.A8R8G8B8;
            PP.BackBufferHeight = height;
            PP.BackBufferWidth = width;
            PP.DeviceWindow = this.Control;
            PP.SwapEffect = SwapEffect.Discard;
            PP.Windowed = true;
            PP.PresentFlag = PresentFlag.None;

            Device = new Device(0, DeviceType.Hardware, Control, CreateFlags.SoftwareVertexProcessing, PP);

            load_Textures(width, height);
            load_Vb(width, height);

            Manager = new EffectsManager(Device);

            // Sprite = New Sprite(Device)

            Device.RenderState.CullMode = Cull.None;
            Device.RenderState.Lighting = false;
            Device.RenderState.SourceBlend = Blend.SourceAlpha;
            Device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
            Device.RenderState.AlphaBlendEnable = true;

            Device.SetSamplerState(0, SamplerStageStates.MagFilter, (int)TextureFilter.Linear);
            Device.SetSamplerState(0, SamplerStageStates.MinFilter, (int)TextureFilter.Linear);
            Device.SetSamplerState(0, SamplerStageStates.MipFilter, (int)TextureFilter.Linear);



        }


        PresentParameters PP;
        public Control Control;
        public Device Device;

        public EffectsManager Manager;

        internal Surface DefaultRt;
     
        internal VertexBuffer vb;

        internal RenderTarget rt;
        internal DataTexture tex;
        internal DataSurface res;

        internal int _Width;
        internal int _Height;
        public int Width
        {
            get { return _Width; }
        }
        public int Height
        {
            get { return _Height; }
        }

        public void load_Textures(int width, int height)
        {
            rt = new RenderTarget(Device, width, height);
           
            tex = new DataTexture(Device, width, height);

            res = new DataSurface(Device, width, height);

        }

        public void load_Vb(int width, int height)
        {
          if(vb == null)  vb = new VertexBuffer(typeof(CustomVertex.TransformedTextured), 6, Device, Usage.SoftwareProcessing, CustomVertex.TransformedTextured.Format, Pool.Managed);

           CustomVertex.TransformedTextured[] vertices = new CustomVertex.TransformedTextured[6];
     
           vertices[0].Position = new Vector4(0, 0, 0, 1);
           vertices[1].Position = new Vector4(width, 0, 0, 1);
           vertices[2].Position = new Vector4(0, height, 0, 1);
           vertices[3].Position = new Vector4(width, height, 0, 1);
           vertices[4].Position = new Vector4(0, height, 0, 1);
           vertices[5].Position = new Vector4(width, 0, 0, 1);


            vertices[0].Tu = 0;
            vertices[0].Tv = 0;

            vertices[1].Tu = 1;
            vertices[1].Tv = 0;

            vertices[2].Tu = 0;
            vertices[2].Tv = 1;

            vertices[3].Tu = 1;
            vertices[3].Tv = 1;

            vertices[4].Tu = 0;
            vertices[4].Tv = 1;

            vertices[5].Tu = 1;
            vertices[5].Tv = 0;

            vb.SetData(vertices, 0, LockFlags.None);

         
        }

        public void ResetDevice()
        {
            Device.Reset(PP);
            DefaultRt = Device.GetRenderTarget(0);

            Device.RenderState.CullMode = Cull.None;
            Device.RenderState.Lighting = false;
            Device.RenderState.SourceBlend = Microsoft.DirectX.Direct3D.Blend.SourceAlpha;
            Device.RenderState.DestinationBlend = Microsoft.DirectX.Direct3D.Blend.InvSourceAlpha;
            Device.RenderState.AlphaBlendEnable = true;

            Device.SetSamplerState(0, SamplerStageStates.MagFilter, (int)TextureFilter.Linear);
            Device.SetSamplerState(0, SamplerStageStates.MinFilter,(int) TextureFilter.Linear);
            Device.SetSamplerState(0, SamplerStageStates.MipFilter, (int)TextureFilter.Linear);


            if (rt.disposed == false)
            {
                rt.Dispose();
            }
            rt = new RenderTarget(Device, PP.BackBufferWidth, PP.BackBufferHeight);

            if (res.disposed == false)
            {
                res.Dispose();
            }
            res = new DataSurface(Device, PP.BackBufferWidth, PP.BackBufferHeight);

            GC.Collect();

        }

        public Frame ApplyEffect(Frame frame, EffectData data)
        {
            SetFrameSize(frame.Width, frame.Height);
            ShaderEffect eff;
            start:
            try
            {
                tex.SetData(frame);
             
                Device.SetRenderTarget(0,(Surface) rt);
                Device.Clear(ClearFlags.Target, Color.FromArgb(0, 0, 0, 0), 0, 0);
                Device.BeginScene();

                Device.VertexFormat = CustomVertex.TransformedTextured.Format;
                Device.SetStreamSource(0, vb, 0);

           
                    if (data.EffectName != null)
                    {
                        eff = Manager.GetEffect(data.EffectName);
                        eff.Draw((Texture)tex, this, data);
                    }else{
                        throw new Exception("eff cannot be null.");
                    }
           
                Device.EndScene();
                Device.GetRenderTargetData((Surface)rt, (Surface)res);

                Frame frm = res.GetData(frame);
                return frm;
                

            }
            catch (DeviceNotResetException ex)
            {
                ResetDevice();
                System.Threading.Thread.Sleep(50);
                goto start;
            }
            catch (DeviceLostException ex)
            {
                System.Threading.Thread.Sleep(100);
                goto start;
            }


            return frame;  

        }
        public Frame ApplyMultipleEffects(Frame frame, EffectData[] series)
        {
            SetFrameSize(frame.Width, frame.Height);
            ShaderEffect eff;
            Frame frm = frame;
            for (int i = 0; i < series.Length; i++)
            {
            start:
                try
                {
                    tex.SetData(frm);
                    Device.SetRenderTarget(0, (Surface)rt);
                    Device.Clear(ClearFlags.Target, Color.FromArgb(0, 0, 0, 0), 0, 0);
                    Device.BeginScene();

                    Device.VertexFormat = CustomVertex.TransformedTextured.Format;
                    Device.SetStreamSource(0, vb, 0);

                 
                        if (series[i].EffectName != null)
                        {
                            eff = Manager.GetEffect(series[i].EffectName);
                            eff.Draw((Texture)tex, this, series[i]);
                        }
                        else
                        {
                            throw new Exception("eff cannot be null.");
                        }
                
                    Device.EndScene();
                    Device.GetRenderTargetData((Surface)rt, (Surface)res);

                    frm = res.GetData(frm);

                    

                }
                catch (DeviceNotResetException ex)
                {
                    ResetDevice();
                    System.Threading.Thread.Sleep(50);
                    goto start;
                }
                catch (DeviceLostException ex)
                {
                    System.Threading.Thread.Sleep(100);
                    goto start;
                }
            }

            return frm;

        }

        internal void SetFrameSize(int width, int height)
        {
            if (rt.disposed == true)
            {
                rt = null;
            }
            else if (rt.Width != width || rt.Height != height)
            {
                rt.Dispose();
                rt = null;
            }
            if (rt == null)
                rt = new RenderTarget(Device, width, height);


            if (tex.disposed == true)
            {
                tex = null;
            }
            else if (tex.Width != width || tex.Height != height)
            {
                tex.Dispose();
                tex = null;
            }
            if (tex == null)
                tex = new DataTexture(Device, width, height);


            if (res.disposed == true)
            {
                res = null;
            }
            else if (res.Width != width || res.Height != height)
            {
                res.Dispose();
                res = null;
            }
            if (res == null)
                res = new DataSurface(Device, width, height);




            load_Vb(width, height);



        }

        public void DisposeAll()
        {
            if (Device != null && Device.Disposed == false)
                Device.Dispose();
            if (rt != null && rt.disposed == false)
                rt.Dispose();

            if (Control != null && Control.IsDisposed == false)
                Control.Dispose();
            if (vb != null && vb.Disposed == false)
                vb.Dispose();
            DefaultRt = null;
          
            if (res != null && res.disposed == false)
                res.Dispose();

            if (tex != null && tex.disposed == false)
                tex.Dispose();
         

        }

        public static System.Drawing.Size GetMaxSize()
        {
            Caps dc =Microsoft.DirectX.Direct3D.Manager.GetDeviceCaps(0, DeviceType.Hardware);
            return new Size(dc.MaxTextureWidth, dc.MaxTextureHeight);
        }


    }
}
