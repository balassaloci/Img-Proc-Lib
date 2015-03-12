using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace ImageProcessing.Effects
{
    public class SwirlEffect :ShaderEffect
    {
        public EffectHandle Center;
        public EffectHandle SpiralStrength;
        public EffectHandle AspectRatio;

        public SwirlEffect(Device device)
            : base(device, "Swirl")
        {
            Center = _InternalEffect.GetParameter(null, "Center");
            SpiralStrength = _InternalEffect.GetParameter(null, "SpiralStrength");
            AspectRatio = _InternalEffect.GetParameter(null, "AspectRatio");

            this._InternalEffect.SetValue(Center, new float[] { 0.5f, 0.5f });

        }


        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count(); i++)
            {
                switch (data.Data[i].Key)
                {
                    case "aspectratio":
                        _InternalEffect.SetValue(AspectRatio, (float)data.Data[i].Value);
                        break;
                    case "center":
                        _InternalEffect.SetValue(Center, (float[])data.Data[i].Value);
                        break;
                    case "spiralstrength":
                        _InternalEffect.SetValue(SpiralStrength,(float)data.Data[i].Value);
                        break;
                }
            }

            _InternalEffect.CommitChanges();

        }


        public override void Begin()
        {
            base.Begin();

        }

        public static EffectData GetData(float spiralstrength,float aspectration, PointF center)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("spiralstrength", spiralstrength));
            dt.Add(new KeyValuePair<string, object>("aspectratio", aspectration));
            dt.Add(new KeyValuePair<string, object>("center", new float[] { center.X, center.Y }));

            return new EffectData("Swirl", dt.ToArray());
        }
        
    }
}
