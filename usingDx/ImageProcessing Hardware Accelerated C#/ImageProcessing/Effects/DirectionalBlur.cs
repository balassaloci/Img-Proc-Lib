using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace ImageProcessing.Effects
{
    public class DirectionalBlurEffect : ShaderEffect
    {
        public EffectHandle Angle;
        public EffectHandle Amount;
       
        public DirectionalBlurEffect(Device device)
            : base(device, "DirectionalBlur")
        {
            Angle = _InternalEffect.GetParameter(null, "Angle");
            Amount = _InternalEffect.GetParameter(null, "Amount");
           
        }


        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count(); i++)
            {
                switch (data.Data[i].Key)
                {
                    case "angle":
                        _InternalEffect.SetValue(Angle, (float)data.Data[i].Value);
                        break;
                    case "amount":
                        _InternalEffect.SetValue(Amount , (float)data.Data[i].Value);
                        break;
                  
                }
            }

            _InternalEffect.CommitChanges();

        }


        public override void Begin()
        {
            base.Begin();

        }

        public static EffectData GetData(float angle,float amount)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("angle", angle));
            dt.Add(new KeyValuePair<string, object>("amount", amount));
       
            return new EffectData("DirectionalBlur", dt.ToArray());
        }

    }
}

