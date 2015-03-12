using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;

namespace ImageProcessing.Effects
{
    public class ToneLevelsEffect :ShaderEffect
    {
        public EffectHandle Levels;
        public ToneLevelsEffect(Device device)
            : base(device, "ToneLevels")
        {
            Levels = _InternalEffect.GetParameter(null, "Levels");

      //      _InternalEffect.CommitChanges();

        }


        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count(); i++)
            {
                switch (data.Data[i].Key)
                {
                    case "levels":
                        _InternalEffect.SetValue(Levels, (float)data.Data[i].Value);
                        break;
                }
            }

            _InternalEffect.CommitChanges();

        }

        public static EffectData GetData(float levels)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("levels", levels));
   
            return new EffectData("ToneLevels", dt.ToArray());
        }



    }
}
