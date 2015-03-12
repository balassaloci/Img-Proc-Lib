using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;

namespace ImageProcessing
{

    public abstract class ShaderEffect
    {

        internal Device _Device;
        public Device Device
        {
            get { return _Device; }
        }

        internal Effect _InternalEffect;
        public Effect InternalEffect
        {
            get { return _InternalEffect; }
        }
        public void SetInternalEffect(Effect value)
        {
            this._InternalEffect = value;
        }
        internal ShaderEffect(Device device, string name)
        {
            _Device = device;
            string err = "";
            _InternalEffect = Effect.FromString(device,(string) Properties.Resources.ResourceManager.GetObject(name),(Include) null,(ShaderFlags) ShaderFlags.None,(EffectPool)null,out err);
            if (err.Length > 0)
                throw new Exception();
            
        }
        public ShaderEffect(String sourceData,Device device)
        {
            _Device = device;
            string err = "";
            _InternalEffect = Effect.FromString(device, sourceData, (Include)null, (ShaderFlags)ShaderFlags.None, (EffectPool)null, out err);
            if (err.Length > 0)
                throw new Exception();
        }
        public ShaderEffect(Device device)
        {
            _Device = device;
            //if you don't load '_InternalEffect' manually before using this instance, application will throw null reference exception;
        }
       
        public void dispose()
        {
            this._InternalEffect.Dispose();
        }
        public bool disposed
        {
            get { return this._InternalEffect.Disposed; }
        }

        public abstract void SetData(EffectData data);

        //public virtual void SetLoadData(EffectData data)
        //{
        //}

        public virtual void Begin()
        {
            _InternalEffect.Technique = _InternalEffect.GetTechnique(0);
            _InternalEffect.Begin(FX.DoNotSaveState);
            _InternalEffect.BeginPass(0);
        }
        public virtual void End()
        {
            _InternalEffect.EndPass();
            _InternalEffect.End();

            Device.VertexShader = null;
            Device.PixelShader = null;

        }

        public virtual void Draw(Texture texture, EffectHelper helper)
        {
            this.Begin();
            Device.SetTexture(0, texture);
            Device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            //  helper.Sprite.Draw2D(texture, New Point, 0, New Point, Color.White)
            this.End();
        }
        public void Draw(Texture texture, EffectHelper helper, EffectData data)
        {
            SetData(data);
            Draw(texture, helper);
        }

        public static void ClearDeviceEffect(Device device)
        {
            device.PixelShader = null;
            device.VertexShader = null;

        }

    }

}
