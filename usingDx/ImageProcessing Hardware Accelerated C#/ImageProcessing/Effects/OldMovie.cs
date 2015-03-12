using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;

namespace ImageProcessing.Effects
{
    public class OldMovieEffect : ShaderEffect
    {

        public OldMovieEffect(Device device)
            : base(device, "OldMovie")
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

            return new EffectData("OldMovie", dt.ToArray());
        }


    }
}
