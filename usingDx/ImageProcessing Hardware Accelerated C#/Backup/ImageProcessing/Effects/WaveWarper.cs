using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace ImageProcessing.Effects
{
    public class WaveWarperEffect :ShaderEffect
    {
        public EffectHandle Time;
        public EffectHandle WaveSize;

        public WaveWarperEffect(Device device)
            : base(device, "WaveWarper")
        {
            Time = _InternalEffect.GetParameter(null, "Time");
            WaveSize = _InternalEffect.GetParameter(null, "WaveSize");
          
        }


        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count(); i++)
            {
                switch (data.Data[i].Key)
                {
                    case "time":
                        _InternalEffect.SetValue(Time, (float)data.Data[i].Value);
                        break;
                    case "wavesize":
                        _InternalEffect.SetValue(WaveSize, (float)data.Data[i].Value);
                        break;
                }
            }

            _InternalEffect.CommitChanges();

        }


        public override void Begin()
        {
            base.Begin();

        }

        public static EffectData GetData(float time, float wavesize)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("time", time));
            dt.Add(new KeyValuePair<string, object>("wavesize", wavesize));
          
            return new EffectData("WaveWarper", dt.ToArray());
        }
        
    }
}

