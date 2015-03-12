using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;

namespace ImageProcessing.Effects
{
    public class InvertEffect :ShaderEffect
    {

        public InvertEffect(Device device)
            : base(device, "Invert")
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

            return new EffectData("Invert", dt.ToArray());
        }


    }
}
