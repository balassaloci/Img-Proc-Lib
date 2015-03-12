using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Microsoft.DirectX.Direct3D;
using ImageProcessing.Effects;

namespace ImageProcessing
{
    
    public class EffectsManager
    {

        internal Device _Device;
        public Device Device
        {
            get { return _Device; }
        }
        
        internal Dictionary<string, ShaderEffect> Effects = new Dictionary<string, ShaderEffect>();
      
        internal string LastEffectName;
        internal ShaderEffect LastEffect;
        public EffectsManager(Device device)
        {
            this._Device = device;
            Load();
        }

        public void Load()
        {
            Effects.Add("GrayScale", null);
            Effects.Add("ZoomBlur", null);
            Effects.Add("Swirl", null);
            Effects.Add("ToneLevels", null);
            Effects.Add("MonoChrome", null);
            Effects.Add("Invert", null);
            Effects.Add("OldMovie", null);
            Effects.Add("WaveWarper", null);
            Effects.Add("Ripple", null);
            Effects.Add("Pixelate", null);
            Effects.Add("DirectionalBlur", null);
            Effects.Add("Blur", null);
            Effects.Add("Matrix", null);
            Effects.Add("RadialBlur", null);
      
        
        }

        public void Register(string name, ShaderEffect effect) 
        {
            Effects.Add(name, effect);
        }

        public ShaderEffect GetEffect(string name)
        {
            if (name == null || name.Length == 0)
                throw new Exception();
            //to be removed 
            ShaderEffect eff = null;
            if (name == LastEffectName)
                eff = LastEffect;
                if (eff != null)   return eff;

            if (Effects.TryGetValue(name, out eff) == false)
            {

                throw new Exception("Effect not found");
                
            }
            else if (eff == null)
            {
                eff = LoadEffect(name);
            }

            LastEffectName = name;
            LastEffect = eff;
            return eff;



        }
   
        public ShaderEffect LoadEffect(string name)
        {
            ShaderEffect eff = default(ShaderEffect);
            switch (name)
            {
                case "GrayScale":

                   eff = new GrayScaleEffect(this.Device);
                    break;
                case "ZoomBlur":

                   eff = new ZoomBlurEffect(this.Device);
                    break;
                case "ToneLevels":

                    eff = new ToneLevelsEffect(this.Device);
                    break;
                case "Invert":

                    eff = new InvertEffect(this.Device);
                    break;
                case "MonoChrome":

                    eff = new MonoChromeEffect(this.Device);
                    break;
                case "OldMovie":

                    eff = new OldMovieEffect(this.Device);
                    break;
                case "Swirl":

                    eff = new SwirlEffect(this.Device);
                    break;
                case "WaveWarper":

                    eff = new WaveWarperEffect(this.Device);
                    break;
                case "Ripple":

                    eff = new RippleEffect(this.Device);
                    break;
                case "Pixelate":

                    eff = new PixelateEffect(this.Device);
                    break;
                case "DirectionalBlur":

                    eff = new DirectionalBlurEffect(this.Device);
                    break;
               
                case "Blur":

                    eff = new BlurEffect(this.Device);
                    break;
               
                default:
                    //to be removed
                    throw new Exception();
            }

             Effects[name] = eff;
            return eff;
        }


    }
  
}
