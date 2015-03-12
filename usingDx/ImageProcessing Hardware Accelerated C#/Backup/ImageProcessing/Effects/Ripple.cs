using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace ImageProcessing.Effects
{
    public class RippleEffect : ShaderEffect
    {
        public EffectHandle Center;
        public EffectHandle Amplitude;
        public EffectHandle Frequency;
        public EffectHandle Phase;
        public EffectHandle AspectRatio;

        public RippleEffect(Device device)
            : base(device, "Ripple")
        {
            Center = _InternalEffect.GetParameter(null, "Center");
            Amplitude = _InternalEffect.GetParameter(null, "Amplitude");
            Frequency = _InternalEffect.GetParameter(null, "Frequency");
            Phase = _InternalEffect.GetParameter(null, "Phase");
            AspectRatio = _InternalEffect.GetParameter(null, "AspectRatio");

        }


        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count(); i++)
            {
                switch (data.Data[i].Key)
                {
                    case "center":
                        _InternalEffect.SetValue(Center, (float[])data.Data[i].Value);
                        break;
                    case "amplitude":
                        _InternalEffect.SetValue(Amplitude, (float)data.Data[i].Value);
                        break;
                    case "frequency":
                        _InternalEffect.SetValue(Frequency, (float)data.Data[i].Value);
                        break;
                    case "phase":
                        _InternalEffect.SetValue(Phase, (float)data.Data[i].Value);
                        break;
                    case "aspectRatio":
                        _InternalEffect.SetValue(AspectRatio, (float)data.Data[i].Value);
                        break;
                  
                }
            }

            _InternalEffect.CommitChanges();

        }


        public override void Begin()
        {
            base.Begin();

        }

        public static EffectData GetData(PointF center, float amplitude, float frequency, float phase, float aspectRatio)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("center", new float[] { center.X, center .Y}));
            dt.Add(new KeyValuePair<string, object>("amplitude", amplitude));
            dt.Add(new KeyValuePair<string, object>("frequency", frequency));
            dt.Add(new KeyValuePair<string, object>("phase", phase));
            dt.Add(new KeyValuePair<string, object>("aspectRatio", aspectRatio));
 
            return new EffectData("Ripple", dt.ToArray());
        }

    }
}

