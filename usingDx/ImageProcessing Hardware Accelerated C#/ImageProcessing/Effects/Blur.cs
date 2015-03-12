using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;



namespace ImageProcessing.Effects
{
    public class BlurEffect : ShaderEffect
    {
        public EffectHandle Amount;

        public BlurEffect(Device device)
            : base(device, "Blur")
        {
            Amount = _InternalEffect.GetParameter(null, "Amount");

        }


        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count(); i++)
            {
                switch (data.Data[i].Key)
                {

                    case "amount":
                        _InternalEffect.SetValue(Amount, (float)data.Data[i].Value);
                        break;

                }
            }

            _InternalEffect.CommitChanges();

        }


        public override void Begin()
        {
            base.Begin();

        }

        public static EffectData GetData(float amount)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("amount", amount));

            return new EffectData("Blur", dt.ToArray());
        }

    }
}

