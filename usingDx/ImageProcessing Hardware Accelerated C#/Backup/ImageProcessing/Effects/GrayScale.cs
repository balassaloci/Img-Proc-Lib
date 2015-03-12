using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace ImageProcessing.Effects
{
    public class GrayScaleEffect : ShaderEffect
    {

        public GrayScaleEffect(Device device)
            : base(device, "GrayScale")
        {

        }
        public override void SetData(EffectData data)
        {
         //   throw new NotImplementedException();
        }


   
        public static EffectData GetData()
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            //   dt.Add(new KeyValuePair<string, object>("amount", amount));
            //   dt.Add(new KeyValuePair<string, object>("center", new float[] { center.X, center.Y }));

            return new EffectData("GrayScale", dt.ToArray());
        }

    }
}
